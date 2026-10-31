using LampShade.BuildingBlocks.Application;
using CommentManagement.Contracts;
using DiscountManagement.Contracts;
using InventoryManagement.Contracts;
using ShopManagement.Application.ReadModel.Product;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Infrastructure.EFCore;

namespace ShopManagement.Infrastructure.ReadModel;

public class ProductQuery(ShopContext shopContext, IInventoryModuleApi inventoryApi, IDiscountModuleApi discountApi, ICommentModuleApi commentApi) : IProductQuery
{
    public async Task<ProductQueryModel> GetProductDetailsAsync(string slug, CancellationToken cancellationToken = default)
    {
        var inventories = (await inventoryApi.GetStockAsync(null, cancellationToken)).Values.ToList();
        var discounts = await discountApi.GetActiveCustomerDiscountsAsync(cancellationToken);
        var entity = await shopContext.Products.AsNoTracking().Include(item => item.Category).Include(item => item.ProductPictures).FirstOrDefaultAsync(item => item.Slug == slug, cancellationToken);
        if (entity is null) return new ProductQueryModel();
        var product = MapProduct(entity, true);
        product.Comments = await commentApi.GetConfirmedCommentsAsync(CommentOwnerType.Product, product.Id, cancellationToken);
        ApplyPricing(product, inventories, discounts);
        return product;
    }

    public async Task<List<ProductQueryModel>> GetLatestArrivalsAsync(CancellationToken cancellationToken = default)
    {
        var inventories = ((await inventoryApi.GetStockAsync(null, cancellationToken)).Values.ToList()).Where(item => item.InStock).ToList();
        var discounts = await discountApi.GetActiveCustomerDiscountsAsync(cancellationToken);
        var products = await shopContext.Products.AsNoTracking().Include(item => item.Category).Include(item => item.ProductPictures).ToListAsync(cancellationToken);
        var result = products.Select(item => MapProduct(item, false)).OrderByDescending(item => item.Id).ToList();
        foreach (var product in result) ApplyPricing(product, inventories, discounts);
        return result;
    }

    public async Task<List<ProductQueryModel>> SearchAsync(string value, CancellationToken cancellationToken = default)
    {
        var inventories = (await inventoryApi.GetStockAsync(null, cancellationToken)).Values.ToList();
        var discounts = await discountApi.GetActiveCustomerDiscountsAsync(cancellationToken);
        var query = shopContext.Products.AsNoTracking().Include(item => item.Category).AsQueryable();
        if (!string.IsNullOrWhiteSpace(value)) query = query.Where(item => item.Name.Contains(value) || item.ShortDescription.Contains(value));
        var products = await query.OrderByDescending(item => item.Id).ToListAsync(cancellationToken);
        var result = products.Select(item => MapProduct(item, false)).ToList();
        foreach (var product in result) ApplyPricing(product, inventories, discounts);
        return result;
    }

    public async Task<List<CartItem>> CheckInventoryStatusAsync(List<CartItem> cartItems, CancellationToken cancellationToken = default)
    {
        var inventory = (await inventoryApi.GetStockAsync(null, cancellationToken)).Values.ToList();
        foreach (var item in cartItems)
        {
            var entry = inventory.FirstOrDefault(record => record.ProductId == item.Id && record.InStock);
            if (entry is not null) item.IsInStock = entry.CurrentCount >= item.Count;
        }
        return cartItems;
    }

    private static ProductQueryModel MapProduct(ShopManagement.Domain.ProductAgg.Product product, bool includeDetails) => new() { Id = product.Id, Slug = product.Slug, Code = product.Code, Name = product.Name, Keywords = includeDetails ? product.Keywords : string.Empty, PictureUrl = product.PictureUrl, PictureAlt = product.PictureAlt, PictureTitle = product.PictureTitle, Category = product.Category.Name, CategorySlug = product.Category.Slug, Description = includeDetails ? product.Description : string.Empty, MetaDescription = includeDetails ? product.MetaDescription : string.Empty, ShortDescription = product.ShortDescription, Pictures = product.ProductPictures.Select(item => new ProductPictureQueryModel { PictureAlt = item.PictureAlt, IsRemoved = item.IsRemoved, PictureTitle = item.PictureTitle, PictureUrl = item.PictureUrl, ProductId = item.ProductId }).Where(item => !item.IsRemoved).ToList() };
    private static void ApplyPricing(ProductQueryModel product, IEnumerable<ProductStock> inventories, IEnumerable<ProductDiscount> discounts)
    {
        var inventory = inventories.FirstOrDefault(item => item.ProductId == product.Id); if (inventory is null) { product.Price = "0"; product.InStock = false; return; }
        var price = inventory.UnitPrice; product.Price = price.ToMoney(); product.DoublePrice = price; product.InStock = inventory.InStock;
        var discount = discounts.FirstOrDefault(item => item.ProductId == product.Id); if (discount is null) return;
        product.DiscountRate = discount.DiscountRate; product.DiscountExpireDate = discount.EndDate.ToDiscountFormat(); product.HasDiscount = discount.DiscountRate > 0; product.PriceWithDiscount = (price - Math.Round(price * discount.DiscountRate / 100)).ToMoney();
    }
}
using _0_Framework.Application;
using CommentManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using LampShade.ReadModel.Contracts.Comment;
using LampShade.ReadModel.Contracts.Product;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Infrastructure.EFCore;

namespace LampShade.ReadModel.Application.Query;

public class ProductQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext, CommentContext commentContext) : IProductQuery
{
    public async Task<ProductQueryModel> GetProductDetailsAsync(string slug, CancellationToken cancellationToken = default)
    {
        var inventories = await inventoryContext.Inventory.AsNoTracking().Select(x => new { x.ProductId, x.InStock, x.UnitPrice }).ToListAsync(cancellationToken);
        var discounts = await ActiveDiscounts(cancellationToken);
        var entity = await shopContext.Products.AsNoTracking().Include(x => x.Category).Include(x => x.ProductPictures).FirstOrDefaultAsync(x => x.Slug == slug, cancellationToken);
        if (entity is null) return new ProductQueryModel();
        var product = MapProduct(entity, includeDetails: true);
        product.Comments = await commentContext.Comments.AsNoTracking().Where(x => x.Type == CommentType.Product && !x.IsCanceled && x.IsConfirmed && x.OwnerRecordId == product.Id).Select(x => new CommentQueryModel { Id = x.Id, Description = x.Description, Rating = x.Rating, Name = x.Name, CreationDate = x.CreationDate.ToFarsi() }).OrderByDescending(x => x.Id).ToListAsync(cancellationToken);
        ApplyPricing(product, inventories, discounts);
        return product;
    }

    public async Task<List<ProductQueryModel>> GetLatestArrivalsAsync(CancellationToken cancellationToken = default)
    {
        var inventories = await inventoryContext.Inventory.AsNoTracking().Where(x => x.InStock).Select(x => new { x.ProductId, x.InStock, x.UnitPrice }).ToListAsync(cancellationToken);
        var discounts = await ActiveDiscounts(cancellationToken);
        var products = await shopContext.Products.AsNoTracking().Include(x => x.Category).Include(x => x.ProductPictures).ToListAsync(cancellationToken);
        var result = products.Select(x => MapProduct(x, includeDetails: false)).OrderByDescending(x => x.Id).ToList();
        foreach (var product in result) ApplyPricing(product, inventories, discounts);
        return result;
    }

    public async Task<List<ProductQueryModel>> SearchAsync(string value, CancellationToken cancellationToken = default)
    {
        var inventories = await inventoryContext.Inventory.AsNoTracking().Select(x => new { x.ProductId, x.InStock, x.UnitPrice }).ToListAsync(cancellationToken);
        var discounts = await ActiveDiscounts(cancellationToken);
        var query = shopContext.Products.AsNoTracking().Include(x => x.Category).AsQueryable();
        if (!string.IsNullOrWhiteSpace(value)) query = query.Where(x => x.Name.Contains(value) || x.ShortDescription.Contains(value));
        var products = await query.OrderByDescending(x => x.Id).ToListAsync(cancellationToken);
        var result = products.Select(x => MapProduct(x, includeDetails: false)).ToList();
        foreach (var product in result) ApplyPricing(product, inventories, discounts);
        return result;
    }

    public async Task<List<CartItem>> CheckInventoryStatusAsync(List<CartItem> cartItems, CancellationToken cancellationToken = default)
    {
        var inventory = await inventoryContext.Inventory.AsNoTracking().ToListAsync(cancellationToken);
        foreach (var item in cartItems)
        {
            var entry = inventory.FirstOrDefault(x => x.ProductId == item.Id && x.InStock);
            if (entry is not null) item.IsInStock = entry.CalculateCurrentCount() >= item.Count;
        }
        return cartItems;
    }

    private async Task<List<dynamic>> ActiveDiscounts(CancellationToken cancellationToken) => (await discountContext.CustomerDiscounts.AsNoTracking().Where(x => x.StartDate < DateTime.UtcNow && x.EndDate > DateTime.UtcNow).Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToListAsync(cancellationToken)).Cast<dynamic>().ToList();
    private static ProductQueryModel MapProduct(ShopManagement.Domain.ProductAgg.Product product, bool includeDetails) => new() { Id = product.Id, Slug = product.Slug, Code = product.Code, Name = product.Name, Keywords = includeDetails ? product.Keywords : string.Empty, PictureUrl = product.PictureUrl, PictureAlt = product.PictureAlt, PictureTitle = product.PictureTitle, Category = product.Category.Name, CategorySlug = product.Category.Slug, Description = includeDetails ? product.Description : string.Empty, MetaDescription = includeDetails ? product.MetaDescription : string.Empty, ShortDescription = product.ShortDescription, Pictures = product.ProductPictures.Select(x => new ProductPictureQueryModel { PictureAlt = x.PictureAlt, IsRemoved = x.IsRemoved, PictureTitle = x.PictureTitle, PictureUrl = x.PictureUrl, ProductId = x.ProductId }).Where(x => !x.IsRemoved).ToList() };
    private static void ApplyPricing(ProductQueryModel product, IEnumerable<dynamic> inventories, IEnumerable<dynamic> discounts)
    {
        var inventory = inventories.FirstOrDefault(x => x.ProductId == product.Id); if (inventory is null) { product.Price = "0"; product.InStock = false; return; }
        var price = inventory.UnitPrice; product.Price = price.ToMoney(); product.DoublePrice = price; product.InStock = inventory.InStock;
        var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id); if (discount is null) return;
        product.DiscountRate = discount.DiscountRate; product.DiscountExpireDate = discount.EndDate.ToDiscountFormat(); product.HasDiscount = discount.DiscountRate > 0; product.PriceWithDiscount = (price - Math.Round(price * discount.DiscountRate / 100)).ToMoney();
    }
}
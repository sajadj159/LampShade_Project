using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Repository;
using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using ShopManagement.Infrastructure.EFCore;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class ColleagueDiscountRepository : RepositoryBase<long, ColleagueDiscount>, IColleagueDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly ShopContext _shopContext;
        public ColleagueDiscountRepository(DiscountContext context, ShopContext shopContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _context.ColleagueDiscounts.Select(x => new EditColleagueDiscount
            {
                Id = x.Id,
                ProductId = x.ProductId,
                DiscountRate = x.DiscountRate
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _context.ColleagueDiscounts
                .Select(x => new ColleagueDiscountViewModel
                {
                    ProductId = x.ProductId,
                    CreationDate = x.CreationDate.ToFarsi(),
                    DiscountRate = x.DiscountRate,
                    IsRemoved = x.IsRemoved
                });
            if (searchModel.ProductId > 0)
            {
                query = query.Where(x => x.ProductId == searchModel.ProductId && x.IsRemoved == false);
            }

            var discounts = query.OrderByDescending(x => x.Id).ToList();
            discounts.ForEach(discount => discount.Product = products.FirstOrDefault(x => x.Id == discount.ProductId)?.Name);
            return discounts;
        }
    }
}
﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Repository;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.A.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductRepository : RepositoryBase<long, Product>, IProductRepository
    {
        private readonly ShopContext _context;

        public ProductRepository(ShopContext context) : base(context)
        {
            _context = context;
        }
        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            var queryable = _context.Products
                .Include(x => x.Category)
                .Select(x => new ProductViewModel()
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    IsInStock = x.IsInStock,
                    UnitPrice = x.UnitPrice,
                    Category = x.Category.Name,
                    PictureUrl = x.PictureUrl ,
                    CreationDate = x.CreationDate.ToFarsi()
                });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                queryable = queryable.Where(x => x.Name.Contains(searchModel.Name));
            if (!string.IsNullOrWhiteSpace(searchModel.Code))
                queryable = queryable.Where(x => x.Code.Contains(searchModel.Code));
            if (searchModel.CategoryId != 0)
                queryable = queryable.Where(x => x.CategoryId == searchModel.CategoryId);

            return queryable.OrderByDescending(x => x.Id).ToList();
        }

        public EditProduct GetDetails(long id)
        {
            return _context.Products
                .Select(x => new EditProduct
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    Keywords = x.Keywords,
                    PictureAlt = x.PictureAlt,
                    CategoryId = x.CategoryId,  
                    UnitPrice = x.UnitPrice,
                    ShortDescription = x.ShortDescription,
                    Slug = x.Slug,
                    PictureUrl = x.PictureUrl,
                    Description = x.Description,
                    PictureTitle = x.PictureTitle,
                    MetaDescription = x.MetaDescription,
                }).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _context.Products.Select(x=>new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

        }
    }
}
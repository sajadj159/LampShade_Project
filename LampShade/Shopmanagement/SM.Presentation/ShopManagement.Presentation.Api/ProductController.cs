using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using _01_LampShadeQuery.Contract.Product;

namespace ShopManagement.Presentation.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductQuery _productQuery;

        public ProductController(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        [HttpGet]
        public List<ProductQueryModel> LatestArrivals()
        {
            return _productQuery.GetLatestArrivals();
        }

        [HttpGet("{value}")]
        public List<ProductQueryModel> SearchBy(string value)
        {
            return _productQuery.Search(value);
        }
    }
}

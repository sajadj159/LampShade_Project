using System.Collections.Generic;
using System.Linq;
using _01_LampShadeQuery.Contract.Account;
using _01_LampShadeQuery.Contract.ArticleCategory;
using _01_LampShadeQuery.Contract.Comment;
using _01_LampShadeQuery.Contract.Order;
using _01_LampShadeQuery.Contract.ProductCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ServiceHost.Areas.Administration.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductCategoryQuery _productCategoryQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly IOrderQuery _orderQuery;
        private readonly ICommentQuery _commentQuery;
        private readonly IAccountQuery _accountQuery;
        public Chart DoughnutDataSet { get; set; }
        public List<Chart> BarLineDataSet { get; set; }
        public List<Chart> LineDataSet { get; set; }
        public List<OrderQueryModel> OrderQueryModel { get; set; }
        public List<CommentQueryModel> CommentQueryModels { get; set; }
        public List<AccountQueryModel> AccountQueryModels { get; set; }
        public double TotalPayAmount;

        public IndexModel(IProductCategoryQuery productCategoryQuery, IArticleCategoryQuery articleCategoryQuery, IOrderQuery orderQuery, ICommentQuery commentQuery, IAccountQuery accountQuery)
        {
            _productCategoryQuery = productCategoryQuery;
            _articleCategoryQuery = articleCategoryQuery;
            _orderQuery = orderQuery;
            _commentQuery = commentQuery;
            _accountQuery = accountQuery;
        }

        public void OnGet()
        {
            var productCategoriesWithProducts = _productCategoryQuery.GetProductCategoriesWithProducts();
            BarLineDataSet = new List<Chart>();
            BarLineDataSet.Add(new Chart
            {
                BackgroundColor = new[] { "#fec5bb" },
                BorderColor = "#d8e2dc",
                Data = new List<int>() ,
                Label = "گروه محصولات"
            });

            foreach (var productCategoriesWithProduct in productCategoriesWithProducts)
            {
                BarLineDataSet.First().Data.Add(productCategoriesWithProduct.Products.Count);
            }

            var articleCategoryQueryModels = _articleCategoryQuery.GetArticleCategories();
            LineDataSet = new List<Chart>();
            LineDataSet.Add(new Chart
            {
                BackgroundColor = new[] {"#fec5bb"},
                BorderColor = "#d8e2dc",
                Data = new List<int>(),
                Label = "گروه مقالات"
            });
            foreach (var model in articleCategoryQueryModels)
            {
                LineDataSet.First().Data.Add(model.Articles.Count);
            }
            
          
            DoughnutDataSet = new Chart
            {
                Label = "Apple",
                Data = new List<int> {100, 200, 300, 250, 50, 300},
                BackgroundColor = new[] {"#03045e", "#fec5bb", "#fca311", "#fb8500", "#ff006e", "#17c3b2"},
                BorderColor = "#f8f9fa"
            };

            OrderQueryModel = _orderQuery.GetPayedOrders();
            foreach (var orderQueryModel in OrderQueryModel)
            {
                orderQueryModel.PayAmount = orderQueryModel.PayAmount + TotalPayAmount;

                TotalPayAmount = orderQueryModel.PayAmount;
            }

            CommentQueryModels = _commentQuery.GetComments();
            AccountQueryModels = _accountQuery.GetAccounts();
        }

    }

    public class Chart
    {
        [JsonProperty("label")] public string Label { get; set; }

        [JsonProperty("data")] public List<int> Data { get; set; }

        [JsonProperty("backgroundColor")] public string[] BackgroundColor { get; set; }

        [JsonProperty("borderColor")] public string BorderColor { get; set; }
    }
}
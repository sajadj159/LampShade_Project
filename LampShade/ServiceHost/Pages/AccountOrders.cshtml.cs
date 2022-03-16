using System.Collections.Generic;
using _0_Framework.Application;
using _01_LampShadeQuery.Contract.Order;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class AccountOrdersModel : PageModel
    {
        public List<OrderQueryModel> OrderQueryModels { get; set; }
        public AuthViewModel AuthViewModel { get; set; }
        private readonly IAuthHelper _authHelper;
        private readonly IOrderQuery _orderQuery;

        public AccountOrdersModel(IAuthHelper authHelper, IOrderQuery orderQuery)
        {
            _authHelper = authHelper;
            _orderQuery = orderQuery;
        }

        public void OnGet()
        {
            if (!_authHelper.IsAuthenticated())
            {
                return;
            }

            AuthViewModel = _authHelper.CurrentAccountInfo();
            OrderQueryModels = _orderQuery.GetOrders(AuthViewModel.Id);
        }
    }
}

using System.Collections.Generic;
using _0_Framework.Application;
using DiscountManagement.Application.Contract.AC.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Application.A.CustomerDiscount
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }

        public OperationResult Define(DefineCustomerDiscount command)
        {
            var operationResult = new OperationResult();
            if (_customerDiscountRepository.Exist(x=>x.Reason==command.Reason))
                operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            var customerDiscount = new Domain.CustomerDiscountAgg.CustomerDiscount(command.ProductId,command.DiscountRate,command.StartDate,command.EndDate,command.Reason);
            _customerDiscountRepository.Create(customerDiscount);
            _customerDiscountRepository.Save();
           return operationResult.Succeeded();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            throw new System.NotImplementedException();
        }

        public List<CustomerDiscountViewmodel> Search(CustomerDiscountSearchModel searchModel)
        {
            throw new System.NotImplementedException();
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}
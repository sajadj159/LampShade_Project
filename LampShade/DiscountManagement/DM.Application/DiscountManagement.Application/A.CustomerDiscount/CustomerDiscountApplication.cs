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
            if (_customerDiscountRepository.Exist(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            var customerDiscount = new Domain.CustomerDiscountAgg.CustomerDiscount(command.ProductId, command.DiscountRate, startDate, endDate, command.Reason);
            _customerDiscountRepository.Create(customerDiscount);
            _customerDiscountRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operationResult = new OperationResult();
            var customerDiscount = _customerDiscountRepository.Get(command.Id);
            if (customerDiscount == null)
            {
                operationResult.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_customerDiscountRepository.Exist(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate && x.Id != command.Id))
            {
                operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            customerDiscount.Edit(command.ProductId, command.DiscountRate, startDate, endDate, command.Reason);
            _customerDiscountRepository.Save();
            return operationResult.Succeeded();


        }

        public List<CustomerDiscountViewmodel> Search(CustomerDiscountSearchModel searchModel)
        {
            return _customerDiscountRepository.Search(searchModel);
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _customerDiscountRepository.GetDetails(id);
        }
    }
}
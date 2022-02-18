using System.Collections.Generic;
using _0_Framework.Application;
using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;

namespace DiscountManagement.Application.A.ColleagueDiscount
{
    public class ColleagueDiscountApplication:IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _colleagueDiscountRepository = colleagueDiscountRepository;
        }

        public OperationResult Define(DefineColleagueDiscount command)
        {
            var operationResult = new OperationResult();
            if (_colleagueDiscountRepository.Exist(x =>
                    x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            var colleagueDiscount = new Domain.ColleagueDiscountAgg.ColleagueDiscount(command.ProductId,command.DiscountRate);
            _colleagueDiscountRepository.Create(colleagueDiscount);
            _colleagueDiscountRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var operationResult = new OperationResult();
            var colleagueDiscount = _colleagueDiscountRepository.Get(command.Id);
            if (colleagueDiscount==null)
            {
                operationResult.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_colleagueDiscountRepository.Exist(x=>x.ProductId==command.ProductId&&x.DiscountRate==command.DiscountRate&&x.Id!=command.Id))
            {
                operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            }
            colleagueDiscount.Edit(command.ProductId,command.DiscountRate);
            _colleagueDiscountRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Remove(long id)
        {
            var operationResult = new OperationResult();
            var colleagueDiscount = _colleagueDiscountRepository.Get(id);
            if (colleagueDiscount==null)
            {
                operationResult.Failed(ApplicationMessages.RecordNotFound);
            }
            colleagueDiscount.Remove();
            _colleagueDiscountRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operationResult = new OperationResult();
            var colleagueDiscount = _colleagueDiscountRepository.Get(id);
            if (colleagueDiscount==null)
            {
                operationResult.Failed(ApplicationMessages.RecordNotFound);
            }
            colleagueDiscount.Restore();
            _colleagueDiscountRepository.Save();
            return operationResult.Succeeded();
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            return _colleagueDiscountRepository.Search(searchModel);
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _colleagueDiscountRepository.GetDetails(id);
        }
    }
}
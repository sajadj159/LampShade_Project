using System.Collections.Generic;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.AC.Account;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Application.A.Account
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IFIleUploader _uploader;

        public AccountApplication(IAccountRepository accountRepository, IFIleUploader uploader)
        {
            _accountRepository = accountRepository;
            _uploader = uploader;
        }

        public OperationResult Create(CreateAccount command)
        {
            var operationResult = new OperationResult();
            if (_accountRepository.Exist(x=>x.UserName==command.UserName))
            {
                operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var profilePath = _uploader.Upload(command.ProfilePhoto,"Account");
            var account = new Domain.AccountAgg.Account(command.UserName,command.FullName,command.Password,command.Mobile,command.RoleId,profilePath);
            _accountRepository.Create(account);
            _accountRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditAccount command)
        {
            var operationResult = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            if (account==null)
            {
                operationResult.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_accountRepository.Exist(x=>x.UserName==command.UserName&&x.Id!=command.Id))
            {
                operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var profilePath = _uploader.Upload(command.ProfilePhoto,"Account");
            account.Edit(command.UserName,command.FullName,command.Mobile,command.RoleId,profilePath);
            _accountRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var operationResult = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            if (account==null)
            {
                operationResult.Failed(ApplicationMessages.RecordNotFound);
            }

            if (command.Password!=command.RePassword)
            {
                operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            }
            account.ChangePassword(command.Password);
            _accountRepository.Save();
            return operationResult.Succeeded();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _accountRepository.Search(searchModel);
        }

        public EditAccount GetDetails(long id)
        {
            return _accountRepository.GetDetails(id);
        }
    }
}
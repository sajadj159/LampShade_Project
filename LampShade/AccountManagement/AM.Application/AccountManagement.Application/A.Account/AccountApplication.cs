using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.AC.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application.A.Account
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAccountRepository _accountRepository;
        private readonly IFIleUploader _uploader;
        private readonly IAuthHelper _authHelper;
        private readonly IRoleRepository _roleRepository;
        public AccountApplication(IAccountRepository accountRepository, IFIleUploader uploader, IPasswordHasher passwordHasher, IAuthHelper authHelper, IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _uploader = uploader;
            _passwordHasher = passwordHasher;
            _authHelper = authHelper;
            _roleRepository = roleRepository;
        }

        public OperationResult Register(RegisterAccount command)
        {
            var operationResult = new OperationResult();
            if (_accountRepository.Exist(x => x.UserName == command.UserName || x.Mobile == command.Mobile))
            {
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var passwordHash = _passwordHasher.Hash(command.Password);
            var path = $"profilePhotos";
            var profilePath = _uploader.Upload(command.ProfilePhoto, path);
            var account = new Domain.AccountAgg.Account(command.UserName, command.FullName, passwordHash, command.Mobile, command.RoleId, profilePath);
            _accountRepository.Create(account);
            _accountRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditAccount command)
        {
            var operationResult = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            if (account == null)
            {
                return operationResult.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_accountRepository.Exist(x => (x.UserName == command.UserName || x.Mobile == command.Mobile) && x.Id != command.Id))
            {
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var path = $"profilePhotos";
            var profilePath = _uploader.Upload(command.ProfilePhoto, path);
            account.Edit(command.UserName, command.FullName, command.Mobile, command.RoleId, profilePath);
            _accountRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var operationResult = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            if (account == null)
            {
                return operationResult.Failed(ApplicationMessages.RecordNotFound);
            }

            if (command.Password != command.RePassword)
            {
                return operationResult.Failed(ApplicationMessages.PasswordNotMatch);
            }

            var passwordHash = _passwordHasher.Hash(command.Password);
            account.ChangePassword(passwordHash);
            _accountRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Login(Login command)
        {
            var operationResult = new OperationResult();
            var account = _accountRepository.GetBy(command.UserName);
            if (account==null)
            {
                return operationResult.Failed(ApplicationMessages.WrongUserPass);
            }

            var result = _passwordHasher.Check(account.Password,command.Password);
            if (!result.Verified)
                return operationResult.Failed(ApplicationMessages.WrongUserPass);

            var permissions = _roleRepository.Get(account.RoleId)
                .Permissions
                .Select(x => x.Code)
                .ToList();
           
            var authViewModel = new AuthViewModel(account.Id,account.UserName,account.FullName,account.Mobile,account.RoleId,permissions);

            _authHelper.Signin(authViewModel);
            return operationResult.Succeeded();
        }

        public OperationResult MakeAddress(MakeAddress command)
        {
            var operationResult = new OperationResult();
            var account = _accountRepository.Get(command.AccountId);
            if (account==null)
            {
                return operationResult.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_accountRepository.Exist(x=>x.Address==command.Address&&x.PostalCode==command.PostalCode))
            {
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            }
            account.MakeAddress(command.Address,command.PostalCode);
            _accountRepository.Save();
            return operationResult.Succeeded();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _accountRepository.Search(searchModel);
        }

        public List<AccountViewModel> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        public EditAccount GetDetails(long id)
        {
            return _accountRepository.GetDetails(id);
        }

        public MakeAddress GetAddressBy(long id)
        {
            return _accountRepository.GetAddressBy(id);
        }

        public AccountViewModel GetAccountBy(long id)
        {
            var account = _accountRepository.Get(id);
            return new AccountViewModel
            {
                FullName = account.FullName,
                Mobile = account.Mobile,
            };
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }
    }
}
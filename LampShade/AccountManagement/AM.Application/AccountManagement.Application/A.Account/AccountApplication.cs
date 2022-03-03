using System.Collections.Generic;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.AC.Account;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Application.A.Account
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAccountRepository _accountRepository;
        private readonly IFIleUploader _uploader;
        private readonly IAuthHelper _authHelper;

        public AccountApplication(IAccountRepository accountRepository, IFIleUploader uploader, IPasswordHasher passwordHasher, IAuthHelper authHelper)
        {
            _accountRepository = accountRepository;
            _uploader = uploader;
            _passwordHasher = passwordHasher;
            _authHelper = authHelper;
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

            (bool Verified, bool NeedsUpgrade) result = _passwordHasher.Check(account.Password,command.Password);
            if (!result.Verified)
                return operationResult.Failed(ApplicationMessages.WrongUserPass);
            var authViewModel = new AuthViewModel(account.Id,account.UserName,account.FullName,account.RoleId);
            _authHelper.Signin(authViewModel);
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

        public void Logout()
        {
            _authHelper.SignOut();
        }
    }
}
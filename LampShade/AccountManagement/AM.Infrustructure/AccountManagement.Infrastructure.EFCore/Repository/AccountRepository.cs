﻿using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Repository;
using AccountManagement.Application.Contracts.AC.Account;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
    {
        private readonly AccountContext _context;
        public AccountRepository(AccountContext context) : base(context)
        {
            _context = context;
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            var queryable = _context.Accounts.Select(x => new AccountViewModel
            {
                Id = x.Id,
                UserName = x.UserName,
                FullName = x.FullName,
                Mobile = x.Mobile,
                ProfilePhoto = x.ProfilePhoto,
                RoleId = 2,
                Role = "مدیر سیستم",
                CreationDate = x.CreationDate.ToFarsi()

            });
            if (!string.IsNullOrWhiteSpace(searchModel.UserName))
            {
                queryable = queryable.Where(x => x.UserName.Contains(searchModel.UserName));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.FullName))
            {
                queryable = queryable.Where(x => x.FullName.Contains(searchModel.FullName));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
            {
                queryable = queryable.Where(x => x.Mobile.Contains(searchModel.Mobile));
            }

            if (searchModel.RoleId > 0)
            {
                queryable = queryable.Where(x => x.RoleId == searchModel.RoleId);
            }

            return queryable.OrderByDescending(x => x.Id).ToList();
        }

        public EditAccount GetDetails(long id)
        {
            return _context.Accounts
                .Select(x => new EditAccount
                {
                    UserName = x.UserName,
                    FullName = x.FullName,
                    Mobile = x.Mobile,
                    RoleId = x.RoleId,
                    Id = x.Id
                }).FirstOrDefault(x => x.Id == id);
        }
    }
}
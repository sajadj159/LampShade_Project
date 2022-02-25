using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Repository;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.A.Comment;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
    {
        private readonly ShopContext _Context;
        public CommentRepository(ShopContext context) : base(context)
        {
            _Context = context;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var queryable = _Context.Comments
                .Include(x => x.Product)
                .Select(x => new CommentViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Description = x.Description,
                    IsCanceled = x.IsCanceled,
                    IsConfirmed = x.IsConfirmed,
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    CommentDate = x.CreationDate.ToFarsi()
                });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                queryable = queryable.Where(x => x.Name.Contains(searchModel.Name));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
            {
                queryable = queryable.Where(x => x.Email.Contains(searchModel.Email));
            }

            return queryable.OrderByDescending(x => x.Id).ToList();
        }
    }
}
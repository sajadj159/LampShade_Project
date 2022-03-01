using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Repository;
using CommentManagement.Application.Contract.A.Comment;
using CommentManagement.Domain.CommentAgg;

namespace CommentManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
    {
        private readonly CommentContext _context;
        public CommentRepository(CommentContext context) : base(context)
        {
            _context = context;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var queryable = _context.Comments
                .Select(x => new CommentViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Website = x.Website,
                    Description = x.Description,
                    IsCanceled = x.IsCanceled,
                    IsConfirmed = x.IsConfirmed,
                    CommentDate = x.CreationDate.ToFarsi(),
                    OwnerRecordId = x.OwnerRecordId,
                    Type = x.Type
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
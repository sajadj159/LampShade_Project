using System.Collections.Generic;
using System.Linq;
using _01_LampShadeQuery.Contract.Comment;
using CommentManagement.Infrastructure.EFCore;

namespace _01_LampShadeQuery.Query
{
    public class CommentQuery :ICommentQuery
    {
        private readonly CommentContext _commentContext;

        public CommentQuery(CommentContext commentContext)
        {
            _commentContext = commentContext;
        }

        public List<CommentQueryModel> GetComments()
        {
           return _commentContext.Comments.Select(x => new CommentQueryModel
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }
    }
}
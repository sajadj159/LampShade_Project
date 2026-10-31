using CommentManagement.Domain.CommentAgg;

namespace CommentManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository(CommentContext context) : RepositoryBase<long, Comment>(context), ICommentRepository
    {
    }
}

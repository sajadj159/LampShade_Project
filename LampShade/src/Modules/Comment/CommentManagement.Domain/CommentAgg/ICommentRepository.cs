using LampShade.BuildingBlocks.Domain;

namespace CommentManagement.Domain.CommentAgg
{
    public interface ICommentRepository:IRepository<long,Comment>
    {
    }
}

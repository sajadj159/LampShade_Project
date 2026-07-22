using System.Collections.Generic;

namespace LampShade.ReadModel.Contracts.Comment
{
    public interface ICommentQuery
    {
        List<CommentQueryModel> GetComments();
    }
}
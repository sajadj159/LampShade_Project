using System.Collections.Generic;

namespace _01_LampShadeQuery.Contract.Comment
{
    public interface ICommentQuery
    {
        List<CommentQueryModel> GetComments();
    }
}
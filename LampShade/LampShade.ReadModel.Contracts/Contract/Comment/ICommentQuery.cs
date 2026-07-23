using LampShade.ReadModel.Contracts.Comments.Dto;

namespace LampShade.ReadModel.Contracts.Comment;

public interface ICommentQuery
{
    List<CommentQueryModel> GetComments();
    List<CommentViewModel> SearchComments(string name, string email);
}

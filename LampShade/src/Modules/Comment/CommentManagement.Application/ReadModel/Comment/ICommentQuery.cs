using LampShade.ReadModel.Contracts.Comments.Dto;

namespace LampShade.ReadModel.Contracts.Comment;

public interface ICommentQuery
{
    Task<List<CommentQueryModel>> GetCommentsAsync(CancellationToken cancellationToken = default);
    Task<List<CommentViewModel>> SearchCommentsAsync(string name, string email, CancellationToken cancellationToken = default);
}
using CommentManagement.Contracts;
using CommentManagement.Application.ReadModel.Comments.Dto;

namespace CommentManagement.Application.ReadModel.Comment;

public interface ICommentQuery
{
    Task<List<CommentQueryModel>> GetCommentsAsync(CancellationToken cancellationToken = default);
    Task<List<CommentViewModel>> SearchCommentsAsync(string name, string email, CancellationToken cancellationToken = default);
}
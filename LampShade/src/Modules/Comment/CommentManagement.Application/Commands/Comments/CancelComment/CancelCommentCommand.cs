using CommentManagement.Application.Contract.A.Comment;
using MediatR;
using _0_Framework.Application;

namespace CommentManagement.Application.Contracts.Commands.Comments.CancelComment;

public class CancelCommentCommand : ICommand<OperationResult>
{
    public long Id { get; set; }
}

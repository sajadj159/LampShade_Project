using CommentManagement.Application.Contract.A.Comment;
using MediatR;
using _0_Framework.Application;

namespace CommentManagement.Application.Contracts.Commands.Comments.ConfirmComment;

public class ConfirmCommentCommand : IRequest<OperationResult>
{
    public long Id { get; set; }
}

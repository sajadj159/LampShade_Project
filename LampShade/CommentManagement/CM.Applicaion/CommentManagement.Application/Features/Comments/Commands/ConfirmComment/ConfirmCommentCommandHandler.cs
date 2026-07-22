using CommentManagement.Application.Contract.A.Comment;
using MediatR;
using _0_Framework.Application;

using CommentManagement.Application.Contracts.Commands.Comments.ConfirmComment;

namespace CommentManagement.Application.Features.Comments.Commands.ConfirmComment;



public class ConfirmCommentCommandHandler : IRequestHandler<ConfirmCommentCommand, OperationResult>
{
    private readonly ICommentApplication _commentApplication;

    public ConfirmCommentCommandHandler(ICommentApplication commentApplication)
    {
        _commentApplication = commentApplication;
    }

    public Task<OperationResult> Handle(ConfirmCommentCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_commentApplication.Confirm(request.Id));
    }
}

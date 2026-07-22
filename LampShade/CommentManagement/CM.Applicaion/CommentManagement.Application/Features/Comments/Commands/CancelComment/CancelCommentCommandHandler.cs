using CommentManagement.Application.Contract.A.Comment;
using MediatR;
using _0_Framework.Application;

using CommentManagement.Application.Contracts.Commands.Comments.CancelComment;

namespace CommentManagement.Application.Features.Comments.Commands.CancelComment;



public class CancelCommentCommandHandler : IRequestHandler<CancelCommentCommand, OperationResult>
{
    private readonly ICommentApplication _commentApplication;

    public CancelCommentCommandHandler(ICommentApplication commentApplication)
    {
        _commentApplication = commentApplication;
    }

    public Task<OperationResult> Handle(CancelCommentCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_commentApplication.Cancel(request.Id));
    }
}

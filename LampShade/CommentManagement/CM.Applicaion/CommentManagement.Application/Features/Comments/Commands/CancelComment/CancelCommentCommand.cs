using CommentManagement.Application.Contract.A.Comment;
using MediatR;
using _0_Framework.Application;

namespace CommentManagement.Application.Features.Comments.Commands.CancelComment;

public class CancelCommentCommand : IRequest<OperationResult>
{
    public long Id { get; set; }
}

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

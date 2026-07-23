using _0_Framework.Application;
using CommentManagement.Application.Contracts.Commands.Comments.CancelComment;
using CommentManagement.Domain.CommentAgg;
using MediatR;

namespace CommentManagement.Application.Features.Comments.Commands.CancelComment;

public class CancelCommentCommandHandler(ICommentRepository comments) : IRequestHandler<CancelCommentCommand, OperationResult>
{
    public Task<OperationResult> Handle(CancelCommentCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        var comment = comments.Get(request.Id);
        if (comment is null) return Task.FromResult(operation.Failed(ApplicationMessages.RecordNotFound));
        comment.Cancel();
        comments.Save();
        return Task.FromResult(operation.Succeeded());
    }
}

using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using CommentManagement.Application.Contracts.Commands.Comments.CancelComment;
using CommentManagement.Domain.CommentAgg;
using MediatR;

namespace CommentManagement.Application.Features.Comments.Commands.CancelComment;

public class CancelCommentCommandHandler(ICommentRepository comments) : IRequestHandler<CancelCommentCommand, OperationResult>
{
    public async Task<OperationResult> Handle(CancelCommentCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        var comment = await comments.GetAsync(request.Id, cancellationToken);
        if (comment is null) return operation.Failed(ApplicationMessages.RecordNotFound);
        comment.Cancel();
        return operation.Succeeded();
    }
}

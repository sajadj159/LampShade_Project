using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using CommentManagement.Application.Contracts.Commands.Comments.ConfirmComment;
using CommentManagement.Domain.CommentAgg;
using MediatR;

namespace CommentManagement.Application.Features.Comments.Commands.ConfirmComment;

public class ConfirmCommentCommandHandler(ICommentRepository comments) : IRequestHandler<ConfirmCommentCommand, OperationResult>
{
    public async Task<OperationResult> Handle(ConfirmCommentCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        var comment = await comments.GetAsync(request.Id, cancellationToken);
        if (comment is null) return operation.Failed(ApplicationMessages.RecordNotFound);
        comment.Confirm();
        return operation.Succeeded();
    }
}

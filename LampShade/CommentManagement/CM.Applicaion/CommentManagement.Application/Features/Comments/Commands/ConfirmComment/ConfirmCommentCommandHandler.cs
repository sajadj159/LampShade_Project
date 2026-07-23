using _0_Framework.Application;
using CommentManagement.Application.Contracts.Commands.Comments.ConfirmComment;
using CommentManagement.Domain.CommentAgg;
using MediatR;

namespace CommentManagement.Application.Features.Comments.Commands.ConfirmComment;

public class ConfirmCommentCommandHandler(ICommentRepository comments) : IRequestHandler<ConfirmCommentCommand, OperationResult>
{
    public Task<OperationResult> Handle(ConfirmCommentCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        var comment = comments.Get(request.Id);
        if (comment is null) return Task.FromResult(operation.Failed(ApplicationMessages.RecordNotFound));
        comment.Confirm();
        comments.Save();
        return Task.FromResult(operation.Succeeded());
    }
}

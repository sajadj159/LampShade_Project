using _0_Framework.Application;
using CommentManagement.Application.Contracts.Commands.Comments.AddComment;
using CommentManagement.Domain.CommentAgg;
using MediatR;

namespace CommentManagement.Application.Features.Comments.Commands.AddComment;

public class AddCommentCommandHandler(ICommentRepository comments) : IRequestHandler<AddCommentCommand, OperationResult>
{
    public Task<OperationResult> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        comments.Create(new Comment(request.Name, request.Email, request.Website, request.Description, request.Rating, request.OwnerRecordId, request.Type, request.ParentId));
        comments.Save();
        return Task.FromResult(new OperationResult().Succeeded());
    }
}

using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using CommentManagement.Application.Contracts.Commands.Comments.AddComment;
using CommentManagement.Domain.CommentAgg;
using MediatR;

namespace CommentManagement.Application.Features.Comments.Commands.AddComment;

public class AddCommentCommandHandler(ICommentRepository comments) : IRequestHandler<AddCommentCommand, OperationResult>
{
    public async Task<OperationResult> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        comments.Add(new Comment(request.Name, request.Email, request.Website, request.Description, request.Rating, request.OwnerRecordId, request.Type, request.ParentId));
        return new OperationResult().Succeeded();
    }
}

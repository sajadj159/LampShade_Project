using CommentManagement.Application.Contract.A.Comment;
using MediatR;
using _0_Framework.Application;

namespace LampShade.Api.Features.Comments.Commands.AddComment;

public class AddCommentCommand : IRequest<OperationResult>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public long OwnerRecordId { get; set; }
    public int Type { get; set; }
    public long? ParentId { get; set; }
}

public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, OperationResult>
{
    private readonly ICommentApplication _commentApplication;

    public AddCommentCommandHandler(ICommentApplication commentApplication)
    {
        _commentApplication = commentApplication;
    }

    public async Task<OperationResult> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        var command = new CommentManagement.Application.Contract.A.Comment.AddComment
        {
            Name = request.Name,
            Email = request.Email,
            Description = request.Description,
            Website = request.Website,
            OwnerRecordId = request.OwnerRecordId,
            Type = request.Type,
            ParentId = request.ParentId ?? 0
        };
        return await Task.FromResult(_commentApplication.Add(command));
    }
}

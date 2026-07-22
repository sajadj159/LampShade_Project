using CommentManagement.Application.Contract.A.Comment;
using MediatR;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace CommentManagement.Application.Features.Comments.Commands.AddComment;

public class AddCommentCommand : IRequest<OperationResult>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    [Range(0, 5)]
    public int Rating { get; set; }
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

    public Task<OperationResult> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        var command = new CommentManagement.Application.Contract.A.Comment.AddComment
        {
            Name = request.Name,
            Email = request.Email,
            Description = request.Description,
            Website = request.Website,
            Rating = request.Rating,
            OwnerRecordId = request.OwnerRecordId,
            Type = request.Type,
            ParentId = request.ParentId
        };
        return Task.FromResult(_commentApplication.Add(command));
    }
}

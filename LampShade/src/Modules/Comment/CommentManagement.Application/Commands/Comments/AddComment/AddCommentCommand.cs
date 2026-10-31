#nullable enable

using MediatR;
using System.ComponentModel.DataAnnotations;
using LampShade.BuildingBlocks.Application;

namespace CommentManagement.Application.Contracts.Commands.Comments.AddComment;

public class AddCommentCommand : ICommand<OperationResult>
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

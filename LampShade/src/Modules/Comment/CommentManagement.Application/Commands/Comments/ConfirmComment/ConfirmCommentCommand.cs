using MediatR;
using LampShade.BuildingBlocks.Application;

namespace CommentManagement.Application.Contracts.Commands.Comments.ConfirmComment;

public class ConfirmCommentCommand : ICommand<OperationResult>
{
    public long Id { get; set; }
}

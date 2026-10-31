using MediatR;
using LampShade.BuildingBlocks.Application;

namespace CommentManagement.Application.Contracts.Commands.Comments.CancelComment;

public class CancelCommentCommand : ICommand<OperationResult>
{
    public long Id { get; set; }
}

#nullable enable

using CommentManagement.Application.Contract.A.Comment;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.Comments.SearchComments;

public class SearchCommentsQuery : IRequest<List<CommentViewModel>>
{
    public string? Name { get; set; }
    public string? Email { get; set; }
}

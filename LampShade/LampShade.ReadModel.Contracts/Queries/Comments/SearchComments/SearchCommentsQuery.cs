#nullable enable

using LampShade.ReadModel.Contracts.Comments.Dto;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.Comments.SearchComments;

public class SearchCommentsQuery : IRequest<List<CommentViewModel>>
{
    public string? Name { get; set; }
    public string? Email { get; set; }
}

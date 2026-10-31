#nullable enable

using CommentManagement.Application.ReadModel.Comments.Dto;
using MediatR;

namespace CommentManagement.Application.ReadModel.Queries.Comments.SearchComments;

public class SearchCommentsQuery : IRequest<List<CommentViewModel>>
{
    public string? Name { get; set; }
    public string? Email { get; set; }
}

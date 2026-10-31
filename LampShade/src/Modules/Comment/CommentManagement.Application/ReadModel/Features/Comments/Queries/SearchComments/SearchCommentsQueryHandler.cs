using System.Threading;
using System.Threading.Tasks;
using LampShade.ReadModel.Contracts.Comment;
using LampShade.ReadModel.Contracts.Comments.Dto;
using LampShade.ReadModel.Contracts.Queries.Comments.SearchComments;
using MediatR;

namespace LampShade.ReadModel.Application.Features.Comments.Queries.SearchComments;

public class SearchCommentsQueryHandler(ICommentQuery query) : IRequestHandler<SearchCommentsQuery, List<CommentViewModel>>
{
    public Task<List<CommentViewModel>> Handle(SearchCommentsQuery request, CancellationToken cancellationToken) =>
        query.SearchCommentsAsync(request.Name ?? string.Empty, request.Email ?? string.Empty, cancellationToken);
}

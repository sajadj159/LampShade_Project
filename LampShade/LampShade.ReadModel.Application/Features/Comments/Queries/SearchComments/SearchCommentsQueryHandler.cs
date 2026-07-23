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
        Task.FromResult(query.SearchComments(request.Name ?? string.Empty, request.Email ?? string.Empty));
}

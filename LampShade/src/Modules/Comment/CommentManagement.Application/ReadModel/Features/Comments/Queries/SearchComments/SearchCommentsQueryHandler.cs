using System.Threading;
using System.Threading.Tasks;
using CommentManagement.Application.ReadModel.Comment;
using CommentManagement.Application.ReadModel.Comments.Dto;
using CommentManagement.Application.ReadModel.Queries.Comments.SearchComments;
using MediatR;

namespace CommentManagement.Application.ReadModel.Features.Comments.Queries.SearchComments;

public class SearchCommentsQueryHandler(ICommentQuery query) : IRequestHandler<SearchCommentsQuery, List<CommentViewModel>>
{
    public Task<List<CommentViewModel>> Handle(SearchCommentsQuery request, CancellationToken cancellationToken) =>
        query.SearchCommentsAsync(request.Name ?? string.Empty, request.Email ?? string.Empty, cancellationToken);
}

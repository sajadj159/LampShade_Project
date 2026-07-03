using CommentManagement.Application.Contract.A.Comment;
using MediatR;

namespace LampShade.Api.Features.Comments.Queries.SearchComments;

public class SearchCommentsQuery : IRequest<List<CommentViewModel>>
{
    public string? Name { get; set; }
    public string? Email { get; set; }
}

public class SearchCommentsQueryHandler : IRequestHandler<SearchCommentsQuery, List<CommentViewModel>>
{
    private readonly ICommentApplication _commentApplication;

    public SearchCommentsQueryHandler(ICommentApplication commentApplication)
    {
        _commentApplication = commentApplication;
    }

    public async Task<List<CommentViewModel>> Handle(SearchCommentsQuery request, CancellationToken cancellationToken)
    {
        var searchModel = new CommentSearchModel
        {
            Name = request.Name,
            Email = request.Email
        };
        return await Task.FromResult(_commentApplication.Search(searchModel));
    }
}

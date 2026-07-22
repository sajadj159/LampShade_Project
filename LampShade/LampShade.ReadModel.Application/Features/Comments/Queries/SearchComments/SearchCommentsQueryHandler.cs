#nullable enable

using CommentManagement.Application.Contract.A.Comment;
using MediatR;

using LampShade.ReadModel.Contracts.Queries.Comments.SearchComments;

namespace LampShade.ReadModel.Application.Features.Comments.Queries.SearchComments;



public class SearchCommentsQueryHandler : IRequestHandler<SearchCommentsQuery, List<CommentViewModel>>
{
    private readonly ICommentApplication _commentApplication;

    public SearchCommentsQueryHandler(ICommentApplication commentApplication)
    {
        _commentApplication = commentApplication;
    }

    public Task<List<CommentViewModel>> Handle(SearchCommentsQuery request, CancellationToken cancellationToken)
    {
        var searchModel = new CommentSearchModel
        {
            Name = request.Name,
            Email = request.Email
        };
        return Task.FromResult(_commentApplication.Search(searchModel));
    }
}

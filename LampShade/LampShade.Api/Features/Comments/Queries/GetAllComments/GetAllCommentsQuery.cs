using MediatR;
using _01_LampShadeQuery.Contract.Comment;

namespace LampShade.Api.Features.Comments.Queries.GetAllComments;

public class GetAllCommentsQuery : IRequest<List<CommentQueryModel>> { }

public class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, List<CommentQueryModel>>
{
    private readonly ICommentQuery _query;
    public GetAllCommentsQueryHandler(ICommentQuery query) => _query = query;
    public async Task<List<CommentQueryModel>> Handle(GetAllCommentsQuery r, CancellationToken c) => await Task.FromResult(_query.GetComments());
}

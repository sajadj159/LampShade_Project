using MediatR;
using _01_LampShadeQuery.Contract.Comment;

namespace _01_LampShadeQuery.Features.Comments.Queries.GetAllComments;

public class GetAllCommentsQuery : IRequest<List<CommentQueryModel>> { }

public class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, List<CommentQueryModel>>
{
    private readonly ICommentQuery _query;
    public GetAllCommentsQueryHandler(ICommentQuery query) => _query = query;
    public Task<List<CommentQueryModel>> Handle(GetAllCommentsQuery r, CancellationToken c) => Task.FromResult(_query.GetComments());
}

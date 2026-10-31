using System.Threading;
using System.Threading.Tasks;
using MediatR;
using LampShade.ReadModel.Contracts.Comment;

using LampShade.ReadModel.Contracts.Queries.Comments.GetAllComments;

namespace LampShade.ReadModel.Application.Features.Comments.Queries.GetAllComments;



public class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, List<CommentQueryModel>>
{
    private readonly ICommentQuery _query;
    public GetAllCommentsQueryHandler(ICommentQuery query) => _query = query;
    public Task<List<CommentQueryModel>> Handle(GetAllCommentsQuery r, CancellationToken c) => _query.GetCommentsAsync(c);
}

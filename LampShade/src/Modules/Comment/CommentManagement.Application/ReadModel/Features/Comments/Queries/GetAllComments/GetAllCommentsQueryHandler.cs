using System.Threading;
using System.Threading.Tasks;
using MediatR;
using CommentManagement.Application.ReadModel.Comment;
using CommentManagement.Contracts;

using CommentManagement.Application.ReadModel.Queries.Comments.GetAllComments;

namespace CommentManagement.Application.ReadModel.Features.Comments.Queries.GetAllComments;



public class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, List<CommentQueryModel>>
{
    private readonly ICommentQuery _query;
    public GetAllCommentsQueryHandler(ICommentQuery query) => _query = query;
    public Task<List<CommentQueryModel>> Handle(GetAllCommentsQuery r, CancellationToken c) => _query.GetCommentsAsync(c);
}

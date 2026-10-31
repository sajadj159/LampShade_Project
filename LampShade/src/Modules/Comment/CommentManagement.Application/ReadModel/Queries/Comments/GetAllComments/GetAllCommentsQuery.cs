using MediatR;
using LampShade.ReadModel.Contracts.Comment;

namespace LampShade.ReadModel.Contracts.Queries.Comments.GetAllComments;

public class GetAllCommentsQuery : IRequest<List<CommentQueryModel>> { }

using MediatR;
using CommentManagement.Application.ReadModel.Comment;
using CommentManagement.Contracts;

namespace CommentManagement.Application.ReadModel.Queries.Comments.GetAllComments;

public class GetAllCommentsQuery : IRequest<List<CommentQueryModel>> { }

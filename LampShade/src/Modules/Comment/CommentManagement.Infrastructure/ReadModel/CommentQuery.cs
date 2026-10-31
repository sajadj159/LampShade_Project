using CommentManagement.Contracts;
using LampShade.BuildingBlocks.Application;
using CommentManagement.Infrastructure.EFCore;
using CommentManagement.Application.ReadModel.Comment;
using CommentManagement.Application.ReadModel.Comments.Dto;
using Microsoft.EntityFrameworkCore;

namespace CommentManagement.Infrastructure.ReadModel;

public class CommentQuery(CommentContext commentContext) : ICommentQuery
{
    public Task<List<CommentQueryModel>> GetCommentsAsync(CancellationToken cancellationToken = default) =>
        commentContext.Comments.AsNoTracking().Select(x => new CommentQueryModel { Id = x.Id, Name = x.Name }).ToListAsync(cancellationToken);

    public Task<List<CommentViewModel>> SearchCommentsAsync(string name, string email, CancellationToken cancellationToken = default)
    {
        var query = commentContext.Comments.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(name)) query = query.Where(x => x.Name.Contains(name));
        if (!string.IsNullOrWhiteSpace(email)) query = query.Where(x => x.Email.Contains(email));
        return query.Select(x => new CommentViewModel { Id = x.Id, Name = x.Name, Email = x.Email, Website = x.Website, Description = x.Description, Rating = x.Rating, OwnerRecordId = x.OwnerRecordId, IsConfirmed = x.IsConfirmed, IsCanceled = x.IsCanceled, Type = x.Type, CommentDate = x.CreationDate.ToFarsi() }).ToListAsync(cancellationToken);
    }
}
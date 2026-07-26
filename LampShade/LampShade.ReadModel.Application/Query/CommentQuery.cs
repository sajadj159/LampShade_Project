using _0_Framework.Application;
using CommentManagement.Infrastructure.EFCore;
using LampShade.ReadModel.Contracts.Comment;
using LampShade.ReadModel.Contracts.Comments.Dto;
using Microsoft.EntityFrameworkCore;

namespace LampShade.ReadModel.Application.Query;

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
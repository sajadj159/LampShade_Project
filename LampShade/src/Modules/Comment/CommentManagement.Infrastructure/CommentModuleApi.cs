using CommentManagement.Infrastructure.EFCore;
using CommentManagement.Contracts;
using LampShade.BuildingBlocks.Application;
using Microsoft.EntityFrameworkCore;

namespace CommentManagement.Infrastructure;

internal sealed class CommentModuleApi(CommentContext context) : ICommentModuleApi
{
    public async Task<List<CommentQueryModel>> GetConfirmedCommentsAsync(int ownerType, long ownerRecordId, CancellationToken cancellationToken = default)
    {
        var comments = await context.Comments.AsNoTracking()
            .Where(x => x.Type == ownerType && x.OwnerRecordId == ownerRecordId && x.IsConfirmed && !x.IsCanceled)
            .OrderByDescending(x => x.Id)
            .Select(x => new { x.Id, x.Name, x.Description, x.Rating, x.ParentId, x.CreationDate })
            .ToListAsync(cancellationToken);

        var result = comments.Select(x => new CommentQueryModel
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Rating = x.Rating,
            ParentId = x.ParentId,
            CreationDate = x.CreationDate.ToFarsi()
        }).ToList();

        foreach (var comment in result.Where(x => x.ParentId > 0))
            comment.ParentName = result.FirstOrDefault(x => x.Id == comment.ParentId)?.Name;

        return result;
    }
}

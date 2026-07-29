using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LampShade.ReadModel.Contracts.Acl;
using Microsoft.EntityFrameworkCore;

namespace CommentManagement.Infrastructure.EFCore.ReadAcl;

public sealed class ShopCommentReadAcl(CommentContext context) : IShopCommentReadAcl
{
    public Task<List<CommentReadModel>> GetConfirmedProductCommentsAsync(long productId, CancellationToken cancellationToken = default) =>
        context.Comments.AsNoTracking().Where(item => item.Type == CommentType.Product && !item.IsCanceled && item.IsConfirmed && item.OwnerRecordId == productId).OrderByDescending(item => item.Id).Select(item => new CommentReadModel(item.Id, item.Name, item.Description, item.Rating, item.CreationDate)).ToListAsync(cancellationToken);
}
namespace CommentManagement.Contracts;

/// <summary>The Comment module's public read API for other modules.</summary>
public interface ICommentModuleApi
{
    /// <summary>Confirmed, not cancelled comments of one record, newest first, with reply parents resolved.</summary>
    Task<List<CommentQueryModel>> GetConfirmedCommentsAsync(int ownerType, long ownerRecordId, CancellationToken cancellationToken = default);
}

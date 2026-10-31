namespace CommentManagement.Contracts;

/// <summary>What kind of record a comment belongs to; the record itself lives in another module.</summary>
public static class CommentOwnerType
{
    public const int Product = 1;
    public const int Article = 2;
}

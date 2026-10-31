namespace CommentManagement.Contracts;

/// <summary>A confirmed comment as other modules show it next to their own records.</summary>
public class CommentQueryModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Rating { get; set; }
    public string CreationDate { get; set; }
    public long? ParentId { get; set; }
    public string ParentName { get; set; }
}

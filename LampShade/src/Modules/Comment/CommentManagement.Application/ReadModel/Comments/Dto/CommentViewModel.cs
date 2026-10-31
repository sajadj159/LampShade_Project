namespace LampShade.ReadModel.Contracts.Comments.Dto;

public class CommentViewModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Rating { get; set; }
    public long OwnerRecordId { get; set; }
    public bool IsConfirmed { get; set; }
    public bool IsCanceled { get; set; }
    public int Type { get; set; }
    public string OwnerName { get; set; } = string.Empty;
    public string CommentDate { get; set; } = string.Empty;
}

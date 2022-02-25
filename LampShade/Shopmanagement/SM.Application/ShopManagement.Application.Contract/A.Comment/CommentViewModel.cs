namespace ShopManagement.Application.Contract.A.Comment
{
    public class CommentViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public long ProductId { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsCanceled { get; set; }
        public string ProductName { get; set; }
        public string CommentDate { get; set; }
    }
}
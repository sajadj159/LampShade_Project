namespace ShopManagement.Application.Contract.A.Comment
{
    public class AddComment
    {
        public string Name { get;  set; }
        public string Email { get;  set; }
        public string Description { get;  set; }
        public long ProductId { get;  set; }
    }
}
using _0_Framework.Domain;

namespace CommentManagement.Domain.CommentAgg
{
    public class Comment : EntityBase
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Website { get; private set; }
        public string Description { get; private set; }
        public bool IsConfirmed { get; private set; }
        public bool IsCanceled { get; private set; }
        public long OwnerRecordId { get; private set; }
        public int Type { get; private set; }
        public long ParentId { get; private set; }
        public Comment Parent { get; private set; }

        protected Comment()
        {
        }

        public Comment(string name, string email,string website, string description, long ownerRecordId,int type,long parentId)
        {
            Name = name;
            Email = email;
            Website = website;
            Description = description;
            OwnerRecordId = ownerRecordId;
            Type = type;
            ParentId = parentId;
        }

        public void Confirm()
        {
            IsConfirmed=true;
        }

        public void Cancel()
        {
            IsCanceled = true;
        }

    }
}
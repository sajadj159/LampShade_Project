using System;

namespace LampShade.BuildingBlocks.Domain
{
    public class EntityBase
    {
        public long Id { get; private set; }
        public DateTime CreationDate { get; set; }

        public EntityBase()
        {
            CreationDate = DateTime.UtcNow;
        }
    }
}
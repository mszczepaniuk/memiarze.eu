using memiarzeEu.Models.Interfaces;
using System;

namespace memiarzeEu.Models
{
    public abstract class XdPoint : BaseEntity, IEntityCreationDate, IOwnedByUser
    {
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
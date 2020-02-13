using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Models
{
    public abstract class XdPoint : BaseEntity, IEntityCreationDate
    {
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Models
{
    public class Comment : BaseEntity, IEntityCreationDate, IEntityUpdateDate
    {
        public string Text { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public List<CommentXdPoint> XdPoints { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}

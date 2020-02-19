using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Models
{
    public class Meme : BaseEntity, IEntityCreationDate, IEntityUpdateDate
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public string ImagePath { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public List<Comment> Comments { get; set; }
        public List<MemeXdPoint> XdPoints { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
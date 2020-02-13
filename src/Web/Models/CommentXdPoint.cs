using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Models
{
    public class CommentXdPoint : XdPoint
    {
        [Required]
        public Comment Comment { get; set; }
        public int CommentId { get; set; }
    }
}

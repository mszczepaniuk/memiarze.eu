using System.ComponentModel.DataAnnotations;

namespace memiarzeEu.Models
{
    public class CommentXdPoint : XdPoint
    {
        [Required]
        public Comment Comment { get; set; }
        public int CommentId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Models
{
    public class Meme
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageLink { get; set; }
        public DateTime CreationDate { get; set; }
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}

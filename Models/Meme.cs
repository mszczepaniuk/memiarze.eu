using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Models
{
    public class Meme
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ImagePath { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [NotMapped]
        public bool IsXdClicked { get; set; } = false;
    }
}

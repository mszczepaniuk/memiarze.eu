using memiarzeEu.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace memiarzeEu.Models
{
    public class ApplicationUser : IdentityUser, IEntityCreationDate, IEntityUpdateDate
    {
        [MaxLength(160)]
        public string About { get; set; }
        public string AvatarPath { get; set; }
        public List<Meme> Memes { get; set; }
        public List<Comment> Comments { get; set; }
        public List<XdPoint> XdPoints { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
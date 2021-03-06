﻿using memiarzeEu.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace memiarzeEu.Models
{
    public class Comment : BaseEntity, IEntityCreationDate, IEntityUpdateDate, IOwnedByUser, IPointable<CommentXdPoint>
    {
        [MaxLength(300)]
        public string Text { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public Meme Meme { get; set; }
        public int? MemeId { get; set; }
        public List<CommentXdPoint> XdPoints { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Models
{
    public class MemeXdPoint : XdPoint
    {
        public Meme Meme { get; set; }
        public int MemeId { get; set; }
    }
}

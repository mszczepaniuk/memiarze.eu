﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Models.Interfaces
{
    public interface IOwnedByUser
    {
        public string UserId { get; set; }
    }
}

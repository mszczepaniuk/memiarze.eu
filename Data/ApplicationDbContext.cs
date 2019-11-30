﻿using System;
using System.Collections.Generic;
using System.Text;
using memiarzeEu.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace memiarzeEu.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Meme> Memes { get; set; }
        public DbSet<XdPoint> XdPoints { get; set; }
    }
}

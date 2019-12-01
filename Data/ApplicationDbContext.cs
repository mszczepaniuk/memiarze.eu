using System;
using System.Collections.Generic;
using System.Text;
using memiarzeEu.Models;
using Microsoft.AspNetCore.Identity;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Meme>()
                .HasOne(b => b.ApplicationUser)
                .WithMany(a => a.Memes)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

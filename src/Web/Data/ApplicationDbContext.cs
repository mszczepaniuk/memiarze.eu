using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        public DbSet<CommentXdPoint> CommentXdPoints { get; set; }
        public DbSet<MemeXdPoint> MemeXdPoints { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Meme>()
                .HasOne(b => b.User)
                .WithMany(a => a.Memes)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Comment>()
                .HasOne(b => b.User)
                .WithMany(a => a.Comments)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<XdPoint>()
                .HasOne(b => b.User)
                .WithMany(a => a.XdPoints)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<XdPoint>()
                .ToTable("XdPoints");
        }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is IEntityUpdateDate && e.State == EntityState.Modified);
            foreach (var entry in modifiedEntries)
            {
                ((IEntityUpdateDate)entry.Entity).UpdateDate = DateTime.Now;
            }

            var addedEntries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is IEntityCreationDate && e.State == EntityState.Added);
            foreach (var entry in addedEntries)
            {
                ((IEntityCreationDate)entry.Entity).CreationDate = DateTime.Now;
            }
            return base.SaveChanges();
        }
    }
}

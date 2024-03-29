using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Project.Data.SeedDb
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>()
                .HasKey(ep => new { ep.UserId});

            modelBuilder.Entity<Owner>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Category>()
                .HasData(new Category()
                {
                    Id = 1,
                    Name = "Fast Food"
                },
                
                new Category()
                {
                    Id = 2,
                    Name = "Elegans"
                });


            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Owner> Owners { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Restaurant> Restaurants { get; set; }

    }
}

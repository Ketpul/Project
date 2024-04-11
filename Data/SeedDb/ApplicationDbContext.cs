using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Data.Models;

namespace Project.Data.SeedDb
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FavoriteRestaurants>()
                .HasKey(ep => new { ep.RestaurantId, ep.UserId });

            modelBuilder.Entity<FavoriteRestaurants>()
                .HasOne(e => e.Restaurant)
                .WithMany()
                .HasForeignKey(e => e.RestaurantId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<FavoriteRestaurants>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RestaurateurRequest>()
                .HasKey(ep => new { ep.RestaurateurId });

            modelBuilder.Entity<RestaurateurRequest>()
                .HasOne(e => e.Restaurateur)
                .WithMany()
                .HasForeignKey(e => e.RestaurateurId)
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
                },
                new Category
                {
                    Id = 3,
                    Name = "Other"
                });


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<FavoriteRestaurants> FavoritesRestaurants { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<RestaurateurRequest> RestaurateursRequests { get; set; }

    }
}

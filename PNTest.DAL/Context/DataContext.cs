using Microsoft.EntityFrameworkCore;
using PNTest.DAL.Entities;

namespace PNTest.DAL.Context
{
    public sealed class DataContext : DbContext
    {
        public DbSet<Request> Requests { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<ResponseLocation> ResponseLocations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<UserFavoriteLocation> UserFavoriteLocations { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Type).IsRequired();

                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Response>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne<Request>()
                      .WithMany()
                      .HasForeignKey(e => e.RequestId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired();
                entity.Property(e => e.ApiKey).IsRequired();
            });
            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.PlaceId);
                entity.Property(e => e.LocationName).IsRequired();
                entity.Property(e => e.LocationType).IsRequired();
                entity.Property(e => e.Address).IsRequired();
            });


            modelBuilder.Entity<UserFavoriteLocation>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LocationId });

                entity.HasOne<User>()
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne<Location>()
                      .WithMany()
                      .HasForeignKey(e => e.LocationId)
                      .HasPrincipalKey(l=> l.PlaceId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ResponseLocation>()
                .HasKey(rl => new { rl.ResponseId, rl.LocationId });

            modelBuilder.Entity<ResponseLocation>()
                .HasOne<Response>()
                .WithMany()
                .HasForeignKey(rl => rl.ResponseId);

            modelBuilder.Entity<ResponseLocation>()
                .HasOne<Location>()
                .WithMany()
                .HasForeignKey(rl => rl.LocationId)
                .HasPrincipalKey(l =>l.PlaceId);
            SeedData(modelBuilder);
        }
        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    ApiKey = "api-key-12345"
                },
                new User
                {
                    Id = 2,
                    Username = "user1",
                    ApiKey = "api-key-67890"
                }
            );
        }
    }
}

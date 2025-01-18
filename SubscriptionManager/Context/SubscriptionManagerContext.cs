using BasicAuth.Models;
using Microsoft.EntityFrameworkCore;

namespace SubscriptionManager.Context;

public class SubscriptionManagerContext : DbContext
{
    public SubscriptionManagerContext(DbContextOptions<SubscriptionManagerContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ModelBuilderExtensions.Seed(modelBuilder);
    }

    public DbSet<User> Users { get; set; } = default!;

    private static class ModelBuilderExtensions
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1000,
                    Username = "User 1",
                    Email = "user1@gmail.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("User11..")
                },
                new User
                {
                    Id = 1001,
                    Username = "User 2",
                    Email = "user2@gmail.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("User22..")
                },
                new User
                {
                    Id = 1002,
                    Username = "User 3",
                    Email = "user3@gmail.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("User33..")
                },
                new User
                {
                    Id = 1004,
                    Username = "Owner",
                    Email = "owner@gmail.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Owner123.")
                }
            );
        }
    }
}
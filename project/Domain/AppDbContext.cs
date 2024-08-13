using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using project.Domain.Entities;
using System.Reflection.Emit;

namespace project.Domain
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<TextField> TextFields { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }
        public DbSet<PhotoField> photoFields { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "7f233c4a-8b63-4ad6-9221-d6c3fae843ce",
                Name = "admin",
                NormalizedName = "ADMIN",
            }) ;
            builder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "05703e3c-2761-45e8-90b4-cab9d6d7dadb",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "mari126723@gmail.com",
                NormalizedEmail = "MARI126723@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "superpassword"),
                SecurityStamp = string.Empty,
            }) ;
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "7f233c4a-8b63-4ad6-9221-d6c3fae843ce",
                UserId = "05703e3c-2761-45e8-90b4-cab9d6d7dadb",
            }) ;
        }
    }
}
 
using Evidencijanezaposlenih.Interface.Context.Modeli;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Evidencijanezaposlenih.Interface.Context
{
    public class IdentitetiDBContext : IdentityDbContext<Korisnik>
    {
        private const string UserRoleId = "d1f8eeb1-0a31-4e0b-9a3b-bccabf1d0002";
        private const string AdminUserId = "d1f8eeb1-0a31-4e0b-9a3b-bccabf1d0003";
        private const string AdminRoleId = "d1f8eeb1-0a31-4e0b-9a3b-bccabf1d0001";
   
        public IdentitetiDBContext(DbContextOptions<IdentitetiDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminRole = new IdentityRole
            {
                Id = AdminRoleId,
                Name = "admin",
                NormalizedName = "admin"
            };

            var userRole = new IdentityRole
            {
                Id = UserRoleId,
                Name = "user",
                NormalizedName = "user"
            };

            var adminUser = new Korisnik
            {
                Id = AdminUserId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var passwordHasher = new PasswordHasher<Korisnik>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin123!");

            var adminUserRole = new IdentityUserRole<string>
            {
                UserId = AdminUserId,
                RoleId = AdminRoleId
            };

            builder.Entity<IdentityRole>().HasData(adminRole, userRole);
            builder.Entity<Korisnik>().HasData(adminUser);
            builder.Entity<IdentityUserRole<string>>().HasData(adminUserRole);
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    var admin = new IdentityRole("admin");
        //    admin.NormalizedName = "admin";

        //    var user = new IdentityRole("user");
        //    user.NormalizedName = "user";

        //    builder.Entity<IdentityRole>().HasData(admin, user);
        //}
    }

}

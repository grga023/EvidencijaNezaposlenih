using Evidencijanezaposlenih.Interface.Context.Modeli;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Evidencijanezaposlenih.Interface.Context
{
    public class IdentitetiDBContext : IdentityDbContext<Korisnik>
    {
        public IdentitetiDBContext(DbContextOptions<IdentitetiDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";

            var user = new IdentityRole("user");
            user.NormalizedName = "user";

            builder.Entity<IdentityRole>().HasData(admin, user);
        }
    }

}

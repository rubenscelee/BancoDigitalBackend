using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.UserBusinessModels;
using Models.UserModels;

namespace IdentityServerAspNetIdentity.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            // 🔹 Create users with hashed passwords
            Guid idAlice = Guid.NewGuid();
            Guid idBbob = Guid.NewGuid();
            //var alice = CreateUser(idAlice.ToString(), "alice", "AliceSmith@email.com");
            //var bob = CreateUser(idBbob.ToString(), "bob", "BobSmith@email.com");

            //// 🔹 Apply seeding
            //builder.Entity<IdentityUser>().HasData(alice, bob);
        }

        //private IdentityUser CreateUser(string id, string username, string email)
        //{
        //    var hasher = new PasswordHasher<IdentityUser>();

        //    var user = new UserAuth
        //    {
        //        Id = id, // 🔹 Ensure consistent ID
        //        UserName = username,
        //        NormalizedUserName = username.ToUpper(),
        //        Email = email,
        //        NormalizedEmail = email.ToUpper(),
        //        EmailConfirmed = true,
        //        PasswordHash = hasher.HashPassword(null, "Pass123$"),
        //        SecurityStamp = Guid.NewGuid().ToString()
        //    };

        //    return user;
        //}
        public DbSet<IdentityUser> Users { get; set; }
    }   
}
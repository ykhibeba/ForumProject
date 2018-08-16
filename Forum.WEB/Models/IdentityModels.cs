using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Forum.WEB.Models
{
    /// <summary>
    /// Customize identity model
    /// </summary>
    public class ApplicationUser: IdentityUser
    {

        /// <summary>
        /// First name user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name user
        /// </summary>
        public string LastName { get; set; }
    }

    /// <summary>
    /// Context of userdb
    /// </summary>
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Create ApplicationDbContext for Identity, throwIfV1Schema: false means that
        /// will not exception if schema not mathes
        /// </summary>
        public ApplicationDbContext(): base("UserContext", throwIfV1Schema: false)
        {

        }

        /// <summary>
        /// Override model
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //AspNetUsers -> User
            modelBuilder.Entity<ApplicationUser>().ToTable("User");
            //AspNetRoles -> Role
            modelBuilder.Entity<IdentityRole>().ToTable("Role");
            //AspNetUserRole /> UserRole
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
            //AspNetUserClaim -> UserClaim
            modelBuilder.Entity<IdentityUserClaim>().ToTable("Claim");
            //AspNetUserLogin -> UserLogin
            modelBuilder.Entity<IdentityUserLogin>().ToTable("Login");

        }
    }
}
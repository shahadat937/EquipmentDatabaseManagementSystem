using SchoolManagement.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Identity.Configurations;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Identity
{
    public class SchoolManagementIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public SchoolManagementIdentityDbContext(DbContextOptions<SchoolManagementIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}

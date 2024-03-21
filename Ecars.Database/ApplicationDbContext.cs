using Ecars.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecars.Database
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<BaseDetail> baseDetails { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }   

        //seeding

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BaseDetail>().HasData(
                new BaseDetail
                {
                    Id = 1,
                    Name = "Honda WTX",
                    ModelYear = 2023,
                    Price = 65000,
                    Mileage = 12.5,
                    Colors = new List<string>
                    {
                        "Sunshine Orange","Lepord Black"
                    }
                }
                );; ;
        }
    }
}

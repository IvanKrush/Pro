using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyCompany.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyCompany.Domain
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }
        public DbSet<TextField>TextFields { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole {

                Id = "12345",
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {

                Id = "23456",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "bodvoz@gmail.com",
                NormalizedEmail = "BODVOZ@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "superpassword"),
                SecurityStamp = string.Empty
            });

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityUserRole<string>
            {

                RoleId = "34567",
                UserId = "23456",
                
            });

            modelBuilder.Entity<TextField>().HasData(new TextField
            {

                Id = new Guid("55555") ,
                CodeWord = "PageIndex",
                Title = "Головна"
            });

            modelBuilder.Entity<TextField>().HasData(new TextField
            {

                Id = new Guid("66666"),
                CodeWord = "PageServices",
                Title = "Послуги"
            });

            modelBuilder.Entity<TextField>().HasData(new TextField
            {

                Id = new Guid("6666"),
                CodeWord = "PageContacts",
                Title = "Контакти"
            });





        }

    }
}

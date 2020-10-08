using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace FacultyWeb.Models
{
    public class MenuContext : IdentityDbContext<User>
    {
        public DbSet<Header> Headers { get; set; }
        public DbSet<Sidebar> Sidebars { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<SubHeader> SubHeaders { get; set; }
        public DbSet<SubSidebar> SubSidebars { get; set; }
        public MenuContext(DbContextOptions<MenuContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Header>()
            //    .HasOne(p => p.Faculty)
            //    .WithMany(t => t.Headers)
            //    .OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<Sidebar>()
            //    .HasOne(p => p.Faculty)
            //    .WithMany(t => t.Sidebars)
            //    .OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<SubSidebar>()
            //    .HasOne(p => p.Faculty)
            //    .WithMany(t => t.SubSidebars)
            //    .OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<SubSidebar>()
            //    .HasOne(p => p.Sidebar)
            //    .WithMany(t => t.SubSidebars)
            //    .OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<SubHeader>()
            //    .HasOne(p => p.Faculty)
            //    .WithMany(t => t.SubHeaders)
            //    .OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<SubHeader>()
            //    .HasOne(p => p.Header)
            //    .WithMany(t => t.SubHeaders)
            //    .OnDelete(DeleteBehavior.Cascade);

        }
    }
}

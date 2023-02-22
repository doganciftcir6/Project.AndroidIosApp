using Microsoft.EntityFrameworkCore;
using Project.AndroidIosApp.DataAccess.Configurations;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.DataAccess.Contexts.EntityFramework
{
    public class AndroidIosContext : DbContext
    {
        //contexti dependecylemek için
        public AndroidIosContext(DbContextOptions<AndroidIosContext> options) : base(options)
        {

        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<ProjectRole> ProjectRoles { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<ProjectUserRole> ProjectUserRoles { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Support> Supports { get; set; }
        public DbSet<SupportUser> SupportUsers { get; set; }
        public DbSet<SupportUserSupport> SupportUserSupports { get; set; }
        public DbSet<OS> GetOS { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }

        //configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BlogConfiguration());
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new GenderConfiguration());
            modelBuilder.ApplyConfiguration(new DeviceConfiguration());
            modelBuilder.ApplyConfiguration(new SocialMediaConfiguration());
            modelBuilder.ApplyConfiguration(new SupportConfiguration());
            modelBuilder.ApplyConfiguration(new SupportUserSupportConfiguration());
            modelBuilder.ApplyConfiguration(new SupportUserConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectUserConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectRoleConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectUserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new OSConfiguration());
            modelBuilder.ApplyConfiguration(new DeviceTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new BlogCommentConfiguration());
        }
    }
}

using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EnterSchoolRegister.BLL.Entities;

namespace EnterSchoolRegister.DAL.EF
{
    public class DbContext<TUser, TRole, TKey> : IdentityDbContext<TUser, TRole, TKey>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly ConnectionStringDto _connectionStringDto;

        // Table properties e.g
        // public virtual DbSet<Entity> TableName { get; set; }
        public virtual DbSet<UserRole> UsersRoles { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Grade> StudentsCoursesGrades { get; set; }

        public DbContext(ConnectionStringDto connectionStringDto)
        {
            _connectionStringDto = connectionStringDto;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_connectionStringDto.ConnectionString); // for provider SQL Server 

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Fluent API commands

            //UserRole primary and foreign keys
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.RoleId, ur.UserId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UsersRoles)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UsersRoles)
                .HasForeignKey(ur => ur.UserId);

            //StudentCourse primary and foreign keys
            modelBuilder.Entity<Grade>()
                .HasKey(gr => new { gr.CourseId, gr.StudentSerialNumber });

            modelBuilder.Entity<Grade>()
                .HasOne(gr => gr.Course)
                .WithMany(g => g.Grades)
                .HasForeignKey(gr => gr.CourseId);

            modelBuilder.Entity<Grade>()
                .HasOne(gr => gr.Student)
                .WithMany(g => g.Grades)
                .HasForeignKey(gr => gr.StudentSerialNumber);
        }
    }
}
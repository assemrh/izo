using izo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace izo.Data
{
    public class DataDbContext:DbContext
    {
        public DataDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Resource> Resources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ExamResult>()
                .HasOne(a => a.Student)
                .WithMany(q => q.ExamResults)
                .HasForeignKey(a => a.StudentId);

            modelBuilder.Entity<Student>()
                .HasMany(s => s.ExamResults)
                .WithOne(e => e.Student)
                .HasForeignKey(s => s.StudentId);
            modelBuilder.Entity<ExamResult>()
                .HasOne(a => a.Course)
                .WithMany(q => q.ExamResults)
                .HasForeignKey(a => a.CourseID);
            modelBuilder.Entity<Course>()
                .HasMany(s => s.ExamResults)
                .WithOne(e => e.Course)
                .HasForeignKey(s => s.CourseID);
            modelBuilder.Entity<Language>()
                .HasMany(l => l.Resources)
                .WithOne(r => r.Language)
                .HasForeignKey(r => r.LanguageId);

            modelBuilder.Entity<Course>().HasData(
            new Course { Id = Guid.NewGuid(), Name = "Example Course 1" },
            new Course { Id = Guid.NewGuid(), Name = "Example Course 2" },
            new Course { Id = Guid.NewGuid(), Name = "Example Course 3" },
            new Course { Id = Guid.NewGuid(), Name = "Example Course 4" },
            new Course { Id = Guid.NewGuid(), Name = "Example Course 5" }
            );
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = Guid.NewGuid(), FullName = "David C. Hunter" },
                new Student { Id = Guid.NewGuid(), FullName = "Carlos Waterslide" },
                new Student { Id = Guid.NewGuid(), FullName = "Luke Sexgator" },
                new Student { Id = Guid.NewGuid(), FullName = "Hector McHector" }
                );
            modelBuilder.Entity<User>().HasData( 
                new User { 
                    Id = Guid.NewGuid(), 
                    FullName = "Admin",
                    Email= "admin@domain.tld",
                    Password="Usr@12345",
                    Role="Admin",
                    Username="admin"
                });

            modelBuilder.Entity<Language>().HasData(
                new Language { Id= 1 , Name = "English", Culture ="en-US"},
                new Language { Id= 2 , Name = "Turkish (Türkçe)", Culture ="tr-TR"},
                new Language { Id= 3 , Name = "Arabic (العربية) ", Culture ="ar-SA"}
                );

        }

    }
}

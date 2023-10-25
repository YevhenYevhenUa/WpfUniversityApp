using Microsoft.EntityFrameworkCore;
using Task10.UniversityWPF.Domain.Core.Models;

namespace Task10.UniversityWPF.Infrastructure.Data;

public partial class University20Context : DbContext
{
    public University20Context(DbContextOptions<University20Context> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().HasData(DBSeeder.GetCourses());
        modelBuilder.Entity<Teacher>().HasData(DBSeeder.GetTeachers());
        modelBuilder.Entity<Group>().HasData(DBSeeder.GetGroups());
        modelBuilder.Entity<Student>().HasData(DBSeeder.GetStudents());
        base.OnModelCreating(modelBuilder);
    }

}

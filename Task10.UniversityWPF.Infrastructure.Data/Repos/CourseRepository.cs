using Microsoft.EntityFrameworkCore;
using Task10.UniversityWPF.Domain.Core.Models;
using Task10.UniversityWPF.Domain.Interfaces;

namespace Task10.UniversityWPF.Infrastructure.Data.Repos;
public class CourseRepository : ICourseRepository
{
    private readonly University20Context _context;

    public CourseRepository(University20Context context)
    {
        _context = context;
    }

    public bool Create(Course course)
    {
        _context.Add(course);
        return Save();
    }

    public bool Delete(Course course)
    {
        _context.Remove(course);
        return Save();
    }

    public bool Edit(Course course)
    {
        _context.Update(course);
        return Save();
    }

    public Course GetCourseById(int id)
    {
        return _context.Courses.FirstOrDefault(c => c.CourseId == id);
    }

    public ICollection<Course> GetCourseList()
    {
        return _context.Courses.ToList();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0;
    }
}

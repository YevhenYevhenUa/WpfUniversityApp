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

    public async Task<bool> CreateAsync(Course course)
    {
        await _context.AddAsync(course);
        return await SaveAsync();
    }

    public async Task<bool> DeleteAsync(Course course)
    {
        _context.Remove(course);
        return await SaveAsync();
    }

    public async Task<bool> EditAsync(Course course)
    {
        _context.Update(course);
        return await SaveAsync();
    }

    public async Task<Course> GetCourseByIdAsync(int id)
    {
        return await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == id);
    }

    public async Task<ICollection<Course>> GetCourseListAsync()
    {
       return await _context.Courses.ToListAsync();
    }

    public async Task<bool> SaveAsync()
    {
        var saved = await _context.SaveChangesAsync();
        return saved > 0;
    }
}

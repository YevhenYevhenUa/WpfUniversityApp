using Task10.UniversityWPF.Domain.Core.Models;

namespace Task10.UniversityWPF.Domain.Interfaces;
public interface ICourseRepository
{
    Task<ICollection<Course>> GetCourseListAsync();
    Task<Course> GetCourseByIdAsync(int id);
    Task<bool> CreateAsync(Course course);
    Task<bool> EditAsync(Course course);
    Task<bool> DeleteAsync(Course course);
    Task<bool> SaveAsync();
}

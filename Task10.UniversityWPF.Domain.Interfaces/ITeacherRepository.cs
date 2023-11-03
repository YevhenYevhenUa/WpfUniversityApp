using Task10.UniversityWPF.Domain.Core.Models;

namespace Task10.UniversityWPF.Domain.Interfaces;
public interface ITeacherRepository
{
    Task<ICollection<Teacher>> GetAllTeachersAsync();
    Task<Teacher> GetTeacherByIdAsync(int id);
    Task<bool> CreateAsync(Teacher teacher);
    Task<bool> EditAsync(Teacher teacher);
    Task<bool> DeleteAsync(Teacher teacher);
    Task<bool> SaveAsync();
}

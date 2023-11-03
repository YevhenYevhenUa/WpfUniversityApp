using Task10.UniversityWPF.Domain.Core.Models;

namespace Task10.UniversityWPF.Domain.Interfaces;
public interface IStudentRepository
{
    Task<ICollection<Student>> GetAllStudentAsync();
    Task<ICollection<Student>> GetListByIdAsync(int id);
    Task<ICollection<Student>> GetStudentsBuCourseIdAsync(int courseId);
    Task<Student> GetStudentByIdAsync(int id);
    Task<bool> AddListOfStudentAsync(List<Student> students);
    Task<bool> RemoveListOfStudentsAsync(List<Student> students);
    Task<bool> CreateAsync(Student student);
    Task<bool> EditAsync(Student student);
    Task<bool> DeleteAsync(Student student);
    Task<bool> SaveAsync();
}

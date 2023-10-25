using Task10.UniversityWPF.Domain.Core.Models;

namespace Task10.UniversityWPF.Domain.Interfaces;
public interface IStudentRepository
{
    ICollection<Student> GetAllStudent();
    ICollection<Student> GetListById(int id);
    ICollection<Student> GetStudentsBuCourseId(int courseId);
    Student GetStudentById(int id);
    bool AddListOfStudent(List<Student> students);
    bool RemoveListOfStudents(List<Student> students);
    bool Create(Student student);
    bool Edit(Student student);
    bool Delete(Student student);
    bool Save();
}

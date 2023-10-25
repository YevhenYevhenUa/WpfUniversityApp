using Task10.UniversityWPF.Domain.Core.Models;

namespace Task10.UniversityWPF.Domain.Interfaces;
public interface ITeacherRepository
{
    ICollection<Teacher> GetAllTeachers();
    Teacher GetTeacherById(int id);
    bool Create(Teacher teacher);
    bool Edit(Teacher teacher);
    bool Delete(Teacher teacher);
    bool Save();
}

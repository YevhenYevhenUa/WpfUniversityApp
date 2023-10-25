using Task10.UniversityWPF.Domain.Core.Models;

namespace Task10.UniversityWPF.Domain.Interfaces;
public interface ICourseRepository
{
    ICollection<Course> GetCourseList();
    Course GetCourseById(int id);
    bool Create(Course course);
    bool Edit(Course course);
    bool Delete(Course course);
    bool Save();

}

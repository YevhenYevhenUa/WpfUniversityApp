using Task10.UniversityWPF.Domain.Core.Models;
using Task10.UniversityWPF.Domain.Interfaces;

namespace Task10.UniversityWPF.Infrastructure.Data.Repos;
public class StudentRepository : IStudentRepository
{
    private readonly University20Context _context;

    public StudentRepository(University20Context context)
    {
        _context = context;
    }

    public bool Create(Student student)
    {
        _context.Add(student);
        return Save();
    }

    public bool Delete(Student student)
    {
        _context.Remove(student);
        return Save();
    }

    public bool Edit(Student student)
    {
        _context.Update(student);
        return Save();
    }

    public ICollection<Student> GetListById(int id)
    {
        return _context.Students.Where(s => s.GroupId == id).ToList();
    }

    public Student GetStudentById(int id)
    {
        return _context.Students.FirstOrDefault(s => s.StudentId == id);
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0;
    }

    public ICollection<Student> GetAllStudent()
    {
      return _context.Students.ToList();
    }

    public bool AddListOfStudent(List<Student> students)
    {
        _context.Students.AddRange(students);
        return Save();
    }

    public bool RemoveListOfStudents(List<Student> students)
    {
        _context.Students.RemoveRange(students);
        return Save();
    }

    public ICollection<Student> GetStudentsBuCourseId(int courseId)
    {
        var students = from s in _context.Students
                       join g in _context.Groups on s.GroupId equals g.GroupId
                       where g.CourseId == courseId
                       select s;

        return students.ToList();
    }

}

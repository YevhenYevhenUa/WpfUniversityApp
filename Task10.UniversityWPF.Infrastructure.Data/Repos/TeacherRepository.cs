using Task10.UniversityWPF.Domain.Core.Models;
using Task10.UniversityWPF.Domain.Interfaces;

namespace Task10.UniversityWPF.Infrastructure.Data.Repos;
public class TeacherRepository : ITeacherRepository
{
    private readonly University20Context _context;

    public TeacherRepository(University20Context context)
    {
        _context = context;
    }

    public bool Create(Teacher teacher)
    {
        _context.Add(teacher);
        return Save();
    }

    public bool Delete(Teacher teacher)
    {
        _context.Remove(teacher);
        return Save();
    }

    public bool Edit(Teacher teacher)
    {
        _context.Update(teacher);
        return Save();
    }

    public Teacher GetTeacherById(int id)
    {
        return _context.Teachers.FirstOrDefault(t => t.TeacherId == id);
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0;
    }

    public ICollection<Teacher> GetAllTeachers()
    {
        return _context.Teachers.ToList();
    }
}

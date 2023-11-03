using Microsoft.EntityFrameworkCore;
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

    public async Task<bool> CreateAsync(Student student)
    {
        _context.AddAsync(student);
        return await SaveAsync();
    }

    public async Task<bool> DeleteAsync(Student student)
    {
        _context.Remove(student);
        return await SaveAsync();
    }

    public async Task<bool> EditAsync(Student student)
    {
        _context.Update(student);
        return await SaveAsync();
    }

    public async Task<ICollection<Student>> GetListByIdAsync(int id)
    {
        return await _context.Students.Where(s => s.GroupId == id).ToListAsync();
    }

    public async Task<Student> GetStudentByIdAsync(int id)
    {
        return await _context.Students.FirstOrDefaultAsync(s => s.StudentId == id);
    }

    public async Task<bool> SaveAsync()
    {
        var saved = await _context.SaveChangesAsync();
        return saved > 0;
    }

    public async Task<ICollection<Student>> GetAllStudentAsync()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task<bool> AddListOfStudentAsync(List<Student> students)
    {
        await _context.Students.AddRangeAsync(students);
        return  await SaveAsync();
    }

    public async Task<bool> RemoveListOfStudentsAsync(List<Student> students)
    {
        _context.Students.RemoveRange(students);
        return await SaveAsync();
    }

    public async Task<ICollection<Student>> GetStudentsBuCourseIdAsync(int courseId)
    {
        var students = from s in _context.Students
                       join g in _context.Groups on s.GroupId equals g.GroupId
                       where g.CourseId == courseId
                       select s;

        return await students.ToListAsync();
    }

}

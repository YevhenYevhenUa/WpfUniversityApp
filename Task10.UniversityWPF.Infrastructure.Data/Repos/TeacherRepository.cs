using Microsoft.EntityFrameworkCore;
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

    public async Task<bool> CreateAsync(Teacher teacher)
    {
        await _context.AddAsync(teacher);
        return await SaveAsync();
    }

    public async Task<bool> DeleteAsync(Teacher teacher)
    {
        _context.Remove(teacher);
        return await SaveAsync();
    }

    public async Task<bool> EditAsync(Teacher teacher)
    {
        _context.Update(teacher);
        return await SaveAsync();
    }

    public async Task<Teacher> GetTeacherByIdAsync(int id)
    {
        return await _context.Teachers.FirstOrDefaultAsync(t => t.TeacherId == id);
    }

    public async Task<bool> SaveAsync()
    {
        var saved = await _context.SaveChangesAsync();
        return saved > 0;
    }

    public async Task<ICollection<Teacher>> GetAllTeachersAsync()
    {
        return await _context.Teachers.ToListAsync();
    }   

}

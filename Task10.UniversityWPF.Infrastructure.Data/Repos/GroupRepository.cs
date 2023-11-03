using Microsoft.EntityFrameworkCore;
using Task10.UniversityWPF.Domain.Core.Models;
using Task10.UniversityWPF.Domain.Interfaces;

namespace Task10.UniversityWPF.Infrastructure.Data.Repos;
public class GroupRepository : IGroupRepository
{
    private readonly University20Context _context;

    public GroupRepository(University20Context context)
    {
        _context = context;
    }

    public async Task<ICollection<Group>> GetAllGroupsAsync()
    {
        return await _context.Groups.ToListAsync();
    }

    public async Task<bool> CreateAsync(Group group)
    {
        await _context.Groups.AddAsync(group);
        return await SaveAsync();
    }

    public async Task<bool> DeleteAsync(Group group)
    {
        _context.Remove(group);
        return await SaveAsync();
    }

    public async Task<bool> EditAsync(Group group)
    {
        _context.Update(group);
        return await SaveAsync();
    }

    public async Task<Group> GetGroupByIdAsync(int id)
    {
        return await _context.Groups.FirstOrDefaultAsync(g => g.GroupId == id);
    }

    public async Task<ICollection<Group>> GetListByIdAsync(int id)
    {
        return await _context.Groups.Where(g => g.CourseId == id).ToListAsync();
    }

    public async Task<bool> SaveAsync()
    {
        var saved = await _context.SaveChangesAsync();
        return saved > 0;
    }

}

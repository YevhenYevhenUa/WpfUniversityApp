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

    public ICollection<Group> GetAllGroups()
    {
        return _context.Groups.ToList();
    }

    public bool Create(Group group)
    {
        _context.Add(group);
        return Save();
    }

    public bool Delete(Group group)
    {
        _context.Remove(group);
        return Save();
    }

    public bool Edit(Group group)
    {
        _context.Update(group);
        return Save();
    }

    public Group GetGroupById(int id)
    {
        return _context.Groups.FirstOrDefault(g => g.GroupId == id);
    }

    public ICollection<Group> GetListById(int id)
    {
        return _context.Groups.Where(g => g.CourseId == id).ToList();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0;
    }
}

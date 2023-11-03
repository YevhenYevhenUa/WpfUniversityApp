using Task10.UniversityWPF.Domain.Core.Models;

namespace Task10.UniversityWPF.Domain.Interfaces;
public interface IGroupRepository
{
    Task<ICollection<Group>> GetAllGroupsAsync();
    Task<ICollection<Group>> GetListByIdAsync(int id);
    Task<Group> GetGroupByIdAsync(int id);
    Task<bool> CreateAsync(Group group);
    Task<bool> EditAsync(Group group);
    Task<bool> DeleteAsync(Group group);
    Task<bool> SaveAsync();
}

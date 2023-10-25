using Task10.UniversityWPF.Domain.Core.Models;

namespace Task10.UniversityWPF.Domain.Interfaces;
public interface IGroupRepository
{
    ICollection<Group> GetAllGroups();
    ICollection<Group> GetListById(int id);
    Group GetGroupById(int id);
    bool Create(Group group);
    bool Edit(Group group);
    bool Delete(Group group);
    bool Save();
}

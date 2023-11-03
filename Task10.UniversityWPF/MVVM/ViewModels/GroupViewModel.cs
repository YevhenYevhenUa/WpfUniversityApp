using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Task10.UniversityWPF.Domain.Core.Models;
using Task10.UniversityWPF.Domain.Interfaces;
using Task10.UniversityWPF.MVVM.CRUDViewModels;
using Task10.UniversityWPF.MVVM.Views.Groups;

namespace Task10.UniversityWPF.MVVM.ViewModels
{
    public class GroupViewModel
    {
        private readonly GroupCRUDVIewModel _groupVM;
        private readonly EditGroup _editGroup;
        private readonly AddGroup _addGroup;
        private readonly IGroupRepository _groupRepository;

        public GroupViewModel(GroupCRUDVIewModel groupVM,
            EditGroup editGroup,
            AddGroup addGroup,
            IGroupRepository groupRepository)
        {
            _groupVM = groupVM;
            _editGroup = editGroup;
            _addGroup = addGroup;
            _groupRepository = groupRepository;
        }

        public async Task EditGroup(Group group)
        {
            _groupVM.SelectedGroup = group;
            await _groupVM.SetTeachersCollection();
            _groupVM.Name = group.Name;
            _groupVM.SelectedTeacher = group.Teacher;
            _editGroup.ShowDialog();
        }

        public async Task<Group> AddGroup()
        {
            _groupVM.CreatedGroup = null;
            _groupVM.Name = string.Empty;
            await _groupVM.SetCourseCollection();
            await _groupVM.SetTeachersCollection();
            _addGroup.ShowDialog();
            return _groupVM.CreatedGroup;
        }

        public async Task<ObservableCollection<Group>> GetGroupsCollection()
        {
            ObservableCollection<Group> Groups = new ObservableCollection<Group>();
            var groups = await _groupRepository.GetAllGroupsAsync();
            foreach (var item in groups)
            {
                Groups.Add(item);
            }

            return Groups;
        }

        public GroupCRUDVIewModel GroupCRUDVIewModel => _groupVM;

    }
}

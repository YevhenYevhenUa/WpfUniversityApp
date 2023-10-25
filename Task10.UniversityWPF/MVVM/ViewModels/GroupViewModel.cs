using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Task10.UniversityWPF.Domain.Core.Models;
using Task10.UniversityWPF.Domain.Interfaces;
using Task10.UniversityWPF.Infrastructure.Data;
using Task10.UniversityWPF.MVVM.CRUDViewModels;
using Task10.UniversityWPF.MVVM.Views.Groups;
using Task10.UniversityWPF.MVVMCore;

namespace Task10.UniversityWPF.MVVM.ViewModels
{
    public class GroupViewModel
    {
        private readonly GroupCRUDVIewModel _groupVM;
        private readonly EditGroup _editGroup;
        private readonly AddGroup _addGroup;

        public GroupViewModel(GroupCRUDVIewModel groupVM, EditGroup editGroup, AddGroup addGroup)
        {
            _groupVM = groupVM;
            _editGroup = editGroup;
            _addGroup = addGroup;
        }

        public void EditGroup(Group group)
        {
            _groupVM.SelectedGroup = group;
            _groupVM.SetTeachersCollection();
            _groupVM.Name = group.Name;
            _groupVM.SelectedTeacher = group.Teacher;
            _editGroup.ShowDialog();
        }

        public void AddGroup()
        {
            _groupVM.Name = string.Empty;
            _groupVM.SetCourseCollection();
            _groupVM.SetTeachersCollection();
            _addGroup.ShowDialog();
        }

        public void ImportStudents(Group group)
        {
            _groupVM.ImortOpen(group);
        }

        public void ExportStudents(Group group)
        {
            _groupVM.ExportOpen(group);
        }

        public void CreateDock(Group group)
        {
            _groupVM.CreateDock(group);
        }
    }
}

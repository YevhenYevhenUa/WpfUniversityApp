using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task10.UniversityWPF.Domain.Core.Models;
using Task10.UniversityWPF.MVVM.CRUDViewModels;
using Task10.UniversityWPF.MVVM.Views.Teachers;
using Task10.UniversityWPF.MVVMCore;

namespace Task10.UniversityWPF.MVVM.ViewModels
{
    public class TeacherViewModel
    {
        private readonly TeacherCRUDViewModel _teacherCRUD;
        private readonly EditTeacher _editTeacher;
        private readonly AddTeacher _addTeacher;

        public TeacherViewModel(TeacherCRUDViewModel teacherCRUD, EditTeacher editTeacher, AddTeacher addTeacher)
        {
            _teacherCRUD = teacherCRUD;
            _editTeacher = editTeacher;
            _addTeacher = addTeacher;
        }

        public void EditTeacher(Teacher teacher)
        {
            _teacherCRUD.SelectedTeacher = teacher;
            _teacherCRUD.Name = teacher.Name;
            _teacherCRUD.Surename = teacher.Surename;
            _editTeacher.ShowDialog();
        }

        public void AddTeacher()
        {
            _teacherCRUD.Name = string.Empty;
            _teacherCRUD.Surename= string.Empty;
            _addTeacher.ShowDialog();
        }
    }
}

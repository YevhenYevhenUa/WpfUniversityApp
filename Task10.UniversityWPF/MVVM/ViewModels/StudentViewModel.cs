using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task10.UniversityWPF.Domain.Core.Models;
using Task10.UniversityWPF.MVVM.CRUDViewModels;
using Task10.UniversityWPF.MVVM.Views.Students;

namespace Task10.UniversityWPF.MVVM.ViewModels
{
    public class StudentViewModel
    {
        private readonly StudentCRUDViewModel _studentCRUD;
        private readonly EditStudent _editStudent;
        private readonly AddStudent _addStudent;

        public StudentViewModel(StudentCRUDViewModel studentCRUD, EditStudent editStudent, AddStudent addStudent)
        {
            _studentCRUD = studentCRUD;
            _editStudent = editStudent;
            _addStudent = addStudent;
        }

        public void EditStudent(Student student)
        {
            _studentCRUD.SelectedStudent = student;
            _studentCRUD.FirstName = student.FirstName;
            _studentCRUD.LastName = student.LastName;
            _editStudent.ShowDialog();
        }

        public void AddStudent()
        {
            _studentCRUD.FirstName = string.Empty;
            _studentCRUD.LastName = string.Empty;
            _studentCRUD.SetCoursesCollection();
            _addStudent.ShowDialog();
        }
    }
}

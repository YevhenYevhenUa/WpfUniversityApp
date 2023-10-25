using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task10.UniversityWPF.Domain.Core.Models;
using Task10.UniversityWPF.Domain.Interfaces;
using Task10.UniversityWPF.Infrastructure.Data;
using Task10.UniversityWPF.Infrastructure.Data.Repos;
using Task10.UniversityWPF.MVVM.CRUDViewModels;
using Task10.UniversityWPF.MVVM.Views.Course;
using Task10.UniversityWPF.MVVMCore;

namespace Task10.UniversityWPF.MVVM.ViewModels
{
    public class CourseViewModel
    {
        private readonly CourseCRUDViewModel _courseVM;
        private readonly EditCourse _editCourse;
        private readonly AddCourse _addCourse;

        public CourseViewModel(CourseCRUDViewModel courseVM, EditCourse editCourse, AddCourse addCourse)
        {
            _courseVM = courseVM;
            _editCourse = editCourse;
            _addCourse = addCourse;
        }

        public void EditCourse(Course course)
        {
            _courseVM.SelectedCourse = course;
            _courseVM.Name = course.Name;
            _courseVM.Description = course.Description;
            _editCourse.ShowDialog();
        }

        public void AddCourse()
        {
            _courseVM.Name = string.Empty;
            _courseVM.Description = string.Empty;
            _addCourse.ShowDialog();
        }

        public void CreateDockWithStudents(Course course)
        {
            _courseVM.CreateDock(course);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Task10.UniversityWPF.Domain.Core.Models;
using Task10.UniversityWPF.Domain.Interfaces;
using Task10.UniversityWPF.Infrastructure.Data.Repos;
using Task10.UniversityWPF.MVVMCore;
using Task10.UniversityWPF.Services;

namespace Task10.UniversityWPF.MVVM.CRUDViewModels
{
    public class StudentCRUDViewModel : BaseViewModel
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IDialogueService _dialogueService;

        public StudentCRUDViewModel(IStudentRepository studentRepository,
            ICourseRepository courseRepository, IDialogueService dialogueService)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _dialogueService = dialogueService;
            EditStudentCommand = new RelayCommand(o => EditStudent(), o => true);
            CreateStudentCommand = new RelayCommand(o => CreateStudent(), o => true);
            DeleteStudentCommand = new RelayCommand(o => Delete(), o => true);
        }

        public RelayCommand EditStudentCommand { get; set; }
        public RelayCommand CreateStudentCommand { get; set; }
        public RelayCommand DeleteStudentCommand { get; set; }

        #region"Properties"
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        private List<Course> _courses;
        public List<Course> Courses
        {
            get { return _courses; }
            set
            {
                _courses = value;
                OnPropertyChanged();
            }
        }

        private Course _selectedCourse;
        public Course SelectedCourse
        {
            get { return _selectedCourse; }
            set
            {
                _selectedCourse = value;
                OnPropertyChanged();
            }
        }

        private Group _selectedGroup;
        public Group SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                _selectedGroup = value;
                OnPropertyChanged();
            }
        }

        private Student _selectedStudent;
        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }
        #endregion
        public void SetCoursesCollection()
        {
            Courses = _courseRepository.GetCourseList().ToList();
        }

        public bool CreateStudent()
        {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || SelectedGroup is null)
            {
                _dialogueService.AddMessageError();
                return false;
            }

            var student = new Student
            {
                FirstName = FirstName,
                LastName = LastName,
                Group = SelectedGroup,
                GroupId = SelectedGroup.GroupId
            };

            var isSuccess = _studentRepository.Create(student);
            _dialogueService.AddMessageSuccess();
            return isSuccess;
        }

        public bool EditStudent()
        {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
            {
                _dialogueService.EditMessageError();
                return false;
            }

            var student = SelectedStudent;
            student.FirstName = FirstName;
            student.LastName = LastName;
            var isSuccess = _studentRepository.Edit(student);
            _dialogueService.EditMessageSuccess();
            return isSuccess;
        }

        public bool Delete()
        {
            string fullName = string.Format("{0} {1}", SelectedStudent.FirstName, SelectedStudent.LastName);
            var result = _dialogueService.DeleteMessage(fullName);

            if (result == MessageBoxResult.Yes)
            {
                var student = SelectedStudent;
                return _studentRepository.Delete(student);
            }
            return false;
        }

    }
}

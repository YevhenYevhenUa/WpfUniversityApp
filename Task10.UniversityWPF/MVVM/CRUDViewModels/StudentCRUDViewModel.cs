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
            EditStudentCommand = new RelayCommandAsync(Edit);
            CreateStudentCommand = new RelayCommandAsync(Add);
        }

        public RelayCommandAsync EditStudentCommand { get; set; }
        public RelayCommandAsync CreateStudentCommand { get; set; }

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

        public Student CreatedStudent { get; set; }
        #endregion
        public async Task SetCoursesCollection()
        {
            var taskResult = await _courseRepository.GetCourseListAsync();
            Courses = taskResult.ToList();
        }

        public async Task<bool> Add()
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

            CreatedStudent = student;
            var isSuccess = await _studentRepository.CreateAsync(student);
            _dialogueService.AddMessageSuccess();
            return isSuccess;
        }

        public async Task<bool> Edit()
        {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
            {
                _dialogueService.EditMessageError();
                return false;
            }

            var student = SelectedStudent;
            student.FirstName = FirstName;
            student.LastName = LastName;
            var isSuccess = await _studentRepository.EditAsync(student);
            _dialogueService.EditMessageSuccess();
            return isSuccess;
        }

        public async Task<bool> Delete(Student student)
        {
            string fullName = string.Format("{0} {1}", student.FirstName, student.LastName);
            var result = _dialogueService.DeleteMessage(fullName);

            if (result == MessageBoxResult.Yes)
            {
                return await _studentRepository.DeleteAsync(student);
            }
            return false;
        }

    }
}

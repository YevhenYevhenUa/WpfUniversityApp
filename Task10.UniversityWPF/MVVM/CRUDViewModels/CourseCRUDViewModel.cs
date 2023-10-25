using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Task10.UniversityWPF.Domain.Core.Models;
using Task10.UniversityWPF.Domain.Interfaces;
using Task10.UniversityWPF.MVVMCore;
using Task10.UniversityWPF.Services;

namespace Task10.UniversityWPF.MVVM.CRUDViewModels
{
    public class CourseCRUDViewModel : BaseViewModel
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IDialogueService _dialogueService;

        public CourseCRUDViewModel(ICourseRepository courseRepository,
            IGroupRepository groupRepository,
            IStudentRepository studentRepository,
            IDialogueService dialogueService)
        {
            _courseRepository = courseRepository;
            _groupRepository = groupRepository;
            _studentRepository = studentRepository;
            _dialogueService = dialogueService;
            EditCourseCommand = new RelayCommand(o => Edit(), o => true);
            AddCourseCommand = new RelayCommand(o => AddNewCourse(), o => true);
            DeleteCourseCommand = new RelayCommand(o => Delete(), o => true);
        }

        public RelayCommand EditCourseCommand { get; set; }
        public RelayCommand DeleteCourseCommand { get; set; }
        public RelayCommand AddCourseCommand { get; set; }
        #region"Properties"
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

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public bool Edit()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Description))
            {
                _dialogueService.EditMessageError();
                return false;
            }

            var course = SelectedCourse;
            course.Name = Name;
            course.Description = Description;
            var isSuccess = _courseRepository.Edit(course);
            Name = string.Empty;
            Description = string.Empty;
            _dialogueService.EditMessageSuccess();
            return isSuccess;
        }

        public bool Delete()
        {
            var groups = _groupRepository.GetListById(SelectedCourse.CourseId);

            if (groups.Count > 0)
            {
                _dialogueService.DeleteMessageError();
                return false;
            }

            var result = _dialogueService.DeleteMessage(SelectedCourse.Name);

            if (result == MessageBoxResult.Yes)
            {
                var course = SelectedCourse;
                return _courseRepository.Delete(course);
            }
            return false;
        }

        public bool AddNewCourse()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Description))
            {
                _dialogueService.AddMessageError();
                return false;
            }

            var course = new Course
            {
                Name = Name,
                Description = Description,
            };

            var isSuccess = _courseRepository.Create(course);
            _dialogueService.AddMessageSuccess();
            return isSuccess;
        }

        public void CreateDock(Course course)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Pdf files (*.pdf)|*.pdf | Csv files (*.csv)|*.csv";
            saveFileDialog.FileName = string.Format("{0}", course.Name);
            StringBuilder stringBuilder = new StringBuilder();
            if (saveFileDialog.ShowDialog() == true)
            {
                var students = _studentRepository.GetStudentsBuCourseId(course.CourseId);
                foreach (var item in students)
                {
                    string line = string.Format("{0} {1}", item.FirstName, item.LastName);
                    stringBuilder.AppendLine(line);
                }
                File.WriteAllText(saveFileDialog.FileName, stringBuilder.ToString());
                _dialogueService.SuccessMessage();
            }
            else
            {
                return;
            }
        }
    }
}

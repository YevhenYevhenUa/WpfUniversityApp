using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using Task10.UniversityWPF.Domain.Core.Models;
using Task10.UniversityWPF.Domain.Interfaces;
using Task10.UniversityWPF.MVVMCore;
using Task10.UniversityWPF.Services;

namespace Task10.UniversityWPF.MVVM.CRUDViewModels
{
    public class GroupCRUDVIewModel : BaseViewModel
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IDialogueService _dialogueService;

        public GroupCRUDVIewModel(ITeacherRepository teacherRepository,
            IGroupRepository groupRepository, 
            ICourseRepository courseRepository, 
            IStudentRepository studentRepository,
            IDialogueService dialogueService)
        {
            _teacherRepository = teacherRepository;
            _groupRepository = groupRepository;
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _dialogueService = dialogueService;
            EditCommand = new RelayCommand(o => Edit(), o => true);
            DeleteCommand = new RelayCommand(o => Delete(), o => true);
            CreateNewCommand = new RelayCommand(o => Add(), o => true);
        }

        public RelayCommand EditCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand CreateNewCommand { get; set; }
        #region"Properties"
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

        private List<Teacher> _teachers;
        public List<Teacher> Teachers
        {
            get { return _teachers; }
            set
            {
                _teachers = value;
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

        private Teacher _selectedTeacher;
        public Teacher SelectedTeacher
        {
            get { return _selectedTeacher; }
            set
            {
                _selectedTeacher = value;
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
        #endregion
        public bool Add()
        {
            if (string.IsNullOrEmpty(Name) || SelectedTeacher is null || SelectedCourse is null)
            {
                _dialogueService.AddMessageError();
                return false;
            }

            var group = new Group
            {
                Name = _name,
                CourseId = SelectedCourse.CourseId,
                Course = SelectedCourse,
                Teacher = _selectedTeacher,
                TeacherId = _selectedTeacher.TeacherId,
            };

            var isSuccess = _groupRepository.Create(group);
            _dialogueService.AddMessageSuccess();
            Name = string.Empty;
            return isSuccess;
        }

        public bool Edit()
        {
            if (string.IsNullOrEmpty(Name) || SelectedTeacher is null)
            {
                _dialogueService.EditMessageError();
                return false;
            }

            var group = SelectedGroup;
            group.Name = Name;
            group.Teacher = SelectedTeacher;
            group.TeacherId = SelectedTeacher.TeacherId;

            var isSuccess = _groupRepository.Edit(group);
            _dialogueService.EditMessageSuccess();
            return isSuccess;
        }

        public bool Delete()
        {
            var students = _studentRepository.GetListById(SelectedGroup.GroupId);

            if (students.Count > 0)
            {
                _dialogueService.DeleteMessageError();
                return false;
            }

            var result = _dialogueService.DeleteMessage(SelectedGroup.Name);
            if (result == MessageBoxResult.Yes)
            {
                var group = SelectedGroup;
                return _groupRepository.Delete(group);
            }
            return false;
        }

        public void ExportOpen(Group group)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Csv files (*.csv)|*.csv";
            if (saveFileDialog.ShowDialog() == true)
            {
                var students = _studentRepository.GetListById(group.GroupId);
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var student in students)
                {
                    var line = string.Format("{0},{1},", student.FirstName, student.LastName);
                    stringBuilder.AppendLine(line);
                }

                File.WriteAllText(saveFileDialog.FileName, stringBuilder.ToString());
                _dialogueService.SuccessMessage();
            }
        }

        public void ImortOpen(Group group)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Csv files (*.csv)|*.csv";
            string[] lines = null;
            if (openFileDialog.ShowDialog() == true)
            {
                lines = File.ReadAllLines(openFileDialog.FileName);
            }
            else
            {
                return;
            }

            List<Student> students = new List<Student>(lines.Length);
            foreach (var item in lines)
            {
                string[] data = item.Split(',');
                int count = data.Length - 1;
                students.Add(new Student { FirstName = data[0], LastName = data[1], GroupId = group.GroupId, Group = group });
            }

            var testList = group.Students.ToList();
            _studentRepository.RemoveListOfStudents(testList);
            _studentRepository.AddListOfStudent(students);
            _dialogueService.SuccessMessage();
        }

        public void CreateDock(Group group)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Pdf files (*.pdf)|*.pdf | Csv files (*.csv)|*.csv";
            saveFileDialog.FileName = string.Format("{0}", group.Name);
            StringBuilder stringBuilder = new StringBuilder();
            if (saveFileDialog.ShowDialog() == true)
            {
                var students = group.Students.ToList();
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

        public void SetTeachersCollection()
        {
            Teachers = _teacherRepository.GetAllTeachers().ToList();
        }

        public void SetCourseCollection()
        {
            Courses = _courseRepository.GetCourseList().ToList();
        }
    }
}


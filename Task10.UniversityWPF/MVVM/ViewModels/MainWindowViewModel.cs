using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using Task10.UniversityWPF.Domain.Core.Models;
using Task10.UniversityWPF.MVVMCore;
using Task10.UniversityWPF.Services;

namespace Task10.UniversityWPF.MVVM.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly CourseViewModel _courseVM;
        private readonly GroupViewModel _groupVM;
        private readonly TeacherViewModel _teacherVM;
        private readonly StudentViewModel _studentVM;
        private readonly IFileIOService _fileIOService;
        private readonly IDialogueService _dialogueService;

        public MainWindowViewModel(
            CourseViewModel courseVM,
            GroupViewModel groupVM,
            TeacherViewModel teacherVM,
            StudentViewModel studentVM,
            IFileIOService fileIOService,
            IDialogueService dialogueService)
        {
            _courseVM = courseVM;
            _groupVM = groupVM;
            _teacherVM = teacherVM;
            _studentVM = studentVM;
            _fileIOService = fileIOService;
            _dialogueService = dialogueService;
            Init();
            AddButtonCommand = new RelayCommandAsync(AddCommand);
            EditButtonCommand = new RelayCommandAsync(EditCommand);
            ImportCommand = new RelayCommandAsync(Import);
            ExportCommand = new RelayCommandAsync(Export);
            CreateDockCommand = new RelayCommandAsync(GenerateFileWithStudents);
            RemoveButtonCommand = new RelayCommandAsync(RemoveCommand);
        }

        #region"properties"
        private ObservableCollection<Course> _courses;
        public ObservableCollection<Course> Courses
        {
            get { return _courses; }
            set
            {
                _courses = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Group> _groups;
        public ObservableCollection<Group> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Student> _students;
        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Teacher> _teachers;
        public ObservableCollection<Teacher> Teachers
        {
            get { return _teachers; }
            set
            {
                _teachers = value;
                OnPropertyChanged();
            }
        }
        #endregion
        public RelayCommandAsync EditButtonCommand { get; set; }
        public RelayCommandAsync AddButtonCommand { get; set; }
        public RelayCommandAsync RemoveButtonCommand { get; set; }
        public RelayCommandAsync CreateDockCommand { get; set; }
        public RelayCommandAsync ImportCommand { get; set; }
        public RelayCommandAsync ExportCommand { get; set; }


        private async Task Init()
        {
            Courses = await _courseVM.GetCourseCollection();
            Teachers = await _teacherVM.GetTeachersCollection();
            Groups = await _groupVM.GetGroupsCollection();
            Students = await _studentVM.GetStudentCollection();
        }

        public async Task EditCommand()
        {
            var @object = SelectedItem;
            switch (@object)
            {
                case Course course:
                    var oldCourses = Courses.FirstOrDefault(c => c.CourseId == course.CourseId);
                    _courseVM.EditCourse(course);
                    break;
                case Group group:
                    var oldGroup = Groups.FirstOrDefault(g => g.GroupId == group.GroupId);
                    await _groupVM.EditGroup(group);
                    break;
                case Student student:
                    var oldStudent = Students.FirstOrDefault(s => s.StudentId == student.StudentId);
                    _studentVM.EditStudent(student);
                    break;
                case Teacher teacher:
                    var oldTeacher = Teachers.FirstOrDefault(t => t.TeacherId == teacher.TeacherId);
                    _teacherVM.EditTeacher(teacher);
                    break;

                default:
                    _dialogueService.NotSelectedEditWarning();
                    break;
            }
        }

        public async Task AddCommand()
        {
            var tab = SelectedTab;
            switch (tab.Header)
            {
                case "Tree view struct":
                case "Courses":
                    var newCourse = _courseVM.AddCourse();
                    if (newCourse is not null)
                    {
                        Courses.Add(newCourse);
                    }

                    break;
                case "Groups":
                    var newGroup = await _groupVM.AddGroup();
                    if (newGroup is not null)
                    {
                        Groups.Add(newGroup);
                    }

                    break;
                case "Students":
                    var newStudent = await _studentVM.AddStudent();
                    if (newStudent is not null)
                    {
                        Students.Add(newStudent);
                    }

                    break;
                case "Teachers":
                    var newTeacher = _teacherVM.AddTeacher();
                    if (newTeacher is not null)
                    {
                        Teachers.Add(newTeacher);
                    }

                    break;
                default:
                    break;
            }
        }

        public async Task RemoveCommand()
        {
            var @object = SelectedItem;
            switch (@object)
            {
                case Course course:
                    var courseResult = await _courseVM.CourseCRUDViewModel.Delete(course);
                    if (courseResult)
                    {
                        Courses.Remove(course);
                    }

                    break;
                case Group group:
                    var groupResult = await _groupVM.GroupCRUDVIewModel.Delete(group);
                    if (groupResult)
                    {
                        Groups.Remove(group);
                    }

                    break;
                case Student student:
                    var studentResult = await _studentVM.StudentCRUDViewModel.Delete(student);
                    if (studentResult)
                    {
                        Students.Remove(student);
                    }

                    break;
                case Teacher teacher:
                    var teacherResult = await _teacherVM.TeacherCRUDViewModel.Delete(teacher);
                    if (teacherResult)
                    {
                        Teachers.Remove(teacher);
                    }

                    break;

                default:
                    _dialogueService.NotSelectedDeleteWarning();
                    break;
            }
        }

        public async Task GenerateFileWithStudents()
        {
            bool result = false;
            switch (SelectedItem)
            {
                case Course course:
                    result = await _fileIOService.GetAllStudentsFromCourseToFile(course);
                    break;
                case Group group:
                    result = await _fileIOService.GetAllStudentsFromGroupToFile(group);
                    break;
                default:
                    _dialogueService.NotSelectedForFileGenerationWarning();
                    break;
            }

            if (result)
            {
                _dialogueService.SuccessMessage();
            }

        }

        public async Task Export()
        {
            bool result = false;
            if (SelectedItem is Group group)
            {
                result = await _fileIOService.ExportStudentsFromGroupToCsv(group);
            }
            else
            {
                _dialogueService.NotSelectedForExportImportWarning();
            }

            if (result)
            {
                _dialogueService.SuccessMessage();
            }

        }

        public async Task Import()
        {
            bool result = false;
            if (SelectedItem is Group group)
            {
                result = await _fileIOService.ImportStudentsToGroup(group);
                Students = await _studentVM.GetStudentCollection();
            }
            else
            {
                _dialogueService.NotSelectedForExportImportWarning();
            }

            if (result)
            {
                _dialogueService.SuccessMessage();
            }
        }


        private object _selectedItem;
        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        private TabItem _selectedTab;
        public TabItem SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                _selectedTab = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsVisibleGeneratePdf));
                OnPropertyChanged(nameof(IsVisibleImportExport));
                OnPropertyChanged(nameof(IsVisibleControlButtons));
            }
        }

        public bool IsVisibleGeneratePdf
        {
            get
            {
                if (SelectedTab is null)
                {
                    return false;
                }

                switch (SelectedTab.Header)
                {
                    case "Courses":
                        return true;
                    case "Groups":
                        return true;
                    default:
                        return false;
                }
            }
        }

        public bool IsVisibleImportExport
        {
            get
            {
                if (SelectedTab is null)
                {
                    return false;
                }

                switch (SelectedTab.Header)
                {
                    case "Groups":
                        return true;
                    default:
                        return false;
                }
            }
        }

        public bool IsVisibleControlButtons
        {
            get
            {
                if (SelectedTab is null)
                {
                    return false;
                }

                switch (SelectedTab.Header)
                {
                    case "Tree view struct":
                        return false;
                    default:
                        return true;
                }
            }
        }
    }
}

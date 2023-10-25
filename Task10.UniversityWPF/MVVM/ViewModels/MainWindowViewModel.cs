using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Task10.UniversityWPF.Domain.Core.Models;
using Task10.UniversityWPF.Domain.Interfaces;
using Task10.UniversityWPF.Infrastructure.Data;
using Task10.UniversityWPF.MVVM.Views.Course;
using Task10.UniversityWPF.MVVM.Views.Groups;
using Task10.UniversityWPF.MVVMCore;

namespace Task10.UniversityWPF.MVVM.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly CourseViewModel _courseVM;
        private readonly GroupViewModel _groupVM;
        private readonly TeacherViewModel _teacherVM;
        private readonly StudentViewModel _studentVM;
        private readonly ICourseRepository _courseRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;

        public MainWindowViewModel(
            CourseViewModel courseVM,
            GroupViewModel groupVM,
            TeacherViewModel teacherVM,
            ICourseRepository courseRepository,
            IGroupRepository groupRepository,
            IStudentRepository studentRepository,
            ITeacherRepository teacherRepository,
            StudentViewModel studentVM)
        {
            _courseVM = courseVM;
            _groupVM = groupVM;
            _teacherVM = teacherVM;
            _studentVM = studentVM;
            _courseRepository = courseRepository;
            _groupRepository = groupRepository;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
            SetCollections();
            EditButtonCommand = new RelayCommand(o => EditCommand(), o => true);
            AddButtonCommand = new RelayCommand(o => AddCommand(), o => true);
            ImportCommand = new RelayCommand(o => Import(), o => true);
            ExportCommand = new RelayCommand(o => Export(), o => true);
            CreateDockCommand = new RelayCommand(o => CreateDockWithStudents(), o => true);
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
        public RelayCommand EditButtonCommand { get; set; }
        public RelayCommand AddButtonCommand { get; set; }
        public RelayCommand CreateDockCommand { get; set; }
        public RelayCommand ImportCommand { get; set; }
        public RelayCommand ExportCommand { get; set; }

        public void EditCommand()
        {
            var @object = SelectedItem;

            switch (@object)
            {
                case Course course:
                    _courseVM.EditCourse(course);
                    SetCollections();
                    break;
                case Group group:
                    _groupVM.EditGroup(group);
                    SetCollections();
                    break;
                case Student student:
                    _studentVM.EditStudent(student);
                    SetCollections();
                    break;
                case Teacher teacher:
                    _teacherVM.EditTeacher(teacher);
                    SetCollections();
                    break;

                default:
                    break;
            }
        }

        public void AddCommand()
        {
            var tab = SelectedTab;

            switch (tab.Header)
            {
                case "Courses":
                    _courseVM.AddCourse();
                    SetCollections();
                    break;
                case "Groups":
                    _groupVM.AddGroup();
                    SetCollections();
                    break;
                case "Students":
                    _studentVM.AddStudent();
                    SetCollections();
                    break;
                case "Teachers":
                    _teacherVM.AddTeacher();
                    SetCollections();
                    break;
                default:
                    break;
            }
        }

        public void CreateDockWithStudents()
        {
            if (SelectedItem is Course course)
            {
                _courseVM.CreateDockWithStudents(course);
            }
            else if (SelectedItem is Group group)
            {
                _groupVM.CreateDock(group);
            }
        }

        public void Export()
        {
            if (SelectedItem is Group group)
            {
                _groupVM.ExportStudents(group);
            }
        }

        public void Import()
        {
            if (SelectedItem is Group group)
            {
                _groupVM.ImportStudents(group);
                SetCollections();
            }
        }

        public void SetCollections()
        {
            var courses = _courseRepository.GetCourseList();
            Courses = new ObservableCollection<Course>();
            foreach (var item in courses)
            {
                item.Groups = _groupRepository.GetListById(item.CourseId);
            }

            foreach (var item in courses)
            {
                for (int i = 0; i < item.Groups.Count; i++)
                {
                    int groupId = item.Groups.ToList()[i].GroupId;
                    item.Groups.ToList()[i].Students = _studentRepository.GetListById(groupId);
                }

                Courses.Add(item);
            }

            Teachers = new ObservableCollection<Teacher>();
            var teacher = _teacherRepository.GetAllTeachers();
            foreach (var item in teacher)
            {
                Teachers.Add(item);
            }

            Groups = new ObservableCollection<Group>();
            var groups = _groupRepository.GetAllGroups();
            foreach (var item in groups)
            {
                Groups.Add(item);
            }

            Students = new ObservableCollection<Student>();
            var student = _studentRepository.GetAllStudent();
            foreach (var item in student)
            {
                Students.Add(item);
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
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            EditCommand = new RelayCommandAsync(Edit);
            CreateNewCommand = new RelayCommandAsync(Add);
        }

        public RelayCommandAsync EditCommand { get; set; }
        public RelayCommandAsync CreateNewCommand { get; set; }
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

        public Group CreatedGroup { get; set; }
        #endregion
        public async Task<bool> Add()
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

            CreatedGroup = group;
            var isSuccess = await _groupRepository.CreateAsync(group);
            _dialogueService.AddMessageSuccess();
            Name = string.Empty;
            return isSuccess;
        }

        public async Task<bool> Edit()
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
            var isSuccess = await _groupRepository.EditAsync(group);
            _dialogueService.EditMessageSuccess();
            return isSuccess;
        }

        public async Task<bool> Delete(Group group)
        {
            var students = await _studentRepository.GetListByIdAsync(group.GroupId);

            if (students.Count > 0)
            {
                _dialogueService.DeleteMessageError();
                return false;
            }

            var result = _dialogueService.DeleteMessage(group.Name);
            if (result == MessageBoxResult.Yes)
            {
                return await _groupRepository.DeleteAsync(group);
            }
            return false;
        }

        public async Task SetTeachersCollection()
        {
            var result = await _teacherRepository.GetAllTeachersAsync();
            Teachers = result.ToList();
        }

        public async Task SetCourseCollection()
        {
            var taskResult = await _courseRepository.GetCourseListAsync();
            Courses = taskResult.ToList();
        }
    }
}


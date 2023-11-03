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
        private readonly IDialogueService _dialogueService;

        public CourseCRUDViewModel(ICourseRepository courseRepository,
            IGroupRepository groupRepository,
            IDialogueService dialogueService)
        {
            _courseRepository = courseRepository;
            _groupRepository = groupRepository;
            _dialogueService = dialogueService;
            EditCourseCommand = new RelayCommandAsync(Edit);
            AddCourseCommand = new RelayCommandAsync(Add);
        }

        public RelayCommandAsync EditCourseCommand { get; set; }
        public RelayCommandAsync AddCourseCommand { get; set; }
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

        public Course CreatedCourse { get; set; }
        #endregion

        public async Task<bool> Edit()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Description))
            {
                _dialogueService.EditMessageError();
                return false;
            }

            var course = SelectedCourse;
            course.Name = Name;
            course.Description = Description;
            var isSuccess =  await _courseRepository.EditAsync(course);
            _dialogueService.EditMessageSuccess();
            return isSuccess;
        }

        public async Task<bool> Delete(Course course)
        {
            var groups = await _groupRepository.GetListByIdAsync(course.CourseId);

            if (groups.Count > 0)
            {
                _dialogueService.DeleteMessageError();
                return false;
            }

            var result = _dialogueService.DeleteMessage(course.Name);

            if (result == MessageBoxResult.Yes)
            {
                var deletionResult = await _courseRepository.DeleteAsync(course);
                return deletionResult;
            }
            return false;
        }

        public async Task<bool> Add()
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

            CreatedCourse = course;
            var isSuccess = await _courseRepository.CreateAsync(course);
            _dialogueService.AddMessageSuccess();
            return isSuccess;
        }
       
    }
}

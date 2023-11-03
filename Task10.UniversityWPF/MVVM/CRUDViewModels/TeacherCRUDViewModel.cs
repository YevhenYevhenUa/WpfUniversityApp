using System.Threading.Tasks;
using System.Windows;
using Task10.UniversityWPF.Domain.Core.Models;
using Task10.UniversityWPF.Domain.Interfaces;
using Task10.UniversityWPF.MVVMCore;
using Task10.UniversityWPF.Services;

namespace Task10.UniversityWPF.MVVM.CRUDViewModels
{
    public class TeacherCRUDViewModel : BaseViewModel
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IDialogueService _dialogueService;

        public TeacherCRUDViewModel(ITeacherRepository teacherRepository, IDialogueService dialogueService)
        {
            _teacherRepository = teacherRepository;
            _dialogueService = dialogueService;
            EditTeacherCommand = new RelayCommandAsync(Edit);
            AddTeacherCommand = new RelayCommandAsync(Add);
        }

        public RelayCommandAsync EditTeacherCommand { get; set; }
        public RelayCommandAsync AddTeacherCommand { get; set; }
        #region"properties"
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

        private string _surename;
        public string Surename
        {
            get { return _surename; }
            set
            {
                _surename = value;
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

        public Teacher CreatedTeacher { get; set; }
        #endregion
        public async Task<bool> Edit()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Surename))
            {
                _dialogueService.EditMessageError();
                return false;
            }

            var teacher = SelectedTeacher;
            teacher.Name = Name;
            teacher.Surename = Surename;
            var isSuccess = await _teacherRepository.EditAsync(teacher);
            _dialogueService.EditMessageSuccess();
            return isSuccess;
        }

        public async Task<bool> Add()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Surename))
            {
                _dialogueService.AddMessageError();
                return false;
            }

            var teacher = new Teacher
            {
                Name = Name,
                Surename = Surename,
            };

            CreatedTeacher = teacher;
            var isSuccess = await _teacherRepository.CreateAsync(teacher);
            _dialogueService.AddMessageSuccess();
            return isSuccess;
        }

        public async Task<bool> Delete(Teacher teacher)
        {
            string fullName = string.Format("{0} {1}", teacher.Name, teacher.Surename);
            var result = _dialogueService.DeleteMessage(fullName);
            if (result == MessageBoxResult.Yes)
            {
                return await _teacherRepository.DeleteAsync(teacher);
            }

            return false;
        }

    }
}

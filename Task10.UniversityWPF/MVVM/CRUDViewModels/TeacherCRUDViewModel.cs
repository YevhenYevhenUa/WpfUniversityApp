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
            EditTeacherCommand = new RelayCommand(o => Edit(), o => true);
            AddTeacherCommand = new RelayCommand(o => Add(), o => true);
            DeleteTeacherCommand = new RelayCommand(o => Delete(), o => true);
        }

        public RelayCommand EditTeacherCommand { get; set; }
        public RelayCommand AddTeacherCommand { get; set; }
        public RelayCommand DeleteTeacherCommand { get; set; }
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
        #endregion
        public bool Edit()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Surename))
            {
                _dialogueService.EditMessageError();
                return false;
            }

            var teacher = SelectedTeacher;
            teacher.Name = Name;
            teacher.Surename = Surename;
            var isSuccess = _teacherRepository.Edit(teacher);
            _dialogueService.EditMessageSuccess();
            return isSuccess;
        }

        public bool Add()
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

            var isSuccess = _teacherRepository.Create(teacher);
            _dialogueService.AddMessageSuccess();
            return isSuccess;
        }

        public bool Delete()
        {
            string fullName = string.Format("{0} {1}", SelectedTeacher.Name, SelectedTeacher.Surename);
            var result = _dialogueService.DeleteMessage(fullName);
            if (result == MessageBoxResult.Yes)
            {
                var teacher = SelectedTeacher;
                return _teacherRepository.Delete(teacher);
            }

            return false;
        }

    }
}

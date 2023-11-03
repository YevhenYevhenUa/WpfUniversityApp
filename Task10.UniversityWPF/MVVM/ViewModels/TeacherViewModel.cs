using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Task10.UniversityWPF.Domain.Core.Models;
using Task10.UniversityWPF.Domain.Interfaces;
using Task10.UniversityWPF.MVVM.CRUDViewModels;
using Task10.UniversityWPF.MVVM.Views.Teachers;

namespace Task10.UniversityWPF.MVVM.ViewModels
{
    public class TeacherViewModel
    {
        private readonly TeacherCRUDViewModel _teacherCRUD;
        private readonly EditTeacher _editTeacher;
        private readonly AddTeacher _addTeacher;
        private readonly ITeacherRepository _teacherRepository;

        public TeacherViewModel(TeacherCRUDViewModel teacherCRUD,
            EditTeacher editTeacher,
            AddTeacher addTeacher,
            ITeacherRepository teacherRepository)
        {
            _teacherCRUD = teacherCRUD;
            _editTeacher = editTeacher;
            _addTeacher = addTeacher;
            _teacherRepository = teacherRepository;
        }

        public void EditTeacher(Teacher teacher)
        {
            _teacherCRUD.SelectedTeacher = teacher;
            _teacherCRUD.Name = teacher.Name;
            _teacherCRUD.Surename = teacher.Surename;
            _editTeacher.ShowDialog();
        }

        public Teacher AddTeacher()
        {
            _teacherCRUD.CreatedTeacher = null;
            _teacherCRUD.Name = string.Empty;
            _teacherCRUD.Surename = string.Empty;
            _addTeacher.ShowDialog();
            return _teacherCRUD.CreatedTeacher;
        }

        public async Task<ObservableCollection<Teacher>> GetTeachersCollection()
        {
            ObservableCollection<Teacher> Teachers = new ObservableCollection<Teacher>();
            var teacher = await _teacherRepository.GetAllTeachersAsync();
            foreach (var item in teacher)
            {
                Teachers.Add(item);
            }
            return Teachers;
        }

        public TeacherCRUDViewModel TeacherCRUDViewModel => _teacherCRUD;

    }
}

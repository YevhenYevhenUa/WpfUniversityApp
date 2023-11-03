using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Task10.UniversityWPF.Domain.Core.Models;
using Task10.UniversityWPF.Domain.Interfaces;
using Task10.UniversityWPF.MVVM.CRUDViewModels;
using Task10.UniversityWPF.MVVM.Views.Students;

namespace Task10.UniversityWPF.MVVM.ViewModels
{
    public class StudentViewModel
    {
        private readonly StudentCRUDViewModel _studentCRUD;
        private readonly EditStudent _editStudent;
        private readonly AddStudent _addStudent;
        private readonly IStudentRepository _studentRepository;

        public StudentViewModel(StudentCRUDViewModel studentCRUD,
            EditStudent editStudent,
            AddStudent addStudent, IStudentRepository studentRepository)
        {
            _studentCRUD = studentCRUD;
            _editStudent = editStudent;
            _addStudent = addStudent;
            _studentRepository = studentRepository;
        }

        public void EditStudent(Student student)
        {
            _studentCRUD.SelectedStudent = student;
            _studentCRUD.FirstName = student.FirstName;
            _studentCRUD.LastName = student.LastName;
            _editStudent.ShowDialog();
        }

        public async Task<Student> AddStudent()
        {
            _studentCRUD.CreatedStudent = null;
            _studentCRUD.FirstName = string.Empty;
            _studentCRUD.LastName = string.Empty;
            await _studentCRUD.SetCoursesCollection();
            _addStudent.ShowDialog();
            return _studentCRUD.CreatedStudent;
        }

        public async Task<ObservableCollection<Student>> GetStudentCollection()
        {
            ObservableCollection<Student> Students = new ObservableCollection<Student>();
            var student = await _studentRepository.GetAllStudentAsync();
            foreach (var item in student)
            {
                Students.Add(item);
            }

            return Students;
        }

        public StudentCRUDViewModel StudentCRUDViewModel => _studentCRUD;

    }
}

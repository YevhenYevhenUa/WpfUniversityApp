using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Task10.UniversityWPF.Domain.Core.Models;
using Task10.UniversityWPF.Domain.Interfaces;
using Task10.UniversityWPF.MVVM.CRUDViewModels;
using Task10.UniversityWPF.MVVM.Views.Course;

namespace Task10.UniversityWPF.MVVM.ViewModels
{
    public class CourseViewModel
    {
        private readonly CourseCRUDViewModel _courseVM;
        private readonly EditCourse _editCourse;
        private readonly AddCourse _addCourse;
        private readonly ICourseRepository _courseRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IStudentRepository _studentRepository;

        public CourseViewModel(CourseCRUDViewModel courseVM, 
            EditCourse editCourse, 
            AddCourse addCourse, 
            ICourseRepository courseRepository,
            IGroupRepository groupRepository,
            IStudentRepository studentRepository)
        {
            _courseVM = courseVM;
            _editCourse = editCourse;
            _addCourse = addCourse;
            _courseRepository = courseRepository;
            _groupRepository = groupRepository;
            _studentRepository = studentRepository;
        }

        public void EditCourse(Course course)
        {
            _courseVM.SelectedCourse = course;
            _courseVM.Name = course.Name;
            _courseVM.Description = course.Description;
            _editCourse.ShowDialog();
        }

        public Course AddCourse()
        {
            _courseVM.CreatedCourse = null;
            _courseVM.Name = string.Empty;
            _courseVM.Description = string.Empty;
            _addCourse.ShowDialog();
            return _courseVM.CreatedCourse;
        }

        public async Task<ObservableCollection<Course>> GetCourseCollection()
        {
            var courses =  await _courseRepository.GetCourseListAsync();
            ObservableCollection<Course> Courses = new ObservableCollection<Course>();
            foreach (var item in courses)
            {
                var groupList = await _groupRepository.GetListByIdAsync(item.CourseId);
                item.Groups = new ObservableCollection<Group>(groupList);
            }

            foreach (var item in courses)
            {
                for (int i = 0; i < item.Groups.Count; i++)
                {
                    int groupId = item.Groups.ToList()[i].GroupId;
                    var studentList = await _studentRepository.GetListByIdAsync(groupId);
                    item.Groups.ToList()[i].Students = new ObservableCollection<Student>(studentList);
                }

                Courses.Add(item);
            }

            return Courses;
        }

        public CourseCRUDViewModel CourseCRUDViewModel => _courseVM;
        
    }
}

using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task10.UniversityWPF.Domain.Core.Models;

[Table("GROUPS")]
public partial class Group : ObservableObject
{
    private int _groupId;
    private int? _courseId;
    private int? _teacherId;
    private string? _name;
    private Course? _course;
    private Teacher? _teacher;
    private ObservableCollection<Student> _students = new ObservableCollection<Student>();

    [Key]
    [Column("GROUP_ID")]
    public int GroupId { get { return _groupId; } set { _groupId = value; OnPropertyChanged(); } }

    [Column("COURSE_ID")]
    public int? CourseId { get { return _courseId; } set { _courseId = value; OnPropertyChanged(); } }

    [Column("TEACHER_ID")]
    public int? TeacherId { get { return _teacherId; } set { _teacherId = value; OnPropertyChanged(); } }

    [Column("NAME")]
    [StringLength(100)]
    public string? Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }

    [ForeignKey("CourseId")]
    [InverseProperty("Groups")]
    public virtual Course? Course { get { return _course; } set { _course = value; OnPropertyChanged(); } }

    [InverseProperty("Group")]
    public virtual ObservableCollection<Student> Students { get { return _students; } set { _students = value; OnPropertyChanged(); } }

    [ForeignKey("TeacherId")]
    [InverseProperty("Groups")]
    public virtual Teacher? Teacher { get { return _teacher; } set { _teacher = value; OnPropertyChanged(); } }
}

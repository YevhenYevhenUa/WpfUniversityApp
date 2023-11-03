using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task10.UniversityWPF.Domain.Core.Models;

[Table("COURSES")]
public partial class Course : ObservableObject
{
    private int _id;
    private string? _name;
    private string? _description;
    private ObservableCollection<Group> _groups = new ObservableCollection<Group>();

    [Key]
    [Column("COURSE_ID")]
    public int CourseId { get { return _id; } set { _id = value; OnPropertyChanged(); } }

    [Column("NAME")]
    [StringLength(100)]
    public string? Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }

    [Column("DESCRIPTION")]
    [StringLength(500)]
    public string? Description { get { return _description; } set { _description = value; OnPropertyChanged(); } }

    [InverseProperty("Course")]
    public virtual ObservableCollection<Group> Groups { get { return _groups; } set { _groups = value; OnPropertyChanged(); } }

}

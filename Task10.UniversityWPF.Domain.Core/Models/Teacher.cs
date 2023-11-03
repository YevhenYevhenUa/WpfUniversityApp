using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task10.UniversityWPF.Domain.Core.Models;

[Table("TEACHERS")]
public partial class Teacher : ObservableObject
{
    private int _teacherId;
    private string? _name;
    private string? _surename;
    private ObservableCollection<Group> _groups = new ObservableCollection<Group>();

    [Key]
    [Column("TEACHER_ID")]
    public int TeacherId { get { return _teacherId; } set { _teacherId = value; OnPropertyChanged(); } }

    [Column("NAME")]
    [StringLength(50)]
    public string? Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }

    [Column("SURENAME")]
    [StringLength(50)]
    public string? Surename { get { return _surename; } set { _surename = value; OnPropertyChanged(); } }

    [InverseProperty("Teacher")]
    public virtual ObservableCollection<Group> Groups { get { return _groups; } set { _groups = value; OnPropertyChanged(); } }
}

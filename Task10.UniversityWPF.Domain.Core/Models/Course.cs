using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task10.UniversityWPF.Domain.Core.Models;

[Table("COURSES")]
public partial class Course
{
    [Key]
    [Column("COURSE_ID")]
    public int CourseId { get; set; }

    [Column("NAME")]
    [StringLength(100)]
    public string? Name { get; set; }

    [Column("DESCRIPTION")]
    [StringLength(500)]
    public string? Description { get; set; }

    [InverseProperty("Course")]
    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}

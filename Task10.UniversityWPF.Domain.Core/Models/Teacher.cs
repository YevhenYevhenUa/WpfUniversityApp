using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Task10.UniversityWPF.Domain.Core.Models;

[Table("TEACHERS")]
public partial class Teacher
{
    [Key]
    [Column("TEACHER_ID")]
    public int TeacherId { get; set; }

    [Column("NAME")]
    [StringLength(50)]
    public string? Name { get; set; }

    [Column("SURENAME")]
    [StringLength(50)]
    public string? Surename { get; set; }

    [InverseProperty("Teacher")]
    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}

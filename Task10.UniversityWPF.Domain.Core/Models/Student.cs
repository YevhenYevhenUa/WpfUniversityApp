using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Task10.UniversityWPF.Domain.Core.Models;

[Table("STUDENTS")]
public partial class Student : ObservableObject
{
    private int _studentId;
    private int? _groupId;
    private string? _firstName;
    private string? _lastName;
    private Group? _group;

    [Key]
    [Column("STUDENT_ID")]
    public int StudentId { get { return _studentId; } set { _studentId = value; OnPropertyChanged(); } }

    [Column("GROUP_ID")]
    public int? GroupId { get { return _groupId; } set { _groupId = value; OnPropertyChanged(); } }

    [Column("FIRST_NAME")]
    [StringLength(50)]
    public string? FirstName { get { return _firstName; } set { _firstName = value; OnPropertyChanged(); } }

    [Column("LAST_NAME")]
    [StringLength(50)]
    public string? LastName { get { return _lastName; } set { _lastName = value; OnPropertyChanged(); } }

    [ForeignKey("GroupId")]
    [InverseProperty("Students")]
    public virtual Group? Group { get { return _group; } set { _group = value; OnPropertyChanged(); } }
}

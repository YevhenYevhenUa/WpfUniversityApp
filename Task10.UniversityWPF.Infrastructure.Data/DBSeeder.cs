using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task10.UniversityWPF.Domain.Core.Models;

namespace Task10.UniversityWPF.Infrastructure.Data;
public static class DBSeeder
{
    public static List<Course> GetCourses()
    {
        var courseList = new List<Course>
        {
             new Course { Name = "Scala Course", Description = "Something useful!", CourseId = 1},
             new Course { Name = "C# course", Description = "from zero to hero" , CourseId = 2},
             new Course { Name = "Frontend course", Description = "You will learn how to paint buttons properly", CourseId = 3},
             new Course { Name = "Backend course", Description = "You will learn how to do everything in this life", CourseId = 4}
        };

        return courseList;
    }

    public static List<Teacher> GetTeachers()
    {
        var teachersList = new List<Teacher>
        {
            new Teacher { Name = "Sonetqwd", Surename = "asdasd", TeacherId = 1},
            new Teacher { Name = "Mr.Asad", Surename = "Aleppo" , TeacherId = 2},
            new Teacher { Name = "Jhon", Surename = "Dalton" , TeacherId = 3},
            new Teacher { Name = "Barak", Surename = "Obema" , TeacherId = 4},
            new Teacher { Name = "Sneeky", Surename = "Peaky" , TeacherId = 5},
            new Teacher { Name = "Easy", Surename = "Breazy" , TeacherId = 6}
        };

        return teachersList;
    }

    public static List<Group> GetGroups()
    {
        var groupList = new List<Group>
        {
            new Group { CourseId = 1, Name = "Beginners", TeacherId = 1, GroupId = 1 },
            new Group { CourseId = 2, Name = "VM-61-1", TeacherId = 2, GroupId = 2 },
            new Group { CourseId = 3, Name = "Dummies", TeacherId = 3 , GroupId = 3},
            new Group { CourseId = 4, Name = "Gigachads", TeacherId = 4 , GroupId = 4}
        };

        return groupList;
    }

    public static List<Student> GetStudents()
    {
        var studentList = new List<Student>
        {
            new Student { StudentId = 1, FirstName = "SomeOne", LastName = "FromNowhere", GroupId = 1 },
            new Student { StudentId = 2, FirstName = "TestName", LastName = "TestSureName", GroupId = 2 },
            new Student { StudentId = 3, FirstName = "Teodosdr", LastName = "asdrqw", GroupId = 3 },
            new Student { StudentId = 4, FirstName = "dsasdqwe", LastName = "cxxxds", GroupId = 1 },
            new Student { StudentId = 5, FirstName = "dfwqe", LastName = "ghgvb", GroupId = 2 },
            new Student { StudentId = 6, FirstName = "dfcx", LastName = "qwrrs", GroupId = 4 },
            new Student { StudentId = 7, FirstName = "fgghnnwr", LastName = "asdfgf", GroupId = 1 },
            new Student { StudentId = 8, FirstName = "werwerwe", LastName = "dfqwe", GroupId = 4 },
            new Student { StudentId = 9, FirstName = "asd", LastName = "asd", GroupId = 3 }
        };

        return studentList;
    }
}

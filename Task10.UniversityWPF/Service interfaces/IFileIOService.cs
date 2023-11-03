using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task10.UniversityWPF.Domain.Core.Models;

namespace Task10.UniversityWPF.Services;
public interface IFileIOService
{
    Task<bool> ImportStudentsToGroup(Group group);
    Task<bool> ExportStudentsFromGroupToCsv(Group group);
    Task<bool> GetAllStudentsFromCourseToFile(Course course);
    Task<bool> GetAllStudentsFromGroupToFile(Group group);
}

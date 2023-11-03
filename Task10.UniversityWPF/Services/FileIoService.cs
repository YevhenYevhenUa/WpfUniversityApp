using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Task10.UniversityWPF.Domain.Core.Models;
using Task10.UniversityWPF.Domain.Interfaces;

namespace Task10.UniversityWPF.Services;
public class FileIoService : IFileIOService
{
    private readonly IStudentRepository _studentRepository;

    public FileIoService(IStudentRepository studentRepository)
    {

        _studentRepository = studentRepository;
    }

    public async Task<bool> ExportStudentsFromGroupToCsv(Group group)
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "Csv files (*.csv)|*.csv";
        saveFileDialog.FileName = string.Format("{0}", group.Name);
        if (saveFileDialog.ShowDialog() == false)
        {
            return false;
        }

        var students = await _studentRepository.GetListByIdAsync(group.GroupId);
        return await StudentsToFile(saveFileDialog, students.ToList());
    }

    public async Task<bool> ImportStudentsToGroup(Group group)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Csv files (*.csv)|*.csv";
        if (openFileDialog.ShowDialog() == false)
        {
            return false;
        }

        string[] lines = File.ReadAllLines(openFileDialog.FileName);
        List<Student> students = new List<Student>(lines.Length);
        foreach (var item in lines)
        {
            string[] data = item.Split(',');
            students.Add(new Student { FirstName = data[0], LastName = data[1], GroupId = group.GroupId, Group = group });
        }

        var testList = await _studentRepository.GetListByIdAsync(group.GroupId);
        await _studentRepository.RemoveListOfStudentsAsync(testList.ToList());
        await _studentRepository.AddListOfStudentAsync(students);
        return true;
    }

    public async Task<bool> GetAllStudentsFromCourseToFile(Course course)
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "Pdf files (*.pdf)|*.pdf | Csv files (*.csv)|*.csv";
        saveFileDialog.FileName = string.Format("{0}", course.Name);
        if (saveFileDialog.ShowDialog() == true)
        {
            var students = await _studentRepository.GetStudentsBuCourseIdAsync(course.CourseId);
            var result = await StudentsToFile(saveFileDialog, students.ToList());
            return result;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> GetAllStudentsFromGroupToFile(Group group)
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "Pdf files (*.pdf)|*.pdf | Csv files (*.csv)|*.csv";
        saveFileDialog.FileName = string.Format("{0}", group.Name);
        if (saveFileDialog.ShowDialog() == true)
        {
            var students = group.Students.ToList();
            var result = await StudentsToFile(saveFileDialog, students);
            return result;
        }
        else
        {
            return false;
        }
    }

    private async Task<bool> StudentsToFile(SaveFileDialog saveFileDialog, List<Student>? students)
    {
        StringBuilder stringBuilder = new StringBuilder();
        string extension = Path.GetExtension(saveFileDialog.FileName);
        if (extension == ".pdf")
        {
            string headerLine = string.Format("First Name Last Name");
            stringBuilder.AppendLine(headerLine);
            foreach (var item in students)
            {
                string line = string.Format("{0} {1}", item.FirstName, item.LastName);
                stringBuilder.AppendLine(line);
            }

            FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write);
            PdfWriter pdfWriter = new PdfWriter(fileStream);
            PdfDocument pdfDocument = new PdfDocument(pdfWriter);
            Document document = new Document(pdfDocument);
            Paragraph paragraph = new Paragraph(stringBuilder.ToString());
            document.Add(paragraph);
            document.Close();
            return true;
        }
        else if (extension == ".csv")
        {
            string headerLine = string.Format("FirstName,LastName,");
            stringBuilder.AppendLine(headerLine);
            foreach (var student in students)
            {
                var line = string.Format("{0},{1},", student.FirstName, student.LastName);
                stringBuilder.AppendLine(line);
            }

            await File.WriteAllTextAsync(saveFileDialog.FileName, stringBuilder.ToString());
            return true;
        }
        else
        {
            return false;
        }

    }
}

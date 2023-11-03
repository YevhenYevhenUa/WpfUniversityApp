using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Task10.UniversityWPF.Domain.Core.Models;
using Task10.UniversityWPF.Domain.Interfaces;
using Task10.UniversityWPF.MVVM.CRUDViewModels;
using Task10.UniversityWPF.Services;

namespace Task10.UniversityWPF.Tests.ViewModelsTests;
public class GroupCRUDViewModelTests
{
    private readonly GroupCRUDVIewModel _sut;
    private readonly Mock<IGroupRepository> _groupRepositoryMock;
    private readonly Mock<ITeacherRepository> _teacherRepositoryMock;
    private readonly Mock<IStudentRepository> _studentRepositoryMock;
    private readonly Mock<ICourseRepository> _courseRepositoryMock;
    private readonly Mock<IDialogueService> _dialogueServiceMock;

    public GroupCRUDViewModelTests()
    {
        _groupRepositoryMock = new Mock<IGroupRepository>();
        _teacherRepositoryMock = new Mock<ITeacherRepository>();
        _studentRepositoryMock = new Mock<IStudentRepository>();
        _courseRepositoryMock = new Mock<ICourseRepository>();
        _dialogueServiceMock = new Mock<IDialogueService>();
        _sut = new GroupCRUDVIewModel(_teacherRepositoryMock.Object, _groupRepositoryMock.Object, _courseRepositoryMock.Object,
            _studentRepositoryMock.Object, _dialogueServiceMock.Object);
    }

    [Theory]
    [InlineData("test", true)]
    [InlineData(null, false)]
    public async void CroupCRUDViewModel_Add_ShouldReturnBoolResult(string name, bool expectResult)
    {
        //Arrange
        var teacher = new Teacher
        {
            Name = "Test",
            Surename = "Test",
            TeacherId = 1
        };
        var testCourse = new Course
        {
            Name = "Test",
            CourseId = 1,
        };
        _sut.SelectedCourse = testCourse;
        _sut.SelectedTeacher = teacher;
        _sut.Name = name;
        _groupRepositoryMock.Setup(o => o.CreateAsync(It.IsAny<Group>())).ReturnsAsync(true);
        //Act
        var result = await _sut.Add();
        //Assert
        Assert.Equal(result, expectResult);
    }

    [Theory]
    [InlineData("test", true)]
    [InlineData(null, false)]
    public async void CroupCRUDViewModel_Edit_ShouldReturnBoolResult(string name, bool expectResult)
    {
        //Arrange
        var testGroup = new Group();
        var testTeacher = new Teacher();
        _sut.SelectedGroup = testGroup;
        _sut.SelectedTeacher = testTeacher;
        _sut.Name = name;
        _groupRepositoryMock.Setup(o => o.EditAsync(It.IsAny<Group>())).ReturnsAsync(true);
        //Act
        var result = await _sut.Edit();
        //Assert
        Assert.Equal(result, expectResult);
    }

    [Theory]
    [InlineData(0, true)]
    [InlineData(2, false)]
    public async void CroupCRUDViewModel_Delete_ShouldReturnBoolResult(int studentCount, bool expectResult)
    {
        //Arrange
        var testList = new List<Student>();
        for (int i = 0; i < studentCount; i++)
        {
            testList.Add(new Student { StudentId = i });
        }

        _studentRepositoryMock.Setup(o => o.GetListByIdAsync(It.IsAny<int>())).ReturnsAsync(testList);
        _dialogueServiceMock.Setup(o => o.DeleteMessage(It.IsAny<string>())).Returns(MessageBoxResult.Yes);
        var testGroup = new Group();
        _sut.SelectedGroup = testGroup;
        _groupRepositoryMock.Setup(o => o.DeleteAsync(It.IsAny<Group>())).ReturnsAsync(true);
        //Act
        var result = await _sut.Delete(testGroup);
        //Assert
        Assert.Equal(result, expectResult);
    }


}

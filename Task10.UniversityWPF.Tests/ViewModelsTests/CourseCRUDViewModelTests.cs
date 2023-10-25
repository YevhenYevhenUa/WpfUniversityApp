using Moq;
using System.Windows;
using Task10.UniversityWPF.Domain.Core.Models;
using Task10.UniversityWPF.Domain.Interfaces;
using Task10.UniversityWPF.MVVM.CRUDViewModels;
using Task10.UniversityWPF.Services;

namespace Task10.UniversityWPF.Tests.ViewModelsTests;
public class CourseCRUDViewModelTests
{
    private readonly CourseCRUDViewModel _sut;
    private readonly Mock<ICourseRepository> _courseRepoMock;
    private readonly Mock<IGroupRepository> _groupRepoMock;
    private readonly Mock<IStudentRepository> _studentRepoMock;
    private readonly Mock<IDialogueService> _dialogueMock;
    public CourseCRUDViewModelTests()
    {
        _courseRepoMock = new Mock<ICourseRepository>();
        _groupRepoMock = new Mock<IGroupRepository>();
        _studentRepoMock = new Mock<IStudentRepository>();
        _dialogueMock = new Mock<IDialogueService>();
        _sut = new CourseCRUDViewModel(_courseRepoMock.Object, _groupRepoMock.Object, _studentRepoMock.Object, _dialogueMock.Object);
    }

    [Theory]
    [InlineData("Test", "Test", true)]
    [InlineData("Test", null, false)]
    [InlineData(null, null, false)]
    public void StudentCRUDViewModel_Edit_ShouldReturnTrueOnSuccess(string name, string description, bool expectResult)
    {
        //Arrange
        var course = new Course
        {
            Name = "TestName",
            Description = "TestDescription"
        };
        _courseRepoMock.Setup(o => o.Edit(It.IsAny<Course>())).Returns(true);
        _sut.Name = name;
        _sut.Description = description;
        _sut.SelectedCourse = course;
        //Act
        var result = _sut.Edit();
        //Assert
        Assert.Equal(expectResult, result);
    }

    [Theory]
    [InlineData("Test", "Test", true)]
    [InlineData("Test", null, false)]
    [InlineData(null, null, false)]
    public void StudentCRUDViewModel_AddNewCourse_ShouldReturnBoolResult(string name, string description, bool expectResult)
    {
        //Arrange
        _sut.Name = name;
        _sut.Description = description;
        _courseRepoMock.Setup(o => o.Create(It.IsAny<Course>())).Returns(true);
        //Act
        var result = _sut.AddNewCourse();
        //Assert
        Assert.Equal(expectResult, result);
    }

    [Theory]
    [InlineData(true, 0)]
    [InlineData(false, 2)]
    public void StudentCRUDViewModel_Delete_ShouldReturnBoolResult(bool expectResult, int groupCount)
    {
        //Arrange
        var testList = new List<Group>();
        for (int i = 0; groupCount > i; i++)
        {
            testList.Add(new Group { CourseId = 1, GroupId = i, Name = i.ToString() });
        }

        var testCourse = new Course
        {
            CourseId = 1,
            Groups = testList,
            Name = "testName",
            Description = "testDescription"
        };
        _groupRepoMock.Setup(o => o.GetListById(It.IsAny<int>())).Returns(testList);
        _sut.SelectedCourse = testCourse;
        _dialogueMock.Setup(o => o.DeleteMessage(It.IsAny<string>())).Returns(MessageBoxResult.Yes);
        _courseRepoMock.Setup(o => o.Delete(It.IsAny<Course>())).Returns(true);
        //Act
        var result = _sut.Delete();
        //Assert
        Assert.Equal(expectResult, result);
    }
}

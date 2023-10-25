using Moq;
using System.Windows;
using Task10.UniversityWPF.Domain.Core.Models;
using Task10.UniversityWPF.Domain.Interfaces;
using Task10.UniversityWPF.MVVM.CRUDViewModels;
using Task10.UniversityWPF.Services;

namespace Task10.UniversityWPF.Tests.ViewModelsTests;
public class StudentCRUDViewModelTests
{
    private readonly StudentCRUDViewModel _sut;
    private readonly Mock<IStudentRepository> _studentRepoMock;
    private readonly Mock<ICourseRepository> _courseRepoMock;
    private readonly Mock<IDialogueService> _dialogueServiceMock;
    public StudentCRUDViewModelTests()
    {
        _studentRepoMock = new Mock<IStudentRepository>();
        _courseRepoMock = new Mock<ICourseRepository>();
        _dialogueServiceMock = new Mock<IDialogueService>();
        _sut = new StudentCRUDViewModel(_studentRepoMock.Object, _courseRepoMock.Object, _dialogueServiceMock.Object);
    }

    [Theory]
    [InlineData("test", "test", true)]
    [InlineData("test", null, false)]
    [InlineData(null, null, false)]
    public void StudentCRUDViewModel_CreateStudent_ShouldReturnBoolResult(string firstName, string lastName, bool expectResult)
    {
        //Arrange
        var testGroup = new Group { GroupId = 1 };
        _sut.SelectedGroup = testGroup;
        _sut.FirstName = firstName;
        _sut.LastName = lastName;
        _studentRepoMock.Setup(o => o.Create(It.IsAny<Student>())).Returns(true);
        //Act
        var result = _sut.CreateStudent();
        //Assert
        Assert.Equal(result, expectResult);
    }

    [Theory]
    [InlineData("test", "test", true)]
    [InlineData("test", null, false)]
    [InlineData(null, null, false)]
    public void StudentCRUDViewModel_EditStudent_ShouldReturnBoolResult(string firstName, string lastName, bool expectResult)
    {
        //Arrange
        var testStudent = new Student();
        _sut.SelectedStudent = testStudent;
        _sut.FirstName = firstName;
        _sut.LastName = lastName;
        _studentRepoMock.Setup(o => o.Edit(It.IsAny<Student>())).Returns(true);
        //Act
        var result = _sut.EditStudent();
        //Assert
        Assert.Equal(result, expectResult);
    }

    [Fact]
    public void StudentCRUDViewModel_DeleteStudent_ShouldReturnBoolResult()
    {
        //Arrange
        var testStudent = new Student
        {
            FirstName = "Test",
            LastName = "Test"
        };

        _sut.SelectedStudent = testStudent;
        _dialogueServiceMock.Setup(o => o.DeleteMessage(It.IsAny<string>())).Returns(MessageBoxResult.Yes);
        _studentRepoMock.Setup(o => o.Delete(It.IsAny<Student>())).Returns(true);
        //Act
        var result = _sut.Delete();
        //Assert
        Assert.True(result);
    }
}

using Moq;
using System.Windows;
using Task10.UniversityWPF.Domain.Core.Models;
using Task10.UniversityWPF.Domain.Interfaces;
using Task10.UniversityWPF.MVVM.CRUDViewModels;
using Task10.UniversityWPF.Services;

namespace Task10.UniversityWPF.Tests.ViewModelsTests;
public class TeachersCRUDVIewModelTests
{
    private readonly TeacherCRUDViewModel _sut;
    private readonly Mock<ITeacherRepository> _teacherRepoMock;
    private readonly Mock<IDialogueService> _dialogueServiceMock;
    public TeachersCRUDVIewModelTests()
    {
        _teacherRepoMock = new Mock<ITeacherRepository>();
        _dialogueServiceMock = new Mock<IDialogueService>();
        _sut = new TeacherCRUDViewModel(_teacherRepoMock.Object, _dialogueServiceMock.Object);
    }

    [Theory]
    [InlineData("test", "test", true)]
    [InlineData(null, "test", false)]
    [InlineData(null, null, false)]
    public void TeacherCRUDViewModel_Edit_ShouldReturnBoolResult(string name, string sureName, bool expectResult)
    {
        //Arrange
        _sut.SelectedTeacher = new Teacher();
        _sut.Name = name;
        _sut.Surename = sureName;
        _teacherRepoMock.Setup(o => o.Edit(It.IsAny<Teacher>())).Returns(true);
        //Act
        var result = _sut.Edit();
        //Assert
        Assert.Equal(expectResult, result);
    }

    [Theory]
    [InlineData("test", "test", true)]
    [InlineData(null, "test", false)]
    [InlineData(null, null, false)]
    public void TeacherCRUDViewModel_Add_ShouldReturnBoolResult(string name, string surename, bool expectResult)
    {
        //Arrange
        _sut.Name = name;
        _sut.Surename = surename;
        _teacherRepoMock.Setup(o => o.Create(It.IsAny<Teacher>())).Returns(true);
        //Act
        var result = _sut.Add();
        //Assert
        Assert.Equal(expectResult, result);
    }

    [Fact]
    public void TeacherCRUDViewModel_Delete_ShouldReturnTrueOnDelete()
    {
        //Arrange
        _sut.SelectedTeacher = new Teacher();
        _dialogueServiceMock.Setup(o => o.DeleteMessage(It.IsAny<string>())).Returns(MessageBoxResult.Yes);
        _teacherRepoMock.Setup(o => o.Delete(It.IsAny<Teacher>())).Returns(true);
        //Act
        var result = _sut.Delete();
        //Assert
        Assert.True(result);
    }
}

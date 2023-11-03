using Castle.DynamicProxy.Generators;
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
    public async void TeacherCRUDViewModel_Edit_ShouldReturnBoolResult(string name, string sureName, bool expectResult)
    {
        //Arrange
        _sut.SelectedTeacher = new Teacher();
        _sut.Name = name;
        _sut.Surename = sureName;
        _teacherRepoMock.Setup(o => o.EditAsync(It.IsAny<Teacher>())).ReturnsAsync(true);
        //Act
        var result = await _sut.Edit();
        //Assert
        Assert.Equal(expectResult, result);
    }

    [Theory]
    [InlineData("test", "test", true)]
    [InlineData(null, "test", false)]
    [InlineData(null, null, false)]
    public async void TeacherCRUDViewModel_Add_ShouldReturnBoolResult(string name, string surename, bool expectResult)
    {
        //Arrange
        _sut.Name = name;
        _sut.Surename = surename;
        _teacherRepoMock.Setup(o => o.CreateAsync(It.IsAny<Teacher>())).ReturnsAsync(true);
        //Act
        var result = await _sut.Add();
        //Assert
        Assert.Equal(expectResult, result);
    }

    [Fact]
    public async void TeacherCRUDViewModel_Delete_ShouldReturnTrueOnDelete()
    {
        //Arrange
        var testTeacher = new Teacher();
        _sut.SelectedTeacher = testTeacher;
        _dialogueServiceMock.Setup(o => o.DeleteMessage(It.IsAny<string>())).Returns(MessageBoxResult.Yes);
        _teacherRepoMock.Setup(o => o.DeleteAsync(It.IsAny<Teacher>())).ReturnsAsync(true);
        //Act
        var result = await _sut.Delete(testTeacher);
        //Assert
        Assert.True(result);
    }
}

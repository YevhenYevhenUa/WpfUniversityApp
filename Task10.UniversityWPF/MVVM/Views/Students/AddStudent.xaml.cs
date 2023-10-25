using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Task10.UniversityWPF.Domain.Core.Models;
using Task10.UniversityWPF.MVVM.CRUDViewModels;

namespace Task10.UniversityWPF.MVVM.Views.Students;
/// <summary>
/// Interaction logic for AddStudent.xaml
/// </summary>
public partial class AddStudent : Window
{
    public AddStudent(StudentCRUDViewModel studentCRUD)
    {
        InitializeComponent();
        DataContext = studentCRUD;
    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var course = CourseCB.SelectedItem as Task10.UniversityWPF.Domain.Core.Models.Course;
        GroupCB.ItemsSource = course.Groups;
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        this.Visibility = Visibility.Hidden;
        e.Cancel = true;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}

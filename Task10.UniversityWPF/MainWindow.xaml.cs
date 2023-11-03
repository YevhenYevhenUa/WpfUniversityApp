using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Task10.UniversityWPF.MVVM.ViewModels;
using System.Drawing.Printing;
using System;

namespace Task10.UniversityWPF;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly MainWindowViewModel _mainVM;

    public MainWindow(MainWindowViewModel mainVM)
    {
        InitializeComponent();
        DataContext = mainVM;
        _mainVM = mainVM;
        GroupList.Items.GroupDescriptions.Add(new PropertyGroupDescription("Course.Name"));
        StudentsList.Items.GroupDescriptions.Add(new PropertyGroupDescription("Group.Name"));
    }

    private void myTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (e.Source is TabControl)
        {
            _mainVM.SelectedTab = (TabItem)myTreeView.SelectedItem;
        }
        else
        {
            _mainVM.SelectedItem = myTreeView.SelectedItem;
        }
    }

    private void TabControl_Loaded(object sender, RoutedEventArgs e)
    {
        var tabControl = (TabControl)sender;
        tabControl.SelectedIndex = 0;

    }

    private async void myTreeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        await _mainVM.EditCommand();
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        Application.Current.Shutdown();
    }

}


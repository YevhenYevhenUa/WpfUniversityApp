using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Task10.UniversityWPF.MVVM.CRUDViewModels;

namespace Task10.UniversityWPF.MVVM.Views.Groups;
/// <summary>
/// Interaction logic for AddGroup.xaml
/// </summary>
public partial class AddGroup : Window
{
    public AddGroup(GroupCRUDVIewModel groupCRUD)
    {
        InitializeComponent();
        DataContext = groupCRUD;
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

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Task10.UniversityWPF.MVVM.ViewModels;
public static class VIewModelDependencies
{
    public static IServiceCollection GetViewModelDependencies(this IServiceCollection services)
    {
        services.AddScoped<CourseViewModel>();
        services.AddScoped<GroupViewModel>();
        services.AddScoped<StudentViewModel>();
        services.AddScoped<TeacherViewModel>();
        services.AddScoped<MainWindowViewModel>();

        return services;
    }

    public static void Refresh<T>(this ObservableCollection<T> value)
    {
        CollectionViewSource.GetDefaultView(value).Refresh();
    }
}

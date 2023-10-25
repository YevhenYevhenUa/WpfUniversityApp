using Microsoft.Extensions.DependencyInjection;
using Task10.UniversityWPF.MVVM.Views.Course;
using Task10.UniversityWPF.MVVM.Views.Groups;
using Task10.UniversityWPF.MVVM.Views.Students;
using Task10.UniversityWPF.MVVM.Views.Teachers;

namespace Task10.UniversityWPF.MVVM;
public static class ViewsDependecies
{
    public static IServiceCollection GetViewsDependencies(this IServiceCollection services)
    {
        services.AddScoped<EditCourse>();
        services.AddScoped<EditGroup>();
        services.AddScoped<EditStudent>();
        services.AddScoped<EditTeacher>();
        services.AddScoped<AddCourse>();
        services.AddScoped<AddGroup>();
        services.AddScoped<AddStudent>();
        services.AddScoped<AddTeacher>();

        return services;
    }
}

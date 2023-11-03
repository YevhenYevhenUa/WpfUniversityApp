using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task10.UniversityWPF.MVVM.CRUDViewModels;
public static class CRUDDependencies
{
    public static IServiceCollection GetCRUDDependencies(this IServiceCollection services)
    {
        services.AddScoped<CourseCRUDViewModel>();
        services.AddScoped<GroupCRUDVIewModel>();
        services.AddScoped<StudentCRUDViewModel>();
        services.AddScoped<TeacherCRUDViewModel>();

        return services;
    }
}

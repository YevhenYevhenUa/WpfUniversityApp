using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Task10.UniversityWPF.Domain.Interfaces;
using Task10.UniversityWPF.Infrastructure.Data.Repos;

namespace Task10.UniversityWPF.Infrastructure.Data;
public static class DataDependencies
{
    public static IServiceCollection GetRepositoryDependencies(this IServiceCollection services)
    {
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();

        return services;
    }

    public static IServiceCollection GetDbContextDependency(this IServiceCollection services, string connectionstring)
    {
        services.AddDbContext<University20Context>(options =>
                options.UseSqlServer(connectionstring));
        return services;
    }


}

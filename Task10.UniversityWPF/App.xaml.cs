using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Windows;
using Task10.UniversityWPF.Domain.Interfaces;
using Task10.UniversityWPF.Infrastructure.Data;
using Task10.UniversityWPF.Infrastructure.Data.Repos;
using Task10.UniversityWPF.MVVM;
using Task10.UniversityWPF.MVVM.CRUDViewModels;
using Task10.UniversityWPF.MVVM.ViewModels;
using Task10.UniversityWPF.MVVM.Views.Course;
using Task10.UniversityWPF.MVVM.Views.Groups;
using Task10.UniversityWPF.MVVM.Views.Students;
using Task10.UniversityWPF.MVVM.Views.Teachers;
using Task10.UniversityWPF.Services;

namespace Task10.UniversityWPF;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IHost? AppHost { get; private set; }
    private string _connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<MainWindow>();
                services.GetRepositoryDependencies();
                services.GetViewsDependencies();

                services.AddScoped<CourseViewModel>();
                services.AddScoped<GroupViewModel>();
                services.AddScoped<StudentViewModel>();
                services.AddScoped<TeacherViewModel>();
                services.AddScoped<MainWindowViewModel>();

                services.AddScoped<CourseCRUDViewModel>();
                services.AddScoped<GroupCRUDVIewModel>();
                services.AddScoped<StudentCRUDViewModel>();
                services.AddScoped<TeacherCRUDViewModel>();

                services.AddDbContext<University20Context>(options=>
                options.UseSqlServer(_connectionstring));

                
                services.AddScoped<IDialogueService, DialogueService>();
                
            })
            .Build();

    }

    protected override void OnStartup(StartupEventArgs e)
    {
        AppHost!.Start();
        var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
        startupForm.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }

    
}

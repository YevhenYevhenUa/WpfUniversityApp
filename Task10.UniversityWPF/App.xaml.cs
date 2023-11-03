using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using Task10.UniversityWPF.Infrastructure.Data;
using Task10.UniversityWPF.MVVM;
using Task10.UniversityWPF.MVVM.CRUDViewModels;
using Task10.UniversityWPF.MVVM.ViewModels;
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
                services.GetViewModelDependencies();
                services.GetCRUDDependencies();
                services.GetDbContextDependency(_connectionstring);
                services.AddScoped<IDialogueService, DialogueService>();
                services.AddScoped<IFileIOService, FileIoService>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();
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

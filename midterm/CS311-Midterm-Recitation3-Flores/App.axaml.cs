using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CS311_CS3A_2026_Flores.Services;
using CS311_CS3A_2026_Flores.ViewModels;
using CS311_CS3A_2026_Flores.Views;
using Microsoft.Extensions.DependencyInjection;

namespace CS311_CS3A_2026_Flores;

public partial class App : Application
{

    public static IServiceProvider? Services { get; private set; }


    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void ConfigureServices(IServiceCollection collection)
    {
        collection.AddSingleton<IDatabaseService, DatabaseService>();
        collection.AddTransient<ViewModels.LoginViewModel>();
        collection.AddTransient<ViewModels.AccountsViewModel>();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var collection = new ServiceCollection();
        ConfigureServices(collection);
        Services = collection.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var loginViewModel = Services!.GetRequiredService<ViewModels.LoginViewModel>();
            var loginView = new Views.Login
            {
                DataContext = loginViewModel
            };
            loginViewModel.OnLoginSuccess += (user) =>
            {
                var mainWindowViewModel = ActivatorUtilities.CreateInstance<MainWindowViewModel>(Services, user);
                var mainWindowView = new MainWindow
                {
                    DataContext = mainWindowViewModel
                };

                desktop.MainWindow = mainWindowView;
                mainWindowView.Show();
                loginView.Hide();

                mainWindowViewModel.OnLogout += () =>
                {
                    desktop.MainWindow = loginView;
                    mainWindowView.Close();
                    loginView.Show();
                };
            };

            desktop.MainWindow = loginView;
        }

        base.OnFrameworkInitializationCompleted();
    }
}
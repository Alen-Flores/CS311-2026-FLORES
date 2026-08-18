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
        collection.AddSingleton(_ =>
            new DatabaseService(
                "127.0.0.1", "CS311-CS3A-2026-FLORES", "marlon", "flores"
                )
        );

        collection.AddSingleton<IDatabaseService>(sp =>
            sp.GetRequiredService<DatabaseService>());
        collection.AddSingleton<IAuthService, AuthService>();
        collection.AddSingleton<IUserService, UserService>();
        collection.AddSingleton<IEquipmentService, EquipmentService>();
        collection.AddSingleton<ILoggingService, LoggingService>();

        collection.AddTransient<LoginViewModel>();
        collection.AddTransient<MainWindowViewModel>();
        collection.AddTransient<AccountsViewModel>();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var collection = new ServiceCollection();
        ConfigureServices(collection);
        Services = collection.BuildServiceProvider();
        var auth = Services.GetRequiredService<IAuthService>();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var loginViewModel = Services!.GetRequiredService<LoginViewModel>();
            var loginView = new Login { DataContext = loginViewModel };

            var mainwindowViewModel = Services!.GetRequiredService<MainWindowViewModel>();
            var mainWindowView = new MainWindow { DataContext = mainwindowViewModel };

            auth.RequestLogin += async chan =>
            {
                desktop.MainWindow = loginView;
                mainWindowView.Hide();
                loginView.Show();

                var user = await loginViewModel.GetUser();
                await chan.WriteAsync(user);

                desktop.MainWindow = mainWindowView;
                loginView.Hide();
                mainWindowView.Show();
            };

            auth.AccountChanged += async newUser =>
            {
                if (newUser == null) await auth.Login();
            };

            auth.Login();
        }

        base.OnFrameworkInitializationCompleted();
    }
}
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using _253503_Ankuda.Applicationn;
using _253503_Ankuda.Persistence;
using _253503_Ankuda.UI.Pages;
using _253503_Ankuda.UI.ViewModels;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using _253503_Ankuda.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace _253503_Ankuda.UI
{
    public static class MauiProgram
    {
        static IServiceCollection RegisterPages(this IServiceCollection services)
        {
            services
                .AddSingleton<TeamAdding>()
                .AddSingleton<TeamsPage>()
                .AddSingleton<CompetitorAdding>()
                .AddTransient<CompetitorDetails>();
            return services;
        }
        static IServiceCollection RegisterViewModels(this IServiceCollection services)
        {
            services
                .AddSingleton<TeamAddingViewModel>()
                .AddSingleton<TeamsViewModel>()
                .AddSingleton<CompetitorAddingViewModel>()
                .AddTransient<CompetitorDetailsViewModel>();
            return services;
        }
        public static MauiApp CreateMauiApp()
        {
            string settingsStream = "_253503_Ankuda.UI.appsettings.json";
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream(settingsStream);
            builder.Configuration.AddJsonStream(stream);
            var connStr = builder.Configuration.GetConnectionString("SqliteConnection");
            string dataDirectory = FileSystem.Current.AppDataDirectory + "/";
            connStr = String.Format(connStr, dataDirectory);
            var options = new DbContextOptionsBuilder<AppDbContext>()
             .UseSqlite(connStr)
             .Options;

            builder.Services
                .AddApplicationn()
                .AddPersistence(options)
                .RegisterPages()
                .RegisterViewModels();
            DbInitializer.Initialize(builder.Services.BuildServiceProvider()).Wait();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
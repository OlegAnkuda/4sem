using Labs.Services;
using Labs.Entities;
using Microsoft.Extensions.Logging;

namespace Labs
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .Services.AddTransient<IDbService, SQLiteService>();
            builder.Services.AddSingleton<SQLiteLab>();

            builder.Services.AddHttpClient<IRateService>(opt =>
            opt.BaseAddress = new Uri("https://www.nbrb.by/api/exrates/rates"));
            builder.Services.AddTransient<IRateService, RateService>();
            builder.Services.AddSingleton<Converter>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

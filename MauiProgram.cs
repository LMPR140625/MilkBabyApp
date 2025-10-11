using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MilkBabyApp.Services;
using MilkBabyApp.ViewModels;
using MilkBabyApp.Views;

namespace MilkBabyApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Font Awesome 6 Brands-Regular-400.otf", "FaBrands");
                    fonts.AddFont("Font Awesome 6 Free-Regular-400.otf", "FaRegular");
                    fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "FaSolid");
                });

            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "babymilk.db3");

            builder.Services.AddSingleton<IDatabase>(new Database(dbPath));
            builder.Services.AddSingleton<ReportViewModel>();
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<MainPageView>();
            builder.Services.AddTransient<ReportView>();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            
            


            return builder.Build();
        }
    }
}

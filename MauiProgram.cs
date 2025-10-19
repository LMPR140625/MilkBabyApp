using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using Microcharts.Maui;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MilkBabyApp.Data;
using MilkBabyApp.Services;
using MilkBabyApp.ViewModels;
using MilkBabyApp.Views;
using SkiaSharp.Views.Maui.Controls.Hosting;

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
                .UseMauiCommunityToolkitCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Font Awesome 6 Brands-Regular-400.otf", "FaBrands");
                    fonts.AddFont("Font Awesome 6 Free-Regular-400.otf", "FaRegular");
                    fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "FaSolid");
                })
                .UseMicrocharts();

            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "babymilk.db3");

            //builder.Services.AddSingleton<IDatabase>(new Database(dbPath));
           
#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<ReportViewModel>();
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<MainPageView>();
            builder.Services.AddTransient<ReportView>();
            builder.Services.AddTransient<SettingsView>();
            builder.Services.AddTransient<SettingsViewModel>();
            builder.Services.AddSingleton<DatabaseContext>();


            return builder.Build();
        }
    }
}

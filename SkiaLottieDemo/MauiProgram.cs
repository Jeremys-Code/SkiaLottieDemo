using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SkiaLottieDemo.ViewModels;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace SkiaLottieDemo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseSkiaSharp()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddHttpClient("weatherClient", client =>
            {
                client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("ThePhotographersForecast", "6.0"));
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/geo+json"));
            });

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            MauiApp app = builder.Build();
            new App(app.Services);
            return app;
        }
    }
}

using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using NagyGergelyProjekt3.ViewModels;
using NagyGergelyProjekt3.Models;
using NagyGergelyProjekt3.Views;

namespace NagyGergelyProjekt3
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>().UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fa_solid.ttf", "FontAwesome");
                    fonts.AddFont("fa_brands.ttf", "FontAwesomeBrands");
                    fonts.AddFont("fa_solid_900.ttf", "FontAwesomeMore");
                    fonts.AddFont("toxigenesisbd.otf", "customFont");
                });
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<GiveawaysPage>();
            builder.Services.AddSingleton<GiveawaysPageViewModel>();
            builder.Services.AddSingleton<GiveawaysByPlatformsMenuPage>();
            builder.Services.AddSingleton<GiveawaysByPlatformsMenuPageViewModel>();
            builder.Services.AddSingleton<GiveawayDetailsPage>();
            builder.Services.AddSingleton<GiveawayDetailsViewModel>();
            builder.Services.AddSingleton<GiveawaysByTypesMenuPage>();
            builder.Services.AddSingleton<GiveawaysByTypesMenuPageViewModel>();
            builder.Services.AddSingleton<UserProfilePage>();
            builder.Services.AddSingleton<UserProfilePageViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
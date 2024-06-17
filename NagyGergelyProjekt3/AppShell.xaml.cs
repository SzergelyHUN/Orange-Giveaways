using NagyGergelyProjekt3.Views;

namespace NagyGergelyProjekt3
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(GiveawaysPage), typeof(GiveawaysPage));
            Routing.RegisterRoute(nameof(GiveawaysByPlatformsMenuPage), typeof(GiveawaysByPlatformsMenuPage));
            Routing.RegisterRoute(nameof(GiveawayDetailsPage), typeof(GiveawayDetailsPage));
            Routing.RegisterRoute(nameof(GiveawaysByTypesMenuPage), typeof(GiveawaysByTypesMenuPage));
            Routing.RegisterRoute(nameof(InformationsPage), typeof(InformationsPage));
            Routing.RegisterRoute(nameof(UserProfilePage), typeof(UserProfilePage));

        }
    }
}
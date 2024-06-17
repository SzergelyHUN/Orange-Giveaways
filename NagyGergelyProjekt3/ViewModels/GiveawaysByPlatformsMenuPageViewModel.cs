using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NagyGergelyProjekt3.ViewModels
{
    public partial class GiveawaysByPlatformsMenuPageViewModel : ObservableObject
    {

        [RelayCommand]
        void NavigateTo(string destination)
        {

            NetworkAccess access = Connectivity.Current.NetworkAccess;
            if (access == NetworkAccess.Internet)
            {
                try
                {
                    switch (destination)
                    {
                        case "back":
                            Shell.Current.GoToAsync($"..");
                            break;
                        case "ps4.ps5":
                            Shell.Current.GoToAsync($"GiveawaysPage", new Dictionary<string, object> { { "giveawayPlatform", "ps4.ps5" } });
                            break;
                        case "pc":
                            Shell.Current.GoToAsync($"GiveawaysPage", new Dictionary<string, object> { { "giveawayPlatform", "pc" } });
                            break;
                        case "xbox-series-xs.xbox-one.xbox-360":
                            Shell.Current.GoToAsync($"GiveawaysPage", new Dictionary<string, object> { { "giveawayPlatform", "xbox-series-xs.xbox-one.xbox-360" } });
                            break;
                        case "android.ios":
                            Shell.Current.GoToAsync($"GiveawaysPage", new Dictionary<string, object> { { "giveawayPlatform", "android.ios" } });
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception)
                {
                    Shell.Current.DisplayAlert("Hiba!", "Az oldal nem nyitható meg!", "Ok");
                }
            }
            else
            {
                Shell.Current.DisplayAlert("Hiba!", "Nincs internet kapcsolat!", "Ok");
            }
 
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NagyGergelyProjekt3.ViewModels
{
    public partial class GiveawaysByTypesMenuPageViewModel : ObservableObject
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
                        case "game":
                            Shell.Current.GoToAsync($"GiveawaysPage", new Dictionary<string, object> { { "giveawayType", "game" } });
                            break;
                        case "loot":
                            Shell.Current.GoToAsync($"GiveawaysPage", new Dictionary<string, object> { { "giveawayType", "loot" } });
                            break;
                        case "beta":
                            Shell.Current.GoToAsync($"GiveawaysPage", new Dictionary<string, object> { { "giveawayType", "beta" } });
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

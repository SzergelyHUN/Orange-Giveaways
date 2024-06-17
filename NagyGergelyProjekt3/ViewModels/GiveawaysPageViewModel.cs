using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NagyGergelyProjekt3.Models;
using NagyGergelyProjekt3.Services;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NagyGergelyProjekt3.ViewModels
{
    [QueryProperty(nameof(giveAwayByPlatform), "giveawayPlatform")]
    [QueryProperty(nameof(giveAwayByType), "giveawayType")]

    public partial class GiveawaysPageViewModel : BindableObject, IQueryAttributable, INotifyPropertyChanged
    {

        public IDictionary<string, object> giveawayDictionary = new Dictionary<string, object>();
        public ObservableCollection<Giveaway> giveAway { get; set; }

        public string giveAwayByPlatform { get; set; }
        public string giveAwayByType { get; set; }

        public Giveaway selectedGiveaway { get; set; }


        public GiveawaysPageViewModel() 
        {
            giveAway = new ObservableCollection<Giveaway>();
            Refresh();
        }

        [RelayCommand]
        public async Task details()
        {
            if (selectedGiveaway == null)
            {
                return;
            }
            await Shell.Current.GoToAsync("GiveawayDetailsPage", new Dictionary<string, object> { { "giveAway", selectedGiveaway } });
            selectedGiveaway = null;
        }

        private async void GetAllGames()
        {
            IEnumerable<Giveaway> list = await DataService.GetGiveaways();
            list.ToList().ForEach(giveaway => giveAway.Add(giveaway));

        }
        private async void GetAllGamesByPlatform(string platform)
        {
            IEnumerable<Giveaway> list = await DataService.GetGiveawaysByPlatform(platform);
            list.ToList().ForEach(giveaway => giveAway.Add(giveaway));
        }        
        private async void GetAllGamesByType(string type)
        {
            IEnumerable<Giveaway> list = await DataService.GetGiveawaysByType(type);
            list.ToList().ForEach(giveaway => giveAway.Add(giveaway));
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            giveawayDictionary = query;
            Refresh();
        }

        [RelayCommand]
        void Refresh()
        {
            giveAway.Clear();
            NetworkAccess access = Connectivity.Current.NetworkAccess;
            if (access == NetworkAccess.Internet)
            {

                if (giveawayDictionary.Keys.Contains("giveawayPlatform"))
                {
                    if (giveawayDictionary.Count == 1)
                    {
                        giveAwayByPlatform = giveawayDictionary["giveawayPlatform"] as string;
                        OnPropertyChanged(nameof(giveAwayByPlatform));
                        GetAllGamesByPlatform(giveAwayByPlatform);
                    }

                }
                else if (giveawayDictionary.Keys.Contains("giveawayType"))
                {
                    if (giveawayDictionary.Count == 1)
                    {
                        giveAwayByType = giveawayDictionary["giveawayType"] as string;
                        OnPropertyChanged(nameof(giveAwayByType));
                        GetAllGamesByType(giveAwayByType);
                    }

                }
                else
                {
                    GetAllGames();
                }
            }
            else
            {
                Shell.Current.DisplayAlert("Hiba!", "Nincs internet kapcsolat!", "Ok");
            }

        }

        [RelayCommand]
        void NavigateTo(string destination)
        {

            NetworkAccess access = Connectivity.Current.NetworkAccess;
            if (access == NetworkAccess.Internet)
            {
                try
                {
                    Shell.Current.GoToAsync($"{destination}", true);
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

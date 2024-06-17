using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using NagyGergelyProjekt3.Models;
using NagyGergelyProjekt3.Services;
using NagyGergelyProjekt3.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NagyGergelyProjekt3.ViewModels
{
    [QueryProperty(nameof(giveaway), "giveAway")]

    public partial class GiveawayDetailsViewModel : BindableObject, IQueryAttributable
    {

        public Giveaway giveaway { get; set; }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            giveaway = query["giveAway"] as Giveaway;
            OnPropertyChanged(nameof(giveaway));
        }

        [RelayCommand]
        Task OpenGiveawayWebsite()
        {
            NetworkAccess access = Connectivity.Current.NetworkAccess;
            if (access == NetworkAccess.Internet)
            {
                try
                {
                    Uri uri = new Uri(giveaway.open_giveaway);
                    return Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
                }
                catch (Exception)
                {
                    return Shell.Current.DisplayAlert("Hiba!", "Az oldal nem nyitható meg!", "Ok");
                }
            }
            else
            {
                return Shell.Current.DisplayAlert("Hiba!", "Nincs internet kapcsolat!", "Ok");
            }
        }

        [RelayCommand]
        public async Task ClaimGiveaway()
        {
            string userData = SecureStorage.Default.GetAsync("userData").Result;
            if (userData != "" && userData != null)
            {
                var adat = await SQLiteService.getGiveawayById(giveaway.id);
                if (adat == null)
                {
                    await SQLiteService.addGiveaway(giveaway);
                    var toast = Toast.Make("Sikeres mentés", ToastDuration.Long, 14);
                    await toast.Show();
                }
                else
                {
                    if (await Application.Current.MainPage.DisplayAlert("Kérdés", "Már begyűjtötted, szeretnéd törölni?", "Igen", "Nem"))
                    {
                        await SQLiteService.removeGiveawayById(giveaway.id);
                        var toast = Toast.Make("Sikeres törlés", ToastDuration.Long, 14);
                        await toast.Show();

                    }
                }

            }
            else
            {
                var toast = Toast.Make("A mentéshez profilt kell létrehoznod", ToastDuration.Long, 14);
                await toast.Show();
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
                    Shell.Current.GoToAsync($"{destination}");
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

        [RelayCommand]
        public async Task ShareGiveaway()
        {
            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Text = $"Nézd a {giveaway.title} most ingyen megszerezhető! Szerezd meg míg le nem jár az ajánlat ezen a linken" +
                $" keresztül: {giveaway.gamerpower_url}",
                Title = "Ingyen játék"
            });
        }

    }

}


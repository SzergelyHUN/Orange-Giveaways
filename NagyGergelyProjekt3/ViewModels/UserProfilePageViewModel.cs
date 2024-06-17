using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using NagyGergelyProjekt3.Models;
using NagyGergelyProjekt3.Services;
using NagyGergelyProjekt3.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace NagyGergelyProjekt3.ViewModels
{
    public partial class UserProfilePageViewModel : BindableObject, INotifyPropertyChanged
    {

        public ObservableCollection<Giveaway> claimedGiveaways { get; set; }
        private int claimedGiveawaysCount;

        public int ClaimedGiveawaysCount
        {
            get { return claimedGiveawaysCount; }
            set { claimedGiveawaysCount = value; OnPropertyChanged(nameof(ClaimedGiveawaysCount)); }
        }

        private decimal mostValuableClaimedGiveaways;

        public decimal MostValuableClaimedGiveaways
        {
            get { return mostValuableClaimedGiveaways; }
            set { mostValuableClaimedGiveaways = value; OnPropertyChanged(nameof(MostValuableClaimedGiveaways)); }
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(nameof(UserName)); }
        }

        private bool alertIsVisible;

        public bool AlertIsVisible
        {
            get { return alertIsVisible; }
            set { alertIsVisible = value; OnPropertyChanged(nameof(AlertIsVisible)); }
        }

        private bool changeUsername;

        public bool ChangeUsername
        {
            get { return changeUsername; }
            set { changeUsername = value; OnPropertyChanged(nameof(ChangeUsername)); }
        }

        private bool saveUsername;

        public bool SaveUsername
        {
            get { return saveUsername; }
            set { saveUsername = value; OnPropertyChanged(nameof(SaveUsername)); }
        }

        private string userProfilePicture;

        public string UserProfilePicture
        {
            get { return userProfilePicture; }
            set { userProfilePicture = value; OnPropertyChanged(nameof(UserProfilePicture)); }
        }

        public UserProfilePageViewModel()
        {
            claimedGiveaways = new ObservableCollection<Giveaway>();
        }

        [RelayCommand]
        public void Appearing()
        { 
            GetLocalstorageData();
        }

        private async void GetLocalstorageData()
        {
            string userData = SecureStorage.Default.GetAsync("userData").Result;
            if (userData != "" && userData != null)
            {

                AlertIsVisible = false;
                ChangeUsername = true;
                SaveUsername = false;

                IEnumerable<Giveaway> list = await SQLiteService.getAllGiveaway();
                claimedGiveaways.Clear();
                list.ToList().ForEach(giveaway => claimedGiveaways.Add(giveaway));
                ClaimedGiveawaysCount = claimedGiveaways.Count;
                List<string> ErtekStringLista = new List<string>();
                foreach (var item in list)
                {
                    if(item.worth != "N/A")
                    {
                        ErtekStringLista.Add(item.worth);
                    }
                }
                if (ErtekStringLista.Count != 0)
                {
                    MostValuableClaimedGiveaways = ErtekStringLista.Select(price => decimal.Parse(price.TrimStart('$'), NumberStyles.Currency, CultureInfo.InvariantCulture)).Max();
                }
                string[] slices = userData.Split(";");

                UserName = slices[0];
                UserProfilePicture = slices[1];

            }
            else
            {
                AlertIsVisible = true;
                ChangeUsername = false;
                SaveUsername = true;
                var toast = Toast.Make("A mentéshez profilt kell létrehoznod", ToastDuration.Short, 14);
                await toast.Show();
                UserProfilePicture = "profilepictureplaceholder.png";
            }
        }


        public async void GoToGiveawayDetailsPage(Giveaway selectedGiveaway)
        {
            await Shell.Current.GoToAsync(nameof(GiveawayDetailsPage), new Dictionary<string, object> { { "giveAway", selectedGiveaway } });
        }

        [RelayCommand]
        public async Task UserNameSave(string destination)
        {
            if(destination == "Save")
            {
                string userData = SecureStorage.Default.GetAsync("userData").Result;

                if (userData != "" && userData != null && UserName != null && UserName != "")
                {
                    await SecureStorage.Default.SetAsync("userData", $"{UserName};{UserProfilePicture}");
                    ChangeUsername = true;
                    SaveUsername = false;
                    var toast = Toast.Make("Név megváltoztatva", ToastDuration.Short, 14);
                    await toast.Show();
                }
                else if(UserName != null && UserName != "")
                {
                    await SecureStorage.Default.SetAsync("userData", $"{UserName};{UserProfilePicture}");
                    GetLocalstorageData();
                    var toast = Toast.Make("Profil sikeresen létrehozva", ToastDuration.Short, 14);
                    await toast.Show();
                }
                else
                {
                    var toast = Toast.Make("A név nem lehet üres", ToastDuration.Short, 14);
                    await toast.Show();
                }

            }
            else
            {
                ChangeUsername = false;
                SaveUsername = true;
            }

        }

        [RelayCommand]
        public async Task ChangeUserProfilePicture()
        {
            string userData = SecureStorage.Default.GetAsync("userData").Result;

            if (userData != "" && userData != null)
            {
                try
                {
                    var result = await FilePicker.PickAsync(new PickOptions
                    {
                        PickerTitle = "Pick Image",
                        FileTypes = FilePickerFileType.Images
                    });
                    if (result == null)
                    {
                        return;
                    }
                    else                    
                    {
                        var stream = await result.OpenReadAsync();
                        UserProfilePicture = result.FullPath;
                        await SecureStorage.Default.SetAsync("userData", $"{UserName};{UserProfilePicture}");
                    }

                }
                catch(Exception error)
                {
                    await Shell.Current.DisplayAlert("Hiba!", $"{error.Message}", "Ok");
                }
            }
            else
            {
                var toast = Toast.Make("A profilkép változtatásához profil szükséges", ToastDuration.Short, 14);
                await toast.Show();
            }

        }

    }
}

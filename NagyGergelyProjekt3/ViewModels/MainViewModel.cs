using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
namespace NagyGergelyProjekt3.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {

        public MainViewModel()
        {
            string stringIsLight = SecureStorage.Default.GetAsync("Theme").Result;
            if (stringIsLight == "False")
            {
                App.Current.UserAppTheme = AppTheme.Light;
                IsLight = false;
            }
            else if (stringIsLight == "True")
            {
                App.Current.UserAppTheme = AppTheme.Dark;
                IsLight = true;
            }
 
        }

        Boolean IsLight = true;

        [RelayCommand]
        async Task AppColorChange()
        {

            if (IsLight == true)
            {
                App.Current.UserAppTheme = AppTheme.Light;
                IsLight = false;
                await SecureStorage.Default.SetAsync("Theme", $"{IsLight}");
            }
            else
            {
                App.Current.UserAppTheme = AppTheme.Dark;
                IsLight = true;
                await SecureStorage.Default.SetAsync("Theme", $"{IsLight}");

            }
        }

        [RelayCommand]
        public async Task NavigateTo(string destination)
        {
            
            NetworkAccess access = Connectivity.Current.NetworkAccess;
            if (access == NetworkAccess.Internet)
            {
                try
                {
                    await Shell.Current.GoToAsync($"{destination}", true);
                }
                catch (Exception error)
                {
                    await Shell.Current.DisplayAlert("Hiba!", $"{error.Message}", "Ok") ;
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Hiba!", "Nincs internet kapcsolat!", "Ok");
            }
        }

    }
}

namespace NagyGergelyProjekt3.Views;

public partial class InformationsPage : ContentPage
{
	public InformationsPage()
	{
		InitializeComponent();
	}

    private void NavigateTo_Clicked(object sender, EventArgs e)
    {
        NetworkAccess access = Connectivity.Current.NetworkAccess;
        if (access == NetworkAccess.Internet)
        {
            try
            {
                Uri uri = new Uri("https://www.gamerpower.com/api-read");
                Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
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
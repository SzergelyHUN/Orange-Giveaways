using NagyGergelyProjekt3.Models;
using NagyGergelyProjekt3.ViewModels;

namespace NagyGergelyProjekt3.Views;

public partial class UserProfilePage : ContentPage
{

    UserProfilePageViewModel model = new UserProfilePageViewModel();
    public UserProfilePage(UserProfilePageViewModel vm)
    {
		InitializeComponent();
        this.BindingContext = vm;
    }


    private void termkekLstv_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

        if (termkekLstv.SelectedItem != null)
        {
            Giveaway selectedGivewaway = (Giveaway)termkekLstv.SelectedItem;
            model.GoToGiveawayDetailsPage(selectedGivewaway);

        }
    }

}
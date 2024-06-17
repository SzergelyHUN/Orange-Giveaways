using NagyGergelyProjekt3.ViewModels;

namespace NagyGergelyProjekt3.Views;

public partial class GiveawayDetailsPage : ContentPage
{
	public GiveawayDetailsPage(GiveawayDetailsViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}
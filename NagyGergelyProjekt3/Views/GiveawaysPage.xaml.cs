using NagyGergelyProjekt3.ViewModels;

namespace NagyGergelyProjekt3.Views;

public partial class GiveawaysPage : ContentPage
{
	public GiveawaysPage(GiveawaysPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;

    }

}
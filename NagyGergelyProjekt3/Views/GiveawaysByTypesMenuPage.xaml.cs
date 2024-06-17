using NagyGergelyProjekt3.ViewModels;

namespace NagyGergelyProjekt3.Views;

public partial class GiveawaysByTypesMenuPage : ContentPage
{
	public GiveawaysByTypesMenuPage(GiveawaysByTypesMenuPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}

    Button Gomb = new Button();
    private async void WindowLikeAnimate_Clicked(object sender, EventArgs e)
    {
        Gomb = (Button)sender;
        switch (Gomb.Text)
        {
            case "Vissza":
                await Back.RotateYTo(80, 700);
                Back.RotationY = 0;
                break;
            case "Játékok":
                await Game.RotateYTo(80, 700);
                Game.RotationY = 0;
                break;
            case "Kiegészítõk":
                await DLC.RotateYTo(80, 700);
                DLC.RotationY = 0;
                break;
            case "Egyéb":
                await Other.RotateYTo(80, 700);
                Other.RotationY = 0;
                break;
            default:
                break;
        }

    }
}
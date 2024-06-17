using NagyGergelyProjekt3.ViewModels;
namespace NagyGergelyProjekt3.Views;

public partial class GiveawaysByPlatformsMenuPage : ContentPage
{
    public GiveawaysByPlatformsMenuPage(GiveawaysByPlatformsMenuPageViewModel vm)
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
            case "Pc":
                await Pc.RotateYTo(80, 700);
                Pc.RotationY = 0;
                break;
            case "Playstation":
                await Playstation.RotateYTo(80, 700);
                Playstation.RotationY = 0;
                break;
            case "Xbox":
                await Xbox.RotateYTo(80, 700);
                Xbox.RotationY = 0;
                break;
            case "Android/Iphone":
                await Mobil.RotateYTo(80, 700);
                Mobil.RotationY = 0;
                break;
            default:
                break;
        }

    }

}
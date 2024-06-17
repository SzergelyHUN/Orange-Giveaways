using NagyGergelyProjekt3.ViewModels;

namespace NagyGergelyProjekt3
{
    public partial class MainPage : ContentPage
    {

        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;

        }

        Button Gomb = new Button();
        private async void WindowLikeAnimate_Clicked(object sender, EventArgs e)
        {

            Gomb = (Button)sender;
            switch (Gomb.Text)
            {
                case "Összes Giveaway":
                    await AllGiveaways.RotateYTo(80, 700);
                    AllGiveaways.RotationY = 0;
                    break;
                case "Platformok":
                    await Platforms.RotateYTo(80, 700);
                    Platforms.RotationY = 0;
                    break;
                case "Típusok":
                    await Types.RotateYTo(80, 700);
                    Types.RotationY = 0;
                    break;
                default:
                    break;
            }
       
        }

    }
}
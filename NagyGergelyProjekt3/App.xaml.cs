using Plugin.Maui.Audio;

namespace NagyGergelyProjekt3
{
    public partial class App : Application
    {
        private IAudioPlayer _audioPlayer;

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            App.Current.UserAppTheme = AppTheme.Dark;
            var audioManager = AudioManager.Current;
            _audioPlayer = audioManager.CreatePlayer(FileSystem.OpenAppPackageFileAsync("bgmmusic.mp3").Result);
            

        }

        protected override void OnStart()
        {
            _audioPlayer?.Play();

        }

        protected override void OnSleep()
        {
            _audioPlayer?.Stop();
        }

        protected override void OnResume()
        {
            _audioPlayer?.Play();

        }
    }
}
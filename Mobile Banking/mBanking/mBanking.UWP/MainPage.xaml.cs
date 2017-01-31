using Windows.UI;

namespace mBanking.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            Xamarin.FormsMaps.Init("v9JsBRhM7l9CYnNQvDeQ~K40WpXAA2-TMqFQ5E-8kCg~Ah7pYGDru8LC-fHy4bVb1hVsqfuehNwLvUwL6QMqhPj0ifr60fDK7aP5jA5wEzf-");
            FFImageLoading.Forms.WinUWP.CachedImageRenderer.Init();
            LoadApplication(new mBanking.App());

            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                Windows.UI.ViewManagement.StatusBar.GetForCurrentView().BackgroundColor = Colors.Black;
                Windows.UI.ViewManagement.StatusBar.GetForCurrentView().BackgroundOpacity = 1;
                Windows.UI.ViewManagement.StatusBar.GetForCurrentView().ForegroundColor = Color.FromArgb(1, 176, 190, 197);
            }
        }
    }
}

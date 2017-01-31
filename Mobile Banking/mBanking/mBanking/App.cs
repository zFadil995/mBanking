using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace mBanking
{
    public class App : Application
    {
        public App()
        {
            MainPage = new NavigationPage(new mBanking.LoadingPage() { Title = "mBanking" });
        }
        protected override void OnStart()
        {
            setUp();
            getData();
            setMainPage();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            if (DownloadedData.currencies.Count == 0 || DownloadedData.accounts.Count == 0)
            {
                MainPage = new NavigationPage(new mBanking.LoadingPage() { Title = "mBanking" }); getData(); setMainPage();
            }
        }
        private async void setMainPage()
        {
            bool set = false;
            for (int i = 0; i < 500; i++)
            {
                if (DownloadedData.currencies != null || DownloadedData.accounts != null)
                    if (DownloadedData.accounts.Count != 0 && DownloadedData.currencies.Count != 0)
                    {
                        MainPage = new NavigationPage(new mBanking.MainPage() { Title = "mBanking" });
                        set = true; break;
                    }

                await Task.Delay(25);
            }
            if (!set)
                MainPage = new mBanking.ErrorPage() { Title = "NoConnection" };
        }
        private void getData()
        {
            if (DownloadedData.accounts.Count == 0)
                DownloadedData.getAccounts();
            if (DownloadedData.currencies.Count == 0)
                DownloadedData.getCurrencies();
        }
        private void setUp()
        {
            IDistance.Init();
            if (DownloadedData.accounts == null) DownloadedData.accounts = new List<Account>();
            if (DownloadedData.currencies == null) DownloadedData.currencies = new List<Currency>();
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = new System.Globalization.CultureInfo("bs-Latn-BA");
        }
    }
}

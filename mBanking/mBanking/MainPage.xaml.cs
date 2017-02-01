using FFImageLoading.Forms;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace mBanking
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            ToolbarItems.Add(new ToolbarItem("Settings", "settings.png", async () =>
            {
                await Navigation.PushAsync(new mBanking.SettingsPage() { Title = "Settings" });
            }));
        }

        protected override void OnAppearing()
        {
            setATM();
            setAccounts();
            setCurrencies();
            setVisibility();
            base.OnAppearing();
        }

        private void setVisibility()
        {
            /*StackLayout accountWidgetTemp = (StackLayout)widgetsLayout.Children[0];
            StackLayout exchangeWidgetTemp = (StackLayout)widgetsLayout.Children[0];
            StackLayout atmWidgetTemp = (StackLayout)widgetsLayout.Children[0];*/

            widgetsLayout.Children.Remove(accountWidget);
            widgetsLayout.Children.Remove(exchangeWidget);
            widgetsLayout.Children.Remove(atmWidget);

            foreach(string setting in new string[] { Settings.WidgetOne, Settings.WidgetTwo, Settings.WidgetThree })
            {
                if (setting == "Account Widget")
                    widgetsLayout.Children.Add(accountWidget);
                if (setting == "Exchange Widget")
                    widgetsLayout.Children.Add(exchangeWidget);
                if (setting == "ATM Widget")
                    widgetsLayout.Children.Add(atmWidget);
            }


            accountWidget.IsVisible = Settings.AccountWidgetVisible;
            exchangeWidget.IsVisible = Settings.ExchangeWidgetVisible;
            atmWidget.IsVisible = Settings.ATMWidgetVisible;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        void setAccounts()
        {
            foreach (Account acc in DownloadedData.accounts)
            {
                if (acc.id == Settings.AccountID)
                {
                    string[] icons = new string[] { "male.png", "female.png", "banker.png", "saver.png" };
                    accountWidgetName.Text = acc.name;
                    accountWidgetNumber.Text = acc.number;
                    if (icons.Contains(acc.image)) accountWidgetImage.Source = acc.image; else accountWidgetImage.Source = "accounticon.png";
                    accountWidgetBalance.Text = acc.amount.ToString("C2", System.Globalization.CultureInfo.DefaultThreadCurrentCulture);
                }
            }
        }
        void setCurrencies()
        {
            exchangeWidgetDate.Text = DateTime.Now.ToLocalTime().ToString("dd MMMM yyyy");
            foreach (Currency curr in DownloadedData.currencies)
            {
                if (curr.id == Settings.CurrencySourceID)
                    exchangeWidgetTitle.Text = "Exchange rates for " + curr.name;
                if (curr.id == Settings.CurrencyOneID)
                {
                    exchangeWidgetOneName.Text = curr.name; exchangeWidgetOneBuy.Text = (Device.OS == TargetPlatform.iOS) ? curr.buy.ToString().Substring(0, 5) : curr.buy.ToString(); exchangeWidgetOneSell.Text = (Device.OS == TargetPlatform.iOS) ? curr.sell.ToString().Substring(0, 5) : curr.sell.ToString(); exchangeWidgetOneImage.Source = curr.image;
                }
                if (curr.id == Settings.CurrencyTwoID)
                {
                    exchangeWidgetTwoName.Text = curr.name; exchangeWidgetTwoBuy.Text = (Device.OS == TargetPlatform.iOS) ? curr.buy.ToString().Substring(0, 5) : curr.buy.ToString(); exchangeWidgetTwoSell.Text = (Device.OS == TargetPlatform.iOS) ? curr.sell.ToString().Substring(0, 5) : curr.sell.ToString(); exchangeWidgetTwoImage.Source = curr.image;
                }
                if (curr.id == Settings.CurrencyThreeID)
                {
                    exchangeWidgetThreeName.Text = curr.name; exchangeWidgetThreeBuy.Text = (Device.OS == TargetPlatform.iOS) ? curr.buy.ToString().Substring(0, 5) : curr.buy.ToString(); exchangeWidgetThreeSell.Text = (Device.OS == TargetPlatform.iOS) ? curr.sell.ToString().Substring(0, 5) : curr.sell.ToString(); exchangeWidgetThreeImage.Source = curr.image;
                }
            }
        }
        async void setATM()
        {
            iATM atm;
            while (true)
            {
                await Task.Delay(1000);
                atm = IDistance.Shortest();
                if (atm.name != null)
                {
                    atmWidgetName.Text = atm.name;
                    atmWidgetAddress.Text = atm.address;
                    atmWidgetDistance.Text = "Distance: " + atm.distance + " m";
                }
            }
        }
        public async void onAccountTapped(object sender, EventArgs e)
        {
            Page loading = new mBanking.LoadingPage() { Title = accountWidgetName.Text };
            await Navigation.PushAsync(loading);
            await Navigation.PushAsync(new mBanking.DetailsPage() { Title = accountWidgetName.Text });
            Navigation.RemovePage(loading);
        }
        public async void onAccountSettingsTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new mBanking.AccountsPage() { Title = "Accounts" });
        }
        public async void onExchangeSettingsTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new mBanking.ExchangesPage() { Title = "Exchanges" });
        }
        public async void onATMTapped(object sender, EventArgs e)
        {
            if (Device.OS == TargetPlatform.Windows && Device.Idiom == TargetIdiom.Phone)
                await Navigation.PushModalAsync(new mBanking.MapPage((IDistance.Shortest().name != null) ? IDistance.Shortest() : null) { Title = "ATM Map" });
            else
                await Navigation.PushAsync(new mBanking.MapPage((IDistance.Shortest().name != null) ? IDistance.Shortest() : null) { Title = "ATM Map" });
        }
    }
}

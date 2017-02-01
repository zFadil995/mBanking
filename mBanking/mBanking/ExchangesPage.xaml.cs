using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace mBanking
{
    public partial class ExchangesPage : ContentPage
    {
        public ExchangesPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            setup();
            base.OnAppearing();
        }
        private void setup()
        {
            foreach (Currency curr in DownloadedData.currencies)
            {
                if (curr.id == Settings.CurrencySourceID) { currencySourceImage.Source = curr.image; currencySourceName.Text = curr.name; }
                if (curr.id == Settings.CurrencyOneID) { currencyOneImage.Source = curr.image; currencyOneName.Text = curr.name; }
                if (curr.id == Settings.CurrencyTwoID) { currencyTwoImage.Source = curr.image; currencyTwoName.Text = curr.name; }
                if (curr.id == Settings.CurrencyThreeID) { currencyThreeImage.Source = curr.image; currencyThreeName.Text = curr.name; }
            }
        }
        public async void onSourceTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new mBanking.CurrencyPickerPage(0) { Title = "Source Currency" });
        }
        public async void onCurrencyOneTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new mBanking.CurrencyPickerPage(1) { Title = "Exchange One Currency" });
        }
        public async void onCurrencyTwoTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new mBanking.CurrencyPickerPage(2) { Title = "Exchange Two Currency" });
        }
        public async void onCurrencyThreeTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new mBanking.CurrencyPickerPage(3) { Title = "Exchange Three Currency" });
        }
    }
}

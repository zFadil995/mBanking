using FFImageLoading.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace mBanking
{
    public partial class CurrencyPickerPage : ContentPage
    {
        private List<Currency> localCurrencies;
        private int currency;
        public CurrencyPickerPage(int currency)
        {
            this.currency = currency;
            InitializeComponent();
            setupLocal();
            setupList();
        }
        private void setupLocal()
        {
            localCurrencies = new List<Currency>();
            foreach (Currency curr in DownloadedData.currencies)
            {
                if (currency == 0)
                    localCurrencies.Add(curr);
                else if (currency == 1)
                {
                    if (curr.id != Settings.CurrencySourceID)
                        localCurrencies.Add(curr);
                }
                else if (currency == 2)
                {
                    if (curr.id != Settings.CurrencySourceID && curr.id != Settings.CurrencyOneID)
                        localCurrencies.Add(curr);
                }
                else if (currency == 3)
                {
                    if (curr.id != Settings.CurrencySourceID && curr.id != Settings.CurrencyOneID && curr.id != Settings.CurrencyTwoID)
                        localCurrencies.Add(curr);
                }

            }
        }
        private void setupList()
        {
            TapGestureRecognizer tapped = new TapGestureRecognizer();
            tapped.Tapped += currencyTapped;

            foreach (Currency curr in localCurrencies)
            {
                StackLayout currencyLine = new StackLayout() { Orientation = StackOrientation.Horizontal, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.FillAndExpand, BackgroundColor = Xamarin.Forms.Color.FromHex("#ECEFF1"), Padding = new Thickness(7, 7, 7, 7) };
                currencyLine.GestureRecognizers.Add(tapped);
                CachedImage currencyImage = new CachedImage() { Source = curr.image, HeightRequest = 25, WidthRequest = 35, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.Start };
                Label currencyName = new Label() { Text = curr.name, TextColor = Xamarin.Forms.Color.FromHex("#424242"), FontSize = 20, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.StartAndExpand };
                currencyLine.Children.Add(currencyImage);
                currencyLine.Children.Add(currencyName);
                currencyLayout.Children.Add(currencyLine);
            }
        }
        private void currencyTapped(object sender, EventArgs e)
        {
            string chosen = ((Label)((StackLayout)sender).Children[1]).Text;
            if (currency == 0)
            {
                foreach (Currency curr in DownloadedData.currencies)
                    if (chosen == curr.name)
                        Settings.CurrencySourceID = curr.id;
            }
            else if (currency == 1)
            {
                foreach (Currency curr in DownloadedData.currencies)
                    if (chosen == curr.name)
                        Settings.CurrencyOneID = curr.id;
            }
            else if (currency == 2)
            {
                foreach (Currency curr in DownloadedData.currencies)
                    if (chosen == curr.name)
                        Settings.CurrencyTwoID = curr.id;
            }
            else if (currency == 3)
            {
                foreach (Currency curr in DownloadedData.currencies)
                    if (chosen == curr.name)
                        Settings.CurrencyThreeID = curr.id;
            }
            removeDoubles(currency);
            Navigation.PopAsync();
        }
        private void removeDoubles(int remove)
        {
            if(remove == 0)
            {
                if (Settings.CurrencySourceID == Settings.CurrencyOneID)
                    foreach (Currency curr in DownloadedData.currencies)
                        if (curr.id != Settings.CurrencySourceID)
                        {
                            Settings.CurrencyOneID = curr.id;
                            break;
                        }
                removeDoubles(1);
            }
            if(remove == 1)
            {
                if (Settings.CurrencySourceID == Settings.CurrencyTwoID || Settings.CurrencyOneID == Settings.CurrencyTwoID)
                    foreach (Currency curr in DownloadedData.currencies)
                        if (curr.id != Settings.CurrencySourceID && curr.id != Settings.CurrencyOneID)
                        {
                            Settings.CurrencyTwoID = curr.id;
                            break;
                        }
                removeDoubles(2);
            }
            if (remove == 2)
            {
                if (Settings.CurrencySourceID == Settings.CurrencyThreeID || Settings.CurrencyOneID == Settings.CurrencyThreeID || Settings.CurrencyTwoID == Settings.CurrencyThreeID)
                    foreach (Currency curr in DownloadedData.currencies)
                        if (curr.id != Settings.CurrencySourceID && curr.id != Settings.CurrencyOneID && curr.id != Settings.CurrencyTwoID)
                        {
                            Settings.CurrencyThreeID = curr.id;
                            break;
                        }
            }
        }
    }
}

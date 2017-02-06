using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace mBanking
{
    public partial class DetailTabPage : ContentPage
    {
        private double scrollY, translationY; private bool down = true;
        public DetailTabPage()
        {
            InitializeComponent();
            detailsTabScroll.Scrolled += detailsTabScrolled;
            scrollY = detailsTabScroll.ScrollY;
            translationY = detailsToolbar.TranslationY;
        }

        private void detailsTabScrolled(object sender, ScrolledEventArgs e)
        {
            if (e.ScrollY >= 0 && e.ScrollY < detailsInfoLayout.Height - detailsTabScroll.Height)
            {
                if (e.ScrollY <= detailsToolbar.Height)
                    moveDOWN();
                else if (e.ScrollY > scrollY + 50)
                {
                    moveUP();
                    scrollY = e.ScrollY;
                }
                else if (e.ScrollY < scrollY - 50)
                {
                    moveDOWN();
                    scrollY = e.ScrollY;
                }
            }
        }
        private async void moveUP()
        {
            if (down && Device.OS != TargetPlatform.Windows)
            {
                await detailsToolbar.TranslateTo(detailsToolbar.TranslationX, translationY - detailsToolbar.Height, 100);
                await Task.Delay(100);
                detailsToolbar.IsVisible = false;
                down = false;
            }
        }
        private async void moveDOWN()
        {
            if (!down && Device.OS != TargetPlatform.Windows)
            {
                detailsToolbar.IsVisible = true;
                await detailsToolbar.TranslateTo(detailsToolbar.TranslationX, translationY, 100);
                down = true;
            }
        }
        protected override void OnAppearing()
        {
            setAccounts();
            base.OnAppearing();
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
                    if (icons.Contains(acc.image)) accountImage.Source = acc.image; else accountImage.Source = "accounticon.png";
                    ((DetailsPage)this.Parent).Title = acc.name;
                }
            }
        }

        async void onUpdateAccountSettingsTapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new mBanking.UpdateDetailsPage());
        }
    }
}

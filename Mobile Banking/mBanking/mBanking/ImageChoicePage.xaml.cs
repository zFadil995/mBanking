using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace mBanking
{
    public partial class ImageChoicePage : ContentPage
    {
        public ImageChoicePage()
        {
            InitializeComponent();
        }

        public void onMaleImageTapped(object sender, EventArgs e)
        {
            UpdateDetailsPage.imagePath = "male.png";
            Navigation.PopModalAsync();
        }
        public void onFemaleImageTapped(object sender, EventArgs e)
        {
            UpdateDetailsPage.imagePath = "female.png";
            Navigation.PopModalAsync();
        }
        public void onBankerImageTapped(object sender, EventArgs e)
        {
            UpdateDetailsPage.imagePath = "banker.png";
            Navigation.PopModalAsync();
        }
        public void onSaverImageTapped(object sender, EventArgs e)
        {
            UpdateDetailsPage.imagePath = "saver.png";
            Navigation.PopModalAsync();
        }
    }
}

using Newtonsoft.Json;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace mBanking
{
    public partial class UpdateDetailsPage : ContentPage
    {
        public static string imagePath, name; string originalPath;
        public UpdateDetailsPage()
        {
            InitializeComponent();
            double height = this.Height, width = this.Width;
            imagePath = ""; name = ""; originalPath = "";
            setAccounts();
            selectImageButton.Clicked += (object sender, EventArgs e) =>
            {
                Navigation.PushModalAsync(new mBanking.ImageChoicePage());
            };
            confirmButton.Clicked += (object sender, EventArgs e) =>
            {
                if (nicknameEntry.Text != null)
                    name = nicknameEntry.Text;
                confirmButton.IsEnabled = false;
                updateAccount(name, imagePath);
            };
            cancelButton.Clicked += (object sender, EventArgs e) =>
            {
                Navigation.PopModalAsync();
            };
            nicknameEntry.Focused += (object sender, FocusEventArgs e) =>
            {
                if (Device.Idiom == TargetIdiom.Phone)
                    if (e.IsFocused == true)
                        settingsView.VerticalOptions = LayoutOptions.StartAndExpand;
                    else
                        settingsView.VerticalOptions = LayoutOptions.CenterAndExpand;
                
            };
            nicknameEntry.Unfocused += (object sender, FocusEventArgs e) =>
            {
                if (Device.Idiom == TargetIdiom.Phone)
                    if (e.IsFocused == false)
                        settingsView.VerticalOptions = LayoutOptions.CenterAndExpand;
            };
        }

        private void setAccounts()
        {
            string[] icons = new string[] { "male.png", "female.png", "banker.png", "saver.png" };
            foreach (Account acc in DownloadedData.accounts)
                if (acc.id == Settings.AccountID)
                {
                    nicknameEntry.Placeholder = acc.name; if (originalPath == "") originalPath = acc.image; if(imagePath == "") imagePath = acc.image; name = acc.name; if (icons.Contains(imagePath)) accountImage.Source = imagePath;
                }
        }
        protected override void OnAppearing()
        {
            setAccounts();
            base.OnAppearing();
        }

        private async void updateAccount(string name, string path)
        {
            try
            {

                if (nicknameEntry.Text != null || imagePath != originalPath)
                {
                    var client = new RestClient("http://ubuntucodenest.cloudapp.net/banking/account.php");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddParameter("id", Settings.AccountID, ParameterType.GetOrPost);
                    request.AddParameter("name", name, ParameterType.GetOrPost);
                    request.AddParameter("image", path, ParameterType.GetOrPost);
                    IRestResponse response = await client.Execute(request);
                    Account updatedAccount = JsonConvert.DeserializeObject<List<Account>>(response.Content)[0];
                    for (int i = 0; i < DownloadedData.accounts.Count; i++)
                        if (DownloadedData.accounts[i].id == updatedAccount.id)
                            DownloadedData.accounts[i] = updatedAccount;
                }
                confirmButton.IsEnabled = true;
                await Navigation.PopModalAsync();
            }
            catch (Exception e)
            {
                await Navigation.PopModalAsync();
            }
        }
    }
}

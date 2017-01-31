using Newtonsoft.Json;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace mBanking
{
    public partial class TransactionTabPage : ContentPage
    {
        int nextPage;
        List<Transaction> transactions;
        ObservableCollection<TransactionGroup> transactionGroups;
        public TransactionTabPage()
        {
            nextPage = 64;
            InitializeComponent();
            transactions = new List<Transaction>();
            transactionGroups = new ObservableCollection<TransactionGroup>();
            if (Device.OS == TargetPlatform.Android || Device.OS== TargetPlatform.iOS)
                transactionsList.HasUnevenRows = true;
            getTransactions(Settings.AccountID, nextPage);
            loadMoreButton.Clicked += (object sender, EventArgs e) =>
            {
                loadMoreButton.IsEnabled = false;
                loadMoreButton.Text = "Loading";
                getTransactions(Settings.AccountID, nextPage);
            };
            transactionsList.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                transactionsList.SelectedItem = null;
            };
        }
        
        private void setup()
        {
            foreach (Transaction tr in transactions)
            {
                bool added = false;
                foreach (TransactionGroup trG in transactionGroups)
                    if (trG.type == tr.year)
                    { trG.Add(tr); added = true; }
                if (!added)
                    transactionGroups.Add(new TransactionGroup(tr) { tr });
            }
            transactionsList.ItemsSource = transactionGroups;
        }
        async void getTransactions(int ID, int PAGE)
        {
            try
            {
                var client = new RestClient("http://ubuntucodenest.cloudapp.net/banking/transaction.php");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("id", Settings.AccountID, ParameterType.GetOrPost);
                request.AddParameter("page", nextPage, ParameterType.GetOrPost);
                IRestResponse response = await client.Execute(request);
                string data = response.Content;
                transactions = JsonConvert.DeserializeObject<List<Transaction>>(data);
                loadMoreButton.Text = "Load More Items";
                loadMoreButton.IsEnabled = true;
                nextPage++;
                setup();
            }
            catch (Exception e) { }
        }
        class TransactionGroup : ObservableCollection<Transaction>
        {
            public TransactionGroup(Transaction tr)
            {
                type = tr.year;
                longType = type;
            }
            public string type
            {
                get; private set;
            }
            public string longType
            {
                get; private set;
            }
        }
    }
}

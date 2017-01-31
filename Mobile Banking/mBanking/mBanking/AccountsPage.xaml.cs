using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace mBanking
{
    public partial class AccountsPage : ContentPage
    {
        ObservableCollection<AccountGroup> accountGroups;
        public AccountsPage()
        {
            setup();
            InitializeComponent();
            accountsList.ItemsSource = accountGroups;
            accountsList.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                Settings.AccountID = ((Account)e.SelectedItem).id;
                Navigation.PopAsync();
            };
        }
        private void setup()
        {
            accountGroups = new ObservableCollection<AccountGroup>();
            foreach (Account acc in DownloadedData.accounts)
            {
                bool added = false;
                foreach (AccountGroup accGroup in accountGroups)
                    if (accGroup.type == acc.type) { accGroup.Add(acc); added = true; }
                if (!added)
                    accountGroups.Add(new AccountGroup(acc) { acc });
            }
        }
        class AccountGroup : ObservableCollection<Account>
        {
            public AccountGroup(Account acc)
            {
                type = acc.type;
                longType = "Account Type ";
                switch (type)
                {
                    case 1:
                        longType += "One";
                        break;
                    case 2:
                        longType += "Two";
                        break;
                    case 3:
                        longType += "Three";
                        break;
                }
            }
            public int type
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

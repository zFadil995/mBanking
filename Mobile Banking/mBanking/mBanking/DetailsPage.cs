using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace mBanking
{
    class DetailsPage : TabbedPage
    {
        public DetailsPage()
        {
            Page detailPage;
            Page transactionPage;
            if (Device.OS == TargetPlatform.iOS)
            {
                detailPage = new mBanking.DetailTabPage() { Title = "Details", Icon = "info.png" };
                transactionPage = new mBanking.TransactionTabPage() { Title = "Transactions", Icon = "transactions.png" };
            }
            else
            {
                detailPage = new mBanking.DetailTabPage() { Title = "Details" };
                transactionPage = new mBanking.TransactionTabPage() { Title = "Transactions" };
            }
            Children.Add(detailPage);
            Children.Add(transactionPage);
        }
    }
}

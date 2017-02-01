using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace mBanking
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            string[] items = new string[]
            {
                Settings.WidgetOne, 
                Settings.WidgetTwo,
                Settings.WidgetThree 
            };
            widgetsList.ItemsSource = items;

            widgetsList.items = new List<string>();
            foreach (string item in items)
                widgetsList.items.Add(item);
        }
    }
}

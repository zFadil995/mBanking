using mBanking;
using mBanking.UWP;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(mListView), typeof(mListViewRenderer))]
namespace mBanking.UWP
{
    public class mListViewRenderer : ListViewRenderer
    {
        mListView formsList;
        ListView nativeList;
        ObservableCollection<string> widgets;
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                nativeList = null;
                widgets = null;
            }

            if (e.NewElement != null)
            {
                widgets = new ObservableCollection<string>();
                formsList = e.NewElement as mListView;
                nativeList = Control as ListView;

                foreach (string widget in formsList.items)
                    widgets.Add(widget);

                nativeList.ItemsSource = widgets;

                nativeList.CanReorderItems = true;
                nativeList.CanDragItems = true;
                nativeList.AllowDrop = true;
                


                widgets.CollectionChanged += widgetsChanged;
            }
        }

        private void widgetsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (((ObservableCollection<string>)sender).Count == formsList.items.Count)
                UpdateSettings();
        }

        private void UpdateSettings()
        {
            Settings.WidgetOne = widgets[0];
            Settings.WidgetTwo = widgets[1];
            Settings.WidgetThree = widgets[2];
        }
    }
}

using System;
using Android.App;
using Android.Views;
using Android.Widget;
using mBanking;
using mBanking.Droid;
using Xamarin.Forms.Platform.Android;
using System.Collections.Generic;
using Android.Content;

[assembly: Xamarin.Forms.ExportRenderer(typeof(mListView), typeof(mListViewRenderer))]
namespace mBanking.Droid
{
    public class mListViewRenderer : ListViewRenderer
    {
        Activity activity;
        mListView formsList;
        ListView nativeList;
        mWidgetAdapter adapter;
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                nativeList = null;
            }

            if (e.NewElement != null)
            {
                activity = this.Context as Activity;
                formsList = e.NewElement as mListView;
                nativeList = Control as ListView;

                adapter = new mWidgetAdapter(activity, formsList.items, nativeList);
                
                nativeList.Adapter = adapter;    
            }
        }
    }

    class mWidgetAdapter : BaseAdapter<string>, View.IOnDragListener
    {
        ListView nativeList;
        List<string> widgets;
        private Context context;
        public mWidgetAdapter(Context context, List<string> widgets, ListView nativeList)
        {
            this.context = context; this.widgets = widgets; this.nativeList = nativeList;
        }
        public override string this[int position]
        {
            get
            {
                return widgets[position];
            }
        }

        public override int Count
        {
            get
            {
                return widgets.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        public int GetPosition(string title)
        {
            return widgets.FindIndex(item => item == title);
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if(row == null)
            {
                row = LayoutInflater.From(context).Inflate(Resource.Layout.WidgetsRow, null, false);
            }

            Switch widgetSwitch = row.FindViewById<Switch>(Resource.Id.widgetSwitch);
            TextView widgetName = row.FindViewById<TextView>(Resource.Id.widgetName);

            widgetName.Text = widgets[position];
            widgetSwitch.Checked = getState(widgetName.Text);

            widgetSwitch.CheckedChange += widgetChanged;

            row.LongClick += rowLongClick;
            row.SetOnDragListener(this);

            return row;
        }
        View oldView; float oldY = -1;
        public bool OnDrag(View v, DragEvent e)
        {
            switch (e.Action)
            {
                case DragAction.Started:
                    break;
                case DragAction.Entered:
                    if (oldY == -1) { oldY = v.GetY(); oldView = v; v.Visibility = ViewStates.Invisible; }
                    if (v.GetY() != oldY)
                    {
                        switchViews(v, oldView);
                        oldY = v.GetY();
                    }
                    break;
                case DragAction.Location:
                    break;
                case DragAction.Ended:

                    this.NotifyDataSetChanged();
                    v.Visibility = ViewStates.Visible;
                    UpdateSettings();
                    oldY = -1;
                    break;
            }
            return true;
        }

        private void UpdateSettings()
        {
            Settings.WidgetOne = widgets[0];
            Settings.WidgetTwo = widgets[1];
            Settings.WidgetThree = widgets[2];
        }

        private void switchViews(View one, View two)
        {
            var oneTop = (int)(one.Top);
            var oneBottom = (int)(one.Bottom);
            var twoTop = (int)(two.Top);
            var twoBottom = (int)(two.Bottom);
            two.Layout(two.Left, oneTop, two.Right, oneBottom);
            one.Layout(one.Left, twoTop, one.Right, twoBottom);

            int onePosition = this.GetPosition(((TextView)((LinearLayout)((LinearLayout)one).GetChildAt(0)).GetChildAt(1)).Text);
            int twoPosition = this.GetPosition(((TextView)((LinearLayout)((LinearLayout)two).GetChildAt(0)).GetChildAt(1)).Text);

            string temp = widgets[onePosition];
            widgets[onePosition] = widgets[twoPosition];
            widgets[twoPosition] = temp;
        }

#pragma warning disable CS0618
        private void rowLongClick(object sender, View.LongClickEventArgs e)
        {
            ClipData data = ClipData.NewPlainText("", "");
            View.DragShadowBuilder shadowBuilder = new View.DragShadowBuilder(((View)sender));
            ((View)sender).StartDrag(data, shadowBuilder, (View)sender, 0);
        }
#pragma warning restore CS0618

        private void widgetChanged(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            string switched = ((TextView)((LinearLayout)((Switch)sender).Parent).GetChildAt(1)).Text;
            if (switched == "Account Widget")
                mBanking.Settings.AccountWidgetVisible = e.IsChecked;
            if (switched == "Exchange Widget")
                mBanking.Settings.ExchangeWidgetVisible = e.IsChecked;
            if (switched == "ATM Widget")
                mBanking.Settings.ATMWidgetVisible = e.IsChecked;
        }

        private bool getState(string widgetName)
        {
            if (widgetName == "Account Widget")
                return mBanking.Settings.AccountWidgetVisible;
            if (widgetName == "Exchange Widget")
                return mBanking.Settings.ExchangeWidgetVisible;
            if (widgetName == "ATM Widget")
                return mBanking.Settings.ATMWidgetVisible;
            return true;
        }

    }
}
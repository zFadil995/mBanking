using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace mBanking
{
    public partial class MapPage : ContentPage
    {
        private Uri url;
        List<Pin> allPins;
        public MapPage(iATM i_ATM = null)
        {
            setupPins();
            Map map = new Map(){
                HasZoomEnabled = true,
                IsShowingUser = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            map.MoveToRegion((MapSpan.FromCenterAndRadius((IDistance.position != null) ? new Position(IDistance.position.Latitude, IDistance.position.Longitude) : new Position(44.536049, 18.679751), Distance.FromKilometers(1))));
            InitializeComponent();
            if (i_ATM != null)
            {
                atmName.Text = i_ATM.name;
                atmAddress.Text = i_ATM.address;
                setURL(i_ATM.name, new Position(i_ATM.latitude, i_ATM.longtitude));
            }
            else
                atmInfo.IsVisible = false;
            if (Device.OS == TargetPlatform.Android)
                mapLayout.Children.Remove(atmInfo);
            mapLayout.Children.Add(map);
            setPins(allPins, map);
            //map.PropertyChanging += mapChanging;
        }

        private void setupPins()
        {
            Pin pin; allPins = new List<Pin>();
            foreach (ATM atm in IDistance.atms)
            {
                pin = new Pin { Type = PinType.Place, Position = new Position(atm.gpsy, atm.gpsx), Label = atm.name, Address = atm.address };
                pin.Clicked += pinClicked;
                allPins.Add(pin);
            }
        }

        private void mapChanging(object sender, PropertyChangingEventArgs e)
        {
            Map map = (Map)sender;
            if (sender != null)
            {
                if (map.VisibleRegion != null)
                {
                    double distance = map.VisibleRegion.Radius.Meters;
                    Position position = map.VisibleRegion.Center;
                    if (map.VisibleRegion.LatitudeDegrees * 200000 > 50000)
                        if (map.Pins.Count != 0)
                        {
                            map.Pins.Clear();
                            map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(distance)));
                        }
                    if (map.VisibleRegion.LatitudeDegrees * 200000 < 50000)
                        if (map.Pins.Count == 0)
                            setPins(allPins, map);
                }
            }
        }
        private void setPins(List<Pin> pins, Map map)
        {
            foreach (Pin pin in pins)
                map.Pins.Add(pin);
        }
        private void pinClicked(object sender, EventArgs e)
        {
            atmInfo.IsVisible = true;
            atmName.Text = ((Pin)sender).Label;
            atmAddress.Text = ((Pin)sender).Address;
            setURL(((Pin)sender).Label,((Pin)sender).Position);
        }
        public void onNavigationTapped(object sender, EventArgs e)
        {
            this.Title = atmName.Text;
            if (url != null)
                Device.OpenUri(url);
        }
        public void setURL(string name, Position position)
        {
            Device.OnPlatform(
                iOS: () =>
                {
                    url = new Uri(String.Format("http://maps.apple.com/maps?daddr={0} {1}&t=h", position.Latitude.ToString().Replace(',', '.'), position.Longitude.ToString().Replace(',', '.')));
                },
                Android: () =>
                {
                    url = new Uri(String.Format("google.navigation:q={0} {1}&mode=w", position.Latitude.ToString().Replace(',', '.'), position.Longitude.ToString().Replace(',', '.')));
                },
                WinPhone: () =>
                {
                    url = new Uri(String.Format("ms-walk-to:?destination.latitude={0}&destination.longitude={1}&destination.name={2}", position.Latitude, position.Longitude, name));
                });
        }
    }
}

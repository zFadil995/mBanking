using Newtonsoft.Json;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mBanking
{
    public static class IDistance
    {
        public static IGeolocator locator { get; set; }
        public static Position position { get; set; }
        public static List<ATM> atms;
        public static void Init()
        {
            var assembly = typeof(IDistance).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("mBanking.atms.txt");
            string atmData = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                atmData = reader.ReadToEnd();
            }
            atms = JsonConvert.DeserializeObject<List<ATM>>(atmData);
            locator = CrossGeolocator.Current; locator.DesiredAccuracy = 50;
            locator.StartListeningAsync(1000, 1);
            locator.PositionChanged += locator_PositionChanged;
        }

        private static void locator_PositionChanged(object sender, PositionEventArgs e)
        {
            position = e.Position;
        }
        public static iATM Shortest()
        {
            iATM closestATM = new iATM();
            if (position != null)
            {
                int distance, temp; 
                distance = Between(position, atms[0].gpsy, atms[0].gpsx);
                closestATM = new iATM(atms[0], distance);
                foreach (ATM atm in atms)
                {
                    temp = Between(position, atm.gpsy, atm.gpsx);
                    if(temp < distance)
                    {
                        distance = temp;
                        closestATM = new iATM(atm, distance);
                    }
                }
            }
            return closestATM;
        }
        public static int Between(Position position, double ToLatidude, double ToLongtitude)
        {
            double theta = position.Longitude - ToLongtitude;
            double dist = Math.Sin(deg2rad(position.Latitude)) * Math.Sin(deg2rad(ToLatidude)) + Math.Cos(deg2rad(position.Latitude)) * Math.Cos(deg2rad(ToLatidude)) * Math.Cos(deg2rad(theta));
            dist = rad2deg(Math.Acos(dist)) * 60 * 1.1515 * 1609.344;
            return (int) dist;
        }
        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }
        private static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}

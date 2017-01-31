using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mBanking
{
    public class ATM
    {
        public ATM()
        {

        }
        public ATM(string name, string address, double gpsx, double gpsy)
        {
            this.name = name; this.address = address; this.gpsx = gpsx; this.gpsy = gpsy;
        }
        public string name { get; set; }
        public string address { get; set; }
        public double gpsx { get; set; }
        public double gpsy { get; set; }
    }
    public class iATM
    {
        public iATM()
        {

        }
        public iATM(ATM atm, int distance)
        {
            this.name = atm.name; this.address = atm.address; this.distance = distance; this.latitude = atm.gpsy; this.longtitude = atm.gpsx;
        }
        public string name { get; set; }
        public string address { get; set; }
        public int distance { get; set; }
        public double latitude { get; set; }
        public double longtitude { get; set; }
    }
}

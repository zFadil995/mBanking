using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mBanking
{
    public class Currency
    {
        public Currency()
        {
            id = 0; name = ""; buy = 0; sell = 0; image = "";
        }
        public Currency(int id, string name, double buy, double sell, string image)
        {
            this.id = id; this.name = name; this.buy = buy; this.sell = sell; this.image = image;
        }
        public int id { get; set; }
        public string name { get; set; }
        public double buy { get; set; }
        public double sell { get; set; }
        public string image { get; set; }
    }
}

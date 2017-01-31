using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mBanking
{
    public class Account
    {
        public Account()
        {
            this.id = 0; this.name = ""; this.amount = 0; this.image = ""; this.type = 0; this.number = "";
        }
        public Account(int id, string name, double amount, string image, int type, string number)
        {
            this.id = id; this.name = name; this.amount = amount; this.image = image; this.type = type; this.number = number;
        }
        public int id { get; set; }
        public string name { get; set; }
        public double amount { get; set; }
        public string image { get; set; }
        public int type { get; set; }
        public string number { get; set; }
    }
}

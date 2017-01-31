using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mBanking
{
    public class Transaction
    {
        public int id { get; set; }
        public int type_id { get { return 0; } set { switch (value) { case (1): type_image = "card.png"; type_name = "Income"; type_color = "income.png"; break; case (2): type_image = "exchange.png"; type_name = "Expense"; type_color = "expense.png"; break; case (3): type_image = "card.png"; type_name = "Expense"; type_color = "expense.png"; break; case (4): type_image = "pos.png"; type_name = "Expense"; type_color = "expense.png"; break; case (5): type_image = "atm.png"; type_name = "Expense"; type_color = "expense.png"; break; case (6): type_image = "etc.png"; type_name = "Expense"; type_color = "expense.png"; break; } } }
        public int account_id { get; set; }
        public int amount { get { return 0; } set { amount_localized = value.ToString("C2", System.Globalization.CultureInfo.DefaultThreadCurrentCulture); } }
        public string date { get { return day + " " + month + " " + year; } set { DateTime transactionDate; DateTime.TryParseExact(value, "yyyy-MM-dd", null, DateTimeStyles.None, out transactionDate); year = transactionDate.ToString("yyyy"); month = transactionDate.ToString("MMM").ToUpper(); day = transactionDate.ToString("dd"); } }
        public string description { get; set; }
        public string type_image { get; set; }
        public string type_name { get; set; }
        public string amount_localized { get; set; }
        public string type_color { get; set; }
        public string year { get; set; }
        public string month { get; set; }
        public string day { get; set; }
        public Transaction() { }
        public Transaction(int id, int type_id, int account_id, int amount, string date, string description)
        {
            this.id = id; this.type_id = type_id; this.account_id = account_id; this.amount = amount; this.date = date; this.description = description;
        }
    }
}

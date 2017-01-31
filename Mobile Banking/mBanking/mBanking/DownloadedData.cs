using Newtonsoft.Json;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mBanking
{
    public static class DownloadedData
    {
        public static List<Account> accounts;
        public static List<Currency> currencies;

        public static async void getAccounts()
        {
            
            try
            {
                var client = new RestClient("http://ubuntucodenest.cloudapp.net/banking/account.php");
                var request = new RestRequest(Method.GET);
                request.AddHeader("postman-token", "91fc7944-50ea-89e7-43f9-8ab5236e7e36");
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "multipart/form-data; boundary=---011000010111000001101001");
                IRestResponse response = await client.Execute(request);
                string data = response.Content;
                accounts = JsonConvert.DeserializeObject<List<Account>>(data);
            }
            catch (Exception e) { }
        }
        public static async void getCurrencies()
        {
            try
            {
                var client = new RestClient("http://ubuntucodenest.cloudapp.net/banking/currency.php");
                var request = new RestRequest(Method.GET);
                request.AddHeader("postman-token", "3152e67c-39fc-c6a2-c8e3-81a6ac3ddfc2");
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "multipart/form-data; boundary=---011000010111000001101001");
                IRestResponse response = await client.Execute(request);
                string data = response.Content;
                currencies = JsonConvert.DeserializeObject<List<Currency>>(data);
            }
            catch (Exception e) { }
        }
    }
}

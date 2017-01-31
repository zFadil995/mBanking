using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mBanking
{
    public static class Settings
    {
        private const string accountID = "accountID";
        private static readonly int accountDefaultID = 1;

        private const string currencySourceID = "currencySourceID";
        private static readonly int currencySourceDefaultID = 5;

        private const string currencyOneID = "currencyOneID";
        private static readonly int currencyOneDefaultID = 3;

        private const string currencyTwoID = "currencyTwoID";
        private static readonly int currencyTwoDefaultID = 4;

        private const string currencyThreeID = "currencyThreeID";
        private static readonly int currencyThreeDefaultID = 7;

        private const string accountWidgetVisible = "accountWidgetVisible";
        private static readonly bool accountWidgetDefaultVisible = true;

        private const string exchangeWidgetVisible = "exchangeWidgetVisible";
        private static readonly bool exchangeWidgetDefaultVisible = true;

        private const string atmWidgetVisible = "atmWidgetVisible";
        private static readonly bool atmWidgetDefaultVisible = true;

        private const string widgetOne = "widgetOne";
        private static readonly string widgetOneDefault = "Account Widget";

        private const string widgetTwo = "widgetTwo";
        private static readonly string widgetTwoDefault = "Exchange Widget";

        private const string widgetThree = "widgetThree";
        private static readonly string widgetThreeDefault = "ATM Widget";



        public static int AccountID
        {
            get { return AppSettings.GetValueOrDefault<int>(accountID, accountDefaultID); }
            set { AppSettings.AddOrUpdateValue<int>(accountID, value); }
        }

        public static int CurrencySourceID
        {
            get { return AppSettings.GetValueOrDefault<int>(currencySourceID, currencySourceDefaultID); }
            set { AppSettings.AddOrUpdateValue<int>(currencySourceID, value); }
        }

        public static int CurrencyOneID
        {
            get { return AppSettings.GetValueOrDefault<int>(currencyOneID, currencyOneDefaultID); }
            set { AppSettings.AddOrUpdateValue<int>(currencyOneID, value); }
        }

        public static int CurrencyTwoID
        {
            get { return AppSettings.GetValueOrDefault<int>(currencyTwoID, currencyTwoDefaultID); }
            set { AppSettings.AddOrUpdateValue<int>(currencyTwoID, value); }
        }

        public static int CurrencyThreeID
        {
            get { return AppSettings.GetValueOrDefault<int>(currencyThreeID, currencyThreeDefaultID); }
            set { AppSettings.AddOrUpdateValue<int>(currencyThreeID, value); }
        }

        public static bool AccountWidgetVisible
        {
            get { return AppSettings.GetValueOrDefault<bool>(accountWidgetVisible, accountWidgetDefaultVisible); }
            set { AppSettings.AddOrUpdateValue<bool>(accountWidgetVisible, value); }
        }

        public static bool ExchangeWidgetVisible
        {
            get { return AppSettings.GetValueOrDefault<bool>(exchangeWidgetVisible, exchangeWidgetDefaultVisible); }
            set { AppSettings.AddOrUpdateValue<bool>(exchangeWidgetVisible, value); }
        }

        public static bool ATMWidgetVisible
        {
            get { return AppSettings.GetValueOrDefault<bool>(atmWidgetVisible, atmWidgetDefaultVisible); }
            set { AppSettings.AddOrUpdateValue<bool>(atmWidgetVisible, value); }
        }

        public static string WidgetOne
        {
            get { return AppSettings.GetValueOrDefault<string>(widgetOne, widgetOneDefault); }
            set { AppSettings.AddOrUpdateValue<string>(widgetOne, value); }
        }
        public static string WidgetTwo
        {
            get { return AppSettings.GetValueOrDefault<string>(widgetTwo, widgetTwoDefault); }
            set { AppSettings.AddOrUpdateValue<string>(widgetTwo, value); }
        }
        public static string WidgetThree
        {
            get { return AppSettings.GetValueOrDefault<string>(widgetThree, widgetThreeDefault); }
            set { AppSettings.AddOrUpdateValue<string>(widgetThree, value); }
        }

        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }
    }
}

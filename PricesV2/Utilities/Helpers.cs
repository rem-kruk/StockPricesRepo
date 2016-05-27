using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Collections;
using PricesV2.Models;
using Newtonsoft.Json;

namespace PricesV2.Utilities
{
    public class Helpers
    {


        internal static dynamic InvestmentIncome(DateTime dateFrom, DateTime dateTo,float amount ,float percentage)
        {
            float income = 0;
            float totalAmount = amount;
            int years = CountYears(dateFrom,dateTo);
            for (int i = 0; i < years; i++)
            {
                totalAmount += totalAmount * (percentage / 100);
            }
            income = totalAmount - amount;
            return Math.Round(income,2);
        }

        private static int CountYears(DateTime from, DateTime to)
        {
            return (to.Year - from.Year - 1) +
            ( ((to.Month > from.Month) ||
            ( (from.Month == to.Month) && (to.Day >= from.Day) ) ) ? 1 : 0);
        }

        internal static string DataChartIncome(String connectionString, DateTime dateFrom, DateTime dateTo, float amount,float percentage)
        {
            float income = 0;
            float valueTo = 0;
            float valueFrom = 0;
            bool state = true;
            ArrayList dataChart = new ArrayList();
            ArrayList header = new ArrayList { "data", "fundusz inwestycyjny", "lokata" };
            dataChart.Add(header);

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            string dateFromStr = dateFrom.ToString("yyyy/MM/dd");
            dateFromStr = dateFromStr.Replace(".", "-");
            string dateToStr = dateTo.ToString("yyyy/MM/dd");
            dateToStr = dateToStr.Replace(".", "-");
            command.CommandText = "SELECT * FROM prices WHERE date BETWEEN " + "'" + dateFromStr + "'" + " AND " + "'" + dateToStr + "'";
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (state)
                {
                    state = false;
                    valueFrom = reader.GetFloat(1);
                }
                valueTo = reader.GetFloat(1);
                dateTo = reader.GetDateTime(0);
                int numberOfActions = (int)(amount / valueFrom);
                if (valueFrom != 0) income = (numberOfActions * valueTo) - (numberOfActions * valueFrom);
                ArrayList elem = new ArrayList { reader.GetDateTime(0).ToString("yyyy/MM/dd"), Math.Round(income,2),InvestmentIncome(dateFrom,dateTo,amount,percentage)};
                dataChart.Add(elem);
            }
            reader.Close();
            connection.Close();

            string dataStr = JsonConvert.SerializeObject(dataChart, Formatting.None);
            return dataStr;
        }     
    }
}
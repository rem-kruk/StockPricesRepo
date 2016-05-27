using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using PricesV2.Models;
using PricesV2.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PricesV2.Controllers
{
    public class HomeController : Controller
    {
        string connectionString = "Database=StockPricesdb;Data Source=eu-cdbr-azure-west-d.cloudapp.net;User Id=be5feffcd3ff04;Password=73491b31";

        public ActionResult Index(string data1,string data2)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            List<StockPrice> data = new List<StockPrice>();

            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM prices WHERE date BETWEEN " + "'" + data1 + "'" + " AND " + "'" + data2 + "'";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                StockPrice one = new StockPrice { date = reader.GetDateTime(0), price = reader.GetFloat(1) };
                data.Add(one);
            }
            reader.Close();
            connection.Close();
            ArrayList dataChart = new ArrayList();
            ArrayList header = new ArrayList { "data", "wartość" };
            dataChart.Add(header);
            foreach (StockPrice s in data)
            {
                ArrayList elem = new ArrayList { s.date.ToString("yyyy/MM/dd"), s.price };
                dataChart.Add(elem);
            }
            string dataStr = JsonConvert.SerializeObject(dataChart, Formatting.None);
            ViewBag.Data = new HtmlString(dataStr);
            return View(data);
        }


        public ActionResult compare([Bind(Include = "Amount,Percentage,DateFrom,DateTo")] InvestmentAndStock @i)
        {
           ViewBag.income = new HtmlString( Helpers.DataChartIncome(connectionString, i.DateFrom, i.DateTo, i.Amount,i.Percentage) );
           return View();
        }
    }
}
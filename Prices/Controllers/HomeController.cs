using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Odbc;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using Prices.Models;

namespace Prices.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            MySqlConnection connection = new MySqlConnection("Database=StockPricesdb;Data Source=eu-cdbr-azure-west-d.cloudapp.net;User Id=be5feffcd3ff04;Password=73491b31");
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "select * from prices";
            MySqlDataReader reader = command.ExecuteReader();

            List<StockPrice> tab = new List<StockPrice>();

            while (reader.Read())
            {
                StockPrice one = new StockPrice { date = reader.GetDateTime(0), price = reader.GetFloat(1) };
                tab.Add(one);
                //reader["column_name"].ToString()        
            }
            reader.Close();


            return View(tab);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
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
using System.Web.UI.WebControls;
using Prices.Utilities;
using System.Collections;
using Newtonsoft.Json;

namespace Prices.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string drop1, string drop2)
        {
            drop1 = Helpers.PrepareString(drop1);
            drop2 = Helpers.PrepareString(drop2);
            string connectionString = "Database=StockPricesdb;Data Source=eu-cdbr-azure-west-d.cloudapp.net;User Id=be5feffcd3ff04;Password=73491b31";

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM prices";
            MySqlDataReader reader = command.ExecuteReader();

            List<StockPrice> data = new List<StockPrice>();
            List<SelectListItem> dropdown = new List<SelectListItem>();
            while (reader.Read())
            {
                StockPrice one = new StockPrice { date = reader.GetDateTime(0), price = reader.GetFloat(1) };
                dropdown.Add(new SelectListItem() { Value = one.date.ToString("yyyy/MM/dd"), Text= one.date.ToString("yyyy/MM/dd") });
            }
            reader.Close();

            ViewBag.DropDownValues = new SelectList(dropdown, "Value", "Text");
            command.CommandText = "SELECT * FROM prices WHERE date BETWEEN " + "'" + drop1 + "'" + " AND " + "'" + drop2 + "'"; 
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                StockPrice one = new StockPrice { date = reader.GetDateTime(0), price = reader.GetFloat(1) };
                data.Add(one);
            }
            reader.Close();
            connection.Close();
            ArrayList data12 = new ArrayList();
            ArrayList header = new ArrayList { "data" , "wartość" };
            data12.Add(header);
            foreach (StockPrice s in data)
            {
                ArrayList elem = new ArrayList { s.date.ToString("yyyy/MM/dd"), s.price };
                data12.Add(elem);
            }
           string dataStr = JsonConvert.SerializeObject(data12, Formatting.None);
            ViewBag.Data = new HtmlString(dataStr);
            return View(data);
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

        public ActionResult ChartArrayBasic()
        {
            return View();
        }
    }
}
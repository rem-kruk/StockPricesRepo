using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PricesV2.Utilities
{
    public class Helpers
    {
        public static string PrepareString(string s)
        {
            string buf;
            string[] a = new string[2];
            try
            {
                a = s.Split('.');
                buf = a[0] + "-" + a[1] + "-" + a[2];
            }
            catch (Exception e)
            {
                buf = "";
            }

            return buf;
        }
    }
}
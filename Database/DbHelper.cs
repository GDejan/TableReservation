using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableReservation.Classes
{
    internal static class DbHelper
    {
        /// <summary>
        /// helper for reading connection string
        /// </summary>
        /// <param name="name">name of a connections string</param>
        /// <returns>connection string</returns>
        public static string ConnectionString (string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}

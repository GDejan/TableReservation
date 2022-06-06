using System.Configuration;

namespace TableReservation.Database
{
    internal static class DbHelper
    {
        /// <summary>
        /// helper for reading connection string
        /// </summary>
        /// <param name="name">name of a connections string</param>
        /// <returns>connection string</returns>
        public static string ConnectionString ()
        {            
            return ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        }
    }
}

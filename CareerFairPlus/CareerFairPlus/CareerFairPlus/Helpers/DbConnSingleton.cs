using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace CareerFairPlus.Helpers
{
    public class DbConnSingleton
    {
        private static DbConnSingleton dbInstance;
        private static MySqlConnection sqlConnection = new MySqlConnection(@"Data Source=localhost;Database=career_fair_plus;Uid=root;Pwd=MySql@1287;");

        public static DbConnSingleton getDbInstance()
        {
            if (dbInstance == null)
            {
                dbInstance = new DbConnSingleton();
            }
            return dbInstance;
        }

        public MySqlConnection GetDBConnection()
        {
            try
            {
                sqlConnection.Dispose();
                sqlConnection.Open();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return sqlConnection;
        }
    }
}
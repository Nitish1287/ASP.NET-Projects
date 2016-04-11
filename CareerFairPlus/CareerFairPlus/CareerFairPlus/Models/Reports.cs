using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using CareerFairPlus.Helpers;

namespace CareerFairPlus.Models
{
    public class Reports
    {
        public List<Int32> getReports()
        {
            List<Int32> list = new List<Int32>();
            Int32 m = new Int32();
            Int32 n = new Int32();

            using (var cn = DbConnSingleton.getDbInstance().GetDBConnection())
            {
                MySqlCommand cmd = new MySqlCommand("reportGeneration", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("companyId", 1));
                cmd.Parameters["companyId"].Direction = ParameterDirection.Input;
                cmd.Parameters.Add(new MySqlParameter("m", m));
                cmd.Parameters["m"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add(new MySqlParameter("n", n));
                cmd.Parameters["n"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                m = (Int32) cmd.Parameters["m"].Value;
                n = (Int32)cmd.Parameters["n"].Value;
                cmd.Connection.Close();
            }
            list.Add(m);
            list.Add(n);


            return list;
        }
    }
}
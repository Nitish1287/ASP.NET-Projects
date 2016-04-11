using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CareerFairPlus.Helpers;
using MySql.Data.MySqlClient;
using CareerFairPlus.Interface;

namespace CareerFairPlus.Helpers
{
    public enum UserTypes
    {
        Student,
        Company_Rep
    }

    public class Helper : UserDetailsInterface
    {
        public int GetIDByUserName(string name, string role)
        {
            using (var cn = DbConnSingleton.getDbInstance().GetDBConnection())
            {
                var searchString = "";
                if (role.Equals(UserTypes.Student.ToString().ToLower()))
                    searchString = "Select s.id from Student s join credentials cr on s.email = cr.email AND cr.username = @u";
                else
                    if (role.Equals(UserTypes.Company_Rep.ToString().ToLower()))
                        searchString = "Select c.company_id from company_rep c join credentials cr on c.email = cr.email AND cr.username = @u";

                var cmd = new MySqlCommand(searchString, cn);
                cmd.Parameters
                    .Add(new MySqlParameter("@u", MySqlDbType.String))
                    .Value = name;
                string reader = cmd.ExecuteScalar().ToString();
                if (!reader.Equals(null))
                {
                    cmd.Dispose();
                    return Convert.ToInt32(reader.ToString());
                }
                else
                {
                    cmd.Dispose();
                    return 0;
                }
            }
        }
    }
}
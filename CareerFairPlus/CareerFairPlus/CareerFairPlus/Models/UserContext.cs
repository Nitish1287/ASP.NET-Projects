using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using CareerFairPlus.Helpers;

namespace CareerFairPlus.Models
{
    public class UserContext
    {
        /// <summary>
        /// Checks if user with given password exists in the database
        /// </summary>
        /// <param name="_username">User name</param>
        /// <param name="_password">User password</param>
        /// <returns>True if user exist and password is correct</returns>
        public static string IsValid(string _username, string _password)
        {
            DbConnSingleton db = DbConnSingleton.getDbInstance();
            using (var conn = db.GetDBConnection())
            {
                string _sql = @"SELECT role FROM credentials " +
                       @"WHERE username = @u AND password = @p";
                var cmd = new MySqlCommand(_sql, conn);
                cmd.Parameters
                    .Add(new MySqlParameter("@u", MySqlDbType.String))
                    .Value = _username;
                cmd.Parameters
                    .Add(new MySqlParameter("@p", MySqlDbType.String))
                    .Value = Helpers.LoginEncryption.Encode(_password);
                string reader = cmd.ExecuteScalar().ToString();
                if (!reader.Equals(null))
                {
                    cmd.Dispose();
                    return reader.ToString();
                }
                else
                {
                    cmd.Dispose();
                    return null;
                }
            }
        }

        /// <summary>
        /// Registers the user into the system
        /// </summary>
        /// <param name="_username"></param>
        /// <param name="_password"></param>
        /// <param name="_email"></param>
        /// <param name="_role"></param>
        /// <param name="_uin"></param>
        /// <param name="_firstname"></param>
        /// <param name="_lastname"></param>
        /// <param name="_major"></param>
        /// <param name="_degree"></param>
        /// <param name="_address"></param>
        /// <param name="_phone"></param>
        /// <returns></returns>
        public static bool RegisterUser(string _username, string _password, string _email, string _role,
                                 double _uin, string _firstname, string _lastname, string _major,
                                 string _degree, string _address, double _phone)
        {
            DbConnSingleton db = DbConnSingleton.getDbInstance();
            using (var conn = db.GetDBConnection())
            {
                string _sql = "spRegisterUser";
                var cmd = new MySqlCommand(_sql, conn);

                cmd.Parameters.AddWithValue("username", _username);
                cmd.Parameters.AddWithValue("password", Helpers.LoginEncryption.Encode(_password));
                cmd.Parameters.AddWithValue("emailid", _email);
                cmd.Parameters.AddWithValue("role", _role);
                cmd.Parameters.AddWithValue("uid", _uin);
                cmd.Parameters.AddWithValue("fname", _firstname);
                cmd.Parameters.AddWithValue("lname", _lastname);
                cmd.Parameters.AddWithValue("major", _major);
                cmd.Parameters.AddWithValue("degree", _degree);
                cmd.Parameters.AddWithValue("address", _address);
                cmd.Parameters.AddWithValue("phone", _phone);

                cmd.CommandType = CommandType.StoredProcedure;
                int reader = cmd.ExecuteNonQuery();
                if (reader != -1)
                {
                    cmd.Dispose();
                    return true;
                }
                else
                {
                    cmd.Dispose();
                    return false;
                }
            }
        }
    }
}
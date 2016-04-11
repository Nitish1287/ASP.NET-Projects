using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using CareerFairPlus.Helpers;

namespace CareerFairPlus.Models
{
    public class CompaniesList
    {
        public List<Company> GetCompanies()
        {
            List<Company> list = new List<Company>();
            Company company;
            Booth booth;

            using (var cn = DbConnSingleton.getDbInstance().GetDBConnection())
            {
                var query = "select * from company c join booth b on c.id = b.company_id";
                MySqlCommand cmd = new MySqlCommand(query, cn);
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adap.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    company = new Company();
                    booth = new Booth();
                    company.companyId = dr["id"].ToString();
                    company.companyName = dr["name"].ToString();
                    company.sector = dr["sector"].ToString();
                    company.headquarters = dr["headquarters"].ToString();
                    booth.boothID = dr["id"].ToString();
                    booth.boothNumber = dr["booth_number"].ToString();
                    booth.date = dr["day"].ToString();
                    booth.queueLength = dr["queue_length"].ToString();
                    company.boothList.Add(booth);
                    list.Add(company);
                }
            }
            return list;
        }
    }
}
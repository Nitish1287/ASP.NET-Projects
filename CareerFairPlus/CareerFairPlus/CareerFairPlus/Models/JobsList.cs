using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using CareerFairPlus.Helpers;
using CareerFairPlus.Interface;

namespace CareerFairPlus.Models
{
    public class JobsList
    {
        private List<Job> jobs;

        public List<Job> JobsInfo
        {
            get { return jobs; }
            set { jobs = value; }
        }

        public List<Job> GetJobs(string companyRepName)
        {
            List<Job> list = new List<Job>();
            Job job;
            UserDetailsInterface udInterface = new Helper();
            using (var cn = DbConnSingleton.getDbInstance().GetDBConnection())
            {
                // Need to change below query so that company_id becomes dynamic
                var query = "select * from job where company_id = @cid";
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@cid", udInterface.GetIDByUserName(companyRepName, "company_rep"));
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adap.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    job = new Job();

                    job.seeking_degree = dr["seeking_degree"].ToString();
                    job.type = dr["type"].ToString();
                    job.title = dr["title"].ToString();
                    job.visa_sponsorship = dr["visa_sponsorhip"].ToString();
                    list.Add(job);
                }
            }
            return list;
        }


        public void addJob(Job j)
        {
            using (var cn = DbConnSingleton.getDbInstance().GetDBConnection())
            {
                var query = "insert into job(company_id,seeking_degree,type,title,visa_sponsorhip) values(" + j.company_id + ",'" + j.seeking_degree + "','" + j.type + "','" + j.title + "'," + j.visa_sponsorship + ")";
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
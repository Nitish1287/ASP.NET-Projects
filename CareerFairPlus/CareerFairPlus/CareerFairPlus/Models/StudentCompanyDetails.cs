using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using CareerFairPlus.Helpers;
using MvcDemo.Models;

namespace CareerFairPlus.Models
{
    public class StudentCompanyDetails
    {
        public StudentCompanyInformation getStudentCompanyDetails(string companyid = null, int studentID = 1, string id =null)
        {
            Job job;
            StudentCompanyInformation information =new StudentCompanyInformation();
            var company_id = "";

            using (var cn = DbConnSingleton.getDbInstance().GetDBConnection())
            {
                string query = null;
                if (companyid != null)
                {
                    query = "select sc.notes,sc.interest, c.name from company c join student_company_info sc on c.id = sc.company_id join job j on c.id = j.company_id where student_id = @sid and c.id = " + companyid;
                    company_id = companyid;
                }
                else
                {
                    query = "select sc.notes,sc.interest, c.name  from company c join student_company_info sc on c.id = sc.company_id join job j on c.id = j.company_id where student_id = @sid and c.id = " + id;
                    company_id = id;
                }


                MySqlCommand cmd = new MySqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@sid", studentID);
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adap.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                   // information = new StudentCompanyInformation(); 
                   // job = new Job();
                    information.notes= dr["notes"].ToString();
                    information.isInterested = dr["interest"].ToString();
                    //job.seeking_degree = dr["seeking_degree"].ToString();
                    //job.type = dr["type"].ToString();
                    //job.visa_sponsorship = dr["visa_sponsorhip"].ToString();
                    //job.title = dr["title"].ToString();
                    information.companyName = dr["name"].ToString();
                   // information.jobs.Add(job);
                }
            }
            information.jobs = getJobs(company_id); // jobs posted by company (if any)
            return information;
        }

        public List<Job> getJobs(string id)
        {
            Job jobObj = null;
            List<Job> list = new List<Job>();
            string query = "select j.* from Job j join company c on j.company_id = c.id where c.id = " + id;

            using (var cn = DbConnSingleton.getDbInstance().GetDBConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, cn);
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adap.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    jobObj = new Job();
                    jobObj.seeking_degree = dr["seeking_degree"].ToString();
                    jobObj.type = dr["type"].ToString();
                    jobObj.title = dr["title"].ToString();
                    jobObj.visa_sponsorship = dr["visa_sponsorhip"].ToString();
                    list.Add(jobObj);
                }
                return list;
            }
        }

        public StudentCompanyInformation UpdateNotes(string id, int sid, string notes = null, string isInterested = null, bool inQueue = false)
        {
           // StudentCompanyDetails compdetails = new StudentCompanyDetails();
            StudentCompanyInformation info = new StudentCompanyInformation();

            using (var cn = DbConnSingleton.getDbInstance().GetDBConnection())
            {
                //var query = "insert into student_company_info(student_id,company_id,interest,notes) values(1,?companyID, ?isInterested, ?notes)";

                var query = "Update student_company_info set notes = @notes  where company_id = @id and student_id = @sid";
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@notes", notes);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@sid", sid);

               // cmd.Parameters.AddWithValue("?companyID", HttpUtility.ParseQueryString("id").ToString());
               // cmd.Parameters.AddWithValue("?isInterested", 1);
               // cmd.Parameters.AddWithValue("?notes", notes);
                cmd.ExecuteNonQuery();
            }

            info = getStudentCompanyDetails(id, sid);
            return info;

           
        }

        public StudentCompanyInformation AddNotes(string id, int sid, string additionalNotes = null)
        {
            StudentCompanyInformation info = new StudentCompanyInformation();
            using (var cn = DbConnSingleton.getDbInstance().GetDBConnection())
            {
                var query = "insert into student_company_info(student_id,company_id,interest,notes) values(@sid,@id, 1,@notes)";
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@notes", additionalNotes.ToString());
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@sid", sid);
                cmd.ExecuteNonQuery();
            }


            info = getStudentCompanyDetails(id, sid); 
            return info;
        }

        public StudentCompanyInformation editBoothQueue(string id, int studentID, bool inQueue)
        {
            StudentCompanyInformation info = new StudentCompanyInformation();

            using (var cn = DbConnSingleton.getDbInstance().GetDBConnection())
            {
                if (inQueue)
                {
                    MySqlCommand cmd = new MySqlCommand("EditBoothQueue", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@companyid", id);
                    // cmd.Parameters.AddWithValue("@queueval", inQueue);
                    cmd.Parameters.AddWithValue("@studentID", studentID);
                    MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
            info = getStudentCompanyDetails(id, studentID);
            return info;
        }

        /// <summary>
        /// Returns List of Company Representatives based on Company Name
        /// </summary>
        /// <param name="companyName"></param>
        /// <returns></returns>
        public List<CompanyRepresentative> GetRepresentativeByCompanyID(int companyID)
        {
            CompanyRepresentative companyRepObj = null;
            List<CompanyRepresentative> list = new List<CompanyRepresentative>();
            string query = "select * from company_rep cr where cr.company_id = @cid";

            using (var cn = DbConnSingleton.getDbInstance().GetDBConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@cid", companyID);
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adap.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    companyRepObj = new CompanyRepresentative();
                    companyRepObj.first_name = dr["first_name"].ToString();
                    companyRepObj.last_name = dr["last_name"].ToString();
                    companyRepObj.email = dr["email"].ToString();
                    companyRepObj.phone = Convert.ToInt32(dr["phone"]);
                    if (Convert.ToInt32(dr["isAlumni"]).Equals(1))
                        companyRepObj.isAlumni = true;
                    else
                        companyRepObj.isAlumni = false;
                    list.Add(companyRepObj);
                }
                return list;
            }
        }
    }
}
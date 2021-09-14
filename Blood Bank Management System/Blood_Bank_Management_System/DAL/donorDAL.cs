using Blood_Bank_Management_System.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blood_Bank_Management_System.DAL
{
    class donorDAL
    {
        //Create a connection string to connect Database
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString; 
        #region SELECT to display data ion DataGridView from database
        public DataTable select()
        {
            //Create object to DataTable to hold the data from datatable and return it
            DataTable dt = new DataTable();

            //Create object of SQL connection to connect database 
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Write the SQL query to select the dta from database
                string sql = "SELECT * FROM tbl_donors";

                //Create the SQLCommand tp Execute the Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Create SQL data adapter to hold the data Temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Databse Connection
                conn.Open();

                //Pass the data from adapter to database
                adapter.Fill(dt);

            }
            catch(Exception ex)
            {
                //Display message if there is any exceptional errors
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Database connection
                conn.Close();
            }

            return dt;
        }
        #endregion

        #region INSERT Data to database
        public bool Insert(donorBLL d)
        {
            //Create a boolean variable and set its default value to false
            bool isSucess = false;

            //Ctrate SQL connection to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Write the data Query to INSERT data into database
                string sql = "INSERT INTO tbl_donors (first_name, last_name, email, contact, gender, address, blood_group, added_date, image_name, added_by) VALUES (@first_name, @last_name, @email, @contact, @gender, @address, @blood_group, @added_date, @image_name, @added_by)";

                //Create SQL command to exceute the query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Pass the value to SQL Query
                cmd.Parameters.AddWithValue("@first_name", d.first_name);
                cmd.Parameters.AddWithValue("@last_name", d.last_name);
                cmd.Parameters.AddWithValue("@email", d.email);
                cmd.Parameters.AddWithValue("@contact", d.contact);
                cmd.Parameters.AddWithValue("@gender", d.gender);
                cmd.Parameters.AddWithValue("@address", d.address);
                cmd.Parameters.AddWithValue("@blood_group", d.blood_group);
                cmd.Parameters.AddWithValue("@added_date", d.added_date);
                cmd.Parameters.AddWithValue("@image_name", d.image_name);
                cmd.Parameters.AddWithValue("@added_by", d.added_by);

                //Open Dtabase Connection
                conn.Open();

                //Create an Inter variable to check  wheather the query was exceuted succcessfully or Not
                int rows = cmd.ExecuteNonQuery();

                //If the query is Exceuted successfully the value of rows will be greater than zero else it will be zero
                if(rows>0)
                {
                    //Query Exceuted Successfully 
                    isSucess = true;
                }
                else
                {
                    //Failed to Exceute Query
                    isSucess = false;
                }


            }
            catch (Exception ex)
            {
                //Display Error Message if there's any exceptional Errors
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Database Connection
                conn.Close();
            }

            return isSucess;
        }
        #endregion
    }
}

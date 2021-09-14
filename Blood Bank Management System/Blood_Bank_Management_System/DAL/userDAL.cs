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
    class userDAL
    {
        //Create a Static string to connet database
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region SELECT data fron database 
        public DataTable Select()
        {
            //Create an iobject to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Create Datatable to Hold the data from database
            DataTable dt = new DataTable();

            try
            {
                //Write SQL quary to get tha data from batabase
                String sql = "SELECT * FROM tbl_users";

                //Create SQL command to execute query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Create Sql Data adapter to hold data from the database temporaily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Database connection 
                conn.Open();

                // Transfer Data from SqlData adapter to data table 
                adapter.Fill(dt);
            
            }
            catch(Exception ex)
            {
                //Display error message if there is any exceptional errors
                MessageBox.Show(ex.Message); 
            }
            finally
            {
                //Close database Connection
                conn.Close();
            }

            return dt;
        }
        #endregion

        #region Insert Data into Database for User Module
        public bool Insert(userBLL u)
        {
            //Create boolean variable and set its default value to false
            bool isSuccess = false;

            //Create an object of Sqlconnection to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Create a string variable to store the INSERT query
                string sql = "INSERT INTO tbl_users(username, email, password, full_name, contact, address, added_date, image_name) VALUES(@username, @email, @password, @full_name, @contact, @address, @added_date, @image_name)";

                //Create sql Command to pass the value in our query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Create the parameter to pass get the value from UI and pass it on SQL query above
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@full_name", u.full_name);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@image_name", u.image_name);

                //Open Datebase connection 
                conn.Open();

                //Create an integer variable to hold the value after the query is execuited
                int rows = cmd.ExecuteNonQuery();

                //The value of rows will begreater than 0 if the query is executed successfully
                //Else it'll be 0

                if (rows > 0)
                {
                    //Query Executed successfully 
                    isSuccess = true;
                }
                else
                {
                    //Failled to Execute query 
                    isSuccess = false;
                }

            }
            catch(Exception ex)
            {
                //Display Error message if there is any exceptional errors 
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close database Connection
                conn.Close();
            }


            return isSuccess;
        }
        #endregion

        #region UPDATE data in  database(User Module)
        public bool Update(userBLL u)
        {
            //create a boolean variable and set its default value to false
            bool isSuccess = false;

            //CreateParams an object for database connection
            SqlConnection conn = new SqlConnection(myconnstrng);


            try
            {
                //create a string variable to hold the SQL query
                string sql = "UPDATE tbl_users SET username=@username, email=@email, password=@password, full_name=@full_name, contact=@contact, address=@address, added_date=@added_date, image_name=@image_name WHERE user_id=@user_id";

                //Create sql command to execute querty and also pass the values to sql query 
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Now pass the values to SQL query 
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@full_name", u.full_name);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@image_name", u.image_name);
                cmd.Parameters.AddWithValue("@user_id", u.user_id);

                //Open Database Connection
                conn.Open();

                //Create an integer variable to hold the value after the query is execuited
                int rows = cmd.ExecuteNonQuery();


                //The value of rows will begreater than 0 if the query is executed successfully
                //Else it'll be 0

                if (rows > 0)
                {
                    //Query Executed successfully 
                    isSuccess = true;
                }
                else
                {
                    //Failled to Execute query 
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {
                //Display error message if there is any exceptional errors
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close database Connection
                conn.Close();
            }

            return isSuccess;

        }
        #endregion

        #region DELETE data from database(User Module)
        public bool Delete(userBLL u)
        {
            //create a boolean variable and set its default value to false
            bool isSuccess = false;

            //CreateParams an object for sql connection
            SqlConnection conn = new SqlConnection(myconnstrng);


            try
            {
                //create a string variable to delete the SQL data
                string sql = "DELETE FROM tbl_users WHERE user_id=@user_id";

                //Create sql command to execute querty
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Now pass the values to parameters 
                cmd.Parameters.AddWithValue("@user_id", u.user_id);

                //Open The Database Connection
                conn.Open();

                //Create an integer variable to hold the value after the query is execuited
                int rows = cmd.ExecuteNonQuery();


                //The value of rows will begreater than 0 if the query is executed successfully
                //Else it'll be 0

                if (rows > 0)
                {
                    //Query Executed successfully 
                    isSuccess = true;
                }
                else
                {
                    //Failled to Execute query 
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {
                //Display error message if there is any exceptional errors
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close database Connection
                conn.Close();
            }

            return isSuccess;

        }
        #endregion

        #region SEARCH
        public DataTable Search(string keywords)
        {
            //1. Create an SQL connection to connect Database
            SqlConnection conn = new SqlConnection(myconnstrng);

            //2. Create Datqa Table to hold the data from database temporarilly
            DataTable dt = new DataTable();
            try
            {

                string sql = "SELECT * FROM tbl_users WHERE user_id LIKE '%" + keywords + "%' OR full_name LIKE '%" + keywords + "%' OR address LIKE '%" + keywords + "%'";


                //Create sql command to execute querty
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Create SQL Data adapter to get the data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open The Database Connection
                conn.Open();

                //Pass the data from adapter to datatable
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                //Display error message if there is any exceptional errors
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close database Connection
                conn.Close();
            }
            return dt;
        }
        #endregion

    }
}

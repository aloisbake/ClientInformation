using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ClientWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ClientService : IClientService
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);

        /********************/
        string connectionString =  @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\alois\source\learn\csharp\ClientInformation\ClientWebService\App_Data\AloisBake.mdf;Integrated Security=True";

        // Provide the query string with a parameter placeholder.
        string queryString = "";          

        public string InsertClientDetails(ClientDetails eDetails)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                queryString = @"INSERT INTO Client(Name,Surname,Gender,ResAddress,WorkAddress,PostalAddress,CellNumber,WorkNumber) 
                            VALUES(@Name,@Surname,@Gender,@ResAddress,@WorkAddress,@PostalAddress,@CellNumber,@WorkNumber";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@Name", eDetails.Name);
                command.Parameters.AddWithValue("@Surname", eDetails.Surname);
                command.Parameters.AddWithValue("@Gender", eDetails.Gender);
                command.Parameters.AddWithValue("@ResAddress", eDetails.ResAddress);
                command.Parameters.AddWithValue("@WorkAddress", eDetails.WorkAddress);
                command.Parameters.AddWithValue("@PostalAddress", eDetails.PostalAddress);
                command.Parameters.AddWithValue("@CellNumber", eDetails.CellNumber);
                command.Parameters.AddWithValue("@WorkNumber", eDetails.WorkNumber);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    return "success";
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);                   
                }
            }
        }
        public DataSet GetClientDetails(ClientDetails eDetails)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                //    SqlCommand command = new SqlCommand(queryString, connection);
                //    command.Parameters.AddWithValue("@Id", eDetails.Id);

                //    try
                //    {
                //        connection.Open();
                //        SqlDataReader reader = command.ExecuteReader();
                //        while (reader.Read())
                //        {
                //            Console.WriteLine("\t{0}\t{1}\t{2}",
                //                reader[0], reader[1], reader[2]);
                //        }
                //        reader.Close();
                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine(ex.Message);
                //    }
                //}

                SqlCommand cmd = new SqlCommand("Get_AllEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", eDetails.Id);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.ExecuteNonQuery();
                con.Close();
                return ds;
            }
        }
        public DataSet FetchUpdatedRecords(ClientDetails eDetails)
        {          

            SqlCommand cmd = new SqlCommand("Get_AllEmployees", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", eDetails.Id);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmd.ExecuteNonQuery();
            con.Close();
            return ds;
        }
        public string UpdateClientDetails(ClientDetails eDetails)
        {
        
            string Status;
            SqlCommand cmd = new SqlCommand("USP_Emp_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", eDetails.Id);
            cmd.Parameters.AddWithValue("@Name", eDetails.Name);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                Status = "Record updated successfully";
            }
            else
            {
                Status = "Record could not be updated";
            }
            con.Close();
            return Status;
        }
        public bool DeleteClientDetails(ClientDetails eDetails)
        {
            SqlCommand cmd = new SqlCommand("USP_Emp_Delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", eDetails.Id);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
    }
}

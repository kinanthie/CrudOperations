using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;

namespace CrudOperations
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
        public string Insert(InsertUser user)
        {
            string Message;

            SqlConnection con = new SqlConnection("Data Source=DESKTOP-22Q18HI;Initial Catalog=premier-svr.MyTestDB;Persist Security Info=True;User ID=sa;Password=kinan;Pooling=false");
            con.Open();
                SqlCommand cmd = new SqlCommand("Insert into UserTab (Name, Email) values(@Name, @Email)", con);
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Email", user.Email);

            int result = cmd.ExecuteNonQuery();
            if(result == 1)
            { 
                Message = "Succesfully Inserted";
            }
            else
            {
                Message = "Failed to Insert";
            }
            con.Close();
            return Message;
        }
        public gettestdata GetInfo()
        {
            gettestdata g = new gettestdata();
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-22Q18HI;Initial Catalog=premier-svr.MyTestDB;Persist Security Info=True;User ID=sa;Password=kinan;Pooling=false");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from UserTab", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("UserTab");
            da.Fill(dt);
            g.userTab = dt;
            cmd.ExecuteNonQuery();
            con.Close();
            return g;

        }

        public string Update(UpdateUser u)
        {
            string Message = "";
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-22Q18HI;Initial Catalog=premier-svr.MyTestDB;Persist Security Info=True;User ID=sa;Password=kinan;Pooling=false");
            con.Open();
            SqlCommand cmd = new SqlCommand("Update UserTab set Name = @Name, Email = @Email where UserID= @UserID", con);
            cmd.Parameters.AddWithValue("@UserID", u.UID);
            cmd.Parameters.AddWithValue("@Name", u.Name);
            cmd.Parameters.AddWithValue("@Email", u.Email);
            int res = cmd.ExecuteNonQuery();
            if (res == 1)
            {
                Message = "Successfully Updated";
            }
            else
            {
                Message = "Failed to Update";
            }
            return Message;
        }

        public string Delete(DeleteUser d)
        {
            string Message = "";
            SqlConnection con = new SqlConnection("Data Source = DESKTOP-22Q18HI; Initial Catalog = premier-svr.MyTestDB; Persist Security Info = True; User ID = sa; Password = kinan; Pooling = false");
            con.Open();
            SqlCommand cmd = new SqlCommand("delete UserTab where UserID = @UserID", con);
            cmd.Parameters.AddWithValue("@UserID", d.UID);
            int res = cmd.ExecuteNonQuery();
            if (res == 1)
            {
                Message = "Succesfully deleted";
            }
            else
            {
                Message = "Failed to delete";
            }
            return Message;

        }
    }
}  

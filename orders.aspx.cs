using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace OZQ_gayendri
{
    public partial class orders : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindOrderData();
            }

        }

        private void BindOrderData()
        {
            if (Session["UserID"] != null)
            {
                int userID = Convert.ToInt32(Session["UserID"]);

                // Replace with your actual database connection string.
                string connectionString = "Data Source=HP-15S\\SQLEXPRESS;Initial Catalog=ozqDB;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT Orders.OrderID, Products.ProductName, Orders.Quantity, Orders.OrderDate, Orders.Status " +
                                   "FROM Orders " +
                                   "INNER JOIN Products ON Orders.ProductID = Products.ProductID " +
                                   "WHERE Orders.UserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                }
            }
            else
            {
                // Handle the case where the user is not logged in or the session variable is not set.
                // You may want to redirect the user to the login page or display an appropriate message.
            }
        }

        private bool IsUserLoggedIn()
        {
            // Check if the "Email" session variable exists and is not empty
            return Session["Email"] != null && !string.IsNullOrEmpty(Session["Email"].ToString());
        }

        private int GetLoggedInUserID()
        {
            if (IsUserLoggedIn())
            {
                string userEmail = Session["Email"].ToString();

                // Implement logic to retrieve the user's ID from the database based on their email
                // You should perform a database query to get the user's ID using their email
                // Replace the following line with your database query:
                int userID = GetUserIDByEmail(userEmail); // Implement this method

                return userID;
            }
            else
            {
                // Handle the case where the user is not logged in or the email is missing
                // You can return -1 or throw an exception, depending on your requirements
                return -1;
            }
        }

        private int GetUserIDByEmail(string email)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ozqDBConnectionString"].ConnectionString; // Use your actual connection string key

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT UserID FROM Users WHERE Email = @email";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@email", email);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        // Handle the case where the user with the specified email does not exist
                        // You can return -1 or throw an exception, depending on your requirements
                        return -1;
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (IsUserLoggedIn())
            {
                int userID = GetLoggedInUserID();
                int orderIDToDelete;

                if (int.TryParse(TextBox1.Text, out orderIDToDelete))
                {
                    // Replace with your actual database connection string.
                    string connectionString = "Data Source=HP-15S\\SQLEXPRESS;Initial Catalog=ozqDB;Integrated Security=True";

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        string deleteQuery = "DELETE FROM Orders WHERE OrderID = @OrderID AND UserID = @UserID";

                        using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@OrderID", orderIDToDelete);
                            cmd.Parameters.AddWithValue("@UserID", userID);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                // Order deleted successfully.
                                Response.Write("<script>alert('Order Canceled Successfully');</script>");
                                Response.Redirect(Request.RawUrl);
                            }
                            else
                            {
                                // Order not found or user doesn't have permission to delete.
                                // You can handle this case accordingly.
                                Response.Write("<script>alert('Order not found or you don't have permission to delete this order.');</script>");
                            }
                        }
                    }
                }
                else
                {
                    // Invalid Order ID.
                    Response.Write("<script>alert('Invalid Order ID.');</script>");
                }
            }
            else
            {
                // User not logged in. Handle this case accordingly.
                Response.Write("<script>alert('You must be logged in to delete orders.');</script>");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OZQ_gayendri
{
    public partial class Site1 : System.Web.UI.MasterPage
    {

        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["userID"] != null)
                {
                    int userID = Convert.ToInt32(Request.QueryString["userID"]);
                    // Now you have the userID, and you can use it to display the user's cart items.
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Check if the user is logged in
            if (IsUserLoggedIn())
            {
                string userEmail = Session["Email"] as string;

                if (!string.IsNullOrEmpty(userEmail))
                {
                    // Retrieve the user's ID from the database based on their email
                    int userID = GetUserIDByEmail(userEmail);

                    if (userID != -1)
                    {
                        // Redirect the user to their cart page with the userID as a query parameter
                        Response.Redirect("cart.aspx?userID=" + userID);
                    }
                    else
                    {
                        // Handle the case where the user's email is not found in the database
                        Response.Write("<script>alert('User not found.');</script>");
                    }
                }
                else
                {
                    // Handle the case where the user's email is missing in the session
                    Response.Write("<script>alert('User email missing.');</script>");
                }
            }
            else
            {
                // Redirect to the sign-in page for non-logged-in users
                Response.Redirect("signin.aspx");
            }
        }

        private int GetUserIDByEmail(string email)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

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

        private bool IsUserLoggedIn()
        {
            // Implement your logic to check if the user is logged in here
            // Return true if logged in, false otherwise
            return Session["Email"] != null; // Modify this based on your authentication method
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            // Get the category entered by the user from the textbox
            string category = TextBox1.Text.Trim();

            if (!string.IsNullOrEmpty(category))
            {
                // Redirect to the shop.aspx page with the selected category as a query parameter
                Response.Redirect("shop.aspx?category=" + category);
            }
            else
            {
                // Handle the case where the user didn't enter a category
                Response.Write("<script>alert('Please enter a category.');</script>");
            }
        }



    }
}
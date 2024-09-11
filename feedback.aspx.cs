using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OZQ_gayendri
{
    public partial class feedback : System.Web.UI.Page
    {
        static string rating;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Bind product data to the Repeater
                BindProductData();
            }

        }

        private void BindProductData()
        {
            // Replace with your actual database connection string.
            string connectionString = ConfigurationManager.ConnectionStrings["ozqDBConnectionString3"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT ProductID, ProductName, ProductImage FROM Products"; // Replace with your actual query
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    ProductRepeater.DataSource = dt;
                    ProductRepeater.DataBind();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (IsUserLoggedIn())
            {
                // Get the selected product's ID from the CommandArgument
                Button button = (Button)sender;
                int productID = Convert.ToInt32(button.CommandArgument);

                // Get the logged-in user's ID
                int userID = GetLoggedInUserID();

                // Get the user's feedback text from the TextBox
                TextBox feedbackTextBox = button.Parent.FindControl("TextBox1") as TextBox;
                string feedbackText = feedbackTextBox.Text;

                // Validate if the user provided feedback
                if (!string.IsNullOrEmpty(feedbackText))
                {
                    // Replace with your actual database connection string.
                    string connectionString = ConfigurationManager.ConnectionStrings["ozqDBConnectionString3"].ConnectionString;

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        // Insert the feedback into the database
                        string insertQuery = "INSERT INTO Feedbacks (UserID, ProductID, FeedbackText, Date) VALUES (@UserID, @ProductID, @FeedbackText, GETDATE())";

                        using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@UserID", userID);
                            cmd.Parameters.AddWithValue("@ProductID", productID);
                            cmd.Parameters.AddWithValue("@FeedbackText", feedbackText);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                // Feedback added successfully.
                                // You can add any success message or redirect to another page here.
                                Response.Write("<script>alert('Feedback submitted successfully.');</script>");
                            }
                            else
                            {
                                // Failed to add feedback.
                                // You can handle this case accordingly.
                                Response.Write("<script>alert('Failed to submit feedback.');</script>");
                            }
                        }
                    }
                }
                else
                {
                    // User didn't provide feedback text.
                    // You can add a validation message or handle this case accordingly.
                    Response.Write("<script>alert('Please provide feedback.');</script>");
                }
            }
            else
            {
                // User not logged in. Handle this case accordingly.
                Response.Write("<script>alert('You must be logged in to submit feedback.');</script>");
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
            string connectionString = ConfigurationManager.ConnectionStrings["ozqDBConnectionString3"].ConnectionString;

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
    }
}
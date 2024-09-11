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
    public partial class comments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get the product ID from the query string or any other source
                if (Request.QueryString["productID"] != null)
                {
                    int productID = Convert.ToInt32(Request.QueryString["productID"]);

                    // Bind feedback data to the Repeater
                    BindFeedbackData(productID);
                }
            }
        }

        private string GetProductName(int productId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string productName = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ProductName FROM Products WHERE ProductID = @ProductId";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ProductId", productId);
                        productName = cmd.ExecuteScalar()?.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
            }

            return productName;
        }

        private void BindFeedbackData(int productID)
        {
            // Replace with your actual database connection string.
            string connectionString = ConfigurationManager.ConnectionStrings["ozqDBConnectionString3"].ConnectionString;

            // Get the product name for the specified product ID
            string productName = GetProductName(productID);

            // Set the product name label
            ProductNameLabel.Text = productName;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Create and execute the SQL query to retrieve feedback data
                string query = @"SELECT Feedbacks.FeedbackID,
                               Feedbacks.ProductID,
                               Feedbacks.FeedbackText,
                               Feedbacks.Date,
                               Users.Email
                        FROM Feedbacks
                        INNER JOIN Users ON Feedbacks.UserID = Users.UserID
                        WHERE Feedbacks.ProductID = @ProductID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productID);

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
            // Redirect to the index.aspx page
            Response.Redirect("index.aspx");
        }
    }
}
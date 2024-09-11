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
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProductData();
                UpdateUIBasedOnUserRole();
            }
        }

        // LinkButtons

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("signup.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("signin.aspx");
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Response.Redirect("userprofile.aspx");
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            // Clear the user's session
            Session.Clear();
            Session.Abandon();

            // Redirect to the home page or any other page
            Response.Redirect("index.aspx");
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminprofile.aspx");
        }

        private void UpdateUIBasedOnUserRole()
        {
            try
            {
                if (Session["role"] == null || Session["role"].ToString() == "")
                {
                    // User is not logged in
                    ShowAnonymousUI();
                }
                else if (Session["role"].ToString() == "Member")
                {
                    // User is logged in as a regular user
                    ShowUserUI();
                }
                else if (Session["role"].ToString() == "Admin")
                {
                    // User is logged in as an admin
                    ShowAdminUI();
                }
            }
            catch
            {
                // Handle exceptions here
            }
        }

        private void ShowAnonymousUI()
        {
            LinkButton1.Visible = true;  // User login linkbutton
            LinkButton2.Visible = true;  // Sign up linkbutton

            LinkButton3.Visible = false; // User my profile linkbutton
            LinkButton4.Visible = false; // Logout linkbutton
            LinkButton5.Visible = false; // Admin my profile linkbutton
        }

        private void ShowUserUI()
        {
            LinkButton1.Visible = false;
            LinkButton2.Visible = false;
            LinkButton5.Visible = false;

            LinkButton3.Visible = true;  // User my profile linkbutton
            LinkButton4.Visible = true;  // Logout linkbutton
        }

        private void ShowAdminUI()
        {
            LinkButton1.Visible = false;
            LinkButton2.Visible = false;
            LinkButton3.Visible = false;

            LinkButton4.Visible = true;  // Logout linkbutton
            LinkButton5.Visible = true;  // Admin my profile linkbutton
        }

        // Display Product cards

        private void BindProductData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT ProductID, ProductName, Description, Price, StockQuantity, ProductImage FROM Products";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    ProductRepeater.DataSource = dt;
                    ProductRepeater.DataBind();
                }
            }
        }

        protected string GetActiveImgClass(int ItemIndex)
        {
            if (ItemIndex == 0)
            {
                return "active";
            }
            else
            {
                return "";

            }
        }

        // Add to cart

        protected void AddToCartButton_Click(object sender, EventArgs e)
        {
            // Get the ProductID from the button's CommandArgument (assuming you set this)
            Button button = (Button)sender;
            int productID = Convert.ToInt32(button.CommandArgument);

            // Check if the user is logged in (you need your own authentication logic)
            if (IsUserLoggedIn())
            {
                int userID = GetLoggedInUserID(); // Implement your logic to get the logged-in user's ID

                // Check if the product is already in the user's cart
                bool productExistsInCart = CheckIfProductExistsInCart(userID, productID);

                if (productExistsInCart)
                {
                    // Update the quantity in the cart
                    UpdateCartItemQuantity(userID, productID);
                }
                else
                {
                    // Add the product to the cart with quantity 1
                    AddProductToCart(userID, productID);
                }

                // Automatically add the product to the Orders table with status "Unpaid" and current timestamp
                //AddProductToOrders(userID, productID);


                // Redirect the user to their cart or continue shopping
                Response.Redirect("cart.aspx?userID=" + userID);
            }
            else
            {
                // Redirect to the sign-in page for non-logged-in users
                Response.Redirect("signin.aspx");
            }
        }

        private void AddProductToOrders(int userID, int productID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string insertQuery = "INSERT INTO Orders (UserID, ProductID, Quantity, OrderDate, Status) VALUES (@UserID, @ProductID, 1, GETDATE(), 'Unpaid')";

                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@ProductID", productID);

                    cmd.ExecuteNonQuery();
                }
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


        private bool CheckIfProductExistsInCart(int userID, int productID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string query = "SELECT COUNT(*) FROM Cart WHERE UserID = @userID AND ProductID = @productID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@userID", userID);
                        cmd.Parameters.AddWithValue("@productID", productID);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception (e.g., log the error)
                    Response.Write("Error checking if product exists in cart: " + ex.Message);
                    return false; // Return false to indicate an error
                }
            }
        }

        private void UpdateCartItemQuantity(int userID, int productID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "UPDATE Cart SET Quantity = Quantity + 1 WHERE UserID = @userID AND ProductID = @productID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@userID", userID);
                    cmd.Parameters.AddWithValue("@productID", productID);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void AddProductToCart(int userID, int productID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "INSERT INTO Cart (UserID, ProductID, Quantity) VALUES (@userID, @productID, 1)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@userID", userID);
                    cmd.Parameters.AddWithValue("@productID", productID);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
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
    public partial class shop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProductData();
            }
            if (!IsPostBack)
            {
                // Check if a category query parameter is provided
                string category = Request.QueryString["category"];

                if (!string.IsNullOrEmpty(category))
                {
                    // Display products based on the category
                    DisplayProductsByCategory(category);
                }
                else
                {
                    // Display all products (default behavior)
                    DisplayAllProducts();
                }
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            // Find the search textbox in the master page
            TextBox searchTextBox = (TextBox)Master.FindControl("TextBox1");

            // Check if the searchTextBox is found
            if (searchTextBox != null)
            {
                // Get the text entered by the user
                string category = searchTextBox.Text.Trim();

                // Now you can use the "category" variable for your search logic
                if (!string.IsNullOrEmpty(category))
                {
                    // Redirect to the shop page with the selected category as a query parameter
                    Response.Redirect("shop.aspx?category=" + category);
                }
            }
        }

        // Helper method to display products by category
        private void DisplayProductsByCategory(string category)
        {
            // Define your database connection string
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            // Create a SQL query to fetch products by category and join with the Categories table to get the category name
            string query = @"
        SELECT p.*, c.CategoryName
        FROM Products p
        INNER JOIN Categories c ON p.CategoryID = c.CategoryID
        WHERE c.CategoryName = @Category";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Set the parameter for the category
                    command.Parameters.AddWithValue("@Category", category);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        // Bind the retrieved data to the ProductRepeater
                        ProductRepeater.DataSource = reader;
                        ProductRepeater.DataBind();
                    }
                    catch (Exception ex)
                    {
                        // Handle any database connection or query errors
                        // You can log the error or display a message to the user
                        Response.Write($"Error: {ex.Message}");
                    }
                }
            }
        }


        // Helper method to display all products (default behavior)
        private void DisplayAllProducts()
        {
            // Define your database connection string
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            // Create a SQL query to fetch all products
            string query = "SELECT * FROM Products";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        // Bind the retrieved data to the ProductRepeater
                        ProductRepeater.DataSource = reader;
                        ProductRepeater.DataBind();
                    }
                    catch (Exception ex)
                    {
                        // Handle any database connection or query errors
                        // You can log the error or display a message to the user
                        Response.Write($"Error: {ex.Message}");
                    }
                }
            }
        }


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
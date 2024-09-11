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
    public partial class product : System.Web.UI.Page
    {
        private void LoadImageForProduct(int productId)
        {
            // Load the image for the specified product (assuming you have a ProductImage column in your database)
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ProductImage FROM Products WHERE ProductID = @ProductId";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ProductId", productId);

                        string imageUrl = cmd.ExecuteScalar()?.ToString();
                        if (!string.IsNullOrEmpty(imageUrl))
                        {
                            Image1.ImageUrl = "~/img/" + imageUrl; // Update the path as needed
                        }
                        else
                        {
                            Image1.ImageUrl = "~/img/default-image.jpg"; // Provide a default image source
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, log it, or display an error message.
                Response.Write("<script>alert('An error occurred while loading the product image.');</script>");
                // You can also log the exception details for debugging:
                // LogException(ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["productId"] != null)
                {
                    int productId;
                    if (int.TryParse(Request.QueryString["productId"], out productId))
                    {
                        // Now you have the product ID, and you can use it to fetch and display the product details.
                        DisplayProductDetails(productId);
                        LoadImageForProduct(productId); // Load the product image
                    }
                    else
                    {
                        // Handle invalid product ID query parameter.
                        Response.Write("<script>alert('Invalid product ID.'); window.location='index.aspx';</script>");
                    }
                }
                else
                {
                    // Handle missing product ID query parameter.
                    Response.Write("<script>alert('Product ID not provided.'); window.location='index.aspx';</script>");
                }
            }
        }

        private void DisplayProductDetails(int productId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM Products WHERE ProductID = @ProductId";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ProductId", productId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate the product details on your page controls.
                                ProductNameLabel.Text = reader["ProductName"].ToString();
                                DescriptionLabel.Text = reader["Description"].ToString();
                                PriceLabel.Text = "$" + Convert.ToDecimal(reader["Price"]).ToString("0.00");
                                StockQuantityLabel.Text = reader["StockQuantity"].ToString();
                            }
                            else
                            {
                                // Product not found, you can handle this case.
                                Response.Write("<script>alert('Product not found.');</script>");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, log it, or display an error message.
                Response.Write("<script>alert('An error occurred while fetching product details.');</script>");
                // You can also log the exception details for debugging:
                // LogException(ex);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Check if the user is logged in (you need your own authentication logic)
            if (IsUserLoggedIn())
            {
                // Get the ProductID from the query string
                int productId;
                if (int.TryParse(Request.QueryString["productId"], out productId))
                {
                    int userID = GetLoggedInUserID(); // Implement your logic to get the logged-in user's ID

                    // Check if the product is already in the user's cart
                    bool productExistsInCart = CheckIfProductExistsInCart(userID, productId);

                    if (productExistsInCart)
                    {
                        // Update the quantity in the cart
                        UpdateCartItemQuantity(userID, productId);
                    }
                    else
                    {
                        // Add the product to the cart with quantity 1
                        AddProductToCart(userID, productId);
                    }

                    // Automatically add the product to the Orders table with status "Unpaid" and current timestamp
                    //AddProductToOrders(userID, productId);

                    // Redirect the user to their cart or continue shopping
                    Response.Redirect("cart.aspx?userID=" + userID);
                }
                else
                {
                    // Handle invalid product ID query parameter.
                    Response.Write("<script>alert('Invalid product ID.'); window.location='index.aspx';</script>");
                }
            }
            else
            {
                // Redirect to the sign-in page for non-logged-in users
                Response.Redirect("signin.aspx");
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
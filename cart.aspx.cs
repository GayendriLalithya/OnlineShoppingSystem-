using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration.Provider;
using PayPal;
using PayPal.Api;


namespace OZQ_gayendri
{
    public partial class cart : System.Web.UI.Page
    {

        string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["userID"] != null)
                {
                    int userID = Convert.ToInt32(Request.QueryString["userID"]);
                    // Fetch and display cart items for the specified user
                    BindCartItems(userID);

                    // Calculate and display the total cost of cart items
                    CalculateTotalCost();
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

        private void BindCartItems(int userID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT C.CartID, P.ProductID, P.ProductName, P.Description, P.Price, P.ProductImage, P.StockQuantity, C.Quantity, Cat.CategoryName " +
                           "FROM Cart C " +
                           "INNER JOIN Products P ON C.ProductID = P.ProductID " +
                           "INNER JOIN Categories Cat ON P.CategoryID = Cat.CategoryID " +
                           "WHERE C.UserID = @userID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userID", userID);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    ProductRepeater.DataSource = dt;
                    ProductRepeater.DataBind();
                }
            }
        }


        // Delete from cart

        protected void DeleteCartItem_Click(object sender, EventArgs e)
        {
            // Get the CartID from the CommandArgument (assuming you set it in the button)
            Button button = (Button)sender;
            int cartID = Convert.ToInt32(button.CommandArgument);

            // Call a method to delete the item from the cart (you need to implement this method)
            bool deleteSuccess = DeleteCartItem(cartID);

            if (deleteSuccess)
            {
                // Delete the corresponding order (if it exists) based on UserID and ProductID
                int userID = GetLoggedInUserID();
                int productID = GetProductIDByCartID(cartID);

                // Reload the cart items after deletion
                BindCartItems(userID);
            }
            else
            {
                // Handle deletion failure (e.g., show an error message)
            }
        }


        private bool DeleteCartItem(int cartID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "DELETE FROM Cart WHERE CartID = @cartID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cartID", cartID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Check if the item was successfully deleted (rowsAffected > 0)
                    return rowsAffected > 0;
                }
            }
        }

        private int GetProductIDByCartID(int cartID)
        {
            int productID = -1; // Initialize to -1 if no match is found

            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Query to retrieve the ProductID based on the CartID
                    string query = "SELECT ProductID FROM Cart WHERE CartID = @cartID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@cartID", cartID);
                        var result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            // Parse the result to an integer
                            productID = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, log it, or print an error message for debugging
                Response.Write($"Error getting ProductID by CartID: {ex.Message}");
            }

            return productID;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Check if the user is logged in
            if (IsUserLoggedIn())
            {
                // Get the logged-in user's ID
                int userID = GetLoggedInUserID();

                // Redirect to the user's "orders.aspx" page with the user ID as a query parameter
                Response.Redirect($"orders.aspx?userID={userID}");
            }
            else
            {
                // Handle the case where the user is not logged in
                // You can display a message or redirect to the login page as needed
                Response.Write("<script>alert('You must be logged in to view your orders.');</script>");
            }
        }

        protected void ProductRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // Check the command name to determine which action to take
            if (e.CommandName == "DeleteCartItem")
            {
                // Get the CartID from the CommandArgument
                int cartID = Convert.ToInt32(e.CommandArgument);

                // Call a method to delete the item from the cart (you need to implement this method)
                bool deleteSuccess = DeleteCartItem(cartID);

                if (deleteSuccess)
                {
                    // Reload the cart items after deletion
                    BindCartItems(GetLoggedInUserID());
                }
                else
                {
                    // Handle deletion failure (e.g., show an error message)
                }
            }
        }

        private void CalculateTotalCost()
        {
            decimal totalCost = 0.00m;

            // Get the logged-in user's ID
            int userID = GetLoggedInUserID();

            // Retrieve the cart items for the user
            DataTable cartItems = RetrieveCartItemsFromDatabase(userID);

            // Calculate the total cost based on retrieved cart items
            totalCost = CalculateTotalCostFromCartItems(cartItems);

            // Display the total cost in the cartTotal label
            cartTotal.Text = totalCost.ToString("0.00");
        }


        private decimal CalculateTotalCostFromCartItems(DataTable cartItems)
        {
            decimal totalCost = 0.00m;

            // Implement the logic to calculate the total cost based on cart items
            // Loop through the DataTable and calculate the total cost
            foreach (DataRow row in cartItems.Rows)
            {
                decimal price = Convert.ToDecimal(row["Price"]);
                int quantity = Convert.ToInt32(row["Quantity"]);
                totalCost += price * quantity;
            }

            return totalCost;
        }

        private DataTable RetrieveCartItemsFromDatabase(int userID)
        {
            DataTable cartItems = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT C.CartID, P.ProductID, P.ProductName, P.Description, P.Price, P.StockQuantity, C.Quantity, Cat.CategoryName " +
                               "FROM Cart C " +
                               "INNER JOIN Products P ON C.ProductID = P.ProductID " +
                               "INNER JOIN Categories Cat ON P.CategoryID = Cat.CategoryID " +
                               "WHERE C.UserID = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    connection.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(cartItems);
                    }
                }
            }

            return cartItems;
        }


        protected void Button2_Click(object sender, EventArgs e)
        {
            // Check if the user is logged in
            if (IsUserLoggedIn())
            {
                // Get the logged-in user's ID
                int userID = GetLoggedInUserID();

                // Retrieve the cart items for the user
                DataTable cartItems = RetrieveCartItemsFromDatabase(userID);

                // Check if there are items in the cart
                if (cartItems.Rows.Count > 0)
                {
                    // Create an order for each item in the cart
                    foreach (DataRow row in cartItems.Rows)
                    {
                        int productID = Convert.ToInt32(row["ProductID"]);
                        int quantity = Convert.ToInt32(row["Quantity"]);
                        DateTime orderDate = DateTime.Now;
                        string status = "Pending"; // You can set the initial status as needed

                        // Add the order to the Orders table
                        AddOrder(userID, productID, quantity, orderDate, status);

                        // Delete the item from the cart
                        int cartID = Convert.ToInt32(row["CartID"]);
                        DeleteCartItem(cartID);
                    }

                    // Reload the cart items after placing the order
                    BindCartItems(userID);

                    // Calculate and display the total cost (if needed)
                    CalculateTotalCost();
                }
                else
                {
                    // Handle the case where the cart is empty (show a message or take appropriate action)
                    Response.Write("<script>alert('Your cart is empty.');</script>");
                }
            }
            else
            {
                // Handle the case where the user is not logged in (show a message or redirect to the login page)
                Response.Write("<script>alert('You must be logged in to place an order.');</script>");
            }
        }

        private void AddOrder(int userID, int productID, int quantity, DateTime orderDate, string status)
        {
            // Implement the logic to add an order to the Orders table
            // You should perform an INSERT query to add the order to the database
            // Use the provided parameters to populate the INSERT query
            string query = "INSERT INTO Orders (UserID, ProductID, Quantity, OrderDate, Status) VALUES (@UserID, @ProductID, @Quantity, @OrderDate, @Status)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@ProductID", productID);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@OrderDate", orderDate);
                    cmd.Parameters.AddWithValue("@Status", status);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        private decimal CalculateTotalCostCalCulator()
        {
            decimal totalCost = 0.00m;

            // Get the logged-in user's ID
            int userID = GetLoggedInUserID();

            // Retrieve the cart items for the user
            DataTable cartItems = RetrieveCartItemsFromDatabase(userID);

            // Calculate the total cost based on retrieved cart items
            totalCost = CalculateTotalCostFromCartItems(cartItems);

            //return totalCost; // Return the actual calculated total cost

            return 100.00m;
        }



        //
        // PayPal Order Section 
        //

        private APIContext apiContext;

        protected void InitializePayPalIntegration()
        {
            // Retrieve PayPal settings from app.config
            var clientId = ConfigurationManager.AppSettings["clientId"];
            var clientSecret = ConfigurationManager.AppSettings["clientSecret"];
            var mode = ConfigurationManager.AppSettings["mode"];

            // Check if clientId is not null or empty
            if (string.IsNullOrEmpty(clientId))
            {
                throw new Exception("PayPal client ID is missing. Please check your configuration.");
            }

            var config = new Dictionary<string, string>
            {
                { "mode", mode },
                { "clientId", clientId },
                { "clientSecret", clientSecret }
            };

            // Get an access token
            var accessToken = new OAuthTokenCredential(config).GetAccessToken();

            // Assign to the class-level apiContext
            apiContext = new APIContext(accessToken) { Config = config };
        }



        protected void SubmitPaymentButton_Click(object sender, EventArgs e)
        {
            // Initialize PayPal integration
            InitializePayPalIntegration();

            int userId = 0; // Default value

            if (Request.QueryString["userID"] != null)
            {
                userId = Convert.ToInt32(Request.QueryString["userID"]);
            }
            else
            {
                // Handle the case where the userID is not provided in the query string
                // For now, let's display an error message
                Response.Write("User ID not found in the query string.");
                return; // Stop processing further as we don't have a valid user ID
            }

            decimal totalAmount = CalculateTotalCostCalCulator();
            //string returnUrl = ""; // Declare the returnUrl variable here

            try
            {
                // Create a Payment object
                var payment = new Payment
                {
                    intent = "sale",
                    payer = new Payer { payment_method = "paypal" },
                    transactions = new List<Transaction>
            {
                new Transaction
                {
                    description = "Your purchase description",
                    amount = new Amount
                    {
                        currency = "USD",
                        total = totalAmount.ToString("0.00") // Make sure to format the amount correctly
                    }
                }
            },
                    redirect_urls = new RedirectUrls
                    {
                        return_url = "https://localhost:44373/PayPal/Success.aspx",
                        cancel_url = "https://localhost:44373/PayPal/Cancel.aspx"   // Set your cancel page URL
                        //return_url = returnUrl, // Set your success page URL
                    }
                };

                // Create and execute the payment
                var createdPayment = payment.Create(apiContext);

                // Set the returnUrl with the createdPayment.id
                //returnUrl = "http://localhost:PayPal/Success.aspx?paymentId=" + createdPayment.id + "&PayerID=" + userId;

                // Add payment information to the payment table with a successful payment status
                AddPaymentToDatabase(userId, totalAmount, true);

                // Redirect to PayPal for payment
                Response.Redirect(createdPayment.links.First(x => x.rel.ToLower() == "approval_url").href);

                // Update the return_url with the new value
                // payment.redirect_urls.return_url = returnUrl;
            }
            catch (PayPalException ex)
            {
                // Handle any exceptions related to PayPal API calls
                // Log the exception, display an error to the user, etc.
                // Example:
                // Response.Write("Error: " + ex.Message);
            }
        }

        private void AddPaymentToDatabase(int userId, decimal totalAmount, bool paymentSuccess)
        {
            // You can use your database connection and SQL query to insert payment information
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string insertPaymentQuery = "INSERT INTO Payments (PaymentDate, PaymentMethod, Amount, PaymentStatus) " +
                                        "VALUES (@PaymentDate, @PaymentMethod, @Amount, @PaymentStatus)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(insertPaymentQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@PaymentDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@PaymentMethod", "Online Payment");
                    cmd.Parameters.AddWithValue("@Amount", totalAmount);

                    // Set the payment status based on whether the payment was successful or not
                    if (paymentSuccess)
                    {
                        cmd.Parameters.AddWithValue("@PaymentStatus", "Paid");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@PaymentStatus", "Failed");
                    }

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
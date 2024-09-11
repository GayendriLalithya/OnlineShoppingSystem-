using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace OZQ_gayendri
{
    public partial class manageproducts : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve the list of categories from the database
                DataTable categoriesTable = GetCategoriesFromDatabase();

                // Bind the categories to the DropDownList
                DropDownList1.DataSource = categoriesTable;
                DropDownList1.DataTextField = "CategoryName"; // Assuming the category name is stored in a field named "CategoryName"
                DropDownList1.DataValueField = "CategoryID"; // Assuming the category ID is stored in a field named "CategoryID"
                DropDownList1.DataBind();
            }
        }

            private DataTable GetCategoriesFromDatabase()
        {
            DataTable categoriesTable = new DataTable();

            // Define your connection string
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Define your SQL query to retrieve categories
                string query = "SELECT CategoryID, CategoryName FROM Categories";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(categoriesTable);
                    }
                    catch (Exception ex)
                    {
                        // Handle any database connection or query errors
                        // You can log the error or display a message to the user
                        Response.Write($"Error: {ex.Message}");
                    }
                }
            }

            return categoriesTable;
        }



        // Add Button

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (CheckIfProductExist())
            {
                Response.Write("<script>alert('This Product Already Exists. You cannot add the same Product ID');</script>");
            }
            else
            {
                AddNewProduct();
            }

        }

        // Update Button

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (CheckIfProductExist())
            {
                UpdateProduct();
            }
            else
            {
                Response.Write("<script>alert('Product Does Not Exist.');</script>");
            }
        }

        // Delete Button

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (CheckIfProductExist())
            {
                DeleteProduct();
            }
            else
            {
                Response.Write("<script>alert('Product Does Not Exist.');</script>");
            }
        }

        // Search Button

        protected void Button4_Click(object sender, EventArgs e)
        {
            GetProductByID();
            LoadImageForProduct();
        }


        // Function to check if a product with the given ProductID already exists
        private bool CheckIfProductExist()
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query = "SELECT COUNT(*) FROM Products WHERE ProductID = @ProductID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", TextBox1.Text.Trim());
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        // Function to add a new product
        private void AddNewProduct()
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query = "INSERT INTO Products (ProductID, ProductName, CategoryID, Description, Price, StockQuantity, ProductImage) " +
                               "VALUES (@ProductID, @ProductName, @CategoryID, @Description, @Price, @StockQuantity, @ProductImage)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@ProductName", TextBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@CategoryID", DropDownList1.SelectedValue);
                    cmd.Parameters.AddWithValue("@Description", TextBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@Price", TextBox4.Text.Trim());
                    cmd.Parameters.AddWithValue("@StockQuantity", TextBox5.Text.Trim());

                    if (FileUpload1.HasFile)
                    {
                        // Check if the file is a valid image file (JPG, JPEG, or PNG)
                        string fileExtension = Path.GetExtension(FileUpload1.FileName).ToLower();
                        if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                        {
                            // Generate a unique file name to prevent conflicts
                            string fileName = Guid.NewGuid().ToString() + fileExtension;
                            string filePath = Server.MapPath("~/img/") + fileName;
                            FileUpload1.SaveAs(filePath);
                            cmd.Parameters.AddWithValue("@ProductImage", fileName);
                        }
                        else
                        {
                            Response.Write("<script>alert('Invalid file format. Please upload a JPG, JPEG, or PNG file.');</script>");
                            return; // Exit the method without adding the product
                        }
                    }
                    else
                    {
                        // Set a default image path if no image is uploaded
                        cmd.Parameters.AddWithValue("@ProductImage", "default-image.jpg");
                    }

                    con.Open();
                    cmd.ExecuteNonQuery();
                    Response.Write("<script>alert('Product added successfully');</script>");
                    ClearForm();
                }
            }
        }



        private void UpdateProduct()
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query = "UPDATE Products SET ProductName = @ProductName, " +
                               "CategoryID = @CategoryID, Description = @Description, " +
                               "Price = @Price, StockQuantity = @StockQuantity, ProductImage = @ProductImage " +
                               "WHERE ProductID = @ProductID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@ProductName", TextBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@CategoryID", DropDownList1.SelectedValue);
                    cmd.Parameters.AddWithValue("@Description", TextBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@Price", TextBox4.Text.Trim());
                    cmd.Parameters.AddWithValue("@StockQuantity", TextBox5.Text.Trim());

                    if (FileUpload1.HasFile)
                    {
                        // Check if the file is a valid image file (JPG, JPEG, or PNG)
                        string fileExtension = Path.GetExtension(FileUpload1.FileName).ToLower();
                        if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                        {
                            // Generate a unique file name to prevent conflicts
                            string fileName = Guid.NewGuid().ToString() + fileExtension;
                            string filePath = Server.MapPath("~/img/") + fileName;
                            FileUpload1.SaveAs(filePath);
                            cmd.Parameters.AddWithValue("@ProductImage", fileName);
                        }
                        else
                        {
                            Response.Write("<script>alert('Invalid file format. Please upload a JPG, JPEG, or PNG file.');</script>");
                            return; // Exit the method without updating the product
                        }
                    }
                    else
                    {
                        // If no image is uploaded, keep the existing image
                        cmd.Parameters.AddWithValue("@ProductImage", GetProductImageFromDatabase(TextBox1.Text.Trim()));
                    }

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Response.Write("<script>alert('Product updated successfully');</script>");
                        ClearForm();
                    }
                    else
                    {
                        Response.Write("<script>alert('Product update failed');</script>");
                    }
                }
            }
        }



        // Function to delete an existing product
        private void DeleteProduct()
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query = "DELETE FROM Products WHERE ProductID = @ProductID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", TextBox1.Text.Trim());

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Response.Write("<script>alert('Product deleted successfully');</script>");
                        ClearForm();
                    }
                    else
                    {
                        Response.Write("<script>alert('Product deletion failed');</script>");
                    }
                }
            }
        }

        // Function to retrieve product information by ProductID
        private void GetProductByID()
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query = "SELECT * FROM Products WHERE ProductID = @ProductID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", TextBox1.Text.Trim());
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        TextBox2.Text = reader["ProductName"].ToString();
                        DropDownList1.SelectedValue = reader["CategoryID"].ToString();
                        TextBox3.Text = reader["Description"].ToString();
                        TextBox4.Text = reader["Price"].ToString();
                        TextBox5.Text = reader["StockQuantity"].ToString();
                    }
                    else
                    {
                        Response.Write("<script>alert('Product not found');</script>");
                        ClearForm();
                    }
                }
            }
        }

        protected void ProductRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Assuming you have a DataRowView as the data source
                DataRowView dataItem = e.Item.DataItem as DataRowView;
                if (dataItem != null)
                {
                    Image Image1 = e.Item.FindControl("Image1") as Image;
                    if (Image1 != null)
                    {
                        string imageUrl = dataItem["ProductImage"].ToString();
                        Image1.ImageUrl = imageUrl;
                    }
                }
            }
        }

        private void LoadImageForProduct()
        {
            // Load the image for the currently selected product (assuming you have a ProductImage column in your database)
            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query = "SELECT ProductImage FROM Products WHERE ProductID = @ProductID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", TextBox1.Text.Trim());
                    con.Open();
                    string imageUrl = cmd.ExecuteScalar()?.ToString();
                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        Image1.ImageUrl = "img/" + imageUrl; // Update the path as needed
                    }
                    else
                    {
                        Image1.ImageUrl = "img/default-image.jpg"; // Provide a default image source
                    }
                }
            }
        }

        // Helper function to retrieve the product image filename from the database
        private string GetProductImageFromDatabase(string productID)
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query = "SELECT ProductImage FROM Products WHERE ProductID = @ProductID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productID);
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return result.ToString();
                    }
                    else
                    {
                        // Return a default image filename if no image is found in the database
                        return "default-image.jpg";
                    }
                }
            }
        }



        // Function to clear form fields
        private void ClearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            DropDownList1.SelectedIndex = 0;
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            // Clear the user's session
            Session.Clear();
            Session.Abandon();

            // Redirect to the home page or any other page
            Response.Redirect("index.aspx");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OZQ_gayendri
{
    public partial class adminprofile : System.Web.UI.Page
    {

        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the user is already logged in and redirect accordingly
                if (Session["role"] != null)
                {
                    RedirectBasedOnUserRole(Session["role"].ToString());
                    if (Session["Email"] != null)
                    {
                        string userEmail = Session["Email"].ToString();
                        LoadUserData(userEmail);
                    }
                }
            }
        }

        private void LoadUserData(string userEmail)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    string query = "SELECT * FROM Users WHERE Email = @Email";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Email", userEmail);

                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                TextBox1.Text = dr["UserName"].ToString();
                                TextBox2.Text = dr["ContactNo"].ToString();
                                TextBox3.Text = dr["Street"].ToString();
                                TextBox4.Text = dr["City"].ToString();
                                TextBox5.Text = dr["PostalCode"].ToString();
                                TextBox6.Text = dr["Email"].ToString();
                                // You may want to hide sensitive data like password or not load it here.
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

        private void RedirectBasedOnUserRole(string userType)
        {
            if (userType == "Member")
            {
                Response.Redirect("userprofile.aspx"); // Replace with the actual admin page URL.
            }
            else if (userType == "Admin")
            {
                // Redirect members to the appropriate page.
                //Response.Redirect("adminprofile.aspx"); // Replace with the actual member page URL.
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            UpdateUser();
        }

        void UpdateUser()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("UPDATE Users SET UserName = @userName, ContactNo = @contactNo, Street = @street, City = @city, PostalCode = @postalCode WHERE Email = @email", con);

                cmd.Parameters.AddWithValue("@email", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@userName", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@contactNo", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@street", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@city", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@postalCode", TextBox5.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Information Updated Successfully');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string userEmail = Session["Email"].ToString(); // Get the user's email from the session
            string oldPassword = TextBox7.Text.Trim(); // Entered old password
            string newPassword = TextBox8.Text.Trim(); // Entered new password

            // Retrieve the stored hashed password from the database
            string storedPasswordHash = GetStoredPasswordHash(userEmail);

            // Verify the entered old password against the stored hashed password
            if (VerifyPassword(oldPassword, storedPasswordHash))
            {
                // Old password is correct, hash the new password
                string newHashedPassword = HashPassword(newPassword);

                // Update the password with the new hashed password in the database
                UpdateUserPassword(userEmail, newHashedPassword);

                Response.Write("<script>alert('Password Changed Successfully');</script>");
            }
            else
            {
                Response.Write("<script>alert('Invalid old password. Please try again.');</script>");
            }
        }

        // Retrieve the stored hashed password from the database
        string GetStoredPasswordHash(string userEmail)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    string query = "SELECT Password FROM Users WHERE Email = @Email";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Email", userEmail);

                        // Execute the query to retrieve the hashed password
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            return result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }

            // Return an empty string if there was an error or if the email wasn't found
            return "";
        }


        // Update the user's password in the database with the new hashed password
        void UpdateUserPassword(string userEmail, string newHashedPassword)
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("UPDATE Users SET Password = @password WHERE Email = @email", con);

                cmd.Parameters.AddWithValue("@email", userEmail);
                cmd.Parameters.AddWithValue("@password", newHashedPassword);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }


        // Hash Password
        string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2")); // Convert each byte to a hexadecimal string
                }
                return builder.ToString();
            }
        }

        // Verify the hashed password
        bool VerifyPassword(string inputPassword, string storedPasswordHash)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputPassword));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2")); // Convert each byte to a hexadecimal string
                }
                string inputPasswordHash = builder.ToString();

                // Compare the hashed input password with the stored hashed password in a case-insensitive manner
                return string.Equals(inputPasswordHash, storedPasswordHash, StringComparison.OrdinalIgnoreCase);
            }
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
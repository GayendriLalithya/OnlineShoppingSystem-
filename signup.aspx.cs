using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace OZQ_gayendri
{
    public partial class signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=HP-15S\\SQLEXPRESS;Initial Catalog=ozqDB;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Check if the user already exists (you can use email or username for uniqueness)
                string checkUserQuery = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                SqlCommand checkUserCmd = new SqlCommand(checkUserQuery, con);
                checkUserCmd.Parameters.AddWithValue("@Email", TextBox1.Text);

                int existingUsersCount = (int)checkUserCmd.ExecuteScalar();

                if (existingUsersCount > 0)
                {
                    // User already exists, show an error message or redirect to a login page.
                    Response.Write("<script> alert('User with this email already exists.'); </script>");
                }
                else
                {
                    // User doesn't exist, proceed with registration and set UserType to "Member"
                    string password = TextBox2.Text;
                    string hashedPassword = HashPassword(password); // Hash the password

                    // User doesn't exist, proceed with registration and set UserType to "Member"
                    string insertQuery = "INSERT INTO Users (UserName, Street, City, PostalCode, ContactNo, Email, Password, UserType) " +
                                         "VALUES (@UserName, @Street, @City, @PostalCode, @ContactNo, @Email, @Password, @UserType)";

                    SqlCommand cmd = new SqlCommand(insertQuery, con);
                    cmd.Parameters.AddWithValue("@UserName", TextBox3.Text);
                    cmd.Parameters.AddWithValue("@Street", TextBox4.Text);
                    cmd.Parameters.AddWithValue("@City", TextBox5.Text);
                    cmd.Parameters.AddWithValue("@PostalCode", TextBox6.Text);
                    cmd.Parameters.AddWithValue("@ContactNo", TextBox7.Text);
                    cmd.Parameters.AddWithValue("@Email", TextBox1.Text);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    cmd.Parameters.AddWithValue("@UserType", "Member"); // Set UserType to "Member"

                    cmd.ExecuteNonQuery();
                    Response.Write("<script> alert('Successfully Registered'); </script>");

                    // Redirect to the login page after successful registration
                    Response.Redirect("signin.aspx");
                }
            }
        }

        // Password Hashing
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
    }
}
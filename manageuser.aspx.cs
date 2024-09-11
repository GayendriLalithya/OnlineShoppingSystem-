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
    public partial class manageuser : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        // Add Button
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (CheckIfUserExist())
            {
                Response.Write("<script>alert('This User Already Exists. You cannot add the same User ID');</script>");
            }
            else
            {
                AddNewUser();
            }
        }

        // Update Button
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (CheckIfUserExist())
            {
                UpdateUser();
            }
            else
            {
                Response.Write("<script>alert('User Does Not Exist.');</script>");
            }
        }

        // Delete Button
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (CheckIfUserExist())
            {
                DeleteUser();
            }
            else
            {
                Response.Write("<script>alert('User Does Not Exist.');</script>");
            }
        }

        // Search Button
        protected void Button4_Click(object sender, EventArgs e)
        {
            GetUserByID();
        }

        // User defined function

        void GetUserByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE UserID = @userID", con);
                cmd.Parameters.AddWithValue("@userID", TextBox1.Text.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    // Populate user details in respective TextBoxes
                    TextBox3.Text = dt.Rows[0]["UserName"].ToString();
                    TextBox2.Text = dt.Rows[0]["UserType"].ToString();
                    TextBox4.Text = dt.Rows[0]["ContactNo"].ToString();
                    TextBox5.Text = dt.Rows[0]["Street"].ToString();
                    TextBox6.Text = dt.Rows[0]["City"].ToString();
                    TextBox7.Text = dt.Rows[0]["PostalCode"].ToString();
                    TextBox8.Text = dt.Rows[0]["Email"].ToString();
                    TextBox9.Text = dt.Rows[0]["Password"].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Invalid User ID');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void DeleteUser()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE UserID='" + TextBox1.Text.Trim() + "';", con);
                
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('User Deleted Successfully');</script>");  
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            ClearForm();
            // After deleting, rebind any relevant data controls (e.g., GridView)
            GridView1.DataBind();
        }

        // Function to hash a password using SHA-256
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);

                // Convert the byte array to a hexadecimal string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2")); // "x2" formats the byte as a two-digit hexadecimal number
                }
                return builder.ToString();
            }
        }

        // Function to add a new user with a hashed password
        private void AddNewUser()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();

                    // Check if the user already exists (you can use email or username for uniqueness)
                    string checkUserQuery = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                    SqlCommand checkUserCmd = new SqlCommand(checkUserQuery, con);
                    checkUserCmd.Parameters.AddWithValue("@Email", TextBox8.Text);

                    int existingUsersCount = (int)checkUserCmd.ExecuteScalar();

                    if (existingUsersCount > 0)
                    {
                        // User already exists, show an error message or redirect to a login page.
                        Response.Write("<script> alert('User with this email already exists.'); </script>");
                    }
                    else
                    {
                        // User doesn't exist, proceed with registration and set UserType to "Member"
                        string insertQuery = "INSERT INTO Users (UserName, Street, City, PostalCode, ContactNo, Email, Password, UserType) " +
                                             "VALUES (@UserName, @Street, @City, @PostalCode, @ContactNo, @Email, @Password, @UserType)";

                        SqlCommand cmd = new SqlCommand(insertQuery, con);
                        cmd.Parameters.AddWithValue("@UserName", TextBox3.Text);
                        cmd.Parameters.AddWithValue("@Street", TextBox5.Text);
                        cmd.Parameters.AddWithValue("@City", TextBox6.Text);
                        cmd.Parameters.AddWithValue("@PostalCode", TextBox7.Text);
                        cmd.Parameters.AddWithValue("@ContactNo", TextBox4.Text);
                        cmd.Parameters.AddWithValue("@Email", TextBox8.Text);

                        // Hash the password before storing it
                        string hashedPassword = HashPassword(TextBox9.Text);
                        cmd.Parameters.AddWithValue("@Password", hashedPassword);

                        cmd.Parameters.AddWithValue("@UserType", TextBox2.Text);

                        cmd.ExecuteNonQuery();
                        Response.Write("<script> alert('User Added Successfully'); </script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('An error occurred: " + ex.Message + "'); </script>");
            }
            ClearForm();
            // After adding, rebind any relevant data controls (e.g., GridView)
            GridView1.DataBind();
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

                // Hash the new password before updating
                string newPasswordHash = HashPassword(TextBox9.Text.Trim());

                SqlCommand cmd = new SqlCommand("UPDATE Users SET UserType = @userType, UserName = @userName, ContactNo = @contactNo, Street = @street, City = @city, PostalCode = @postalCode, Email = @email, Password = @password WHERE UserID = @userID", con);

                cmd.Parameters.AddWithValue("@userID", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@userType", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@userName", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@contactNo", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@street", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@city", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@postalCode", TextBox7.Text.Trim());
                cmd.Parameters.AddWithValue("@email", TextBox8.Text.Trim());
                cmd.Parameters.AddWithValue("@password", newPasswordHash); // Store the hashed password

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('User Updated Successfully');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            ClearForm();
            // After updating, rebind any relevant data controls (e.g., GridView)
            GridView1.DataBind();
        }

        bool CheckIfUserExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE UserID='" + TextBox1.Text.Trim() + "';", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }


        void ClearForm()
        {
            // Clear the form fields
            TextBox1.Text = string.Empty;
            TextBox2.Text = string.Empty;
            TextBox3.Text = string.Empty;
            TextBox4.Text = string.Empty;
            TextBox5.Text = string.Empty;
            TextBox6.Text = string.Empty;
            TextBox7.Text = string.Empty;
            TextBox8.Text = string.Empty;
            TextBox9.Text = string.Empty;
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
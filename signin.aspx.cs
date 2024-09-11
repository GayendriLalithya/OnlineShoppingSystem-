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
    public partial class signin : System.Web.UI.Page
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
                }
            }
        }

        // User Login


        protected void Button1_Click(object sender, EventArgs e)
        {
            string email = TextBox1.Text.Trim();
            string password = TextBox2.Text.Trim();

            string recaptchaResponse = Request.Form["g-recaptcha-response"];
            bool isCaptchaValid = ReCaptcha.ValidateRecaptcha(recaptchaResponse);

            if (!isCaptchaValid)
            {
                lblResult.Text = "ReCaptcha validation failed. Please try again.";
                return;  // Do not proceed with login if ReCaptcha is not valid
            }

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(strcon))
                    {
                        con.Open();
                        string query = "SELECT * FROM Users WHERE Email = @Email";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@Email", email); // Set the @Email parameter

                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    string storedPasswordHash = dr["Password"].ToString(); // Get the stored hashed password
                                    if (VerifyPassword(password, storedPasswordHash))
                                    {
                                        Session["Email"] = dr["Email"].ToString();
                                        Session["username"] = dr["UserName"].ToString();
                                        Session["role"] = dr["UserType"].ToString();
                                    }

                                    string userType = Session["role"].ToString();
                                    RedirectBasedOnUserRole(userType);
                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('Invalid Username or Password');</script>");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Email and password cannot be empty');</script>");
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


        private void RedirectBasedOnUserRole(string userType)
        {
            if (userType == "Admin")
            {
                Response.Redirect("index.aspx?userType=admin");
            }
            else if (userType == "Member")
            {
                 Response.Redirect("index.aspx?userType=user");
            }
        }
    }
}
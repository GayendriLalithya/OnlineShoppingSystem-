using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OZQ_gayendri
{
    public partial class category : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // Add Button

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkIfCategoryExist())
            {
                Response.Write("<script>alert('This Category Already Exist. You Cannot add the same category ID');</script>");
            }
            else
            {
                addNewCategory();
            }
        }

        // Delete Button

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkIfCategoryExist())
            {
                deleteCategory();
            }
            else
            {
                Response.Write("<script>alert('Category Does not Exist.');</script>");
            }
        }

        // Search Button

        protected void Button4_Click(object sender, EventArgs e)
        {
            getCategoryByID();
        }



        // Category defined function

        void getCategoryByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Categories WHERE CategoryID='" + TextBox1.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    TextBox2.Text = dt.Rows[0][1].ToString();
                }
                else
                {
                    Response.Write("<script> alert('Invalid Category ID'); </script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }
        }

        void deleteCategory()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("DELETE FROM Categories WHERE CategoryID='" + TextBox1.Text.Trim() + "';", con);

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script> alert('Category Deleted Successfully'); </script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            ClearForm();
            // After deleting, rebind the GridView
            GridView1.DataBind();
        }

        void addNewCategory()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO Categories (CategoryID, CategoryName)" +
                                         "VALUES (@categoryid, @categoryname)", con);


                cmd.Parameters.AddWithValue("@categoryid", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@categoryname", TextBox2.Text.Trim());

                cmd.ExecuteNonQuery();
                Response.Write("<script> alert('Category added successfully'); </script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            ClearForm();
            // After adding, rebind the GridView
            GridView1.DataBind();
        }

        bool checkIfCategoryExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Categories where CategoryID='" + TextBox1.Text.Trim() + "';", con);
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
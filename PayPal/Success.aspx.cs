using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OZQ_gayendri.PayPal
{
    public partial class Success : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve paymentId and PayerID from the query string
                string paymentId = Request.QueryString["paymentId"];
                string payerId = Request.QueryString["PayerID"];

                if (!string.IsNullOrEmpty(paymentId) && !string.IsNullOrEmpty(payerId))
                {
                    // Payment was successful, update the payment status or perform other actions
                    // You can use paymentId and payerId to identify the payment and the payer
                    // Update your database or perform any necessary processing here

                    // Redirect to a success page or display a success message
                    Response.Redirect("http://localhost:PayPal/Success.aspx");
                }
                else
                {
                    // Payment parameters are missing or invalid, handle the error
                    // Redirect to an error page or display an error message
                    Response.Redirect("http://localhost:PayPal/Cancel.aspx");
                }
            }
        }
    }
}
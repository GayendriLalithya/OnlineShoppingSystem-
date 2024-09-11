<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="feedback.aspx.cs" Inherits="OZQ_gayendri.feedback" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <!---------PRODUCT LIST PRODUCT LIST-------->
    <div class="container-fluid">
        <div class="container-fluid main-section p-3">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <div class="heading-section">
                            <h2 class="p-5" style="margin-top: 60px;">Send Us A Feedback</h2>
                            <div class="heading-borders">
                                <!--<span class="selected"></span>-->
                            </div>
                        </div>
                    </div>
                    <div class="col-md-8">
                    </div>
                </div>
                <div class="container-fluid">
                    <div class="row flex flex-wrap product-grid-main-wrap justify-content-center">
                        <asp:Repeater ID="ProductRepeater" runat="server">
                            <ItemTemplate>
                                <div class="col-lg-3 col-md-6 d-flex justify-content-center custom-nav-sudo-wrap famous-product"
                                    style="margin-left: 10px; margin-right: 10px;">
                                    <div class="card">
                                        <h5 class="card-title"><%# Eval("ProductName") %></h5>
                                        <div class="card-body">
                                            <img src='<%# ResolveUrl("~/img/" + Eval("ProductImage")) %>'
                                                class="card-img-top" alt='<%# Eval("ProductName") %>' width="50%"
                                                height="auto" max-width="708px" max-height="708px" />
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-group"
                                                TextMode="MultiLine" Placeholder="Add A Comment"></asp:TextBox>
                                            <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btn btn-secondary"
                                                CommandName="Ratings" CommandArgument='<%# Eval("ProductID") %>' OnClick="Button1_Click" />
                                        </div>
                                        <a href="comments.aspx?productId=<%# Eval("ProductID") %>" class="btn btn-dark">Comments</a>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
     <style>
         .main-section {
             font-size: 14px;
             color: white !important;
             background: #f7f1e8 !important;
             font-family: Poppins, sans-serif;
             /*overflow: scroll; */
         }

         .bt-card-wrap-main-custom-wrap{
            padding: 3em 0em 0em 0em !important;
        }

        .bt-card-wrap-main-custom-wrap .card{
            width: 100%;
        }

        .bt-card-wrap-main-custom-wrap .card img{
            padding:10px;
            width: 100%;
        }
        .card {
            border: none;
        }

        .form-group {
            width: 100%;
        }

        /* Add styles for buttons (adjust as needed) */
        .btn.button-1, .btn.button-2 {
            margin-top: 10px;
        }
        .carousel-indicators, .carousel span{
            visibility: hidden;
        }

        .btn-primary{
            background-color: forestgreen;
        }
        .btn-primary:hover{
            background-color: darkgreen;
        }

        .btn-secondary{
            background-color: #b80818;
            float: right;
        }
        .btn-secondary:hover{
            background-color: darkgrey;
            border: none;
        }
        .btn {
            color: white;
        }
        .btn:hover {
            color: white;
        }

        /* Rating */
        .Star {
            background-image: url(stars/Star.gif);
            height: 17px;
            width: 17px;
        }

        .WaitingStar {
            background-image: url(stars/WaitingStar.gif);
            height: 17px;
            width: 17px;
        }

        .FilledStar {
            background-image: url(stars/FilledStar.gif);
            height: 17px;
            width: 17px;
        }
        
     </style>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="shop.aspx.cs" Inherits="OZQ_gayendri.shop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!---------PRODUCT LIST PRODUCT LIST-------->
    <div class="container-fluid">
        <div class="container-fluid  main-section p-3">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <div class="heading-section">
                            <h2 class="p-5" style="margin-top: 60px;">Checkout Our Top Sell Electronic Devices
                            </h2>
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
                                        <span class="heart-icon" style="font-size: 25px; color: #FF177C;"><i class="fas fa-heart">
                                        </i></span>
                                        <img src='<%# ResolveUrl("~/img/" + Eval("ProductImage")) %>' class="card-img-top"
                                            alt='<%# Eval("ProductName") %>' width="50%" height="auto" max-width="708px"
                                            max-heght="708px" />
                                        <hr>
                                        <div class="card-body">
                                            <h5 class="card-title"><%# Eval("ProductName") %></h5>
                                            <!--<p class="card-text card-disc"><%# Eval("Description") %></p>-->
                                            <p class="card-text">Price: $<%# Eval("Price") %></p>
                                            <p class="card-text">Stock: <%# Eval("StockQuantity") %></p>
                                            <a href='product.aspx?productId=<%# Eval("ProductID") %>' class="btn btn-primary">View More</a>
                                            <asp:Button runat="server" ID="AddToCartButton" CssClass="btn btn-secondary fa" Text="&#xf217;"
                                                CommandName="AddToCart" CommandArgument='<%# Eval("ProductID") %>' OnClick="AddToCartButton_Click" />
                                        </div>
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

         .bt-card-wrap-main-custom-wrap {
             padding: 3em 0em 0em 0em !important;
         }

             .bt-card-wrap-main-custom-wrap .card {
                 width: 100%;
             }

                 .bt-card-wrap-main-custom-wrap .card img {
                     padding: 10px;
                     width: 100%;
                 }

                 .bt-card-wrap-main-custom-wrap .card .heart-icon i {
                     position: absolute;
                     top: 5px;
                     left: 5px;
                     font-size: 25px !important;
                     color: #FF177C;
                 }

         .card {
             border: none;
         }
         /* Set the container for the carousel and banner content */
         .banner-container {
             position: relative;
         }

         /* Set the position of the carousel to absolute */
         #myCarousel {
             position: absolute;
             top: 0;
             left: 0;
             width: 100%;
             height: 100%;
             z-index: 0; /* Set a lower z-index to make it the background */
         }

         /* Set the z-index of the banner content to make it appear on top */
         .banner-detail {
             position: relative;
             z-index: 1;
             text-align: center; /* Center the text */
             color: #fff; /* Text color */
             padding: 50px; /* Add padding to improve visibility */
             width: 640px;
         }

         /* Additional styles for the carousel content (adjust as needed) */
         .carousel-caption {
             background-color: rgba(0, 0, 0, 0.5); /* Add a semi-transparent background for readability */
         }

         /* Add styles for buttons (adjust as needed) */
         .btn.button-1, .btn.button-2 {
             margin-top: 10px;
         }

         .carousel-indicators, .carousel span {
             visibility: hidden;
         }

         .btn-primary {
             background-color: forestgreen;
         }

             .btn-primary:hover {
                 background-color: darkgreen;
             }

         .btn-secondary {
             background-color: #b80818;
             float: right;
         }

             .btn-secondary:hover {
                 background-color: darkgrey;
                 border: none;
             }
     </style>

</asp:Content>

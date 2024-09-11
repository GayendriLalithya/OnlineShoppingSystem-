<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="index.aspx.cs" Inherits="OZQ_gayendri.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!---------BANNER Section BANNER Section------->
    <div class="container-fluid">
        <!-- Create a container for the carousel and banner content -->
        <div class="banner-container">
            <!-- Carousel -->
            <div id="myCarousel" class="carousel slide" data-ride="carousel" data-interval="3000">
                <!-- Indicators -->
                <ol class="carousel-indicators">
                    <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                    <li data-target="#myCarousel" data-slide-to="1"></li>
                    <li data-target="#myCarousel" data-slide-to="2"></li>
                </ol>
                <!-- Wrapper for slides -->
                <div class="carousel-inner">
                    <div class="carousel-item active">
                        <img src="img/laptops.jpg" alt="Laptops" style="width: 100%;">
                    </div>
                    <div class="carousel-item">
                        <img src="img/phone.jpg" alt="Laptops" style="width: 100%;">
                    </div>
                    <div class="carousel-item">
                        <img src="img/headset.jpg" alt="Laptops" style="width: 100%;">
                    </div>
                </div> 
                <!-- Left and right controls -->
                <a class="carousel-control-prev" href="#myCarousel" role="button" data-slide="prev">
                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                    <span class="sr-only">Previous</span>
                </a>
                <a class="carousel-control-next" href="#myCarousel" role="button" data-slide="next">
                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                    <span class="sr-only">Next</span>
                </a>
            </div>
            <!-- Banner content -->
            <div class="row">
                <div class="col-lg-1 col-12"></div>
                <div class="col-lg-4 col-md-7 col-12">
                    <div class="banner-detail carousel-caption">
                        <h5>Shopping Revaluation</h5>
                        <h1>OZQ Cart</h1>
                        <div class="col-md-12">
                            <asp:LinkButton ID="LinkButton1" runat="server" class="btn button-1" OnClick="LinkButton1_Click">Sign Up</asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" class="btn button-2" OnClick="LinkButton2_Click">Login</asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" runat="server" class="btn button-1" OnClick="LinkButton3_Click">My Profile</asp:LinkButton>
                            <asp:LinkButton ID="LinkButton5" runat="server" class="btn button-1" OnClick="LinkButton5_Click">My Profile</asp:LinkButton>
                            <asp:LinkButton ID="LinkButton4" runat="server" class="btn button-2" OnClick="LinkButton4_Click">Logout</asp:LinkButton>
                            <!--<a href="signup.aspx" class="btn button-1">Sign Up</a>
                          <a href="signin.aspx" class="btn button-2">Login</a>-->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
  <!---------PRODUCT LIST PRODUCT LIST-------->
    <div class="container-fluid  main-section">
        <div class="container" style="color: white; background: #f7f1e8;">
            <div class="row">
                <div class="col-md-12">
                    <div class="heading-section">
                        <h3>Our Top Sell</h3>
                        <h2 class="p-3">Explore Our Top-Selling Electronic Devices</h2>
                        <div class="heading-borders">
                            <span class="selected"></span>
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
    <style>
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

        /* Responsive styles */
        @media (max-width: 768px) {
            .banner-detail {
                width: 100%;
            }
        }

        @media (max-width: 576px) {
            .banner-container {
                padding: 0;
            }

            .banner-detail {
                padding: 20px;
            }

            .carousel-indicators,
            .carousel span {
                display: none;
            }

            .btn.button-1,
            .btn.button-2 {
                margin-top: 20px;
            }
        }
    </style>
    </style>




</asp:Content>

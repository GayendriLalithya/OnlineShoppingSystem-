<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="product.aspx.cs" Inherits="OZQ_gayendri.product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid">
        <div class="container-fluid  main-section p-3">
            <div class="p-5" style="margin-top: 40px;">
                <div class="container">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="heading-section">
                                <div class="heading-borders">
                                    <!--<span class="selected"></span>-->
                                </div>
                            </div>
                        </div>
                        <div class="col-md-8">
                        </div>
                    </div>
                    <div class="container-fluid">
                        <div class="row flex flex-wrap product-grid-main-wrap">
                            <div class="col-md-10 mx-auto">
                                <div class="card">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12 p-3" style="margin-left: 20px;">
                                                <asp:Label ID="ProductNameLabel" runat="server" CssClass="form-group" Font-Bold="True"
                                                    Font-Size="Large"></asp:Label>
                                                <div class="form-group">
                                                    <h3><b><%# Eval("ProductName") %></b></h3>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <center>
                                                    <asp:Image ID="Image1" runat="server" class="card-img-top" />
                                                    <!-- width="50%" height="auto" max-width="708px" max-heght="708px" -->
                                                </center>
                                            </div>
                                            <div class="col-md-6 align-items-center" style="padding-top: 10%;">
                                                <div class="col-md-6">
                                                    <div class="row">
                                                        <asp:Label ID="Label1" runat="server" CssClass="form-group"></asp:Label>
                                                        <div class="form-group">
                                                            <p>Price :</p>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <asp:Label ID="Label2" runat="server" CssClass="form-group"></asp:Label>
                                                        <div class="form-group">
                                                            <p>StockQuantity :</p>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <asp:Label ID="Label3" runat="server" CssClass="form-group"></asp:Label>
                                                        <div class="form-group">
                                                            <p>Description :</p>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 p-3" style="float: left;">
                                                    <div class="row">
                                                        <asp:Label ID="PriceLabel" runat="server" CssClass="form-group"></asp:Label>
                                                        <div class="form-group">
                                                            <p><%# Eval("Price") %></p>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <asp:Label ID="StockQuantityLabel" runat="server" CssClass="form-group"></asp:Label>
                                                        <div class="form-group">
                                                            <p><%# Eval("StockQuantity") %></p>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <asp:Label ID="DescriptionLabel" runat="server" CssClass="form-group"></asp:Label>
                                                        <div class="form-group">
                                                            <p><%# Eval("Description") %></p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="p-4">
                                            <asp:Button ID="Button1" runat="server" Text="Add to Cart"
                                                CssClass="btn btn-secondary" OnClick="Button1_Click"/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div style="margin-left: 95px;">
                            <a href="index.aspx"><< Back to Home</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>

     <style>
         .form-group {
             color: black !important;
             font-size: 20px !important;
         }

         a {
             color: #b80818;
         }

         .form-group h3 {
             font-size: 30px !important;
         }

         .main-section {
             font-size: 14px;
             color: white !important;
             background: #f7f1e8 !important;
             font-family: Poppins, sans-serif;
             /*overflow: scroll; */
         }

         .card {
             border: none;
         }


         /* Add styles for buttons (adjust as needed) */
         .btn.button-1, .btn.button-2 {
             margin-top: 10px;
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

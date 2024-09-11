<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cart.aspx.cs" Inherits="OZQ_gayendri.cart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- Bootstrap css -->
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="bootstrap/css/npm_bootstrap.min.css" rel="stylesheet" />
    <!-- Datatables css -->
    <link href="datatables/css/jquery.dataTables.min.css" rel="stylesheet" />
    <!-- Fontawesome css -->
    <link href="fontawesome/css/all.css" rel="stylesheet" />
    <!-- Custom CSS file -->
    <link href="css/cart.css" rel="stylesheet" />



</head>
<body>
    <form id="form1" runat="server">
        <div>
            <section class="h-100 h-custom">
                <div class="container">
                    <div class="row d-flex justify-content-center align-items-center h-100">
                        <div class="col" style="padding: 0 0 0 0;">
                            <div class="card shopping-cart" style="margin-top: 20px;">
                                <div class="row">
                                    <div class="col-md-10">
                                        <center>
                                            <h1>View Cart </h1>
                                        </center>
                                    </div>
                                    <div class="col-md-2 p-4">
                                        <center>
                                            <asp:Button ID="Button1" runat="server" Text="My Orders" CssClass="btn btn-primary" OnClick="Button1_Click"/>
                                        </center>
                                    </div>
                                </div>
                                <hr />
                                <div class="card-body text-black">
                                    <div class="row">
                                        <div class="col-md-7 p-3">
                                            <h3 class="text-center fw-bold text-uppercase p-3">Your products</h3>
                                            <asp:Repeater ID="ProductRepeater" runat="server">
                                                <ItemTemplate>
                                                    <div class="d-flex align-items-center">
                                                        <div class="flex-shrink-0">
                                                            <img src="<%# ResolveUrl("~/img/" + Eval("ProductImage")) %>"
                                                                class="img-fluid" style="width: 150px;" alt="<%# Eval("ProductName") %>">
                                                        </div>
                                                        <div class="flex-grow-1 ms-3">
                                                            <div class="row">
                                                                <div class="col-lg-10">
                                                                    <h5 class="text-primary"><%# Eval("ProductName") %></h5>
                                                                </div>
                                                                <div class="col-lg-2">
                                                                    <asp:Button ID="DeleteCartItem_Click" runat="server" Text="&#xf1f8;" CssClass="btn btn-outline-danger fa btn-lg"
                                                                        OnClick="DeleteCartItem_Click" CommandArgument='<%# Eval("CartID") %>' />
                                                                </div>
                                                            </div>
                                                            <h6 style="color: #9e9e9e;"><%# Eval("Description") %></h6>
                                                            <h6 style="color: #000;">Category: <%# Eval("CategoryName") %></h6>
                                                            <h6 style="color: #000;">Stock: <%# Eval("StockQuantity") %></h6>
                                                            <h6 style="color: #000;">Quantity: <%# Eval("Quantity") %></h6>
                                                            <div class="d-flex align-items-center">
                                                                <p class="fw-bold mb-0 me-5 pe-3"><%# Eval("Price") %></p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <hr class="mb-4" style="height: 2px; background-color: #1266f1; opacity: 1;" />
                                            <div class="d-flex justify-content-between p-2" style="background-color: #f7f1e8;">
                                                <!--background-color: #e1f5fe;-->
                                                <h5 class="fw-bold mb-0">Total:</h5>
                                                <h5 class="fw-bold mb-0">
                                                    <asp:Label ID="cartTotal" runat="server" Text=""></asp:Label>
                                                </h5>
                                                <!--Display the total cost of cart items-->
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="p-3" style="background-color: #f7f1e8;">
                                                <h3 class="text-center fw-bold text-uppercase p-3" style="color: black;">Payment
                                                </h3>
                                                <div class="row">
                                                    <div class="col">
                                                        <label>Name</label>
                                                        <div class="form-group">
                                                            <asp:TextBox CssClass="form-control" ID="TextBox9" runat="server" placeholder="Name"></asp:TextBox>
                                                        </div>
                                                        <label>Email</label>
                                                        <div class="form-group">
                                                            <asp:TextBox CssClass="form-control" ID="TextBox10" runat="server" placeholder="Email"></asp:TextBox>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6">
                                                                <label>Contact No</label>
                                                                <div class="form-group">
                                                                    <asp:TextBox CssClass="form-control" ID="TextBox11" runat="server" placeholder="Contact No"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6">
                                                                <label>Date</label>
                                                                <div class="form-group">
                                                                    <asp:TextBox CssClass="form-control" ID="TextBox12" runat="server" placeholder="Date"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <p class="p-3">
                                                            Lorem ipsum dolor sit amet consectetur, adipisicing elit <a
                                                                href="#!">obcaecati sapiente</a>.
                                                        </p>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <asp:Button ID="SubmitPaymentButton" runat="server" Text="Pay Now" CssClass="btn btn-primary btn1" OnClick="SubmitPaymentButton_Click" />
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Button class="btn btn-primary btn1" ID="Button2" runat="server" Text="Place Order" Onclick="Button2_Click"/>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <h5 class="fw-bold p-3">
                                                    <a href="index.aspx"><i class="fas fa-angle-left me-2"></i>Back to shopping</a>
                                                </h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
        </div>
    </form>
</body>
</html>

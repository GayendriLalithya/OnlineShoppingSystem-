<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminprofile.aspx.cs" Inherits="OZQ_gayendri.adminprofile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="fontawesome/css/all.css" rel="stylesheet" />
    <link href="css/style2.css" rel="stylesheet" />
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <style>
        /* Style the side menu */
        .side-menu {
            min-height: 100vh; /* Minimum height to cover the full viewport */
        }

        .bg-dark hr {
            margin-bottom: 6rem !important;
        }

        .bg-dark {
            background-color: black !important;
        }

        h3 {
            font-size: 30px;
        }

        .container {
            align-items: center;
        }

        .card {
            margin-top: 30px;
            margin-left: 40px !important;
            margin-right: 40px !important;
        }

        .btn {
            width: 100px;
            /*float: right;*/
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <div class="container-fluid">
                <div class="row flex-nowrap ">
                    <div class="bg-dark col-auto col-md-4 col-lg-3 p-0 d-flex flex-column justify-content-between">
                        <div class="bg-dark side-menu m-0 p-0">
                            <a class="d-flex text-decoration-none mt-1 p-4 align-items-center text-white" style="font-size: 25px;">
                                <i class="fa-solid fa-gauge-high p-3"></i><span class="fs-4 d-none d-sm-inline">Admin
                                    Dashboard</span>
                            </a>
                            <ul class="nav nav-pills flex-column mt-4 p-3">
                                <li class="nav-item py-2 py-sm">
                                    <a href="manageproducts.aspx" class="nav-link text-white">
                                        <i class="fa-brands fa-product-hunt"></i><span class="fs-4 ms-3 d-none d-sm-inline">
                                            Manage Products</span>
                                    </a>
                                </li>
                                <li class="nav-item py-2 py-sm">
                                    <a href="category.aspx" class="nav-link text-white">
                                        <i class="fa-solid fa-list-check"></i><span class="fs-4 ms-3 d-none d-sm-inline">Manage
                                            Categories</span>
                                    </a>
                                </li>
                                <li class="nav-item py-2 py-sm">
                                    <a href="manageuser.aspx" class="nav-link text-white">
                                        <i class="fa-solid fa-users"></i><span class="fs-4 ms-3 d-none d-sm-inline">Manage Users</span>
                                    </a>
                                </li>
                            </ul>
                            <hr />
                            <div class="mt-auto p-4">
                                <ul class="nav nav-pills flex-column">
                                    <li class="nav-item py-2 py-sm">
                                        <a href="index.aspx" class="nav-link text-white">
                                            <i class="fa-solid fa-house"></i><span class="fs-4 ms-3 d-none d-sm-inline">Home</span>
                                        </a>
                                    </li>
                                    <li class="nav-item py-2 py-sm">
                                        <a href="adminprofile.aspx" class="nav-link text-white">
                                            <i class="fa-solid fa-user"></i><span class="fs-4 ms-3 d-none d-sm-inline">My Profile</span>
                                        </a>
                                    </li>
                                    <li class="nav-item py-2 py-sm">
                                        <li class="nav-item py-2 py-sm">
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="nav-link text-white"
                                                OnClick="LinkButton1_Click"> <i class="fa-solid fa-power-off"></i> Logout</asp:LinkButton>
                                        </li>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-8 col-lg-9">

                        <div class="card" style="margin-top: 10px;">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col">
                                        <center>
                                            <img width="100px" src="img/login.png" />
                                            <!-- fetch the image form the database user table -->
                                        </center>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col">
                                        <center>
                                            <h3>My Profile</h3>
                                            <span>Account Status </span>
                                            <asp:Label class="badge badge-pill badge-success" ID="Label1" runat="server" Text="Active"></asp:Label>
                                        </center>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col">
                                        <center>
                                            <hr />
                                        </center>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Name</label>
                                        <div class="form-group">
                                            <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Name"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Contact No</label>
                                        <div class="form-group">
                                            <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Contact No"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <label>Address</label>
                                <div class="row">
                                    <div class="col-lg-4 col-md-6">
                                        <label>Street</label>
                                        <div class="form-group">
                                            <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="Street"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-6">
                                        <label>City</label>
                                        <div class="form-group">
                                            <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" placeholder="City"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-12">
                                        <label>Postal Code</label>
                                        <div class="form-group">
                                            <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" placeholder="Postal Code"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group p-3">
                                    <asp:Button class="btn btn-dark btn-block btn-lg" ID="Button1" runat="server"
                                        Text="Update" OnClick="Button1_Click" />
                                </div>
                                <div class="row">
                                    <div class="col-lg-4 col-md-6">
                                        <label>Email</label>
                                        <div class="form-group">
                                            <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server" placeholder="Email"
                                                ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-6">
                                        <label>Current Password</label>
                                        <div class="form-group">
                                            <asp:TextBox CssClass="form-control" ID="TextBox7" runat="server" placeholder="Current Password"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-12">
                                        <label>New Password</label>
                                        <div class="form-group">
                                            <asp:TextBox CssClass="form-control" ID="TextBox8" runat="server" placeholder="New Password"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group p-3">
                                    <asp:Button class="btn btn-primary btn-block btn-lg" ID="Button2" runat="server"
                                        Text="Change Password" OnClick="Button2_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

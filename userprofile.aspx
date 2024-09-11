<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userprofile.aspx.cs" Inherits="OZQ_gayendri.userprofile" %>

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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="container-fluid">
                <div class="row" style="margin-top: 1px;">
                    <div class="col-md-8 mx-auto">
                        <div class="card p-3">
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
                            <a href="index.aspx"><< Back to Home</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <style>
        h1 {
            margin-top: 20px;
            font-size: 30px;
        }

        h3 {
            font-size: 20px;
        }

        .container {
            align-items: center;
        }

        a {
            text-decoration: none;
            color: #b80818;
        }

            a:hover {
                color: #850c17;
            }

        body {
            background: #f7f1e8;
        }

        .btn1 {
            background-color: #b80818 !important;
            border: none;
            margin-left: 43px;
            margin-bottom: 10px;
            width: 80%;
        }

            .btn1:hover {
                background-color: #850c17 !important;
            }

        .btn-primary {
            background-color: #b80818;
            border: none;
        }

            .btn-primary:hover {
                background-color: #850c17 !important;
            }
    </style>


</body>
</html>

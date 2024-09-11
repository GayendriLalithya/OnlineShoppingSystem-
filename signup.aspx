<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="OZQ_gayendri.signup" %>

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
            <div class="container">
        <div class="row" style="margin-top:1px;">
            <div class="col-md-6 mx-auto">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img width="100px" src="img/login.png" />
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>User Registration</h3>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <hr/>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>Name</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="Name"></asp:TextBox>
                                </div>

                                <div class="row">

                                    <label>Address</label>
                                    <div class="col-lg-4 col-md-6">
                                        <label>Street</label>
                                        <div class="form-group">
                                            <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" placeholder="Street"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-6">
                                        <label>City</label>
                                        <div class="form-group">
                                            <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" placeholder="City"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-6">
                                        <label>Postal Code</label>
                                        <div class="form-group">
                                            <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server" placeholder="Postal Code"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <label>Contact No</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox7" runat="server" placeholder="Contact No"></asp:TextBox>
                                </div>

                                <label>Email address</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Username"></asp:TextBox>
                                </div>

                                <label>Password</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                                </div>

                                <div class="g-recaptcha-custom-wrapper">
                                    <div class="g-recaptcha" data-sitekey="6LeIxAcTAAAAAJcZVRqyHh71UMIEGNQ_MXjiZKhI">
                                    </div>
                                </div>

                                <div class="form-group p-1">
                                    <asp:Button class="btn btn-login btn-block btn-lg" ID="Button1" runat="server" 
                                        Text="Sign up" OnClick="Button1_Click" />
                                </div>

                                <div class="form-group">
                                    <p>Already have an account <a href="signin.aspx">Login Now</a> </p>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

                <a href="index.aspx"><< Back to Home</a>
            </div>
        </div>
    </div>
        </div>

     <style>
        h3{
            font-size: 15px;
        }

        .container{
            align-items: center;
        }

        .btn-signup{
            color: white;
            border: #FF177C;
            background-color: #FF177C;
        }

        .btn-signup:hover{
            color: white;
            background-color: #b71259;
        }

        .btn-login{
            color: white;
            border: #FF177C;
            background-color: #b80818;
        }

        .btn-login:hover{
            color: white;
            background-color: #850c17;
        }
        a {
            text-decoration: none;
            color: #b80818;
        }
        a:hover{
            color: #850c17;
        }
        body {
            background: #f7f1e8;
    </style>

    </form>

    <script src="https://www.google.com/recaptcha/api.js" async defer></script>

     </body>
</html>

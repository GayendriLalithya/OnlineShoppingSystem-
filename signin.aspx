<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signin.aspx.cs" Inherits="OZQ_gayendri.signin" %>

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
                <div class="row" style="margin-top: 100px;">
                    <div class="col-md-6 mx-auto">
                        <div class="card">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col">
                                        <center>
                                            <img width="150px" src="img/login.png" />
                                        </center>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col">
                                        <center>
                                            <h3>User Login</h3>
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
                                    <div class="col">
                                        <label>Email address</label>
                                        <div class="form-group">
                                            <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Username"></asp:TextBox>
                                        </div>

                                        <label>Password</label>
                                        <div class="form-group">
                                            <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Password"
                                                TextMode="Password"></asp:TextBox>
                                        </div>

                                        <div class="g-recaptcha-custom-wrapper">
                                            <div class="g-recaptcha" data-sitekey="6LeIxAcTAAAAAJcZVRqyHh71UMIEGNQ_MXjiZKhI">
                                            </div>
                                        </div>

                                        <div class="form-group p-1">
                                            <asp:Button class="btn btn-login btn-block btn-lg" ID="Button1" runat="server"
                                                Text="Login" OnClick="Button1_Click" />
                                            <asp:Label ID="lblResult" runat="server"></asp:Label>
                                        </div>

                                        <div class="form-group">
                                            <p>Don't have an account <a href="signup.aspx">Sign Up Here</a> </p>
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

    </form>

    <style>
        h3 {
            font-size: 20px;
        }

        .container {
            align-items: center;
        }

        .btn-signup {
            color: white;
            border: #FF177C;
            background-color: #FF177C;
        }

            .btn-signup:hover {
                color: white;
                background-color: #b71259;
            }

        .btn-login {
            color: white;
            border: #FF177C;
            background-color: #b80818;
        }

            .btn-login:hover {
                color: white;
                background-color: #850c17;
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
    </style>

    <script src="https://www.google.com/recaptcha/api.js" async defer></script>

</body>
</html>

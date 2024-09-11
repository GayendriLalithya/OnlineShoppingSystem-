<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="manageuser.aspx.cs" Inherits="OZQ_gayendri.manageuser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- Bootstrap css -->
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Datatables css -->
    <link href="datatables/css/jquery.dataTables.min.css" rel="stylesheet" />
    <!-- Fontawesome css -->
    <link href="fontawesome/css/all.css" rel="stylesheet" />
    <!-- Our Custom css file -->
    <link href="css/style2.css" rel="stylesheet" />

    <script>
        $(document).ready(function () {
            $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
            //$('.table1').DataTable();
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="container-fluid">
                <div class="row flex-nowrap ">
                    <div class="bg-dark col-auto col-md-4 col-lg-3 p-0 d-flex flex-column justify-content-between">
                        <div class="bg-dark side-menu m-0 p-0">
                            <a class="d-flex text-decoration-none mt-1 p-4 align-items-center text-white" style="font-size: 25px;">
                                <i class="fa-solid fa-gauge-high p-3"></i> <span class="fs-4 d-none d-sm-inline">Admin Dashboard</span>
                            </a>
                            <ul class="nav nav-pills flex-column mt-4 p-3">
                                <li class="nav-item py-2 py-sm">
                                    <a href="manageproducts.aspx" class="nav-link text-white">
                                        <i class="fa-brands fa-product-hunt"></i> <span class="fs-4 ms-3 d-none d-sm-inline">Manage Products</span>
                                    </a>
                                </li>
                                <li class="nav-item py-2 py-sm">
                                    <a href="category.aspx" class="nav-link text-white">
                                        <i class="fa-solid fa-list-check"></i> <span class="fs-4 ms-3 d-none d-sm-inline">Manage Categories</span>
                                    </a>
                                </li>
                                <li class="nav-item py-2 py-sm">
                                    <a href="manageuser.aspx" class="nav-link text-white">
                                        <i class="fa-solid fa-users"></i> <span class="fs-4 ms-3 d-none d-sm-inline">Manage Users</span>
                                    </a>
                                </li>
                            </ul>
                            <hr />
                                <div class="mt-auto p-4">
                                    <ul class="nav nav-pills flex-column">
                                        <li class="nav-item py-2 py-sm">
                                            <a href="index.aspx" class="nav-link text-white">
                                                <i class="fa-solid fa-house"></i> <span class="fs-4 ms-3 d-none d-sm-inline">Home</span>
                                            </a>
                                        </li>
                                        <li class="nav-item py-2 py-sm">
                                            <a href="adminprofile.aspx" class="nav-link text-white">
                                                <i class="fa-solid fa-user"></i> <span class="fs-4 ms-3 d-none d-sm-inline">My Profile</span>
                                            </a>
                                        </li>
                                        <li class="nav-item py-2 py-sm">
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="nav-link text-white" 
                                                OnClick="LinkButton1_Click"> <i class="fa-solid fa-power-off"></i> Logout</asp:LinkButton>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    <div class="col-md-8 col-lg-9">
                        
                        <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>Manage Users</h3>
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
                       <div class="container-fluid  main-section">
                          <div class="container">
                              <div class="row">
                              <div class="col-md-6">
                                <label>User ID</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="User ID"></asp:TextBox>
                                </div>
                              </div>
                              <div class="col-md-6">
                                <label>User Type</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="User Type"></asp:TextBox> 
                                </div>
                              </div>
                            </div>
                            <div class="row">
                              <div class="col-md-6">
                                <label>Name</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="Name"></asp:TextBox>
                                </div>
                              </div>
                              <div class="col-md-6">
                                <label>Contact No</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" placeholder="Contact No"></asp:TextBox>
                                </div>
                              </div>
                            </div>
                              <label>Address</label>
                            <div class="row">
                              <div class="col-lg-4 col-md-6">
                                <label>Street</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" placeholder="Street"></asp:TextBox>
                                </div>
                              </div>
                              <div class="col-lg-4 col-md-6">
                                <label>City</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server" placeholder="City"></asp:TextBox>
                                </div>
                              </div>
                              <div class="col-lg-4 col-md-12">
                                <label>Postal Code</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox7" runat="server" placeholder="Postal Code"></asp:TextBox>
                                </div>
                              </div>
                            </div>
                            <div class="row">
                              <div class="col-md-6">
                                <label>Email address</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox8" runat="server" placeholder="Username"></asp:TextBox>
                                </div>
                              </div>
                              <div class="col-md-6">
                                <label>Password</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox9" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                                </div>
                              </div>
                            </div>
                          </div>
                        </div>
                        <div class="form-group col-md-8">
                            <div class="row">
                                <div class="col-lg-3 col-md-6">
                                    <div class="form-group p-3">
                                        <asp:Button CssClass="btn btn-secondary" ID="Button1" runat="server" Text="Add" 
                                            OnClick="Button1_Click" />
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-6">
                                    <div class="form-group p-3">
                                        <asp:Button CssClass="btn btn-success" ID="Button2" runat="server" 
                                            Text="Update" OnClick="Button2_Click" />
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-6">
                                    <div class="form-group p-3">
                                        <asp:Button CssClass="btn btn-danger" ID="Button3" runat="server" Text="Delete" 
                                            OnClick="Button3_Click" />
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-6">
                                    <div class="form-group p-3">
                                        <asp:Button CssClass="btn btn-info" ID="Button4" runat="server" Text="Search" 
                                            OnClick="Button4_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    </div>
                        <div class="row">
                            <div class="container-fluid">
                                <div class="card">
                                    <div style="margin: 20px;">
                                        <center>
                                            <h5>View All Categories</h5>
                                        </center>
                                    </div>
                                    <div class="row">
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:ozqDBConnectionString3 %>" 
                                            ProviderName="<%$ ConnectionStrings:ozqDBConnectionString3.ProviderName %>" 
                                            SelectCommand="SELECT * FROM [Users]"></asp:SqlDataSource>
                                        <div class="col">
                                            <hr />
                                        </div>
                                    </div>
                                    <asp:ScriptManager ID="ScriptManager1" runat="server" />
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col">
                                                    <asp:GridView class="table table-striped table-bordered" ID="GridView1" 
                                            runat="server" AutoGenerateColumns="False" DataKeyNames="UserID" 
                                            DataSourceID="SqlDataSource1">
                                                        <Columns>
                                                            <asp:BoundField DataField="UserID" HeaderText="User ID" ReadOnly="True" SortExpression="UserID" />
                                                            <asp:BoundField DataField="UserType" HeaderText="User Type" SortExpression="UserType" />
                                                            <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName" />
                                                            <asp:BoundField DataField="Street" HeaderText="Street" SortExpression="Street" />
                                                            <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
                                                            <asp:BoundField DataField="PostalCode" HeaderText="PostalCode" SortExpression="PostalCode" />
                                                            <asp:BoundField DataField="ContactNo" HeaderText="ContactNo" SortExpression="ContactNo" />
                                                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                         </ContentTemplate>
                                    </asp:UpdatePanel>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="manageproducts.aspx.cs" Inherits="OZQ_gayendri.manageproducts" %>

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

    <script type="text/javascript">
    function displaySelectedImage() {
        var fileInput = document.getElementById('FileUpload1');
        var imagePreview = document.getElementById('Image1');

        if (fileInput.files && fileInput.files[0]) {
            var reader = new FileReader();

            reader.onload = function(e) {
                imagePreview.src = e.target.result;
            };

            reader.readAsDataURL(fileInput.files[0]);
        } else {
            imagePreview.src = 'img/default-image.jpg'; // Provide a default image source
        }
    }
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


                     <ItemTemplate>   
                        <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>Manage Products</h3>
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
                            <div class="col-md-5">
                                 <div class="card">
                                     <asp:Image ID="Image1" runat="server" class="card-img-top" alt="" />
                                </div>
                                <div class="card">
                                    <asp:FileUpload CssClass="form-control" ID="FileUpload1" runat="server" onchange="displaySelectedImage();"/>
                                </div>
                                <div class="card" style="border: none;">
                                    <label>Price $</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" placeholder="Price"></asp:TextBox>
                                    </div>

                                    <label>Stock Quantity</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" placeholder="Stock Quantity"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="col-md-6">
                                <label>Product ID</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Product ID"></asp:TextBox>
                                </div>
                                <label>Product Name</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Product Name"></asp:TextBox>
                                </div>

                                <label>Category</label>
                                <div class="form-group">
                                    <asp:DropDownList CssClass="form-control" ID="DropDownList1" runat="server"></asp:DropDownList>
                                </div>

                                <label>Description</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="Description" TextMode="MultiLine"></asp:TextBox>
                                </div>

                                </ItemTemplate>

                                <div class="form-group">
                                    <div class="row" style="margin-top: 120px;">
                                        <div class="col-lg-3 col-md-6">
                                            <div class="form-group p-3">
                                                <asp:Button CssClass="btn btn-secondary" ID="Button1" runat="server" Text="Add" OnClick="Button1_Click" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-6">
                                            <div class="form-group p-3">
                                                <asp:Button CssClass="btn btn-success" ID="Button2" runat="server" Text="Update" OnClick="Button2_Click" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-6">
                                            <div class="form-group p-3">
                                                <asp:Button CssClass="btn btn-danger" ID="Button3" runat="server" Text="Delete" OnClick="Button3_Click" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-6">
                                            <div class="form-group p-3">
                                                <asp:Button CssClass="btn btn-info" ID="Button4" runat="server" Text="Search" OnClick="Button4_Click" />
                                            </div>
                                        </div>
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

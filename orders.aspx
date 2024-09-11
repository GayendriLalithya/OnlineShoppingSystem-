<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orders.aspx.cs" Inherits="OZQ_gayendri.orders" %>

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
    <!-- Custom Css file -->
    <link href="css/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="container">
                <div class="row" style="margin-top: 1px;">
                    <div class="col-md-6 mx-auto p-3">
                        <div class="card">
                            <div class="card-body">
                                <div class="row">
                                </div>
                                <div class="row">
                                    <div class="col">
                                        <center>
                                            <h3>My Orders</h3>
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
                                <asp:ScriptManager ID="ScriptManager1" runat="server" />
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <!--Sql data source here-->
                                            <div class="col">
                                                <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                                    ConnectionString="<%$ ConnectionStrings:ozqDBConnectionString %>"
                                                    SelectCommand="SELECT * FROM [Orders] WHERE ([UserID] = @UserID)">
                                                    <SelectParameters>
                                                        <asp:QueryStringParameter Name="UserID" QueryStringField="userId"
                                                            Type="Int32" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                                <asp:GridView class="table table-striped table-bordered" ID="GridView1"
                                                    runat="server" AutoGenerateColumns="False" DataKeyNames="OrderID"
                                                    DataSourceID="SqlDataSource2">
                                                    <Columns>
                                                        <asp:BoundField DataField="OrderID" HeaderText="OrderID" ReadOnly="True"
                                                            SortExpression="OrderID" InsertVisible="False" />
                                                        <asp:BoundField DataField="UserID" HeaderText="UserID"
                                                            SortExpression="UserID" />
                                                        <asp:BoundField DataField="ProductID" HeaderText="ProductID"
                                                            SortExpression="ProductID" />
                                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity"
                                                            SortExpression="Quantity" />
                                                        <asp:BoundField DataField="OrderDate" HeaderText="OrderDate"
                                                            SortExpression="OrderDate" />
                                                        <asp:BoundField DataField="Status" HeaderText="Status"
                                                            SortExpression="Status" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="row">
                                <div class="col-md-8 col-lg-6">
                                    <div class="form-group" style="float: right;">
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-group" placeholder="Type the Order ID Here"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4 col-lg-6">
                                    <div class="form-group">
                                        <asp:Button ID="Button1" runat="server" Text="Cancel Order" CssClass="btn btn-danger btn-sm"
                                            OnClick="Button1_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <a href="index.aspx"><< Back to Home</a>
                    </div>
                </div>
            </div>
    </form>
</body>
</html>

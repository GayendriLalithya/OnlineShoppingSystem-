<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="comments.aspx.cs" Inherits="OZQ_gayendri.comments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid">
        <div class="container-fluid  main-section p-3">
            <div class="p-5" style="margin-top: 60px;">
                <div class="container">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="container-fluid">
                                <div class="row flex flex-wrap product-grid-main-wrap">
                                    <div class="col-md-10 mx-auto">
                                        <div class="card">
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-md-10">
                                                        <center>
                                                            <h1 style="color: black;"><asp:Label ID="ProductNameLabel" runat="server"></asp:Label></h1>
                                                        </center>
                                                    </div>
                                                    <div class="col-md-2 p-4 mx-auto">
                                                        <asp:Button ID="Button1" runat="server" Text="&#xf015;" CssClass="btn btn-dark fa" OnClick="Button1_Click"/>
                                                    </div>
                                                </div>
                                                <hr />
                                                <asp:Repeater ID="ProductRepeater" runat="server">
                                                    <ItemTemplate>
                                                        <div class="flex-grow-1 ms-3">
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <h6 class="fw-bold mb-0 me-5 pe-3" style="color: #9e9e9e;"><%# Eval("Email") %></h6>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <h6 style="color: #9e9e9e; float: right;"><%# Eval("Date", "{0:MM/dd/yyyy}") %></h6>
                                                                </div>
                                                                <div class="row">
                                                                    <p style="color: #000;"><%# Eval("FeedbackText") %></p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
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

         .btn-dark {
             color: white;
         }

             .btn-dark:hover {
                 color: white;
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

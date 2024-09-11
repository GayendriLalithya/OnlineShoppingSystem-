<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cancel.aspx.cs" Inherits="OZQ_gayendri.PayPal.Cancel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/css/bootstrap.min.css"
        rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <div class="vh-100 d-flex justify-content-center align-items-center">
                <div class="col-md-4">
                    <div class="border border-3 border-danger"></div>
                    <div class="card bg-white shadow p-5">
                        <div class="mb-4 text-center">
                            <svg xmlns="http://www.w3.org/2000/svg" class="text-danger" width="75" height="75"
                                fill="currentColor" class="bi bi-x-circle" viewBox="0 0 16 16">
                                <path
                                    d="M8 0a8 8 0 1 1 0 16A8 8 0 0 1 8 0zm-.354 4.646a.5.5 0 0 0-.292.293L6.293 8l-2.647 2.647a.5.5 0 1 0 .708.708L8 8.707l2.647 2.647a.5.5 0 0 0 .708-.708L9.293 8l2.647-2.647a.5.5 0 0 0-.708-.708L8 7.293 5.353 4.646a.5.5 0 0 0-.708 0z" />
                            </svg>
                        </div>
                        <div class="text-center">
                            <h1>Payment Canceled</h1>
                            <p>Your payment was canceled. Would you like to  </p>
                            <a class="btn btn-outline-danger" href="https://localhost:44373/index.aspx">Back Home</a>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </form>

    <style>
        body {
            background: #f7f1e8;
        }
    </style>

</body>
</html>

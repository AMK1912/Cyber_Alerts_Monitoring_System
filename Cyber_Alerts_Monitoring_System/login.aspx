<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CyberAlertsWebApp.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
</head>
<body>
    <form id="form1" runat="server" class="container">
        <h2 class="mt-5">Login</h2>
        <div class="form-group">
            <label for="txtEmpCode">Employee Code:</label>
            <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtPassword">Password:</label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
        </div>
        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary" OnClick="btnLogin_Click" />
        <asp:Label ID="lblErrorMessage" runat="server" CssClass="text-danger"></asp:Label>
    </form>
</body>
</html>

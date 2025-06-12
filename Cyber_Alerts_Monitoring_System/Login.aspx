<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container d-flex justify-content-center mt-5">
        <div class="card" style="width: 25rem;">
            <div class="card-header text-center">
                <h2 class="mb-0">Login</h2>
            </div>
            <div class="card-body">
                <p class="text-center">Please Sign In on the website.</p>
                <asp:Label ID="message" runat="server" Text="" CssClass="d-block text-center mb-3 fw-bold"
                    Font-Italic="true" Font-Names="Arial" Font-Size="12px" ForeColor="Brown"></asp:Label>

                <div class="mb-3">
                    <asp:Label ID="Label23" runat="server" Text="SAIL PNO :" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtusername" runat="server" onkeyup="return toUpper(this.id)"
                        Width="100%" MaxLength="7" TabIndex="1" AutoCompleteType="Disabled"
                        oncopy="return false" onpaste="return false" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="mb-4">
                    <asp:Label ID="Label24" runat="server" Text="PASSWORD :" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtpassword" runat="server" Width="100%" TextMode="Password"
                        TabIndex="2" MaxLength="20" OnTextChanged="btnlogin_Click"
                        AutoCompleteType="Disabled" oncopy="return false" onpaste="return false" CssClass="form-control"></asp:TextBox>
                </div>

                <asp:Button ID="btnlogin" runat="server" OnClick="btnlogin_Click"
                    Text="Sign In" TabIndex="3" CssClass="btn btn-primary w-100"></asp:Button>
            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2 class="mt-5">Login</h2>
        <p>Please Sign In on the website.</p>
        <table style="border:1px solid gray" >
        <tr>
        <td colspan="2" style="height:40px">
            Login</td>
        </tr>
        <tr>
            <td style="height: 30px; text-align: right;"  >
            <asp:Label ID="Label23" runat="server" Text="SAIL PNO :" Font-Bold="False" Font-Names="Arial" Font-Size="12px" ForeColor="Navy"></asp:Label> &nbsp;
        </td>
            <td align="left" style="font-weight: bold; font-size: 12px; color: navy; font-family: Arial;  height: 30px;">
            <asp:TextBox ID="txtusername" runat="server" onkeyup="return toUpper(this.id)"  Width="133px" MaxLength="7" TabIndex="1" AutoCompleteType="Disabled" oncopy="return false" onpaste="return false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="height: 30px; text-align: right;"  >
            <asp:Label ID="Label24" runat="server" Text="PASSWORD :" Font-Bold="False" Font-Names="Arial" Font-Size="12px" ForeColor="Navy"></asp:Label> &nbsp; 
        </td> 
        <td align="left" style="font-weight: bold; font-size: 12px; color: navy; font-family: Arial;  ">
        <asp:TextBox ID="txtpassword" runat="server" Width="132px" TextMode="Password" TabIndex="2" MaxLength="20" OnTextChanged="btnlogin_Click" AutoCompleteType="Disabled" oncopy="return false" onpaste="return false"></asp:TextBox>
        &nbsp;&nbsp;</td>
        </tr>
            <tr>
                <td style="font-size: 12px; color: navy; font-family: Arial;" colspan="2" valign="top">
                <asp:UpdatePanel ID="UP1" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td style="width: 100px; " valign="top" align="right">
                                        &nbsp;
                                    </td>
                                    <td style="width: 100px;" valign="top">
                                        </td>
                                    <td  valign="top">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>  </td>
            </tr>
            <tr>
            <td colspan="2" valign="top">
                <table>
                    <tr>
                    <td style="font-size: 12px; color: navy; font-family: Arial;">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    </tr>
                    </table>
            </td>
            </tr>
        <tr>
            <td style="height: 25px" >
                </td>
                <td align="left" style="font-weight: bold; font-size: 14px;   color: navy;
                    font-family: Arial">
        <asp:Button ID="btnlogin" runat="server" BorderStyle="Outset" Font-Bold="False" Font-Names="Arial"
            Font-Size="14px" ForeColor="DarkBlue" OnClick="btnlogin_Click"
            Text="Sign In" Width="72px" TabIndex="3" Height="25px" /></td>
        </tr>
            <tr>
            <td colspan="2"  align="center">
            <asp:Label ID="message" runat="server" Text="" Font-Italic="true" Width="329px" Font-Names="Arial" Font-Size="12px" ForeColor="Brown" Font-Bold="True" Height="28px"></asp:Label></td>
            </tr>
        </table>
    </div>
</asp:Content>

<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CyberAlert._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Welcome, <asp:Label ID="lblWelcome" runat="server"></asp:Label></h1>
        <p class="lead">This is the home page of your Cyber Alerts Monitor application.</p>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CyberAlertsLocal.aspx.cs" Inherits="CyberAlertsWebApp.CyberAlertsLocal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2 class="mt-5">Cyber Alerts - Plant/Unit</h2>
        <p>Please fill out the form below to report a new cyber alert for your Plant/Unit.</p>

        <form id="cyberAlertFormLocal" runat="server" class="mt-4">
            <div class="form-group">
                <label for="txtAlertTitleLocal">Alert Title:</label>
                <asp:TextBox ID="txtAlertTitleLocal" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtDescriptionLocal">Description:</label>
                <asp:TextBox ID="txtDescriptionLocal" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="ddlSeverityLocal">Severity Level:</label>
                <asp:DropDownList ID="ddlSeverityLocal" runat="server" CssClass="form-control">
                    <asp:ListItem Value="High">High</asp:ListItem>
                    <asp:ListItem Value="Medium">Medium</asp:ListItem>
                    <asp:ListItem Value="Low">Low</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="txtAffectedSystemsLocal">Affected System(s):</label>
                <asp:TextBox ID="txtAffectedSystemsLocal" runat="server" CssClass="form-control"></asp:TextBox>
                <small class="form-text text-muted">Separate multiple systems with commas if needed.</small>
            </div>

            <div class="form-group">
                <label for="txtReporterLocal">Reporter:</label>
                <asp:TextBox ID="txtReporterLocal" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtAlertDateTimeLocal">Date/Time of Alert:</label>
                <asp:TextBox ID="txtAlertDateTimeLocal" runat="server" TextMode="DateTimeLocal" CssClass="form-control"></asp:TextBox>
            </div>

            <asp:Button ID="btnSubmitAlertLocal" runat="server" Text="Submit Alert" CssClass="btn btn-primary" OnClick="btnSubmitAlertLocal_Click" />
            <asp:Label ID="lblSubmissionMessageLocal" runat="server" CssClass="text-success mt-2"></asp:Label>
        </form>
    </div>
</asp:Content>

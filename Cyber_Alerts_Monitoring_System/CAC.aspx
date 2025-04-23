<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CyberAlertsCentral.aspx.cs" Inherits="CyberAlertsWebApp.CyberAlertsCentral" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2 class="mt-5">Cyber Alerts - Corporate Office</h2>
        <p>Please fill out the form below to report a new cyber alert.</p>

        <form id="cyberAlertForm" runat="server" class="mt-4">
            <div class="form-group">
                <label for="txtAlertTitle">Alert Title:</label>
                <asp:TextBox ID="txtAlertTitle" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtDescription">Description:</label>
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="ddlSeverity">Severity Level:</label>
                <asp:DropDownList ID="ddlSeverity" runat="server" CssClass="form-control">
                    <asp:ListItem Value="High">High</asp:ListItem>
                    <asp:ListItem Value="Medium">Medium</asp:ListItem>
                    <asp:ListItem Value="Low">Low</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="txtAffectedSystems">Affected System(s):</label>
                <asp:TextBox ID="txtAffectedSystems" runat="server" CssClass="form-control"></asp:TextBox>
                <small class="form-text text-muted">Separate multiple systems with commas if needed.</small>
            </div>

            <div class="form-group">
                <label for="txtReporter">Reporter:</label>
                <asp:TextBox ID="txtReporter" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtAlertDateTime">Date/Time of Alert:</label>
                <asp:TextBox ID="txtAlertDateTime" runat="server" TextMode="DateTimeLocal" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtReceivedDateCentral">Received Date:</label>
                <asp:TextBox ID="txtReceivedDateCentral" runat="server" TextMode="DateTimeLocal" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtSenderDetailsCentral">Sender Details:</label>
                <asp:TextBox ID="txtSenderDetailsCentral" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtIncidentDateCentral">Incident Date:</label>
                <asp:TextBox ID="txtIncidentDateCentral" runat="server" TextMode="DateTimeLocal" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtEntryDateCentral">Entry Date:</label>
                <asp:TextBox ID="txtEntryDateCentral" runat="server" TextMode="DateTimeLocal" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtEmailDateCentral">Email Date:</label>
                <asp:TextBox ID="txtEmailDateCentral" runat="server" TextMode="DateTimeLocal" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtPertainingToUnitCentral">Pertaining To Unit:</label>
                <asp:TextBox ID="txtPertainingToUnitCentral" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtAffectedSailIPCentral">Affected SAIL IP:</label>
                <asp:TextBox ID="txtAffectedSailIPCentral" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtAffectedPortCentral">Affected Port:</label>
                <asp:TextBox ID="txtAffectedPortCentral" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtMaliciousIPCentral">Malicious IP:</label>
                <asp:TextBox ID="txtMaliciousIPCentral" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtAlertDetailsCentral">Alert Details:</label>
                <asp:TextBox ID="txtAlertDetailsCentral" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" MaxLength="250"></asp:TextBox>
            </div>

            <asp:Button ID="btnSubmitAlert" runat="server" Text="Submit Alert" CssClass="btn btn-primary" OnClick="btnSubmitAlert_Click" />
            <asp:Label ID="lblSubmissionMessage" runat="server" CssClass="text-success mt-2"></asp:Label>
        </form>
    </div>
</asp:Content>

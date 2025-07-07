<%@ Page Title="Cyber Alerts Form" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CyberAlertsCentral.aspx.cs" Inherits="CyberAlert.CyberAlertsCentral" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2 class="mt-5">Cyber Alerts - Corporate Office</h2>
        <p>Please fill out the form below to report a new cyber alert.</p>

        <form id="cyberAlertForm" runat="server" class="mt-4">
            <div class="row mb-3">
                <div class="col-md-6">
                    <label for="txtAlertDateTime" class="form-label">Date/Time of Alert:</label>
                    <asp:TextBox ID="txtAlertDateTime" runat="server" TextMode="DateTimeLocal"
                        CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                        ControlToValidate="txtAlertDateTime" ErrorMessage="Alert Date/Time is required."
                        CssClass="text-danger"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="AlertDateTimeValidator" runat="server" ControlToValidate="txtAlertDateTime"
                        Type="Date" Operator="DataTypeCheck" ErrorMessage="Invalid Alert Date/Time format."
                        CssClass="text-danger"></asp:CompareValidator>
                </div>
                <div class="col-md-6">
                    <label for="txtReceivedDateCentral" class="form-label">Received Date:</label>
                    <asp:TextBox ID="txtReceivedDateCentral" runat="server" TextMode="DateTimeLocal"
                        CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtReceivedDateCentral" ErrorMessage="Received Date is required."
                        CssClass="text-danger"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="ReceivedDateValidator" runat="server" ControlToValidate="txtReceivedDateCentral"
                        Type="Date" Operator="DataTypeCheck" ErrorMessage="Invalid Received Date format."
                        CssClass="text-danger"></asp:CompareValidator>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <label for="txtCentreUnit" class="form-label">Centre Unit:</label>
                    <asp:TextBox ID="txtCentreUnit" runat="server"
                        CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReceivedDateRequired" runat="server"
                        ControlToValidate="txtCentreUnit" ErrorMessage="Centre Unit is required"
                        CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-6">
                    <label for="txtSenderDetailsCentral" class="form-label">Sender Details:</label>
                    <asp:TextBox ID="txtSenderDetailsCentral" runat="server" CssClass="form-control"
                        MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                        ControlToValidate="txtSenderDetailsCentral" ErrorMessage="Sender Details Required"
                        CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <label for="txtIncidentDateCentral" class="form-label">Incident Date:</label>
                    <asp:TextBox ID="txtIncidentDateCentral" runat="server" TextMode="DateTimeLocal"
                        CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="IncidentDateRequired" runat="server"
                        ControlToValidate="txtIncidentDateCentral" ErrorMessage="Incident Date is required"
                        CssClass="text-danger"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="IncidentDateValidator" runat="server" ControlToValidate="txtIncidentDateCentral"
                        Type="Date" Operator="DataTypeCheck" ErrorMessage="Invalid Incident Date format."
                        CssClass="text-danger"></asp:CompareValidator>
                </div>
                <div class="col-md-6">
                    <label for="txtEntryDateCentral" class="form-label">Entry Date:</label>
                    <asp:TextBox ID="txtEntryDateCentral" runat="server" TextMode="DateTimeLocal"
                        CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="EntryDateRequired" runat="server"
                        ControlToValidate="txtEntryDateCentral" ErrorMessage="Entry Date is required"
                        CssClass="text-danger"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="EntryDateValidator" runat="server" ControlToValidate="txtEntryDateCentral"
                        Type="Date" Operator="DataTypeCheck" ErrorMessage="Invalid Entry Date format."
                        CssClass="text-danger"></asp:CompareValidator>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <label for="txtEmailDateCentral" class="form-label">Email Date:</label>
                    <asp:TextBox ID="txtEmailDateCentral" runat="server" TextMode="DateTimeLocal"
                        CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="EmailDateRequired" runat="server"
                        ControlToValidate="txtEmailDateCentral" ErrorMessage="Email Date is required"
                        CssClass="text-danger"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="EmailDateValidator" runat="server" ControlToValidate="txtEmailDateCentral"
                        Type="Date" Operator="DataTypeCheck" ErrorMessage="Invalid Email Date format."
                        CssClass="text-danger"></asp:CompareValidator>
                </div>
                <div class="col-md-6">
                    <label for="ddlPertainingToUnit" class="form-label">Pertaining To Unit:</label>
                    <%-- Replaced TextBox with DropDownList --%>
                    <asp:DropDownList ID="ddlPertainingToUnit" runat="server" CssClass="form-select"></asp:DropDownList>
                    <%-- Updated Validator to point to the new DropDownList and use InitialValue --%>
                    <asp:RequiredFieldValidator ID="PertainingToUnitRequired" runat="server"
                        ControlToValidate="ddlPertainingToUnit"
                        ErrorMessage="Pertaining To Unit is required" CssClass="text-danger"
                        InitialValue=""></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <label for="txtAffectedSailIPCentral" class="form-label">Affected SAIL IP:</label>
                    <asp:TextBox ID="txtAffectedSailIPCentral" runat="server" TextMode="SingleLine"
                        CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="SailIPIsRequired" runat="server"
                        ControlToValidate="txtAffectedSailIPCentral"
                        ErrorMessage="Affected IP is required" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-6">
                    <label for="txtAffectedPortCentral" class="form-label">Affected Port:</label>
                    <asp:TextBox ID="txtAffectedPortCentral" runat="server" TextMode="SingleLine"
                        CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PortIsRequired" runat="server"
                        ControlToValidate="txtAffectedPortCentral" ErrorMessage="Port is required"
                        CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="mb-3">
                <label for="txtMaliciousIPCentral" class="form-label">Malicious IP:</label>
                <asp:TextBox ID="txtMaliciousIPCentral" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="MaliciousIPIsRequired" runat="server"
                    ControlToValidate="txtMaliciousIPCentral" ErrorMessage="Malicious IP is required"
                    CssClass="text-danger"></asp:RequiredFieldValidator>
            </div>

            <div class="mb-3">
                <label for="txtAlertDetailsCentral" class="form-label">Alert Details:</label>
                <asp:TextBox ID="txtAlertDetailsCentral" runat="server" TextMode="MultiLine"
                    Rows="4" CssClass="form-control" MaxLength="250"></asp:TextBox>
                <asp:RequiredFieldValidator ID="AlertDetailsRequired" runat="server"
                    ControlToValidate="txtAlertDetailsCentral" ErrorMessage="Alert Details is required"
                    CssClass="text-danger"></asp:RequiredFieldValidator>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <label for="txtActionDateCentral" class="form-label">Action Date: </label>
                    <asp:TextBox ID="txtActionDateCentral" runat="server" TextMode="DateTimeLocal"
                        CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                        ControlToValidate="txtActionDateCentral" ErrorMessage="Action Date is required."
                        CssClass="text-danger"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="ActionDateValidator" runat="server" ControlToValidate="txtActionDateCentral"
                        Type="Date" Operator="DataTypeCheck" ErrorMessage="Invalid Action Date format."
                        CssClass="text-danger"></asp:CompareValidator>
                </div>
                <div class="col-md-6">
                    <label for="txtClosingDateCentral" class="form-label">Closing Date: </label>
                    <asp:TextBox ID="txtClosingDateCentral" runat="server" TextMode="DateTimeLocal"
                        CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                        ControlToValidate="txtClosingDateCentral" ErrorMessage="Closing Date is required."
                        CssClass="text-danger"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="ClosingDateValidator" runat="server" ControlToValidate="txtClosingDateCentral"
                        Type="Date" Operator="DataTypeCheck" ErrorMessage="Invalid Closing Date format."
                        CssClass="text-danger"></asp:CompareValidator>
                </div>
            </div>

            <div class="mb-3">
                <label for="txtActionDetails" class="form-label">Action Details:</label>
                <asp:TextBox ID="txtActionDetails" runat="server" TextMode="MultiLine"
                    Rows="4" CssClass="form-control" MaxLength="250"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                    ControlToValidate="txtActionDetails" ErrorMessage="Action Details is required"
                    CssClass="text-danger"></asp:RequiredFieldValidator>
            </div>
            <div class="mb-3">
                <label for="txtRemarksCentral" class="form-label">Remarks:</label>
                <asp:TextBox ID="txtRemarksCentral" runat="server" TextMode="MultiLine"
                    Rows="4" CssClass="form-control" MaxLength="250"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                    ControlToValidate="txtRemarksCentral" ErrorMessage="Remarks is required"
                    CssClass="text-danger"></asp:RequiredFieldValidator>
            </div>
            <div class="mb-3">
                <label for="txtRepliedSenderCentral" class="form-label">Replied to Sender:</label>
                <asp:TextBox ID="txtRepliedSenderCentral" runat="server" TextMode="DateTimeLocal"
                    CssClass="form-control"></asp:TextBox> <%-- Removed MaxLength as it's a date field --%>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                    ControlToValidate="txtRepliedSenderCentral" ErrorMessage="Replied to Sender is required"
                    CssClass="text-danger"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="RepliedSenderValidator" runat="server" ControlToValidate="txtRepliedSenderCentral"
                    Type="Date" Operator="DataTypeCheck" ErrorMessage="Invalid Replied to Sender Date format."
                    CssClass="text-danger"></asp:CompareValidator>
            </div>

            <asp:Button ID="btnSubmitAlert" runat="server" Text="Submit Alert"
                CssClass="btn btn-primary" OnClick="btnSubmitAlert_Click" />
            <asp:Label ID="lblSubmissionMessage" runat="server" CssClass="mt-2"></asp:Label>
        </form>
    </div>
</asp:Content>

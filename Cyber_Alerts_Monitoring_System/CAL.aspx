<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CyberAlertsLocal.aspx.cs" Inherits="CyberAlerts.CyberAlertsLocal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cyber Alert Submission (Local)</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <style>
        .form-group {
            margin-bottom: 15px;
        }
        .text-danger {
            color: red;
        }
        .text-success {
            color: green;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="container mt-4">
        <h2>Cyber Alert Submission (Local)</h2>

        <div class="form-group">
            <label for="txtReporterLocal">Reporter (Auto-filled)</label>
            <asp:TextBox ID="txtReporterLocal" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtAlertTitleLocal">Alert Title <span class="text-danger">*</span></label>
            <asp:TextBox ID="txtAlertTitleLocal" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtDescriptionLocal">Description <span class="text-danger">*</span></label>
            <asp:TextBox ID="txtDescriptionLocal" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="ddlSeverityLocal">Severity</label>
            <asp:DropDownList ID="ddlSeverityLocal" runat="server" CssClass="form-control">
                <asp:ListItem Value="High">High</asp:ListItem>
                <asp:ListItem Value="Medium">Medium</asp:ListItem>
                <asp:ListItem Value="Low">Low</asp:ListItem>
            </asp:DropDownList>
        </div>

        <%-- Removed Affected Systems as per the C# code comments --%>

        <div class="form-group">
            <label for="txtReceivedFromSenderLocal">Received From (Sender) <span class="text-danger">*</span></label>
            <asp:TextBox ID="txtReceivedFromSenderLocal" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtPertainingToUnitLocal">Pertaining To Unit <span class="text-danger">*</span></label>
            <asp:TextBox ID="txtPertainingToUnitLocal" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtAffectedSailIPLocal">Affected SAIL IP <span class="text-danger">*</span></label>
            <asp:TextBox ID="txtAffectedSailIPLocal" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtAffectedPortLocal">Affected Port <span class="text-danger">*</span></label>
            <asp:TextBox ID="txtAffectedPortLocal" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtMaliciousIPLocal">Malicious IP <span class="text-danger">*</span></label>
            <asp:TextBox ID="txtMaliciousIPLocal" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtAlertDetailsLocal">Alert Details <span class="text-danger">*</span></label>
            <asp:TextBox ID="txtAlertDetailsLocal" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtReceivedDateLocal">Received Date <span class="text-danger">*</span></label>
            <asp:TextBox ID="txtReceivedDateLocal" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtIncidentDateLocal">Incident Date <span class="text-danger">*</span></label>
            <asp:TextBox ID="txtIncidentDateLocal" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtEntryDateLocal">Entry Date <span class="text-danger">*</span></label>
            <asp:TextBox ID="txtEntryDateLocal" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
        </div>

        <hr />
        <h5>Action Taken Details (For Plant/Unit Use Only)</h5>

        <div class="form-group">
            <label for="txtFirstActionTakenDateLocal">First Action Taken Date</label>
            <asp:TextBox ID="txtFirstActionTakenDateLocal" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtDetailsOfActionLocal">Details of Action Taken <span class="text-danger">*</span></label>
            <asp:TextBox ID="txtDetailsOfActionLocal" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtRemarksLocal">Remarks <span class="text-danger">*</span></label>
            <asp:TextBox ID="txtRemarksLocal" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtRepliedToDateLocal">Replied To Date</label>
            <asp:TextBox ID="txtRepliedToDateLocal" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtClosingDateLocal">Closing Date</label>
            <asp:TextBox ID="txtClosingDateLocal" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
        </div>

        <asp:Button ID="btnSubmitAlertLocal" runat="server" Text="Submit Alert" CssClass="btn btn-primary" OnClick="btnSubmitAlertLocal_Click" />

        <asp:Label ID="lblSubmissionMessageLocal" runat="server" Text="" Visible="false"></asp:Label>

    </form>

    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.3/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>

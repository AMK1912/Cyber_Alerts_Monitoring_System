<%@ Page Title="Reports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Reports.aspx.cs" Inherits="CyberAlert.Reports" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2>Report Center</h2>
        <p>Select a report type from the given below Report types to view data. Reports will be filtered by your plant location</p>



            <ol>
                <li><asp:Button ID="Button1" runat="server" CssClass="auto-style1" OnClick="Button1_Click" Text="Affected IP Report" Width="175px" Height="26px" /></li>
                <li><asp:Button ID="Button2" runat="server" CssClass="auto-style3" OnClick="Button2_Click" Text="SEBI Report" Width="175px" Height="26px" /></li>
                <li><asp:Button ID="Button3" runat="server" CssClass="auto-style3" OnClick="Button3_Click" Text="Malicious IP Report" Width="175px" Height="26px" /></li>
                <li><asp:Button ID="Button4" runat="server" CssClass="auto-style3" OnClick="Button4_Click" Text="Affected Ports Report" Width="175px" Height="26px" /></li>
                <li><asp:Button ID="Button5" runat="server" CssClass="auto-style3" OnClick="Button5_Click" Text="Action Pending Report" Width="175px" Height="26px" /></li>
                <li><asp:Button ID="Button6" runat="server" CssClass="auto-style3" OnClick="Button6_Click" Text="Closed Incidents Report" Width="175px" Height="26px" /></li>
            </ol>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />

        <div class="auto-style2">
            <br />
            <br />
        </div>

        <div>

            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />

        </div>
        <%--<div class="row mb-4">
            <div class="col-md-3">
                <div class="list-group">
                    <asp:LinkButton ID="btnConsolidatedReport" runat="server" OnClick="btnReport_Click" CommandArgument="Consolidated"
                        CssClass="list-group-item list-group-item-action">Consolidated Reports</asp:LinkButton>
                    <asp:LinkButton ID="btnAffectedIPReport" runat="server" OnClick="btnReport_Click" CommandArgument="AffectedIP"
                        CssClass="list-group-item list-group-item-action">Affected IP Reports</asp:LinkButton>
                    <asp:LinkButton ID="btnMaliciousReport" runat="server" OnClick="btnReport_Click" CommandArgument="Malicious"
                        CssClass="list-group-item list-group-item-action" style="left: 0px; top: 0px">Malicious Reports</asp:LinkButton>
                    <%-- Add more report types here as needed --%>
                    <%-- <asp:LinkButton ID="btnOtherReport" runat="server" OnClick="btnReport_Click" CommandArgument="Other"
                        CssClass="list-group-item list-group-item-action">Other Reports</asp:LinkButton>
                </div>
            </div>
            <div class="col-md-9">
                <div class="card">
                    <div class="card-header">
                        <h4 id="reportTitle" runat="server" class="mb-0">Select a Report</h4>
                    </div>
                    <div class="card-body">
                        <asp:Label ID="lblPlantLocation" runat="server" CssClass="d-block mb-3 text-muted"></asp:Label>
                        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="text-danger d-block mb-3"></asp:Label>

                        <asp:GridView ID="gvReports" runat="server" AutoGenerateColumns="true"
                            CssClass="table table-striped table-bordered" EmptyDataText="No data available for this report type or plant location."
                            AllowPaging="True" PageSize="10" OnPageIndexChanging="gvReports_PageIndexChanging">
                            <%-- Columns will be auto-generated for simplicity. For custom columns, set AutoGenerateColumns="false" and define <asp:BoundField> or <asp:TemplateField>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>--%>
    </div>
    </strong>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .auto-style1 {
            font-weight: bold;
            background-color: #FFFFFF;
            margin-left: 39;
        }
        .auto-style2 {
            margin-left: 160px;
            height: 69px;
        }
        .auto-style3 {
            font-weight: bold;
            background-color: #FFFFFF;
            margin-left: 0px;
        }
        </style>
</asp:Content>

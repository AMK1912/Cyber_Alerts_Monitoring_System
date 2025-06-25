<%@ Page Title="Reports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Reports.aspx.cs" Inherits="CyberAlert.Reports" %>

<%-- IMPORTANT: Register the Crystal Reports tag prefix.
     Verify the Version and PublicKeyToken from your CrystalDecisions.Web.dll reference properties.
     (e.g., in VS Solution Explorer, click 'References', find CrystalDecisions.Web, view Properties) --%>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="mb-3">Report Center</h2>
        <p class="mb-4">Select a report type from the list to view data. Reports will be filtered by your plant location.</p>

        <div class="row">
            <%-- Left column for report selection --%>
            <div class="col-md-3">
                <div class="card">
                    <div class="card-header">
                        <h5 class="mb-0">Report Types</h5>
                    </div>
                    <div class="card-body">
                        <ul class="list-group list-group-flush">
                            <li class="list-group-item d-flex justify-content-between align-items-center">
                                <div>
                                    <h6 class="mb-1">Consolidated Reports</h6>
                                    <small class="text-muted">Summary of all alerts.</small>
                                </div>
                                <asp:Button ID="btnLoadConsolidatedReport" runat="server" Text="Load Report"
                                    OnClick="btnLoadReport_Click" CommandArgument="Consolidated"
                                    CssClass="btn btn-primary btn-sm" />
                            </li>
                            <li class="list-group-item d-flex justify-content-between align-items-center bg-light text-muted">
                                <div>
                                    <h6 class="mb-1">SEBI Reports</h6>
                                    <small>Not yet implemented.</small>
                                </div>
                                <asp:Button ID="btnLoadSEBIReport" runat="server" Text="Load Report"
                                    Enabled="false"
                                    CssClass="btn btn-secondary btn-sm disabled" />
                            </li>
                            <%-- Add more report types here --%>
                            <li class="list-group-item d-flex justify-content-between align-items-center bg-light text-muted">
                                <div>
                                    <h6 class="mb-1">Affected IP Reports</h6>
                                    <small>Not yet implemented.</small>
                                </div>
                                <asp:Button ID="btnLoadAffectedIPReport" runat="server" Text="Load Report"
                                    Enabled="false"
                                    CssClass="btn btn-secondary btn-sm disabled" />
                            </li>
                            <li class="list-group-item d-flex justify-content-between align-items-center bg-light text-muted">
                                <div>
                                    <h6 class="mb-1">Malicious Reports</h6>
                                    <small>Not yet implemented.</small>
                                </div>
                                <asp:Button ID="btnLoadMaliciousReport" runat="server" Text="Load Report"
                                    Enabled="false"
                                    CssClass="btn btn-secondary btn-sm disabled" />
                            </li>
                        </ul>
                    </div>
                </div>
            </div>

            <%-- Right column for report viewer --%>
            <div class="col-md-9">
                <div class="card">
                    <div class="card-header">
                        <h4 id="reportTitle" runat="server" class="mb-0">Please Select a Report</h4>
                    </div>
                    <div class="card-body">
                        <%-- Displays the current plant location --%>
                        <asp:Label ID="lblPlantLocation" runat="server" CssClass="d-block mb-3 text-muted"></asp:Label>
                        <%-- For displaying error or informational messages --%>
                        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="text-danger d-block mb-3"></asp:Label>

                        <%-- The Crystal Reports Viewer control --%>
                        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server"
                            AutoDataBind="true"
                            DisplayGroupTree="False"             <%-- Hides the group tree panel --%>
                            EnableDatabaseLogonPrompt="False"    <%-- Prevents database login dialogs --%>
                            HasCrystalLogo="False"               <%-- Removes the Crystal Reports logo --%>
                            HasToggleGroupTreeButton="False"
                            HasToggleParameterPanelButton="False"
                            HasPageNavigationButtons="True"
                            BestFitPage="True"
                            ToolPanelView="None"                 <%-- Hides the side tool panel completely --%>
                            Height="800px"                       <%-- Set a fixed height for the viewer --%>
                            Width="100%" />                      <%-- Make the viewer take full width of its container --%>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<%-- Remove the Content2 block and its style definitions.
     All styling should ideally be in Site.css or directly via Bootstrap classes. --%>
<%-- <asp:Content ID="Content2" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .auto-style1 {
            font-weight: bold;
            background-color: #FFFFFF;
        }
        .auto-style2 {
            margin-left: 160px;
        }
        .auto-style3 {
            width: 172px;
            height: 23px;
        }
        .auto-style4 {
            width: 128px;
            height: 23px;
        }
    </style>
</asp:Content> --%>

<%@ Page Title="Reports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Reports.aspx.cs" Inherits="CyberAlert.Reports" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2>Report Center</h2>
        <p>Select a report type from the navbar to view data. Reports will be filtered by your plant location.</p>



            <strong>
            <h3 class="auto-style3">1. Consolidate Reports<strong><asp:Button ID="Button1" runat="server" CssClass="auto-style1" OnClick="Button1_Click" Text="Load Report" Width="128px" Height="30px" />
            </strong>

        </h3>
            <h3 class="auto-style4">1. SEBI Reports</h3>
            <br />
            <asp:Button ID="Button2" runat="server" CssClass="auto-style1" OnClick="Button1_Click" Text="Load Report" Width="128px" Height="30px" />
            </strong>



        <div class="auto-style2">
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
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="HeadContent">
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
</asp:Content>

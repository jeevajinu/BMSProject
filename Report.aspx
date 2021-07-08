<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="BMS.Report" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<html>
<head>
    <meta name="viewport" content="width=device-width" />

    <title>Index</title>

    <style type="text/css">
        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
            padding: 8px;
            line-height: 1.42857143;
            vertical-align: top;
            border-top: 1px solid #ddd;
        }

        th {
            text-align: left;
        }

        td, th {
            padding: 0;
        }

        * {
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
        }

        user agent stylesheet
        th {
            display: table-cell;
            vertical-align: inherit;
            font-weight: bold;
            text-align: -internal-center;
        }

        table {
            border-collapse: collapse;
            border-spacing: 0;
        }

        user agent stylesheet
        table {
            border-collapse: separate;
            text-indent: initial;
            border-spacing: 2px;
        }

        body {
            font-family: "Helvetica Neue", Helvetica, Arial, sans-serif;
            font-size: 14px;
            line-height: 1.42857143;
            color: #333333;
            background-color: #fff;
        }
    </style>
</head>
<body>
  

    <h2>Maintanance Requests</h2>   
    <form id="form1" runat="server">
        <div style="width:98%;">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server"  CssClass="report" >
            </rsweb:ReportViewer>
        </div>
        <asp:Button ID="Button1" runat="server" Text="Back Home" OnClick="Button1_Click" />
    </form>
</body>
</html>

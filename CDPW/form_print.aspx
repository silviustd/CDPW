<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="form_print.aspx.cs" Inherits="CDPW.form_print" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:PlaceHolder ID="phRaport" runat="server">
        <rsweb:ReportViewer ID="rwPrintPreview" runat="server" SizeToReportContent="True">
        </rsweb:ReportViewer>
    </asp:PlaceHolder>
    


    <asp:PlaceHolder ID="phError" runat="server" Visible="false"><span class="span50"></span>
        <p class="msg_box msg_error corners">
            <asp:Literal ID="ltrError" Text="Sorry, an error has occured. Please try again." runat="server" />
        </p>
    </asp:PlaceHolder>
</asp:Content>

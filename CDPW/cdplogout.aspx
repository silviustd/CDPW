<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="cdplogout.aspx.cs" Inherits="CDPW.cdplogout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <asp:PlaceHolder ID="phError" runat="server" Visible="false"><span class="span50"></span>
        <p class="msg_box msg_error corners">
            <asp:Literal ID="ltrError" Text="Sorry, an error has occured. Please try again." runat="server" />
        </p>
    </asp:PlaceHolder>
</asp:Content>

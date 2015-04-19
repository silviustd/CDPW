<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="cdpconfirmreg.aspx.cs" Inherits="CDPW.cdpconfirmreg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <asp:PlaceHolder ID="phMailIncorrect" runat="server" Visible="false">
        <span class="span50"></span>
        <p class="msg_box msg_alert corners bold">
            <asp:Literal ID="ltrLoginUserNoExist" Text="Sorry, there is no user  with the email address/username provided. In order to access the application, please sign-up." runat="server" />
        </p>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phURLIncorrect" runat="server" Visible="false">
        <span class="span50"></span>
        <p class="msg_box msg_alert corners bold">
            <asp:Literal ID="ltrURLIncorrect" Text="Sorry, the URL doesn't include a neccessary information. Please access the email sent to you when you signed up." runat="server" />
        </p>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phSuccess" runat="server" Visible="false">
        <span class="span50"></span>
        <p class="msg_box msg_ok corners msg_box_text">
            <asp:Literal ID="ltrSignupSuccess" Text="Congratulations! You have activated your account." runat="server" />
        </p>
        <span class="span30"></span>
        <p class="msg_box msg_info corners msg_box_text">
            To log in, click <a href="cdplogin.aspx">here</a>.
        </p>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phError" runat="server" Visible="false">
        <span class="span50"></span>
        <p id="pError" class="msg_box msg_error corners msg_box_text">
            <asp:Literal ID="ltrError" Text="Sorry, an error has occured. Please try again." runat="server" />
        </p>
    </asp:PlaceHolder>
</asp:Content>

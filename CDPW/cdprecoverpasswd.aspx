<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="cdprecoverpasswd.aspx.cs" Inherits="CDPW.cdprecoverpasswd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div id="container">
        <asp:PlaceHolder ID="phFormRecoverPasswd" runat="server">
            <div id="main-1" style="width: 420px; height: 250px; margin: 130px auto 200px;">
                <div id="col-1" style="border: none;">
                    <ul class="nav">
                        <li><a class="current">Recover Password</a></li>
                    </ul>
                    <div class="list-wrap">
                        <ul id="tab-1">
                            <li>
                                <fieldset id="recoverPasswd">
                                    <p>
                                        <asp:Label ID="lblEmail" Text="Email: " AssociatedControlID="txtEmail" runat="server" />
                                        <asp:TextBox ID="txtEmail" CssClass="validate[required,custom[email]]" runat="server" />
                                    </p>
                                    <p id="recovButton">
                                        <label>
                                            &nbsp;</label><asp:Button ID="btnSend" Text="Reset my password" OnClick="btnSend_Click"
                                                runat="server" />
                                    </p>
                                </fieldset>
                            </li>
                        </ul>
                        <%--                    <asp:PlaceHolder ID="phRecoverSuccess" runat="server" Visible="false">
                        <span class="span50"></span>
                        <p class="msg_box_v msg_info corners" style="width: 300px;">
                            <asp:Literal ID="ltrRecoverSuccess" runat="server">
                                An e-mail message was sent to the e-mail address provided with instructions how to reset your passsword.
                                <br />
                                Note that confirmation string expire in approximately 3 days.
                            </asp:Literal>
                        </p>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phRecoverWrong" runat="server" Visible="false">
                        <span class="span50"></span>
                        <p class="msg_box_v msg_alert corners bold" style="width: 300px;">
                            <asp:Literal ID="ltrRecoverWrong" Text="Sorry, there is no user  with the email address provided." runat="server" />
                        </p>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phResetPasswd" runat="server" Visible="false">
                        <span class="span50"></span>
                        <p class="msg_box_v msg_ok corners" style="width: 300px;">
                            <asp:Literal ID="ltrResetPwd" Text="You've successfully reset your password. An e-mail message was sent to the e-mail address provided with instructions how to access your account." runat="server" />
                        </p>
                    </asp:PlaceHolder>
        
                    <asp:PlaceHolder ID="phError" runat="server" Visible="false">
                        <span class="span50"></span>
                        <p id="pError" class="msg_box_v msg_error corners" style="width:300px;">
                            <asp:Literal ID="ltrError" Text="Sorry, an error has occured. Please try again." runat="server" />
                        </p>
                    </asp:PlaceHolder>--%>
                    </div>
        </asp:PlaceHolder>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#form1").validationEngine({ promptPosition: "topRight", autoHidePrompt: true, autoHideDelay: 3000, fadeDuration: 0.3 });
        });
    </script>
</asp:Content>

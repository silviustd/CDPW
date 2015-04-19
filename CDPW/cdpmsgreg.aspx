<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="cdpmsgreg.aspx.cs" Inherits="CDPW.cdpmsgreg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <asp:PlaceHolder ID="phError" runat="server" Visible="false">
        <p id="pError" class="msg_box msg_error corners">
            <asp:Literal ID="ltrError" runat="server" />
        </p>
    </asp:PlaceHolder>



    <div id="main-3" style="width: 850px; height: 320px;">
        <p id="response-message">
            <asp:Label ID="lblMesaj" runat="server" Text="Label" style="display: block; padding-bottom: 5px;text-align:center"></asp:Label>
            
            </p>

        <p><asp:Label ID="lblLogOut" Visible="false" runat="server" Text="Please <a href='cdplogout.aspx'> Log Out</a>  and Log in again" style="display: block; padding-bottom: 5px;text-align:center"></asp:Label></p> 
        <span style="padding-left: 690px;">
       <asp:ImageButton ID="btnUnsub" runat="server" 
            ImageUrl="https://www.paypalobjects.com/en_US/i/btn/btn_unsubscribe_SM.gif" 
            onclick="btnUnsub_Click" />
            </span>
            
        
    </div>

	<asp:PlaceHolder ID="phLogout" runat="server" Visible="false"><span class="30">
	</span>
		<div style="text-align: center;">
			Please <a href="cdplogout.aspx"> Log Out</a>  and Log in again
		</div>
	</asp:PlaceHolder>

</asp:Content>



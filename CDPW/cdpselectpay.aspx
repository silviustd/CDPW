<%@ Page Title="" Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="cdpselectpay.aspx.cs" Inherits="CDPW.cdpselectpay" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <asp:PlaceHolder ID="phError" runat="server" Visible="false">
        <p class="msg_box msg_error corners">
            <asp:Literal ID="ltrError" runat="server" />
        </p>
    </asp:PlaceHolder>

	<div id="container">
		<div id="main-2" style="margin-top: 20px;">
			<h1>Customs forms</h1>
			<div id="col-1">
				<div>
					<h2><asp:Label ID="lblLUSCBP" runat="server" Text="U.S. Customs and Border Protection Custom Declaration" /></h2>
					<p class="special-1">
						US CBP Form 6059b<br />
					</p>
				</div>
				<div>
					<a href="theme/clasic/images/usa-big-declaration.jpg" id="DUSA" title="U.S. Customs and Border Protection Custom Declaration" style="border-bottom: none;"><img src="theme/clasic/images/usa-declaration.png" alt="" /></a>
				</div>
			</div>
			<div id="col-2">
				<div>
					<h2><asp:Label ID="lblLCBSA" runat="server" Text="Canada Border Services Agency Declaration Card" /></h2>
					<p class="special-1">
						CBSA Form E311<br />
					</p>
				</div>
				<div>
					<a href="theme/clasic/images/canada-big-declaration.jpg" id="DCAN" title="Canada Border Services Agency Declaration Card" style="border-bottom: none;"><img src="theme/clasic/images/canada-declaration.png" alt="" /></a>
				</div>
			</div>

        <div class="clearfloat">&nbsp;</div>
        <form action="#">&nbsp;</form>
<%--		<div class="select-1">
			<h3>U.S. Customs and Border Protection Customs Declaration</h3>
			<select>
				<option selected="selected">0.99 CAD</option>
				
			</select>

			<form action="https://www.paypal.com/cgi-bin/webscr" method="post" style="display: inline-block; width: auto;">
				<input type="hidden" name="cmd" value="_s-xclick" />
				<input type="hidden" name="hosted_button_id" value="HWT5EK2N9KVY4" />
				<input type="image" src="https://www.paypalobjects.com/en_US/i/btn/btn_paynow_SM.gif" border="0" name="submit" alt="PayPal - The safer, easier way to pay online!" />
				<img alt="" border="0" src="https://www.paypalobjects.com/en_US/i/scr/pixel.gif" width="1" height="1" />
			</form>
        </div>--%>
         

<%--		<div class="select-1">
			<h3>Canada Border Services Agency Declaration Card</h3>
			<select>
				<option selected="selected">0.99 CAD</option>
				
			</select>
			<form action="https://www.paypal.com/cgi-bin/webscr" method="post" style="display: inline-block; width: auto;">
				<input type="hidden" name="cmd" value="_s-xclick" />
				<input type="hidden" name="hosted_button_id" value="N9U3L8TBZ7NX8" />
				<input type="image" src="https://www.paypalobjects.com/en_US/i/btn/btn_paynow_SM.gif" border="0" name="submit" alt="PayPal - The safer, easier way to pay online!" />
				<img alt="" border="0" src="https://www.paypalobjects.com/en_US/i/scr/pixel.gif" width="1" height="1" />
			</form>
		</div>

--%>
<%--			<div id="cut">Or cut the costs and select a plan</div>
			<p class="note-1" style="border-top: none;"><asp:Label ID="lblCUSCBP" runat="server" Text=""/></p>
			<asp:Label ID="lblCCBSA" runat="server" Text="" />
			<div class="clearfloat">
				&nbsp;</div>
			<p class="note-1" style="padding-bottom: 20px;">
				* All the plans include 2 weeks free trial<br/>
				** Any plan will be automatically renewed, unless otherwise notified
			</p>--%>
		</div>
	</div>

</asp:Content>
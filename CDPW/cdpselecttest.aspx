<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cdpselecttest.aspx.cs" Inherits="CDPW.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">


    
    

<input type="hidden" name="currency_code" value="CAD">
    <input type="hidden" name="on0" value="">
<input type="hidden" name="hosted_button_id" value="47SB7PBK6M47N">
<input type="hidden" name="cmd" value="_s-xclick">


    
    

    <table style="width: 100%;">
    <tr>
        <td>
            <asp:Label ID="lblLUSCBP" runat="server" Text="U.S. Customs and Border Protection Customs Declaration" />
            
            <br />
        </td>
        <td>
            &nbsp;
        </td>
        
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblLCBSA" runat="server" Text="Canada Border Services Agency Declaration Card" />
            <br />
        </td>
        <td>
            &nbsp;
        </td>

    </tr>
            <tr>
        <td>

            &nbsp;</td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>

            Plan CAN Declaration Card
            <br />
            </br>
            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                Text="Subscribe" />
            &nbsp; &nbsp; &nbsp;
            <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
                Text="Unsubscribe" />
            <br />
            </br>
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            Plan USA Customs Card
            </br>
            <br />
            <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
                Text="Subscribe" />
            &nbsp; &nbsp; &nbsp;
            <asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
                Text="Unsubscribe" />
            </br>
        
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>


    <div class="select-1">
		</div>
        
                </td>
        <td>
            &nbsp;
        </td>
    </tr>

    <tr>
        <td>

            &nbsp;</td>
        <td>
            &nbsp;
        </td>
    </tr>

<tr>
        <td>

<asp:Label ID="lblCUSCBP" runat="server" Text="">


        
</asp:Label>
           
        </td>
        <td>
            &nbsp;
        </td>
    </tr>

<tr>
        <td>

<asp:Label ID="lblCCBSA" runat="server" Text="">


        
</asp:Label>
           
        </td>
        <td>
            &nbsp;
        </td>
    </tr>

</table>


    
    
    </form>    
</body>
</html>


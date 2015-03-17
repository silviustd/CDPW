<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DCANForm.aspx.cs" Inherits="CDPW.DCANForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <asp:PlaceHolder ID="phCanForm" runat="server">

<div id="top">
    <asp:PlaceHolder ID="phFormSave" runat="server" Visible="false">
        <p class="msg_box msg_info corners"  align = "center" >
            <asp:Literal ID="ltrFromSave" Text="Please Save first, then Print !" runat="server"  />
        </p>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phError" runat="server" Visible="false">
        <p class="msg_box msg_error corners">
            <asp:Literal ID="ltrError" runat="server" />
        </p>
    </asp:PlaceHolder>

    <asp:PlaceHolder ID="phReproduction" runat="server" Visible="true">
    
    <p id="discl_CAN" class="disclaimer-4">
        This is a reproduction and it does not replace Canada Border Services Agency official
        form. <a class="open-1" style="cursor: pointer;">See more</a><a class="open-2" style="cursor: pointer;
            display: none;">Hide</a><br />
        It was done to ease filling out the information. For printing you have to use an
        official empty form issued by Canada Border Services Agency
    </p>
    </asp:PlaceHolder>



    </div>
    
    <div id="main-2">
        <table id="DCANForm">
            <tr>
                <td id="column-1">
                    <h2>
                        Instructions</h2>
                    <p>
                        All travellers must be identified on a Canada Border Services Agency (CBSA) Declaration
                        Card. You may list up to four people living at the same address on one card. <strong>
                            Each traveller is responsible for his or her own declaration.</strong></p>
                    <p>
                        <strong>Under the law</strong>, failure to properly declare goods, currency and/or
                        monetary instruments brought into Canada may result in seizure action, monetary
                        penalties and/or criminal prosecution.</p>
                    <p>
                        Information from this declaration will be used for CBSA control purposes, and may
                        be shared with other government departments to enforce Canadian laws. For more information
                        see Info Source (ref. no. CBSA PPU 018), at a public library or visit <a href="http://infosource.gc.ca"
                            target="_blank"><strong>http://infosource.gc.ca</strong></a>.</p>
                    <h2>
                        Part B - Visitors to Canada</h2>
                    <p>
                        The following duty-free allowances apply to each visitor entering into Canada:</p>
                    <ul>
                        <li>Gifts (excludes alcohol and tobacco) valued at no more than CAN$60 each.</li>
                        <li>1.5 L of wine or 1.14 L of liquor or 24 x 355 ml cans or bottles (8.5 L) of beer
                            or ale.</li>
                        <li>200 cigarettes, 200 tobacco sticks, 50 cigars or cigarillos and 200 grams of manufactured
                            tobacco.</li>
                    </ul>
                    <h2>
                        Part C - Residents of Canada</h2>
                    <p>
                        Each resident returning to Canada is entitled to one of the following personal exemptions
                        based on his/her time absent from Canada (include all goods and/or gifts purchased
                        or received abroad):</p>
                    <ul>
                        <li><strong>4 hours: CAN$50</strong><br />
                            Not claimable if goods exceed $50.<br />
                            Alcohol and tobacco cannot be claimed.</li>
                        <li><strong>48 hours: CAN$400</strong><br />
                            This includes alcohol and tobacco<br />
                            (see table below).</li>
                        <li><strong>7 days: CAN$750</strong><br />
                            This includes alcohol and tobacco<br />
                            (see table below).</li>
                    </ul>
                    <table id="note-1">
                        <tr>
                            <td style="font-weight: bold; text-align: center;">
                                Alcohol and tobacco<br />
                                exemption table
                            </td>
                        </tr>
                        <tr>
                            <td>
                                1.5 L of wine or 1.14 L of liquor or 24 x 355 ml cans or bottles (8.5 L) of beer
                                or ale. (You must be of legal age in the province of importation.)
                            </td>
                        </tr>
                        <tr>
                            <td>
                                200 cigarettes, 200 tobacco sticks, 50 cigars or cigarillos and 200 grams of manufactured
                                tobacco (Special Duty may apply).
                            </td>
                        </tr>
                    </table>
                </td>
                <td id="column-2">
                    &nbsp;
                </td>
                <td id="column-3" style="vertical-align: top;">
                    <table id="table-1">
                        <tr>
                            <td colspan="2" class="header">
                                Part A / All travellers (living at the same address)
                            </td>
                        </tr>
                        <tr>
                            <td class="td-1">
                                1
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblName1" AssociatedControlID="txtName1" runat="server" Text="Last name, first name and initials"></asp:Label><br />
                                            <asp:TextBox ID="txtName1" runat="server" class="text text-1" original-title="26 characters" AutoPostBack="False" MaxLength="26" onchange="javascript:ChangeAlert();"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 210px;">
                                            <asp:Label ID="lblDOB1" AssociatedControlID="txtDOB1" runat="server" Text="Date of birth" class="label-1"></asp:Label>
                                            <asp:TextBox ID="txtDOB1" runat="server" class="text text-2 validate[custom[date]" original-title="mm/dd/yyyy" AutoPostBack="False" MaxLength="10" onchange="javascript:ChangeAlert();"></asp:TextBox>
                                        </td>
                                        <td style="width: 340px;">
                                            <asp:Label ID="lblCitiz1" AssociatedControlID="txtCitiz1" runat="server" Text="Citizenship" class="label-2"></asp:Label>
                                            <asp:TextBox ID="txtCitiz1" runat="server" class="text text-3" original-title="12 characters"  AutoPostBack="False" MaxLength="12" onchange="javascript:ChangeAlert();"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-1">
                                2
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblName2" AssociatedControlID="txtName2" runat="server" Text="Last name, first name and initials"></asp:Label><br />
                                            <asp:TextBox ID="txtName2" runat="server" class="text text-1" original-title="26 characters" AutoPostBack="False" MaxLength="26" onchange="javascript:ChangeAlert();"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 210px;">
                                            <asp:Label ID="lblDOB2" AssociatedControlID="txtDOB2" runat="server" Text="Date of birth"
                                                class="label-1"></asp:Label>
                                            <asp:TextBox ID="txtDOB2" runat="server" class="text text-2 validate[custom[date]" original-title="mm/dd/yyyy" MaxLength="10" AutoPostBack="False" onchange="javascript:ChangeAlert();"></asp:TextBox>
                                        </td>
                                        <td style="width: 340px;">
                                            <asp:Label ID="lblCitiz2" AssociatedControlID="txtCitiz2" runat="server" Text="Citizenship"
                                                class="label-2"></asp:Label>
                                            <asp:TextBox ID="txtCitiz2" runat="server" class="text text-3" original-title="12 characters" MaxLength="12" AutoPostBack="False" onchange="javascript:ChangeAlert();"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-1">
                                3
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblName3" AssociatedControlID="txtName3" runat="server" Text="Last name, first name and initials"></asp:Label><br />
                                            <asp:TextBox ID="txtName3" runat="server" class="text text-1" original-title="26 characters" AutoPostBack="False" MaxLength="26" onchange="javascript:ChangeAlert();"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 210px;">
                                            <asp:Label ID="lblDOB3" AssociatedControlID="txtDOB3" runat="server" Text="Date of birth"
                                                class="label-1"></asp:Label>
                                            <asp:TextBox ID="txtDOB3" runat="server" class="text text-2 validate[custom[date]" original-title="mm/dd/yyyy" MaxLength="10" AutoPostBack="False" onchange="javascript:ChangeAlert();"></asp:TextBox>
                                        </td>
                                        <td style="width: 340px;">
                                            <asp:Label ID="lblCitiz3" AssociatedControlID="txtCitiz3" runat="server" Text="Citizenship"
                                                class="label-2"></asp:Label>
                                            <asp:TextBox ID="txtCitiz3" runat="server" class="text text-3" original-title="12 characters" MaxLength="12" AutoPostBack="False" onchange="javascript:ChangeAlert();"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-1">
                                4
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblName4" AssociatedControlID="txtName4" runat="server" Text="Last name, first name and initials"></asp:Label><br />
                                            <asp:TextBox ID="txtName4" runat="server" class="text text-1" original-title="26 characters" AutoPostBack="False" MaxLength="26" onchange="javascript:ChangeAlert();"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 210px;">
                                            <asp:Label ID="lblDOB4" AssociatedControlID="txtDOB4" runat="server" Text="Date of birth"
                                                class="label-1"></asp:Label>
                                            <asp:TextBox ID="txtDOB4" runat="server" class="text text-2 validate[custom[date]"
                                                original-title="mm/dd/yyyy" AutoPostBack="False" MaxLength="10" onchange="javascript:ChangeAlert();"></asp:TextBox>
                                        </td>
                                        <td style="width: 340px;">
                                            <asp:Label ID="lblCitiz4" AssociatedControlID="txtCitiz4" runat="server" Text="Citizenship"
                                                class="label-2"></asp:Label>
                                            <asp:TextBox ID="txtCitiz4" runat="server" class="text text-3" original-title="12 characters" AutoPostBack="False" MaxLength="12" onchange="javascript:ChangeAlert();"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="table-2">
                        <tr>
                            <td colspan="2" style="width: 350px;">
                                <asp:Label ID="lblAddress" AssociatedControlID="txtAddress" runat="server" Text="HOME ADDRESS - Number, street, apartment No."></asp:Label><br />
                                <asp:TextBox ID="txtAddress" runat="server" class="text text-1" original-title="38 characters" MaxLength="38" AutoPostBack="False" onchange="javascript:ChangeAlert();"></asp:TextBox>
                            </td>
                            <td style="width: 200px;">
                                <asp:Label ID="lblCity" AssociatedControlID="txtCity" runat="server" Text="City/Town"></asp:Label><br />
                                <asp:TextBox ID="txtCity" runat="server" class="text text-2" original-title="10 characters" MaxLength="10" AutoPostBack="False"  onchange="javascript:ChangeAlert();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 110px;">
                                <asp:Label ID="lbllProv" AssociatedControlID="ddlProv" runat="server" Text="Prov/State"></asp:Label><br />
                                <asp:DropDownList ID="ddlProv" runat="server" class="lista-11" AutoPostBack="False" onchange="javascript:ChangeAlert();"/>
                            </td>
                            <td style="width: 220px;">
                                <asp:Label ID="lblCountry" AssociatedControlID="ddlCountry" runat="server" Text="Country"></asp:Label><br />
                                <asp:DropDownList ID="ddlCountry" runat="server" class="lista-1" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" />
                            </td>
                            <td style="width: 220px;">
                                <asp:Label ID="lblPostalCode" AssociatedControlID="txtPostalCode" runat="server"
                                    Text="Postal/ Zip code"></asp:Label><br />
                                <asp:TextBox ID="txtPostalCode" runat="server" class="text text-4" original-title="10 characters" MaxLength="10" AutoPostBack="False" onchange="javascript:ChangeAlert();"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table id="table-3">
                        <tr>
                            <td style="width: 258px;">
                                <asp:Label ID="lblArrive" AssociatedControlID="rblArrive" runat="server" Text="Arriving by:"
                                    class="bold"></asp:Label><br />
                                <asp:RadioButtonList ID="rblArrive" runat="server" RepeatLayout="Flow" TextAlign="Left" RepeatDirection="Horizontal" AutoPostBack="False" onchange="javascript:ChangeAlert();">
                                    <asp:ListItem Value="0" Text="Air"  />
                                    <asp:ListItem Value="1" Text="Rail" />
                                    <asp:ListItem Value="2" Text="Marina" />
                                    <asp:ListItem Value="3" Text="Highway" />
                                 </asp:RadioButtonList>
                                <asp:Label ID="lblTripNo" AssociatedControlID="txtTripNo" runat="server" Text="<strong>Airline/Flight No., Train No. or Vessel Name</strong>"></asp:Label>
                                <asp:TextBox ID="txtTripNo" runat="server" class="text text-5" original-title="13 characters/digits" MaxLength="13" style="margin-left: 8px; width: 135px;" AutoPostBack="False" onchange="javascript:ChangeAlert();"></asp:TextBox>

                            </td>
                            <td style="width: 120px; padding-right: 20px; border-right: 1px solid #000; border-left: 1px solid #000;
                                text-align: right;">
                                <asp:Label ID="lblPurpose" AssociatedControlID="rblPurpose" runat="server" Text="Purpose of the trip:"
                                    class="bold"></asp:Label><br />
                                <asp:RadioButtonList ID="rblPurpose" runat="server" TextAlign="Left" RepeatLayout="UnorderedList"
                                    CssClass="radiosVertical" AutoPostBack="False" onchange="javascript:ChangeAlert();">
                                    <asp:ListItem Value="0" Text="Study" />
                                    <asp:ListItem Value="1" Text="Personal" />
                                    <asp:ListItem Value="2" Text="Business" />
                                </asp:RadioButtonList>
                            </td>
                            <td style="width: 150px; text-align: right;">
                                <asp:Label ID="lblArriveFrom" AssociatedControlID="rblArriveFrom" runat="server"
                                    Text="Arriving from:" class="bold"></asp:Label><br />
                                <asp:RadioButtonList ID="rblArriveFrom" runat="server" TextAlign="Left" RepeatLayout="UnorderedList" CssClass="radiosVertical" AutoPostBack="False" onchange="javascript:ChangeAlert();">
                                    <asp:ListItem Value="0" Text="U.S only" />
                                    <asp:ListItem Value="1" Text="Other country direct" />
                                    <asp:ListItem Value="2" Text="Other country via U.S." />
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <table id="table-4">
                        <tr>
                            <td rowspan="7" style="width: 210px;">
                                <img src="theme/clasic/images/canada-1.jpg" alt="" />
                            </td>
                            <td style="padding: 5px 0;">
                                <strong>I am/we are bringing into Canada:</strong>
                            </td>
                            <td style="padding: 5px 0; text-align: right; width:23px;">
                                Yes
                            </td>
                            <td style="padding: 5px 0; text-align: center; width: 23px;">
                                No
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <ul>
                                    <li>Firearms or other weapons (e.g. switchblades, Mace or pepper spray).</li>
                                </ul>
                            </td>
                            <td colspan="2">
                                <asp:RadioButtonList ID="rblFirearms" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Table" CssClass="radiosHorizontal" AutoPostBack="False" onchange="javascript:ChangeAlert();">
                                    <asp:ListItem Value="1" Text="" />
                                    <asp:ListItem Value="0" Text="" />
                                </asp:RadioButtonList>
                                <asp:CheckBox ID="chkWeaponsY" runat="server" Text="" TextAlign="Left" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <ul>
                                    <li>Commercial goods, whether or not for resale (e.g. sampless, tools, equipment).</li>
                                </ul>
                            </td>
                            <td colspan="2">
                                <asp:RadioButtonList ID="rblCG" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" AutoPostBack="False" onchange="javascript:ChangeAlert();"
                                    CssClass="radiosHorizontal">
                                    <asp:ListItem Value="1" Text="" />
                                    <asp:ListItem Value="0" Text=""  />
                                </asp:RadioButtonList>
                                <asp:CheckBox ID="chkCGY" runat="server" Text="" TextAlign="Left" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <ul>
                                    <li>Meat/meat products; dairy products; fruits; vegetables; seeds; nuts; plants and
                                        animals or their parts/products; cut flowers; soil; wood/wood products; birds; insects.</li>
                                </ul>
                            </td>
                            <td colspan="2">
                                <asp:RadioButtonList ID="rblFood" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" AutoPostBack="False" onchange="javascript:ChangeAlert();"
                                    CssClass="radiosHorizontal">
                                    <asp:ListItem Value="1" Text="" />
                                    <asp:ListItem Value="0" Text="" />
                                </asp:RadioButtonList>
                                <asp:CheckBox ID="chkFoodY" runat="server" Text="" TextAlign="Left" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <ul>
                                    <li>Currency and/or monetary instruments totaling CAN$10,000 or more.</li>
                                </ul>
                            </td>
                            <td colspan="2">
                                <asp:RadioButtonList ID="rblMoney" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" AutoPostBack="False" onchange="javascript:ChangeAlert();"
                                    CssClass="radiosHorizontal">
                                    <asp:ListItem Value="1" Text="" />
                                    <asp:ListItem Value="0" Text="" />
                                </asp:RadioButtonList>
                                <asp:CheckBox ID="chkMoneyY" runat="server" Text="" TextAlign="Left" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p>
                                    I/we have shipped goods that are not accompanying me/us.</p>
                            </td>
                            <td colspan="2">
                                <asp:RadioButtonList ID="rblGoods" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" AutoPostBack="False" onchange="javascript:ChangeAlert();"
                                    CssClass="radiosHorizontal">
                                    <asp:ListItem Value="1" Text="" />
                                    <asp:ListItem Value="0" Text=""  />
                                </asp:RadioButtonList>
                                <asp:CheckBox ID="chkGoodsY" runat="server" Text="" TextAlign="Left" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p>
                                    I/we have visited a farm and will be going to a farm in Canada.</p>
                            </td>
                            <td colspan="2">
                                <asp:RadioButtonList ID="rblFarm" runat="server" RepeatDirection="Horizontal" AutoPostBack="False" onchange="javascript:ChangeAlert();"
                                    RepeatLayout="Table" CssClass="radiosHorizontal">
                                    <asp:ListItem Value="1" Text="" />
                                    <asp:ListItem Value="0" Text=""  />
                                </asp:RadioButtonList>
                                <asp:CheckBox ID="chkFarmY" runat="server" Text="" TextAlign="Left" Visible="false" />
                            </td>
                        </tr>
                    </table>
                    <table id="table-5">
                        <tr>
                            <td colspan="3" class="header">
                                Part B / Visitors to Canada
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 200px;">
                                <strong>Duration of stay in Canada</strong><br />
                                <asp:TextBox ID="txtDureation" runat="server" class="text validate[custom[onlyNumberSp],minSize[1]]"
                                    original-title="3 digits" MaxLength="3" AutoPostBack="False" onchange="javascript:ChangeAlert();"></asp:TextBox>
                                days
                            </td>
                            <td style="width: 350px;">
                                Do you or any person listed above exceed the duty-free allowances per person?<br />
                                <em>(See instructions on the left.)</em>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rblExceededDutyFree" runat="server" RepeatDirection="Horizontal" AutoPostBack="False" onchange="javascript:ChangeAlert();"
                                    TextAlign="Left" CssClass="rblB_C">
                                    <asp:ListItem Value="1" Text="Yes" />
                                    <asp:ListItem Value="0" Text="No" />
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <table id="table-6">
                        <tr>
                            <td colspan="2" class="header">
                                Part C / Residents of Canada
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 550px;">
                                Do you or any person listed above exceed the exemptions per person?<br />
                                <em>(See instructions on the left.)</em>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rblExceededExemptions" runat="server" RepeatDirection="Horizontal" AutoPostBack="False" onchange="javascript:ChangeAlert();"
                                    TextAlign="Left" CssClass="rblB_C">
                                    <asp:ListItem Value="1" Text="Yes" />
                                    <asp:ListItem Value="0" Text="No" />
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <table id="table-7">
                        <tr>
                            <td colspan="6" class="header">
                                Complete in the same order as Part A
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                Date left Canada
                                <br />
                                YYYY - MM - DD
                            </td>
                            <td>
                                Value of goods - CAN$<br />
                                <span class="small-1">purchased or received abroad (including gifts, alcohol &amp; tobacco)</span>
                            </td>
                            <td colspan="2">
                                Date left Canada
                                <br />
                                YYYY - MM - DD
                            </td>
                            <td>
                                Value of goods - CAN$<br />
                                <span class="small-1">purchased or received abroad (including gifts, alcohol &amp; tobacco</span>)
                            </td>
                        </tr>
                        <tr>
                            <td class="td-1">
                                1
                            </td>
                            <td>
                                <asp:TextBox ID="txtDtC1" runat="server" class="text validate[custom[date-YYYY-MM-DD]" AutoPostBack="False"  MaxLength="10" onchange="javascript:ChangeAlert();" original-title="YYYY-MM-DD"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCVG1" runat="server" class="text text-2 validate[funcCall[numValF]]" MaxLength="12" original-title="######.#### (<6 digits>.<4 digits>)" AutoPostBack="False" onchange="javascript:ChangeAlert();"></asp:TextBox>
                            </td>
                            <td class="td-1">
                                3
                            </td>
                            <td>
                                <asp:TextBox ID="txtDtC3" runat="server" class="text validate[custom[date-YYYY-MM-DD]" original-title="YYYY-MM-DD" MaxLength="10" AutoPostBack="False" onchange="javascript:ChangeAlert();"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCVG3" runat="server" class="text text-2 validate[funcCall[numValF]]" MaxLength="12" original-title="######.#### (<6 digits>.<4 digits>)" AutoPostBack="False" onchange="javascript:ChangeAlert();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-1">
                                2
                            </td>
                            <td>
                                <asp:TextBox ID="txtDtC2" runat="server" class="text validate[custom[date-YYYY-MM-DD]" original-title="YYYY-MM-DD" MaxLength="10" AutoPostBack="False" onchange="javascript:ChangeAlert();"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCVG2" runat="server" class="text text-2 validate[funcCall[numValF]]" MaxLength="12" original-title="######.#### (<6 digits>.<4 digits>)" AutoPostBack="False" onchange="javascript:ChangeAlert();"></asp:TextBox>
                            </td>
                            <td class="td-1">
                                4
                            </td>
                            <td>
                                <asp:TextBox ID="txtDtC4" runat="server" class="text validate[custom[date-YYYY-MM-DD]" original-title="YYYY-MM-DD" MaxLength="10" AutoPostBack="False" onchange="javascript:ChangeAlert();"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCVG4" runat="server" class="text text-2 validate[funcCall[numValF]]" MaxLength="12" original-title="######.#### (<6 digits>.<4 digits>)" AutoPostBack="False" onchange="javascript:ChangeAlert();"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table id="table-8">
                        <tr>
                            <td colspan="4" class="header">
                                Part D / Signatures (age 16 and older): I certify that my declaration is true and
                                complete.
                            </td>
                        </tr>
                        <tr>
                            <td class="td-1">
                                1
                            </td>
                            <td style="width: 250px;">
                                <%--<asp:TextBox ID="D1" runat="server" class="text text-1" AutoPostBack="False" OnText Changed="OnCChangedEvent"></asp:TextBox>--%> &nbsp;
                            </td>
                            <td colspan="2" rowspan="2">
                                Date
                                <asp:TextBox ID="txtdDate" runat="server" class="text text-2 validate[custom[date]" original-title="MM/DD/YYYY" MaxLength="10" onchange="javascript:ChangeAlert();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-1">
                                2
                            </td>
                            <td>
                                <%--<asp:TextBox ID="D2" runat="server" class="text text-1" AutoPostBack="False" OnText Changed="OnCChangedEvent"></asp:TextBox>--%> &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="td-1">
                                3
                            </td>
                            <td>
                                <%--<asp:TextBox ID="D3" runat="server" class="text text-1" AutoPostBack="False" OnText Changed="OnCChangedEvent"></asp:TextBox>--%> &nbsp;
                            </td>
                            <td class="td-1">
                                4
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <p class="disclaimer-3">
        This is a reproduction and it does not replace Canada Border Services Agency official
        form.
    </p>
    <div id="button-form-3">
    <%--
        <asp:LinkButton ID="btnSaveForm" Text="Save" runat="server" OnClick="btnSaveForm_Click" runat="server" />
                   
            --%>
        <asp:Button ID="btnSaveForm" Text="Save" OnClick="btnSaveForm_Click" runat="server" />
        <a href="javascript:IfPrint()" id="print-2">Print</a> 
        <%-- 
        <a href="cdpmysettings.aspx" id="my_settings_2">My Settings</a> 
        <a href="cdplogout.aspx" id="logout-2">Logout</a>
        --%>
        <asp:HyperLink ID="my_settings_2" runat="server" NavigateUrl="cdpmysettings.aspx">My Settings</asp:HyperLink>
        <asp:HyperLink ID="logout_2" runat="server" NavigateUrl="cdplogout.aspx">Logout</asp:HyperLink>            

    </div>
        <asp:HiddenField ID="hidName1" runat="server" />
        <asp:HiddenField ID="hidDOB1" runat="server" />
        <asp:HiddenField ID="hidCitiz1" runat="server" />
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phCanFormSuccess" runat="server" Visible="false">
        <span class="span50"></span>
        <p class="msg_box msg_ok corners">
            <asp:Literal ID="ltrCanFormSuccess" Text="The form was submited with success." runat="server" />
        </p>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phCanFormNoName1" runat="server" Visible="false"><span class="span50">
    </span>
        <p class="msg_box msg_error corners">
            <asp:Literal ID="ltrCanFormNoName1" Text="You didn't complete the Last name, first name and initials for the first person. Please do this in order to be able to save the form's information." runat="server" />
        </p>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phCanFormWrongName1" runat="server" Visible="false"><span class="span50">
    </span>
        <p class="msg_box msg_info corners">
            <asp:Literal ID="ltrCanFormWrongName1" Text="You are not allowed to enter a different name other than yours for the first person." runat="server" />
        </p>
    </asp:PlaceHolder>

    <asp:PlaceHolder ID="phCanFormGoback" runat="server" Visible="false">
        <span class="30"></span>
        <div style="text-align: center;">
            <a href="javascript:void()" onclick="history.go(-1);">Go back to form</a>
        </div>
    </asp:PlaceHolder>


    <script type="text/javascript">

        $(function () {
            $('input.text').tipsy({ gravity: 's' });
        });

        $(document).ready(function () {
            $('.open-1').click(function () {
                $('p.disclaimer-2').css('height', '35px').slideDown('slow');
                $('a.open-1').css('display', 'none').fadeOut("slow");
                $('a.open-2').css('display', 'inline').fadeIn("slow");
            });
            $('.open-2').click(function () {
                $('p.disclaimer-2').css('height', '10px').slideDown('slow');
                $('a.open-1').css('display', 'inline').fadeIn("slow");
                $('a.open-2').css('display', 'none').fadeOut("slow");
            });

            var d = new Date();
            $("#txtDOB1").datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: (d.getFullYear() - 100) + ':' + (d.getFullYear()),
                defaultDate: new Date((d.getFullYear() - 18), 1 - 1, 1),
                minDate: new Date((d.getFullYear() - 100), 1 - 1, 1),
                maxDate: new Date((d.getFullYear()), d.getMonth(), d.getDate())
            });

            $("#txtDOB2").datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: (d.getFullYear() - 100) + ':' + (d.getFullYear()),
                defaultDate: new Date((d.getFullYear() - 18), 1 - 1, 1),
                minDate: new Date((d.getFullYear() - 100), 1 - 1, 1),
                maxDate: new Date((d.getFullYear()), d.getMonth(), d.getDate())
            });

            $("#txtDOB3").datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: (d.getFullYear() - 100) + ':' + (d.getFullYear()),
                defaultDate: new Date((d.getFullYear() - 18), 1 - 1, 1),
                minDate: new Date((d.getFullYear() - 100), 1 - 1, 1),
                maxDate: new Date((d.getFullYear()), d.getMonth(), d.getDate())
            });

            $("#txtDOB4").datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: (d.getFullYear() - 100) + ':' + (d.getFullYear()),
                defaultDate: new Date((d.getFullYear() - 18), 1 - 1, 1),
                minDate: new Date((d.getFullYear() - 100), 1 - 1, 1),
                maxDate: new Date((d.getFullYear()), d.getMonth(), d.getDate())
            });

            $("#txtDtC1").datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: (d.getFullYear() - 100) + ':' + (d.getFullYear()),
                defaultDate: new Date((d.getFullYear() - 18), 1 - 1, 1),
                minDate: new Date((d.getFullYear() - 100), 1 - 1, 1),
                maxDate: new Date((d.getFullYear()), d.getMonth(), d.getDate()),
                dateFormat: "yy-mm-dd"
            });

            $("#txtDtC2").datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: (d.getFullYear() - 100) + ':' + (d.getFullYear()),
                defaultDate: new Date((d.getFullYear() - 18), 1 - 1, 1),
                minDate: new Date((d.getFullYear() - 100), 1 - 1, 1),
                maxDate: new Date((d.getFullYear()), d.getMonth(), d.getDate()),
                dateFormat: "yy-mm-dd"
            });

            $("#txtDtC3").datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: (d.getFullYear() - 100) + ':' + (d.getFullYear()),
                defaultDate: new Date((d.getFullYear() - 18), 1 - 1, 1),
                minDate: new Date((d.getFullYear() - 100), 1 - 1, 1),
                maxDate: new Date((d.getFullYear()), d.getMonth(), d.getDate()),
                dateFormat: "yy-mm-dd"
            });

            $("#txtDtC4").datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: (d.getFullYear() - 100) + ':' + (d.getFullYear()),
                defaultDate: new Date((d.getFullYear() - 18), 1 - 1, 1),
                minDate: new Date((d.getFullYear() - 100), 1 - 1, 1),
                maxDate: new Date((d.getFullYear()), d.getMonth(), d.getDate()),
                dateFormat: "yy-mm-dd"
            });

            $("#txtdDate").datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: (d.getFullYear() - 100) + ':' + (d.getFullYear()),
                defaultDate: new Date((d.getFullYear()), d.getMonth(), d.getDate()),
                minDate: new Date((d.getFullYear() - 100), 1 - 1, 1),
                maxDate: new Date((d.getFullYear()), d.getMonth(), d.getDate())
            });


            $("#form1").validationEngine({ promptPosition: "topRight", autoHidePrompt: true, autoHideDelay: 3000, fadeDuration: 0.3 });
        })
    </script>
</asp:Content>
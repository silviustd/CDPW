<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="DUSAForm.aspx.cs" Inherits="CDPW.DUSAForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <asp:PlaceHolder ID="phUsForm" runat="server">
        <div id="top">
            <asp:PlaceHolder ID="phFormSave" runat="server" Visible="false">
                <p class="msg_box msg_info corners">
                    <asp:Literal ID="ltrFromSave" Text="Please Save first, then Print !" runat="server" />
                </p>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phError" runat="server" Visible="false">
                <p class="msg_box msg_error corners">
                    <asp:Literal ID="ltrError" runat="server" /></p>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phReproduction" runat="server" Visible="true">
                <p id="discl_USA" class="disclaimer-4">
                    This is a reproduction and it does not replace U.S. Customs and Border Protection
                    official form. <a class="open-1" style="cursor: pointer;">See more</a><a class="open-2"
                        style="cursor: pointer; display: none;">Hide</a><br />
                    It was done to ease filling out the information. For printing you have to use an
                    official empty form issued by U.S. Customs and Border Protection.
                </p>
            </asp:PlaceHolder>
        </div>
        <div class="main-3-container">
            <!-- front -->
            <div class="main-3">
                <div id="header-form">
                    &nbsp;</div>
                <div>
                </div>
                <h2>
                    Custom Declaration</h2>
                <p>
                    Each arriving traveler or responsible family member must provide the following information
                    (only ONE written declaration per family is required):</p>
                <table class="DUSAForm1">
                    <tr>
                        <td class="column-1">
                            1.
                        </td>
                        <td style="width: 40px;">
                            <asp:Label ID="lblFamName" AssociatedControlID="txtFamName" runat="server" Text="Family <strong>Name</strong>"></asp:Label>
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="txtFamName" runat="server" class="text text-1" MaxLength="39" original-title="39 characters"
                                AutoPostBack="False" onchange="javascript:ChangeAlert();" />
                        </td>
                    </tr>
                    <!--<tr>
				<td class="column-1">2.</td>
				<td><asp:Label ID="lblFName" AssociatedControlID="txtFName" runat="server" Text="First <em>(Given)</em>"></asp:Label></td>
				<td><asp:TextBox ID="txtFName" runat="server" class="text text-2" MaxLength="22" original-title="22 characters" AutoPostBack="False" onchange="javascript:ChangeAlert();"/></td>
				<td class="right"><asp:Label ID="lblMName" AssociatedControlID="txtMName" runat="server" Text="Middle"></asp:Label></td>
				<td colspan="2" class="right"><asp:TextBox ID="txtMName" runat="server" class="text text-3" MaxLength="10" original-title="10 characters" AutoPostBack="False" onchange="javascript:ChangeAlert();"></asp:TextBox></td>
			  </tr>-->
                    <tr>
                        <td colspan="6">
                            <table>
                                <tr>
                                    <td>
                                        <td class="column-1">
                                            2.
                                        </td>
                                        <td>
                                            <label id="lblFName" for="txtFName">
                                                First <em>(Given)</em></label>
                                        </td>
                                        <td>
                                            <input type="text" onchange="javascript:ChangeAlert();" original-title="22 characters"
                                                class="text text-3" id="txtFName" maxlength="22" name="ctl00$cphMain$txtFName">
                                        </td>
                                        <td class="right">
                                            <label id="lblMName" for="txtMName">
                                                Middle</label>
                                        </td>
                                        <td class="right" colspan="2">
                                            <input type="text" onchange="javascript:ChangeAlert();" original-title="10 characters"
                                                class="text text-2" id="txtMName" maxlength="10" name="ctl00$cphMain$txtMName">
                                        </td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="column-1">
                            &nbsp;
                        </td>
                        <td>
                            <strong>Birth date</strong>
                        </td>
                        <td colspan="4">
                            Day
                            <asp:TextBox ID="txtdobDay" runat="server" class="text validate[funcCall[checkBD]]"
                                MaxLength="2" original-title="dd" AutoPostBack="False" onchange="javascript:ChangeAlert();" />
                            Month
                            <asp:TextBox ID="txtdobMonth" runat="server" class="text validate[funcCall[checkBD]]"
                                MaxLength="2" original-title="mm" AutoPostBack="False" onchange="javascript:ChangeAlert();"></asp:TextBox>
                            Year
                            <asp:TextBox ID="txtdobYear" runat="server" class="text validate[funcCall[checkBD]]"
                                MaxLength="4" original-title="yyyy" AutoPostBack="False" onchange="javascript:ChangeAlert();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="column-1">
                            3.
                        </td>
                        <td colspan="3">
                            Number of <strong>family members</strong> traveling with you
                        </td>
                        <td colspan="2" class="right">
                            <asp:TextBox ID="txtNoFamMembers" runat="server" class="text text-3 validate[custom[byte]]"
                                original-title="3 digits" MaxLength="3" AutoPostBack="False" onchange="javascript:ChangeAlert();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="column-1">
                            4.
                        </td>
                        <td colspan="5">
                            U.S. Street <strong>Address</strong> (hotel name/ destination)
                        </td>
                    </tr>
                    <tr>
                        <td class="column-1">
                            &nbsp;
                        </td>
                        <td colspan="5" class="right">
                            <asp:TextBox ID="txtAddress" runat="server" class="text text-4" original-title="48 characters"
                                MaxLength="48" AutoPostBack="False" onchange="javascript:ChangeAlert();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="column-1">
                            &nbsp;
                        </td>
                        <td style="padding-left: 10px;">
                            City
                        </td>
                        <td>
                            <asp:TextBox ID="txtCity" runat="server" class="text text-2" original-title="25 characters"
                                MaxLength="25" AutoPostBack="False" onchange="javascript:ChangeAlert();"></asp:TextBox>
                        </td>
                        <td colspan="2" class="right">
                            State
                        </td>
                        <td class="right">
                            <asp:DropDownList ID="ddlState" runat="server" class="lista-2  text-5" AutoPostBack="False"
                                onchange="javascript:ChangeAlert();" />
                        </td>
                    </tr>
                    <tr>
                        <td class="column-1">
                            5.
                        </td>
                        <td colspan="2">
                            <strong>Passport issued by</strong> (country)
                        </td>
                        <td colspan="3" class="right">
                            <asp:DropDownList ID="ddlCountry" runat="server" class="lista-2  text-5" AutoPostBack="False"
                                onchange="javascript:ChangeAlert();" />
                        </td>
                    </tr>
                    <tr>
                        <td class="column-1">
                            6.
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblPasspNo" AssociatedControlID="txtPasspNo" runat="server" Text="<strong>Passport number</strong>"></asp:Label>
                        </td>
                        <td colspan="3" class="right">
                            <asp:TextBox ID="txtPasspNo" runat="server" class="text text-5" original-title="29 characters"
                                MaxLength="29" AutoPostBack="False" onchange="javascript:ChangeAlert();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="column-1">
                            7.
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblCountryResidence" AssociatedControlID="ddlCountryResidence" runat="server"
                                Text="Country of <strong>Residence</strong>" AutoPostBack="False" onchange="javascript:ChangeAlert();" />
                        </td>
                        <td colspan="3" class="right">
                            <asp:DropDownList ID="ddlCountryResidence" runat="server" class="lista-2" AutoPostBack="False"
                                onchange="javascript:ChangeAlert();" />
                        </td>
                    </tr>
                    <tr>
                        <td class="column-1">
                            8.
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblCountriesVisited" AssociatedControlID="txtCountriesVisited" runat="server"
                                Text="<strong>Countries visited</strong> on this trip prior to U.S. arrival"></asp:Label>
                        </td>
                        <td colspan="3" class="right">
                            <asp:TextBox ID="txtCountriesVisited" runat="server" class="text text-5" original-title="32 characters"
                                MaxLength="32" Style="margin-bottom: 4px;" AutoPostBack="False" onchange="javascript:ChangeAlert();"></asp:TextBox>
                            <asp:TextBox ID="txtCountriesVisited2" runat="server" class="text text-5" original-title="32 characters"
                                MaxLength="32" Visible="true" AutoPostBack="False" onchange="javascript:ChangeAlert();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="column-1">
                            9.
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblTripNo" AssociatedControlID="txtTripNo" runat="server" Text="<strong>Airline/Flight No.</strong> or <strong>Vessel Name</strong>"></asp:Label>
                        </td>
                        <td colspan="3" class="right">
                            <asp:TextBox ID="txtTripNo" runat="server" class="text text-5" original-title="9 characters/digits"
                                MaxLength="9" AutoPostBack="False" onchange="javascript:ChangeAlert();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="column-1">
                            10.
                        </td>
                        <td colspan="4">
                            <asp:Label ID="lblPrimaryPurpose" AssociatedControlID="rblPrimaryPurpose" runat="server"
                                Text="The primary purpose of this trip is business"></asp:Label>
                        </td>
                        <td class="right">
                            <asp:RadioButtonList ID="rblPrimaryPurpose" runat="server" RepeatDirection="Horizontal"
                                TextAlign="Left" RepeatLayout="Flow" AutoPostBack="False" onchange="javascript:ChangeAlert();">
                                <asp:ListItem Value="1" Text="Yes" />
                                <asp:ListItem Value="0" Text="No" 
                                 />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="column-1">
                            11.
                        </td>
                        <td colspan="4">
                            <asp:Label ID="Label7" runat="server" Text="I am (We are) bringing"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="column-1">
                            &nbsp;
                        </td>
                        <td colspan="4">
                            <asp:Label ID="lblBringFruits" AssociatedControlID="rblBringFruits" runat="server"
                                Text=" (a) fruits, vegetables, plants, seeds, food, insects:"></asp:Label>
                        </td>
                        <td class="right">
                            <asp:RadioButtonList ID="rblBringFruits" runat="server" RepeatDirection="Horizontal"
                                TextAlign="Left" RepeatLayout="Flow" AutoPostBack="False" onchange="javascript:ChangeAlert();">
                                <asp:ListItem Value="1" Text="Yes" />
                                <asp:ListItem Value="0" Text="No" />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="column-1">
                            &nbsp;
                        </td>
                        <td colspan="4">
                            <asp:Label ID="lblBringAnimal" AssociatedControlID="rblBringAnimal" runat="server"
                                Text="(b) meats, animals, or animal/wildlife products:"></asp:Label>
                        </td>
                        <td class="right">
                            <asp:RadioButtonList ID="rblBringAnimal" runat="server" RepeatDirection="Horizontal"
                                TextAlign="Left" RepeatLayout="Flow" AutoPostBack="False" onchange="javascript:ChangeAlert();">
                                <asp:ListItem Value="1" Text="Yes" />
                                <asp:ListItem Value="0" Text="No"  />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="column-1">
                            &nbsp;
                        </td>
                        <td colspan="4">
                            <asp:Label ID="lblBringCultures" AssociatedControlID="rblBringCultures" runat="server"
                                Text="(c) disease agents, cell cultures, or snails:"></asp:Label>
                        </td>
                        <td class="right">
                            <asp:RadioButtonList ID="rblBringCultures" runat="server" RepeatDirection="Horizontal"
                                TextAlign="Left" RepeatLayout="Flow" AutoPostBack="False" onchange="javascript:ChangeAlert();">
                                <asp:ListItem Value="1" Text="Yes" />
                                <asp:ListItem Value="0" Text="No"  />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="column-1">
                            &nbsp;
                        </td>
                        <td colspan="4">
                            <asp:Label ID="lblBringSoil" AssociatedControlID="rblBringSoil" runat="server" Text="(d) soil or have been on a farm/ranch/pasture:"></asp:Label>
                        </td>
                        <td class="right">
                            <asp:RadioButtonList ID="rblBringSoil" runat="server" RepeatDirection="Horizontal"
                                TextAlign="Left" RepeatLayout="Flow" AutoPostBack="False" onchange="javascript:ChangeAlert();">
                                <asp:ListItem Value="1" Text="Yes" />
                                <asp:ListItem Value="0" Text="No"  />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="column-1">
                            12.
                        </td>
                        <td colspan="4">
                            <asp:Label ID="lblProximityLivestock" AssociatedControlID="rblProximityLivestock"
                                runat="server" Text="I have (We have) been in close proximity of (such as touching and handling) <strong>livestock</strong>:"></asp:Label>
                        </td>
                        <td class="right">
                            <asp:RadioButtonList ID="rblProximityLivestock" runat="server" RepeatDirection="Horizontal"
                                TextAlign="Left" RepeatLayout="Flow" AutoPostBack="False" onchange="javascript:ChangeAlert();">
                                <asp:ListItem Value="1" Text="Yes" />
                                <asp:ListItem Value="0" Text="No"  />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="column-1">
                            13.
                        </td>
                        <td colspan="4">
                            <asp:Label ID="lblCurrencyOver" AssociatedControlID="rblCurrencyOver" runat="server"
                                Text="I am (We are) carrying <strong>currency or monetary instruments</strong> over $10,000 or foreign equivalent (see definition of monetray instruments on reverse):"></asp:Label>
                        </td>
                        <td class="right">
                            <asp:RadioButtonList ID="rblCurrencyOver" runat="server" RepeatDirection="Horizontal"
                                TextAlign="Left" RepeatLayout="Flow" AutoPostBack="False" onchange="javascript:ChangeAlert();">
                                <asp:ListItem Value="1" Text="Yes" />
                                <asp:ListItem Value="0" Text="No"  />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="column-1">
                            14.
                        </td>
                        <td colspan="4">
                            <asp:Label ID="lblCommercialMerchandise" AssociatedControlID="rblCommercialMerchandise"
                                runat="server" Text="I have (We  have) <strong>commercial merchandise</strong>: (articles for sale, samples used for soliciting orders, or goods that are not considered personal effects)"></asp:Label>
                        </td>
                        <td class="right">
                            <asp:RadioButtonList ID="rblCommercialMerchandise" runat="server" RepeatDirection="Horizontal"
                                TextAlign="Left" RepeatLayout="Flow" AutoPostBack="False" onchange="javascript:ChangeAlert();">
                                <asp:ListItem Value="1" Text="Yes" />
                                <asp:ListItem Value="0" Text="No"  />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="column-1">
                            15.
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblResTotValGoods" AssociatedControlID="txtResTotValGoods" runat="server"
                                Text="<strong>Residents</strong> - the <strong>total value of all goods</strong>, including commercial merchandise I/we have purchased or acquired abroad, (including gifts for someone else, but not items mailed to the U.S.) and  am/are bringing to the U.S. is:"></asp:Label>
                        </td>
                        <td colspan="2" class="right">
                            <asp:TextBox ID="txtResTotValGoods" runat="server" CssClass="text text-5 validate[funcCall[numValF]]"
                                original-title="######.#### (<6 digits>.<4 digits>)" MaxLength="12" AutoPostBack="False"
                                onchange="javascript:ChangeAlert();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="column-1">
                            &nbsp;
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblVisTotValGoods" AssociatedControlID="txtVisTotValGoods" runat="server"
                                Text="<strong>Visitors</strong> - the <strong>total value of all articles</strong> that will remain in the U.S., including commercial merchandise is:"></asp:Label>
                        </td>
                        <td colspan="2" class="right">
                            <asp:TextBox ID="txtVisTotValGoods" runat="server" class="text text-5 validate[funcCall[numValF]]"
                                original-title="######.#### (<6 digits>.<4 digits>)" MaxLength="12" AutoPostBack="False"
                                onchange="javascript:ChangeAlert();"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <p>
                    <asp:Label ID="lblIndications" runat="server" Text="Read the instructions on the back of this form. Space is provided to list all the items you must declare.<br /><strong>I HAVE READ THE IMPORTANT INFORMATION ON THE REVERSE SIDE OF THIS FORM AND HAVE MADE A TRUTHFUL DECLARATION</strong>"></asp:Label></p>
                <p>
                    <asp:Label ID="Label18" runat="server" Text="_________________________________________"></asp:Label>
                    <span style="margin-left: 10px;">
                        <asp:TextBox ID="txtSignDate" runat="server" class="text validate[custom[date2]] datepicker"
                            Columns="10" MaxLength="10" Width="100" original-title="Date(day/month/year)"
                            AutoPostBack="False" onchange="javascript:ChangeAlert();" />
                    </span>
                    <br />
                    (Signature) <span style="margin-left: 234px;">Date(day/month/year)</span>
                </p>
            </div>
            <!-- back -->
            <div class="main-3 back">
                <p style="font-size: 11px;">
                    <asp:Label ID="lblIntroductions1" runat="server" Text="U.S. Customs and Border Protection is responsible for protecting the United States against the illegal importation of prohibited items. CBP officers have the authority to question you and to examine you and your personal property. If you are one of the travelers selected for an examination, you will be treated in a courteous, professional, and dignified manner. CBP Supervisors and Passenger Service Representatives are available to answer your questions. Comment cards are available to compliment or provide feedback."></asp:Label></p>
                <p style="font-size: 11px;">
                    <asp:Label ID="Label20" runat="server" Text="<strong>Important Information</strong> <br />U.S. Residents - Declare all articles that you have acquired abroad and are bringing into the United States.<br />Visitors (Non-Residents) - Declare the value of all articles that will remain in the United States.<br />Declare all articles on this declaration form and show the value in U.S. dollars. For gifts, please indicate the retail value.<br />Duty - CBP officers will determine duty. U.S. residents are normally entitled to a duty-free exemption of $800 on items accompanying them. Visitors (nonresidents) are normally entitled to an exemption of $100. Duty will be assessed at the current rate on the first $1,000 above the exemption.<br />Agricultural and Wildlife Products - To prevent the entry of dangerous agricultural pests and prohibited wildlife, the following are restricted: Fruits, vegetables, plants, plant products, soil, meat, meat products, birds, snails, and other live animals or animal products. Failure to declare such items to a Customs and Border Protection Officer/Customs and Border Protection Agriculture Specialist/Fish and Wildlife Inspector can result in penalties and the items may be subject to seizure. <br />Controlled substances, obscene articles, and toxic substances are generally prohibited entry.<br /><em>Thank You, and Welcome to the United States.</em><br />The transportation of currency or monetary instruments, regardless of the amount, is legal. However, if you bring in to or take out of the United States more than $10,000 (U.S. or foreign equivalent, or a combination of both), you are required by law to file a report on FinCEN 105 (formerly Customs Form 4790) with U.S. Customs and Border Protection. Monetary instruments include coin, currency, travelers checks and bearer instruments such as personal or cashiers checks and stocks and bonds. If you have someone else carry the currency or monetary instrument for you, you must also file a report on FinCEN 105. Failure to file the required report or failure to report the total amount that you are carrying may lead to the seizure of all the currency or monetary instruments, and may subject you to civil penalties and/or criminal prosecution. SIGN ON THE OPPOSITE 
		SIDE OF THIS FORM AFTER YOU HAVE READ I'IIE IMPORTANT INFORMATION ABOVE AND MADE A TRUTHFUL DECLARATION."></asp:Label>
                    <asp:Label ID="lblInstructions1" runat="server" Text="Controlled substances, obscene articles, and toxic substances are generally prohibited entry. 
		Thank You, and Welcome to the United States."></asp:Label>
                    <asp:Label ID="lblInstructions2" runat="server" Text="The transportation of currency or monetary instruments, regardless of the amount, is legal. However, if you bring in to or take out of the United States more than $10,000 (U.S. or foreign equivalent, or a combination of both), you are required by law to file a report on FinCEN 105 (formerly Customs Form 4790) with U.S. Customs and Border Protection. Monetary instruments include coin, currency, travelers checks and bearer instruments such as personal or cashiers checks and stocks and bonds. If you have someone else carry the currency or monetary instrument for you, you must also file a report on FinCEN 105. Failure to file the required report or failure to report the total amount that you are carrying may lead to the seizure of all the currency or monetary instruments, and may subject you to civil penalties and/or criminal prosecution. SIGN ON THE OPPOSITE 
		SIDE OF THIS FORM AFTER YOU HAVE READ THE IMPORTANT INFORMATION ABOVE AND MADE A TRUTHFUL DECLARATION">.</asp:Label></p>
                <br />
                <table class="DUSAForm1">
                    <tr>
                        <td style="width: 290px;">
                            <strong>Description of Articles </strong>
                            <br />
                            (List may continue on another CBP Form 6059B)
                        </td>
                        <td style="width: 100px;">
                            <strong>Value</strong>
                        </td>
                        <td>
                            <strong>CBP</strong><br />
                            Use Only
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtDescrArticles1" runat="server" class="text text-7" original-title="37 characters"
                                MaxLength="37" AutoPostBack="False" onchange="javascript:ChangeAlert();" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtArticlesValue1" runat="server" class="text-10 validate[funcCall[numVal]]"
                                original-title="######.#### (<6 digits>.<4 digits>)" MaxLength="12" AutoPostBack="False" />
                        </td>
                        <td rowspan="7">
                            <asp:TextBox ID="TextBox21" runat="server" class="text-11" disabled original-title="US CBP Use Only"
                                AutoPostBack="False" onchange="javascript:ChangeAlert();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtDescrArticles2" runat="server" class="text text-7" original-title="37 characters"
                                MaxLength="37" AutoPostBack="False" onchange="javascript:ChangeAlert();" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtArticlesValue2" runat="server" class="text-10 validate[funcCall[numVal]]"
                                original-title="######.#### (<6 digits>.<4 digits>)" MaxLength="12" AutoPostBack="False"
                                onchange="javascript:ChangeAlert();" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtDescrArticles3" runat="server" class="text text-7" original-title="37 characters"
                                MaxLength="37" AutoPostBack="False" onchange="javascript:ChangeAlert();" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtArticlesValue3" runat="server" class="text-10 validate[funcCall[numVal]]"
                                original-title="######.#### (<6 digits>.<4 digits>)" MaxLength="12" AutoPostBack="False"
                                onchange="javascript:ChangeAlert();" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtDescrArticles4" runat="server" class="text text-7" original-title="37 characters"
                                MaxLength="37" AutoPostBack="False" onchange="javascript:ChangeAlert();" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtArticlesValue4" runat="server" class="text-10 validate[funcCall[numVal]]"
                                original-title="######.#### (<6 digits>.<4 digits>)" MaxLength="12" AutoPostBack="False"
                                onchange="javascript:ChangeAlert();" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtDescrArticles5" runat="server" class="text text-7" original-title="37 characters"
                                MaxLength="37" AutoPostBack="False" onchange="javascript:ChangeAlert();" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtArticlesValue5" runat="server" class="text-10 validate[funcCall[numVal]]"
                                original-title="######.#### (<6 digits>.<4 digits>)" MaxLength="12" AutoPostBack="False"
                                onchange="javascript:ChangeAlert();" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtDescrArticles6" runat="server" class="text text-7" original-title="37 characters"
                                MaxLength="37" AutoPostBack="False" onchange="javascript:ChangeAlert();" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtArticlesValue6" runat="server" class="text-10 validate[funcCall[numVal]]"
                                original-title="######.#### (<6 digits>.<4 digits>)" MaxLength="12" AutoPostBack="False"
                                onchange="javascript:ChangeAlert();" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            Total
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtTotalValArticles" runat="server" disabled class="text-10 validate[custom[number]]"
                                original-title="######.#### (<6 digits>.<4 digits>)" AutoPostBack="False" onchange="javascript:ChangeAlert();"
                                Width="205"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <p style="font-size: 11px;">
                    <asp:Label ID="Label23" runat="server" Text="PAPERWORK REDUCTION ACT NOTICE: The Paperwork Reduction Act says we must tell you why we are collecting this information, how we will use it, and whether you have to give it to us. The information collected on this form is needed to carry out the Customs. Agriculture, and currency laws of the United States. CBP requires the information on this form to insure that travelers are complying with these laws and to allow us to figure and collect the right amount of duty and tax Your response is mandatory An agency may not conduct or sponsor and a person is not required to respond to a collection of information. unless it displays a valid OMB control number. The estimated average burden associated with this collection of information is 4 minutes per respondent or record keeper depending on individual circumstances. Comments concerning the accuracy of this burden estimate and suggestions for reducing this burden should be directed to U.S. Customs and Border Protection. Reports Clearance Officer, Information Services Branch. Washington DC 20229, and to the Office of Management and Budget, Paperwork Reduction Project (1651-0009), Washington, DC 20503 THIS FORM MAY NOT BE REPRODUCED WITHOUT APPROVAL FROM THE CBP FORMS MANAGER. G:Y> U.S. G.P.O.: 2011-570-089/40009 CBP Form 6059B (10/07)"></asp:Label></p>
            </div>
        </div>
        <p class="disclaimer-3">
            This is a reproduction and it does not replace U.S. Customs and Border Protection
            official form.</p>
        <div id="button-form-3">
            <%--
			<asp:LinkButton ID="btnSaveForm" Text="Save" OnClick="btnSaveForm_Click" runat="server" />
            --%>
            <asp:Button ID="btnSaveForm" Text="Save" OnClick="btnSaveForm_Click" runat="server" />
            <%--<a href="#" id="save-2">Save</a> --%>
            <span id="print-2-container"><a href="#" id="print-2">Print</a>
                <ul id="print-choice-bottom">
                    <li>
                        <input type="checkbox" id="opt11" name="print-choice" value="Option 1" />
                        Custom Declaration - Page 1</li>
                    <li>
                        <input type="checkbox" id="opt22" name="print-choice" value="Option 2" />
                        Custom Declaration - Page 2</li>
                    <li class="print-button"><a href="#" onclick="IfPrint();">Print</a></li>
                </ul>
            </span>
            <%--
			<a href="cdpmysettings.aspx" id="my_settings_2">My Settings</a> 
			<a href="cdplogout.aspx" id="logout-2">Logout</a>
            --%>
            <asp:HyperLink ID="my_settings_2" runat="server" NavigateUrl="cdpmysettings.aspx">My Settings</asp:HyperLink>
            <asp:HyperLink ID="logout_2" runat="server" NavigateUrl="cdplogout.aspx">Logout</asp:HyperLink>
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phUSAFormSuccess" runat="server" Visible="false"><span class="span50">
    </span>
        <p class="msg_box msg_ok corners">
            <asp:Literal ID="ltrUSAFormSuccess" Text="The form was submited with success." runat="server" />
        </p>
    </asp:PlaceHolder>
    <script type="text/javascript">
        $(function () {
            $('input.text').tipsy({ gravity: 's' });
        });

        $(document).ready(function () {
            $('.open-1').click(function () {
                $('p.disclaimer-4').css('height', '35px').slideDown('slow');
                $('a.open-1').css('display', 'none').fadeOut("slow");
                $('a.open-2').css('display', 'inline').fadeIn("slow");
            });
            $('.open-2').click(function () {
                $('p.disclaimer-4').css('height', '10px').slideDown('slow');
                $('a.open-1').css('display', 'inline').fadeIn("slow");
                $('a.open-2').css('display', 'none').fadeOut("slow");
            });
            var d = new Date();
            $("#txtdobDay").datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: '-99Y' + ':' + (d.getFullYear()),
                minDate: '-99Y',
                maxDate: 0,
                dateFormat: "dd/mm/yy",
                altField: "#txtdobMonth",
                altFormat: "mm",
                onSelect: function (dateText, picker) {
                    $('#txtdobDay').val('');
                    $('#txtdobDay').val(dateText.split(/\//)[0]);
                    $('#txtdobYear').val(dateText.split(/\//)[2]);
                    ChangeAlert();
                }
            });

            $("#txtdobMonth").datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: '-99Y' + ':' + (d.getFullYear()),
                minDate: '-99Y',
                maxDate: 0,
                dateFormat: "dd/mm/yy",
                altField: "#txtdobDay",
                altFormat: "dd",
                onSelect: function (dateText, picker) {
                    $('#txtdobMonth').val('');
                    $('#txtdobMonth').val(dateText.split(/\//)[1]);
                    $('#txtdobYear').val(dateText.split(/\//)[2]);
                    ChangeAlert();
                }
            });

            $("#txtdobYear").datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: '-99Y' + ':' + (d.getFullYear()),
                minDate: '-99Y',
                maxDate: 0,
                dateFormat: "dd/mm/yy",
                altField: "#txtdobDay",
                altFormat: "dd",
                onSelect: function (dateText, picker) {
                    $('#txtdobYear').val('');
                    $('#txtdobYear').val(dateText.split(/\//)[2]);
                    $('#txtdobMonth').val(dateText.split(/\//)[1]);
                    ChangeAlert();
                }
            });

            $("#txtSignDate").datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: '-99Y' + ':' + (d.getFullYear()),
                minDate: '-99Y',
                maxDate: 0,
                dateFormat: "dd/mm/yy"
            });


            //			$("#txtArticlesValue1").blur(function () {
            //				var suma = 0.00;

            //				suma = $("#txtArticlesValue1").val();
            //				$("#txtTotalValArticles").val() = suma.toFixed(2);
            //			});

            //		    function CalcValue() {
            //		        var suma = 0.00;

            //		        suma = $("#txtArticlesValue1").val();
            //		        //                +$("#txtArticlesValue2").val() + $("#txtArticlesValue3").val();
            //		        //		        suma = suma + $("#txtArticlesValue4").val() + $("#txtArticlesValue5").val() + $("#txtArticlesValue6").val();

            //		        alert("Suma: " + suma);

            //		        $("#txtTotalValArticles").val() = suma.toFixed(2);
            //		    }

            $("#form1").validationEngine({ promptPosition: "topRight", autoHidePrompt: true, autoHideDelay: 3000, fadeDuration: 0.3 });
        });
    </script>
    <script type="text/javascript">

        //        var purl = window.location.href;
        //        if (purl.indexOf("DUSAForm.aspx") > 0) {
        //            for (var i = 1; i < 7; i++) {
        //                var ctrl = "txtArticlesValue" + i;
        //                document.getElementById(ctrl).onblur = function () { sumVal() };
        //            }
        //        }

        function sumVal() {

            var artTotal = new Number(0);
            var isValue = new Boolean(false);

            for (var i = 1; i < 7; i++) {
                var ctrl = "txtArticlesValue" + i;
                if (!isNaN(document.getElementById(ctrl).value)) {
                    artTotal = artTotal + Number(document.getElementById(ctrl).value);
                    if (document.getElementById(ctrl).value.length > 0) { isValue = true; }
                }
            }

            if (Number(isValue) > 0) {
                document.getElementById("txtTotalValArticles").value = artTotal.toString();
            }
            else {
                document.getElementById("txtTotalValArticles").value = '';
            }
        }

        //        function checkBD() {

        //            var sBD = document.getElementById("txtdobYear") + "/" + document.getElementById("txtdobMonth") + "/" + document.getElementById("txtdobDay");
        //            var pattern = new RegExp(/^(\d{4})[\/\-\.](0?[1-9]|1[012])[\/\-\.](0?[1-9]|[12][0-9]|3[01])$/);
        //            var match = pattern.exec(sBD);
        //            if (match == null)
        //                return false;

        //            var year = match[1];
        //            var month = match[2] * 1;
        //            var day = match[3] * 1;
        //            var date = new Date(year, month - 1, day); // because months starts from 0.

        //            if (!(date.getFullYear() == year && date.getMonth() == (month - 1) && date.getDate() == day)) {
        //                return "Not a valid date";
        //            }
        //        }


        function checkBD(field, rules, i, options) {
            var dyear = $("#txtdobYear").val();
            var dmonth = $("#txtdobMonth").val();
            var dday = $("#txtdobDay").val();
            var dat = new Date(dyear, dmonth, dday);
            var dd = dmonth + "/" + dday + "/" + dyear;

            if (!esteData(dd)) {
                return options.allrules.date2.alertText;
            }
        }

        function numVal(field, rules, i, options) {
            var fId = field[0].id;
            var numf = document.getElementById(fId).value;
            //var numf = document.getElementById("txtArticlesValue1").value;
            //var numf = field.val();
            //alert($.number(numf, 2))

            var nA = numf.split(",");
            var nS = "";
            for (i = 0; i < nA.length; i++) {
                nS = nS + nA[i];
            }

            if (!isNaN(nS)) {
                var nN = new Number(nS);
                document.getElementById(field[0].id).value = $.number(nN, 2);
                //field.value = $.number(nN, 2);
                if (!(nN >= 0 && nN <= 214748.3647)) {
                    return "The value should be between 0 and 214,748.3647";
                }
            }
            else {
                return "The value should be between 0 and 214,748.3647";
            }


            var artTotal = new Number(0);
            var isValue = new Boolean(false);

            for (var i = 1; i < 7; i++) {
                var ctrl = "txtArticlesValue" + i;
                if (!isNaN(jsnumber(document.getElementById(ctrl).value))) {
                    if (document.getElementById(ctrl).value.length > 0) {
                        isValue = true;
                        artTotal = artTotal + jsnumber(document.getElementById(ctrl).value);
                    }
                }
            }

            if (Number(isValue) > 0) {
                document.getElementById("txtTotalValArticles").value = $.number(artTotal.toString(), 2);
            }
            else {
                document.getElementById("txtTotalValArticles").value = '';
            }

        }

        function jsnumber(numf) {

            var nA = numf.split(",");
            var nS = "";
            for (i = 0; i < nA.length; i++) {
                nS = nS + nA[i];
            }

            return new Number(nS);
        }
        
    </script>
</asp:Content>

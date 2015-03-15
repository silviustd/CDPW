<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="cdphelp.aspx.cs" Inherits="CDPW.cdphelp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div id="help-wrapper" style='font-size: 14px; color:#3D3D3D;'>
    <ul id="help-menu">
        <li><a href="#register">Register</a></li>
        <li><a href="#translate">Translate</a></li>
        <li><a href="#customs_forms">Customs Forms</a></li>
        <li><a href="#print_custom_forms">Print Custom Forms</a></li>
        <li><a href="#my_settings">My Settings</a></li>
    </ul>
    <h2><a name="register">Why Register ?</a></h2>
    <p>By registering you will save time; your data will be saved an make it available for future usage of the application. 
    <br />For each border crossing, the application will auto fill your personal data, and you simply input what you declare each trip. 
    <br /> When registered, you can enroll in a plan that will allow printing an unlimited number of Customs Forms for the duration of the plan.</p>
    <h2>Not Registered ?</h2>
    <p><span class="special-2">Continue without Login</span> or <span class="special-2">Sign Up</span> allows the application to be used if you choose to not register or even not to log in (although registered).</p>
    <p>A temporary user will be created and you will only have the option to fill out and print a Custom Form (US CBP Form 6059b or CBSA Form E311), one time only.</p>
    <p>Any data entered during a <span class="special-2">Continue without Login</span> or <span class="special-2">Sign Up</span> session will be deleted.</p>
    <h2><a name="translate">Translate</a></h2>
    <p>The translate widget can be used to translate the current page in various languages.<br />At any time you can return to the original page by closing or canceling the widget.<br />The widget can be closed by clicking <span class="special-2">Close [x]</span> on the widget or by clicking X on the widget bar.</p>
    <div></div>
    <p><img style="float: left; margin-right: 20px;" src="theme/clasic/images/Translate_1.png" alt="" /><img src="theme/clasic/images/Translate_2.png" alt="" /></p>
    
    
    <h2><a name="customs_forms">Customs Forms</a></h2>
    <p>Customs Forms page allows one to choose the Customs Form (US CBP Form 6059b or CBSA Form E311) wanted to be filled out and printed.</p>
    <p>If registered, there is also the option of enrolling in a plan for one of the Customs Forms(US CBP Form 6059b or CBSA Form E311).</p>
    <p> More information about how the form should be filled out:</p>
    <p class="special-2">US CBP 6059b</p>
    <p><a href="http://www.cbp.gov/xp/cgov/travel/vacation/sample_declaration_form.xml">http://www.cbp.gov/xp/cgov/travel/vacation/sample_declaration_form.xml</a></p>
    <p><a href="http://www.cbp.gov/linkhandler/cgov/travel/clearing/traveler_entry_form/cbp_traveler_brochure.ctt/cbp_traveler_brochure.pdf" target = "_blank"></a>http://www.cbp.gov/linkhandler/cgov/travel/clearing/traveler_entry_form/cbp_traveler_brochure.ctt/cbp_traveler_brochure.pdf</p>

    <p class="special-2">CBSA E311</p>
    <p><a href="http://www.cbsa-asfc.gc.ca/publications/dm-md/d2/d2-5-7-eng.html" target = "_blank">http://www.cbsa-asfc.gc.ca/publications/dm-md/d2/d2-5-7-eng.html</a></p>
    <p><a href="http://www.cbsa-asfc.gc.ca/publications/forms-formulaires/e311-eng.pdf" target = "_blank">http://www.cbsa-asfc.gc.ca/publications/forms-formulaires/e311-eng.pdf</a></p>
    <p><a href="http://www.cbsa-asfc.gc.ca/publications/dm-md/d2/d2-5-7-fra.html" target = "_blank">http://www.cbsa-asfc.gc.ca/publications/dm-md/d2/d2-5-7-fra.html</a></p>
    <p><a href="http://www.cbsa-asfc.gc.ca/publications/forms-formulaires/e311-fra.pdf" target = "_blank">http://www.cbsa-asfc.gc.ca/publications/forms-formulaires/e311-fra.pdf</a></p>

    <h2><a name="print_custom_forms">Print Custom Forms</a></h2>
    <p>To print Custom Forms you have to use Internet Explorer browser</p>
    <p>The browser should allow pop-ups</p>
    <p><img src="theme/clasic/images/IE_Allow_popups1.jpg" alt="" /></p>
    

    <p>If requested, allow to install SQL Server Reporting Services, an add-on provided by Microsoft that enable the reports’ printing</p>
    <p><img src="theme/clasic/images/Install_SQLServerReportingServices2008R2.jpg" alt="" /></p>
    <p><img src="theme/clasic/images/Install_SQLServerReportingServices2008R2_2.jpg" alt="" /></p>

    <h2><a name="my_settings">My Settings</a></h2>
    <p>My Settings page allows editing of:</p>
    <p><span class="special-2">Account Info:</span> Username, Email, Alternate Email, Password. With Email and Password being mandatory information</p>
    <p><span class="special-2">General Info:</span> Salutation, First, Middle and Last Name, Date of Birth and Gender</p>
    <p><span class="special-2">Residential Address:</span> Street Number, Street Type, Street Name, Street Direction, Unit/Apt Number, City, Postal Code, Province/State and Country</p>

    <p><span class="special-2">Do not keep data:</span> If checked, certain information filled in using My Settings or Customs Forms will be deleted at log out. The information that will be deleted are also listed on My Settings page and they are - Passport, Address, Date of Birth and Citizenship.</p>
    <p><span class="special-2">Update address for Customs Forms:</span> If checked, the address information filled in on this page will be used on Customs Forms, where residential address information is required to be filled out.</p>

    

</div>

</asp:Content>

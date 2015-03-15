using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CDPW.DAL;
using CDPW.BLL;

namespace CDPW
{
    public partial class DUSAForm : System.Web.UI.Page, System.Web.UI.ICallbackEventHandler
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected String returnValue;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");

            try
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["cdpUser"] != null)
                {
                    //register client scripts for callback
                    String cbReference =
                                Page.ClientScript.GetCallbackEventReference(this,
                                "arg", "ReceiveServerData", "context");
                    String callbackScript;
                    callbackScript = "function CallServer(arg, context)" +
                        "{ " + cbReference + ";}";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(),
                        "CallServer", callbackScript, true);

                    CurrentUser uLogin = (CurrentUser)HttpContext.Current.Session["cdpUser"];

                    if (uLogin.NoSignUp)
                    {
                        my_settings_2.Visible = false;
                        logout_2.Visible = false;
                    }  

                    if (!Page.IsPostBack)
                    {
                        Int64 UserID = uLogin.WAppUserId;
                        Int64 USPId = uLogin.PId_USA;

                        //ltrError.Text = string.Format("UserID: {0}<br />Username: {1}<br />PersonId: {2}<br />Nume: {3} {4} {5}", uLogin.WAppUserId, uLogin.WAppUserName, uLogin.WPersonId, uLogin.WPLastName, uLogin.WPFirstName, uLogin.WPMiddleName);
                        //phError.Visible = true;

                        DataSet dsUSA = CDPW.DAL.FormUSA.Form_Get(UserID, USPId);

                        if (dsUSA.Tables[0].Rows.Count < 1)
                        {
                            Helpers.Lists(ddlCountry, string.Empty, Lists.Countries(), "CountryName", "CountryId");
                        }
                        else
                        {
                            // person data
                            foreach (DataRow dr in dsUSA.Tables[0].Rows)
                            {
                                USPId = String.IsNullOrEmpty(dr["PersonId"].ToString()) ? 0 : (Int64)dr["PersonId"];
                                txtFamName.Text = dr["LastName"].ToString().Trim();
                                txtFName.Text = dr["FirstName"].ToString().Trim();
                                txtMName.Text = dr["MiddleName"].ToString().Trim();
                                if (!string.IsNullOrEmpty(dr["DateofBirth"].ToString()))
                                {
                                    DateTime dt = DateTime.Parse(dr["DateofBirth"].ToString());

                                    txtdobMonth.Text = dt.ToString("MM");
                                    txtdobDay.Text = dt.ToString("dd");
                                    txtdobYear.Text = dt.Year.ToString();
                                }

                                txtPasspNo.Text = dr["PassportNo"].ToString().Trim();
                                Helpers.Lists(ddlCountry, dr["CountryId"].ToString(), Lists.Countries(), "CountryName", "CountryId");
                            }
                        }

                        //add PIds to ulogin and then in seesion
                        uLogin.PId_USA = USPId;
                        HttpContext.Current.Session.Add("cdpUser", uLogin);

                        // address
                        if (dsUSA.Tables[1].Rows.Count < 1)
                        {
                            Helpers.Lists(ddlState, string.Empty, Lists.Regions(254), "AdministrativeRegion", "AdministrativeRegionId");
                        }
                        else
                        {
                            foreach (DataRow dr in dsUSA.Tables[1].Rows)
                            {
                                Int32 AdminRegId = String.IsNullOrEmpty(dr["AdminRegionID"].ToString()) ? 0 : (Int32)dr["AdminRegionID"];

                                Helpers.Lists(ddlState, AdminRegId.ToString(), Lists.Regions(254), "AdministrativeRegion", "AdministrativeRegionId");
                                txtAddress.Text = dr["StreetName"].ToString();
                                txtCity.Text = dr["City"].ToString();
                            }
                        }

                        if (dsUSA.Tables[2].Rows.Count < 1)
                        {
                            Helpers.Lists(ddlCountryResidence, string.Empty, Lists.Countries(), "CountryName", "CountryId");
                        }
                        else
                        {
                            foreach (DataRow dr in dsUSA.Tables[2].Rows)
                            {
                                Helpers.Lists(ddlCountryResidence, dr["CountryResidence"].ToString(), Lists.Countries(), "CountryName", "CountryId");
                                txtNoFamMembers.Text = dr["NoFamMembers"].ToString();
                                txtCountriesVisited.Text = dr["ContriesVisited"].ToString();
                                txtCountriesVisited2.Text = dr["CountriesVisited2"].ToString();
                                txtTripNo.Text = dr["TransportationId"].ToString();
                                rblPrimaryPurpose.SelectedValue = TextUtils.ReturnBitFromDB(Convert.ToBoolean(String.IsNullOrEmpty(dr["TripPPurposeBusiness"].ToString()) ? false : dr["TripPPurposeBusiness"]));
                                rblBringFruits.SelectedValue = TextUtils.ReturnBitFromDB(Convert.ToBoolean(String.IsNullOrEmpty(dr["BringFruits"].ToString()) ? false : dr["BringFruits"]));
                                rblBringAnimal.SelectedValue = TextUtils.ReturnBitFromDB(Convert.ToBoolean(String.IsNullOrEmpty(dr["BringMeats"].ToString()) ? false : dr["BringMeats"]));
                                rblBringCultures.SelectedValue = TextUtils.ReturnBitFromDB(Convert.ToBoolean(String.IsNullOrEmpty(dr["BringDiseaseAgents"].ToString()) ? false : dr["BringDiseaseAgents"]));
                                rblBringSoil.SelectedValue = TextUtils.ReturnBitFromDB(Convert.ToBoolean(String.IsNullOrEmpty(dr["BringSoil"].ToString()) ? false : dr["BringSoil"]));
                                rblProximityLivestock.SelectedValue = TextUtils.ReturnBitFromDB(Convert.ToBoolean(String.IsNullOrEmpty(dr["Livestock"].ToString()) ? false : dr["Livestock"]));
                                rblCurrencyOver.SelectedValue = TextUtils.ReturnBitFromDB(Convert.ToBoolean(String.IsNullOrEmpty(dr["CurrencyValue"].ToString()) ? false : dr["CurrencyValue"]));
                                rblCommercialMerchandise.SelectedValue = TextUtils.ReturnBitFromDB(Convert.ToBoolean(String.IsNullOrEmpty(dr["CommercialMerchandise"].ToString()) ? false : dr["CommercialMerchandise"]));
                                txtResTotValGoods.Text = string.Format(new System.Globalization.CultureInfo("en-US"), "{0:#.00}", dr["RGoodsValue"]);
                                txtVisTotValGoods.Text = string.Format(new System.Globalization.CultureInfo("en-US"), "{0:#.00}", dr["VGoodsValue"]); //dr["VGoodsValue"].ToString();

                                txtSignDate.Text = string.IsNullOrEmpty(dr["TripDate"].ToString()) ? string.Empty : TextUtils.ReturnFromDB(dr["TripDate"].ToString(), false, false, true);

                                string strrr = string.Format("Business Purpose: {0}<br />Brong Fruits: {1}<br />Bring Animal: {2}<br />Bring Disease: {3}<br />Bring Soil: {4}<br />Proximity LIvestock: {5}<br />Currency Over: {6}<br />Commercial: {7}<br />", dr["TripPPurposeBusiness"].ToString(), dr["BringFruits"].ToString(), dr["BringMeats"].ToString(), dr["BringDiseaseAgents"].ToString(), dr["BringSoil"].ToString(), dr["Livestock"].ToString(), dr["CurrencyValue"].ToString(), dr["CommercialMerchandise"].ToString());

                                ltrError.Text += strrr;
                            }
                        }

                        Boolean isValue = false;
                        // articles
                        if (dsUSA.Tables[3].Rows.Count > 0)
                        {
                            int jj = 1;
                            foreach (DataRow dr in dsUSA.Tables[3].Rows)
                            {
                                switch (jj)
                                {
                                    case 1:
                                        txtDescrArticles1.Text = dr["WTripInfoDetail"].ToString();
                                        txtArticlesValue1.Text = string.Format(new System.Globalization.CultureInfo("en-US"), "{0:#.00}", dr["WTripInfoDetailValue"]);
                                        if (!String.IsNullOrEmpty(txtArticlesValue1.Text)) { isValue = true; };
                                        break;
                                    case 2:
                                        txtDescrArticles2.Text = dr["WTripInfoDetail"].ToString();
                                        txtArticlesValue2.Text = string.Format(new System.Globalization.CultureInfo("en-US"), "{0:#.00}", dr["WTripInfoDetailValue"]);
                                        if (!String.IsNullOrEmpty(txtArticlesValue2.Text)) { isValue = true; };
                                        break;
                                    case 3:
                                        txtDescrArticles3.Text = dr["WTripInfoDetail"].ToString();
                                        txtArticlesValue3.Text = string.Format(new System.Globalization.CultureInfo("en-US"), "{0:#.00}", dr["WTripInfoDetailValue"]);
                                        if (!String.IsNullOrEmpty(txtArticlesValue3.Text)) { isValue = true; };
                                        break;
                                    case 4:
                                        txtDescrArticles4.Text = dr["WTripInfoDetail"].ToString();
                                        txtArticlesValue4.Text = string.Format(new System.Globalization.CultureInfo("en-US"), "{0:#.00}", dr["WTripInfoDetailValue"]);
                                        if (!String.IsNullOrEmpty(txtArticlesValue4.Text)) { isValue = true; };
                                        break;
                                    case 5:
                                        txtDescrArticles5.Text = dr["WTripInfoDetail"].ToString();
                                        txtArticlesValue5.Text = string.Format(new System.Globalization.CultureInfo("en-US"), "{0:#.00}", dr["WTripInfoDetailValue"]);
                                        if (!String.IsNullOrEmpty(txtArticlesValue5.Text)) { isValue = true; };
                                        break;
                                    case 6:
                                        txtDescrArticles6.Text = dr["WTripInfoDetail"].ToString();
                                        txtArticlesValue6.Text = string.Format(new System.Globalization.CultureInfo("en-US"), "{0:#.00}", dr["WTripInfoDetailValue"]);
                                        if (!String.IsNullOrEmpty(txtArticlesValue6.Text)) { isValue = true; };
                                        break;
                                }

                                jj++;
                            }
                        }

                        if (dsUSA.Tables[4].Rows.Count > 0)
                        {
                            if (isValue)
                            {
                                txtTotalValArticles.Text = string.Format(new System.Globalization.CultureInfo("en-US"), "{0:#.00}", dsUSA.Tables[4].Rows[0]["TotalTripInfoValue"]);
                            }
                            else
                            {
                                txtTotalValArticles.Text = String.Empty;
                            }
                        }
                    }

                    if (!uLogin.SavedDUSA)
                    {
                        if (!String.IsNullOrEmpty(Request.QueryString["saved"]) && !Boolean.Parse(Request.QueryString["saved"]))
                        {
                            phFormSave.Visible = true;
                            phReproduction.Visible = false;
                        }
                    }

                    if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
                }
                else
                {
                    if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Redirect to Login");
                    Response.Redirect("cdplogin.aspx", true);
                }
                

            }
            catch (Exception ex)
            {
                components.Error_Show.Show(phError, true, ltrError, ex, phReproduction, true);
                if (log.IsErrorEnabled) log.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Error", ex);
            }



        }

        // SAVE the form info
        protected void btnSaveForm_Click(Object sender, EventArgs e)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            try
            {
                // throw new Exception("testing");

                if (HttpContext.Current.Session != null && HttpContext.Current.Session["cdpUser"] != null && HttpContext.Current.Session["cdpUser"] is CurrentUser)
                {
                    using (System.Transactions.TransactionScope tran = new System.Transactions.TransactionScope())
                    {
                        CurrentUser uLogin = (CurrentUser)HttpContext.Current.Session["cdpUser"];
                        Int64 UserID = uLogin.WAppUserId;

                        // Form - add REGION, CITY, ADDRESS, WTripInfoUSA
                        string TripInfo_Address = String.Empty;

                        TripInfo_Address = CDPW.DAL.FormUSA.Form_AddInfo(UserID, uLogin.PId_USA, uLogin.WAppUserName, txtAddress.Text, txtCity.Text,
                                                   ddlState.SelectedValue,
                                                   txtNoFamMembers.Text, ddlCountryResidence.SelectedValue,
                                                   txtCountriesVisited.Text, txtCountriesVisited2.Text, txtTripNo.Text, rblPrimaryPurpose.SelectedValue,
                                                   rblBringFruits.SelectedValue, rblBringAnimal.SelectedValue,
                                                   rblBringCultures.SelectedValue, rblBringSoil.SelectedValue,
                                                   rblProximityLivestock.SelectedValue, rblCurrencyOver.SelectedValue,
                                                   rblCommercialMerchandise.SelectedValue, txtResTotValGoods.Text,
                                                   txtVisTotValGoods.Text, txtSignDate.Text);

                        string[] TripInfo = TripInfo_Address.Split(',');

                        if (UserID == 0)
                        {
                            UserID = Int64.Parse(TripInfo[2]);
                            uLogin.WAppUserId = UserID;
                            HttpContext.Current.Session.Add("cdpUser", uLogin);
                        }
                        //string dob = string.Format("{0}/{1}/{2}", txtdobMonth.Text, txtdobDay.Text, txtdobYear.Text);
                        string dob2 = (string.IsNullOrEmpty(txtdobMonth.Text) || string.IsNullOrEmpty(txtdobDay.Text) || string.IsNullOrEmpty(txtdobYear.Text)) ? string.Empty : string.Format("{0}/{1}/{2}", txtdobMonth.Text, txtdobDay.Text, txtdobYear.Text);

                        // Form - add PERSON, WTripInfoUSADetailsP
                        CDPW.DAL.FormUSA.Form_AddInfo_Person(UserID, uLogin.PId_USA, txtFamName.Text, txtFName.Text, txtMName.Text, dob2, ddlCountry.SelectedValue, ddlCountry.SelectedItem.Text, txtPasspNo.Text, txtDescrArticles1.Text, txtArticlesValue1.Text, txtDescrArticles2.Text, txtArticlesValue2.Text, txtDescrArticles3.Text, txtArticlesValue3.Text, txtDescrArticles4.Text, txtArticlesValue4.Text, txtDescrArticles5.Text, txtArticlesValue5.Text, txtDescrArticles6.Text, txtArticlesValue6.Text, TripInfo[1], TripInfo[0]);

                        //phError.Visible = true;
                        //ltrError.Text = string.Format("WTripInfoUSAId - {0}, si AddressID - {1}", TripInfo[0], TripInfo[1]);

                        //Response.Redirect("cdpselectpay.aspx");
                        //}

                        phUsForm.Visible = false;
                        phUSAFormSuccess.Visible = true;

                        tran.Complete();
                        uLogin.SavedDUSA = true;
                        HttpContext.Current.Session.Add("cdpUser", uLogin);
                        Response.Redirect("DUSAForm.aspx?saved=true", true);
                    }
                }
                else
                {
                    if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Redirect to Login");
                    Response.Redirect("cdplogin.aspx");
                }
            }
            catch (Exception ex)
            {
                components.Error_Show.Show(phError, true, ltrError, ex, phReproduction, true);
                if (log.IsErrorEnabled) log.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Error", ex);
            }
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }

        protected void Page_LoadComplete()
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");

            String _Msg = Request.QueryString["msg"];

            if (!String.IsNullOrEmpty(_Msg) && _Msg.CompareTo("sub") == 0) {
                Response.Write("<script language='javascript'>alert('You have been subscribed to US CBP Plan');</script>");
            } 
            

            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }

        protected void OnCChangedEvent(object source, EventArgs e)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");

            if (HttpContext.Current.Session != null && HttpContext.Current.Session["cdpUser"] != null)
            {
                try
                {
                    CurrentUser uLogin = (CurrentUser)HttpContext.Current.Session["cdpUser"];
                    uLogin.SavedDUSA = false;
                    ((WebControl)source).Focus();
                    HttpContext.Current.Session.Add("cdpUser", uLogin);
                }
                catch (Exception ex)
                {
                    components.Error_Show.Show(phError, true, ltrError, ex, phReproduction, true);
                    if (log.IsErrorEnabled) log.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Error", ex);
                }
            }

            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }


        public void RaiseCallbackEvent(String eventArgument)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");

            if (eventArgument == null)
            {
                returnValue = "-1";
            }
            else if (String.Compare(eventArgument, "valchange", true) == 0)
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["cdpUser"] != null)
                {
                    try
                    {
                        CurrentUser uLogin = (CurrentUser)HttpContext.Current.Session["cdpUser"];
                        uLogin.SavedDUSA = false;
                        HttpContext.Current.Session.Add("cdpUser", uLogin);
                    }
                    catch (Exception ex)
                    {
                        components.Error_Show.Show(phError, true, ltrError, ex, phReproduction, true);
                        if (log.IsErrorEnabled) log.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Error", ex);
                    }
                }
                returnValue = eventArgument;
            }
            else if (String.Compare(eventArgument, "ifprint", true) == 0)
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["cdpUser"] != null && !((CurrentUser)(HttpContext.Current.Session["cdpUser"])).SavedDUSA)
                {
                    phFormSave.Visible = true;
                    phReproduction.Visible = false;
                    returnValue = "#1600";
                }
                else
                {
                    //returnValue = "cdpprintpreview.aspx?Report=USAForm";
                    returnValue = eventArgument;
                }

            }

            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }


        public string GetCallbackResult()
        {
            return returnValue;
        }



    }
}
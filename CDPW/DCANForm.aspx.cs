using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Transactions;
using CDPW.DAL;
using CDPW.BLL;

namespace CDPW
{
    public partial class DCANForm : System.Web.UI.Page, System.Web.UI.ICallbackEventHandler
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

                    if (uLogin.NoSignUp) {
                        my_settings_2.Visible = false;
                        logout_2.Visible = false;
                    }  

                    if (!Page.IsPostBack)
                    {
                        Int64 UserID = uLogin.WAppUserId;

                        Int64[] PIds = uLogin.PIds_CAN;

                        if (PIds == null || PIds.Length == 0)
                        {
                            PIds = new Int64[4];
                        }

                        ltrError.Text = string.Format("UserID: {0}<br />Username: {1}<br />PersonId: {2}<br />Nume: {3} {4} {5}", uLogin.WAppUserId, uLogin.WAppUserName, uLogin.WPersonId, uLogin.WPLastName, uLogin.WPFirstName, uLogin.WPMiddleName);
                        phError.Visible = false;

                        DataSet dsCAN = CDPW.DAL.FormCanada.Form_Get(UserID, PIds[0]);

                        if (dsCAN.Tables[0].Rows.Count > 0)
                        {
                            // persons
                            int i = 1;
                            //PIds = new Int64[4];
                            foreach (DataRow dr in dsCAN.Tables[0].Rows)
                            {
                                switch (i)
                                {
                                    case 1:
                                        PIds[0] = String.IsNullOrEmpty(dr["PersonId"].ToString()) ? 0 : (Int64)dr["PersonId"];
                                        txtName1.Text = string.Format("{0}", dr["Name"].ToString()).Trim();
                                        hidName1.Value = string.Format("{0}", dr["Name"].ToString()).Trim();
                                        if (!string.IsNullOrEmpty(dr["DateofBirth"].ToString()))
                                        {
                                            txtDOB1.Text = TextUtils.ReturnFromDB(dr["DateofBirth"].ToString(), true);
                                            hidDOB1.Value = TextUtils.ReturnFromDB(dr["DateofBirth"].ToString(), true);
                                        }
                                        txtCitiz1.Text = dr["Citizenship"].ToString().Trim();
                                        hidCitiz1.Value = dr["Citizenship"].ToString().Trim();
                                        break;
                                    case 2:
                                        PIds[1] = String.IsNullOrEmpty(dr["PersonId"].ToString()) ? 0 : (Int64)dr["PersonId"];
                                        txtName2.Text = string.Format("{0}", dr["Name"].ToString()).Trim();
                                        if (!string.IsNullOrEmpty(dr["DateofBirth"].ToString()))
                                        {
                                            txtDOB2.Text = TextUtils.ReturnFromDB(dr["DateofBirth"].ToString(), true);
                                        }
                                        txtCitiz2.Text = dr["Citizenship"].ToString().Trim();
                                        break;
                                    case 3:
                                        PIds[2] = String.IsNullOrEmpty(dr["PersonId"].ToString()) ? 0 : (Int64)dr["PersonId"];
                                        txtName3.Text = string.Format("{0}", dr["Name"].ToString()).Trim();
                                        if (!string.IsNullOrEmpty(dr["DateofBirth"].ToString()))
                                        {
                                            txtDOB3.Text = TextUtils.ReturnFromDB(dr["DateofBirth"].ToString(), true);
                                        }
                                        txtCitiz3.Text = dr["Citizenship"].ToString().Trim();
                                        break;
                                    case 4:
                                        PIds[3] = String.IsNullOrEmpty(dr["PersonId"].ToString()) ? 0 : (Int64)dr["PersonId"];
                                        txtName4.Text = string.Format("{0}", dr["Name"].ToString()).Trim();
                                        if (!string.IsNullOrEmpty(dr["DateofBirth"].ToString()))
                                        {
                                            txtDOB4.Text = TextUtils.ReturnFromDB(dr["DateofBirth"].ToString(), true);
                                        }
                                        txtCitiz4.Text = dr["Citizenship"].ToString().Trim();
                                        break;
                                }

                                i++;
                            }

                            //add PIds to ulogin and then in seesion
                            uLogin.PIds_CAN = PIds;
                            HttpContext.Current.Session.Add("cdpUser", uLogin);

                            // address
                            if (dsCAN.Tables[1].Rows.Count < 1)
                            {
                                Helpers.Lists(ddlProv, string.Empty, Lists.Regions(1), "AdministrativeRegion", "AdministrativeRegionId");
                                Helpers.Lists(ddlCountry, null, Lists.Countries(), "CountryName", "CountryId");
                            }
                            else
                            {
                                foreach (DataRow dr in dsCAN.Tables[1].Rows)
                                {
                                    txtAddress.Text = string.Format("{0}", dr["StreetName"]);
                                    txtCity.Text = dr["City"].ToString();

                                    int iCountryId = String.IsNullOrEmpty(dr["CountryId"].ToString()) ? 0 : int.Parse(dr["CountryId"].ToString());

                                    Helpers.Lists(ddlProv, dr["AdministrativeRegionId"].ToString(), Lists.Regions(iCountryId), "AdministrativeRegion", "AdministrativeRegionId");
                                    Helpers.Lists(ddlCountry, dr["CountryId"].ToString(), Lists.Countries(), "CountryName", "CountryId");
                                    txtPostalCode.Text = dr["PostalCode"].ToString();
                                }
                            }

                            // Trip info
                            foreach (DataRow dr in dsCAN.Tables[2].Rows)
                            {
                                rblArrive.SelectedValue = dr["WArrivingBy"].ToString();
                                txtTripNo.Text = TextUtils.ReturnFromDB(dr["TransportationId"].ToString());
                                rblPurpose.SelectedValue = dr["WTripPurpose"].ToString();
                                rblArriveFrom.SelectedValue = dr["WArrivingFrom"].ToString();
                                rblFirearms.SelectedValue = dr["Firearms"].ToString();
                                rblCG.SelectedValue = dr["CommercialGoods"].ToString();
                                rblFood.SelectedValue = dr["MeatProducts"].ToString();
                                rblMoney.SelectedValue = dr["CurrencyValue"].ToString();
                                rblGoods.SelectedValue = dr["Goods"].ToString();
                                rblFarm.SelectedValue = dr["FarmVisit"].ToString();
                                rblExceededDutyFree.SelectedValue = dr["ExceedDutyFree"].ToString();
                                rblExceededExemptions.SelectedValue = dr["ExceedExemptions"].ToString();
                                txtDureation.Text = dr["DurationOfStay"].ToString();
                                if (!string.IsNullOrEmpty(dr["TripDate"].ToString()))
                                {
                                    txtdDate.Text = TextUtils.ReturnFromDB(dr["TripDate"].ToString(), true);
                                }
                            }

                            // Arrival info
                            int j = 1;
                            foreach (DataRow dr in dsCAN.Tables[3].Rows)
                            {
                                switch (j)
                                {
                                    case 1:
                                        txtDtC1.Text = TextUtils.ReturnFromDB(dr["DateLeftCanada"].ToString(), false, true);
                                        txtCVG1.Text = string.Format(new System.Globalization.CultureInfo("en-US"), "{0:#.##}", dr["ValueGoods"]);
                                        break;
                                    case 2:
                                        txtDtC2.Text = TextUtils.ReturnFromDB(dr["DateLeftCanada"].ToString(), false, true);
                                        txtCVG2.Text = string.Format(new System.Globalization.CultureInfo("en-US"), "{0:#.##}", dr["ValueGoods"]);
                                        break;
                                    case 3:
                                        txtDtC3.Text = TextUtils.ReturnFromDB(dr["DateLeftCanada"].ToString(), false, true);
                                        txtCVG3.Text = string.Format(new System.Globalization.CultureInfo("en-US"), "{0:#.#}", dr["ValueGoods"]);
                                        break;
                                    case 4:
                                        txtDtC4.Text = TextUtils.ReturnFromDB(dr["DateLeftCanada"].ToString(), false, true);
                                        txtCVG4.Text = string.Format(new System.Globalization.CultureInfo("en-US"), "{0:#.##}", dr["ValueGoods"]);
                                        break;
                                }
                                j++;
                            }
                        }
                        else
                        {
                            Helpers.Lists(ddlProv, string.Empty, Lists.Regions(1), "AdministrativeRegion", "AdministrativeRegionCode");
                            Helpers.Lists(ddlCountry, null, Lists.Countries(), "CountryName", "CountryId");
                        }
                    }
                    if (!uLogin.SavedDCAN)
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
                    // user is not logged
                    if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Redirect to Login");
                    Response.Redirect("cdplogin.aspx",true);
                }
            }
            catch (Exception ex)
            {
                components.Error_Show.Show(phError, true, ltrError, ex, phReproduction, true);
                if (log.IsErrorEnabled) log.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Error", ex);
            }


        }

        protected void Page_LoadComplete()
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");

            String _Msg = Request.QueryString["msg"];

            if (!String.IsNullOrEmpty(_Msg) && _Msg.CompareTo("sub") == 0)
            {
                Response.Write("<script language='javascript'>alert('You have been subscribed to CBSA Plan');</script>");
            }


            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }


        // Filling the provices list for the selected country
        protected void ddlCountry_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            try
            {
                ddlProv.Items.Clear();
                Helpers.Lists(ddlProv, null, Lists.Regions(int.Parse(ddlCountry.SelectedValue)), "AdministrativeRegion", "AdministrativeRegionId");

                if (HttpContext.Current.Session != null && HttpContext.Current.Session["cdpUser"] != null)
                {
                    CurrentUser uLogin = (CurrentUser)HttpContext.Current.Session["cdpUser"];
                    uLogin.SavedDCAN = false;
                    HttpContext.Current.Session.Add("cdpUser", uLogin);
                }

            }
            catch (Exception ex)
            {
                components.Error_Show.Show(phError, true, ltrError, ex, phReproduction, true);
                if (log.IsErrorEnabled) log.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Error", ex);
            }
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }


        #region "---   SAVING THE FORM   ---"

        // Saving the info in database
        protected void btnSaveForm_Click(Object sender, EventArgs e)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");

            try
            {
                //throw new Exception("testing");

                if (HttpContext.Current.Session != null && HttpContext.Current.Session["cdpUser"] != null && HttpContext.Current.Session["cdpUser"] is CurrentUser)
                {
                    using (System.Transactions.TransactionScope tran = new System.Transactions.TransactionScope())
                    {
                        CurrentUser uLogin = (CurrentUser)HttpContext.Current.Session["cdpUser"];
                        Int64 UserID = uLogin.WAppUserId;

                        string CurrentName = hidName1.Value.Trim();

                        string TripInfo_Address;
                        TripInfo_Address = CDPW.DAL.FormCanada.Form_AddInfo(UserID, uLogin.PIds_CAN, uLogin.WAppUserName, txtAddress.Text, txtCity.Text, ddlProv.SelectedValue, ddlCountry.SelectedValue, txtPostalCode.Text, rblArrive.SelectedValue, txtTripNo.Text, rblPurpose.SelectedValue, rblArriveFrom.SelectedValue, rblFirearms.SelectedValue, rblCG.SelectedValue, rblFood.SelectedValue, rblMoney.SelectedValue, rblGoods.SelectedValue, rblFarm.SelectedValue, txtDureation.Text, rblExceededDutyFree.SelectedValue, rblExceededExemptions.SelectedValue, txtdDate.Text);

                        string[] TripInfo = TripInfo_Address.Split(',');

                        if (UserID == 0)
                        {
                            UserID = Int64.Parse(TripInfo[2]);
                            uLogin.WAppUserId = UserID;
                            //HttpContext.Current.Session.Add("cdpUser", uLogin);
                        }

                        // Form - add PERSON, WTripInfoCANDetailsP
                        CDPW.DAL.FormCanada.Form_AddInfo_Person(UserID, uLogin.PIds_CAN, txtName1.Text, txtDOB1.Text, txtCitiz1.Text, txtDtC1.Text, txtCVG1.Text, txtName2.Text, txtDOB2.Text, txtCitiz2.Text, txtDtC2.Text, txtCVG2.Text, txtName3.Text, txtDOB3.Text, txtCitiz3.Text, txtDtC3.Text, txtCVG3.Text, txtName4.Text, txtDOB4.Text, txtCitiz4.Text, txtDtC4.Text, txtCVG4.Text, int.Parse(TripInfo[1]), Int64.Parse(TripInfo[0]));

                        phCanForm.Visible = false;
                        phCanFormSuccess.Visible = true;

                        tran.Complete();

                        uLogin.SavedDCAN = true;
                        HttpContext.Current.Session.Add("cdpUser", uLogin);

                        Response.Redirect("DCANForm.aspx?saved=true");
                    }
                }
                else
                {
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


        public static Boolean CANForm_Save()
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");

            Boolean bRet = false;





            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");

            return bRet;
        }
        #endregion

        protected void OnCChangedEvent(object source, EventArgs e)
        {

            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");

            if (HttpContext.Current.Session != null && HttpContext.Current.Session["cdpUser"] != null)
            {
                try
                {
                    CurrentUser uLogin = (CurrentUser)HttpContext.Current.Session["cdpUser"];
                    uLogin.SavedDCAN = false;
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
                        uLogin.SavedDCAN = false;
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
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["cdpUser"] != null && !((CurrentUser)(HttpContext.Current.Session["cdpUser"])).SavedDCAN)
                {
                    phFormSave.Visible = true;
                    phReproduction.Visible = false;
                    returnValue = "#24";
                }
                else
                {
                    //returnValue = "cdpprintpreview.aspx?Report=CANForm";
                    returnValue = eventArgument;
                }
            }
            //ifdeletedata
            else if (String.Compare(eventArgument, "ifdeletedata", true) == 0)
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["cdpUser"] != null && !((CurrentUser)(HttpContext.Current.Session["cdpUser"])).SavedDCAN)
                {                                       
                    CurrentUser uLogin = (CurrentUser)HttpContext.Current.Session["cdpUser"];
                    if (uLogin.NoSignUp || !uLogin.NoSignUp)
                    {
                        returnValue = "msgdeletedata";
                    }
                }
                else
                {
                    //returnValue = "cdpprintpreview.aspx?Report=CANForm";
                    returnValue = eventArgument;
                }
            }
            else if (String.Compare(eventArgument, "deletedata", true) == 0)
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["cdpUser"] != null && !((CurrentUser)(HttpContext.Current.Session["cdpUser"])).SavedDCAN)
                {
                    
                    Boolean DeleteUData = false;
                    CurrentUser uLogin = (CurrentUser)HttpContext.Current.Session["cdpUser"];
                    DeleteUData = cdplogout.UserData_Delete(uLogin, 0);
                }
                else
                {
                    //returnValue = "cdpprintpreview.aspx?Report=CANForm";
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
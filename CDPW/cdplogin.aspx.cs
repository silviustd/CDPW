using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDPW.DAL;
using CDPW.BLL;


namespace CDPW
{
    public partial class cdplogin : System.Web.UI.Page
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            imgContinueCheck.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["ThemeDirectory"].ToString() + "images/bullet-1.png";

            //if (log.IsInfoEnabled) log.Info("cdplogin Page_Load - Exit");
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }


        // Sending the SignUp form
        public void btnSignUp_Click(Object sender, EventArgs e)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            try
            {
                Helpers.HideMessages(this, false);
                if (CDPW.DAL.Users.Check_Username(txtSignUName.Text) == true)
                {
                    phLoginMessages.Visible = true;
                    ltrLoginMessages.Text = CDPWMessages.SAME_USERNAME;
                    //ltrLoginMessages.CssClass = "msg_box msg_info corners";
                    
                }
                else if (CDPW.DAL.Users.Check_Email(txtSignEmail.Text) == true)
                {
                    phLoginMessages.Visible = true;
                    ltrLoginMessages.Text = CDPWMessages.SAME_EMAIL;
                    //ltrLoginMessages.CssClass = "msg_box msg_info corners";
                }
                else
                {
                    using (System.Transactions.TransactionScope tran = new System.Transactions.TransactionScope())
                    {
                        CDPW.DAL.Users.Add(txtSignUName.Text, txtSignEmail.Text, txtSignPasswd.Text);

                        //string mailBody = string.Format("Hello, <br /><br />In order to be able to acces your account at CDP, please click <a href=\"{2}{3}?ma={0}&sg={1}\">here</a>.<br /><br />Thank you.", txtSignEmail.Text, System.Guid.NewGuid(), Request.Url.GetLeftPart(UriPartial.Authority), ResolveUrl("~/cdpconfirmreg.aspx"));

                        //// send mai message to confirm the sign-up
                        //BLL.Mail.Send(txtSignEmail.Text, null, null, "CDP - Sign-up confirm", mailBody, System.Net.Mail.MailPriority.Normal);

                        String signupEmail = txtSignEmail.Text;
                        String urlLogin = String.Format("<a href=\"{2}{3}?ma={0}&sg={1}\">here</a>", signupEmail, System.Guid.NewGuid(), Request.Url.GetLeftPart(UriPartial.Authority), ResolveUrl("~/cdpconfirmreg.aspx"));

                        string emailTemplate = Template.readTemplateD("[link_login]", urlLogin, HttpContext.Current.Server.MapPath("components/signup.html"));
                        Mail.Send(signupEmail, null, null, CDPWMessages.EMAIL_SUBJECT_SIGNUP_CONFIRM, emailTemplate, System.Net.Mail.MailPriority.Normal);

                        tran.Complete();

                        phSignupSuccess.Visible = true;
                        phSignupEmailExists.Visible = false;
                        phSignupUserExists.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                //phLoginSignUp.Visible = false;
                components.Error_Show.Show(phError, true, ltrError, ex, phLoginSignUp, false);
                if (log.IsErrorEnabled) log.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Error", ex);
            }
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }


        // Login process
        public void btnLogin_Click(Object sender, EventArgs e)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                ds = CDPW.DAL.Users.Login_Authenticate(txtLoginName.Text, txtLoginPasswd.Text);

                Helpers.HideMessages(this, false);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    // User not activated
                    //phLoginUserNoActive.Visible = true;
                    phLoginPasswordWrong.Visible = false;
                    phLoginUserNoExist.Visible = false;
                    //phLoginSignUp.Visible = false;
                    phLoginMessages.Visible = true;
                    ltrLoginMessages.Text = CDPWMessages.ACCT_NOT_ACTIVE;
                    ltrLoginMessages.CssClass = "msg_box msg_alert corners";
                }
                else if (ds.Tables[1].Rows.Count < 1)
                {
                    // User doesn't exist
                    //phLoginUserNoExist.Visible = true;
                    //phSignUpGoback.Visible = true;
                    //phLoginSignUp.Visible = false;
                    phLoginMessages.Visible = true;
                    ltrLoginMessages.Text = CDPWMessages.NO_USER;
                    ltrLoginMessages.CssClass = "msg_box msg_alert corners";
                }
                else if (ds.Tables[2].Rows.Count < 1)
                {
                    //phLoginPasswordWrong.Visible = true;
                    phLoginUserNoActive.Visible = false;
                    phLoginUserNoExist.Visible = false;
                    //phLoginSignUp.Visible = false;
                    phLoginMessages.Visible = true;
                    ltrLoginMessages.Text = CDPWMessages.WRONG_PWD;
                    ltrLoginMessages.CssClass = "msg_box msg_alert corners";

                }
                else
                {
                    // Setting a working session and register the user data for later use
                    CurrentUser uLogin = new CurrentUser();

                    foreach (System.Data.DataRow dr in ds.Tables[2].Rows)
                    {
                        uLogin.WAppUserId = (Int64)dr["WAppUserId"];
                        if (dr["PersonId"].ToString().Length == 0)
                            uLogin.WPersonId = -1;
                        else
                            uLogin.WPersonId = (Int64)dr["PersonId"];
                        uLogin.WAppUserName = TextUtils.ReturnFromDB(dr["WAppUserName"].ToString()) ;
                        uLogin.WAppUserPwd = TextUtils.ReturnFromDB(dr["WAppUserPwd"].ToString()) ;
                        uLogin.WAppUserEmail = TextUtils.ReturnFromDB(dr["WAppUserEmail"].ToString()) ;
                        uLogin.WAppUserAEmail = TextUtils.ReturnFromDB(dr["WAppUserAltEmail"].ToString());
                        uLogin.NoSignUp = (bool)dr["NoSignUp"];
                        uLogin.NotKeepData = (bool)dr["NotKeepData"];
                    }

                    /* Get User F, M, L Names from Persons*/
                    foreach (System.Data.DataRow dr in ds.Tables[4].Rows)
                    {
                        uLogin.WPFirstName = TextUtils.ReturnFromDB(dr["FirstName"].ToString()) ;
                        uLogin.WPMiddleName = TextUtils.ReturnFromDB(dr["MiddleName"].ToString());
                        uLogin.WPLastName = TextUtils.ReturnFromDB(dr["LastName"].ToString());
                    }


                    uLogin.ActiveEnrollments = new List<UserActiveEnrollments>();

                    foreach (System.Data.DataRow dr in ds.Tables[3].Rows)
                    {
                        var entry = new UserActiveEnrollments();
                        entry.PLicenseType = (string)dr["PLicensesType"];
                        entry.PLicenseTypeId = (Int16)dr["PLicensesTypeId"];
                        entry.EnrollmentId = (Int64)dr["EnrollmentId"];
                        entry.EnrollmentBeginDate = (DateTime)dr["EnrollmentBeginDate"];
                        if (!String.IsNullOrEmpty(TextUtils.ReturnFromDB(dr["EnrollmentEndDate"].ToString(), true))) {
                            entry.EnrollmentEndDate = (DateTime)dr["EnrollmentEndDate"]; //(DateTime)dr["EnrollmentEndDate"];
                        }

                        uLogin.ActiveEnrollments.Add(entry);
                    }

                    // register the object in session
                    //if (HttpContext.Current.Session.Equals("cdpUser") != true)
                    HttpContext.Current.Session.Add("cdpUser", uLogin);

                    //check deleted data 
                    Boolean DeleteUData = false;
                    DeleteUData = cdplogout.UserData_Delete(uLogin,1);
                    uLogin = null;

                    Response.Redirect("cdpselectpay.aspx",false);

                    ////for test
                    //if (String.Compare(System.Configuration.ConfigurationManager.AppSettings["runmode"].ToString(), "test", true) == 0)
                    //   Response.Redirect("cdpselecttest.aspx");
                    //else
                    //    Response.Redirect("cdpselectpay.aspx");
                    ////for test

                }
            }
            catch (Exception ex)
            {
                //phLoginSignUp.Visible = false;
                components.Error_Show.Show(phError, true, ltrError, ex, phLoginSignUp, false);
                if (log.IsErrorEnabled) log.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Error", ex);
            }
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }

        public void btnNoSignUp_Click(Object sender, EventArgs e)
        {

            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");

            try
            {

                CurrentUser uLogin = new CurrentUser();

                System.Data.DataSet ds = new System.Data.DataSet();
                ds = CDPW.DAL.Users.Add_NoUser();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
                    {
                        uLogin.WAppUserId = (Int64)dr["WAppUserId"];
                        if (dr["PersonId"].ToString().Length == 0)
                            uLogin.WPersonId = -1;
                        else
                            uLogin.WPersonId = (Int64)dr["PersonId"];
                        
                        uLogin.WAppUserName = TextUtils.ReturnFromDB(dr["WAppUserName"].ToString()); 
                        uLogin.WAppUserPwd = TextUtils.ReturnFromDB(dr["WAppUserPwd"].ToString()); 
                        uLogin.WAppUserEmail = TextUtils.ReturnFromDB(dr["WAppUserEmail"].ToString()); 
                        uLogin.WAppUserAEmail = TextUtils.ReturnFromDB(dr["WAppUserAltEmail"].ToString());
                        uLogin.NoSignUp = true;
                        uLogin.NotKeepData = true; 

                        uLogin.WPFirstName = TextUtils.ReturnFromDB(dr["FirstName"].ToString());
                        uLogin.WPMiddleName = TextUtils.ReturnFromDB(dr["MiddleName"].ToString()); 
                        uLogin.WPLastName = TextUtils.ReturnFromDB(dr["LastName"].ToString());  

                        HttpContext.Current.Session.Add("cdpUser", uLogin);

                        Response.Redirect("cdpselectpay.aspx", false);

                    }
                }
            }
            catch (Exception ex)
            {
                //phLoginSignUp.Visible = false;
                components.Error_Show.Show(phError, false, ltrError, ex, phLoginSignUp, false);
                if (log.IsErrorEnabled) log.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Error", ex);
            }

            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }

    }
}
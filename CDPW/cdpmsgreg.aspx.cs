using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using CDPW.DAL;
using CDPW.BLL;


namespace CDPW
{
    public partial class cdpmsgreg : System.Web.UI.Page
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        String _Action = String.Empty;
        String _Plan = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            Int64 _UserId = 0;
            String _Msg = String.Empty;
            String _CFPage = String.Empty;
            String _PlanMsg = String.Empty;
            String _Subject = String.Empty;
            Int64 _PlanId = 0;
            SqlDataReader sdr;


            _Plan = Request.QueryString["plan"];
            _Action = Request.QueryString["act"];
            //Label1.Text = Label1.Text + "user:" + HttpContext.Current.Session["cdpUserId"] + "plan: " + plan + "t3: " + t3;


            try
            {
                if ( HttpContext.Current.Session != null && HttpContext.Current.Session["cdpUser"] != null)
                {
                    if (!IsPostBack)
                    {
                        CurrentUser uLogin = new CurrentUser();
                        uLogin = (CurrentUser)HttpContext.Current.Session["cdpUser"];
                        _UserId = uLogin.WAppUserId;

                        if (!String.IsNullOrEmpty(_Plan) && !String.IsNullOrEmpty(_Action))
                        {
                            if (String.Compare(_Action, "sub", true) == 0)
                            {
                                //TODO should return the ID and update the CurrentUser
                                using (SqlConnection dbConnection = new SqlConnection(DBConnection.ConnectionString))
                                {
                                    SqlCommand cmd = new SqlCommand("Enrollment_Register", dbConnection);
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    // Add parameters to SPROC
                                    cmd.Parameters.Add("@UserId", SqlDbType.BigInt).Value = _UserId;
                                    cmd.Parameters.Add("@PlanType", SqlDbType.VarChar, 15).Value = _Plan;

                                    dbConnection.Open();

                                    sdr = cmd.ExecuteReader();

                                    while (sdr.Read())
                                    {
                                        var entry = new UserActiveEnrollments();
                                        entry.PLicenseType = sdr.GetString(1);
                                        entry.PLicenseTypeId = sdr.GetInt16(0);
                                        entry.EnrollmentId = sdr.GetInt64(2);
                                        entry.EnrollmentBeginDate = sdr.GetDateTime(3);
                                        if (sdr.IsDBNull(4))
                                        {
                                            entry.EnrollmentEndDate = null;
                                        }
                                        else
                                        {
                                            entry.EnrollmentEndDate = sdr.GetDateTime(4);
                                        }

                                        uLogin.ActiveEnrollments.Add(entry);

                                        HttpContext.Current.Session.Add("cdpUser", uLogin);

                                        _PlanId = sdr.GetInt64(2);
                                    }
                                }
                                if (String.Compare(_Plan, "CBSA", true) == 0)
                                {
                                    _PlanMsg = "CBSA Declaration Card";
                                    _CFPage = "DCANForm.aspx?msg=sub";
                                }
                                else if (String.Compare(_Plan, "USCBP", true) == 0)
                                {
                                    _PlanMsg = "USCBP Customs Declaration";
                                    _CFPage = "DUSAForm.aspx?msg=sub";
                                }
                                else
                                {
                                    _PlanMsg = "Custom Forms Plan";
                                    _CFPage = "DUSAForm.aspx?msg=sub";
                                }

                                _Msg = "You successfully subscribed for " + _PlanMsg + " Plan.";
                                _Msg += " You will now being redirected to " + _PlanMsg + ". ";
                                _Msg += "<br/> If you are not automatically redirected, please <a href='" + _CFPage + "'> click here</a>";

                                lblMesaj.Text = _Msg;
                                btnUnsub.Visible = false;
                                Response.Redirect(_CFPage, true);

                            }
                            else if (String.Compare(_Action, "unsub", true) == 0)
                            {
                                if (String.Compare(_Plan, "CBSA", true) == 0)
                                {
                                    _Subject = "CBSA Declaration Card-Unsubscribe";
                                }
                                else if (String.Compare(_Plan, "USCBP", true) == 0)
                                {
                                    _Subject = "USCBP Customs Declaration-Unsubscribe";
                                }
                                else
                                {
                                    _Subject = "Unsubscribe";
                                }


                                if (!(uLogin.ActiveEnrollments == null || uLogin.ActiveEnrollments.Capacity == 0))
                                {
                                    foreach (UserActiveEnrollments uAEnroll in uLogin.ActiveEnrollments)
                                    {
                                        if (String.Compare(uAEnroll.PLicenseType, _Plan, true) == 0)
                                        {
                                            _PlanId = uAEnroll.EnrollmentId;
                                            //using (SqlConnection dbConnection = new SqlConnection(DBConnection.ConnectionString))
                                            using (System.Transactions.TransactionScope tran = new System.Transactions.TransactionScope())
                                            {
                                                SqlConnection dbConnection = new SqlConnection(DBConnection.ConnectionString);

                                                SqlCommand cmd = new SqlCommand("Enrollment_Cancel", dbConnection);
                                                cmd.CommandType = CommandType.StoredProcedure;

                                                // Add parameters to SPROC
                                                cmd.Parameters.Add("@UserId", SqlDbType.BigInt).Value = _UserId;
                                                cmd.Parameters.Add("@PlanType", SqlDbType.VarChar, 15).Value = _Plan;
                                                cmd.Parameters.Add("@PlanId", SqlDbType.BigInt).Value = _PlanId;

                                                dbConnection.Open();

                                                cmd.ExecuteNonQuery();

                                                String unsubEmail = System.Configuration.ConfigurationManager.AppSettings["unsubEmail"].ToString();
                                                String unsubPeriod = System.Configuration.ConfigurationManager.AppSettings["unsubPeriod"].ToString();

                                                string mailBody = "Hi, <br /><br />This confirms that we have received your request to unsubscribe from {0} Plan.";
                                                mailBody += "<br/>Unless otherwise notified in {1} we will unsubscribe for:";
                                                mailBody += "<br/> Name: {2}";
                                                mailBody += "<br/> Email: {3}";
                                                mailBody += "<br/><br/> We would like your feedback so please reaply to this email and tell us how we can improve";
                                                mailBody += "<br/><br/>Thank you,";
                                                mailBody += "<br/> - Columna Soft Team";

                                                String uName = (String.IsNullOrEmpty(uLogin.WPFirstName) && String.IsNullOrEmpty(uLogin.WPLastName)) ? uLogin.WAppUserName : uLogin.WPFirstName + " " + uLogin.WPLastName;    

                                                mailBody = String.Format(mailBody, _Plan.ToUpper(), unsubPeriod, uLogin.WAppUserName, uLogin.WAppUserEmail);
                                                //TODO
                                                BLL.Mail.Send(uLogin.WAppUserEmail, unsubEmail, null, _Subject, mailBody, System.Net.Mail.MailPriority.High);

                                                uLogin.ActiveEnrollments.Remove(uAEnroll);

                                                HttpContext.Current.Session.Add("cdpUser", uLogin);

                                                tran.Complete();

                                                _Msg = " We are sorry to see you unsubscribing. After unsubscribe, please tell us what went wrong at <A HREF='mailto:office@columnasoft.com?subject='" + _Subject + "'>office@columnasoft.com</A>";
                                                _Msg += "<br/>A confirmation email has been sent to the emaill address provided";
                                                _Msg += "<br/>If you have a paypal account, please confirm you unsubscribe by clicking on the unsubscribe button below";

                                                lblMesaj.Text = _Msg;

                                                break;

                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    _Msg = " Something went wrong with the unsubscribing process.";
                                    _Msg += "\nPlease <A HREF='mailto:office@columnasoft.com?subject='" + _Subject + "'>email us</A> with your Name, Address, Email and Plan";
                                    lblMesaj.Text = _Msg;
                                }

                            }
                        }
                        if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
                    }
                }
                else
                {
                    if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
                    Response.Redirect("cdplogin.aspx");
                }
            }
            catch (Exception ex)
            {
                //string strError = string.Format("There was an error:<br /> {0}<br />", ex.Message);
                String strError = CDPWMessages.ERR_MSG_ERR;
                lblMesaj.Text = strError;
                lblMesaj.CssClass = "msg_box msg_error corners msg_box_text14";
                lblLogOut.Text = String.Format(CDPWMessages.ERR_MSG_LOGOUT_LOGIN_EMAIL, _Subject); 
                btnUnsub.Visible = false;
                lblLogOut.Visible = true;
                if (log.IsErrorEnabled) log.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Error", ex);
            }

        }

        protected void btnUnsub_Click(object sender, ImageClickEventArgs e)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");

            String _Msg = String.Empty;
            String _Subject = String.Empty;
            CurrentUser cUser = new CurrentUser();

            try
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["cdpUser"] != null)
                {
                    //Int64 _UserId = 0;
                    //Int64 _PlanId = 0;

                    //cUser = (CurrentUser)HttpContext.Current.Session["cdpUser"];
                    //_UserId = cUser.WAppUserId;
                    //if (!(cUser.ActiveEnrollments == null || cUser.ActiveEnrollments.Capacity == 0))
                    //{
                    //    foreach (UserActiveEnrollments uAEnroll in cUser.ActiveEnrollments)
                    //    {
                    //        if (String.Compare(uAEnroll.PLicenseType, _Plan, true) == 0)
                    //        {
                    //            _PlanId = uAEnroll.EnrollmentId;
                    //            using (SqlConnection dbConnection = new SqlConnection(DBConnection.ConnectionString))
                    //            {
                    //                SqlCommand cmd = new SqlCommand("Enrollment_Cancel", dbConnection);
                    //                cmd.CommandType = CommandType.StoredProcedure;

                    //                // Add parameters to SPROC
                    //                cmd.Parameters.Add("@UserId", SqlDbType.BigInt).Value = _UserId;
                    //                cmd.Parameters.Add("@PlanType", SqlDbType.VarChar, 15).Value = _Plan;
                    //                cmd.Parameters.Add("@PlanId", SqlDbType.BigInt).Value = _PlanId;

                    //                dbConnection.Open();

                    //                cmd.ExecuteNonQuery();
                    //            }
                    //        }


                            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");

                            if (String.Compare(_Plan, "CBSA", true) == 0)
                            {
                                Response.Redirect("https://www.paypal.com/cgi-bin/webscr?cmd=_subscr-find&alias=EWHNCM3S6UUYY",false);
                            }
                            else if (String.Compare(_Plan, "USCBP", true) == 0)
                            {
                                Response.Redirect("https://www.paypal.com/cgi-bin/webscr?cmd=_subscr-find&alias=EWHNCM3S6UUYY",false);
                            }
                            else
                            {
                                Response.Redirect("https://www.paypal.com/cgi-bin/webscr?cmd=_subscr-find&alias=EWHNCM3S6UUYY",false);
                            }
                        //}
                    //}
                    //else
                    //{
                    //    _Msg = " Something went wrong with the unsubscribing process.";
                    //    _Msg += "\nPlease <A HREF='mailto:office@columnasoft.com?subject='" + _Subject + "'>email us</A> with your Name, Address, Email and Plan";
                    //    lblMesaj.Text = _Msg;
                    //}

                    //if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
                }
                else
                {
                    if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
                    Response.Redirect("cdplogin.aspx");
                }
            }
            catch (Exception ex)
            {
                string strError = string.Format("There was an error:<br /> {0}<br />", ex.Message);
                lblMesaj.Text = strError;
                lblMesaj.CssClass = "msg_box msg_error corners";
                lblLogOut.Text = "Please <a href='cdplogout.aspx'> Log Out</a>  and Log in again.<br/>If the problem persists please <A HREF='mailto:office@columnasoft.com?subject='" + _Subject + "'>email us</A> with your Name, Address, Email and Plan";
                btnUnsub.Visible = false;
                lblLogOut.Visible = true;
                if (log.IsErrorEnabled) log.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Error", ex);
            }

        }
    }
}
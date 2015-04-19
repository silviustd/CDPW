using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDPW.BLL;
using CDPW.DAL;

namespace CDPW
{
    public partial class cdprecoverpasswd : System.Web.UI.Page
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");

            string pSection = (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["ps"])) ? HttpContext.Current.Request.QueryString["ps"].Trim() : string.Empty;

            try
            {
                if (!Page.IsPostBack)
                {
                    if (pSection == "rp")
                    {

                        string emailEncrypted = (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["etr"])) ? HttpContext.Current.Request.QueryString["etr"].ToString() : string.Empty;

                        if (!string.IsNullOrEmpty(emailEncrypted))
                        {
                            string dateEncrypted = (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["dte"])) ? HttpContext.Current.Request.QueryString["dte"].ToString() : string.Empty;

                            if (!string.IsNullOrEmpty(dateEncrypted))
                            {
                                dateEncrypted = dateEncrypted.Replace(" ", "+");

                                DateTime oldDate = Convert.ToDateTime(EncryptDecrypt.Decrypt(dateEncrypted));
                                DateTime currentDate = DateTime.Now;
                                int dateDiff = DateTime.Compare(oldDate, currentDate);

                                if (dateDiff < 0)
                                {
                                    // Recover message EXPIRED
                                    phFormRecoverPasswd.Visible = false;
                                    ltrRecoverWrong.Text = "The confirmation message was sent more than 3 days ago. <br />Please try to re-submit your request.";
                                    phRecoverWrong.Visible = true;
                                }
                                else
                                {
                                    emailEncrypted = emailEncrypted.Replace(" ", "+");
                                    string emailToReset = EncryptDecrypt.Decrypt(emailEncrypted);
                                    string uName = String.Empty;

                                    if (Users.Check_Email(emailToReset, ref uName))
                                    {
                                        string newPasswd = System.Web.Security.Membership.GeneratePassword(20, 5);

                                        // Change password in database
                                        Users.Password_Reset(emailToReset, newPasswd);

                                        string emailTemplate = Template.readTemplate("[email_address]\\[new_password]", uName + "\\" + newPasswd, HttpContext.Current.Server.MapPath("components/reset_password.html"));

                                        Mail.Send(emailToReset, null, null, CDPWMessages.EMAIL_SUBJECT_NEW_PASSWORD, emailTemplate, System.Net.Mail.MailPriority.Normal);

                                        phFormRecoverPasswd.Visible = false;
                                        phResetPasswd.Visible = true;

                                        Response.Redirect("cdprecoverpasswd.aspx?ps=pes",false);
                                        //ltrResetPwd.Text += "<br />Old date: " + oldDate.ToString();
                                        //ltrResetPwd.Text += "<br />Current date: " + currentDate.ToString();
                                        //ltrResetPwd.Text += "<br />Difference: " + dateDiff.ToString();
                                    }
                                    else
                                    {
                                        phFormRecoverPasswd.Visible = false;
                                        phRecoverWrong.Visible = true;
                                    }
                                }

                            }
                            else
                            {
                                phFormRecoverPasswd.Visible = false;
                                ltrRecoverWrong.Text = "The confirmation message was sent more than 3 days ago. <br />Please try to re-submit your request.";
                                phRecoverWrong.Visible = true;
                            }

                        }
                        else
                        {
                            phFormRecoverPasswd.Visible = false;
                            phError.Visible = true;
                        }

                    }
                    else if (pSection == "pes")
                    {
                        phFormRecoverPasswd.Visible = false;
                        phResetPasswd.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                components.Error_Show.Show(phError, true, ltrError, ex, phFormRecoverPasswd, true);
                if (log.IsErrorEnabled) log.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Error", ex);
            }

            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            try
            {
                string email = txtEmail.Text;
                string uName = String.Empty;

                if (Users.Check_Email(email, ref uName))
                {
                    string guid = System.Guid.NewGuid().ToString();
                    string encodedEmail = EncryptDecrypt.Encrypt(email);
                    DateTime dt = DateTime.Now.AddDays(3);
                    string encodedDate = EncryptDecrypt.Encrypt(dt.ToString());
                    string url = string.Format("<a href=\"http://www.columnasoft.com/cdpww/cdprecoverpasswd.aspx?etr={0}&g={1}&dte={2}&ps=rp\">Yes, reset my password</a>", encodedEmail, guid, encodedDate);

                    //string emailTemplate = Template.readTemplate("[email_address]\\[reset_link]", email + "\\" + url, HttpContext.Current.Server.MapPath("components/recover_passwd.html"));
                    string emailTemplate = Template.readTemplate("[email_address]\\[reset_link]", uName + "\\" + url, HttpContext.Current.Server.MapPath("components/recover_passwd.html"));
                    Mail.Send(email, null, null, CDPWMessages.EMAIL_SUBJECT_RESET_PWD, emailTemplate, System.Net.Mail.MailPriority.Normal);

                    //ltrRecoverSuccess.Text += "<br /><br />Mesaj: <br />" + emailTemplate;
                    //ltrRecoverSuccess.Text += "<br /><br />URL: " + url;
                    //ltrRecoverSuccess.Text += "<br /><br />Email criptat: " + encodedEmail;
                    //ltrRecoverSuccess.Text += "<br /><br />Email: " + EncryptDecrypt.Decrypt(encodedEmail);
                    //ltrRecoverSuccess.Text += "<br /><br />Data: " + dt.ToString();
                    //ltrRecoverSuccess.Text += "<br /><br />Data criptata: " + encodedDate;
                    //ltrRecoverSuccess.Text += "<br /><br />Data decriptata: " + EncryptDecrypt.Decrypt(encodedDate);

                    phFormRecoverPasswd.Visible = false;
                    phRecoverSuccess.Visible = true;
                }
                else
                {
                    phFormRecoverPasswd.Visible = false;
                    phRecoverWrong.Visible = true;
                }
            }
            catch (Exception ex)
            {
                components.Error_Show.Show(phError, true, ltrError, ex, phFormRecoverPasswd, true);
                if (log.IsErrorEnabled) log.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Error", ex);
            }

            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }
    }
}
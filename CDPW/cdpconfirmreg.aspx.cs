using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDPW.BLL;

namespace CDPW
{
    public partial class cdpconfirmreg : System.Web.UI.Page
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");

            Boolean WAppUserActivated = false;

            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["sg"]))
                {
                    string email = Request.QueryString["ma"];

                    if (!string.IsNullOrEmpty(email))
                    {
                        if (CDPW.DAL.Users.Check_Email(email, out WAppUserActivated))
                        {
                            if (!WAppUserActivated)
                            {
                                CDPW.DAL.Users.UserAccount_Confirm(email);
                                ltrSignupSuccess.Text = CDPWMessages.MSG_ACCOUNT_ACTIVATED;
                            }
                            else
                            {
                                ltrSignupSuccess.Text = CDPWMessages.MSG_ACCOUNT_ALREADY_ACTIVATED;
                            }
                            phSuccess.Visible = true;
                            phMailIncorrect.Visible = false;
                            phURLIncorrect.Visible = false;
                        }
                        else
                        {
                            phMailIncorrect.Visible = true;
                            phSuccess.Visible = false;
                            phURLIncorrect.Visible = false;
                        }
                    }
                    else
                    {
                        phMailIncorrect.Visible = true;
                        phSuccess.Visible = false;
                        phURLIncorrect.Visible = false;
                    }
                }
                else
                {
                    phMailIncorrect.Visible = false;
                    phSuccess.Visible = false;
                    phURLIncorrect.Visible = true;
                }
            }
            catch (Exception ex)
            {
                components.Error_Show.Show(phError, false, ltrError, ex, phSuccess, true);
                if (log.IsErrorEnabled) log.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Error", ex);
            }
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }
    }
}
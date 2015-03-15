using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using CDPW.BLL;

namespace CDPW
{
    public class Global : System.Web.HttpApplication
    {

        //private static readonly log4net.ILog log;

        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();

            //log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

            //if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");

            //if (HttpContext.Current.Session != null && HttpContext.Current.Session["cdpUser"] != null)
            //{
            //    CurrentUser uLogin = (CurrentUser)HttpContext.Current.Session["cdpUser"];

            //    //check deleted data 
            //    Boolean DeleteUData = false;
            //    DeleteUData = cdplogout.UserData_Delete(uLogin, 0);
            //    uLogin = null;

            //}

            //if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
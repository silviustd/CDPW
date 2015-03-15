using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDPW.DAL;
using CDPW.BLL;
using System.IO;

namespace CDPW
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            //imgUSAForm.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["ThemeDirectory"].ToString() + "images/usa-declaration.jpg";
            //imgCanadaForm.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["ThemeDirectory"].ToString() + "images/canada-declaration.jpg";

            Boolean hasUSCBPPLan = false;
            Boolean hasCBSAPLan = false;
            Boolean unregisteredUser = false;

            //for test purposes
            CurrentUser uLogin1 = (CurrentUser)HttpContext.Current.Session["cdpUser"]; // new CurrentUser();
            //uLogin1.WAppUserId = 1;
            uLogin1.WAppUserName = "user1";
            uLogin1.WAppUserPwd = "user1";
            uLogin1.WAppUserEmail = "silviu.stochita@gmail.com"; ;
            //uLogin1.NoSignUp = false;

            uLogin1.ActiveEnrollments = new List<UserActiveEnrollments>();

            UserActiveEnrollments uAE = new UserActiveEnrollments();
            uAE.PLicenseType = "USCBP";
            uAE.EnrollmentBeginDate = new DateTime(2012, 01, 01);
            uLogin1.ActiveEnrollments.Add(uAE);

            uAE = new UserActiveEnrollments();
            uAE.PLicenseType = "CBSA";
            uAE.EnrollmentBeginDate = new DateTime(2012, 01, 01);
            uLogin1.ActiveEnrollments.Add(uAE);

            HttpContext.Current.Session.Add("cdpUser", uLogin1);
            //for test purposes

            CurrentUser cUser = (CurrentUser)HttpContext.Current.Session["cdpUser"];
            unregisteredUser = cUser.NoSignUp;

            //enable/disable image links cf enrollments
            if (unregisteredUser) {
                lblLUSCBP.Text = "<A HREF='DUSAForm.aspx'>U.S. Customs and Border Protection Customs Declaration</A>";
                lblLCBSA.Text = "<A HREF='DCANForm.aspx'>Canada Border Services Agency Declaration Card</A>";
            }
            else if (!unregisteredUser && (cUser.ActiveEnrollments == null || cUser.ActiveEnrollments.Capacity == 0))
            {
                lblLUSCBP.Text = "U.S. Customs and Border Protection Customs Declaration";
                lblLCBSA.Text = "Canada Border Services Agency Declaration Card";
            }
            else
            {
                //UserActiveEnrollments uAEnroll = new UserActiveEnrollments();
                lblLUSCBP.Text = "U.S. Customs and Border Protection Customs Declaration";
                lblLCBSA.Text = "Canada Border Services Agency Declaration Card";

                foreach (UserActiveEnrollments uAEnroll in cUser.ActiveEnrollments)
                {
                    if (String.Compare(uAEnroll.PLicenseType, "USCBP", true) == 0)
                    {
                        lblLUSCBP.Text = "<A HREF='DUSAForm.aspx'>U.S. Customs and Border Protection Customs Declaration</A>";
                        hasUSCBPPLan = true;
                    }

                    if (String.Compare(uAEnroll.PLicenseType, "CBSA", true) == 0)
                    {
                        lblLCBSA.Text = "<A HREF='DCANForm.aspx'>Canada Border Services Agency Declaration Card</A>";
                        hasCBSAPLan = true;
                    }
                }
            }

            if (!unregisteredUser)
            {
                String strlblPUSCBP = String.Empty;
                String strBuffer = String.Empty;
                //foreach (string line in File.ReadLines("C:\\AInstalls\\Dropbox\\CDP_Web\\3 - Transfer\\silvius\\CDP_web_mine\\CDP_web\\_ppButtonsUSCBPPlan.txt"))
                if (hasUSCBPPLan)
                {
                    foreach (string line in File.ReadLines(Server.MapPath("./components/_subUSCBPPlan.txt")))
                    {
                        if (line.Contains("<subunsub>"))
                        {
                            strBuffer = line.Replace("<subunsub>", "subscribed");
                        }
                        else
                        {
                            strBuffer = line;
                        }
                        strlblPUSCBP += strBuffer.Replace('"', '\'');
                    }
                }
                else
                {
                    foreach (string line in File.ReadLines(Server.MapPath("./components/_ppButtonsUSCBPPlan.txt")))
                    {
                        if (line.Contains("<subunsub>"))
                        {
                            strBuffer = line.Replace("<subunsub>", "subscribed");
                        }
                        else
                        {
                            strBuffer = line;
                        }
                        strlblPUSCBP += strBuffer.Replace('"', '\'');
                    }
                }


                lblCUSCBP.Text = strlblPUSCBP;
            }
            else
            {
                lblCUSCBP.Text = "Plans are available only for registered users";
            }

            if (!unregisteredUser)
            {
                String strlblPCBSA = String.Empty;
                String strBuffer = String.Empty;
                //foreach (string line in File.ReadLines("C:\\AInstalls\\Dropbox\\CDP_Web\\3 - Transfer\\silvius\\CDP_web_mine\\CDP_web\\_ppButtonsCBSAPlan.txt"))

                if (hasCBSAPLan)
                {
                    foreach (string line in File.ReadLines(Server.MapPath("./components/_subCBSAPlan.txt")))
                    {
                        if (line.Contains("<subunsub>"))
                        {
                            strBuffer = line.Replace("<subunsub>", "subscribed");
                        }
                        else
                        {
                            strBuffer = line;
                        }

                        strlblPCBSA += strBuffer.Replace('"', '\'');
                    }
                }
                else
                {
                    foreach (string line in File.ReadLines(Server.MapPath("./components/_ppButtonsCBSAPlan.txt")))
                    {
                        if (line.Contains("<subunsub>"))
                        {
                            strBuffer = line.Replace("<subunsub>", "subscribed");
                        }
                        else
                        {
                            strBuffer = line;
                        }

                        strlblPCBSA += strBuffer.Replace('"', '\'');
                    }
                }
                lblCCBSA.Text = strlblPCBSA;
            }
            else
            {
                //lblCCBSA.Text = "Plans are available only for registered users";
            }





            /*
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["cdpUser"] != null)
            {
                object obj = HttpContext.Current.Session["cdpUser"];

                if (obj is CurrentUser)
                {
                    CurrentUser uLogin = (CurrentUser)obj;

                    ltrSession.Text = "Nume utilizator: " + uLogin.WAppUserName;
                    ltrSession.Text += "<br />Email: " + uLogin.WAppUserEmail;
                    ltrSession.Text += "<br />parola: " + uLogin.WAppUserPwd;
                    ltrSession.Text += "<br />NoSignUp: " + uLogin.NoSignUp;
                    //ltrSession.Text += "<br />Enrollment BeginDate: " + uLogin.EnrollmentsBeginDate;
                    //ltrSession.Text += "<br />Enrollment EndDate: " + uLogin.EnrollmentsEndDate;
                    //ltrSession.Text += "<br />License Type: " + uLogin.PLicensesType;
                }
                else if (obj is UserNoSignup)
                {
                    UserNoSignup uNoSignup = (UserNoSignup)obj;

                    ltrSession.Text = "Nume utilizator: " + uNoSignup.WAppUserName;
                    ltrSession.Text += "<br />NoSignUp: " + uNoSignup.NoSignUp;
                }
            }
            else
            {
                //Response.Redirect("cdplogin.aspx");

                // Setting a session object for this user - nosignup
                string uname_extension = string.Format("{0}{1}{2}{3}{4}{5}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                UserNoSignup uNoSignup = new UserNoSignup();
                uNoSignup.WAppUserName = string.Format("nosignup{0}", uname_extension);
                uNoSignup.NoSignUp = true;

                if (HttpContext.Current.Session.Equals("cdpUser") != true)
                {
                    HttpContext.Current.Session.Add("cdpUser", uNoSignup);
                }
                uNoSignup = null;
            }
             */
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            //Response.Redirect("http://www.columnasoft.com/vladt/cdpmsgreg.aspx?plan=CBSA&act=sub");  
            Response.Redirect("cdpmsgreg.aspx?plan=CBSA&act=sub");
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            //Response.Redirect("http://www.columnasoft.com/vladt/cdpmsgreg.aspx?plan=CBSA&act=unsub");  
            Response.Redirect("cdpmsgreg.aspx?plan=CBSA&act=unsub");
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            //Response.Redirect("http://www.columnasoft.com/vladt/cdpmsgreg.aspx?plan=USCBP&act=sub");  
            Response.Redirect("cdpmsgreg.aspx?plan=USCBP&act=sub");
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            //Response.Redirect("http://www.columnasoft.com/vladt/cdpmsgreg.aspx?plan=USCBP&act=unsub");  
            Response.Redirect("cdpmsgreg.aspx?plan=USCBP&act=unsub");
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }



    }
}
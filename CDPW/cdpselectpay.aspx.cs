using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.IO;
using CDPW.BLL;


namespace CDPW
{
    public partial class cdpselectpay : System.Web.UI.Page
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            //imgUSAForm.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["ThemeDirectory"].ToString() + "images/usa-declaration.png";
            //imgCanadaForm.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["ThemeDirectory"].ToString() + "images/canada-declaration.png";

            Boolean hasUSCBPPLan = false;
            Boolean hasCBSAPLan = false;
            Boolean unregisteredUser = true;

            /* //for test purposes
             CurrentUser uLogin1 = new CurrentUser();
             //uLogin1.WAppUserId = 1; 
             uLogin1.WAppUserName = "user1";
             uLogin1.WAppUserPwd = "user1";
             uLogin1.WAppUserEmail = "user1"; ;
             uLogin1.NoSignUp = false;

             uLogin1.ActiveEnrollments = new List<UserActiveEnrollments>();

             UserActiveEnrollments uAE = new UserActiveEnrollments();
             uAE.PLicenseType = "USCBP";
             uAE.EnrollmentBeginDate  = new DateTime(2012,01,01);
             //uLogin1.ActiveEnrollments.Add(uAE);

             uAE = new UserActiveEnrollments();
             uAE.PLicenseType = "CBSA";
             uAE.EnrollmentBeginDate = new DateTime(2012, 01, 01);
             uLogin1.ActiveEnrollments.Add(uAE);  


             HttpContext.Current.Session.Add("cdpUser", uLogin1);
             //for test purposes */

            try
            {
                 //throw new Exception("testing");

                if (HttpContext.Current.Session != null && HttpContext.Current.Session["cdpUser"] != null)
                {
                    CurrentUser cUser = (CurrentUser)HttpContext.Current.Session["cdpUser"];
                    unregisteredUser = cUser.NoSignUp;

                    if (!Page.IsPostBack)
                    {
                        //enable/disable image links cf enrollments
                        if (unregisteredUser || (cUser.ActiveEnrollments == null || cUser.ActiveEnrollments.Capacity == 0))
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

                            //lblCUSCBP.Text = strlblPUSCBP;
                        }
                        else
                        {
                            //lblCUSCBP.Text = "Plans are available only for registered users";
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
                            //lblCCBSA.Text = strlblPCBSA;
                        }
                        else
                        {
                            //lblCCBSA.Text = "Plans are available only for registered users";
                        }

                    }

                    if (String.Compare(System.Configuration.ConfigurationManager.AppSettings["runmode"].ToString(), "test", true) == 0) {
                        lblLCBSA.Text = "<A HREF='DCANForm.aspx'>Canada Border Services Agency Declaration Card</A>";
                        lblLUSCBP.Text = "<A HREF='DUSAForm.aspx'>U.S. Customs and Border Protection Customs Declaration</A>";
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
                components.Error_Show.Show(phError, true, ltrError, ex, null, false);
                if (log.IsErrorEnabled) log.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Error", ex);
            }


        }
    }
}
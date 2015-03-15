using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using CDPW.BLL;
using CDPW.DAL;

namespace CDPW
{
    public partial class cdplogout : System.Web.UI.Page
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            Boolean LNotKeepData = false;
            SqlTransaction tran = null;
            Boolean DeleteData = false;

            try
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["cdpUser"] != null)
                {
                    CurrentUser cU = (CurrentUser)HttpContext.Current.Session["cdpUser"];
                    LNotKeepData = cU.NotKeepData;

                    if (LNotKeepData)
                    {

                        DeleteData = UserData_Delete(cU, 0);

                    }

                    HttpContext.Current.Session.Remove("cdpUser");

                    Session.Abandon();

                    Response.Redirect("cdplogin.aspx", false);
                }
                else
                {
                    Response.Redirect("cdplogin.aspx");
                }
            }
            catch (Exception ex)
            {
                components.Error_Show.Show(phError, true, ltrError, ex, null, false);
                if (log.IsErrorEnabled) log.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Error", ex);
                if (tran != null)
                {
                    tran.Rollback();
                    tran.Dispose();
                }

            }
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }

        public static Boolean UserData_Delete(CurrentUser cU, byte Action)
        {

            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            Boolean bRet = false;
            SqlTransaction tran = null;

            try
            {
                SqlConnection dbConnection = new SqlConnection(DBConnection.ConnectionString);

                dbConnection.Open();

                tran = dbConnection.BeginTransaction();
                //add action - logout or continue delte
                //cont delete - check table againt tablestodelete & start from
                SqlCommand cmd = new SqlCommand("UserData_Delete", dbConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters to SPROC
                cmd.Parameters.Add("@WUId", SqlDbType.BigInt).Value = cU.WAppUserId;
                cmd.Parameters.Add("@Action", SqlDbType.TinyInt).Value = Action;
                cmd.Parameters.Add("@NoSignUp", SqlDbType.Bit).Value = cU.NoSignUp;
                cmd.Parameters.Add("@NotKeepData", SqlDbType.Bit).Value = cU.NotKeepData;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();

                cmd.Dispose();

                bRet = true;
            }
            catch (Exception ex)
            {
                if (tran != null)
                {
                    tran.Rollback();
                    tran.Dispose();
                }

                throw ex;

            }
            finally
            {

            }


            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");

            return bRet;
        }

    }
}
﻿using System;
using System.Web.UI.WebControls;
using System.Timers;
using System.Web.UI;

namespace CDPW.BLL
{
    public class Helpers
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static String RedirectPage = "cdplogin.aspx";
        public static Boolean bRedirect = false;

        /// <summary>
        /// Create a dropdownlist based on Dataset.
        /// </summary>
        /// <param name="ddl">The DropDownList to fill.</param>
        /// <param name="SelectedValue">Selected item value.</param>
        /// <param name="ds">Dataset to use.</param>
        /// <param name="DataTextField">The column name for text to show on list.</param>
        /// <param name="DataValueField">The column name for the list's value.</param>
        public static void Lists(DropDownList ddl, string SelectedValue, System.Data.DataSet ds, string DataTextField, string DataValueField)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            ddl.DataSource = ds;
            ddl.DataTextField = DataTextField;
            ddl.DataValueField = DataValueField;
            //ddl.SelectedValue = string.IsNullOrEmpty(SelectedValue) ? string.Empty : SelectedValue;
            if (!string.IsNullOrEmpty(SelectedValue)) { ddl.SelectedValue = SelectedValue; }
            ddl.DataBind();
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }

        public static void setTimer(Int32 ms)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            // Set the Interval to 5 seconds.
            aTimer.Interval = ms;
            aTimer.Enabled = true;
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            //Response.Redirect(RedirectPage); 
            bRedirect = true;
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }

        public static void EnDisControls( PlaceHolder pg, string css, Boolean bEn) {

            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            //Boolean bRet = false;

            foreach (Control ct in pg.Controls) {
                if (ct.HasControls()) {
                    foreach (Control c in ct.Controls) {
                        if (c is TextBox)
                        {
                            ((TextBox)c).Enabled = bEn;
                            ((TextBox)c).CssClass = css;
                        }
                        if (c is DropDownList)
                        {
                            ((DropDownList)c).Enabled = bEn;
                            ((DropDownList)c).CssClass = css;
                        }
                    }
                }
                
            }

            //bRet = true; 
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
            //return bRet; 
        }

    }
}
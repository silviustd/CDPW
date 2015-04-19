using System;
using System.Web.UI.WebControls;
using CDPW.BLL;

namespace CDPW.components
{
    public class Error_Show
    {
        public static void Show(PlaceHolder ph, bool show, Literal ltr, System.Exception ex, PlaceHolder toHide, bool doHide, string ToShow = "")
        {
            ph.Visible = show;
            //string strError = string.Format("Mesaj: {0}<br />Data: {4}<br />Exceptie: {1}<br />Sursa: {2}<br />StackTrace: {3}", ex.Message, ex.InnerException, ex.Source, ex.StackTrace, ex.Data);
            String strError = CDPWMessages.ERR_MSG_ERR;
            ltr.Text = strError;
            if (doHide) { toHide.Visible = false; }

            ltr.Text += "<br />"+ ToShow;
        }

        public static void Show(PlaceHolder ph, bool show, Literal ltr, String errMsg, PlaceHolder toHide, bool doHide, string ToShow = "")
        {
            ph.Visible = show;
            //string strError = string.Format("Mesaj: {0}<br />Data: {4}<br />Exceptie: {1}<br />Sursa: {2}<br />StackTrace: {3}", ex.Message, ex.InnerException, ex.Source, ex.StackTrace, ex.Data);
            string strError = errMsg;
            ltr.Text = strError;
            if (doHide) { toHide.Visible = false; }

            ltr.Text += "<br />" + ToShow;
        }
    }
}
using System;
using System.Web.UI.WebControls;

namespace CDPW.components
{
    public class Error_Show
    {
        public static void Show(PlaceHolder ph, bool show, Literal ltr, System.Exception ex, PlaceHolder toHide, bool doHide, string ToShow = "")
        {
            ph.Visible = show;
            //string strError = string.Format("Mesaj: {0}<br />Data: {4}<br />Exceptie: {1}<br />Sursa: {2}<br />StackTrace: {3}", ex.Message, ex.InnerException, ex.Source, ex.StackTrace, ex.Data);
            string strError = string.Format("There was an error:<br /> {0}", ex.Message);
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
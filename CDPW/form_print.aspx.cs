using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using CDPW.DAL;
using CDPW.BLL;

namespace CDPW
{
    public partial class form_print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    long WAppUserId;
                    
                    CurrentUser cu = new CurrentUser();

                    if (HttpContext.Current.Session != null && HttpContext.Current.Session["cdpUser"] != null)
                    {
                        object obj = HttpContext.Current.Session["cdpUser"];

                        if (obj is CurrentUser)
                        {
                            CurrentUser uLogin = (CurrentUser)obj;
                            WAppUserId = uLogin.WAppUserId;
                    //WAppUserId = 1;  // for DUSAForm
                    //WAppUserId = 333;  // for DCANForm, but it gives errors for subreport Person

                            string Raport = Request.QueryString["Report"];
                            rwPrintPreview.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;

                            switch (Raport)
                            {
                                case "USForm":
                                    rwPrintPreview.LocalReport.ReportPath = Server.MapPath(@"USForm.rdlc");
                                    rwPrintPreview.LocalReport.DataSources.Add(new ReportDataSource(rwPrintPreview.LocalReport.GetDataSourceNames()[0], Load_USReport_Data(WAppUserId)));
                                    break;
                                case "USForm1":
                                    rwPrintPreview.LocalReport.ReportPath = Server.MapPath(@"USForm_Back.rdlc");
                                    rwPrintPreview.LocalReport.DataSources.Add(new ReportDataSource(rwPrintPreview.LocalReport.GetDataSourceNames()[0], Load_USReport1_Data(WAppUserId)));
                                    break;
                                case "CNDForm":
                                    rwPrintPreview.LocalReport.ReportPath = Server.MapPath(@"CanadianForm.rdlc");
                                    rwPrintPreview.LocalReport.DataSources.Add(new ReportDataSource(rwPrintPreview.LocalReport.GetDataSourceNames()[0], Load_CNDReport_Data(WAppUserId)));
                                    rwPrintPreview.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
                                    break;
                            }

                        }
                        else
                        {
                            Response.Redirect("cdplogin.aspx");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                components.Error_Show.Show(phError, true, ltrError, ex, phRaport, false);
            }
        }



        private DataTable Load_USReport_Data(long UserId)
        {
            DataTable table = null;

            try
            {
                CDP_Dataset.Get_US_FlightInfoDataTable dt = new CDP_Dataset.Get_US_FlightInfoDataTable();
                CDP_DatasetTableAdapters.Get_US_FlightInfoTableAdapter ta = new CDP_DatasetTableAdapters.Get_US_FlightInfoTableAdapter();
                ta.Fill(dt, UserId);
                table = dt;
            }
            catch (Exception ex)
            {
                components.Error_Show.Show(phError, false, ltrError, ex, phRaport, false);
            }

            return table;
        }

        private DataTable Load_USReport1_Data(long UserId)
        {
            DataTable table = null;

            try
            {
                CDP_Dataset.Get_US_FlightInfo2DataTable dt = new CDP_Dataset.Get_US_FlightInfo2DataTable();
                CDP_DatasetTableAdapters.Get_US_FlightInfo2TableAdapter ta = new CDP_DatasetTableAdapters.Get_US_FlightInfo2TableAdapter();
                ta.Fill(dt, UserId);
                table = dt;
            }
            catch (Exception ex)
            {
                components.Error_Show.Show(phError, false, ltrError, ex, phRaport, false);
            }

            return table;
        }

        private DataTable Load_CNDReport_Data(long UserId)
        {
            DataTable table = null;

            try
            {
                CDP_Dataset.Get_CAN_FlightInfoDataTable dt = new CDP_Dataset.Get_CAN_FlightInfoDataTable();
                CDP_DatasetTableAdapters.Get_CAN_FlightInfoTableAdapter ta = new CDP_DatasetTableAdapters.Get_CAN_FlightInfoTableAdapter();
                ta.Fill(dt, UserId);
                table = dt;
            }
            catch (Exception ex)
            {

                components.Error_Show.Show(phError, false, ltrError, ex, phRaport, false);
            }

            return table;
        }

        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            try
            {
                switch (e.DataSourceNames[0])
                {
                    case "dsPersons":
                        CDP_Dataset.Get_CAN_PersonsDataTable dtPersons = new CDP_Dataset.Get_CAN_PersonsDataTable();
                        CDP_DatasetTableAdapters.Get_CAN_PersonsTableAdapter taPersons = new CDP_DatasetTableAdapters.Get_CAN_PersonsTableAdapter();
                        taPersons.Fill(dtPersons, long.Parse(e.Parameters["AddressId"].Values[0]));
                        e.DataSources.Add(new ReportDataSource(e.DataSourceNames[0], (DataTable)dtPersons));
                        break;
                    case "dsGoods1":
                        CDP_Dataset.Get_CAN_Goods1DataTable dtGoods1 = new CDP_Dataset.Get_CAN_Goods1DataTable();
                        CDP_DatasetTableAdapters.Get_CAN_Goods1TableAdapter taGoods1 = new CDP_DatasetTableAdapters.Get_CAN_Goods1TableAdapter();
                        taGoods1.Fill(dtGoods1, long.Parse(e.Parameters["AddressId"].Values[0]));
                        e.DataSources.Add(new ReportDataSource(e.DataSourceNames[0], (DataTable)dtGoods1));
                        break;
                    case "dsGoods2":
                        CDP_Dataset.Get_CAN_Goods2DataTable dtGoods2 = new CDP_Dataset.Get_CAN_Goods2DataTable();
                        CDP_DatasetTableAdapters.Get_CAN_Goods2TableAdapter taGoods2 = new CDP_DatasetTableAdapters.Get_CAN_Goods2TableAdapter();
                        taGoods2.Fill(dtGoods2, long.Parse(e.Parameters["AddressId"].Values[0]));
                        e.DataSources.Add(new ReportDataSource(e.DataSourceNames[0], (DataTable)dtGoods2));
                        break;
                }

            }
            catch (Exception ex)
            {
                components.Error_Show.Show(phError, false, ltrError, ex, phRaport, false);
            }
        }
    }
}
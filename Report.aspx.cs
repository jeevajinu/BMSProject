using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
namespace BMS
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("Report.rdlc");
                BMSdbEntities entities = new BMSdbEntities();
                //ReportDataSource datasource = new ReportDataSource("MaintanenceTable", (from MaintanenceTables in entities.MaintanenceTable.Take(10)
                //                                                                        select MaintanenceTables));
                ReportDataSource datasource = new ReportDataSource("MaintananceDS", (from customer in entities.MaintanenceTables.Take(10)
                                                                                 select customer));
                ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.AsyncRendering = false;
                ReportViewer1.SizeToReportContent = true;
                ReportViewer1.ZoomMode = ZoomMode.FullPage;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Home/ShowAdmin");
        }
    }
}
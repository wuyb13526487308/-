using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RX.Gas.ReportLib;
using DevExpress.XtraReports.UI;

namespace ReportWebLib
{
    public partial class ReportViewForm : System.Web.UI.Page
    {
        XtraReport xtraReport;
        protected void Page_Load(object sender, EventArgs e)
        {
            BaseReport report = this.Session["Report"] as BaseReport;
            if (report != null)
                OpenReport(report);
        }

        private void OpenReport(BaseReport report)
        {
            if (report == null) return;
            xtraReport = XtraReport.FromStream(report.getReportStream(), true);
            this.xtraReport.DataSource = report.DataSource;
            this.ReportViewer1.Report = this.xtraReport;
        }
    }
}
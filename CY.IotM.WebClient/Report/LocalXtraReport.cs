using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.XtraReports.UI;
using RX.Gas.ReportLib;

namespace ReportWebLib
{
    public class LocalXtraReport : DevExpress.XtraReports.UI.XtraReport
    {
        private TopMarginBand topMarginBand1;
        private DetailBand detailBand1;
        private BottomMarginBand bottomMarginBand1;
    
        public LocalXtraReport()
        {
            //InitializeComponent();
            //isFirst = true;
            this.PrintingSystem.ShowMarginsWarning = false;
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(LocalXtraReport_BeforePrint);
            this.PreviewClick += new PreviewMouseEventHandler(LocalXtraReport_PreviewClick);
        }

        void LocalXtraReport_PreviewClick(object sender, PreviewMouseEventArgs e)
        {
            this.PrintingSystem.ShowMarginsWarning = false;
            Fill();
        }

        void LocalXtraReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //打印之前的操作
            //设置打印机纸张
            //this.DefaultPrinterSettingsUsing.AllSettingsUsed = true;
            this.DefaultPrinterSettingsUsing.UseLandscape = false;
            this.DefaultPrinterSettingsUsing.UseMargins = false;
            this.DefaultPrinterSettingsUsing.UsePaperKind = false;
            this.PrintingSystem.ShowMarginsWarning = false;
            //MessageBox.Show(this.PrinterName);            
            Fill();
        }

        /// <summary>
        /// 加载报表数据
        /// </summary>
        private void Fill()
        {
            try
            {
                if (this.DataSource != null && this.DataSource.GetType().FullName == "RX.Gas.ReportLib.ReportDataAdaptor")
                {

                    ReportDataAdaptor reportDataAdaptor = (ReportDataAdaptor)this.DataSource;
                    string result = reportDataAdaptor.Fill();
                    if (result != "")
                    {
                        //File.WriteAllText(@"C:\report_log.txt", result);
                        //MessageBox.Show(result);
                    }
                }
            }
            catch { }
           // CY.Common.LoadingForm.getInstance().Hide();
        }

        private void InitializeComponent()
        {
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.detailBand1 = new DevExpress.XtraReports.UI.DetailBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // detailBand1
            // 
            this.detailBand1.Name = "detailBand1";
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // LocalXtraReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.topMarginBand1,
            this.detailBand1,
            this.bottomMarginBand1});
            this.Version = "12.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
    }
}
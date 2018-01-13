using CY.IoTM.Common;
using CY.IoTM.Common.ADSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CY.IoTM.MeterSimulator
{
    public partial class UpLoadFile : Form
    {
        public UpLoadFile()
        {
            InitializeComponent();
            this.txtDate.Text = string.Format("{0:yyMMdd}", DateTime.Now);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtFile.Text = this.openFileDialog1.FileName;
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            WCFServiceProxy<IADFileService> _iTaskManageProxy = new WCFServiceProxy<IADFileService>();
            System.IO.FileStream fs = System.IO.File.OpenRead(this.txtFile.Text);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            fs.Close();
            string fileName = string.Format("{0}{1}{2}{3}", txtCompanyID.Text, txtDate.Text, txtOrder.Value.ToString().PadLeft(5, '0'),System.IO.Path.GetExtension(this.txtFile.Text));
            string result = _iTaskManageProxy.getChannel.UpLoad(txtCompanyID.Text, fileName, buffer);
            _iTaskManageProxy.Close();
            if (result == "")
            {
                MessageBox.Show("上传完成。");
                this.txtOrder.Value++;
            }
            else
            {
                MessageBox.Show("上传失败，原因：" + result);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPublish_Click(object sender, EventArgs e)
        {
            WCFServiceProxy<IADPublishManager> _iTaskManageProxy = new WCFServiceProxy<IADPublishManager>();
            string result = _iTaskManageProxy.getChannel.ADPublish(Convert.ToInt64(this.txtAP.Value));
            _iTaskManageProxy.Close();
            if (result == "")
            {
                MessageBox.Show("发布完成");
            }
            else
            {
                MessageBox.Show("发布失败，原因：" + result);
            }
        }
    }
}

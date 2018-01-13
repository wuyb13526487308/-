using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CY.IoTM.Common;
using CY.IoTM.Common.Business;

namespace CY.IoTM.MeterSimulator
{
    public partial class FormMeterManage : Form
    {
        WCFServiceProxy<IDianHuo> _iTaskManageProxy = new WCFServiceProxy<IDianHuo>();
        IoT_Meter meter;
        public FormMeterManage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                meter = _iTaskManageProxy.getChannel.QueryMeter(this.txt_meterNo.Text);
                if (meter != null)
                {
                    this.bindingSource1.DataSource = meter;
                }
            }
            catch
            {
            }

        }

        private void btnDH_Click(object sender, EventArgs e)
        {
            if (this.meter == null) return;
            List<IoT_Meter> list = new List<IoT_Meter>();
            this.meter.MeterState = '5';
            list.Add(meter);
            string result = _iTaskManageProxy.getChannel.Do(list);
            if (result == "")
                MessageBox.Show("点火任务下发完成");
            else
                MessageBox.Show("点火任务下发失败："+result);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //关阀操作
            if (this.meter == null) return;
            List<IoT_Meter> list = new List<IoT_Meter>();
            FormValveControlcs frm = new FormValveControlcs();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                WCFServiceProxy<IValveControl>  _valveProxy = new WCFServiceProxy<IValveControl> ();

                string result = _valveProxy.getChannel.TurnOff(this.meter, frm.Reason, "test");
                _valveProxy.CloseChannel();
                if (result == "")
                    MessageBox.Show("关阀任务下发成功");
                else
                    MessageBox.Show("关阀任务下发失败，原因：" + result);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.meter == null) return;
            List<IoT_Meter> list = new List<IoT_Meter>();
            FormValveControlcs frm = new FormValveControlcs();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                WCFServiceProxy<IValveControl> _valveProxy = new WCFServiceProxy<IValveControl>();

                string result = _valveProxy.getChannel.TurnOn(this.meter, frm.Reason, "test");
                _valveProxy.CloseChannel();
                if (result == "")
                    MessageBox.Show("开阀阀任务下发成功");
                else
                    MessageBox.Show("开阀任务下发失败，原因：" + result);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //充值操作
            FormTopUp topup = new FormTopUp();
            if (topup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                WCFServiceProxy<IMeterTopUp> _valveProxy = new WCFServiceProxy<IMeterTopUp>();
                string result = _valveProxy.getChannel.Topup(this.meter.MeterNo.Trim(), topup.topUp, TopUpType.本地营业厅, "", "",new IoT_MeterTopUp ());
                _valveProxy.CloseChannel();
                if (result == "")
                    MessageBox.Show("充值任务下发成功");
                else
                    MessageBox.Show("充值任务下发失败，原因：" + result);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            new FormTaskQuery().Show();
        }
    }
}

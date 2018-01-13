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
    public partial class AddMSimulatorForm : Form
    {
        public List<Simulator> Simulatorlist = new List<Simulator>();
        List<MSimulator> mSimulatorList;
        string host;
        int port;
        int zhouqi = 5;
        public AddMSimulatorForm(List<MSimulator> mSimulatorList, string host, int port, int zhouqi = 5)
        {
            InitializeComponent();
            this.mSimulatorList = mSimulatorList;
            this.host = host;
            this.port = port;
            this.zhouqi = zhouqi;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            WCFServiceProxy<ITaskManage> _iTaskManageProxy = new WCFServiceProxy<ITaskManage>();
            try
            {
                List<IoT_Meter> list = _iTaskManageProxy.getChannel.getIotMeters(this.textBox1.Text.Trim(), this.textBox2.Text.Trim());
                _iTaskManageProxy.CloseChannel();
                if (list != null)
                {
                    foreach (IoT_Meter m in list)
                    {
                        this.Simulatorlist.Add(new Simulator() { 选择 = false, 表号 = m.MeterNo.Trim(), 户号 = m.UserID, 表类型 = m.MeterType });
                    }
                    this.bindingSource1.DataSource = this.Simulatorlist;

                    this.dataGridView1.DataSource = this.bindingSource1;
                    this.dataGridView1.Refresh();
                }
            }
            catch
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.bindingSource1.EndEdit();
            //foreach (Simulator sim in this.list)
            //{
            //    if (sim.选择)
            //    {
            //        MSimulator simulator = MSimulator.Create (sim.表号,this.host,this.port);
            //        this.mSimulatorList.Add(simulator);
            //        simulator.Start();
            //    }
            //}
            this.Close();
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //权限
            if (Simulatorlist == null) return;
            foreach (Simulator sim in Simulatorlist)
                sim.选择 = true;
            this.dataGridView1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Simulatorlist == null) return;
            foreach (Simulator sim in Simulatorlist)
                sim.选择 = false;
            this.dataGridView1.Refresh();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Simulatorlist == null) return;
            foreach (Simulator sim in Simulatorlist)
                sim.选择 = !sim.选择;
            this.dataGridView1.Refresh();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //条件选择

        }
    }


    public class Simulator
    {
        public bool 选择 { get; set; }
        public string 表号 { get; set; }
        public string 户号 { get; set; }
        public string 表类型 { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CY.IoTM.MeterSimulator
{
    public partial class Form1 : Form
    {
        List<MSimulator> list = new List<MSimulator>();
        List<MSimulatorListViewItem> listViewItems = new List<MSimulatorListViewItem>();
        int pageCount = 1;
        int currentPageIndex = 1;
        const int PageLength = 20;

        public Form1()
        {
            InitializeComponent();
            this.txt_server.Text = CY.IoTM.MeterSimulator.Properties.Settings.Default.IP;
            this.bindingSource1.DataSource = this.list;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new MeterTest(this.txt_server.Text, Convert.ToInt32(this.txt_port.Text)).Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            new FormMeterManage().ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            AddMSimulatorForm frm = new AddMSimulatorForm(this.list,this.txt_server.Text ,Convert.ToInt32(this.txt_port.Text));

            frm.ShowDialog();
            int index = this.listViewItems.Count+1;
            foreach (Simulator sim in frm.Simulatorlist)
            {
                if (sim.选择)
                {
                    var query = (from p in this.list where p.Mac == sim.表号 select p).Count ();
                    if (query == 0)
                    {
                        MSimulator simulator = MSimulator.Create(sim.表号, this.txt_server.Text, Convert.ToInt32(this.txt_port.Text));
                        this.list.Add(simulator);
                        simulator.Start();
                        MSimulatorListViewItem item = new MSimulatorListViewItem(simulator);
                        item.SetIndex(index);
                        if (this.pageCount <= 1 && this.listViewItems.Count < PageLength)
                            this.listView1.Items.Add(item);
                        this.listViewItems.Add(item);
                        this.pageCount = this.listViewItems.Count / PageLength;
                        if ((this.listViewItems.Count % PageLength) > 0)
                            this.pageCount++;
                        index++;
                        Application.DoEvents();
                    }
                }
            }
            this.numericUpDown1.Value = this.currentPageIndex;

            this.labPage.Text = string.Format("{0}/{1}", this.currentPageIndex, this.pageCount);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
              foreach (MSimulatorListViewItem item in this.listViewItems)
                item.Close();
          CY.IoTM.MeterSimulator.Properties.Settings.Default.IP = this.txt_server.Text;
            CY.IoTM.MeterSimulator.Properties.Settings.Default.Save();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (MSimulator m in this.list)
                m.Stop();
        }
        MSimulator _mSimulator;

        private void listView1_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count >= 1)
            {
                this._mSimulator = this.listView1.SelectedItems[0].Tag as MSimulator;
                this.txt_Mac.Text = this._mSimulator.Mac;
                this.txt周期.Value = this._mSimulator.周期;
                this.txt流量.Value = this._mSimulator.hourLiuLiang;

            }
        }

        private void txt周期_ValueChanged(object sender, EventArgs e)
        {
            if (this._mSimulator != null)
                this._mSimulator.周期 = Convert.ToInt32(txt周期.Value);

        }

        private void txt流量_ValueChanged(object sender, EventArgs e)
        {
            if (this._mSimulator != null)
                this._mSimulator.hourLiuLiang = txt流量.Value;

        }

        private void btnWindows_Click(object sender, EventArgs e)
        {
            if (this._mSimulator != null)
                new MeterTest(this.txt_server.Text, Convert.ToInt32(this.txt_port.Text),this._mSimulator).Show();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this._mSimulator != null && MessageBox.Show ("确定移除当前虚拟表嘛?","移除",MessageBoxButtons.YesNo,MessageBoxIcon.Question ) == System.Windows.Forms.DialogResult.Yes)
            {
                this._mSimulator.Stop();
                this.list.Remove(this._mSimulator);
                this.listView1.Items.Remove(this.listView1.SelectedItems[0]);
                this._mSimulator = null;
                int index = 0;
                foreach (ListViewItem item in this.listView1.Items)
                    (item as MSimulatorListViewItem).SetIndex(index++);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.listView1.SelectedItems)
            {
                ((item as MSimulatorListViewItem).Tag as MSimulator).hourLiuLiang = -1;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.listView1.SelectedItems)
            {
                ((item as MSimulatorListViewItem).Tag as MSimulator).hourLiuLiang =this.txt指定值.Value;
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.pageCount == 1) return;
            if (this.currentPageIndex == this.pageCount) return;
            this.listView1.Items.Clear();
            int iCount = PageLength;
            this.currentPageIndex++;
            if (this.currentPageIndex * PageLength > this.listViewItems.Count)
                iCount = this.listViewItems.Count % PageLength;
            for (int i = 0; i < iCount; i++)
                this.listView1.Items.Add(this.listViewItems[(this.currentPageIndex - 1) * PageLength + i]);
            this.labPage.Text = string.Format("{0}/{1}", this.currentPageIndex, this.pageCount);
            this.numericUpDown1.Value = this.currentPageIndex;

         }

        private void button7_Click(object sender, EventArgs e)
        {
            if (this.pageCount == 1) return;
            if (this.currentPageIndex == 1) return;
            this.listView1.Items.Clear();
            int iCount = PageLength;
            this.currentPageIndex--;
            if (this.currentPageIndex * PageLength > this.listViewItems.Count)
                iCount = this.listViewItems.Count % PageLength;
            for (int i = 0; i < iCount; i++)
                this.listView1.Items.Add(this.listViewItems[(this.currentPageIndex - 1) * PageLength + i]);

            this.labPage.Text = string.Format("{0}/{1}", this.currentPageIndex, this.pageCount);
            this.numericUpDown1.Value = this.currentPageIndex;
        }

        private void btnUpLoad_Click(object sender, EventArgs e)
        {
            new UpLoadFile().ShowDialog();
        }
    }


}

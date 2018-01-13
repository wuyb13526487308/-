using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CY.IoTM.MongoDataHelper;
using MongoDB.Driver;

namespace CY.IoTM.MeterSimulator
{
    public partial class MeterTest : Form
    {
        MSimulator _simulator;
        string hostName;
        int port;
        private const string TaskCollectionName = "MSimulator";
        bool isClose = false;
        public MeterTest(string host, int port, MSimulator simulator = null)
        {
            InitializeComponent();
            this.hostName = host;
            this.port = port;
            if (simulator != null)
            {
                this._simulator = simulator;
                this._simulator.OnNoticed += _simulator_OnNoticed;
                this._simulator.OnJiliang += _simulator_OnJiliang;
                this.txt_Mac.Text = this._simulator.Mac;
                this.txt_Mac.ReadOnly = true;
                this.button1.Enabled = false;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this._simulator = Query(this.txt_Mac.Text);
            if (this._simulator == null)
            {
                this._simulator = new MSimulator();
                this._simulator.hostname = hostName;
                this._simulator.port = port;
                this._simulator.周期 = Convert.ToInt32(txt周期.Value);
                this._simulator.Mac = this.txt_Mac.Text;
                this._simulator.Key = "8888888888888888";
                this._simulator.MeterType = "00";
                this._simulator.MKeyVer = 0;
                this._simulator.PriceCheck = "0";
                this._simulator.SettlementDay = 28;
                this._simulator.SettlementType = "00";
                this._simulator.TotalAmount = 0;                
                this._simulator.TotalTopUp = 0;
                this._simulator.ValveState = "0";
                this._simulator.SettlementDateTime = "2015-01-01";
                this._simulator.Insert();
            }
            this._simulator.hostname = hostName;
            this._simulator.port = port;

            this._simulator.OnNoticed += _simulator_OnNoticed;
            this._simulator.OnJiliang += _simulator_OnJiliang;
            this._simulator.Start();

            this.button1.Enabled = false;
        }

        void _simulator_OnJiliang(MSimulator simulator)
        {
            if (this.isClose) return;

            if (this.IsDisposed) return;
            this.Invoke(new MethodInvoker(delegate
            {
                this.txt_ljgas.Text = simulator.TotalAmount.ToString("0.00");
                this.txt_syMoney.Text = simulator.CurrentBalance.ToString("0.00");
                this.txt_zongChongzhi.Text = simulator.TotalTopUp.ToString("0.00");
                this.txt_biaoleix.Text = simulator.MeterType == "00" ? "气量表" : "金额表";
                this.txtGas1.Text = simulator.Gas1.ToString();
                this.txtGas2.Text = simulator.Gas2.ToString();
                this.txtGas3.Text = simulator.Gas3.ToString();
                this.txtGas4.Text = simulator.Gas4.ToString();
                this.txtPrice1.Text = simulator.Price1.ToString();
                this.txtPrice2.Text = simulator.Price2.ToString();
                this.txtPrice3.Text = simulator.Price3.ToString();
                this.txtPrice4.Text = simulator.Price4.ToString();
                this.txtPrice5.Text = simulator.Price5.ToString();
                this.txtLabber.Text = simulator.Ladder.ToString ();
                this.txt周期.Value = simulator.周期;
                this.checkBox1.Checked = simulator.IsUsedLadder;
                this.txtLastBalance.Text = simulator.LastSettlementAmount.ToString ();
                this.txtLastLJGas.Text = simulator.LastTotal.ToString();
                if (simulator.SettlementType == "00")
                    this.txtSettlementType.Text = "月";
                else if (simulator.SettlementType == "01")
                    this.txtSettlementType.Text = "季度";
                else if (simulator.SettlementType == "10")
                    this.txtSettlementType.Text = "半年";
                else
                    this.txtSettlementType.Text = "全年";
                this.txtSettlementMonth.Text = simulator.SettlementMonth.ToString();
                this.txtSettlementDay.Text = simulator.SettlementDay.ToString();
                this.txtValve.Text = simulator.ValveState == "0" ? "开" : "关";

                this.txtCurrentLader.Text = simulator.CurrentLadder.ToString ();
                this.txtCurrentPrice.Text = simulator.CurrentPrice.ToString();
                this.txtNextPoint.Text = simulator.NextSettlementPointGas.ToString();

            }));
        }

        void _simulator_OnNoticed(MSimulator simulator)
        {

            if (this.isClose) return;
            if (this.richTextBox1.IsDisposed) return;
            this.Invoke(new MethodInvoker(delegate
            {
                this.richTextBox1.Text += simulator.Message;
                this.richTextBox1.Refresh();
            }));
        }


        private MSimulator Query(string mac)
        {
            MongoDBHelper<MSimulator> mongo = new MongoDBHelper<MSimulator>();
            QueryDocument query = new QueryDocument();
            query.Add("Mac", mac);
            MongoCursor<MSimulator> mongoCursor = mongo.Query(TaskCollectionName, query);
            var dataList = mongoCursor.ToList();
            if (dataList == null || dataList.Count == 0)
                return null;
            else
                return dataList[0];
        }

        private void MeterTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            this._simulator.OnNoticed -= _simulator_OnNoticed;
            this._simulator.OnJiliang -= _simulator_OnJiliang;

            isClose = true;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if(this._simulator!= null)
                this._simulator.hourLiuLiang = numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (this._simulator != null)
                this._simulator.周期 = Convert.ToInt32(txt周期.Value);
        }

        private void MeterTest_FormClosed(object sender, FormClosedEventArgs e)
        {

            if (_simulator != null)
                _simulator.Stop();
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void btnLCD_Click(object sender, EventArgs e)
        {
            if (this._simulator == null) return;
            new FormLCD(this._simulator).ShowDialog();

        }

        private void ckExcptioin_CheckedChanged(object sender, EventArgs e)
        {
            if(this.ckExcptioin.Checked)
                this._simulator.AnswerType = "异常应答";
            else
                this._simulator.AnswerType = "正常应答";

        }
    }
}

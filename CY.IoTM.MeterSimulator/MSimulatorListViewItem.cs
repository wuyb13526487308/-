using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CY.IoTM.MeterSimulator
{
    class MSimulatorListViewItem : ListViewItem
    {
        MSimulator _mSimulator;
        ListViewSubItem _MeterNo = new ListViewSubItem();//表号
        ListViewSubItem _Netware = new ListViewSubItem();//网络
        ListViewSubItem _MeterType = new ListViewSubItem();//表类型
        ListViewSubItem _阀门 = new ListViewSubItem();//表类型
        ListViewSubItem _TotalAmount = new ListViewSubItem();//累计量
        ListViewSubItem _周期 = new ListViewSubItem();//周期
        ListViewSubItem _流量 = new ListViewSubItem();
        ListViewSubItem _当前阶梯 = new ListViewSubItem();
        ListViewSubItem _当前价格 = new ListViewSubItem();
        ListViewSubItem _下一个阶梯点 = new ListViewSubItem();
        ListViewSubItem _上次结算点 = new ListViewSubItem();
        ListViewSubItem _结算日期 = new ListViewSubItem();
        ListViewSubItem _剩余金额 = new ListViewSubItem();
        ListViewSubItem _启用阶梯价 = new ListViewSubItem();
        ListViewSubItem _阶梯数 = new ListViewSubItem();
        ListViewSubItem _阶梯信息 = new ListViewSubItem();
        ListViewSubItem _信息 = new ListViewSubItem();



        public MSimulatorListViewItem(MSimulator simulator)
        {
            this.Tag = simulator;

            this._mSimulator = simulator;
            this._mSimulator.OnJiliang += _mSimulator_OnJiliang;
            this._mSimulator.OnNoticed += _mSimulator_OnJiliang;
            this.Text = simulator.MeterID.ToString ();
            //表号：
            _MeterNo.Text = simulator.Mac;
            _Netware.Text = simulator.IsOnline ? "在线" : "离线";
            if (simulator.IsOnline)
                _Netware.BackColor = BackColor;
            else
                _Netware.ForeColor = System.Drawing.Color.Red;

            _MeterType.Text = simulator.MeterType == "00" ? "气量表" : "金额表";
            _TotalAmount.Text = simulator.TotalAmount.ToString("0.00");
            _周期.Text = simulator.周期.ToString();
            _流量.Text = simulator.hourLiuLiang.ToString("0.00");
            _当前阶梯.Text = simulator.CurrentLadder.ToString();
            _当前价格.Text = simulator.CurrentPrice.ToString("0.00");
            _下一个阶梯点.Text = simulator.NextSettlementPointGas.ToString("0.00");
            _结算日期.Text = simulator.GetSettlementTimePoint().ToString("yyyy-MM-dd");
            _剩余金额.Text = simulator.CurrentBalance.ToString("0.00");
            _启用阶梯价.Text = simulator.IsUsedLadder ? "启用" : "未启用";
            _阶梯数.Text = simulator.Ladder.ToString();
            _阶梯信息.Text = string.Format("P1:{0} G1:{1} P2:{2} G2:{3} P3:{4} G3:{5} P4:{6} G4:{7} P5:{8}",
                simulator.Price1, simulator.Gas1, simulator.Price2, simulator.Gas2, simulator.Price3, simulator.Gas3, simulator.Price4, simulator.Gas4, simulator.Price5);
            _信息.Text = simulator.Message;
            _阀门.Text = simulator.ValveState == "0" ? "开" : "关";
            this.SubItems.Add(this._MeterNo);
            this.SubItems.Add(this._Netware);
            this.SubItems.Add(this._MeterType);
            this.SubItems.Add(this._阀门);

            this.SubItems.Add(this._TotalAmount);
            this.SubItems.Add(this._周期);
            this.SubItems.Add(this._流量);
            this.SubItems.Add(this._当前阶梯);
            this.SubItems.Add(this._当前价格);
            this.SubItems.Add(this._下一个阶梯点);
            this.SubItems.Add(this._结算日期);
            this.SubItems.Add(this._剩余金额);
            this.SubItems.Add(this._启用阶梯价);
            this.SubItems.Add(this._阶梯数);
            this.SubItems.Add(this._阶梯信息);
            this.SubItems.Add(this._信息);
        }

        void _mSimulator_OnJiliang(MSimulator simulator)
        {
            if (this.ListView == null) return;
            lock (this._mSimulator)
            {
                if (this.ListView == null) return;
                this.ListView.Invoke(new MethodInvoker(delegate
                {
                    _MeterType.Text = simulator.MeterType == "00" ? "气量表" : "金额表";
                    _TotalAmount.Text = simulator.TotalAmount.ToString("0.00");
                    _Netware.Text = simulator.IsOnline ? "在线" : "离线";
                    if (simulator.IsOnline)
                        _Netware.BackColor = BackColor;
                    else
                        _Netware.BackColor = System.Drawing.Color.Red;
                    _MeterType.Text = simulator.MeterType == "00" ? "气量表" : "金额表";
                    _TotalAmount.Text = simulator.TotalAmount.ToString("0.00");
                    _周期.Text = simulator.周期.ToString();
                    _流量.Text = simulator.hourLiuLiang.ToString("0.00");
                    _当前阶梯.Text = simulator.CurrentLadder.ToString();
                    _当前价格.Text = simulator.CurrentPrice.ToString("0.00");
                    _下一个阶梯点.Text = simulator.NextSettlementPointGas.ToString("0.00");
                    _结算日期.Text = simulator.GetSettlementTimePoint().ToString("yyyy-MM-dd");
                    _剩余金额.Text = simulator.CurrentBalance.ToString("0.00");
                    _启用阶梯价.Text = simulator.IsUsedLadder ? "启用" : "未启用";
                    _阶梯数.Text = simulator.Ladder.ToString();
                    _阶梯信息.Text = string.Format("P1:{0} G1:{1} P2:{2} G2:{3} P3:{4} G3:{5} P4:{6} G4:{7} P5:{8}",
                        simulator.Price1, simulator.Gas1, simulator.Price2, simulator.Gas2, simulator.Price3, simulator.Gas3, simulator.Price4, simulator.Gas4, simulator.Price5);
                    _信息.Text = simulator.Message;
                    _阀门.Text = simulator.ValveState == "0" ? "开" : "关";

                }));
            }
        }

        public void SetIndex(int index)
        {
            this.Text = index.ToString();
        }

        public void Close()
        {
            this._mSimulator.OnJiliang -= _mSimulator_OnJiliang;
            this._mSimulator.OnNoticed -= _mSimulator_OnJiliang;

        }
    }
}

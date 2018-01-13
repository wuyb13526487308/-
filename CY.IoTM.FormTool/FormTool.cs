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
using CY.IoTM.Common.Item;
using CY.IoTM.Protocol;



namespace CY.IoTM.FormTool
{
    public partial class FormTool : Form
    {



        string meterId = "39615123355104";

        string meterType = "SerialPort";

        TestMeter meter = null;


        public FormTool()
        {
            InitializeComponent();

            Channel.IDataChannel channel = Channel.DataChannelFactoryService.getInstance().getDataChannel(meterId, meterType);

            meter = new TestMeter(meterId, channel);
        }





        // 读计量数据 = 0x901F,
        //历史计量数据1 = 0xD120,
        //历史计量数据2 = 0xD121,
        //历史计量数据3 = 0xD122,
        //历史计量数据4 = 0xD123,
        //历史计量数据5 = 0xD124,
        //历史计量数据6 = 0xD125,
        //历史计量数据7 = 0xD126,
        //历史计量数据8 = 0xD127,
        //历史计量数据9 = 0xD128,
        //历史计量数据10 = 0xD129,
        //历史计量数据11 = 0xD12A,
        //历史计量数据12 = 0xD12B,
        //读价格表 = 0x8102,
        //读结算日 = 0x8103,
        //读抄表日 = 0x8104,
        //读购入金额 = 0x8105,
        //读密钥版本号 = 0x8106,
        //读地址 = 0x810A,




        private void ReadDataCmdSend(DataItem item)
        {
            TaskArge data = new TaskArge(meterId, item, ControlCode.ReadData);
            string str = "发送:" + meter.SendData(data);

            SetTextDo(str);
        
        }


        private void btn_901f_Click(object sender, EventArgs e)
        {
         
            //ReadDataCmdSend(new DataItem_901F());

        }

        private void btn_8102_Click(object sender, EventArgs e)
        {
            ReadDataCmdSend(new DataItem_8102());
        }

        private void btn_8103_Click(object sender, EventArgs e)
        {
            ReadDataCmdSend(new DataItem_8103());
        }

        private void btn_8104_Click(object sender, EventArgs e)
        {
            ReadDataCmdSend(new DataItem_8104());
        }

        private void btn_8105_Click(object sender, EventArgs e)
        {
            ReadDataCmdSend(new DataItem_8105());
        }

        private void btn_8106_Click(object sender, EventArgs e)
        {
            //ReadDataCmdSend(new DataItem_8106());
        }

        private void btn_810a_Click(object sender, EventArgs e)
        {
            ReadDataCmdSend(new DataItem_810A());
        }

        /// <summary>
        /// 窗体初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormTool_Load(object sender, EventArgs e)
        {
            this.txt_time.Text = DateTime.Now.ToString();
            this.txt_time.Enabled = false;


            IList<Info> infoList = new List<Info>();

            infoList.Add(new Info() { Id = "0", Name = "月周期" });
            infoList.Add(new Info() { Id = "1", Name = "日周期" });
            infoList.Add(new Info() { Id = "2", Name = "时周期" });
            infoList.Add(new Info() { Id = "3", Name = "周期采集" });



            this.combox_SBtype.DataSource = infoList;
            this.combox_SBtype.ValueMember = "Id";
            this.combox_SBtype.DisplayMember = "Name";
            this.combox_SBtype.SelectedValue = "1";




            IList<Info> infoList1 = new List<Info>();

            infoList1.Add(new Info() { Id = "0", Name = "手机号" });
            infoList1.Add(new Info() { Id = "1", Name = "IP地址" });
            infoList1.Add(new Info() { Id = "2", Name = "域名地址" });


            this.combox_fwq_type.DataSource = infoList1;
            this.combox_fwq_type.ValueMember = "Id";
            this.combox_fwq_type.DisplayMember = "Name";
            this.combox_fwq_type.SelectedValue = "1";





            IList<Info> infoList2 = new List<Info>();

            infoList2.Add(new Info() { Id = "0", Name = "月" });
            infoList2.Add(new Info() { Id = "1", Name = "季度" });
            infoList2.Add(new Info() { Id = "10", Name = "半年度" });
            infoList2.Add(new Info() { Id = "11", Name = "全年度" });


            this.combox_price_zq.DataSource = infoList2;
            this.combox_price_zq.ValueMember = "Id";
            this.combox_price_zq.DisplayMember = "Name";
            this.combox_price_zq.SelectedValue = "0";




            IList<Info> infoList3 = new List<Info>();

            infoList3.Add(new Info() { Id = "0", Name = "燃气表" });
            infoList3.Add(new Info() { Id = "1", Name = "金额表" });

            this.combox_meterType.DataSource = infoList3;
            this.combox_meterType.ValueMember = "Id";
            this.combox_meterType.DisplayMember = "Name";
            this.combox_meterType.SelectedValue = "0";






            IList<Info> infoList4 = new List<Info>();

            infoList4.Add(new Info() { Id = "1", Name = "1" });
            infoList4.Add(new Info() { Id = "2", Name = "2" });
            infoList4.Add(new Info() { Id = "3", Name = "3" });
            infoList4.Add(new Info() { Id = "4", Name = "4" });
            infoList4.Add(new Info() { Id = "5", Name = "5" });

            this.combox_priceNum.DataSource = infoList4;
            this.combox_priceNum.ValueMember = "Id";
            this.combox_priceNum.DisplayMember = "Name";
            this.combox_priceNum.SelectedValue = "1";





        }


        //写价格表 = 0xA010,
        //写结算日 = 0xA011,
        //写抄表日 = 0xA012,
        //写购入金额 = 0xA013,
        //写新密钥 = 0xA014,
        //写标准时间 = 0xA015,
        //写阀门控制 = 0xA017,
        //出厂启用 = 0xA019,
        //写地址 = 0xA018,
        //写表底数 = 0xA016,




        public void SetTextDo(string text)
        {
            listBox1.Items.Add(text);
        }



        private void WriteDataCmdSend(DataItem item)
        {
            TaskArge data = new TaskArge(meterId, item, ControlCode.WriteData);
            string str = "发送:" + meter.SendData(data);

            SetTextDo(str);

        }



        private void btn_A011_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.txt_jiesuan.Text))
            {
                MessageBox.Show("结算日不能为空"); return;
            }


            int jiesuan = int.Parse(this.txt_jiesuan.Text.Trim());

            //DataItem item = new DataItem_A011(jiesuan);

            //WriteDataCmdSend(item);


        }

        private void btn_A012_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_chaobiao.Text)) 
            {
                MessageBox.Show("抄表日不能为空"); return;
            }

            int chaobiao = int.Parse(this.txt_chaobiao.Text.Trim());

            DataItem item = new DataItem_A012(chaobiao);

            WriteDataCmdSend(item);


        }

        private void btn_A013_Click(object sender, EventArgs e)
        {



            if (string.IsNullOrEmpty(this.txt_buynum.Text))
            {
                MessageBox.Show("序号不能为空"); return;
            }

            if (string.IsNullOrEmpty(this.txt_buymoney.Text))
            {
                MessageBox.Show("金额不能为空"); return;
            }


            int buynum = int.Parse(this.txt_buynum.Text.Trim());
            decimal buymoney = decimal.Parse(this.txt_buymoney.Text.Trim());

            //DataItem item = new DataItem_A013(buynum, buymoney);

            //WriteDataCmdSend(item);

        }

        private void btn_A015_Click(object sender, EventArgs e)
        {

            //DataItem item = new DataItem_A015(DateTime.Now);
            //WriteDataCmdSend(item);

        }

        private void btn_A017_Click(object sender, EventArgs e)
        {


            int kaiguanfa = this.radioButton_K.Checked ? 1 : 0;

            //DataItem item = new DataItem_A017(kaiguanfa);

            //WriteDataCmdSend(item);

        }

        private void btn_A016_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.txt_biaodi.Text))
            {
                MessageBox.Show("表底不能为空"); return;
            }


            int meterNum = int.Parse(this.txt_biaodi.Text.Trim());

            //DataItem item = new DataItem_A016(meterNum);

            //WriteDataCmdSend(item);
        }

        private void btn_A018_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.txt_dizhi.Text))
            {
                MessageBox.Show("地址不能为空"); return;
            }


            string adress = this.txt_dizhi.Text.Trim();

            //DataItem item = new DataItem_A018(adress);

            //WriteDataCmdSend(item);
        }

        private void btn_A014_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.txt_keyver.Text))
            {
                MessageBox.Show("密钥版本不能为空"); return;
            }

            if (string.IsNullOrEmpty(this.txt_key.Text))
            {
                MessageBox.Show("密钥不能为空"); return;
            }

           

            int keyver = int.Parse(this.txt_keyver.Text.Trim());
            string key = this.txt_key.Text.Trim().PadRight(16,'0');

            //DataItem item = new DataItem_A014(keyver, key);

            //WriteDataCmdSend(item);
        }

        private void btn_A019_Click(object sender, EventArgs e)
        {
            //DataItem item = new DataItem_A019();

            //WriteDataCmdSend(item);

        }




        private void btn_A010_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrEmpty(this.txt_price1.Text)||
                string.IsNullOrEmpty(this.txt_price2.Text)||
                string.IsNullOrEmpty(this.txt_price3.Text)||
                string.IsNullOrEmpty(this.txt_price4.Text)||
                string.IsNullOrEmpty(this.txt_price5.Text)||
                  string.IsNullOrEmpty(this.txt_gas1.Text)||
                  string.IsNullOrEmpty(this.txt_gas2.Text)||
                  string.IsNullOrEmpty(this.txt_gas3.Text)||
                  string.IsNullOrEmpty(this.txt_gas4.Text)||
                  string.IsNullOrEmpty(this.txt_startDate.Text)
                
                )
            {

                MessageBox.Show("价格参数不能为空"); return;
            
            
            }





            MeterType type = (MeterType)Convert.ToInt32(this.combox_meterType.SelectedValue);

            JieSuanType jiesuanType = (JieSuanType)Convert.ToInt32(this.combox_price_zq.SelectedValue);

            int priceNum = Convert.ToInt32(this.combox_priceNum.SelectedValue);

            CT ct = new CT(type, this.checkBox_isladder.Checked, jiesuanType, priceNum);


            decimal price1 = decimal.Parse(this.txt_price1.Text);
            decimal price2 = decimal.Parse(this.txt_price2.Text);
            decimal price3 = decimal.Parse(this.txt_price3.Text);
            decimal price4 = decimal.Parse(this.txt_price4.Text);
            decimal price5 = decimal.Parse(this.txt_price5.Text);


            int gas1 = int.Parse(this.txt_gas1.Text);
            int gas2 = int.Parse(this.txt_gas2.Text);
            int gas3 = int.Parse(this.txt_gas3.Text);
            int gas4 = int.Parse(this.txt_gas4.Text);

            int startDate = int.Parse(this.txt_startDate.Text);


            //DataItem item = new DataItem_A010(ct, price1, gas1, price2, gas2, price3, gas3, price4, gas4, price5, startDate);

      

            //WriteDataCmdSend(item);



        }






        private void timer1_Tick(object sender, EventArgs e)
        {
            this.txt_time.Text = DateTime.Now.ToString();
        }




        //读时钟 = 0xC200,
        //读切断报警参数 = 0xC201,
        //读服务器信息 = 0xC202,
        //读上传周期 = 0xC203,
        //读公称流量 = 0xC204,



        private void CYReadDataCmdSend(DataItem item)
        {
            TaskArge data = new TaskArge(meterId, item, ControlCode.CYReadData);
            string str = "发送:" + meter.SendData(data);

            SetTextDo(str);

        }





        private void btn_c200_Click(object sender, EventArgs e)
        {
            CYReadDataCmdSend(new DataItem_C200_Answer());
        }

        private void btn_c201_Click(object sender, EventArgs e)
        {
            CYReadDataCmdSend(new DataItem_C201_Answer());
        }

        private void btn_c202_Click(object sender, EventArgs e)
        {
            CYReadDataCmdSend(new DataItem_C202_Answer());
        }

        private void btn_c203_Click(object sender, EventArgs e)
        {
            CYReadDataCmdSend(new DataItem_C203_Answer());
        }

        private void btn_c204_Click(object sender, EventArgs e)
        {
            CYReadDataCmdSend(new DataItem_C204_Anwser());
        }


        //设置公称流量 = 0xC101,
        //修正表数据 = 0xC102,
        //设置切断报警参数 = 0xC103,
        //设置服务器信息 = 0xC104,
        //设置上传周期 = 0xC105,
        //恢复初始设置 = 0xC106



        private void CYWriteDataCmdSend(DataItem item)
        {
            TaskArge data = new TaskArge(meterId, item, ControlCode.CYWriteData);
            string str = "发送:" + meter.SendData(data);

            SetTextDo(str);

        }





        private void btn_c101_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.txt_gcliuliang.Text))
            {
                MessageBox.Show("公称流量不能为空"); return;
            }


            int gcliuliang = int.Parse(this.txt_gcliuliang.Text.Trim());

            DataItem item = new DataItem_C101(gcliuliang);

            CYWriteDataCmdSend(item);

        }


        private void btn_c104_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.txt_fwq_ip.Text)
                || string.IsNullOrEmpty(this.txt_fwq_phone.Text)
                || string.IsNullOrEmpty(this.txt_fwq_port.Text)
                || string.IsNullOrEmpty(this.txt_fwq_apn.Text)
                || string.IsNullOrEmpty(this.txt_fwq_apnname.Text)
                   || string.IsNullOrEmpty(this.txt_fwq_apnkey.Text)
               
                )
            {
                MessageBox.Show("服务器参数不能为空"); return;
            }



            ServerParamType type = (ServerParamType)Convert.ToInt32(this.combox_fwq_type.SelectedValue);


            string gprs_adress = this.txt_fwq_ip.Text;

            string gprs_port =this.txt_fwq_port.Text;

            string gsm =this.txt_fwq_phone.Text;

            string apn = this.txt_fwq_apn.Text;

            string apn_name = this.txt_fwq_apnname.Text;

            string apn_key = this.txt_fwq_apnkey.Text;

            DataItem item = new DataItem_C104(type, gprs_adress, gprs_port, gsm, apn, apn_name, apn_key);

            CYWriteDataCmdSend(item);


        }

        private void btn_c106_Click(object sender, EventArgs e)
        {

            CYWriteDataCmdSend(new DataItem_C106());

        }

        private void btn_c105_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrEmpty(this.txt_xx.Text) || string.IsNullOrEmpty(this.txt_yy.Text) || string.IsNullOrEmpty(this.txt_zz.Text))
            {
                MessageBox.Show("上传周期不能为空"); return;
            }

            ReportCycleType type = (ReportCycleType)(Convert.ToInt32(this.combox_SBtype.SelectedValue));

            int xx = int.Parse(this.txt_xx.Text.Trim());
            int yy = int.Parse(this.txt_yy.Text.Trim());
            int zz = int.Parse(this.txt_zz.Text.Trim());


            DataItem item = new DataItem_C105(00,type,xx,yy,zz);

            CYWriteDataCmdSend(item);
        }


    }
}

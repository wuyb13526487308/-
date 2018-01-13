using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using CY.IotM.Common;
using CY.IotM.Common.Tool;
using CY.IoTM.Common.Business;


using CY.IoTM.MongoDataHelper;
using MongoDB.Driver;


namespace CY.IoTM.FormTool
{
    public partial class MeterTest : Form
    {
        public MeterTest()
        {
            InitializeComponent();

            bindCbox();
        }

        List<String> userIdList;
        List<String> meterIdList;

        WCFServiceProxy<IDianHuo> dianHuoService = new WCFServiceProxy<IDianHuo>();
        WCFServiceProxy<IUserManage> userService = new WCFServiceProxy<IUserManage>();
        WCFServiceProxy<IMeterManage> meterService = new WCFServiceProxy<IMeterManage>();


        /// <summary>
        /// 批量报装
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {

                //起始表号
                long meterNoStart = Convert.ToInt64(this.textBox1.Text);

                //表数目
                int meterNum = Convert.ToInt32(this.textBox2.Text);

                userIdList = new List<string>();
                meterIdList = new List<string>();

                for (int i = 0; i < meterNum; i++)
                {

                    //创建测试用户
                    IoT_User user = new IoT_User()
                    {
                        Address = "测试用户" + i + "的地址",
                        Community = "1",
                        CompanyID = "zzcy",
                        Door = "测试门牌号",
                        Phone = "xxxxxxxxxxxx",
                        State = '1',
                        Street = "1",
                        UserName = "测试用户" + i
                    };
                    //创建测试表具
                    IoT_Meter meter = new IoT_Meter()
                    {
                        CompanyID = "zzcy",
                        MeterNo = (meterNoStart + i).ToString(),
                        MeterType="01"

                    };
                    CY.IotM.Common.Message m= userService.getChannel.BatchAddUserMeter(user, meter);
                    if (!m.Result) {
                        MessageBox.Show(m.TxtMessage);
                        return;
                    }
                    userIdList.Add(user.UserID);
                    meterIdList.Add(meter.MeterNo);

                }
                MessageBox.Show("批量创建测试用户完成");
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {

                //价格Id
                long priceId = Convert.ToInt64(comboBox1.SelectedValue);

                 CY.IotM.Common.Message m = dianHuoService.getChannel.DianHuo(meterIdList, priceId, "zzcy", DateTime.Now, userIdList, "测试操作员");

                 if (m.Result)
                 {
                     MessageBox.Show("点火成功");
                 }
                 else {
                     MessageBox.Show(m.TxtMessage);
                 }
            
            }
            catch(Exception ex){
            
            
                MessageBox.Show(ex.Message);
            }
         
        }

        List<MSimulator> simulatorList;

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                simulatorList = new List<MSimulator>();

                MSimulator _simulator = null;

                string hostName = System.Configuration.ConfigurationManager.AppSettings["ServerHost"];
                int port = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ServerPort"]);


                if (meterIdList == null)
                {

                    meterIdList = new List<string>();
                    //起始表号
                    long meterNoStart = Convert.ToInt64(this.textBox1.Text);

                    //表数目
                    int meterNum = Convert.ToInt32(this.textBox2.Text);

                    for (int i = 0; i < meterNum; i++)
                    {
                        meterIdList.Add((meterNoStart + i).ToString());
                    }

                }

                foreach (string meterNo in meterIdList)
                {

                    _simulator = Query(meterNo);
                    if (_simulator == null)
                    {
                        _simulator = new MSimulator();
                        _simulator.hostname = hostName;
                        _simulator.port = port;
                        _simulator.周期 = Convert.ToInt32(this.textBox3.Text);
                        _simulator.Mac = meterNo;
                        _simulator.Key = "88888888888888";
                        _simulator.MeterType = "00";
                        _simulator.MKeyVer = 0;
                        _simulator.PriceCheck = "0";
                        _simulator.SettlementDay = 28;
                        _simulator.SettlementType = "00";
                        _simulator.TotalAmount = 0;
                        _simulator.TotalTopUp = 0;
                        _simulator.ValveState = "0";
                        _simulator.SettlementDateTime = "2015-01-01";

                        _simulator.SetNextSettlementDateTime();
                        _simulator.Insert();
                    }

                    _simulator.hourLiuLiang = Convert.ToDecimal(this.textBox4.Text);
                    _simulator.hostname = hostName;
                    _simulator.port = port;

                   
                    _simulator.Start();
                    simulatorList.Add(_simulator);
                }

              
                MessageBox.Show("设备上线成功");
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

        }

     


        private string TaskCollectionName = "MSimulator";

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




        private void button4_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = GetMeterList();


            this.dataGridView1.Columns["ID"].Visible = false;

            this.dataGridView1.Columns["MeterNo"].HeaderText = "表号";
            this.dataGridView1.Columns["MeterNo"].DisplayIndex = 0;

            this.dataGridView1.Columns["MeterType"].HeaderText = "表类型";
            this.dataGridView1.Columns["MeterType"].DisplayIndex = 1;

            this.dataGridView1.Columns["MeterState"].HeaderText = "表状态";
            this.dataGridView1.Columns["MeterState"].DisplayIndex = 2;

           

        }




        public List<IoT_Meter> GetMeterList() 
        {

            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
         
            var list = dd.GetTable<IoT_Meter>().Where(p => p.CompanyID == "zzcy").ToList();
            return list;

        }


        public List<IoT_PricePar> GetPriceList()
        {

            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);

            var list = dd.GetTable<IoT_PricePar>().Where(p => p.CompanyID == "zzcy").ToList();
            return list;

        }


        private void bindCbox()
        {

            comboBox1.DataSource = GetPriceList();
            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "PriceName";
        }





    }
}

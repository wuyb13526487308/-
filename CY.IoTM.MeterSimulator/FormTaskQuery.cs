using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CY.IoTM.MongoDataHelper;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CY.IoTM.MeterSimulator
{
    public partial class FormTaskQuery : Form
    {
        public FormTaskQuery()
        {
            InitializeComponent();
            this.comboBox1.SelectedIndex = 0;

        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            MongoDBHelper<CY.IoTM.Common.Business.Task> mongo = new MongoDBHelper<CY.IoTM.Common.Business.Task>();
            List<CY.IoTM.Common.Business.Task> list = new List<CY.IoTM.Common.Business.Task>();

            QueryDocument query = new QueryDocument();
            if (this.comboBox1.Text != "全部")
            {
                if(this.comboBox1.Text == "等待")
                    query.Add("TaskState", 0);
                else if(this.comboBox1.Text == "撤销")
                    query.Add("TaskState", 1);
                else if (this.comboBox1.Text == "已完成")
                    query.Add("TaskState", 2);
                else if (this.comboBox1.Text == "失败")
                    query.Add("TaskState", 3);
            }
            MongoCursor<CY.IoTM.Common.Business.Task> mongoCursor = mongo.Query("Task", query);
            if (mongoCursor != null)
            {
                var dataList = mongoCursor.ToList();

                this.bindingSource1.DataSource = dataList;
            }

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            string taskID = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString ();

            MongoDBHelper<CY.IoTM.Common.Business.Command> mongo = new MongoDBHelper<CY.IoTM.Common.Business.Command>();
            QueryDocument query = new QueryDocument();
            query.Add("TaskID", taskID);

            var list = mongo.Query("Command", query);
            this.bindingSource2.DataSource = list;

        }
    }
}

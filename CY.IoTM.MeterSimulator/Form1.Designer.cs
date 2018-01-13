namespace CY.IoTM.MeterSimulator
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_server = new System.Windows.Forms.TextBox();
            this.txt_port = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch表号 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch网络状态 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch表类型 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch阀门 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch表底 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch上传周期 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch流量 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch当前阶梯 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch当前价格 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch下个阶梯点 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch结算日 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch剩余金额 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch启用阶梯 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch阶梯数 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch阶梯信息 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch信息 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txt_Mac = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt周期 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txt流量 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnWindows = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.txt指定值 = new System.Windows.Forms.NumericUpDown();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.labPage = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.btnUpLoad = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txt周期)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt流量)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt指定值)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "服务器：";
            // 
            // txt_server
            // 
            this.txt_server.Location = new System.Drawing.Point(75, 9);
            this.txt_server.Name = "txt_server";
            this.txt_server.Size = new System.Drawing.Size(162, 21);
            this.txt_server.TabIndex = 1;
            this.txt_server.Text = "192.168.1.25";
            // 
            // txt_port
            // 
            this.txt_port.Location = new System.Drawing.Point(302, 12);
            this.txt_port.Name = "txt_port";
            this.txt_port.Size = new System.Drawing.Size(78, 21);
            this.txt_port.TabIndex = 1;
            this.txt_port.Text = "8802";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(243, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "端口：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 52);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 26);
            this.button1.TabIndex = 4;
            this.button1.Text = "添加表";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(794, 52);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(79, 26);
            this.button2.TabIndex = 4;
            this.button2.Text = "单表测试";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(794, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(84, 35);
            this.button3.TabIndex = 5;
            this.button3.Text = "界面层功能";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.ch表号,
            this.ch网络状态,
            this.ch表类型,
            this.ch阀门,
            this.ch表底,
            this.ch上传周期,
            this.ch流量,
            this.ch当前阶梯,
            this.ch当前价格,
            this.ch下个阶梯点,
            this.ch结算日,
            this.ch剩余金额,
            this.ch启用阶梯,
            this.ch阶梯数,
            this.ch阶梯信息,
            this.ch信息});
            this.listView1.FullRowSelect = true;
            this.listView1.HotTracking = true;
            this.listView1.HoverSelection = true;
            this.listView1.Location = new System.Drawing.Point(17, 84);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(861, 408);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.Click += new System.EventHandler(this.listView1_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "MID";
            this.columnHeader1.Width = 40;
            // 
            // ch表号
            // 
            this.ch表号.Text = "表号";
            this.ch表号.Width = 111;
            // 
            // ch网络状态
            // 
            this.ch网络状态.Text = "网络状态";
            // 
            // ch表类型
            // 
            this.ch表类型.Text = "表类型";
            // 
            // ch阀门
            // 
            this.ch阀门.Text = "阀门";
            this.ch阀门.Width = 40;
            // 
            // ch表底
            // 
            this.ch表底.Text = "表底";
            this.ch表底.Width = 73;
            // 
            // ch上传周期
            // 
            this.ch上传周期.Text = "上传周期(分钟)";
            this.ch上传周期.Width = 98;
            // 
            // ch流量
            // 
            this.ch流量.Text = "流量";
            this.ch流量.Width = 46;
            // 
            // ch当前阶梯
            // 
            this.ch当前阶梯.Text = "当前阶梯";
            // 
            // ch当前价格
            // 
            this.ch当前价格.Text = "当前价格";
            this.ch当前价格.Width = 64;
            // 
            // ch下个阶梯点
            // 
            this.ch下个阶梯点.Text = "下个阶梯点";
            this.ch下个阶梯点.Width = 72;
            // 
            // ch结算日
            // 
            this.ch结算日.Text = "结算日";
            this.ch结算日.Width = 80;
            // 
            // ch剩余金额
            // 
            this.ch剩余金额.Text = "剩余金额";
            // 
            // ch启用阶梯
            // 
            this.ch启用阶梯.Text = "阶梯价";
            this.ch启用阶梯.Width = 50;
            // 
            // ch阶梯数
            // 
            this.ch阶梯数.Text = "阶梯";
            this.ch阶梯数.Width = 40;
            // 
            // ch阶梯信息
            // 
            this.ch阶梯信息.Text = "阶梯信息";
            this.ch阶梯信息.Width = 250;
            // 
            // ch信息
            // 
            this.ch信息.Text = "信息";
            this.ch信息.Width = 300;
            // 
            // txt_Mac
            // 
            this.txt_Mac.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txt_Mac.Location = new System.Drawing.Point(53, 506);
            this.txt_Mac.MaxLength = 14;
            this.txt_Mac.Name = "txt_Mac";
            this.txt_Mac.ReadOnly = true;
            this.txt_Mac.Size = new System.Drawing.Size(94, 21);
            this.txt_Mac.TabIndex = 8;
            this.txt_Mac.Text = "15413526487308";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 510);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "表号：";
            // 
            // txt周期
            // 
            this.txt周期.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txt周期.Location = new System.Drawing.Point(264, 506);
            this.txt周期.Name = "txt周期";
            this.txt周期.Size = new System.Drawing.Size(67, 21);
            this.txt周期.TabIndex = 15;
            this.txt周期.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.txt周期.ValueChanged += new System.EventHandler(this.txt周期_ValueChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(160, 510);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "上报周期(分钟)：";
            // 
            // txt流量
            // 
            this.txt流量.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txt流量.Location = new System.Drawing.Point(408, 506);
            this.txt流量.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txt流量.Name = "txt流量";
            this.txt流量.Size = new System.Drawing.Size(65, 21);
            this.txt流量.TabIndex = 17;
            this.txt流量.ValueChanged += new System.EventHandler(this.txt流量_ValueChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(340, 510);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "小时流量：";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(106, 52);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(79, 26);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "移除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnWindows
            // 
            this.btnWindows.Location = new System.Drawing.Point(205, 52);
            this.btnWindows.Name = "btnWindows";
            this.btnWindows.Size = new System.Drawing.Size(79, 26);
            this.btnWindows.TabIndex = 4;
            this.btnWindows.Text = "窗口查看";
            this.btnWindows.UseVisualStyleBackColor = true;
            this.btnWindows.Click += new System.EventHandler(this.btnWindows_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(302, 52);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(110, 26);
            this.button4.TabIndex = 18;
            this.button4.Text = "设为随机流量";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button5.Location = new System.Drawing.Point(490, 502);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(79, 26);
            this.button5.TabIndex = 19;
            this.button5.Text = "设为指定值";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // txt指定值
            // 
            this.txt指定值.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txt指定值.Location = new System.Drawing.Point(575, 505);
            this.txt指定值.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txt指定值.Name = "txt指定值";
            this.txt指定值.Size = new System.Drawing.Size(65, 21);
            this.txt指定值.TabIndex = 17;
            this.txt指定值.ValueChanged += new System.EventHandler(this.txt流量_ValueChanged);
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(CY.IoTM.MeterSimulator.MSimulator);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(408, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "数据分页：";
            // 
            // labPage
            // 
            this.labPage.AutoSize = true;
            this.labPage.Location = new System.Drawing.Point(479, 15);
            this.labPage.Name = "labPage";
            this.labPage.Size = new System.Drawing.Size(23, 12);
            this.labPage.TabIndex = 21;
            this.labPage.Text = "1/1";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(569, 10);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(71, 21);
            this.numericUpDown1.TabIndex = 22;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(646, 10);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(43, 23);
            this.button6.TabIndex = 23;
            this.button6.Text = ">";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(516, 9);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(47, 23);
            this.button7.TabIndex = 24;
            this.button7.Text = "<";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // btnUpLoad
            // 
            this.btnUpLoad.Location = new System.Drawing.Point(708, 7);
            this.btnUpLoad.Name = "btnUpLoad";
            this.btnUpLoad.Size = new System.Drawing.Size(80, 33);
            this.btnUpLoad.TabIndex = 25;
            this.btnUpLoad.Text = "UpLoad";
            this.btnUpLoad.UseVisualStyleBackColor = true;
            this.btnUpLoad.Click += new System.EventHandler(this.btnUpLoad_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 538);
            this.Controls.Add(this.btnUpLoad);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.labPage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.txt指定值);
            this.Controls.Add(this.txt流量);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt周期);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_Mac);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnWindows);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_port);
            this.Controls.Add(this.txt_server);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txt周期)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt流量)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt指定值)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_server;
        private System.Windows.Forms.TextBox txt_port;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader ch表号;
        private System.Windows.Forms.ColumnHeader ch表类型;
        private System.Windows.Forms.ColumnHeader ch表底;
        private System.Windows.Forms.ColumnHeader ch上传周期;
        private System.Windows.Forms.ColumnHeader ch流量;
        private System.Windows.Forms.ColumnHeader ch当前阶梯;
        private System.Windows.Forms.ColumnHeader ch当前价格;
        private System.Windows.Forms.ColumnHeader ch网络状态;
        private System.Windows.Forms.ColumnHeader ch下个阶梯点;
        private System.Windows.Forms.ColumnHeader ch结算日;
        private System.Windows.Forms.ColumnHeader ch剩余金额;
        private System.Windows.Forms.ColumnHeader ch启用阶梯;
        private System.Windows.Forms.ColumnHeader ch阶梯信息;
        private System.Windows.Forms.ColumnHeader ch阶梯数;
        private System.Windows.Forms.ColumnHeader ch信息;
        private System.Windows.Forms.TextBox txt_Mac;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown txt周期;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown txt流量;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnWindows;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.NumericUpDown txt指定值;
        private System.Windows.Forms.ColumnHeader ch阀门;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labPage;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button btnUpLoad;
    }
}


namespace CY.IoTM.MeterSimulator
{
    partial class FormMeterManage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_meterNo = new System.Windows.Forms.TextBox();
            this.btn_query = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnDH = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.meterNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.meterTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.companyIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalAmountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalTopUpDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.directionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.installDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gas1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gas2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gas3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price4DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gas4DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price5DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isUsedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ladderDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.settlementTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.settlementDayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valveStateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.meterStateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.readDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remainingAmountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastTotalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceCheckDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mKeyVerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mKeyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enableMeterDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enableMeterOperDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "表号：";
            // 
            // txt_meterNo
            // 
            this.txt_meterNo.Location = new System.Drawing.Point(83, 24);
            this.txt_meterNo.Name = "txt_meterNo";
            this.txt_meterNo.Size = new System.Drawing.Size(100, 21);
            this.txt_meterNo.TabIndex = 1;
            // 
            // btn_query
            // 
            this.btn_query.Location = new System.Drawing.Point(202, 22);
            this.btn_query.Name = "btn_query";
            this.btn_query.Size = new System.Drawing.Size(70, 23);
            this.btn_query.TabIndex = 2;
            this.btn_query.Text = "查询";
            this.btn_query.UseVisualStyleBackColor = true;
            this.btn_query.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.meterNoDataGridViewTextBoxColumn,
            this.meterTypeDataGridViewTextBoxColumn,
            this.companyIDDataGridViewTextBoxColumn,
            this.userIDDataGridViewTextBoxColumn,
            this.totalAmountDataGridViewTextBoxColumn,
            this.totalTopUpDataGridViewTextBoxColumn,
            this.directionDataGridViewTextBoxColumn,
            this.installDateDataGridViewTextBoxColumn,
            this.price1DataGridViewTextBoxColumn,
            this.gas1DataGridViewTextBoxColumn,
            this.price2DataGridViewTextBoxColumn,
            this.gas2DataGridViewTextBoxColumn,
            this.price3DataGridViewTextBoxColumn,
            this.gas3DataGridViewTextBoxColumn,
            this.price4DataGridViewTextBoxColumn,
            this.gas4DataGridViewTextBoxColumn,
            this.price5DataGridViewTextBoxColumn,
            this.isUsedDataGridViewTextBoxColumn,
            this.ladderDataGridViewTextBoxColumn,
            this.settlementTypeDataGridViewTextBoxColumn,
            this.settlementDayDataGridViewTextBoxColumn,
            this.valveStateDataGridViewTextBoxColumn,
            this.meterStateDataGridViewTextBoxColumn,
            this.readDateDataGridViewTextBoxColumn,
            this.remainingAmountDataGridViewTextBoxColumn,
            this.lastTotalDataGridViewTextBoxColumn,
            this.priceCheckDataGridViewTextBoxColumn,
            this.mKeyVerDataGridViewTextBoxColumn,
            this.mKeyDataGridViewTextBoxColumn,
            this.enableMeterDateDataGridViewTextBoxColumn,
            this.enableMeterOperDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.bindingSource1;
            this.dataGridView1.Location = new System.Drawing.Point(12, 61);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(673, 308);
            this.dataGridView1.TabIndex = 4;
            // 
            // btnDH
            // 
            this.btnDH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDH.Location = new System.Drawing.Point(12, 375);
            this.btnDH.Name = "btnDH";
            this.btnDH.Size = new System.Drawing.Size(75, 40);
            this.btnDH.TabIndex = 5;
            this.btnDH.Text = "点火";
            this.btnDH.UseVisualStyleBackColor = true;
            this.btnDH.Click += new System.EventHandler(this.btnDH_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button4.Location = new System.Drawing.Point(108, 375);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 40);
            this.button4.TabIndex = 5;
            this.button4.Text = "换表";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button5.Location = new System.Drawing.Point(202, 375);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 40);
            this.button5.TabIndex = 5;
            this.button5.Text = "关阀";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(283, 375);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 40);
            this.button2.TabIndex = 5;
            this.button2.Text = "开阀";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button6.Location = new System.Drawing.Point(364, 375);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 40);
            this.button6.TabIndex = 5;
            this.button6.Text = "充值";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button7.Location = new System.Drawing.Point(445, 375);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 40);
            this.button7.TabIndex = 5;
            this.button7.Text = "调价";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(585, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "查询后台任务";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            // 
            // meterNoDataGridViewTextBoxColumn
            // 
            this.meterNoDataGridViewTextBoxColumn.DataPropertyName = "MeterNo";
            this.meterNoDataGridViewTextBoxColumn.HeaderText = "MeterNo";
            this.meterNoDataGridViewTextBoxColumn.Name = "meterNoDataGridViewTextBoxColumn";
            // 
            // meterTypeDataGridViewTextBoxColumn
            // 
            this.meterTypeDataGridViewTextBoxColumn.DataPropertyName = "MeterType";
            this.meterTypeDataGridViewTextBoxColumn.HeaderText = "MeterType";
            this.meterTypeDataGridViewTextBoxColumn.Name = "meterTypeDataGridViewTextBoxColumn";
            // 
            // companyIDDataGridViewTextBoxColumn
            // 
            this.companyIDDataGridViewTextBoxColumn.DataPropertyName = "CompanyID";
            this.companyIDDataGridViewTextBoxColumn.HeaderText = "CompanyID";
            this.companyIDDataGridViewTextBoxColumn.Name = "companyIDDataGridViewTextBoxColumn";
            // 
            // userIDDataGridViewTextBoxColumn
            // 
            this.userIDDataGridViewTextBoxColumn.DataPropertyName = "UserID";
            this.userIDDataGridViewTextBoxColumn.HeaderText = "UserID";
            this.userIDDataGridViewTextBoxColumn.Name = "userIDDataGridViewTextBoxColumn";
            // 
            // totalAmountDataGridViewTextBoxColumn
            // 
            this.totalAmountDataGridViewTextBoxColumn.DataPropertyName = "TotalAmount";
            this.totalAmountDataGridViewTextBoxColumn.HeaderText = "TotalAmount";
            this.totalAmountDataGridViewTextBoxColumn.Name = "totalAmountDataGridViewTextBoxColumn";
            // 
            // totalTopUpDataGridViewTextBoxColumn
            // 
            this.totalTopUpDataGridViewTextBoxColumn.DataPropertyName = "TotalTopUp";
            this.totalTopUpDataGridViewTextBoxColumn.HeaderText = "TotalTopUp";
            this.totalTopUpDataGridViewTextBoxColumn.Name = "totalTopUpDataGridViewTextBoxColumn";
            // 
            // directionDataGridViewTextBoxColumn
            // 
            this.directionDataGridViewTextBoxColumn.DataPropertyName = "Direction";
            this.directionDataGridViewTextBoxColumn.HeaderText = "Direction";
            this.directionDataGridViewTextBoxColumn.Name = "directionDataGridViewTextBoxColumn";
            // 
            // installDateDataGridViewTextBoxColumn
            // 
            this.installDateDataGridViewTextBoxColumn.DataPropertyName = "InstallDate";
            this.installDateDataGridViewTextBoxColumn.HeaderText = "InstallDate";
            this.installDateDataGridViewTextBoxColumn.Name = "installDateDataGridViewTextBoxColumn";
            // 
            // price1DataGridViewTextBoxColumn
            // 
            this.price1DataGridViewTextBoxColumn.DataPropertyName = "Price1";
            this.price1DataGridViewTextBoxColumn.HeaderText = "Price1";
            this.price1DataGridViewTextBoxColumn.Name = "price1DataGridViewTextBoxColumn";
            // 
            // gas1DataGridViewTextBoxColumn
            // 
            this.gas1DataGridViewTextBoxColumn.DataPropertyName = "Gas1";
            this.gas1DataGridViewTextBoxColumn.HeaderText = "Gas1";
            this.gas1DataGridViewTextBoxColumn.Name = "gas1DataGridViewTextBoxColumn";
            // 
            // price2DataGridViewTextBoxColumn
            // 
            this.price2DataGridViewTextBoxColumn.DataPropertyName = "Price2";
            this.price2DataGridViewTextBoxColumn.HeaderText = "Price2";
            this.price2DataGridViewTextBoxColumn.Name = "price2DataGridViewTextBoxColumn";
            // 
            // gas2DataGridViewTextBoxColumn
            // 
            this.gas2DataGridViewTextBoxColumn.DataPropertyName = "Gas2";
            this.gas2DataGridViewTextBoxColumn.HeaderText = "Gas2";
            this.gas2DataGridViewTextBoxColumn.Name = "gas2DataGridViewTextBoxColumn";
            // 
            // price3DataGridViewTextBoxColumn
            // 
            this.price3DataGridViewTextBoxColumn.DataPropertyName = "Price3";
            this.price3DataGridViewTextBoxColumn.HeaderText = "Price3";
            this.price3DataGridViewTextBoxColumn.Name = "price3DataGridViewTextBoxColumn";
            // 
            // gas3DataGridViewTextBoxColumn
            // 
            this.gas3DataGridViewTextBoxColumn.DataPropertyName = "Gas3";
            this.gas3DataGridViewTextBoxColumn.HeaderText = "Gas3";
            this.gas3DataGridViewTextBoxColumn.Name = "gas3DataGridViewTextBoxColumn";
            // 
            // price4DataGridViewTextBoxColumn
            // 
            this.price4DataGridViewTextBoxColumn.DataPropertyName = "Price4";
            this.price4DataGridViewTextBoxColumn.HeaderText = "Price4";
            this.price4DataGridViewTextBoxColumn.Name = "price4DataGridViewTextBoxColumn";
            // 
            // gas4DataGridViewTextBoxColumn
            // 
            this.gas4DataGridViewTextBoxColumn.DataPropertyName = "Gas4";
            this.gas4DataGridViewTextBoxColumn.HeaderText = "Gas4";
            this.gas4DataGridViewTextBoxColumn.Name = "gas4DataGridViewTextBoxColumn";
            // 
            // price5DataGridViewTextBoxColumn
            // 
            this.price5DataGridViewTextBoxColumn.DataPropertyName = "Price5";
            this.price5DataGridViewTextBoxColumn.HeaderText = "Price5";
            this.price5DataGridViewTextBoxColumn.Name = "price5DataGridViewTextBoxColumn";
            // 
            // isUsedDataGridViewTextBoxColumn
            // 
            this.isUsedDataGridViewTextBoxColumn.DataPropertyName = "IsUsed";
            this.isUsedDataGridViewTextBoxColumn.HeaderText = "IsUsed";
            this.isUsedDataGridViewTextBoxColumn.Name = "isUsedDataGridViewTextBoxColumn";
            // 
            // ladderDataGridViewTextBoxColumn
            // 
            this.ladderDataGridViewTextBoxColumn.DataPropertyName = "Ladder";
            this.ladderDataGridViewTextBoxColumn.HeaderText = "Ladder";
            this.ladderDataGridViewTextBoxColumn.Name = "ladderDataGridViewTextBoxColumn";
            // 
            // settlementTypeDataGridViewTextBoxColumn
            // 
            this.settlementTypeDataGridViewTextBoxColumn.DataPropertyName = "SettlementType";
            this.settlementTypeDataGridViewTextBoxColumn.HeaderText = "SettlementType";
            this.settlementTypeDataGridViewTextBoxColumn.Name = "settlementTypeDataGridViewTextBoxColumn";
            // 
            // settlementDayDataGridViewTextBoxColumn
            // 
            this.settlementDayDataGridViewTextBoxColumn.DataPropertyName = "SettlementDay";
            this.settlementDayDataGridViewTextBoxColumn.HeaderText = "SettlementDay";
            this.settlementDayDataGridViewTextBoxColumn.Name = "settlementDayDataGridViewTextBoxColumn";
            // 
            // valveStateDataGridViewTextBoxColumn
            // 
            this.valveStateDataGridViewTextBoxColumn.DataPropertyName = "ValveState";
            this.valveStateDataGridViewTextBoxColumn.HeaderText = "ValveState";
            this.valveStateDataGridViewTextBoxColumn.Name = "valveStateDataGridViewTextBoxColumn";
            // 
            // meterStateDataGridViewTextBoxColumn
            // 
            this.meterStateDataGridViewTextBoxColumn.DataPropertyName = "MeterState";
            this.meterStateDataGridViewTextBoxColumn.HeaderText = "MeterState";
            this.meterStateDataGridViewTextBoxColumn.Name = "meterStateDataGridViewTextBoxColumn";
            // 
            // readDateDataGridViewTextBoxColumn
            // 
            this.readDateDataGridViewTextBoxColumn.DataPropertyName = "ReadDate";
            this.readDateDataGridViewTextBoxColumn.HeaderText = "ReadDate";
            this.readDateDataGridViewTextBoxColumn.Name = "readDateDataGridViewTextBoxColumn";
            // 
            // remainingAmountDataGridViewTextBoxColumn
            // 
            this.remainingAmountDataGridViewTextBoxColumn.DataPropertyName = "RemainingAmount";
            this.remainingAmountDataGridViewTextBoxColumn.HeaderText = "RemainingAmount";
            this.remainingAmountDataGridViewTextBoxColumn.Name = "remainingAmountDataGridViewTextBoxColumn";
            // 
            // lastTotalDataGridViewTextBoxColumn
            // 
            this.lastTotalDataGridViewTextBoxColumn.DataPropertyName = "LastTotal";
            this.lastTotalDataGridViewTextBoxColumn.HeaderText = "LastTotal";
            this.lastTotalDataGridViewTextBoxColumn.Name = "lastTotalDataGridViewTextBoxColumn";
            // 
            // priceCheckDataGridViewTextBoxColumn
            // 
            this.priceCheckDataGridViewTextBoxColumn.DataPropertyName = "PriceCheck";
            this.priceCheckDataGridViewTextBoxColumn.HeaderText = "PriceCheck";
            this.priceCheckDataGridViewTextBoxColumn.Name = "priceCheckDataGridViewTextBoxColumn";
            // 
            // mKeyVerDataGridViewTextBoxColumn
            // 
            this.mKeyVerDataGridViewTextBoxColumn.DataPropertyName = "MKeyVer";
            this.mKeyVerDataGridViewTextBoxColumn.HeaderText = "MKeyVer";
            this.mKeyVerDataGridViewTextBoxColumn.Name = "mKeyVerDataGridViewTextBoxColumn";
            // 
            // mKeyDataGridViewTextBoxColumn
            // 
            this.mKeyDataGridViewTextBoxColumn.DataPropertyName = "MKey";
            this.mKeyDataGridViewTextBoxColumn.HeaderText = "MKey";
            this.mKeyDataGridViewTextBoxColumn.Name = "mKeyDataGridViewTextBoxColumn";
            // 
            // enableMeterDateDataGridViewTextBoxColumn
            // 
            this.enableMeterDateDataGridViewTextBoxColumn.DataPropertyName = "EnableMeterDate";
            this.enableMeterDateDataGridViewTextBoxColumn.HeaderText = "EnableMeterDate";
            this.enableMeterDateDataGridViewTextBoxColumn.Name = "enableMeterDateDataGridViewTextBoxColumn";
            // 
            // enableMeterOperDataGridViewTextBoxColumn
            // 
            this.enableMeterOperDataGridViewTextBoxColumn.DataPropertyName = "EnableMeterOper";
            this.enableMeterOperDataGridViewTextBoxColumn.HeaderText = "EnableMeterOper";
            this.enableMeterOperDataGridViewTextBoxColumn.Name = "enableMeterOperDataGridViewTextBoxColumn";
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(CY.IoTM.Common.Business.IoT_Meter);
            // 
            // FormMeterManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 433);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnDH);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_query);
            this.Controls.Add(this.txt_meterNo);
            this.Controls.Add(this.label1);
            this.Name = "FormMeterManage";
            this.Text = "对表操作";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_meterNo;
        private System.Windows.Forms.Button btn_query;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnDH;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn meterNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn meterTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn companyIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn userIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalAmountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalTopUpDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn directionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn installDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn price1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gas1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn price2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gas2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn price3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gas3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn price4DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gas4DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn price5DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn isUsedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ladderDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn settlementTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn settlementDayDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valveStateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn meterStateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn readDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn remainingAmountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastTotalDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceCheckDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mKeyVerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mKeyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn enableMeterDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn enableMeterOperDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button button1;
    }
}
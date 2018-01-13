namespace CY.IoTM.MeterSimulator
{
    partial class FormTaskQuery
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
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.controlCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.identificationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataLengthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataCommandDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskIDDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commandStateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.answerDataDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.answerDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.taskIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.meterMacDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskStateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.finishedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(571, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "刷新";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.taskIDDataGridViewTextBoxColumn,
            this.taskTypeDataGridViewTextBoxColumn,
            this.meterMacDataGridViewTextBoxColumn,
            this.taskDateDataGridViewTextBoxColumn,
            this.taskStateDataGridViewTextBoxColumn,
            this.finishedDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.bindingSource1;
            this.dataGridView1.Location = new System.Drawing.Point(12, 57);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(653, 240);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.controlCodeDataGridViewTextBoxColumn,
            this.identificationDataGridViewTextBoxColumn,
            this.dataLengthDataGridViewTextBoxColumn,
            this.dataCommandDataGridViewTextBoxColumn,
            this.taskIDDataGridViewTextBoxColumn1,
            this.commandStateDataGridViewTextBoxColumn,
            this.answerDataDataGridViewTextBoxColumn,
            this.answerDateDataGridViewTextBoxColumn,
            this.orderDataGridViewTextBoxColumn});
            this.dataGridView2.DataSource = this.bindingSource2;
            this.dataGridView2.Location = new System.Drawing.Point(12, 327);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(653, 137);
            this.dataGridView2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "任务列表：";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 309);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "指令列表";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "任务状态：";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "全部",
            "等待",
            "已完成",
            "失败"});
            this.comboBox1.Location = new System.Drawing.Point(84, 9);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 6;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(222, 7);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 28);
            this.btnQuery.TabIndex = 7;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // controlCodeDataGridViewTextBoxColumn
            // 
            this.controlCodeDataGridViewTextBoxColumn.DataPropertyName = "ControlCode";
            this.controlCodeDataGridViewTextBoxColumn.HeaderText = "ControlCode";
            this.controlCodeDataGridViewTextBoxColumn.Name = "controlCodeDataGridViewTextBoxColumn";
            this.controlCodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // identificationDataGridViewTextBoxColumn
            // 
            this.identificationDataGridViewTextBoxColumn.DataPropertyName = "Identification";
            this.identificationDataGridViewTextBoxColumn.HeaderText = "Identification";
            this.identificationDataGridViewTextBoxColumn.Name = "identificationDataGridViewTextBoxColumn";
            this.identificationDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dataLengthDataGridViewTextBoxColumn
            // 
            this.dataLengthDataGridViewTextBoxColumn.DataPropertyName = "DataLength";
            this.dataLengthDataGridViewTextBoxColumn.HeaderText = "DataLength";
            this.dataLengthDataGridViewTextBoxColumn.Name = "dataLengthDataGridViewTextBoxColumn";
            this.dataLengthDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dataCommandDataGridViewTextBoxColumn
            // 
            this.dataCommandDataGridViewTextBoxColumn.DataPropertyName = "DataCommand";
            this.dataCommandDataGridViewTextBoxColumn.HeaderText = "DataCommand";
            this.dataCommandDataGridViewTextBoxColumn.Name = "dataCommandDataGridViewTextBoxColumn";
            this.dataCommandDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // taskIDDataGridViewTextBoxColumn1
            // 
            this.taskIDDataGridViewTextBoxColumn1.DataPropertyName = "TaskID";
            this.taskIDDataGridViewTextBoxColumn1.HeaderText = "TaskID";
            this.taskIDDataGridViewTextBoxColumn1.Name = "taskIDDataGridViewTextBoxColumn1";
            this.taskIDDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // commandStateDataGridViewTextBoxColumn
            // 
            this.commandStateDataGridViewTextBoxColumn.DataPropertyName = "CommandState";
            this.commandStateDataGridViewTextBoxColumn.HeaderText = "CommandState";
            this.commandStateDataGridViewTextBoxColumn.Name = "commandStateDataGridViewTextBoxColumn";
            this.commandStateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // answerDataDataGridViewTextBoxColumn
            // 
            this.answerDataDataGridViewTextBoxColumn.DataPropertyName = "AnswerData";
            this.answerDataDataGridViewTextBoxColumn.HeaderText = "AnswerData";
            this.answerDataDataGridViewTextBoxColumn.Name = "answerDataDataGridViewTextBoxColumn";
            this.answerDataDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // answerDateDataGridViewTextBoxColumn
            // 
            this.answerDateDataGridViewTextBoxColumn.DataPropertyName = "AnswerDate";
            this.answerDateDataGridViewTextBoxColumn.HeaderText = "AnswerDate";
            this.answerDateDataGridViewTextBoxColumn.Name = "answerDateDataGridViewTextBoxColumn";
            this.answerDateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // orderDataGridViewTextBoxColumn
            // 
            this.orderDataGridViewTextBoxColumn.DataPropertyName = "Order";
            this.orderDataGridViewTextBoxColumn.HeaderText = "Order";
            this.orderDataGridViewTextBoxColumn.Name = "orderDataGridViewTextBoxColumn";
            this.orderDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bindingSource2
            // 
            this.bindingSource2.DataSource = typeof(CY.IoTM.Common.Business.Command);
            // 
            // taskIDDataGridViewTextBoxColumn
            // 
            this.taskIDDataGridViewTextBoxColumn.DataPropertyName = "TaskID";
            this.taskIDDataGridViewTextBoxColumn.HeaderText = "TaskID";
            this.taskIDDataGridViewTextBoxColumn.Name = "taskIDDataGridViewTextBoxColumn";
            this.taskIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // taskTypeDataGridViewTextBoxColumn
            // 
            this.taskTypeDataGridViewTextBoxColumn.DataPropertyName = "TaskType";
            this.taskTypeDataGridViewTextBoxColumn.HeaderText = "TaskType";
            this.taskTypeDataGridViewTextBoxColumn.Name = "taskTypeDataGridViewTextBoxColumn";
            this.taskTypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // meterMacDataGridViewTextBoxColumn
            // 
            this.meterMacDataGridViewTextBoxColumn.DataPropertyName = "MeterMac";
            this.meterMacDataGridViewTextBoxColumn.HeaderText = "MeterMac";
            this.meterMacDataGridViewTextBoxColumn.Name = "meterMacDataGridViewTextBoxColumn";
            this.meterMacDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // taskDateDataGridViewTextBoxColumn
            // 
            this.taskDateDataGridViewTextBoxColumn.DataPropertyName = "TaskDate";
            this.taskDateDataGridViewTextBoxColumn.HeaderText = "TaskDate";
            this.taskDateDataGridViewTextBoxColumn.Name = "taskDateDataGridViewTextBoxColumn";
            this.taskDateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // taskStateDataGridViewTextBoxColumn
            // 
            this.taskStateDataGridViewTextBoxColumn.DataPropertyName = "TaskState";
            this.taskStateDataGridViewTextBoxColumn.HeaderText = "TaskState";
            this.taskStateDataGridViewTextBoxColumn.Name = "taskStateDataGridViewTextBoxColumn";
            this.taskStateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // finishedDataGridViewTextBoxColumn
            // 
            this.finishedDataGridViewTextBoxColumn.DataPropertyName = "Finished";
            this.finishedDataGridViewTextBoxColumn.HeaderText = "Finished";
            this.finishedDataGridViewTextBoxColumn.Name = "finishedDataGridViewTextBoxColumn";
            this.finishedDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(CY.IoTM.Common.Business.Task);
            // 
            // FormTaskQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 467);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Name = "FormTaskQuery";
            this.Text = "系统任务查询窗口";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn meterMacDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskStateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn finishedDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn controlCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn identificationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataLengthDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataCommandDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskIDDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn commandStateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn answerDataDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn answerDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource bindingSource2;
    }
}
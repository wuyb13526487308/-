namespace CY.IoTM.MeterSimulator
{
    partial class UpLoadFile
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCompanyID = new System.Windows.Forms.TextBox();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.txtOrder = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.btnPublish = new System.Windows.Forms.Button();
            this.txtAP = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAP)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "企业编码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(214, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "日期：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(392, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "序号：";
            // 
            // txtCompanyID
            // 
            this.txtCompanyID.Location = new System.Drawing.Point(89, 36);
            this.txtCompanyID.Name = "txtCompanyID";
            this.txtCompanyID.Size = new System.Drawing.Size(100, 21);
            this.txtCompanyID.TabIndex = 1;
            this.txtCompanyID.Text = "zzcy";
            // 
            // txtDate
            // 
            this.txtDate.Location = new System.Drawing.Point(261, 36);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(100, 21);
            this.txtDate.TabIndex = 1;
            // 
            // txtOrder
            // 
            this.txtOrder.Location = new System.Drawing.Point(439, 36);
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.Size = new System.Drawing.Size(120, 21);
            this.txtOrder.TabIndex = 2;
            this.txtOrder.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "文件：";
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(89, 79);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(470, 21);
            this.txtFile.TabIndex = 1;
            this.txtFile.Text = "zzcy";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(565, 79);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(45, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "选择";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(415, 139);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(45, 23);
            this.btnUpload.TabIndex = 3;
            this.btnUpload.Text = "上传";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(496, 139);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(45, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "广告ID：";
            // 
            // btnPublish
            // 
            this.btnPublish.Location = new System.Drawing.Point(195, 141);
            this.btnPublish.Name = "btnPublish";
            this.btnPublish.Size = new System.Drawing.Size(75, 23);
            this.btnPublish.TabIndex = 4;
            this.btnPublish.Text = "发布广告";
            this.btnPublish.UseVisualStyleBackColor = true;
            this.btnPublish.Click += new System.EventHandler(this.btnPublish_Click);
            // 
            // txtAP
            // 
            this.txtAP.Location = new System.Drawing.Point(87, 139);
            this.txtAP.Name = "txtAP";
            this.txtAP.Size = new System.Drawing.Size(76, 21);
            this.txtAP.TabIndex = 5;
            this.txtAP.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // UpLoadFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 199);
            this.Controls.Add(this.txtAP);
            this.Controls.Add(this.btnPublish);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtOrder);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.txtCompanyID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Name = "UpLoadFile";
            this.Text = "UpLoadFile";
            ((System.ComponentModel.ISupportInitialize)(this.txtOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCompanyID;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.NumericUpDown txtOrder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnPublish;
        private System.Windows.Forms.NumericUpDown txtAP;
    }
}
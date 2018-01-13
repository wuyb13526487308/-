using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CY.IoTM.MeterSimulator
{
    public partial class FormLCD : Form
    {
        MSimulator _ms;
        private List<PublishItem> list = new List<PublishItem>();
        private List<LCD_Item> _lcdItemlist = null;
        private LCD_Item _current_LCD_Item;
        private int itemIndex = 0;
        private bool isDisplay = false;
        private int itemCount = 0;

        public FormLCD(MSimulator ms)
        {
            InitializeComponent();

            this._ms = ms;
            LoadItem();
        }

        private void LoadItem()
        {
            this._lcdItemlist = this._ms.LCD.ADFileList;
            this.itemCount = this._lcdItemlist.Count;
            foreach (LCD_Item item in this._lcdItemlist)
            {
                if (item.ADFile == null) continue;
                list.Add(item.ADItem);
                File f = item.ADFile;
            }
            this.dataGridView1.DataSource = list;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.dataGridView1.Visible = this.checkBox1.Checked;
            this.panel1.Visible = !this.checkBox1.Checked;
            if (this.panel1.Visible)
            {
                isDisplay = true;
                new Thread(DoDisplay).Start();
            }
            else
            {
                isDisplay = false;
            }
        }
        int displayIndex = 0;
        private void DoDisplay()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            while (this.isDisplay)
            {
                if (displayIndex >= itemCount) displayIndex = 0;

                if (this.isDisplay && this._lcdItemlist != null && this._lcdItemlist.Count > 0)
                {
                    if (!this._lcdItemlist[displayIndex].Equals(this._current_LCD_Item))
                    {
                        this._current_LCD_Item = this._lcdItemlist[displayIndex];

                        this.Invoke(new MethodInvoker(delegate
                        {
                            try
                            {
                                ShowADPage();
                            }
                            catch { }
                        }));
                    }
                    if (watch.ElapsedMilliseconds > this._current_LCD_Item.ADItem.DisplayLength * 1000)
                    {
                        watch.Restart();
                        displayIndex++;
                    }
                }
                else
                {
                    if (this.isDisplay)
                        this.Invoke(new MethodInvoker(delegate
                        {
                            LoadItem();
                        }));
                }

                    
                if (!this.isDisplay) break;               
            }
        }

        private void ShowADPage()
        {
            if (!this.isDisplay) return;
            System.Drawing.Bitmap pic = null;
            if (this._current_LCD_Item.ADFile.State == FileState.Normal)
            {
                if (this._current_LCD_Item.ADFile.FileName.ToUpper().IndexOf(".TXT") > 0)
                {
                    //文本文件
                    //pic = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
                    //Graphics g = Graphics.FromImage(pic);
                    //g.Clear(Color.White);
                    String drawString = System.Text.Encoding.Default.GetString(this._current_LCD_Item.ADFile.Data); // Create font and brush.

                    //Font drawFont = new Font("Arial", 16);

                    //SolidBrush drawBrush = new SolidBrush(Color.Black);// Create point for upper-left corner of drawing.
                    //float x = 1.0F; float y = 1.0F;// Draw string to screen.
                    //g.DrawString(drawString, drawFont, drawBrush, x, y);
                    this.richTextBox1.Text = drawString;
                    this.richTextBox1.Visible = true;
                    this.pictureBox1.Visible = false;
                }
                else
                {
                    MemoryStream stream = new MemoryStream();
                    byte[] tmp = this._current_LCD_Item.ADFile.Data;
                    stream.Write(tmp, 0, tmp.Length);
                    stream.Position = 0;
                    pic = new Bitmap(stream);
                    this.pictureBox1.Image = pic;
                    this.richTextBox1.Visible = false;
                    this.pictureBox1.Visible = true;
                }
            }
            else
            {
                //文本文件
                pic = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
                Graphics g = Graphics.FromImage(pic);
                g.Clear(Color.White);
                Font drawFont = new Font("Arial", 16);
                string str = "正在下载文件：" + this._current_LCD_Item.ADFile.FileName;
                SolidBrush drawBrush = new SolidBrush(Color.Black);// Create point for upper-left corner of drawing.
                float x = 1.0F; float y = 1.0F;// Draw string to screen.
                g.DrawString(str, drawFont, drawBrush, x, y);
                this.pictureBox1.Image = pic;
                this.richTextBox1.Visible = false;
                this.pictureBox1.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int index = 0;
            if (index < this._lcdItemlist.Count)
            {
            }
            else
            {
                index = 0;
            }
        }

        private void FormLCD_Load(object sender, EventArgs e)
        {

        }

        private void FormLCD_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.isDisplay = false;
            Thread.Sleep(100);
        }
    }
}

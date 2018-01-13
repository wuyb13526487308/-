using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;

namespace CY.IotM.WebClient.AdM
{
    public partial class txtToIMG : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string str = "高高马永马永马永马永马永马永马永马永马永马永马永马永马永马永马永马永马永马永马永马永马永";
            str += "高高高高高高高高高高高高高高高高高高高高高高高高高高高高高高高高高高";
            str +="hello!马永!hello!马永!hello!马永!hello!马永!hello!马永!hello!马永!hello!马永!hello!马永!hello!马永!hello!马永!hello!马永!hello!马永!hello!马永!hello!马永!hello!马永!hello!马永!hello!马永!hello!马永!hello!马永!hello!马永!hello!马永!hello!马永!hello!马永!hello!马永!hello!马永!hello!马永!hello!马永!hello!马永!";
            
            byte[] byData = new byte[100];
            FileStream file = new FileStream("D:\\wifiPwd.txt", FileMode.Open);
            file.Seek(0, SeekOrigin.Begin);
            file.Read(byData, 0, 100); 
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);

            string bstr = System.Text.Encoding.UTF8.GetString(byData);
            Font font = new Font("宋体", 10);//字体样式
            Color fontcolor = Color.Black; //字段颜色
            Color backcolor = Color.White;//背景颜色
            Bitmap img = TextToBitmap(bstr, font, Rectangle.Empty, fontcolor, backcolor);//生成图片
            //输出图片  
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            Response.BinaryWrite(ms.ToArray());  



            //-----------------------------------------------

            //获取文本
            //string text = this.TextBox1.Text;
            ////得到Bitmap(传入Rectangle.Empty自动计算宽高)
            //Bitmap bmp = TextToBitmap(text, font, Rectangle.Empty, this.TextBox1.ForeColor, this.TextBox1.BackColor);

            ////用PictureBox显示
            //this.pbTextView.Image = bmp;

            //保存到桌面save.jpg
            //string directory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
            //bmp.Save(directory + "\\save.jpg", ImageFormat.Jpeg);

        }


        /// <summary>
        /// 把文字转换才Bitmap
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="rect">用于输出的矩形，文字在这个矩形内显示，为空时自动计算</param>
        /// <param name="fontcolor">字体颜色</param>
        /// <param name="backColor">背景颜色</param>
        /// <returns></returns>
        private Bitmap TextToBitmap(string text, Font font, Rectangle rect, Color fontcolor, Color backColor)
        {
            Graphics g;
            Bitmap bmp;
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);
            if (rect == Rectangle.Empty)
            {
                bmp = new Bitmap(1, 1);
                g = Graphics.FromImage(bmp);
                //计算绘制文字所需的区域大小（根据宽度计算长度），重新创建矩形区域绘图
                SizeF sizef = g.MeasureString(text, font, PointF.Empty, format);

                int width = (int)(sizef.Width + 1);
                int height = (int)(sizef.Height + 1);

                width = 700;
                height = 500;
                rect = new Rectangle(0, 0, width, height);
                bmp.Dispose();

                bmp = new Bitmap(width, height);
            }
            else
            {
                bmp = new Bitmap(rect.Width, rect.Height);
            }

            g = Graphics.FromImage(bmp);

            //使用ClearType字体功能
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.FillRectangle(new SolidBrush(backColor), rect);
            g.DrawString(text, font, Brushes.Black, rect, format);
            return bmp;
        }




    }
}
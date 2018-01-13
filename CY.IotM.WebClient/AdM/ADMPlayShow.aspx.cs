using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using CY.IotM.Common.Tool;
using CY.IoTM.Common.ADSystem;
using System.Drawing;
using System.IO;

namespace CY.IotM.WebClient.AdM
{
    public partial class ADMPlayShow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string CompanyID = Request.QueryString["CompanyID"] == null ? string.Empty : Request.QueryString["CompanyID"].ToString();
            string StoreName = Request.QueryString["StoreName"] == null ? string.Empty : Request.QueryString["StoreName"].ToString();
            string fileLen = Request.QueryString["fileLen"] == null ? string.Empty : Request.QueryString["fileLen"].ToString();
            if (CompanyID == "" || StoreName=="")
            {
                Response.Write("<script>alert('参数错误！CompanyID,StoreName不能为空！')</script>");
                return;
            }
            //文件控制接口
            WCFServiceProxy<IADFileService> fileContrl = new WCFServiceProxy<IADFileService>();

            byte[] Fdata = new byte[int.Parse(fileLen.ToString())];
            //调用图片服务器提取图片或文件的二进制串
            Fdata = fileContrl.getChannel.DownLoad(CompanyID, StoreName);

            //判断文件类型,如果是图片格式直接输出,如果是文件进行文本图转换后输出;
            //取得扩展名
            string fileExtendName = StoreName.Substring(StoreName.IndexOf("."), StoreName.Length - StoreName.IndexOf("."));
            if (fileExtendName.IndexOf("txt") < 0)
            {
                Response.Clear();
                Response.ContentType = "image/gif";
                Response.BinaryWrite(Fdata);
            }
            else {
                string bstr = System.Text.Encoding.UTF8.GetString(Fdata);//转成文本格式;
                Font font = new Font("宋体", 10);//字体样式
                Color fontcolor = Color.Black; //字段颜色
                Color backcolor = Color.White;//背景颜色
                Bitmap img = TextToBitmap(bstr, font, Rectangle.Empty, fontcolor, backcolor);//生成图片
                //输出图片  
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                Response.BinaryWrite(ms.ToArray());  
            }
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

                //width = 700;
                //height = 500;
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
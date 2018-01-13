using CY.IoTM.Common.Business;
using CY.IoTM.DataService.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Channel.MeterTCP
{
   //public class ADFilesControl
   // {
   //    private static ADFilesControl _adfileControl;
   //    private static readonly object _object = new object();
   //    public List<ADFile> _adFileList = new List<ADFile>();

   //    /// <summary>
   //    /// 文件内容最大长度128字节
   //    /// </summary>
   //    int SendMax = 128;
   //    /// <summary>
   //    /// 获取广告文件调度对象
   //    /// </summary>
   //    /// <returns></returns>
   //    public static ADFilesControl getInstance()
   //    {
   //        if (_adfileControl == null)
   //        {
   //            lock (_object)
   //            {
   //                if (_adfileControl == null)
   //                    _adfileControl = new  ADFilesControl();
   //            }
   //        }
   //        return _adfileControl;
   //    }
   //    private ADFilesControl()
   //    {
   //         //从服务中获取广告文件列表
   //        GetAllADFiles();
   //    }
       
   //    /// <summary>
   //    /// 从服务中获取广告文件列表
   //    /// </summary>
   //    private void GetAllADFiles()
   //    {
   //        //从服务中获取广告文件列表
   //        lock (_object)
   //        {
   //            if (_adFileList != null)
   //            {
   //                _adFileList.Clear();  //清空
   //            }
   //            List<ADFile> tmpAdFileList = new AdInfoService().GetAdFileList();
   //            foreach (ADFile oneadfile in tmpAdFileList)
   //            {
   //                if (_adFileList.Contains(oneadfile) == false)
   //                {
   //                    _adFileList.Add(oneadfile);
   //                }
   //            }
   //        }
   //    }
   //    /// <summary>
   //    /// 添加广告文件
   //    /// </summary>
   //    /// <param name="oneAdFile"></param>
   //    public void AddADFile(ADFile oneAdFile)
   //    {
   //        lock (_object)
   //        {
   //            if (_adFileList.Contains(oneAdFile) == false)
   //            {
   //                _adFileList.Add(oneAdFile);
   //            }
   //        }
   //    }
   //    /// <summary>
   //    /// 获取广告文件
   //    /// </summary>
   //    /// <param name="fileid"></param>
   //    /// <returns></returns>
   //    public ADFile GetADFile(int fileID)
   //    {
   //        lock (_object)
   //        {
   //            int id = fileID;
   //            if (this._adFileList.Where(p => p.id == id).ToList().Count == 0)
   //            {
   //                GetAllADFiles();//从服务中读取广告文件列表
   //            }
   //            ADFile adfile = this._adFileList.Where(p => p.id == id).Single();
   //            if (adfile == null) return null;
   //            else
   //            {
   //                int total = adfile.DataContent.Length / SendMax;
   //                if (adfile.DataContent.Length % SendMax == 0)
   //                {
   //                    adfile.TotalSegment = total;
   //                }
   //                else
   //                {
   //                    adfile.TotalSegment = total + 1; //片段总数
   //                }  
   //            }
   //            return adfile;
   //        }
   //    }

   //    /// <summary>
   //    /// 分包文件
   //    /// </summary>
   //    /// <param name="fileid">文件编号</param>
   //    /// <param name="currentsegment">片段索引 从0开始</param>
   //    /// <returns>当前片段文件内容byte[]</returns>
   //    public byte[] FenbaoFile(int fileid,int currentsegment)
   //    {
   //        ADFile adfile = GetADFile(fileid);
   //        byte[] onedatabyte;
   //        if (adfile != null)
   //        {
   //            byte[] contentAllBytes = adfile.DataContent;
   //            onedatabyte = contentAllBytes.Skip(currentsegment * SendMax).Take(SendMax).ToArray();
   //        }
   //        else
   //        { 
   //            onedatabyte=new byte[1];
   //        }
   //        return onedatabyte;
   //    }
   // }
}

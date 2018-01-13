using CY.IotM.Common.Memcached;
using CY.IotM.Common.Tool;
using CY.IoTM.Common.ADSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.DataService.Business
{
    public class ADFileCacheService
    {
        private static object _readOnly = new object();
        private static ADFileCacheService _aDFileCacheService;

        public static ADFileCacheService getInstance()
        {
            if (_aDFileCacheService == null)
            {
                lock (_readOnly)
                {
                    if (_aDFileCacheService == null)
                    {
                        _aDFileCacheService = new ADFileCacheService();
                    }
                }
            }
            return _aDFileCacheService;
        }
        /// <summary>
        /// 私有构造器
        /// </summary>
        private ADFileCacheService()
        {

        }

        public string ReadFileSeg(Stream stream, string companyID, string fileName, int fileLength, int totalSegments, int currentSegmentsIndex, int dataLength)
        {
            lock(_readOnly)
            {
                string result = "";
                if (totalSegments < currentSegmentsIndex)
                    return "请求当前段号大于文件同段数";

                byte[] buffer = ADFileCached.getInstance().ReadFileSegment(companyID, fileName, fileLength, totalSegments, currentSegmentsIndex);
                if (buffer == null)
                {
                    buffer = ADFileCached.getInstance().ReadFile(companyID, fileName);
                    if (buffer == null)
                    {
                        //没有找到缓存文件
                        WCFServiceProxy<IADFileService> _iTaskManageProxy = new WCFServiceProxy<IADFileService>();
                        buffer = _iTaskManageProxy.getChannel.DownLoad(companyID, fileName);
                        _iTaskManageProxy.Close();
                        ADFileCached.getInstance().SaveFile(buffer,companyID, fileName);
                    }
                    MemoryStream ms = new MemoryStream();

                    int bPos = (currentSegmentsIndex - 1) * 1024;
                    if ((bPos + 1024) > (fileLength - 1))
                        dataLength = fileLength - bPos;
                    else
                        dataLength = 1024;

                    ms.Write(buffer, bPos, dataLength);
                    buffer = new byte[dataLength];
                    ms.Position = 0;
                    ms.Read(buffer, 0, dataLength);
                    ms.Close();
                    ADFileCached.getInstance().SaveFileSegment(buffer, companyID, fileName, fileLength, totalSegments, currentSegmentsIndex);
                }


                if (buffer != null)
                    stream.Write(buffer, 0, buffer.Length);
                else
                    result = "读取文件段失败";

                return result;
            }
        }
    }
}

using CY.IoTM.Common.ADSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CY.IoTM.ADService
{
    public class ADFileService : IADFileService
    {
        public string Delete(string companyid, string filename)
        {
            return File.ADFileManager.getInstance().Delete(companyid, filename);
        }

        public byte[] DownLoad(string companyId, string filename)
        {
            MemoryStream ms = new MemoryStream();
            string result = File.ADFileManager.getInstance().Read(companyId, filename, ms);
            if (result == "")
            {
                byte[] buffer = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(buffer, 0, buffer.Length);
                ms.Close();
                return buffer;
            }
            else
            {
                return null;
            }
        }
        public bool Exists(string companyid, string filename)
        {
            return File.ADFileManager.getInstance().Exists(companyid, filename);
        }

        public int FileLength(string companyid, string filename)
        {
            return File.ADFileManager.getInstance().FileLength(companyid, filename);
        }

        public string UpLoad(string companyid, string filename, byte[] data)
        {
            return File.ADFileManager.getInstance().Save(companyid, filename,data);
        }
    }
}

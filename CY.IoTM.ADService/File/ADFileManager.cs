using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.ADService.File
{
    public class ADFileManager
    {
        private static object _object = new object();
        private static ADFileManager _server;
        private string _fileRoot;
        private const string FILE_ROOT_NAME = "FileRoot";


        private ADFileManager()
        {
            //文件存放根目录
            this._fileRoot = System.Configuration.ConfigurationManager.AppSettings[FILE_ROOT_NAME];
            if(this._fileRoot == null || this._fileRoot =="")
            {
                throw new Exception("未设置ADFile的存放路径");
            }
        }

        public static ADFileManager getInstance()
        {
            if (_server == null)
            {
                lock (_object)
                {
                    if (_server == null)
                    {
                        _server = new ADFileManager();
                    }
                }
            }
            return _server;
        }

        public string Save(string root, string filename, byte[] data)
        {
            lock (_object)
            {
                string result = "";
                string savePath = this._fileRoot + @"\" + root;
                if (!System.IO.Directory.Exists(savePath))
                    System.IO.Directory.CreateDirectory(savePath);
                FileStream fs = null;
                try
                {
                    fs = System.IO.File.Create(savePath + @"\" + filename);
                }
                catch (Exception e1)
                {
                    return "创建文件失败，原因："+e1.Message;
                }
                try
                {
                    fs.Write(data, 0, data.Length);

                }
                catch (Exception e)
                {
                    result = "写文件失败，原因：" + e.Message;
                }
                finally
                {
                    fs.Close();
                }

                return result;
            }
        }

        public string Delete(string root, string filename)
        {
            lock (_object)
            {
                string result = "";
                string savePath = this._fileRoot + @"\" + root + @"\" + filename;
                if (System.IO.File.Exists(savePath))
                {
                    try
                    {
                        System.IO.File.Delete(savePath);
                    }
                    catch(Exception e)
                    {
                        result = "删除失败，原因：" + e.Message;
                    }
                } 

                return result;
            }
        }

        public bool Exists(string root, string filename)
        {
            lock (_object)
            {
                string savePath = this._fileRoot + @"\" + root + @"\" + filename;
                if (System.IO.File.Exists(savePath))
                    return true;
                else
                    return false;
            }
        }

        public int FileLength(string root, string filename)
        {
            lock (_object)
            {
                int result = 0;
                FileStream fs = null;
                string savePath = this._fileRoot + @"\" + root + @"\" + filename;
                if (System.IO.File.Exists(savePath))
                {
                    fs = System.IO.File.Open(savePath, FileMode.Open);
                    result = (int)fs.Length;
                    fs.Close();
                }
                else
                {
                    result =-1;
                }

                return result;
            }
        }

        public string Read(string root, string filename,System.IO.MemoryStream outStream)
        {
            lock(_object)
            {
                string result = "";
                FileStream fs = null;
                string savePath = this._fileRoot + @"\" + root + @"\" + filename;
                if (System.IO.File.Exists(savePath))
                {
                    fs = System.IO.File.Open(savePath, FileMode.Open);
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    fs.Close();
                    outStream.Write(buffer, 0, buffer.Length);
                }
                else
                {
                    result = "文件不存在";
                }

                return result;
            }
        }
    }
}

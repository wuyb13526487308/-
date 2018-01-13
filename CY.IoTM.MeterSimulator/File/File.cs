using CY.IoTM.Common.Business;
using CY.IoTM.MongoDataHelper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.MeterSimulator
{
    public class File : BaseEntity
    {
        public string MAC { get; set; }
        public string FileName { get; set; }
        /// <summary>
        /// 获取或设置文件长度
        /// </summary>
        public int Length { get; set; }
        public FileState State { get; set; }
        /// <summary>
        /// 获取或设置文件总分段数
        /// </summary>
        public int TotalSeg { get; set; }
        /// <summary>
        /// 获取或设置当前下载段
        /// </summary>
        public Int16 segIndex { get; set; }
        public byte[] Data { get; set; }

        public void Update()
        {
            MongoDBHelper<File> mongo = new MongoDBHelper<File>();
            var query = new QueryDocument();
            query.Add("_id", this._id);
            var update = new UpdateDocument();
            update.Add("MAC", this.MAC);
            update.Add("FileName", this.FileName);
            update.Add("Length", this.Length);
            update.Add("State", this.State);
            update.Add("TotalSeg", this.TotalSeg);
            update.Add("segIndex", this.segIndex);
            update.Add("Data", this.Data);
            mongo.Update(LCD.FileCollectionName, query, update);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        public void DownLoad(MSimulator mSimulator)
        {
            mSimulator.AddDownLoadTask(this);
        }
    }

    public class PublishItem : BaseEntity
    {
        public string MAC { get; set; }
        public int Order { get; set; }
        public string FileName { get; set; }
        public string ExtName { get; set; }
        public FileState State { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DisplayLength { get; set; }
    }

    public enum FileState
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal =0,
        /// <summary>
        /// 下载中
        /// </summary>
        Loading =1,
    }
}

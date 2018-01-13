using CY.IoTM.Common.Item;
using CY.IoTM.MongoDataHelper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.MeterSimulator
{
    public class LCD
    {
        private MSimulator _mSimulator;
        public const string MSPublishItemCollectionName = "MSPublishItem";
        public const string FileCollectionName = "MSFile";
        private List<LCD_Item> _adFileList = new List<LCD_Item>();

        public LCD(MSimulator ms)
        {
            this._mSimulator = ms;
        }

        

        public void SaveADList(string mac, DataItem_C109 item)
        {
            //清除广告列表
            var dataList = ReadPublish(mac);
            //dataList.ForEach(delegate (PublishItem pitem)
            //{
            //    DeletePublishItem(pitem);
            //});

            foreach (PublishItem pitem in dataList)
            {
                DeletePublishItem(pitem);
            }
            var fileList = from p in item.FileList
                           orderby p.FileNO
                           select p;

            this._adFileList.Clear();

            foreach (CY.IoTM.Common.Item.ADFile file in fileList)
            {
                PublishItem pubItem = new PublishItem() { MAC = mac, Order = file.FileNO, FileName = file.FileName, DisplayLength = file.PollTime, BeginDate = file.DTStart, EndDate = file.DTEnd };
                File fl = ReadFile(mac, file.FileName);
                if (fl == null)
                {
                    //插入文件
                    InsertFile(mac, file);
                    pubItem.State = FileState.Loading;
                }
                else
                {
                    pubItem.State = fl.State;
                }
                InsertPublishItem(pubItem);
                this._adFileList.Add(new LCD_Item(this._mSimulator) { ADFile = fl, ADItem = pubItem });
            }
        }

        private void LoadLCDItem()
        {
            var dataList = ReadPublish(this._mSimulator.Mac);
            var fileList = from p in dataList
                           orderby p.Order
                           select p;

            this._adFileList.Clear();
            foreach (PublishItem pubItem in fileList)
            {
                File fl = ReadFile(this._mSimulator.Mac, pubItem.FileName);
                this._adFileList.Add(new LCD_Item(this._mSimulator) { ADFile = fl, ADItem = pubItem });
            }
        }
        private List<PublishItem> ReadPublish(string mac)
        {
            MongoDBHelper<PublishItem> mongo = new MongoDBHelper<PublishItem>();
            QueryDocument query = new QueryDocument();
            query.Add("MAC", mac);
            MongoCursor<PublishItem> mongoCursor = mongo.Query(MSPublishItemCollectionName, query);
            var dataList = mongoCursor.ToList();
            return dataList;
        }
        private void InsertPublishItem(PublishItem pubItem)
        {
            MongoDBHelper<PublishItem> mongo = new MongoDBHelper<PublishItem>();
            mongo.Insert(MSPublishItemCollectionName, pubItem);

        }

        private void DeletePublishItem(PublishItem pitem)
        {
            MongoDBHelper<PublishItem> mongo = new MongoDBHelper<PublishItem>();
            var iDelete = new QueryDocument();
            iDelete.Add("MAC", pitem.MAC);
            iDelete.Add("FileName", pitem.FileName);
            //删除老计划
            mongo.Delete(MSPublishItemCollectionName, iDelete);
        }
        private void InsertFile(string mac, CY.IoTM.Common.Item.ADFile file)
        {
            MongoDBHelper<File> mongo = new MongoDBHelper<File>();
            File f = new File() { MAC = mac, FileName = file.FileName, Length = file.FileLength, State = FileState.Loading, segIndex = 0 };
            f.TotalSeg = file.FileLength / 1024 + (file.FileLength % 1024 > 0 ? 1 : 0);
            mongo.Insert(FileCollectionName, f);
        }
        private File ReadFile(string mac, string filename)
        {
            MongoDBHelper<File> mongo = new MongoDBHelper<File>();
            QueryDocument query = new QueryDocument();
            query.Add("MAC", mac);
            query.Add("FileName", filename);
            MongoCursor<File> mongoCursor = mongo.Query(FileCollectionName, query);
            var dataList = mongoCursor.ToList();
            if (dataList != null && dataList.Count == 1)
                return dataList[0];
            else
                return null;

        }

        public List<LCD_Item> ADFileList
        {
            get
            {
                //if (this._adFileList.Count == 0)
                    LoadLCDItem();
                return this._adFileList;
            }
        }
    }

    public class LCD_Item
    {
        public PublishItem ADItem{get; set; }
        private File _file;
        private MSimulator _mSimulator;
        public LCD_Item(MSimulator mSimulator)
        {
            this._mSimulator = mSimulator;
        }
        public File ADFile
        {
            get
            {
                if (_file != null && _file.State != FileState.Normal)
                {
                    //启动下载文件功能
                    this._file.DownLoad(this._mSimulator);
                }
                return this._file;
            }
            set
            {
                this._file =value;
            }
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using CY.IoTM.SystemInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CY.IotM.Common;

namespace CY.IoTM.SystemInterface.Tests
{
    [TestClass()]
    public class UserTests
    {
        [TestMethod()]
        public void AddIotUserTest()
        {
            //测试添加燃气用户
            Random random = new System.Random();
            IoT_User userInfo = new IoT_User()
            {
                SF_UserId = $"{DateTime.Now.ToString("ddHHmm")}{random.Next(1000, 9999).ToString()}",
                Address = $"街道-测试地址{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}",
                Street = $"街道-{random.Next(1, 9999).ToString()}",
                Community = "花语里(二期)",
                BGL = true,
                Door = "",
                DY = "1单元",
                Phone = "13526487308",
                LD = "28号楼",
                SFZH = "1234567890123657",
                UserType = "0",
                UserName = "测试用户"
            };
            IoT_Meter meterinfo = new IoT_Meter()
            {
                MeterNo = "13526487308995",//表号，物联网表号，14位数字字符串
                Direction = "右表",
                InstallDate = DateTime.Now,
                InstallPlace = "高位安装",
                Installer = "",
                MeterModel = "户内"
            };
            User user = new User();
            string result = user.AddIotUser(userInfo, meterinfo);
            Assert.AreEqual(result, "", true);
        }

        [TestMethod()]
        public void UpdateIotUserTest()
        {
            IoT_User userInfo = new IoT_User()
            {
                UserID = "2018040032",
                Address = $"街道-测试地址-修改{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}",
                Street = $"街道",
                Community = "花语里(二期)",
                BGL = true,
                Door = "123",
                DY = "1单元",
                Phone = "13526487308",
                LD = "28号楼",
                SFZH = "1234567890123654",
                UserType = "0",
                UserName = "测试用户"
            };
            IoT_Meter meterinfo = new IoT_Meter()
            {
                MeterNo = "13526487308992",//表号，物联网表号，14位数字字符串
                Direction = "左表",
                InstallDate = DateTime.Now,
                InstallPlace = "低位安装",
                Installer = "武宜波",
                MeterModel = "户内"
                
            };

            User user = new User();
            string result = user.UpdateIotUser(userInfo, meterinfo);
            Assert.AreEqual(result, "", true);
        }
    }
}
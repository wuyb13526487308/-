using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace CY.IoTM.Common.Business
{
    /// <summary>
    /// 任务管理接口
    /// </summary>
    [ServiceContract()]
    public interface ITaskManage
    {
        /// <summary>
        /// 根据表地址MAC读取表基础信息
        /// </summary>
        /// <param name="mac"></param>
        /// <returns></returns>
        [OperationContract]
        Meter GetMeter(string mac);
        /// <summary>
        /// 获取指定表的通讯任务（发给燃气表的执行任务）
        /// </summary>
        /// <param name="mac">表地址（表号）</param>
        /// <returns></returns>
        [OperationContract]
        List<Task> GetTaskList(string mac);

        /// <summary>
        /// 设置指令执行完成。
        /// </summary>
        /// <param name="command"></param>
        [OperationContract]
        void CommandCompletes(Command command,Task task);
        /// <summary>
        /// 任务执行完成
        /// </summary>
        /// <param name="task"></param>
        [OperationContract]
        void TaskCompletes(Task task, decimal ljGas);

        /// <summary>
        /// 设置对时任务
        /// </summary>
        /// <param name="task"></param>
        [OperationContract]
        void SetDuiShiTask(Task task, Command cmd);
        /// <summary>
        /// 更新表对象（mongo中）
        /// </summary>
        /// <param name="meter"></param>
        /// <returns></returns>
        [OperationContract]
        string UpdateMeter(Meter meter);

        int TestChannel();

        /// <summary>
        /// 读取数据库中登记的所有表（该方法仅用于模拟器测试）
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<IoT_Meter> getIotMeters(string address = "",string meterMac = "" );

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CY.IoTM.OlbCommon;
using System.Data;
using System.Data.Linq;
using CY.IoTM.OlbCommon.Tool;


namespace CY.IoTM.OlbService
{
    /// <summary>
    /// »º∆¯’Àªßπ‹¿Ì
    /// </summary>
    public class GasUserManageService : IGasUserManage
    {



        private static GasUserManageService instance = null;
        public static GasUserManageService GetInstance()
        {
            if (instance == null)
            {
                instance = new GasUserManageService();
            }
            return instance;
        }

        string configName = "";

        private GasUserManageService()
        {
            configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"]; 
        }




        /// <summary>
        /// ÃÌº”»º∆¯’À∫≈
        /// </summary>
        /// <param name="account"></param>
        /// <param name="userId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public Message AddGasUser(string account, string userId,string companyId) {

            Message m;
            try
            {
              
                DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
                Table<Olb_GasUserRelation> tbl = dd.GetTable<Olb_GasUserRelation>();

                Olb_GasUserRelation relation = new Olb_GasUserRelation()
                {
                    Account = account,
                    CompanyID = companyId,
                    GasUserID = userId
                };

                tbl.InsertOnSubmit(relation);
                dd.SubmitChanges();

                m = new Message()
                {
                    Result = true,
                    TxtMessage = JSon.TToJson<Olb_GasUserRelation>(relation)
                };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "ÃÌº”»º∆¯’À∫≈ ß∞‹£°" + e.Message
                };
            }
            return m;
           
        }

        /// <summary>
        /// …æ≥˝»º∆¯’À∫≈
        /// </summary>
        /// <param name="account"></param>
        /// <param name="userId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public Message RemoveGasUser(string account, string userId, string companyId)
        {

            Message m;
            try
            {
                DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
                Table<Olb_GasUserRelation> tbl = dd.GetTable<Olb_GasUserRelation>();
                var s = tbl.Where(p => p.Account == account && p.GasUserID == userId && p.CompanyID == companyId).Single();
                tbl.DeleteOnSubmit(s as Olb_GasUserRelation);

                dd.SubmitChanges();
                m = new Message()
                {
                    Result = true,
                    TxtMessage = "…æ≥˝»º∆¯’À∫≈≥…π¶£°"
                };
            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "…æ≥˝»º∆¯’À∫≈ ß∞‹£°" + e.Message
                };
            }
            return m;
           
        }

        /// <summary>
        /// ªÒ»°»º∆¯’À∫≈
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public List<GasUser> GetGasUserList(string account)
        {

            List<GasUser> list = new List<GasUser>();

            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            Table<Olb_GasUserRelation> tbl = dd.GetTable<Olb_GasUserRelation>();
            List<Olb_GasUserRelation> relationList = tbl.Where(p => p.Account == account).ToList();

            foreach (Olb_GasUserRelation o in relationList)
            {
                GasUser user = IotMService.GetInstance().GetGasUserByUserId(o.GasUserID, o.CompanyID);
                list.Add(user);
            }
            return list;

        }


	}
}



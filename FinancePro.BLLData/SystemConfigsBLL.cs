using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancePro.DALData;
using FinancePro.DataModels;

namespace FinancePro.BLLData
{
    public class SystemConfigsBLL
    {
        /// <summary>
        /// 根据ID查询配置项值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetConfigsValueByID(int id)
        {
            return SystemConfigsDAL.GetConfigsValueByID(id);
        }
        /// <summary>
        /// 添加新的配置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddNewSystemConfigs(SystemConfigsModel model)
        {
            return SystemConfigsDAL.AddNewSystemConfigs(model);
        }
        /// <summary>
        /// 修改配置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateConfigs(SystemConfigsModel model)
        {
            return SystemConfigsDAL.UpdateConfigs(model);
        }
        /// <summary>
        /// 查询所有的配置项
        /// </summary>
        /// <param name="isadmin"></param>
        /// <returns></returns>
        public List<SystemConfigsModel> GetAllConfigs(int isadmin)
        {
            return SystemConfigsDAL.GetAllConfigs(isadmin);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancePro.DALData;
using FinancePro.DataModels;

namespace FinancePro.BLLData
{
    public class ReginTableBLL
    {
        /// <summary>
        /// 得到行政区域列表
        /// </summary>
        /// <param name="parentid">父级ID</param>
        /// <returns></returns>
        public static List<ReginTableModel> GetReginTableListModel(int parentid)
        {
            return ReginTableDAL.GetReginTableListModel(parentid);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancePro.DataModels;

namespace FinancePro.DALData
{
    public class ReginTableDAL
    {
        public static DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 得到行政区域列表
        /// </summary>
        /// <param name="parentid">父级ID</param>
        /// <returns></returns>
        public static List<ReginTableModel> GetReginTableListModel(int parentid)
        {
            List<ReginTableModel> list = new List<ReginTableModel>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select REGION_ID, REGION_CODE, REGION_NAME, PARENT_ID, REGION_LEVEL, REGION_ORDER, REGION_NAME_EN, REGION_SHORTNAME_EN  ");
            strSql.Append("  from ReginTable ");
            strSql.Append(" where PARENT_ID=@PARENT_ID  ORDER BY LEN(REGION_NAME) ASC ,REGION_NAME_EN ASC");
            SqlParameter[] parameters = {
					new SqlParameter("@PARENT_ID", SqlDbType.Int)	};
            parameters[0].Value = parentid;
            DataSet ds = helper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    ReginTableModel model = new ReginTableModel();
                    if (item["REGION_ID"].ToString() != "")
                    {
                        model.REGION_ID = int.Parse(item["REGION_ID"].ToString());
                    }
                    model.REGION_CODE = item["REGION_CODE"].ToString();
                    model.REGION_NAME = item["REGION_NAME"].ToString();
                    if (item["PARENT_ID"].ToString() != "")
                    {
                        model.PARENT_ID = int.Parse(item["PARENT_ID"].ToString());
                    }
                    if (item["REGION_LEVEL"].ToString() != "")
                    {
                        model.REGION_LEVEL = int.Parse(item["REGION_LEVEL"].ToString());
                    }
                    if (item["REGION_ORDER"].ToString() != "")
                    {
                        model.REGION_ORDER = int.Parse(item["REGION_ORDER"].ToString());
                    }
                    model.REGION_NAME_EN = item["REGION_NAME_EN"].ToString();
                    model.REGION_SHORTNAME_EN = item["REGION_SHORTNAME_EN"].ToString();
                    list.Add(model);
                }
                return list;
            }
            else
            {
                return null;
            }
        }
    }
}

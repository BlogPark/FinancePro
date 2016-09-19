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
    public class SystemConfigsDAL
    {
        public static DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 添加新的配置项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddNewSystemConfigs(SystemConfigsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SystemConfigs(");
            strSql.Append("ConfigName,ConfigValue,ConfigRemark,AddTime,ConfigStatus,IsAdmin");
            strSql.Append(") values (");
            strSql.Append("@ConfigName,@ConfigValue,@ConfigRemark,GETDATE(),1,@IsAdmin");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@ConfigName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@ConfigValue", SqlDbType.NVarChar) ,            
                        new SqlParameter("@ConfigRemark", SqlDbType.NVarChar) ,       
                        new SqlParameter("@IsAdmin", SqlDbType.Int)  
            };
            parameters[0].Value = model.ConfigName;
            parameters[1].Value = model.ConfigValue;
            parameters[2].Value = model.ConfigRemark;
            parameters[3].Value = model.IsAdmin;
            object obj = helper.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 根据ID得到配置信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static string GetConfigsValueByID(int id)
        {
            string result = "";
            string sqltxt = @"SELECT  ConfigValue
FROM    SystemConfigs
WHERE   ID = @id
        AND ConfigStatus = 1";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@id",id)
                                      };
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                result = dt.Rows[0]["ConfigValue"].ToString();
            }
            return result;
        }
        /// <summary>
        /// 修改配置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int UpdateConfigs(SystemConfigsModel model)
        {
            int rowcount = 0;
            string sqltxt = @"UPDATE  SystemConfigs
SET     ConfigName = @ConfigName ,
        ConfigValue = @ConfigValue ,
        ConfigRemark = @ConfigRemark ,
        ConfigStatus = @ConfigStatus
WHERE   ID = @id";
            SqlParameter[] paramter ={
                                    new SqlParameter("@ConfigName",model.ConfigName),
                                    new SqlParameter("@ConfigValue",model.ConfigValue),
                                    new SqlParameter("@ConfigRemark",model.ConfigRemark),
                                    new SqlParameter("@ConfigStatus",model.ConfigStatus),
                                    new SqlParameter("@id",model.ID)
                                    };
            rowcount = helper.ExecuteSql(sqltxt, paramter);
            return rowcount;
        }
        /// <summary>
        /// 得到所有的系统配置
        /// </summary>
        /// <param name="isadmin">身份标识</param>
        /// <returns></returns>
        public static List<SystemConfigsModel> GetAllConfigs(int isadmin = 0)
        {
            List<SystemConfigsModel> list = new List<SystemConfigsModel>();
            string sqltxt = @"SELECT  ID ,
        ConfigName ,
        ConfigFID ,
        ConfigValue ,
        ConfigRemark ,
        AddTime ,
        ConfigStatus ,
        CASE ConfigStatus
          WHEN 1 THEN '启用'
          ELSE '禁用'
        END AS ConfigStatusName
FROM    dbo.SystemConfigs WITH(NOLOCK) ";
            if (isadmin == 0)
            {
                sqltxt += "where ISNULL(IsAdmin,0)=0 ";
            }
            DataTable dt = helper.Query(sqltxt).Tables[0];
            foreach (DataRow item in dt.Rows)
            {
                SystemConfigsModel model = new SystemConfigsModel();
                model.AddTime = item["AddTime"].ToString().ParseToDateTime(DateTime.Now);
                model.ConfigName = item["ConfigName"].ToString();
                model.ConfigRemark = item["ConfigRemark"].ToString();
                model.ConfigStatus = int.Parse(item["ConfigStatus"].ToString());
                model.ConfigStatusName = item["ConfigStatusName"].ToString();
                model.ConfigValue = item["ConfigValue"].ToString();
                model.ID = int.Parse(item["ID"].ToString());
                list.Add(model);
            }
            return list;
        }
    }
}

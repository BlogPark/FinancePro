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
    /// <summary>
    /// 会员编号操作类
    /// </summary>
    public class MemberCodeDAL
    {
        public static DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 新增会员编号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddNewMemberCode(MemberCodeModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MemberCode(");
            strSql.Append("MemberCode,CStatus");
            strSql.Append(") values (");
            strSql.Append("@MemberCode,1");
            strSql.Append(") ");
            SqlParameter[] parameters = {       
                        new SqlParameter("@MemberCode", SqlDbType.Int)    
            };
            parameters[0].Value = model.MemberCode;
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
        /// 得到会员编号
        /// </summary>
        /// <returns></returns>
        public static int GetMemberCode()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TOP 1 MemberCode  ");
            strSql.Append("  from MemberCode ");
            strSql.Append(" where  CStatus=1");
            return helper.GetSingle(strSql.ToString()).ToString().ParseToInt(0);
        }
        /// <summary>
        /// 得到最大会员编号
        /// </summary>
        /// <returns></returns>
        public static int GetMaxMemberCode()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MAX(MemberCode)  ");
            strSql.Append("  from MemberCode ");
            return helper.GetSingle(strSql.ToString()).ToString().ParseToInt(0);
        }
    }
}

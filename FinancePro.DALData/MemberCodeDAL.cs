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
            string sqltxt = @"IF NOT EXISTS ( SELECT  1
                FROM    dbo.MemberCode
                WHERE   MemberCode = @MemberCode )
    BEGIN 
        INSERT  INTO dbo.MemberCode
                ( MemberCode, CStatus )
        VALUES  ( @MemberCode, 1 )
    END";
            SqlParameter[] parameters = {       
                        new SqlParameter("@MemberCode", SqlDbType.Int)    
            };
            parameters[0].Value = model.MemberCode;
            object obj = helper.GetSingle(sqltxt, parameters);
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
        public static MemberCodeModel GetMemberCode()
        {
            MemberCodeModel model = new MemberCodeModel();
            string sqltxt = @"update top (1)  A  
  set cstatus=2
  output deleted.MemberCode,deleted.ID
  from FinanceProData.dbo.MemberCode A where cstatus=1";
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("select TOP 1 ID,MemberCode  ");
            //strSql.Append("  from MemberCode ");
            //strSql.Append(" where  CStatus=1");
            DataTable dt = helper.Query(sqltxt).Tables[0];
            if (dt.Rows.Count > 0)
            {
                int id=dt.Rows[0]["ID"].ToString().ParseToInt(0);               
                model.ID=id;
                model.MemberCode = dt.Rows[0]["MemberCode"].ToString().ParseToInt(0);
            }
            return model;
        }
        /// <summary>
        /// 得到最大会员编号
        /// </summary>
        /// <returns></returns>
        public static int GetMaxMemberCode()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ISNULL(MAX(MemberCode),0)  ");
            strSql.Append("  from MemberCode ");
            return helper.GetSingle(strSql.ToString()).ToString().ParseToInt(0);
        }
        /// <summary>
        /// 更改数据状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateMemberCodeStatus(int id)
        {
            string sqltxt = @" UPDATE dbo.MemberCode
  SET CStatus=0
  WHERE ID=@id";
            SqlParameter[] paramter = { new SqlParameter("@id",id)};
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 更改数据临时状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateMemberCodeTempStatus(int id)
        {
            string sqltxt = @" UPDATE dbo.MemberCode
  SET CStatus=2
  WHERE ID=@id";
            SqlParameter[] paramter = { new SqlParameter("@id", id) };
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 查询剩余会员编号数量
        /// </summary>
        /// <returns></returns>
        public static int GetCanUsedCodeCount ()
        {
            string sqltxt = @"  SELECT COUNT(0)
  FROM dbo.MemberCode
  WHERE CStatus=1";
            return helper.GetSingle(sqltxt).ToString().ParseToInt(0);
        }
    }
}

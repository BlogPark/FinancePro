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
    ///会员扩展信息操作结构类
    /// </summary>
    public class MemberExtendInfoDAL
    {
        public static DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 添加新会员的扩展信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddNewMemberExtendInfo(MemberExtendInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MemberExtendInfo(");
            strSql.Append("MemberID,MemberName,MemberCode,FormCurreyNum");
            strSql.Append(") values (");
            strSql.Append("@MemberID,@MemberName,@MemberCode,@FormCurreyNum");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@MemberID", SqlDbType.Int) ,            
                        new SqlParameter("@MemberName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@FormCurreyNum", SqlDbType.Int)  
            };
            parameters[0].Value = model.MemberID;
            parameters[1].Value = model.MemberName;
            parameters[2].Value = model.MemberCode;
            parameters[3].Value = model.FormCurreyNum;
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
        /// 读取会员的扩展信息
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public static MemberExtendInfoModel GetMemberExtendInfoByMemberID(int memberid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *  ");
            strSql.Append("  from MemberExtendInfo ");
            strSql.Append(" where MemberID=@MemberID");
            SqlParameter[] parameters = {
					new SqlParameter("@MemberID", SqlDbType.Int)
			};
            parameters[0].Value = memberid;
            MemberExtendInfoModel model = new MemberExtendInfoModel();
            DataSet ds = helper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.ID = ds.Tables[0].Rows[0]["ID"].ToString().ParseToInt(0);
                model.MemberID = ds.Tables[0].Rows[0]["MemberID"].ToString().ParseToInt(0);
                model.MemberName = ds.Tables[0].Rows[0]["MemberName"].ToString();
                model.MemberCode = ds.Tables[0].Rows[0]["MemberCode"].ToString();
                model.FormCurreyNum = ds.Tables[0].Rows[0]["FormCurreyNum"].ToString().ParseToInt(0);
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 更改会员的报单币数量
        /// </summary>
        /// <param name="formcurrey"></param>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public static int UpdateMemberFormCurrey(int formcurrey,int memberid,string remark)
        {
            string sqltxt = @"UPDATE  A
SET     FormCurreyNum = FormCurreyNum + @FormCurreyNum
OUTPUT  DELETED.MemberID ,
        DELETED.MemberName ,
        DELETED.MemberCode ,
        DELETED.FormCurreyNum ,
        INSERTED.FormCurreyNum ,
        @remark ,
        GETDATE()
        INTO dbo.MemberFormCurreyLog
FROM    dbo.MemberExtendInfo A
WHERE   MemberID = @memberid";
            SqlParameter[] paramter = {
                                          new SqlParameter("@FormCurreyNum",formcurrey),
                                          new SqlParameter("@remark",remark),
                                          new SqlParameter("@memberid",memberid)
                                    };
            return helper.ExecuteSql(sqltxt, paramter);
        }
    }
}

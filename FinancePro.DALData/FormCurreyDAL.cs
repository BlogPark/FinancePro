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
    public class FormCurreyDAL
    {
        public static DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 新增系统报单币(带日志)
        /// </summary>
        /// <param name="countnum"></param>
        /// <returns></returns>
        public static bool AddNewFormCurrey(int countnum)
        {
            string sqltxt = @"UPDATE  SystemConfigs
SET     ConfigValue = CONVERT(INT, ConfigValue) + @count
OUTPUT  0 ,
        '' ,
        '' ,
        @count ,
        '系统新增' + @count + '个报单币' ,
        GETDATE()
        INTO FormCurreyLog
WHERE   ID = 1";
            SqlParameter[] parameters = {
			            new SqlParameter("@count", countnum) 
            };
            int rows = helper.ExecuteSql(sqltxt, parameters);
            if (rows > 0)
            {
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// 扣减系统的报单币
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int UpdateDeductionFormCurrey(FormCurreyLogModel model)
        {
            string sqltxt = @"UPDATE  SystemConfigs
SET     ConfigValue = CONVERT(INT, ConfigValue) - @count
OUTPUT  @memberid ,
        @membername ,
        @membercode ,
        @count ,
        @remark ,
        GETDATE()
        INTO FormCurreyLog
WHERE   ID = 1";
            SqlParameter[] parameters = {
			            new SqlParameter("@count", model.FormCurreyNum),
                        new SqlParameter("@memberid",model.MemberID),
                        new SqlParameter("@membername",model.MemberName),
                        new SqlParameter("@membercode",model.MemberCode),
                        new SqlParameter("@remark",model.Remark)
            };
            return helper.ExecuteSql(sqltxt, parameters);
        }
        /// <summary>
        /// 会员得到转赠的报单币
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddNewMemberFormCurrey(MemberFormCurreyLogModel model)
        {
            string sqltxt = @"UPDATE  dbo.MemberExtendInfo
SET     FormCurreyNum = FormCurreyNum + @count
OUTPUT  DELETED.MemberID ,
        DELETED.MemberName ,
        DELETED.MemberCode ,
        DELETED.FormCurreyNum ,
        INSERTED.FormCurreyNum ,
        @remark ,
        GETDATE()
        INTO dbo.MemberFormCurreyLog
WHERE   MemberID = @memberid";
            SqlParameter[] paramter = { 
                                          new SqlParameter("@count",model.NFormCurreyNum),
                                          new SqlParameter("@remark",model.Remark),
                                          new SqlParameter("@memberid",model.MemberID)
                                    };
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 扣减会员的报单币
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int UpdateDeductionMemberFormCurrey(MemberFormCurreyLogModel model)
        {
            string sqltxt = @"UPDATE  dbo.MemberExtendInfo
SET     FormCurreyNum = FormCurreyNum - @count
OUTPUT  DELETED.MemberID ,
        DELETED.MemberName ,
        DELETED.MemberCode ,
        DELETED.FormCurreyNum ,
        INSERTED.FormCurreyNum ,
        @remark ,
        GETDATE()
        INTO dbo.MemberFormCurreyLog
WHERE   MemberID = @memberid";
            SqlParameter[] paramter = { 
                                          new SqlParameter("@count",model.NFormCurreyNum),
                                          new SqlParameter("@remark",model.Remark),
                                          new SqlParameter("@memberid",model.MemberID)
                                    };
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 查询会员的报单币余额
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public static int GetMemberFormCurrey(int memberid)
        {
            string sqltxt = @"SELECT  ISNULL(FormCurreyNum, 0) FormCurreyNum
FROM    dbo.MemberExtendInfo
WHERE   MemberID = @memberid";
            SqlParameter[] paramter = { new SqlParameter("@memberid",memberid)};
            return helper.GetSingle(sqltxt, paramter).ToString().ParseToInt(0);
        }
    }
}

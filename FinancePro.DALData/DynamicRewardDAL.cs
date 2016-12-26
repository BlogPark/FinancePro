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
    public class DynamicRewardDAL
    {
        public static DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 添加新记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddNewDynamicReward(DynamicRewardModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DynamicReward(");
            strSql.Append("SourceMemberName,LStatus,MemberID,MemberName,GameCurrency,SharesCurrency,ShoppingCurrency,MemberPoints,CompoundCurrency,SourceMemberID,LType");
            strSql.Append(") values (");
            strSql.Append("@SourceMemberName,1,@MemberID,@MemberName,@GameCurrency,@SharesCurrency,@ShoppingCurrency,@MemberPoints,@CompoundCurrency,@SourceMemberID,@LType");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@SourceMemberName", SqlDbType.NVarChar) ,  
                        new SqlParameter("@MemberID", SqlDbType.Int) ,            
                        new SqlParameter("@MemberName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@GameCurrency", SqlDbType.Decimal) ,            
                        new SqlParameter("@SharesCurrency", SqlDbType.Decimal) ,            
                        new SqlParameter("@ShoppingCurrency", SqlDbType.Decimal) ,            
                        new SqlParameter("@MemberPoints", SqlDbType.Decimal) ,            
                        new SqlParameter("@CompoundCurrency", SqlDbType.Decimal) ,            
                        new SqlParameter("@SourceMemberID", SqlDbType.Int),
                        new SqlParameter("@LType",SqlDbType.Int)
            };
            parameters[0].Value = model.SourceMemberName;
            parameters[1].Value = model.MemberID;
            parameters[2].Value = model.MemberName;
            parameters[3].Value = model.GameCurrency;
            parameters[4].Value = model.SharesCurrency;
            parameters[5].Value = model.ShoppingCurrency;
            parameters[6].Value = model.MemberPoints;
            parameters[7].Value = model.CompoundCurrency;
            parameters[8].Value = model.SourceMemberID;
            parameters[9].Value = model.Ltype;
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
        /// 释放会员的动态金额
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public static int ReleaseDynamicReward(int memberid, string remark)
        {
            string sqltxt = @"UPDATE  A
SET     GameCurrency = A.GameCurrency + B.GameCurrency ,
        SharesCurrency = A.SharesCurrency + b.SharesCurrency ,
        ShoppingCurrency = A.ShoppingCurrency + b.ShoppingCurrency ,
        MemberPoints = A.MemberPoints + b.MemberPoints ,
        CompoundCurrency = A.CompoundCurrency + b.CompoundCurrency
OUTPUT  DELETED.MemberID ,
        DELETED.MemberName ,
        DELETED.MemberCode ,
        DELETED.MemberPoints ,
        INSERTED.MemberPoints ,
        DELETED.GameCurrency ,
        INSERTED.GameCurrency ,
        DELETED.SharesCurrency ,
        INSERTED.SharesCurrency ,
        DELETED.ShoppingCurrency ,
        INSERTED.ShoppingCurrency ,
        DELETED.CompoundCurrency ,
        INSERTED.CompoundCurrency ,
        @remark ,
        GETDATE()
        INTO MemberCapitalLog ( MemberID, MemberName, MemberCode,
                                BMemberPoints, NMemberPoints, BGameCurrency,
                                NGameCurrency, BSharesCurrency,
                                NSharesCurrency, BShoppingCurrency,
                                NShoppingCurrency, BCompoundCurrency,
                                NCompoundCurrency, LogRemark, AddTime )
FROM    dbo.MemberCapitalDetail A
        INNER JOIN dbo.DynamicReward B ON A.MemberID = B.MemberID
                                          AND B.LStatus = 1 AND B.SourceMemberID=@memberid";
            SqlParameter[] paramter = { new SqlParameter("@remark", remark), new SqlParameter("@memberid",memberid) };
            return helper.ExecuteSql(sqltxt,paramter);
        }
        /// <summary>
        /// 按照奖励类型释放奖励金额
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="type"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public static int ReleaseDynamicRewardByType(int memberid, int type,string remark)
        {
            string sqltxt = @"UPDATE  A
SET     GameCurrency = A.GameCurrency + B.GameCurrency ,
        SharesCurrency = A.SharesCurrency + b.SharesCurrency ,
        ShoppingCurrency = A.ShoppingCurrency + b.ShoppingCurrency ,
        MemberPoints = A.MemberPoints + b.MemberPoints ,
        CompoundCurrency = A.CompoundCurrency + b.CompoundCurrency
OUTPUT  DELETED.MemberID ,
        DELETED.MemberName ,
        DELETED.MemberCode ,
        DELETED.MemberPoints ,
        INSERTED.MemberPoints ,
        DELETED.GameCurrency ,
        INSERTED.GameCurrency ,
        DELETED.SharesCurrency ,
        INSERTED.SharesCurrency ,
        DELETED.ShoppingCurrency ,
        INSERTED.ShoppingCurrency ,
        DELETED.CompoundCurrency ,
        INSERTED.CompoundCurrency ,
        @remark ,
        GETDATE()
        INTO MemberCapitalLog ( MemberID, MemberName, MemberCode,
                                BMemberPoints, NMemberPoints, BGameCurrency,
                                NGameCurrency, BSharesCurrency,
                                NSharesCurrency, BShoppingCurrency,
                                NShoppingCurrency, BCompoundCurrency,
                                NCompoundCurrency, LogRemark, AddTime )
FROM    dbo.MemberCapitalDetail A
        INNER JOIN dbo.DynamicReward B ON A.MemberID = B.MemberID
                                          AND B.LStatus = 1 AND B.SourceMemberID=@memberid AND B.LType=@LType";
            SqlParameter[] paramter = { new SqlParameter("@remark", remark), new SqlParameter("@memberid", memberid), new SqlParameter("@LType",type) };
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 更改会员的释放状态
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public static int UpdateDynamicRewardStatus(int memberid)
        {
            string sqltxt = @"UPDATE   dbo.DynamicReward  SET LStatus=2
                                WHERE LStatus = 1 AND SourceMemberID=@memberid";
            SqlParameter[] paramter = {  new SqlParameter("@memberid", memberid) };
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 按照类型更改奖励状态
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int UpdateDynamicRewardStatusByType(int memberid,int type)
        {
            string sqltxt = @"UPDATE   dbo.DynamicReward  SET LStatus=2
                                WHERE LStatus = 1 AND SourceMemberID=@memberid AND LType=@LType";
            SqlParameter[] paramter = { new SqlParameter("@memberid", memberid), new SqlParameter("@LType",type) };
            return helper.ExecuteSql(sqltxt, paramter);
        }
    }
}

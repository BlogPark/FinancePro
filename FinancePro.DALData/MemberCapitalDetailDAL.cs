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
    public class MemberCapitalDetailDAL
    {
        public static DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 初始化会员资产信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int SetDefaultMemberCapitalDetail(MemberCapitalDetailModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MemberCapitalDetail(");
            strSql.Append("MemberID,MemberName,MemberCode,GameCurrency,SharesCurrency,ShoppingCurrency,MemberPoints,CompoundCurrency");
            strSql.Append(") values (");
            strSql.Append("@MemberID,@MemberName,@MemberCode,0,0,0,0,0");
            strSql.Append(") ");
            SqlParameter[] parameters = {
			            new SqlParameter("@MemberID", SqlDbType.Int) ,            
                        new SqlParameter("@MemberName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberCode", SqlDbType.NVarChar) 
            };
            parameters[0].Value = model.MemberID;
            parameters[1].Value = model.MemberName;
            parameters[2].Value = model.MemberCode;
            return helper.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 添加会员资产信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddNewMemberCapitalDetail(MemberCapitalDetailModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MemberCapitalDetail(");
            strSql.Append("MemberID,MemberName,MemberCode,GameCurrency,SharesCurrency,ShoppingCurrency,MemberPoints,CompoundCurrency");
            strSql.Append(") values (");
            strSql.Append("@MemberID,@MemberName,@MemberCode,@GameCurrency,@SharesCurrency,@ShoppingCurrency,@MemberPoints,@CompoundCurrency");
            strSql.Append(") ");
            SqlParameter[] parameters = {
			            new SqlParameter("@MemberID", SqlDbType.Int) ,            
                        new SqlParameter("@MemberName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@GameCurrency", SqlDbType.Decimal) ,            
                        new SqlParameter("@SharesCurrency", SqlDbType.Decimal) ,            
                        new SqlParameter("@ShoppingCurrency", SqlDbType.Decimal) ,            
                        new SqlParameter("@MemberPoints", SqlDbType.Decimal) ,            
                        new SqlParameter("@CompoundCurrency", SqlDbType.Decimal) 
            };
            parameters[0].Value = model.MemberID;
            parameters[1].Value = model.MemberName;
            parameters[2].Value = model.MemberCode;
            parameters[3].Value = model.GameCurrency;
            parameters[4].Value = model.SharesCurrency;
            parameters[5].Value = model.ShoppingCurrency;
            parameters[6].Value = model.MemberPoints;
            parameters[7].Value = model.CompoundCurrency;
            return helper.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更改会员的游戏币和复利币数量
        /// </summary>
        /// <param name="gamecurrency">游戏币数量</param>
        /// <param name="compoundcurrency">复利币数量</param>
        /// <param name="remark">变动日志</param>
        /// <param name="memberid">会员ID</param>
        /// <returns></returns>
        public static int UpdateCompoundCurrencyAndGameCurrency(decimal gamecurrency, decimal compoundcurrency, string remark, int memberid)
        {
            string sqltxt = @"UPDATE  MemberCapitalDetail
SET     CompoundCurrency = CompoundCurrency + @CompoundCurrency ,
        GameCurrency = GameCurrency + @GameCurrency
OUTPUT  DELETED.MemberID ,
        DELETED.MemberName ,
        DELETED.MemberCode ,
        DELETED.GameCurrency ,
        INSERTED.GameCurrency ,
        DELETED.CompoundCurrency ,
        INSERTED.CompoundCurrency ,
        @remark ,
        GETDATE()
        INTO MemberCapitalLog ( MemberID, MemberName, MemberCode,
                                BGameCurrency, NGameCurrency,
                                BCompoundCurrency, NCompoundCurrency,
                                LogRemark, AddTime )
WHERE   MemberID = @memberid";
            SqlParameter[] paramter = { 
                                          new SqlParameter("@CompoundCurrency",compoundcurrency),
                                          new SqlParameter("@GameCurrency",gamecurrency),
                                          new SqlParameter("@remark",remark),
                                          new SqlParameter("@memberid",memberid)
                                    };
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 更改会员的积分数量
        /// </summary>
        /// <param name="compoundcurrency">积分数量</param>
        /// <param name="remark">变动日志</param>
        /// <param name="memberid">会员ID</param>
        /// <returns></returns>
        public static int UpdateMemberPoints(decimal memberpoints, string remark, int memberid)
        {
            string sqltxt = @"UPDATE  MemberCapitalDetail
SET     MemberPoints = MemberPoints + @MemberPoints
OUTPUT  DELETED.MemberID ,
        DELETED.MemberName ,
        DELETED.MemberCode ,
        DELETED.MemberPoints ,
        INSERTED.MemberPoints ,
        @remark ,
        GETDATE()
        INTO MemberCapitalLog ( MemberID, MemberName, MemberCode,
                                BMemberPoints, NMemberPoints,
                                LogRemark, AddTime )
WHERE   MemberID = @memberid";
            SqlParameter[] paramter = { 
                                          new SqlParameter("@MemberPoints",memberpoints),
                                          new SqlParameter("@remark",remark),
                                          new SqlParameter("@memberid",memberid)
                                    };
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 修改会员各项资产信息
        /// </summary>
        /// <param name="SharesCurrency">股权币</param>
        /// <param name="ShoppingCurrency">购物币</param>
        /// <param name="CompoundCurrency">复利币</param>
        /// <param name="GameCurrency">游戏币</param>
        /// <param name="MemberPoints">会员积分</param>
        /// <param name="remark">操作备注</param>
        /// <param name="memberid">会员ID</param>
        /// <returns></returns>
        public static int UpdateAllCapitalDetail(decimal sharescurrency, decimal shoppingcurrency, decimal compoundcurrency, decimal gamecurrency, decimal memberpoints, string remark, int memberid)
        {
            string sqltxt = @"UPDATE  MemberCapitalDetail
SET     SharesCurrency = SharesCurrency + @SharesCurrency ,
        ShoppingCurrency = ShoppingCurrency + @ShoppingCurrency ,
        CompoundCurrency = CompoundCurrency + @CompoundCurrency ,
        GameCurrency = GameCurrency + @GameCurrency ,
        MemberPoints = MemberPoints + @MemberPoints
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
WHERE   MemberID = @memberid";
            SqlParameter[] paramter = { 
                                          new SqlParameter("@SharesCurrency",sharescurrency),
                                          new SqlParameter("@ShoppingCurrency",shoppingcurrency),
                                          new SqlParameter("@CompoundCurrency",compoundcurrency),
                                          new SqlParameter("@GameCurrency",gamecurrency),
                                          new SqlParameter("@MemberPoints",memberpoints),
                                          new SqlParameter("@remark",remark),
                                          new SqlParameter("@memberid",memberid)
                                    };
            return helper.ExecuteSql(sqltxt, paramter);
        }
    }
}

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
        /// <param name="remark">变动日志描述</param>
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
        /// <param name="memberpoints">积分数量</param>
        /// <param name="remark">变动日志描述</param>
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
        /// 更改会员的游戏币数量
        /// </summary>
        /// <param name="gamecurrency">游戏币数量</param>
        /// <param name="remark">变动日志描述</param>
        /// <param name="memberid">会员ID</param>
        /// <returns></returns>
        public static int UpdateGameCurrency(decimal gamecurrency, string remark, int memberid)
        {
            string sqltxt = @"UPDATE  MemberCapitalDetail
SET     GameCurrency = GameCurrency + @GameCurrency
OUTPUT  DELETED.MemberID ,
        DELETED.MemberName ,
        DELETED.MemberCode ,
        DELETED.GameCurrency ,
        INSERTED.GameCurrency ,
        @remark ,
        GETDATE()
        INTO MemberCapitalLog ( MemberID, MemberName, MemberCode,
                                BGameCurrency, NGameCurrency,
                                LogRemark, AddTime )
WHERE   MemberID = @memberid";
            SqlParameter[] paramter = { 
                                          new SqlParameter("@GameCurrency",gamecurrency),
                                          new SqlParameter("@remark",remark),
                                          new SqlParameter("@memberid",memberid)
                                    };
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 更改会员的股权币数量
        /// </summary>
        /// <param name="sharescurrency">股权币数量</param>
        /// <param name="remark">变动日志描述</param>
        /// <param name="memberid">会员ID</param>
        /// <returns></returns>
        public static int UpdateSharesCurrency(decimal sharescurrency, string remark, int memberid)
        {
            string sqltxt = @"UPDATE  MemberCapitalDetail
SET     SharesCurrency = SharesCurrency + @SharesCurrency
OUTPUT  DELETED.MemberID ,
        DELETED.MemberName ,
        DELETED.MemberCode ,
        DELETED.SharesCurrency ,
        INSERTED.SharesCurrency ,
        @remark ,
        GETDATE()
        INTO MemberCapitalLog ( MemberID, MemberName, MemberCode,
                                BSharesCurrency, NSharesCurrency,
                                LogRemark, AddTime )
WHERE   MemberID = @memberid";
            SqlParameter[] paramter = { 
                                          new SqlParameter("@SharesCurrency",sharescurrency),
                                          new SqlParameter("@remark",remark),
                                          new SqlParameter("@memberid",memberid)
                                    };
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 更改会员的购物币数量
        /// </summary>
        /// <param name="shoppingcurrency">购物币数量</param>
        /// <param name="remark">变动日志描述</param>
        /// <param name="memberid">会员ID</param>
        /// <returns></returns>
        public static int UpdateShoppingCurrency(decimal shoppingcurrency, string remark, int memberid)
        {
            string sqltxt = @"UPDATE  MemberCapitalDetail
SET     ShoppingCurrency = ShoppingCurrency + @ShoppingCurrency
OUTPUT  DELETED.MemberID ,
        DELETED.MemberName ,
        DELETED.MemberCode ,
        DELETED.ShoppingCurrency ,
        INSERTED.ShoppingCurrency ,
        @remark ,
        GETDATE()
        INTO MemberCapitalLog ( MemberID, MemberName, MemberCode,
                                BShoppingCurrency, NShoppingCurrency,
                                LogRemark, AddTime )
WHERE   MemberID = @memberid";
            SqlParameter[] paramter = { 
                                          new SqlParameter("@ShoppingCurrency",shoppingcurrency),
                                          new SqlParameter("@remark",remark),
                                          new SqlParameter("@memberid",memberid)
                                    };
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 更改会员的复利币数量
        /// </summary>
        /// <param name="compoundcurrency">复利币数量</param>
        /// <param name="remark">变动日志描述</param>
        /// <param name="memberid">会员ID</param>
        /// <returns></returns>
        public static int UpdateCompoundCurrency(decimal compoundcurrency, string remark, int memberid)
        {
            string sqltxt = @"UPDATE  MemberCapitalDetail
SET     CompoundCurrency = CompoundCurrency + @CompoundCurrency
OUTPUT  DELETED.MemberID ,
        DELETED.MemberName ,
        DELETED.MemberCode ,
        DELETED.CompoundCurrency ,
        INSERTED.CompoundCurrency ,
        @remark ,
        GETDATE()
        INTO MemberCapitalLog ( MemberID, MemberName, MemberCode,
                                BCompoundCurrency, NCompoundCurrency,
                                LogRemark, AddTime )
WHERE   MemberID = @memberid";
            SqlParameter[] paramter = { 
                                          new SqlParameter("@CompoundCurrency",compoundcurrency),
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
        /// <param name="remark">变动日志描述</param>
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
        /// <summary>
        /// 根据会员ID查询会员的资产信息
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public static MemberCapitalDetailModel GetMemberCapitalDetailByMemberID(int memberID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  MemberID, MemberName, MemberCode, GameCurrency, SharesCurrency, ShoppingCurrency, MemberPoints, CompoundCurrency  ");
            strSql.Append("  from MemberCapitalDetail ");
            strSql.Append(" where MemberID=@MemberID");
            SqlParameter[] parameters = {
					new SqlParameter("@MemberID", SqlDbType.Int)
			};
            parameters[0].Value = memberID;
            MemberCapitalDetailModel model = new MemberCapitalDetailModel();
            DataSet ds = helper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.MemberID = ds.Tables[0].Rows[0]["MemberID"].ToString().ParseToInt(0);
                model.MemberName = ds.Tables[0].Rows[0]["MemberName"].ToString();
                model.MemberCode = ds.Tables[0].Rows[0]["MemberCode"].ToString();
                model.GameCurrency = ds.Tables[0].Rows[0]["GameCurrency"].ToString().ParseToDecimal(0);
                model.SharesCurrency = ds.Tables[0].Rows[0]["SharesCurrency"].ToString().ParseToDecimal(0);
                model.ShoppingCurrency = ds.Tables[0].Rows[0]["ShoppingCurrency"].ToString().ParseToDecimal(0);
                model.MemberPoints = ds.Tables[0].Rows[0]["MemberPoints"].ToString().ParseToDecimal(0);
                model.CompoundCurrency = ds.Tables[0].Rows[0]["CompoundCurrency"].ToString().ParseToDecimal(0);
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 分页查询会员的资产信息
        /// </summary>
        /// <param name="searchmodel"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public static List<MemberCapitalDetailModel> GetMemberCapitalDetailForPage(MemberCapitalDetailModel searchmodel, out int totalrowcount)
        {
            List<MemberCapitalDetailModel> list = new List<MemberCapitalDetailModel>();
            string columms = @" ID,MemberID,MemberName,MemberCode,GameCurrency,SharesCurrency,ShoppingCurrency,MemberPoints,CompoundCurrency ";
            string where = "";
            if (searchmodel != null)
            {
                //名字
                if (!string.IsNullOrWhiteSpace(searchmodel.MemberName) && string.IsNullOrWhiteSpace(where))
                {
                    where += @" MemberName Like '%" + searchmodel.MemberName + "%'";
                }
                else if (!string.IsNullOrWhiteSpace(searchmodel.MemberName) && !string.IsNullOrWhiteSpace(where))
                {
                    where += @" AND MemberName Like '%" + searchmodel.MemberName + "%'";
                }
                //编号
                if (!string.IsNullOrWhiteSpace(searchmodel.MemberCode) && string.IsNullOrWhiteSpace(where))
                {
                    where += @" MemberCode ='" + searchmodel.MemberCode + "'";
                }
                else if (!string.IsNullOrWhiteSpace(searchmodel.MemberCode) && !string.IsNullOrWhiteSpace(where))
                {
                    where += @" AND MemberCode ='" + searchmodel.MemberCode + "'";
                }
            }
            PageProModel page = new PageProModel();
            page.colums = columms;
            page.orderby = "ID";
            page.pageindex = searchmodel.PageIndex;
            page.pagesize = searchmodel.PageSize;
            page.tablename = @"dbo.MemberCapitalDetail";
            page.where = where;
            DataTable dt = PublicHelperDAL.GetTable(page, out totalrowcount);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    MemberCapitalDetailModel model = new MemberCapitalDetailModel();
                    model.ID = item["ID"].ToString().ParseToInt(0);
                    model.MemberID = item["MemberID"].ToString().ParseToInt(0);
                    model.MemberName = item["MemberName"].ToString();
                    model.MemberCode = item["MemberCode"].ToString();
                    model.GameCurrency = item["GameCurrency"].ToString().ParseToDecimal(0);
                    model.SharesCurrency = item["SharesCurrency"].ToString().ParseToDecimal(0);
                    model.ShoppingCurrency = item["ShoppingCurrency"].ToString().ParseToDecimal(0);
                    model.MemberPoints = item["MemberPoints"].ToString().ParseToDecimal(0);
                    model.CompoundCurrency = item["CompoundCurrency"].ToString().ParseToDecimal(0);
                    list.Add(model);
                }
                return list;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 扣减平台管理费
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public static int UpdateMemberISDeSysCost(int memberid, decimal syscost)
        {
            string sqltxt = @"UPDATE  dbo.MemberCapitalDetail
SET     ISDeSysCost = 1 ,
        MemberPoints = MemberPoints - @syscost
OUTPUT  DELETED.MemberID ,
        DELETED.MemberName ,
        DELETED.MemberCode ,
        DELETED.MemberPoints ,
        INSERTED.MemberPoints ,
        @remark ,
        GETDATE()
        INTO MemberCapitalLog ( MemberID, MemberName, MemberCode,
                                BMemberPoints, NMemberPoints, LogRemark,
                                AddTime )
WHERE   MemberID = @mid
        AND MemberPoints > 30
        AND ISNULL(ISDeSysCost, 0) = 0";
            SqlParameter[] paramter = { 
                                          new SqlParameter("@syscost",syscost),
                                          new SqlParameter("@remark","扣减平台管理费用"),
                                          new SqlParameter("@mid",memberid)
                                      };
            return helper.ExecuteSql(sqltxt);
        }
    }
}

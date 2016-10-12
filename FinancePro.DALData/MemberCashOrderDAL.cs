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
    /// 提现单据管理
    /// </summary>
    public class MemberCashOrderDAL
    {
        public static DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 提交新的提现申请
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddNewMemberCashOrder(MemberCashOrderModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MemberCashOrder(");
            strSql.Append("CashOrderCode,CashBankCode,MemberID,MemberName,MemberCode,CashNum,FinishCashNum,CStatus,AddTime,CashBankName,CommissionNum,CashBankUserName");
            strSql.Append(") values (");
            strSql.Append("@CashOrderCode,@CashBankCode,@MemberID,@MemberName,@MemberCode,@CashNum,@FinishCashNum,1,GETDATE(),@CashBankName,@CommissionNum,@CashBankUserName");
            strSql.Append(") ");
            SqlParameter[] parameters = {
			            new SqlParameter("@CashOrderCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@CashBankCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberID", SqlDbType.Int) ,            
                        new SqlParameter("@MemberName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@CashNum", SqlDbType.Decimal) ,            
                        new SqlParameter("@FinishCashNum", SqlDbType.Decimal) ,         
                        new SqlParameter("@CashBankName", SqlDbType.NVarChar),
                        new SqlParameter("@CommissionNum",SqlDbType.Decimal),
                        new SqlParameter("@CashBankUserName",SqlDbType.NVarChar)
            };
            parameters[0].Value = model.CashOrderCode;
            parameters[1].Value = model.CashBankCode;
            parameters[2].Value = model.MemberID;
            parameters[3].Value = model.MemberName;
            parameters[4].Value = model.MemberCode;
            parameters[5].Value = model.CashNum;
            parameters[6].Value = model.FinishCashNum;
            parameters[7].Value = model.CashBankName;
            parameters[8].Value = model.CommissionNum;
            parameters[9].Value = model.CashBankUserName;
            return helper.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更改提现申请状态
        /// </summary>
        /// <param name="orderid">单据ID</param>
        /// <param name="status">状态值</param>
        /// <returns></returns>
        public static int UpdateMemberCashOrderStatus(int orderid, int status)
        {
            string sqltxt = @"UPDATE  MemberCashOrder
SET     CStatus = @status
WHERE   ID = @id";
            SqlParameter[] paramter = { new SqlParameter("@id", orderid), new SqlParameter("@status", status) };
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 按照分页查询提现单据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public static List<MemberCashOrderModel> GetAllMemberCashByPage(MemberCashOrderModel model, out int totalrowcount)
        {
            List<MemberCashOrderModel> list = new List<MemberCashOrderModel>();
            string columms = @"ID ,CashOrderCode ,MemberID ,MemberName ,MemberCode ,CashNum ,FinishCashNum ,CStatus ,CASE CStatus WHEN 1 THEN '新申请'  WHEN 2 THEN '已打款'  WHEN 3 THEN '已驳回' END AS CStatusName , AddTime , CashBankName ,CashBankCode,CashBankUserName,DATEDIFF(DAY,AddTime,GETDATE()) diffday";
            string where = "";
            if (model != null)
            {
                if (model.CStatus != 0)
                {
                    where += "CStatus=" + model.CStatus + "";
                }
                if (!string.IsNullOrWhiteSpace(model.MemberName) && string.IsNullOrWhiteSpace(where))
                {
                    where += @" MemberName Like '%" + model.MemberName + "%'";
                }
                else if (!string.IsNullOrWhiteSpace(model.MemberName) && !string.IsNullOrWhiteSpace(where))
                {
                    where += @" AND MemberName Like '%" + model.MemberName + "%'";
                }
                if (!string.IsNullOrWhiteSpace(model.MemberCode) && string.IsNullOrWhiteSpace(where))
                {
                    where += @" MemberCode ='" + model.MemberCode + "'";
                }
                else if (!string.IsNullOrWhiteSpace(model.MemberCode) && !string.IsNullOrWhiteSpace(where))
                {
                    where += @" AND MemberCode ='" + model.MemberCode + "'";
                }
            }
            PageProModel page = new PageProModel();
            page.colums = columms;
            page.orderby = "AddTime";
            page.pageindex = model.PageIndex;
            page.pagesize = model.PageSize;
            page.tablename = @"dbo.MemberCashOrder";
            page.where = where;
            DataTable dt = PublicHelperDAL.GetTable(page, out totalrowcount);
            foreach (DataRow item in dt.Rows)
            {
                MemberCashOrderModel membercashordermodel = new MemberCashOrderModel();
                if (item["ID"].ToString() != "")
                {
                    membercashordermodel.ID = int.Parse(item["ID"].ToString());
                }
                membercashordermodel.CashBankName = item["CashBankName"].ToString();
                membercashordermodel.CashBankCode = item["CashBankCode"].ToString();
                membercashordermodel.CashOrderCode = item["CashOrderCode"].ToString();
                if (item["MemberID"].ToString() != "")
                {
                    membercashordermodel.MemberID = int.Parse(item["MemberID"].ToString());
                }
                membercashordermodel.MemberName = item["MemberName"].ToString();
                membercashordermodel.MemberCode = item["MemberCode"].ToString();
                if (item["CashNum"].ToString() != "")
                {
                    membercashordermodel.CashNum = decimal.Parse(item["CashNum"].ToString());
                }
                if (item["FinishCashNum"].ToString() != "")
                {
                    membercashordermodel.FinishCashNum = decimal.Parse(item["FinishCashNum"].ToString());
                }
                if (item["CStatus"].ToString() != "")
                {
                    membercashordermodel.CStatus = int.Parse(item["CStatus"].ToString());
                }
                if (item["AddTime"].ToString() != "")
                {
                    membercashordermodel.AddTime = DateTime.Parse(item["AddTime"].ToString());
                }
                membercashordermodel.CashBankUserName = item["CashBankUserName"].ToString();
                membercashordermodel.CStatusName = item["CStatusName"].ToString();
                membercashordermodel.DiffDay = item["diffday"].ToString().ParseToInt(0);
                list.Add(membercashordermodel);
            }
            return list;
        }
        /// <summary>
        /// 按照会员ID查询上次提现信息
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public static MemberCashOrderModel GetLastMemberCashOrderByMemberId(int memberid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  TOP 1 ID, CashBankName, CashBankCode, CashOrderCode, MemberID, MemberName, MemberCode, CashNum, FinishCashNum, CStatus, AddTime,DATEDIFF(DAY, AddTime, GETDATE()) diffDay  ");
            strSql.Append("  from MemberCashOrder ");
            strSql.Append(" where MemberID = @memberid AND CStatus <> 3 ORDER BY ID DESC");
            SqlParameter[] parameters = {
					new SqlParameter("@memberid", SqlDbType.Int)
			};
            parameters[0].Value = memberid;
            MemberCashOrderModel model = new MemberCashOrderModel();
            DataSet ds = helper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.ID = ds.Tables[0].Rows[0]["ID"].ToString().ParseToInt(0);
                model.CashBankName = ds.Tables[0].Rows[0]["CashBankName"].ToString();
                model.CashBankCode = ds.Tables[0].Rows[0]["CashBankCode"].ToString();
                model.CashOrderCode = ds.Tables[0].Rows[0]["CashOrderCode"].ToString();
                model.MemberID = ds.Tables[0].Rows[0]["MemberID"].ToString().ParseToInt(0);
                model.MemberName = ds.Tables[0].Rows[0]["MemberName"].ToString();
                model.MemberCode = ds.Tables[0].Rows[0]["MemberCode"].ToString();
                model.CashNum = decimal.Parse(ds.Tables[0].Rows[0]["CashNum"].ToString());
                model.FinishCashNum = ds.Tables[0].Rows[0]["FinishCashNum"].ToString().ParseToDecimal(0);
                model.CStatus = int.Parse(ds.Tables[0].Rows[0]["CStatus"].ToString());
                model.AddTime = ds.Tables[0].Rows[0]["AddTime"].ToString().ParseToDateTime(DateTime.MinValue);
                model.DiffDay = ds.Tables[0].Rows[0]["diffDay"].ToString().ParseToInt(0);
                return model;
            }
            else
            {
                return null;
            }
        }
    }
}

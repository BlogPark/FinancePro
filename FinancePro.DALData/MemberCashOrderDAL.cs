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
            strSql.Append("CashOrderCode,CashBankCode,MemberID,MemberName,MemberCode,CashNum,FinishCashNum,CStatus,AddTime,CashBankName");
            strSql.Append(") values (");
            strSql.Append("@CashOrderCode,@CashBankCode,@MemberID,@MemberName,@MemberCode,@CashNum,@FinishCashNum,1,GETDATE(),@CashBankName");
            strSql.Append(") ");
            SqlParameter[] parameters = {
			            new SqlParameter("@CashOrderCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@CashBankCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberID", SqlDbType.Int) ,            
                        new SqlParameter("@MemberName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@CashNum", SqlDbType.Decimal) ,            
                        new SqlParameter("@FinishCashNum", SqlDbType.Decimal) ,         
                        new SqlParameter("@CashBankName", SqlDbType.NVarChar)
            };
            parameters[0].Value = model.CashOrderCode;
            parameters[1].Value = model.CashBankCode;
            parameters[2].Value = model.MemberID;
            parameters[3].Value = model.MemberName;
            parameters[4].Value = model.MemberCode;
            parameters[5].Value = model.CashNum;
            parameters[6].Value = model.FinishCashNum;
            parameters[7].Value = model.CashBankName;
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
        public static List<MemberCashOrderModel> GetAllMemberCashByPage(MemberCashOrderModel model,out int totalrowcount)
        {
            List<MemberCashOrderModel> list = new List<MemberCashOrderModel>();
            string columms = @"ID ,CashOrderCode ,MemberID ,MemberName ,MemberCode ,CashNum ,FinishCashNum ,CStatus ,CASE CStatus WHEN 1 THEN '新申请'  WHEN 2 THEN '已打款'  WHEN 3 THEN '已驳回' END AS CStatusName , AddTime , CashBankName ,CashBankCode";
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
                list.Add(membercashordermodel);
            }
            return list;
        }
    }
}

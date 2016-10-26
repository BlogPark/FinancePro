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
    /// 会员转账记录
    /// </summary>
    public class MemberTransferOrderDAL
    {
        public static DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 添加会员转账记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddNewMemberTransferOrder(MemberTransferOrderModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MemberTransferOrder(");
            strSql.Append("AddTime,TransferResult,LaunchMemberID,LaunchMemberCode,ReceiveMemberID,ReceiveMemberCode,TransferType,TransferNumber,TransferRemark,CounterFee");
            strSql.Append(") values (");
            strSql.Append("@AddTime,@TransferResult,@LaunchMemberID,@LaunchMemberCode,@ReceiveMemberID,@ReceiveMemberCode,@TransferType,@TransferNumber,@TransferRemark,@CounterFee");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@AddTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@TransferResult", SqlDbType.Int) ,            
                        new SqlParameter("@LaunchMemberID", SqlDbType.Int) ,            
                        new SqlParameter("@LaunchMemberCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@ReceiveMemberID", SqlDbType.Int) ,            
                        new SqlParameter("@ReceiveMemberCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@TransferType", SqlDbType.Int) ,            
                        new SqlParameter("@TransferNumber", SqlDbType.Decimal) ,            
                        new SqlParameter("@TransferRemark", SqlDbType.NVarChar) ,            
                        new SqlParameter("@CounterFee", SqlDbType.Decimal)  
            };
            parameters[0].Value = model.AddTime;
            parameters[1].Value = model.TransferResult;
            parameters[2].Value = model.LaunchMemberID;
            parameters[3].Value = model.LaunchMemberCode;
            parameters[4].Value = model.ReceiveMemberID;
            parameters[5].Value = model.ReceiveMemberCode;
            parameters[6].Value = model.TransferType;
            parameters[7].Value = model.TransferNumber;
            parameters[8].Value = model.TransferRemark;
            parameters[9].Value = model.CounterFee;

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
        /// 按照ID查询单独记录
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static MemberTransferOrderModel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, AddTime, TransferResult, LaunchMemberID, LaunchMemberCode, ReceiveMemberID, ReceiveMemberCode, TransferType, TransferNumber, TransferRemark, CounterFee  ");
            strSql.Append("  from MemberTransferOrder ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int)
			};
            parameters[0].Value = ID;
            MemberTransferOrderModel model = new MemberTransferOrderModel();
            DataSet ds = helper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TransferResult"].ToString() != "")
                {
                    model.TransferResult = int.Parse(ds.Tables[0].Rows[0]["TransferResult"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LaunchMemberID"].ToString() != "")
                {
                    model.LaunchMemberID = int.Parse(ds.Tables[0].Rows[0]["LaunchMemberID"].ToString());
                }
                model.LaunchMemberCode = ds.Tables[0].Rows[0]["LaunchMemberCode"].ToString();
                if (ds.Tables[0].Rows[0]["ReceiveMemberID"].ToString() != "")
                {
                    model.ReceiveMemberID = int.Parse(ds.Tables[0].Rows[0]["ReceiveMemberID"].ToString());
                }
                model.ReceiveMemberCode = ds.Tables[0].Rows[0]["ReceiveMemberCode"].ToString();
                if (ds.Tables[0].Rows[0]["TransferType"].ToString() != "")
                {
                    model.TransferType = int.Parse(ds.Tables[0].Rows[0]["TransferType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TransferNumber"].ToString() != "")
                {
                    model.TransferNumber = decimal.Parse(ds.Tables[0].Rows[0]["TransferNumber"].ToString());
                }
                model.TransferRemark = ds.Tables[0].Rows[0]["TransferRemark"].ToString();
                if (ds.Tables[0].Rows[0]["CounterFee"].ToString() != "")
                {
                    model.CounterFee = decimal.Parse(ds.Tables[0].Rows[0]["CounterFee"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据发起人查询转账记录
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public static List<MemberTransferOrderModel> GetMemberTransferByLaunchMemberID(int memberid, int pageindex, int pagesize, out int totalrowcount)
        {
            List<MemberTransferOrderModel> list = new List<MemberTransferOrderModel>();
            string columms = @"ID ,LaunchMemberID ,LaunchMemberCode ,ReceiveMemberID ,ReceiveMemberCode ,TransferType,CASE TransferType WHEN 1 THEN '报单币' WHEN 2 THEN '积分' WHEN 3 THEN '购物币' WHEN 4 THEN '股权币' WHEN 5 THEN '复利币' WHEN 6 THEN '游戏币' END AS TransferTypeName ,TransferNumber ,TransferRemark ,CounterFee ,AddTime ,TransferResult";
            string where = " LaunchMemberID =" + memberid;
             PageProModel page = new PageProModel();
            page.colums = columms;
            page.orderby = "AddTime";
            page.pageindex = pageindex;
            page.pagesize = pagesize;
            page.tablename = @"dbo.MemberTransferOrder";
            page.where = where;
            DataTable dt = PublicHelperDAL.GetTable(page, out totalrowcount);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    MemberTransferOrderModel model = new MemberTransferOrderModel();
                    if (item["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(item["ID"].ToString());
                    }
                    if (item["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(item["AddTime"].ToString());
                    }
                    if (item["TransferResult"].ToString() != "")
                    {
                        model.TransferResult = int.Parse(item["TransferResult"].ToString());
                    }
                    if (item["LaunchMemberID"].ToString() != "")
                    {
                        model.LaunchMemberID = int.Parse(item["LaunchMemberID"].ToString());
                    }
                    model.LaunchMemberCode = item["LaunchMemberCode"].ToString();
                    if (item["ReceiveMemberID"].ToString() != "")
                    {
                        model.ReceiveMemberID = int.Parse(item["ReceiveMemberID"].ToString());
                    }
                    model.ReceiveMemberCode = item["ReceiveMemberCode"].ToString();
                    if (item["TransferType"].ToString() != "")
                    {
                        model.TransferType = int.Parse(item["TransferType"].ToString());
                    }
                    if (item["TransferNumber"].ToString() != "")
                    {
                        model.TransferNumber = decimal.Parse(item["TransferNumber"].ToString());
                    }
                    model.TransferRemark = item["TransferRemark"].ToString();
                    if (item["CounterFee"].ToString() != "")
                    {
                        model.CounterFee = decimal.Parse(item["CounterFee"].ToString());
                    }
                    model.TransferTypeName = item["TransferTypeName"].ToString();
                }
            }
            return list;
        }
    }
}

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
    /// 会员资金变动记录操作类
    /// </summary>
    public class MemberCapitalLogDAL
    {
        public static DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 添加新的日志记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddNewMemberCapitalLog(MemberCapitalLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MemberCapitalLog(");
            strSql.Append("NSharesCurrency,BShoppingCurrency,NShoppingCurrency,BCompoundCurrency,NCompoundCurrency,LogRemark,AddTime,MemberID,MemberName,MemberCode,BMemberPoints,NMemberPoints,BGameCurrency,NGameCurrency,BSharesCurrency");
            strSql.Append(") values (");
            strSql.Append("@NSharesCurrency,@BShoppingCurrency,@NShoppingCurrency,@BCompoundCurrency,@NCompoundCurrency,@LogRemark,@AddTime,@MemberID,@MemberName,@MemberCode,@BMemberPoints,@NMemberPoints,@BGameCurrency,@NGameCurrency,@BSharesCurrency");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@NSharesCurrency", SqlDbType.Int) ,            
                        new SqlParameter("@BShoppingCurrency", SqlDbType.Int) ,            
                        new SqlParameter("@NShoppingCurrency", SqlDbType.Int) ,            
                        new SqlParameter("@BCompoundCurrency", SqlDbType.Int) ,            
                        new SqlParameter("@NCompoundCurrency", SqlDbType.Int) ,            
                        new SqlParameter("@LogRemark", SqlDbType.NVarChar) ,            
                        new SqlParameter("@AddTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@MemberID", SqlDbType.Int) ,            
                        new SqlParameter("@MemberName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@BMemberPoints", SqlDbType.Int) ,            
                        new SqlParameter("@NMemberPoints", SqlDbType.Int) ,            
                        new SqlParameter("@BGameCurrency", SqlDbType.Int) ,            
                        new SqlParameter("@NGameCurrency", SqlDbType.Int) ,            
                        new SqlParameter("@BSharesCurrency", SqlDbType.Int)             
              
            };

            parameters[0].Value = model.NSharesCurrency;
            parameters[1].Value = model.BShoppingCurrency;
            parameters[2].Value = model.NShoppingCurrency;
            parameters[3].Value = model.BCompoundCurrency;
            parameters[4].Value = model.NCompoundCurrency;
            parameters[5].Value = model.LogRemark;
            parameters[6].Value = model.AddTime;
            parameters[7].Value = model.MemberID;
            parameters[8].Value = model.MemberName;
            parameters[9].Value = model.MemberCode;
            parameters[10].Value = model.BMemberPoints;
            parameters[11].Value = model.NMemberPoints;
            parameters[12].Value = model.BGameCurrency;
            parameters[13].Value = model.NGameCurrency;
            parameters[14].Value = model.BSharesCurrency;
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
        /// 按照会员ID读取会员资金操作记录
        /// </summary>
        /// <param name="memberid">会员ID</param>
        /// <param name="pageindex">页面数</param>
        /// <param name="pagesize">页容量</param>
        /// <returns></returns>
        public static List<MemberCapitalLogModel> GetMemberCapitalLogByMemberID(int memberid, int pageindex, int pagesize, out int totalrowcount)
        {
            List<MemberCapitalLogModel> list = new List<MemberCapitalLogModel>();
            string columms = @"ID, NSharesCurrency, BShoppingCurrency, NShoppingCurrency, BCompoundCurrency, NCompoundCurrency, LogRemark, AddTime, MemberID, MemberName, MemberCode, BMemberPoints, NMemberPoints, BGameCurrency, NGameCurrency, BSharesCurrency";
            string where = "";
            where += "MemberID=" + memberid + "";
            PageProModel page = new PageProModel();
            page.colums = columms;
            page.orderby = "ID";
            page.pageindex = pageindex;
            page.pagesize = pagesize;
            page.tablename = @"dbo.MemberCapitalLog";
            page.where = where;
            DataTable dt = PublicHelperDAL.GetTable(page, out totalrowcount);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    MemberCapitalLogModel logmodel = new MemberCapitalLogModel();
                    if (item["ID"].ToString() != "")
                    {
                        logmodel.ID = int.Parse(item["ID"].ToString());
                    }
                    if (item["NSharesCurrency"].ToString() != "")
                    {
                        logmodel.NSharesCurrency = int.Parse(item["NSharesCurrency"].ToString());
                    }
                    if (item["BShoppingCurrency"].ToString() != "")
                    {
                        logmodel.BShoppingCurrency = int.Parse(item["BShoppingCurrency"].ToString());
                    }
                    if (item["NShoppingCurrency"].ToString() != "")
                    {
                        logmodel.NShoppingCurrency = int.Parse(item["NShoppingCurrency"].ToString());
                    }
                    if (item["BCompoundCurrency"].ToString() != "")
                    {
                        logmodel.BCompoundCurrency = int.Parse(item["BCompoundCurrency"].ToString());
                    }
                    if (item["NCompoundCurrency"].ToString() != "")
                    {
                        logmodel.NCompoundCurrency = int.Parse(item["NCompoundCurrency"].ToString());
                    }
                    logmodel.LogRemark = item["LogRemark"].ToString();
                    if (item["AddTime"].ToString() != "")
                    {
                        logmodel.AddTime = DateTime.Parse(item["AddTime"].ToString());
                    }
                    if (item["MemberID"].ToString() != "")
                    {
                        logmodel.MemberID = int.Parse(item["MemberID"].ToString());
                    }
                    logmodel.MemberName = item["MemberName"].ToString();
                    logmodel.MemberCode = item["MemberCode"].ToString();
                    if (item["BMemberPoints"].ToString() != "")
                    {
                        logmodel.BMemberPoints = int.Parse(item["BMemberPoints"].ToString());
                    }
                    if (item["NMemberPoints"].ToString() != "")
                    {
                        logmodel.NMemberPoints = int.Parse(item["NMemberPoints"].ToString());
                    }
                    if (item["BGameCurrency"].ToString() != "")
                    {
                        logmodel.BGameCurrency = int.Parse(item["BGameCurrency"].ToString());
                    }
                    if (item["NGameCurrency"].ToString() != "")
                    {
                        logmodel.NGameCurrency = int.Parse(item["NGameCurrency"].ToString());
                    }
                    if (item["BSharesCurrency"].ToString() != "")
                    {
                        logmodel.BSharesCurrency = int.Parse(item["BSharesCurrency"].ToString());
                    }
                    list.Add(logmodel);
                }
                return list;
            }
            else
            {
                return null;
            }
        }
    }
}

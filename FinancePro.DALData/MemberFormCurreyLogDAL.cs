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
    /// 会员报单币日志使用操作类
    /// </summary>
    public class MemberFormCurreyLogDAL
    {
        public static DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 添加新的日志记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddNewMemberFormCurreyLog(MemberFormCurreyLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MemberFormCurreyLog(");
            strSql.Append("MemberID,MemberName,MemberCode,BFormCurreyNum,NFormCurreyNum,Remark,AddTime");
            strSql.Append(") values (");
            strSql.Append("@MemberID,@MemberName,@MemberCode,@BFormCurreyNum,@NFormCurreyNum,@Remark,@AddTime");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@MemberID", SqlDbType.Int) ,            
                        new SqlParameter("@MemberName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@BFormCurreyNum", SqlDbType.Int) ,            
                        new SqlParameter("@NFormCurreyNum", SqlDbType.Int) ,            
                        new SqlParameter("@Remark", SqlDbType.NVarChar) ,            
                        new SqlParameter("@AddTime", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.MemberID;
            parameters[1].Value = model.MemberName;
            parameters[2].Value = model.MemberCode;
            parameters[3].Value = model.BFormCurreyNum;
            parameters[4].Value = model.NFormCurreyNum;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.AddTime;
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
        /// 得到会员报单币操作记录
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public static List<MemberFormCurreyLogModel> GetMemberFormCurreyByMemberID(int memberid, int pageindex, int pagesize, out int totalrowcount)
        {
            List<MemberFormCurreyLogModel> list = new List<MemberFormCurreyLogModel>();
            string columms = @"ID, MemberID, MemberName, MemberCode, BFormCurreyNum, NFormCurreyNum, Remark, AddTime";
            string where = "";
            where += "MemberID=" + memberid + "";
            PageProModel page = new PageProModel();
            page.colums = columms;
            page.orderby = "ID";
            page.pageindex = pageindex;
            page.pagesize = pagesize;
            page.tablename = @"dbo.MemberFormCurreyLog";
            page.where = where;
            DataTable dt = PublicHelperDAL.GetTable(page, out totalrowcount);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    MemberFormCurreyLogModel model = new MemberFormCurreyLogModel();
                    if (item["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(item["ID"].ToString());
                    }
                    if (item["MemberID"].ToString() != "")
                    {
                        model.MemberID = int.Parse(item["MemberID"].ToString());
                    }
                    model.MemberName = item["MemberName"].ToString();
                    model.MemberCode = item["MemberCode"].ToString();
                    if (item["BFormCurreyNum"].ToString() != "")
                    {
                        model.BFormCurreyNum = int.Parse(item["BFormCurreyNum"].ToString());
                    }
                    if (item["NFormCurreyNum"].ToString() != "")
                    {
                        model.NFormCurreyNum = int.Parse(item["NFormCurreyNum"].ToString());
                    }
                    model.Remark = item["Remark"].ToString();
                    if (item["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(item["AddTime"].ToString());
                    }
                    list.Add(model);
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

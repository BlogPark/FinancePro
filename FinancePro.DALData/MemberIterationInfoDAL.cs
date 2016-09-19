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
    public class MemberIterationInfoDAL
    {
        public static DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 添加会员的代数信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddNewMemberIterationInfo(MemberIterationInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MemberIterationInfo(");
            strSql.Append("RAddTime,MemberID,MemberName,MemberCode,SuperiorMemberID,SuperiorMemberName,SuperiorMemberCode,GenerationNum,RStatus");
            strSql.Append(") values (");
            strSql.Append("GETDATE(),@MemberID,@MemberName,@MemberCode,@SuperiorMemberID,@SuperiorMemberName,@SuperiorMemberCode,@GenerationNum,1");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {            
                        new SqlParameter("@MemberID", SqlDbType.Int) ,            
                        new SqlParameter("@MemberName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@SuperiorMemberID", SqlDbType.Int) ,            
                        new SqlParameter("@SuperiorMemberName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@SuperiorMemberCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@GenerationNum", SqlDbType.Int) 
            };
            parameters[0].Value = model.MemberID;
            parameters[1].Value = model.MemberName;
            parameters[2].Value = model.MemberCode;
            parameters[3].Value = model.SuperiorMemberID;
            parameters[4].Value = model.SuperiorMemberName;
            parameters[5].Value = model.SuperiorMemberCode;
            parameters[6].Value = model.GenerationNum;
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
        /// 添加会员的代数信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddNewMemberIterationInfo(List<MemberIterationInfoModel> listmodel)
        {
            int rowcount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MemberIterationInfo(");
            strSql.Append("RAddTime,MemberID,MemberName,MemberCode,SuperiorMemberID,SuperiorMemberName,SuperiorMemberCode,GenerationNum,RStatus");
            strSql.Append(") values (");
            strSql.Append("GETDATE(),@MemberID,@MemberName,@MemberCode,@SuperiorMemberID,@SuperiorMemberName,@SuperiorMemberCode,@GenerationNum,1");
            strSql.Append(") ");
            foreach (MemberIterationInfoModel item in listmodel)
            {
                SqlParameter[] parameters = {            
                        new SqlParameter("@MemberID", SqlDbType.Int) ,            
                        new SqlParameter("@MemberName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@SuperiorMemberID", SqlDbType.Int) ,            
                        new SqlParameter("@SuperiorMemberName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@SuperiorMemberCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@GenerationNum", SqlDbType.Int) 
                       };
                parameters[0].Value = item.MemberID;
                parameters[1].Value = item.MemberName;
                parameters[2].Value = item.MemberCode;
                parameters[3].Value = item.SuperiorMemberID;
                parameters[4].Value = item.SuperiorMemberName;
                parameters[5].Value = item.SuperiorMemberCode;
                parameters[6].Value = item.GenerationNum;
                rowcount += helper.ExecuteSql(strSql.ToString(), parameters);
            }
            return rowcount;
        }
        /// <summary>
        /// 查询会员*代以内所有会员
        /// </summary>
        /// <param name="memberid">会员ID</param>
        /// <param name="iterationnum">世代数</param>
        /// <returns></returns>
        public static List<MemberIterationInfoModel> GetMemberIterationInfoByMemberForIteration(int memberid, int iterationnum)
        {
            List<MemberIterationInfoModel> list = new List<MemberIterationInfoModel>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, RAddTime, MemberID, MemberName, MemberCode, SuperiorMemberID, SuperiorMemberName, SuperiorMemberCode, GenerationNum, RStatus  ");
            strSql.Append("  from MemberIterationInfo ");
            strSql.Append(" where SuperiorMemberID=@SuperiorMemberID AND GenerationNum<=@GenerationNum");
            SqlParameter[] parameters = {
					new SqlParameter("@SuperiorMemberID", SqlDbType.Int),
                    new SqlParameter("@GenerationNum",SqlDbType.Int)
			};
            parameters[0].Value = memberid;
            parameters[1].Value = iterationnum;
            MemberIterationInfoModel model = new MemberIterationInfoModel();
            DataTable dt = helper.Query(strSql.ToString(), parameters).Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    if (item["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(item["ID"].ToString());
                    }
                    if (item["RAddTime"].ToString() != "")
                    {
                        model.RAddTime = DateTime.Parse(item["RAddTime"].ToString());
                    }
                    if (item["MemberID"].ToString() != "")
                    {
                        model.MemberID = int.Parse(item["MemberID"].ToString());
                    }
                    model.MemberName = item["MemberName"].ToString();
                    model.MemberCode = item["MemberCode"].ToString();
                    if (item["SuperiorMemberID"].ToString() != "")
                    {
                        model.SuperiorMemberID = int.Parse(item["SuperiorMemberID"].ToString());
                    }
                    model.SuperiorMemberName = item["SuperiorMemberName"].ToString();
                    model.SuperiorMemberCode = item["SuperiorMemberCode"].ToString();
                    if (item["GenerationNum"].ToString() != "")
                    {
                        model.GenerationNum = int.Parse(item["GenerationNum"].ToString());
                    }
                    if (item["RStatus"].ToString() != "")
                    {
                        model.RStatus = int.Parse(item["RStatus"].ToString());
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

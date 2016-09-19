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
    public class ReMemberRelationDAL
    {
        public static DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 添加会员推荐信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddNewReMemberRelation(ReMemberRelationModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ReMemberRelation(");
            strSql.Append("RecommendMemberID,RecommendMemberName,RecommendMemberCode,BeRecommMemberID,BeRecommMemberName,BeRecommMemberCode,RecommendTime,RecommendStatus");
            strSql.Append(") values (");
            strSql.Append("@RecommendMemberID,@RecommendMemberName,@RecommendMemberCode,@BeRecommMemberID,@BeRecommMemberName,@BeRecommMemberCode,GETDATE(),1");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@RecommendMemberID", SqlDbType.Int) ,            
                        new SqlParameter("@RecommendMemberName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@RecommendMemberCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@BeRecommMemberID", SqlDbType.Int) ,            
                        new SqlParameter("@BeRecommMemberName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@BeRecommMemberCode", SqlDbType.NVarChar) 
            };
            parameters[0].Value = model.RecommendMemberID;
            parameters[1].Value = model.RecommendMemberName;
            parameters[2].Value = model.RecommendMemberCode;
            parameters[3].Value = model.BeRecommMemberID;
            parameters[4].Value = model.BeRecommMemberName;
            parameters[5].Value = model.BeRecommMemberCode;
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
        /// 更改会员的推荐关系状态
        /// </summary>
        /// <param name="rememberid">推荐人ID</param>
        /// <param name="memberid">被推荐人ID</param>
        /// <param name="status">状态值</param>
        /// <returns></returns>
        public static int UpdateReMemberRelationStatus(int rememberid, int memberid, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ReMemberRelation set ");
            strSql.Append(" RecommendStatus = @RecommendStatus  ");
            strSql.Append(" where RecommendMemberID = @RecommendMemberID AND BeRecommMemberID = @BeRecommMemberID ");
            SqlParameter[] parameters = {           
                        new SqlParameter("@RecommendMemberID", SqlDbType.Int) ,            
                        new SqlParameter("@BeRecommMemberID", SqlDbType.Int) ,            
                        new SqlParameter("@RecommendStatus", SqlDbType.Int)   
            };
            parameters[0].Value = rememberid;
            parameters[1].Value = memberid;
            parameters[2].Value = status;
            return helper.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 根据被推荐人查找推荐关系
        /// </summary>
        /// <param name="berecommendmemberid">被推荐人ID</param>
        /// <returns></returns>
        public static ReMemberRelationModel GetReMemberRelationByBeRecommMemberID(int berecommendmemberid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select RecommendMemberID, RecommendMemberName, RecommendMemberCode, BeRecommMemberID, BeRecommMemberName, BeRecommMemberCode ");
            strSql.Append("  from ReMemberRelation ");
            strSql.Append(" where BeRecommMemberID=@BeRecommMemberID AND RecommendStatus=1 ");
            SqlParameter[] parameters = {
					new SqlParameter("@BeRecommMemberID", SqlDbType.Int)
			};
            parameters[0].Value = berecommendmemberid;
            ReMemberRelationModel model = new ReMemberRelationModel();
            DataSet ds = helper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.RecommendMemberID = ds.Tables[0].Rows[0]["RecommendMemberID"].ToString().ParseToInt(0);
                model.RecommendMemberName = ds.Tables[0].Rows[0]["RecommendMemberName"].ToString();
                model.RecommendMemberCode = ds.Tables[0].Rows[0]["RecommendMemberCode"].ToString();
                model.BeRecommMemberID = ds.Tables[0].Rows[0]["BeRecommMemberID"].ToString().ParseToInt(0);
                model.BeRecommMemberName = ds.Tables[0].Rows[0]["BeRecommMemberName"].ToString();
                model.BeRecommMemberCode = ds.Tables[0].Rows[0]["BeRecommMemberCode"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据被推荐人查找推荐关系
        /// </summary>
        /// <param name="berecommendmemberid">被推荐人ID列表</param>
        /// <returns></returns>
        public static List<ReMemberRelationModel> GetReMemberRelationByBeRecommMemberID(string berecommendmemberid)
        {
            List<ReMemberRelationModel> list = new List<ReMemberRelationModel>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select RecommendMemberID, RecommendMemberName, RecommendMemberCode, BeRecommMemberID, BeRecommMemberName, BeRecommMemberCode ");
            strSql.Append("  from ReMemberRelation ");
            strSql.Append(" where BeRecommMemberID IN (" + berecommendmemberid + ") AND RecommendStatus=1 ");
            DataTable dt = helper.Query(strSql.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ReMemberRelationModel model = new ReMemberRelationModel();
                    model.RecommendMemberID = item["RecommendMemberID"].ToString().ParseToInt(0);
                    model.RecommendMemberName = item["RecommendMemberName"].ToString();
                    model.RecommendMemberCode = item["RecommendMemberCode"].ToString();
                    model.BeRecommMemberID = item["BeRecommMemberID"].ToString().ParseToInt(0);
                    model.BeRecommMemberName = item["BeRecommMemberName"].ToString();
                    model.BeRecommMemberCode = item["BeRecommMemberCode"].ToString();
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
        /// 根据推荐人查找推荐关系
        /// </summary>
        /// <param name="berecommendmemberid">推荐人ID</param>
        /// <returns></returns>
        public static ReMemberRelationModel GetReMemberRelationByRecommMemberID(int recommendmemberid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select RecommendMemberID, RecommendMemberName, RecommendMemberCode, BeRecommMemberID, BeRecommMemberName, BeRecommMemberCode ");
            strSql.Append("  from ReMemberRelation ");
            strSql.Append(" where RecommendMemberID=@RecommendMemberID AND RecommendStatus=1 ");
            SqlParameter[] parameters = {
					new SqlParameter("@RecommendMemberID", SqlDbType.Int)
			};
            parameters[0].Value = recommendmemberid;
            ReMemberRelationModel model = new ReMemberRelationModel();
            DataSet ds = helper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.RecommendMemberID = ds.Tables[0].Rows[0]["RecommendMemberID"].ToString().ParseToInt(0);
                model.RecommendMemberName = ds.Tables[0].Rows[0]["RecommendMemberName"].ToString();
                model.RecommendMemberCode = ds.Tables[0].Rows[0]["RecommendMemberCode"].ToString();
                model.BeRecommMemberID = ds.Tables[0].Rows[0]["BeRecommMemberID"].ToString().ParseToInt(0);
                model.BeRecommMemberName = ds.Tables[0].Rows[0]["BeRecommMemberName"].ToString();
                model.BeRecommMemberCode = ds.Tables[0].Rows[0]["BeRecommMemberCode"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据推荐人查找推荐关系
        /// </summary>
        /// <param name="berecommendmemberid">推荐人ID列表</param>
        /// <returns></returns>
        public static List<ReMemberRelationModel> GetReMemberRelationByRecommMemberID(string recommendmemberid)
        {
            List<ReMemberRelationModel> list = new List<ReMemberRelationModel>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select RecommendMemberID, RecommendMemberName, RecommendMemberCode, BeRecommMemberID, BeRecommMemberName, BeRecommMemberCode ");
            strSql.Append("  from ReMemberRelation ");
            strSql.Append(" where RecommendMemberID IN (" + recommendmemberid + ") AND RecommendStatus=1 ");
            DataTable dt = helper.Query(strSql.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ReMemberRelationModel model = new ReMemberRelationModel();
                    model.RecommendMemberID = item["RecommendMemberID"].ToString().ParseToInt(0);
                    model.RecommendMemberName = item["RecommendMemberName"].ToString();
                    model.RecommendMemberCode = item["RecommendMemberCode"].ToString();
                    model.BeRecommMemberID = item["BeRecommMemberID"].ToString().ParseToInt(0);
                    model.BeRecommMemberName = item["BeRecommMemberName"].ToString();
                    model.BeRecommMemberCode = item["BeRecommMemberCode"].ToString();
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
        /// 正向查询会员推荐树（按照推荐人）
        /// </summary>
        /// <param name="memberid">推荐人ID</param>
        /// <returns></returns>
        public static List<ReMemberRelationModel> GetReMemberRelationListByPositive(int memberid, out int totalcount)
        {
            List<ReMemberRelationModel> list = new List<ReMemberRelationModel>();
            string memberids = memberid.ToString();
            totalcount = 0;
            do
            {
                List<ReMemberRelationModel> relist = GetReMemberRelationByRecommMemberID(memberids);
                if (relist != null)
                {
                    memberids = "";
                    foreach (var item in relist)
                    {
                        memberids += item.RecommendMemberID + ",";
                    }
                    list.AddRange(relist);
                    totalcount += relist.Count;
                }
                else
                {
                    memberids = "";
                }

            } while (!string.IsNullOrWhiteSpace(memberids));
            return list;
        }
        /// <summary>
        /// 反向查询会员推荐树（按照被推荐人）
        /// </summary>
        /// <param name="memberid">被推荐人ID</param>
        /// <returns></returns>
        public static List<ReMemberRelationModel> GetReMemberRelationListByReverse(int memberid, out int totalcount)
        {
            List<ReMemberRelationModel> list = new List<ReMemberRelationModel>();
            string memberids = memberid.ToString();
            totalcount = 0;
            do
            {
                List<ReMemberRelationModel> relist = GetReMemberRelationByBeRecommMemberID(memberids);
                if (relist != null)
                {
                    memberids = "";
                    foreach (var item in relist)
                    {
                        memberids += item.RecommendMemberID + ",";
                    }
                    list.AddRange(relist);
                    totalcount += relist.Count;
                }
                else
                {
                    memberids = "";
                }

            } while (!string.IsNullOrWhiteSpace(memberids));
            return list;
        }
        /// <summary>
        /// 查询会员的直推会员人数
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public static int GetReMemberRelationCountByMemberid(int memberid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select COUNT(ID) ");
            strSql.Append("  from ReMemberRelation ");
            strSql.Append(" where RecommendMemberID=@RecommendMemberID AND RecommendStatus=1");
            SqlParameter[] parameters = {
					new SqlParameter("@RecommendMemberID", SqlDbType.Int)
			};
            parameters[0].Value = memberid;
            object obj = helper.GetSingle(strSql.ToString(), parameters);
            if (obj!=null)
            {
                return obj.ToString().ParseToInt(0);
            }
            else
            {
                return 0;
            }
        }
    }
}

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
    public class ApplyPOSDAL
    {
        public static DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 添加新的申请记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddNewApplyPOS(ApplyPOSModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ApplyPOS(");
            strSql.Append("CardFrontPath,ReceiveAddress,PStatus,ApplyRemark,AddTime,MemberID,MemberCode,MemberName,MemberPhone,MemberIDNumber,IDNumberFrontPath,IDNumberBackPath,IDWithHandPath");
            strSql.Append(") values (");
            strSql.Append("@CardFrontPath,@ReceiveAddress,1,@ApplyRemark,GETDATE(),@MemberID,@MemberCode,@MemberName,@MemberPhone,@MemberIDNumber,@IDNumberFrontPath,@IDNumberBackPath,@IDWithHandPath");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@CardFrontPath", SqlDbType.NVarChar) ,            
                        new SqlParameter("@ReceiveAddress", SqlDbType.NVarChar) ,  
                        new SqlParameter("@ApplyRemark", SqlDbType.NVarChar) ,      
                        new SqlParameter("@MemberID", SqlDbType.Int) ,            
                        new SqlParameter("@MemberCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberPhone", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberIDNumber", SqlDbType.NVarChar) ,            
                        new SqlParameter("@IDNumberFrontPath", SqlDbType.NVarChar) ,            
                        new SqlParameter("@IDNumberBackPath", SqlDbType.NVarChar) ,            
                        new SqlParameter("@IDWithHandPath", SqlDbType.NVarChar)     
            };

            parameters[0].Value = model.CardFrontPath;
            parameters[1].Value = model.ReceiveAddress;
            parameters[2].Value = model.ApplyRemark;
            parameters[3].Value = model.MemberID;
            parameters[4].Value = model.MemberCode;
            parameters[5].Value = model.MemberName;
            parameters[6].Value = model.MemberPhone;
            parameters[7].Value = model.MemberIDNumber;
            parameters[8].Value = model.IDNumberFrontPath;
            parameters[9].Value = model.IDNumberBackPath;
            parameters[10].Value = model.IDWithHandPath;
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
        /// 分页查询申请POS的列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public static List<ApplyPOSModel> GetApplyPOSListByPage(ApplyPOSModel applymodel, out int totalrowcount)
        {
            List<ApplyPOSModel> list = new List<ApplyPOSModel>();
            string columms = @"ID,MemberID,MemberCode,MemberName,MemberPhone,MemberIDNumber,IDNumberFrontPath,IDNumberBackPath,IDWithHandPath,CardFrontPath,ReceiveAddress,PStatus,ApplyRemark,AddTime,CASE PStatus WHEN 1 THEN '新申请' WHEN 2 THEN '审核通过' WHEN 3 THEN '发货途中' WHEN 4 THEN '审核失败' END PStatusName ";
            string where = "";
            if (applymodel != null)
            {
                if (applymodel.PStatus > 0)
                {
                    where += @" PStatus=" + applymodel.PStatus;
                }
                //名字
                if (!string.IsNullOrWhiteSpace(applymodel.MemberName) && string.IsNullOrWhiteSpace(where))
                {
                    where += @" MemberName Like '%" + applymodel.MemberName + "%'";
                }
                else if (!string.IsNullOrWhiteSpace(applymodel.MemberName) && !string.IsNullOrWhiteSpace(where))
                {
                    where += @" AND MemberName Like '%" + applymodel.MemberName + "%'";
                }
                //编号
                if (!string.IsNullOrWhiteSpace(applymodel.MemberCode) && string.IsNullOrWhiteSpace(where))
                {
                    where += @" MemberCode ='" + applymodel.MemberCode + "'";
                }
                else if (!string.IsNullOrWhiteSpace(applymodel.MemberCode) && !string.IsNullOrWhiteSpace(where))
                {
                    where += @" AND MemberCode ='" + applymodel.MemberCode + "'";
                }
                //手机
                if (!string.IsNullOrWhiteSpace(applymodel.MemberPhone) && string.IsNullOrWhiteSpace(where))
                {
                    where += @" MemberPhone ='" + applymodel.MemberPhone + "'";
                }
                else if (!string.IsNullOrWhiteSpace(applymodel.MemberPhone) && !string.IsNullOrWhiteSpace(where))
                {
                    where += @" AND MemberPhone ='" + applymodel.MemberPhone + "'";
                }
            }
            PageProModel page = new PageProModel();
            page.colums = columms;
            page.orderby = "AddTime";
            page.pageindex = applymodel.PageIndex;
            page.pagesize = applymodel.PageSize;
            page.tablename = @"dbo.ApplyPOS";
            page.where = where;
            DataTable dt = PublicHelperDAL.GetTable(page, out totalrowcount);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ApplyPOSModel model = new ApplyPOSModel();
                    if (item["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(item["ID"].ToString());
                    }
                    model.CardFrontPath = string.IsNullOrWhiteSpace(item["CardFrontPath"].ToString()) ? "" : AdminConfigs.ImgDoMain + item["CardFrontPath"].ToString();
                    model.ReceiveAddress = item["ReceiveAddress"].ToString();
                    if (item["PStatus"].ToString() != "")
                    {
                        model.PStatus = int.Parse(item["PStatus"].ToString());
                    }
                    model.ApplyRemark = item["ApplyRemark"].ToString();
                    if (item["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(item["AddTime"].ToString());
                    }
                    if (item["MemberID"].ToString() != "")
                    {
                        model.MemberID = int.Parse(item["MemberID"].ToString());
                    }
                    model.MemberCode = item["MemberCode"].ToString();
                    model.MemberName = item["MemberName"].ToString();
                    model.MemberPhone = item["MemberPhone"].ToString();
                    model.MemberIDNumber = item["MemberIDNumber"].ToString();
                    model.IDNumberFrontPath = string.IsNullOrWhiteSpace(item["IDNumberFrontPath"].ToString()) ? "" : AdminConfigs.ImgDoMain + item["IDNumberFrontPath"].ToString();
                    model.IDNumberBackPath = string.IsNullOrWhiteSpace(item["IDNumberBackPath"].ToString()) ? "" : AdminConfigs.ImgDoMain + item["IDNumberBackPath"].ToString();
                    model.IDWithHandPath = string.IsNullOrWhiteSpace(item["IDWithHandPath"].ToString()) ? "" : AdminConfigs.ImgDoMain + item["IDWithHandPath"].ToString();
                    model.PStatusName = item["PStatusName"].ToString();
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
        /// 修改申请单状态
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public static int UpdatePStatusByID(int aid, int statusnum)
        {
            string sqltxt = @"  UPDATE dbo.ApplyPOS
  SET PStatus=@statusid
  WHERE ID=@aid";
            SqlParameter[] paramter = { new SqlParameter("@statusid", statusnum), new SqlParameter("@aid", aid) };
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 批量修改单据状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="statusnum"></param>
        /// <returns></returns>
        public static int UpdatePStatusByIDs(string ids, int statusnum)
        {
            string sqltxt = @"  UPDATE dbo.ApplyPOS
  SET PStatus=@statusid
  WHERE ID IN (" + ids + @")";
            SqlParameter[] paramter = { new SqlParameter("@statusid", statusnum) };
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 查询单据的图片信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<string> GetApplyPOSImageByID(int id)
        {
            List<string> imagelist = new List<string>();
            string sqltxt = @"SELECT  IDNumberFrontPath ,
        IDNumberBackPath ,
        IDWithHandPath ,
        CardFrontPath
FROM    dbo.ApplyPOS
WHERE   ID = @id";
            SqlParameter[] paramter = { new SqlParameter("@id", id) };
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                imagelist.Add(string.IsNullOrWhiteSpace(dt.Rows[0]["IDNumberFrontPath"].ToString()) ? "" : AdminConfigs.ImgDoMain + dt.Rows[0]["IDNumberFrontPath"].ToString());
                imagelist.Add(string.IsNullOrWhiteSpace(dt.Rows[0]["IDNumberBackPath"].ToString()) ? "" : AdminConfigs.ImgDoMain + dt.Rows[0]["IDNumberBackPath"].ToString());
                imagelist.Add(string.IsNullOrWhiteSpace(dt.Rows[0]["IDWithHandPath"].ToString()) ? "" : AdminConfigs.ImgDoMain + dt.Rows[0]["IDWithHandPath"].ToString());
                imagelist.Add(string.IsNullOrWhiteSpace(dt.Rows[0]["CardFrontPath"].ToString()) ? "" : AdminConfigs.ImgDoMain + dt.Rows[0]["CardFrontPath"].ToString());
            }
            return imagelist;
        }
        /// <summary>
        /// 根据会员查询申请单信息
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public static List<ApplyPOSModel> GetApplyPosOrderByMemberid(int memberid)
        {
            List<ApplyPOSModel> list = new List<ApplyPOSModel>();
            string sqltxt = @"SELECT  MemberID ,
        MemberCode ,
        MemberName ,
        MemberPhone ,
        MemberIDNumber ,
        PStatus ,
        CASE PStatus
          WHEN 1 THEN '新申请'
          WHEN 2 THEN '审核通过'
          WHEN 3 THEN '发货途中'
          WHEN 4 THEN '审核失败'
        END AS PStatusName ,
        ApplyRemark ,
        AddTime
FROM    dbo.ApplyPOS
WHERE   MemberID = @memberid";
            SqlParameter[] paramter={new SqlParameter("@memberid",memberid)};
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ApplyPOSModel model = new ApplyPOSModel();
                    if (item["PStatus"].ToString() != "")
                    {
                        model.PStatus = int.Parse(item["PStatus"].ToString());
                    }
                    model.ApplyRemark = item["ApplyRemark"].ToString();
                    if (item["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(item["AddTime"].ToString());
                    }
                    if (item["MemberID"].ToString() != "")
                    {
                        model.MemberID = int.Parse(item["MemberID"].ToString());
                    }
                    model.MemberCode = item["MemberCode"].ToString();
                    model.MemberName = item["MemberName"].ToString();
                    model.MemberPhone = item["MemberPhone"].ToString();
                    model.MemberIDNumber = item["MemberIDNumber"].ToString();
                    model.PStatusName = item["PStatusName"].ToString();
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

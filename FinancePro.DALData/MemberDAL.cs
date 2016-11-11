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
    /// 会员操作类
    /// </summary>
    public class MemberDAL
    {
        public static DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 根据ID读取单个会员信息（全部字段）
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static MemberInfoModel GetSingleMemberModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select ID, MemberAddress, MemberBankName, MemberBankCode, MemberLogPwd, MemberStatus, MemberType, IsFinalMember, IsDerivativeMember, IsSpecialMember, IsReportMember, MemberName, AddTime, MemberCode, MemberSex, MemberPhone, MemberEmail, MemberProvince, MemberCity, MemberArea,MemberIDNumber,SourceMemberCode,MemberSecondPwd,MemberBankUserName  ");
            strSql.Append("  from MemberInfo ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int)
			};
            parameters[0].Value = ID;
            MemberInfoModel model = new MemberInfoModel();
            DataSet ds = helper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.MemberAddress = ds.Tables[0].Rows[0]["MemberAddress"].ToString();
                model.MemberBankName = ds.Tables[0].Rows[0]["MemberBankName"].ToString();
                model.MemberBankCode = ds.Tables[0].Rows[0]["MemberBankCode"].ToString();
                model.MemberLogPwd = ds.Tables[0].Rows[0]["MemberLogPwd"].ToString();
                model.MemberStatus = ds.Tables[0].Rows[0]["MemberStatus"].ToString().ParseToInt(1);
                model.MemberType = ds.Tables[0].Rows[0]["MemberType"].ToString().ParseToInt(1);
                model.IsFinalMember = ds.Tables[0].Rows[0]["IsFinalMember"].ToString().ParseToInt(0);
                model.IsDerivativeMember = ds.Tables[0].Rows[0]["IsDerivativeMember"].ToString().ParseToInt(0);
                model.IsSpecialMember = ds.Tables[0].Rows[0]["IsSpecialMember"].ToString().ParseToInt(0);
                model.IsReportMember = ds.Tables[0].Rows[0]["IsReportMember"].ToString().ParseToInt(0);
                model.MemberName = ds.Tables[0].Rows[0]["MemberName"].ToString();
                if (ds.Tables[0].Rows[0]["AddTime"].ToString() != "")
                {
                    model.AddTime = ds.Tables[0].Rows[0]["AddTime"].ToString().ParseToDateTime(DateTime.Now);
                }
                model.MemberCode = ds.Tables[0].Rows[0]["MemberCode"].ToString();
                model.MemberSex = ds.Tables[0].Rows[0]["MemberSex"].ToString().ParseToInt(1);
                model.MemberPhone = ds.Tables[0].Rows[0]["MemberPhone"].ToString();
                model.MemberEmail = ds.Tables[0].Rows[0]["MemberEmail"].ToString();
                model.MemberProvince = ds.Tables[0].Rows[0]["MemberProvince"].ToString();
                model.MemberCity = ds.Tables[0].Rows[0]["MemberCity"].ToString();
                model.MemberArea = ds.Tables[0].Rows[0]["MemberArea"].ToString();
                model.MemberIDNumber = ds.Tables[0].Rows[0]["MemberIDNumber"].ToString();
                model.SourceMemberCode = ds.Tables[0].Rows[0]["SourceMemberCode"].ToString();
                model.MemberSecondPwd = ds.Tables[0].Rows[0]["MemberSecondPwd"].ToString();
                model.MemberBankUserName = ds.Tables[0].Rows[0]["MemberBankUserName"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据ID读取单个会员信息(简要字段)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static MemberInfoModel GetBriefSingleMemberModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select ID, MemberBankName, MemberBankCode, MemberStatus, MemberType, IsFinalMember, IsDerivativeMember, IsSpecialMember, IsReportMember, MemberName, MemberCode, MemberPhone,MemberIDNumber,SourceMemberCode,MemberBankUserName  ");
            strSql.Append("  from MemberInfo ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int)
			};
            parameters[0].Value = ID;
            MemberInfoModel model = new MemberInfoModel();
            DataSet ds = helper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.MemberBankName = ds.Tables[0].Rows[0]["MemberBankName"].ToString();
                model.MemberBankCode = ds.Tables[0].Rows[0]["MemberBankCode"].ToString();
                model.MemberStatus = ds.Tables[0].Rows[0]["MemberStatus"].ToString().ParseToInt(1);
                model.MemberType = ds.Tables[0].Rows[0]["MemberType"].ToString().ParseToInt(1);
                model.IsFinalMember = ds.Tables[0].Rows[0]["IsFinalMember"].ToString().ParseToInt(0);
                model.IsDerivativeMember = ds.Tables[0].Rows[0]["IsDerivativeMember"].ToString().ParseToInt(0);
                model.IsSpecialMember = ds.Tables[0].Rows[0]["IsSpecialMember"].ToString().ParseToInt(0);
                model.IsReportMember = ds.Tables[0].Rows[0]["IsReportMember"].ToString().ParseToInt(0);
                model.MemberName = ds.Tables[0].Rows[0]["MemberName"].ToString();
                model.MemberCode = ds.Tables[0].Rows[0]["MemberCode"].ToString();
                model.MemberPhone = ds.Tables[0].Rows[0]["MemberPhone"].ToString();
                model.MemberIDNumber = ds.Tables[0].Rows[0]["MemberIDNumber"].ToString();
                model.SourceMemberCode = ds.Tables[0].Rows[0]["SourceMemberCode"].ToString();
                model.MemberBankUserName = ds.Tables[0].Rows[0]["MemberBankUserName"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据会员编号读取单个会员信息(简要字段)
        /// </summary>
        /// <param name="membercode">会员编号</param>
        /// <returns></returns>
        public static MemberInfoModel GetBriefSingleMemberModel(string membercode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select ID, MemberBankName, MemberBankCode, MemberStatus, MemberType, IsFinalMember, IsDerivativeMember, IsSpecialMember, IsReportMember, MemberName, MemberCode, MemberPhone,MemberIDNumber,SourceMemberCode,MemberBankUserName  ");
            strSql.Append("  from MemberInfo ");
            strSql.Append(" where MemberCode=@MemberCode");
            SqlParameter[] parameters = {
					new SqlParameter("@MemberCode", SqlDbType.NVarChar)
			};
            parameters[0].Value = membercode;
            MemberInfoModel model = new MemberInfoModel();
            DataSet ds = helper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.MemberBankName = ds.Tables[0].Rows[0]["MemberBankName"].ToString();
                model.MemberBankCode = ds.Tables[0].Rows[0]["MemberBankCode"].ToString();
                model.MemberStatus = ds.Tables[0].Rows[0]["MemberStatus"].ToString().ParseToInt(1);
                model.MemberType = ds.Tables[0].Rows[0]["MemberType"].ToString().ParseToInt(1);
                model.IsFinalMember = ds.Tables[0].Rows[0]["IsFinalMember"].ToString().ParseToInt(0);
                model.IsDerivativeMember = ds.Tables[0].Rows[0]["IsDerivativeMember"].ToString().ParseToInt(0);
                model.IsSpecialMember = ds.Tables[0].Rows[0]["IsSpecialMember"].ToString().ParseToInt(0);
                model.IsReportMember = ds.Tables[0].Rows[0]["IsReportMember"].ToString().ParseToInt(0);
                model.MemberName = ds.Tables[0].Rows[0]["MemberName"].ToString();
                model.MemberCode = ds.Tables[0].Rows[0]["MemberCode"].ToString();
                model.MemberPhone = ds.Tables[0].Rows[0]["MemberPhone"].ToString();
                model.MemberIDNumber = ds.Tables[0].Rows[0]["MemberIDNumber"].ToString();
                model.SourceMemberCode = ds.Tables[0].Rows[0]["SourceMemberCode"].ToString();
                model.MemberBankUserName = ds.Tables[0].Rows[0]["MemberBankUserName"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据会员编号读取单个会员信息(简要字段)(登陆专用)
        /// </summary>
        /// <param name="membercode">会员编号</param>
        /// <returns></returns>
        public static MemberInfoModel GetBriefSingleMemberModelForLogin(string membercode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select ID, MemberBankName, MemberBankCode, MemberStatus, MemberType, IsFinalMember, IsDerivativeMember, IsSpecialMember, IsReportMember, MemberName, MemberCode, MemberPhone,MemberIDNumber,SourceMemberCode,MemberBankUserName,MemberLogPwd  ");
            strSql.Append("  from MemberInfo ");
            strSql.Append(" where MemberCode=@MemberCode");
            SqlParameter[] parameters = {
					new SqlParameter("@MemberCode", SqlDbType.NVarChar)
			};
            parameters[0].Value = membercode;
            MemberInfoModel model = new MemberInfoModel();
            DataSet ds = helper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.MemberBankName = ds.Tables[0].Rows[0]["MemberBankName"].ToString();
                model.MemberBankCode = ds.Tables[0].Rows[0]["MemberBankCode"].ToString();
                model.MemberStatus = ds.Tables[0].Rows[0]["MemberStatus"].ToString().ParseToInt(1);
                model.MemberType = ds.Tables[0].Rows[0]["MemberType"].ToString().ParseToInt(1);
                model.IsFinalMember = ds.Tables[0].Rows[0]["IsFinalMember"].ToString().ParseToInt(0);
                model.IsDerivativeMember = ds.Tables[0].Rows[0]["IsDerivativeMember"].ToString().ParseToInt(0);
                model.IsSpecialMember = ds.Tables[0].Rows[0]["IsSpecialMember"].ToString().ParseToInt(0);
                model.IsReportMember = ds.Tables[0].Rows[0]["IsReportMember"].ToString().ParseToInt(0);
                model.MemberName = ds.Tables[0].Rows[0]["MemberName"].ToString();
                model.MemberCode = ds.Tables[0].Rows[0]["MemberCode"].ToString();
                model.MemberPhone = ds.Tables[0].Rows[0]["MemberPhone"].ToString();
                model.MemberIDNumber = ds.Tables[0].Rows[0]["MemberIDNumber"].ToString();
                model.SourceMemberCode = ds.Tables[0].Rows[0]["SourceMemberCode"].ToString();
                model.MemberBankUserName = ds.Tables[0].Rows[0]["MemberBankUserName"].ToString();
                model.MemberLogPwd = ds.Tables[0].Rows[0]["MemberLogPwd"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据会员编号读取单个会员信息(简要字段)
        /// </summary>
        /// <param name="membercode">会员编号</param>
        /// <returns></returns>
        public static MemberInfoModel GetBriefSingleMemberModelByMemberName(string membername)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select ID, MemberBankName, MemberBankCode, MemberStatus, MemberType, IsFinalMember, IsDerivativeMember, IsSpecialMember, IsReportMember, MemberName, MemberCode, MemberPhone,MemberIDNumber,SourceMemberCode,MemberBankUserName  ");
            strSql.Append("  from MemberInfo ");
            strSql.Append(" where MemberName=@MemberName");
            SqlParameter[] parameters = {
					new SqlParameter("@MemberName", SqlDbType.NVarChar)
			};
            parameters[0].Value = membername;
            MemberInfoModel model = new MemberInfoModel();
            DataSet ds = helper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.MemberBankName = ds.Tables[0].Rows[0]["MemberBankName"].ToString();
                model.MemberBankCode = ds.Tables[0].Rows[0]["MemberBankCode"].ToString();
                model.MemberStatus = ds.Tables[0].Rows[0]["MemberStatus"].ToString().ParseToInt(1);
                model.MemberType = ds.Tables[0].Rows[0]["MemberType"].ToString().ParseToInt(1);
                model.IsFinalMember = ds.Tables[0].Rows[0]["IsFinalMember"].ToString().ParseToInt(0);
                model.IsDerivativeMember = ds.Tables[0].Rows[0]["IsDerivativeMember"].ToString().ParseToInt(0);
                model.IsSpecialMember = ds.Tables[0].Rows[0]["IsSpecialMember"].ToString().ParseToInt(0);
                model.IsReportMember = ds.Tables[0].Rows[0]["IsReportMember"].ToString().ParseToInt(0);
                model.MemberName = ds.Tables[0].Rows[0]["MemberName"].ToString();
                model.MemberCode = ds.Tables[0].Rows[0]["MemberCode"].ToString();
                model.MemberPhone = ds.Tables[0].Rows[0]["MemberPhone"].ToString();
                model.MemberIDNumber = ds.Tables[0].Rows[0]["MemberIDNumber"].ToString();
                model.SourceMemberCode = ds.Tables[0].Rows[0]["SourceMemberCode"].ToString();
                model.MemberBankUserName = ds.Tables[0].Rows[0]["MemberBankUserName"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据来源会员编号读取单个终极会员信息(简要字段)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static MemberInfoModel GetBriefSingleMemberModelBySourceMemberCode(string sourcemembercode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select ID, MemberBankName, MemberBankCode, MemberStatus, MemberType, IsFinalMember, IsDerivativeMember, IsSpecialMember, IsReportMember, MemberName, MemberCode, MemberPhone,MemberIDNumber,SourceMemberCode,MemberBankUserName  ");
            strSql.Append("  from MemberInfo ");
            strSql.Append(" where SourceMemberCode = @sourcemembercode AND MemberType = 3");
            SqlParameter[] parameters = {
					new SqlParameter("@sourcemembercode", SqlDbType.Int)
			};
            parameters[0].Value = sourcemembercode;
            MemberInfoModel model = new MemberInfoModel();
            DataSet ds = helper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.MemberBankName = ds.Tables[0].Rows[0]["MemberBankName"].ToString();
                model.MemberBankCode = ds.Tables[0].Rows[0]["MemberBankCode"].ToString();
                model.MemberStatus = ds.Tables[0].Rows[0]["MemberStatus"].ToString().ParseToInt(1);
                model.MemberType = ds.Tables[0].Rows[0]["MemberType"].ToString().ParseToInt(1);
                model.IsFinalMember = ds.Tables[0].Rows[0]["IsFinalMember"].ToString().ParseToInt(0);
                model.IsDerivativeMember = ds.Tables[0].Rows[0]["IsDerivativeMember"].ToString().ParseToInt(0);
                model.IsSpecialMember = ds.Tables[0].Rows[0]["IsSpecialMember"].ToString().ParseToInt(0);
                model.IsReportMember = ds.Tables[0].Rows[0]["IsReportMember"].ToString().ParseToInt(0);
                model.MemberName = ds.Tables[0].Rows[0]["MemberName"].ToString();
                model.MemberCode = ds.Tables[0].Rows[0]["MemberCode"].ToString();
                model.MemberPhone = ds.Tables[0].Rows[0]["MemberPhone"].ToString();
                model.MemberIDNumber = ds.Tables[0].Rows[0]["MemberIDNumber"].ToString();
                model.SourceMemberCode = ds.Tables[0].Rows[0]["SourceMemberCode"].ToString();
                model.MemberBankUserName = ds.Tables[0].Rows[0]["MemberBankUserName"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 添加会员
        /// </summary>
        /// <param name="model">会员模型</param>
        /// <returns></returns>
        public static int AddNewMember(MemberInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MemberInfo(");
            strSql.Append("MemberAddress,MemberBankName,MemberBankCode,MemberLogPwd,MemberStatus,MemberType,IsFinalMember,IsDerivativeMember,IsSpecialMember,IsReportMember,MemberName,AddTime,MemberCode,MemberSex,MemberPhone,MemberEmail,MemberProvince,MemberCity,MemberArea,MemberIDNumber,SourceMemberCode,MemberSecondPwd,MemberBankUserName");
            strSql.Append(") values (");
            strSql.Append("@MemberAddress,@MemberBankName,@MemberBankCode,@MemberLogPwd,@MemberStatus,@MemberType,@IsFinalMember,@IsDerivativeMember,@IsSpecialMember,@IsReportMember,@MemberName,GETDATE(),@MemberCode,@MemberSex,@MemberPhone,@MemberEmail,@MemberProvince,@MemberCity,@MemberArea,@MemberIDNumber,@SourceMemberCode,@MemberSecondPwd,@MemberBankUserName");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@MemberAddress", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberBankName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberBankCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberLogPwd", SqlDbType.NVarChar) ,          
                        new SqlParameter("@MemberType", SqlDbType.Int) ,            
                        new SqlParameter("@IsFinalMember", SqlDbType.Int) ,            
                        new SqlParameter("@IsDerivativeMember", SqlDbType.Int) ,            
                        new SqlParameter("@IsSpecialMember", SqlDbType.Int) ,            
                        new SqlParameter("@IsReportMember", SqlDbType.Int) ,            
                        new SqlParameter("@MemberName", SqlDbType.NVarChar) ,         
                        new SqlParameter("@MemberCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberSex", SqlDbType.Int) ,            
                        new SqlParameter("@MemberPhone", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberEmail", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberProvince", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberCity", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberArea", SqlDbType.NVarChar),
                        new SqlParameter("@MemberIDNumber",SqlDbType.NVarChar),
                        new SqlParameter("@SourceMemberCode",SqlDbType.NVarChar),
                        new SqlParameter("@MemberStatus",SqlDbType.Int),
                        new SqlParameter("@MemberSecondPwd",SqlDbType.NVarChar),
                        new SqlParameter("@MemberBankUserName",SqlDbType.NVarChar)
            };
            parameters[0].Value = model.MemberAddress;
            parameters[1].Value = model.MemberBankName;
            parameters[2].Value = model.MemberBankCode;
            parameters[3].Value = model.MemberLogPwd;
            parameters[4].Value = model.MemberType;
            parameters[5].Value = model.IsFinalMember;
            parameters[6].Value = model.IsDerivativeMember;
            parameters[7].Value = model.IsSpecialMember;
            parameters[8].Value = model.IsReportMember;
            parameters[9].Value = model.MemberName;
            parameters[10].Value = model.MemberCode;
            parameters[11].Value = model.MemberSex;
            parameters[12].Value = model.MemberPhone;
            parameters[13].Value = model.MemberEmail;
            parameters[14].Value = model.MemberProvince;
            parameters[15].Value = model.MemberCity;
            parameters[16].Value = model.MemberArea;
            parameters[17].Value = model.MemberIDNumber;
            parameters[18].Value = model.SourceMemberCode;
            parameters[19].Value = model.MemberStatus;
            parameters[20].Value = model.MemberSecondPwd;
            parameters[21].Value = model.MemberBankUserName;
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
        /// 更新会员的状态值
        /// </summary>
        /// <param name="memberid">会员ID</param>
        /// <param name="status">状态值</param>
        /// <returns></returns>
        public static int UpdateMemberStatus(int memberid, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MemberInfo set ");
            strSql.Append(" MemberStatus = @MemberStatus  ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int) ,            
                        new SqlParameter("@MemberStatus", SqlDbType.Int)
            };
            parameters[0].Value = memberid;
            parameters[1].Value = status;
            return helper.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新会员是否报单中心
        /// </summary>
        /// <param name="memberid">会员ID</param>
        /// <param name="status">是否报单中心</param>
        /// <returns></returns>
        public static int UpdateMemberIsReportMember(int memberid, int isreportmember)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MemberInfo set ");
            strSql.Append(" IsReportMember = @IsReportMember ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int) ,           
                        new SqlParameter("@IsReportMember", SqlDbType.Int) 
            };
            parameters[0].Value = memberid;
            parameters[1].Value = isreportmember;
            return helper.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新会员的信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int UpdateMemberInfo(MemberInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MemberInfo set ");
            strSql.Append(" MemberBankName = @MemberBankName , ");
            strSql.Append(" MemberBankCode = @MemberBankCode , ");
            strSql.Append(" MemberBankUserName = @MemberBankUserName , ");
            strSql.Append(" MemberName = @MemberName , ");
            strSql.Append(" MemberPhone = @MemberPhone , ");
            strSql.Append(" MemberIDNumber = @MemberIDNumber , ");
            strSql.Append(" MemberEmail = @MemberEmail , ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
			            new SqlParameter("@ID", model.ID) ,               
                        new SqlParameter("@MemberBankName", model.MemberBankName) ,            
                        new SqlParameter("@MemberBankCode", model.MemberBankCode) , 
                        new SqlParameter("@MemberName", model.MemberName) ,        
                        new SqlParameter("@MemberIDNumber", model.MemberIDNumber) ,            
                        new SqlParameter("@MemberBankUserName", model.MemberBankUserName) ,            
                        new SqlParameter("@MemberPhone", model.MemberPhone) ,            
                        new SqlParameter("@MemberEmail", model.MemberEmail) 
            };
            return helper.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新会员的登录密码
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="logpwd"></param>
        /// <returns></returns>
        public static int UpdateMemberLogpwd(int memberid, string logpwd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MemberInfo set ");
            strSql.Append(" MemberLogPwd = @MemberLogPwd  ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int) ,          
                        new SqlParameter("@MemberLogPwd", SqlDbType.NVarChar)  
            };
            parameters[0].Value = memberid;
            parameters[1].Value = logpwd;
            return helper.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新会员的二级密码
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="logpwd"></param>
        /// <returns></returns>
        public static int UpdateMemberMemberSecondPwd(int memberid, string secpwd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MemberInfo set ");
            strSql.Append(" MemberSecondPwd = @MemberSecondPwd  ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int) ,          
                        new SqlParameter("@MemberSecondPwd", SqlDbType.NVarChar)  
            };
            parameters[0].Value = memberid;
            parameters[1].Value = secpwd;
            return helper.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 查询会员是否报单中心
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public static bool GetMemberIsReportMember(int memberid)
        {
            string sqltxt = @"  SELECT    ISNULL(IsReportMember, 0) AS IsReportMember
  FROM      MemberInfo
  WHERE     ID = @id
            AND MemberStatus = 2";
            SqlParameter[] paramter = { new SqlParameter("@id", memberid) };
            object obj = helper.GetSingle(sqltxt, paramter);
            if (obj != null)
            {
                if (obj.ToString().ParseToInt(0) == 0)
                    return false;
                else
                    return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据身份证编号查询注册会员个数
        /// </summary>
        /// <param name="memberidnumber"></param>
        /// <returns></returns>
        public static int GetMemberCountByMemberIDNumber(string memberidnumber)
        {
            string sqltxt = @"SELECT  COUNT(ID)
FROM    MemberInfo
WHERE   MemberIDNumber = @MemberIDNumber
        AND MemberStatus <> 3
        AND MemberType = 1";
            SqlParameter[] paramter = { new SqlParameter("@MemberIDNumber", memberidnumber) };
            return helper.GetSingle(sqltxt, paramter).ToString().ParseToInt(0);
        }
        /// <summary>
        /// 分页查询会员信息
        /// </summary>
        /// <param name="searchmodel">查询条件</param>
        /// <param name="totalrowcount">总记录数</param>
        /// <returns></returns>
        public static List<MemberInfoModel> GetMemberListForPage(MemberInfoModel searchmodel, out int totalrowcount)
        {
            List<MemberInfoModel> list = new List<MemberInfoModel>();
            string columms = @"ID,MemberName,MemberCode,MemberPhone,MemberEmail,MemberIDNumber,MemberProvince,MemberCity,MemberArea,MemberAddress,MemberBankName,MemberBankCode, MemberStatus,CASE MemberStatus WHEN 1 THEN '待激活' WHEN 2 THEN '已激活' WHEN 3 THEN '已禁用' WHEN 4 THEN '已出局'END AS MemberStatusName,MemberType,CASE MemberType WHEN 1 THEN '常规账户' WHEN 2 THEN '衍生账户' WHEN 3 THEN '终端账户' WHEN 4 THEN '超级会员'END AS MemberTypeName,IsFinalMember,IsDerivativeMember,IsSpecialMember,IsReportMember,AddTime,SourceMemberCode";
            string where = "";
            if (searchmodel != null)
            {
                if (searchmodel.MemberStatus > 0)
                {
                    where += @" MemberStatus=" + searchmodel.MemberStatus;
                }
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
                //手机
                if (!string.IsNullOrWhiteSpace(searchmodel.MemberPhone) && string.IsNullOrWhiteSpace(where))
                {
                    where += @" MemberPhone ='" + searchmodel.MemberPhone + "'";
                }
                else if (!string.IsNullOrWhiteSpace(searchmodel.MemberPhone) && !string.IsNullOrWhiteSpace(where))
                {
                    where += @" AND MemberPhone ='" + searchmodel.MemberPhone + "'";
                }
            }
            PageProModel page = new PageProModel();
            page.colums = columms;
            page.orderby = "AddTime";
            page.pageindex = searchmodel.PageIndex;
            page.pagesize = searchmodel.PageSize;
            page.tablename = @"dbo.MemberInfo";
            page.where = where;
            DataTable dt = PublicHelperDAL.GetTable(page, out totalrowcount);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    MemberInfoModel model = new MemberInfoModel();
                    if (item["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(item["ID"].ToString());
                    }
                    model.MemberArea = item["MemberArea"].ToString();
                    model.MemberAddress = item["MemberAddress"].ToString();
                    model.MemberBankName = item["MemberBankName"].ToString();
                    model.MemberBankCode = item["MemberBankCode"].ToString();
                    if (item["MemberStatus"].ToString() != "")
                    {
                        model.MemberStatus = int.Parse(item["MemberStatus"].ToString());
                    }
                    if (item["MemberType"].ToString() != "")
                    {
                        model.MemberType = int.Parse(item["MemberType"].ToString());
                    }
                    if (item["IsFinalMember"].ToString() != "")
                    {
                        model.IsFinalMember = int.Parse(item["IsFinalMember"].ToString());
                    }
                    if (item["IsDerivativeMember"].ToString() != "")
                    {
                        model.IsDerivativeMember = int.Parse(item["IsDerivativeMember"].ToString());
                    }
                    if (item["IsSpecialMember"].ToString() != "")
                    {
                        model.IsSpecialMember = int.Parse(item["IsSpecialMember"].ToString());
                    }
                    model.MemberName = item["MemberName"].ToString();
                    if (item["IsReportMember"].ToString() != "")
                    {
                        model.IsReportMember = int.Parse(item["IsReportMember"].ToString());
                    }
                    if (item["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(item["AddTime"].ToString());
                    }
                    model.SourceMemberCode = item["SourceMemberCode"].ToString();
                    model.MemberCode = item["MemberCode"].ToString();
                    model.MemberPhone = item["MemberPhone"].ToString();
                    model.MemberEmail = item["MemberEmail"].ToString();
                    model.MemberIDNumber = item["MemberIDNumber"].ToString();
                    model.MemberProvince = item["MemberProvince"].ToString();
                    model.MemberCity = item["MemberCity"].ToString();
                    model.MemberStatusName = item["MemberStatusName"].ToString();
                    model.MemberTypeName = item["MemberTypeName"].ToString();
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
        /// 得到会员的总数
        /// </summary>
        /// <param name="isactive">是否激活</param>
        /// <returns></returns>
        public static int GetMemberCount(int isactive)
        {
            string sqltxt = @"SELECT  COUNT(0) AS con
FROM    dbo.MemberInfo ";
            if (isactive == 1)
            {
                sqltxt += @" WHERE   MemberStatus = 2 ";
            }
            return helper.GetSingle(sqltxt).ToString().ParseToInt(0);
        }
        /// <summary>
        /// 按照来源会员编号查询会员信息
        /// </summary>
        /// <param name="membercode"></param>
        /// <returns></returns>
        public static List<MemberInfoModel> GetChildCountListBySourceMemberCode(string membercode)
        {
            List<MemberInfoModel> list = new List<MemberInfoModel>();
            string sqltxt = @"SELECT  ID ,
        MemberName ,
        MemberCode ,
        MemberIDNumber ,
        MemberPhone ,
        CASE MemberStatus
          WHEN 1 THEN '待激活'
          WHEN 2 THEN '已激活'
          WHEN 3 THEN '已冻结'
          WHEN 4 THEN '已完成'
        END AS MemberStatusName ,
        MemberStatus,AddTime
FROM    dbo.MemberInfo
WHERE  SourceMemberCode=@sourcecode";
            SqlParameter[] paramter = { new SqlParameter("@sourcecode", membercode) };
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    list.Add(new MemberInfoModel()
                    {
                        ID = item["ID"].ToString().ParseToInt(0),
                        MemberName = item["MemberName"].ToString(),
                        MemberCode = item["MemberCode"].ToString(),
                        MemberIDNumber = item["MemberIDNumber"].ToString(),
                        MemberPhone = item["MemberPhone"].ToString(),
                        MemberStatusName = item["MemberStatusName"].ToString(),
                        MemberStatus = item["MemberStatus"].ToString().ParseToInt(1),
                        AddTime = item["AddTime"].ToString().ParseToDateTime(DateTime.Now)
                    });
                }
            }
            return list;
        }
        /// <summary>
        /// 批量创建终极账户
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="maxcommeurry"></param>
        /// <returns></returns>
        public static int AddNewMemberInfoByDynamicReward(int memberid,decimal maxcommeurry)
        {
            int result = 0;
            string sqltxt = @"SELECT  A.MemberID
        INTO    #t1
        FROM    dbo.MemberCapitalDetail A
        INNER JOIN dbo.DynamicReward B ON A.MemberID=B.MemberID
        WHERE B.SourceMemberID=@memberid AND  ISNULL(ISCreadChild, 0) = 0
                AND A.CompoundCurrency >= @maxcomf
INSERT  INTO dbo.MemberInfo
                ( MemberName ,
                  MemberCode ,
                  MemberSex ,
                  MemberPhone ,
                  MemberEmail ,
                  MemberIDNumber ,
                  MemberProvince ,
                  MemberCity ,
                  MemberArea ,
                  MemberAddress ,
                  MemberBankName ,
                  MemberBankCode ,
                  MemberLogPwd ,
                  MemberStatus ,
                  MemberType ,
                  IsFinalMember ,
                  IsDerivativeMember ,
                  IsSpecialMember ,
                  IsReportMember ,
                  AddTime ,
                  SourceMemberCode ,
                  MemberSecondPwd ,
                  MemberBankUserName
                )
                SELECT  MemberName ,
                        MemberCode + '-1' ,
                        MemberSex ,
                        MemberPhone ,
                        MemberEmail ,
                        MemberIDNumber ,
                        MemberProvince ,
                        MemberCity ,
                        MemberArea ,
                        MemberAddress ,
                        MemberBankName ,
                        MemberBankCode ,
                        MemberLogPwd ,
                        1 ,
                        3 ,
                        1 ,
                        0 ,
                        0 ,
                        0 ,
                        GETDATE() ,
                        MemberCode ,
                        MemberSecondPwd ,
                        MemberBankUserName
                FROM    dbo.MemberInfo
                WHERE   id IN ( SELECT  memberid
                                FROM    #t1 )
                        AND ISNULL(IsFinalMember, 0) = 0
             
              INSERT  INTO dbo.MemberCapitalDetail
                ( MemberID ,
                  MemberName ,
                  MemberCode ,
                  GameCurrency ,
                  SharesCurrency ,
                  ShoppingCurrency ,
                  MemberPoints ,
                  CompoundCurrency ,
                  ISCreadChild ,
                  TotalAssignedPoints ,
                  ISDeSysCost ,
                  DeSysCost
                )
                SELECT  ID ,
                        MemberName ,
                        MemberCode  ,
                        0 ,
                        0 ,
                        0 ,
                        0 ,
                        0 ,
                        0 ,
                        0 ,
                        0 ,
                        0
                FROM    dbo.MemberInfo
                WHERE   SourceMemberCode IN ( SELECT  MemberCode
                                FROM    #t1 A INNER JOIN dbo.MemberInfo B ON A.MemberID=B.ID )
                        AND ISNULL(IsFinalMember, 0) = 1
                        
        INSERT  INTO dbo.MemberExtendInfo
                ( MemberID ,
                  MemberName ,
                  MemberCode ,
                  FormCurreyNum
                )
                SELECT  ID ,
                        MemberName ,
                        MemberCode + '-1' ,
                        0
                FROM    dbo.MemberInfo
                WHERE   SourceMemberCode IN ( SELECT  MemberCode
                                FROM    #t1 A INNER JOIN dbo.MemberInfo B ON A.MemberID=B.ID )
                        AND ISNULL(IsFinalMember, 0) = 1
                     
        UPDATE A                
       SET ISCreadChild=1
       FROM dbo.MemberCapitalDetail A
       INNER JOIN dbo.MemberInfo C ON C.ID = A.MemberID
       INNER JOIN #t1 B ON A.MemberID=B.MemberID AND  ISNULL(C.IsFinalMember, 0) = 0 ";
            SqlParameter[] paramter = { new SqlParameter("@memberid", memberid), new SqlParameter("@maxcomf", maxcommeurry) };
            result = helper.ExecuteSql(sqltxt,paramter);
            return result;
        }
    }
}

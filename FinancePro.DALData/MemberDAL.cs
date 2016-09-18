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
    public class MemberDAL
    {
        public static DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 根据读取单个会员信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static MemberInfoModel GetSingleMemberModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select ID, MemberAddress, MemberBankName, MemberBankCode, MemberLogPwd, MemberStatus, MemberType, IsFinalMember, IsDerivativeMember, IsSpecialMember, IsReportMember, MemberName, AddTime, MemberCode, MemberSex, MemberPhone, MemberEmail, MemberProvince, MemberCity, MemberArea  ");
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
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据读取单个会员信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static MemberInfoModel GetBriefSingleMemberModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select ID, MemberBankName, MemberBankCode, MemberStatus, MemberType, IsFinalMember, IsDerivativeMember, IsSpecialMember, IsReportMember, MemberName, MemberCode, MemberPhone  ");
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
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据读取单个会员信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static MemberInfoModel GetBriefSingleMemberModel(string membercode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select ID, MemberBankName, MemberBankCode, MemberStatus, MemberType, IsFinalMember, IsDerivativeMember, IsSpecialMember, IsReportMember, MemberName, MemberCode, MemberPhone  ");
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
                return model;
            }
            else
            {
                return null;
            }
        }
    }
}

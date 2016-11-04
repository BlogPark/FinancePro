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
    public  class ApplyPOSDAL
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
            strSql.Append("@CardFrontPath,@ReceiveAddress,@PStatus,@ApplyRemark,GETDATE(),@MemberID,@MemberCode,@MemberName,@MemberPhone,@MemberIDNumber,@IDNumberFrontPath,@IDNumberBackPath,@IDWithHandPath");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@CardFrontPath", SqlDbType.NVarChar) ,            
                        new SqlParameter("@ReceiveAddress", SqlDbType.NVarChar) ,            
                        new SqlParameter("@PStatus", SqlDbType.Int) ,            
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
            parameters[2].Value = model.PStatus;
            parameters[3].Value = model.ApplyRemark;
            parameters[4].Value = model.MemberID;
            parameters[5].Value = model.MemberCode;
            parameters[6].Value = model.MemberName;
            parameters[7].Value = model.MemberPhone;
            parameters[8].Value = model.MemberIDNumber;
            parameters[9].Value = model.IDNumberFrontPath;
            parameters[10].Value = model.IDNumberBackPath;
            parameters[11].Value = model.IDWithHandPath;
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


    }
}

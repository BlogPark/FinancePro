using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancePro.DALData;
using FinancePro.DataModels;

namespace FinancePro.BLLData
{
    /// <summary>
    /// 会员编号操作类
    /// </summary>
    public class MemberCodeBLL
    {
        /// <summary>
        /// 创建新的会员编号
        /// </summary>
        /// <returns></returns>
        public static int CreateNewMemberCode()
        {
            try
            {
                int basenum = MemberCodeDAL.GetMaxMemberCode();
                if (basenum == 0)
                {
                    basenum = 1000000;
                }
                for (int i = 0; i < 40000; i++)
                {
                    MemberCodeModel model = new MemberCodeModel();
                    model.MemberCode = basenum;
                    MemberCodeDAL.AddNewMemberCode(model);
                    basenum++;
                }
            }
            catch
            {
                return 0;
            }
            return 1;
        }
        /// <summary>
        /// 得到会员编号
        /// </summary>
        /// <returns></returns>
        public static MemberCodeModel GetMemberCode()
        {
            return MemberCodeDAL.GetMemberCode();
        }
        /// <summary>
        /// 得到最大的会员ID
        /// </summary>
        /// <returns></returns>
        public static int GetMaxMemberCode()
        {
            return MemberCodeDAL.GetMaxMemberCode();
        }
        /// <summary>
        /// 更改数据状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateMemberCodeStatus(int id)
        {
            return MemberCodeDAL.UpdateMemberCodeStatus(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using FinancePro.DALData;
using FinancePro.DataModels;

namespace FinancePro.BLLData
{
    /// <summary>
    /// 会员的资产管理结构类
    /// </summary>
    public class MemberCapitalDetailBLL
    {
        /// <summary>
        /// 会员间转赠报单币
        /// </summary>
        /// <param name="sourcememberid">转出账户</param>
        /// <param name="acceptmemberid">接收账户</param>
        /// <param name="count">数量</param>
        /// <returns></returns>
        public string MemberTransferFormCurreyNum(int sourcememberid, int acceptmemberid, int count)
        {
            string result = "1";
            #region 查询会员拥有资产信息
            MemberExtendInfoModel memberextend = MemberExtendInfoDAL.GetMemberExtendInfoByMemberID(sourcememberid);//会员扩展信息（报单币数量）
            #endregion
            #region 验证
            if (memberextend.FormCurreyNum < count)
            {
                return "余额不足，无法完成转账";
            }
            #endregion
            using (TransactionScope scope = new TransactionScope())
            {
                int rowcount=MemberExtendInfoDAL.UpdateMemberFormCurrey(0-count,sourcememberid,"转账支出"+count+"个报单币");
                if(rowcount<1)
                {
                    return "操作失败";
                }
                rowcount=MemberExtendInfoDAL.UpdateMemberFormCurrey(count,sourcememberid,"接收转入"+count+"个报单币");
                if(rowcount<1)
                {
                    return "操作失败";
                }
                scope.Complete();
            }
            return result;
        }
        /// <summary>
        /// 会员间转赠游戏币
        /// </summary>
        /// <param name="sourcememberid"></param>
        /// <param name="acceptmemberid"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public string MemberTransferGameCurrency(int sourcememberid, int count, int acceptmemberid = 0, string acceptmembercode = "")
        {
            string result = "1";
            #region 检验会员信息
            MemberInfoModel AcceptMemberInfo = null;
            if (acceptmemberid == 0 && !string.IsNullOrWhiteSpace(acceptmembercode))
            {
                AcceptMemberInfo = MemberDAL.GetBriefSingleMemberModel(acceptmembercode);
                if (AcceptMemberInfo != null)
                {
                    acceptmemberid = AcceptMemberInfo.ID;
                }
                else
                {
                    return "没有找到对应的接受会员";
                }
            }
            #endregion
            #region 查询会员的资产信息
            MemberCapitalDetailModel sourcemembercapital = MemberCapitalDetailDAL.GetMemberCapitalDetailByMemberID(sourcememberid);//转出会员的资产信息
            #endregion
            #region 验证
            if (sourcemembercapital.GameCurrency < count)
            {
                return "转出金额不足，无法完成";
            }
            #endregion
            using (TransactionScope scope = new TransactionScope())
            {
                int rowcount = MemberCapitalDetailDAL.UpdateGameCurrency(0 - count, "转账支出"+count+"个游戏币", sourcememberid);
                if (rowcount < 1)
                {
                    return "操作失败";
                }
                rowcount = MemberCapitalDetailDAL.UpdateGameCurrency(count, "接收转入"+count+"个游戏币", acceptmemberid);
                if (rowcount < 1)
                {
                    return "操作失败";
                }
                scope.Complete();
            }            
            return result;
        }
        /// <summary>
        /// 会员间转赠股权币
        /// </summary>
        /// <param name="sourcememberid"></param>
        /// <param name="acceptmemberid"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public string MemberTransferSharesCurrency(int sourcememberid, int count, int acceptmemberid = 0, string acceptmembercode = "")
        {
            string result = "1";
            #region 检验会员信息
            MemberInfoModel AcceptMemberInfo = null;
            if (acceptmemberid == 0 && !string.IsNullOrWhiteSpace(acceptmembercode))
            {
                AcceptMemberInfo = MemberDAL.GetBriefSingleMemberModel(acceptmembercode);
                if (AcceptMemberInfo != null)
                {
                    acceptmemberid = AcceptMemberInfo.ID;
                }
                else
                {
                    return "没有找到对应的接受会员";
                }
            }
            #endregion
            #region 查询会员的资产信息
            MemberCapitalDetailModel sourcemembercapital = MemberCapitalDetailDAL.GetMemberCapitalDetailByMemberID(sourcememberid);//转出会员的资产信息
            #endregion
            #region 验证
            if (sourcemembercapital.SharesCurrency < count)
            {
                return "转出金额不足，无法完成";
            }
            #endregion
            using (TransactionScope scope = new TransactionScope())
            {
                int rowcount = MemberCapitalDetailDAL.UpdateSharesCurrency(0 - count, "转账支出" + count + "个股权币", sourcememberid);
                if (rowcount < 1)
                {
                    return "操作失败";
                }
                rowcount = MemberCapitalDetailDAL.UpdateSharesCurrency(count, "接收转入" + count + "个股权币", acceptmemberid);
                if (rowcount < 1)
                {
                    return "操作失败";
                }
                scope.Complete();
            }
            return result;
        }
        /// <summary>
        /// 会员间转赠购物币
        /// </summary>
        /// <param name="sourcememberid"></param>
        /// <param name="acceptmemberid"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public string MemberTransferShoppingCurrency(int sourcememberid, int count, int acceptmemberid = 0, string acceptmembercode = "")
        {
            string result = "1";
            #region 检验会员信息
            MemberInfoModel AcceptMemberInfo = null;
            if (acceptmemberid == 0 && !string.IsNullOrWhiteSpace(acceptmembercode))
            {
                AcceptMemberInfo = MemberDAL.GetBriefSingleMemberModel(acceptmembercode);
                if (AcceptMemberInfo != null)
                {
                    acceptmemberid = AcceptMemberInfo.ID;
                }
                else
                {
                    return "没有找到对应的接受会员";
                }
            }
            #endregion
            #region 查询会员的资产信息
            MemberCapitalDetailModel sourcemembercapital = MemberCapitalDetailDAL.GetMemberCapitalDetailByMemberID(sourcememberid);//转出会员的资产信息
            #endregion
            #region 验证
            if (sourcemembercapital.ShoppingCurrency < count)
            {
                return "转出金额不足，无法完成";
            }
            #endregion
            using (TransactionScope scope = new TransactionScope())
            {
                int rowcount = MemberCapitalDetailDAL.UpdateShoppingCurrency(0 - count, "转账支出" + count + "个购物币", sourcememberid);
                if (rowcount < 1)
                {
                    return "操作失败";
                }
                rowcount = MemberCapitalDetailDAL.UpdateShoppingCurrency(count, "接收转入" + count + "个购物币", acceptmemberid);
                if (rowcount < 1)
                {
                    return "操作失败";
                }
                scope.Complete();
            }
            return result;
        }
        /// <summary>
        /// 会员间转赠复利币
        /// </summary>
        /// <param name="sourcememberid"></param>
        /// <param name="acceptmemberid"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public string MemberTransferCompoundCurrency(int sourcememberid,  int count, int acceptmemberid = 0, string acceptmembercode = "")
        {
            string result = "1";
            #region 检验会员信息
            MemberInfoModel AcceptMemberInfo = null;
            if (acceptmemberid == 0 && !string.IsNullOrWhiteSpace(acceptmembercode))
            {
                AcceptMemberInfo = MemberDAL.GetBriefSingleMemberModel(acceptmembercode);
                if (AcceptMemberInfo != null)
                {
                    acceptmemberid = AcceptMemberInfo.ID;
                }
                else
                {
                    return "没有找到对应的接受会员";
                }
            }
            #endregion
            #region 查询会员的资产信息
            MemberCapitalDetailModel sourcemembercapital = MemberCapitalDetailDAL.GetMemberCapitalDetailByMemberID(sourcememberid);//转出会员的资产信息
            #endregion
            #region 验证
            if (sourcemembercapital.CompoundCurrency < count)
            {
                return "转出金额不足，无法完成";
            }
            #endregion
            using (TransactionScope scope = new TransactionScope())
            {
                int rowcount = MemberCapitalDetailDAL.UpdateCompoundCurrency(0 - count, "转账支出" + count + "个复利币", sourcememberid);
                if (rowcount < 1)
                {
                    return "操作失败";
                }
                rowcount = MemberCapitalDetailDAL.UpdateCompoundCurrency(count, "接收转入" + count + "个复利币", acceptmemberid);
                if (rowcount < 1)
                {
                    return "操作失败";
                }
                scope.Complete();
            }
            return result;
        }
        /// <summary>
        /// 会员间转赠会员积分（转致对方报单币账户）
        /// </summary>
        /// <param name="sourcememberid"></param>
        /// <param name="acceptmemberid"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public string MemberTransferMemberPoints(int sourcememberid, int count, int acceptmemberid=0,string acceptmembercode="")
        {
            string result = "1";
            #region 检验会员信息
            MemberInfoModel AcceptMemberInfo = null;
            if (acceptmemberid == 0 && !string.IsNullOrWhiteSpace(acceptmembercode))
            {
                AcceptMemberInfo = MemberDAL.GetBriefSingleMemberModel(acceptmembercode);
                if (AcceptMemberInfo != null)
                {
                    acceptmemberid = AcceptMemberInfo.ID;
                }
                else
                {
                    return "没有找到对应的接受会员";
                }
            }
            #endregion
            #region 查询会员的资产信息
            MemberCapitalDetailModel sourcemembercapital = MemberCapitalDetailDAL.GetMemberCapitalDetailByMemberID(sourcememberid);//转出会员的资产信息
            #endregion
            #region 验证
            if (sourcemembercapital.MemberPoints < count)
            {
                return "转出金额不足，无法完成";
            }
            #endregion
            using (TransactionScope scope = new TransactionScope())
            {
                int rowcount = MemberCapitalDetailDAL.UpdateMemberPoints(0 - count, "转账支出" + count + "个积分", sourcememberid);
                if (rowcount < 1)
                {
                    return "操作失败";
                }
                rowcount = MemberExtendInfoDAL.UpdateMemberFormCurrey(count, acceptmemberid, "接收转入" + count + "个报单币");
                if (rowcount < 1)
                {
                    return "操作失败";
                }
                scope.Complete();
            }
            return result;
        }
    }
}

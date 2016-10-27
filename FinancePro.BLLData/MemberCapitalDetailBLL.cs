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
        /// 会员转账
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string MemberTransferOrder(MemberTransferOrderModel model)
        {
            string result = "";
            if (string.IsNullOrWhiteSpace(model.ReceiveMemberCode))
            {
                return "没有传入接受会员";
            }
            if (model.TransferNumber < 1)
            {
                return "转账金额不正确";
            }
            MemberInfoModel receivemember = MemberDAL.GetBriefSingleMemberModel(model.ReceiveMemberCode);
            return result;
        }
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
                int rowcount = MemberExtendInfoDAL.UpdateMemberFormCurrey(0 - count, sourcememberid, "转账支出" + count + "个报单币");
                if (rowcount < 1)
                {
                    return "操作失败";
                }
                rowcount = MemberExtendInfoDAL.UpdateMemberFormCurrey(count, sourcememberid, "接收转入" + count + "个报单币");
                if (rowcount < 1)
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
                int rowcount = MemberCapitalDetailDAL.UpdateGameCurrency(0 - count, "转账支出" + count + "个游戏币", sourcememberid);
                if (rowcount < 1)
                {
                    return "操作失败";
                }
                rowcount = MemberCapitalDetailDAL.UpdateGameCurrency(count, "接收转入" + count + "个游戏币", acceptmemberid);
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
        public string MemberTransferCompoundCurrency(int sourcememberid, int count, int acceptmemberid = 0, string acceptmembercode = "")
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
        public string MemberTransferMemberPoints(int sourcememberid, int count, int acceptmemberid = 0, string acceptmembercode = "")
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
        /// <summary>
        /// 系统派发报单币
        /// </summary>
        /// <param name="membercode"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public string SystemDistributeFormCurrey(int memberid, int count)
        {
            string result = "1";
            #region 查询会员信息
            MemberInfoModel member = MemberDAL.GetBriefSingleMemberModel(memberid);
            if (member == null)
            {
                return "没有查询到此会员信息";
            }
            if (member.MemberStatus != 2)
            {
                return "会员状态不正确，无法为其派发";
            }
            int sysfcount = SystemConfigsBLL.GetConfigsValueByID(1).ParseToInt(0);
            #endregion
            #region 判断逻辑
            if (count > sysfcount)
            {
                return "系统报单币不足以完成本次操作";
            }
            #endregion
            using (TransactionScope scope = new TransactionScope())
            {
                FormCurreyLogModel formcurreylogmodel = new FormCurreyLogModel();
                formcurreylogmodel.FormCurreyNum = count;
                formcurreylogmodel.MemberID = member.ID;
                formcurreylogmodel.MemberName = member.MemberName;
                formcurreylogmodel.MemberCode = member.MemberCode;
                if (count > 0)
                    formcurreylogmodel.Remark = "为会员" + member.MemberName + ":" + member.MemberCode + "派发" + count + "个报单币";
                else
                    formcurreylogmodel.Remark = "惩罚会员" + member.MemberName + ":" + member.MemberCode + count + "个报单币";
                int rowcount = FormCurreyDAL.UpdateDeductionFormCurrey(formcurreylogmodel);
                if (rowcount < 1)
                {
                    return "操作失败";
                }
                MemberFormCurreyLogModel memberformcurreylogmodel = new MemberFormCurreyLogModel();
                memberformcurreylogmodel.NFormCurreyNum = count;
                if (count > 0)
                    memberformcurreylogmodel.Remark = "得到系统派发" + count + "个报单币";
                else
                    memberformcurreylogmodel.Remark = "被系统惩罚" + count + "个报单币";
                memberformcurreylogmodel.MemberID = member.ID;
                rowcount = FormCurreyDAL.AddNewMemberFormCurrey(memberformcurreylogmodel);
                if (rowcount < 1)
                {
                    return "操作失败";
                }
                scope.Complete();
            }
            return result;
        }
        /// <summary>
        /// 查询会员的报单币信息
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public List<MemberExtendInfoModel> GetMemberExtendInfoForPage(MemberExtendInfoModel searchmodel, out int totalrowcount)
        {
            return MemberExtendInfoDAL.GetMemberExtendInfoForPage(searchmodel, out totalrowcount);
        }
        /// <summary>
        /// 分页查询会员的资产信息
        /// </summary>
        /// <param name="searchmodel"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public List<MemberCapitalDetailModel> GetMemberCapitalDetailForPage(MemberCapitalDetailModel searchmodel, out int totalrowcount)
        {
            return MemberCapitalDetailDAL.GetMemberCapitalDetailForPage(searchmodel, out totalrowcount);
        }
        /// <summary>
        /// 奖励和惩罚会员的资产信息
        /// </summary>
        /// <param name="memberid">会员ID</param>
        /// <param name="operatetype">操作类型（1 奖励 2 惩罚）</param>
        /// <param name="count">数量</param>
        /// <param name="moneytype">资产类型</param>
        /// <returns></returns>
        public int RewardAndPunishmentMember(int memberid, int operatetype, decimal count, int moneytype, string remark)
        {
            int result = 0;
            if (operatetype == 2)
            {
                count = 0 - count;
            }
            if (string.IsNullOrWhiteSpace(remark))
            {
                if (operatetype == 1)
                {
                    remark = "系统奖励";
                }
                if (operatetype == 2)
                {
                    remark = "系统惩罚";
                }
            }
            switch (moneytype)
            {
                case 1://游戏币
                    result = MemberCapitalDetailDAL.UpdateGameCurrency(count, remark, memberid);
                    break;
                case 2://股权币
                    result = MemberCapitalDetailDAL.UpdateSharesCurrency(count, remark, memberid);
                    break;
                case 3://购物币
                    result = MemberCapitalDetailDAL.UpdateShoppingCurrency(count, remark, memberid);
                    break;
                case 4://会员积分
                    result = MemberCapitalDetailDAL.UpdateMemberPoints(count, remark, memberid);
                    break;
                case 5://复利币
                    result = MemberCapitalDetailDAL.UpdateCompoundCurrency(count, remark, memberid);
                    break;
                default:
                    break;
            }
            return result;
        }
        /// <summary>
        /// 追加系统报单币
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool AddSystemFormCurrey(int count)
        {
            return FormCurreyDAL.AddNewFormCurrey(count);
        }
        /// <summary>
        /// 根据会员ID查询会员的资产信息
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public  MemberCapitalDetailModel GetMemberCapitalDetailByMemberID(int memberID)
        {
            return MemberCapitalDetailDAL.GetMemberCapitalDetailByMemberID(memberID);
        }
    }
}

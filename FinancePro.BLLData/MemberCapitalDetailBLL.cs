﻿using System;
using System.Collections.Generic;
using System.Data;
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
        public string AddNewMemberTransferOrder(MemberTransferOrderModel model)
        {
            string result = "";
            #region 验证信息
            int feetype = SystemConfigsBLL.GetConfigsValueByID(25).ParseToInt(0);
            decimal transfernumber = 0;
            if (feetype == model.TransferType)
            {
                if (model.CounterFee != (model.Transfernum * (decimal)0.3))
                {
                    return "手续费用计算不正确";
                }
                transfernumber = model.Transfernum;
            }
            else
            {
                transfernumber = model.TransferNumber;
            }
            if (string.IsNullOrWhiteSpace(model.ReceiveMemberCode))
            {
                return "没有传入接受会员";
            }
            if (model.TransferNumber < 1)
            {
                return "转账金额不正确";
            }
            MemberInfoModel receivemember = MemberDAL.GetBriefSingleMemberModel(model.ReceiveMemberCode);
            if (receivemember == null)
            {
                return "没有找到接受会员";
            }
            if (model.LaunchMemberID == receivemember.ID)
            {
                return "不能为自己转账";
            }
            if (model.TransferType == 1)
            {
                MemberExtendInfoModel extendmodel = MemberExtendInfoDAL.GetMemberExtendInfoByMemberID(model.LaunchMemberID);//会员扩展信息（报单币数量）
                if (extendmodel.FormCurreyNum < model.TransferNumber)
                {
                    return "账户余额不足";
                }
            }
            else
            {
                MemberCapitalDetailModel sourcemembercapital = MemberCapitalDetailDAL.GetMemberCapitalDetailByMemberID(model.LaunchMemberID);
                switch (model.TransferType)
                {
                    case 2://积分
                        if (sourcemembercapital.MemberPoints < transfernumber)
                        {
                            return "账户余额不足";
                        }
                        break;
                    case 3://购物
                        if (sourcemembercapital.ShoppingCurrency < transfernumber)
                        {
                            return "账户余额不足";
                        }
                        break;
                    case 4://股权
                        if (sourcemembercapital.SharesCurrency < transfernumber)
                        {
                            return "账户余额不足";
                        }
                        break;
                    case 5://复利
                        if (sourcemembercapital.CompoundCurrency < transfernumber)
                        {
                            return "账户余额不足";
                        }
                        break;
                    case 6://游戏
                        if (sourcemembercapital.GameCurrency < transfernumber)
                        {
                            return "账户余额不足";
                        }
                        break;
                    default:
                        break;
                }

            }
            #endregion
            using (TransactionScope scope = new TransactionScope())
            {
                int rowcount = 0;
                #region 扣减/追加

                switch (model.TransferType)
                {
                    case 1://报单币
                        rowcount = MemberExtendInfoDAL.UpdateMemberFormCurrey((0 - transfernumber), model.LaunchMemberID, "转账支出" + transfernumber + "个报单币");
                        if (rowcount < 1)
                        {
                            return "操作失败";
                        }
                        rowcount = MemberExtendInfoDAL.UpdateMemberFormCurrey(model.TransferNumber, receivemember.ID, "接收转入" + model.TransferNumber + "个报单币");
                        if (rowcount < 1)
                        {
                            return "操作失败";
                        }
                        break;
                    case 2://积分
                        rowcount = MemberCapitalDetailDAL.UpdateMemberPoints(0 - transfernumber, "转账支出" + transfernumber + "个积分", model.LaunchMemberID);
                        if (rowcount < 1)
                        {
                            return "操作失败";
                        }
                        rowcount = MemberExtendInfoDAL.UpdateMemberFormCurrey(model.TransferNumber, receivemember.ID, "接收转入" + model.TransferNumber + "个报单币");
                        if (rowcount < 1)
                        {
                            return "操作失败";
                        }
                        break;
                    case 3://购物
                        rowcount = MemberCapitalDetailDAL.UpdateShoppingCurrency(0 - transfernumber, "转账支出" + transfernumber + "个购物币", model.LaunchMemberID);
                        if (rowcount < 1)
                        {
                            return "操作失败";
                        }
                        rowcount = MemberCapitalDetailDAL.UpdateShoppingCurrency(model.TransferNumber, "接收转入" + model.TransferNumber + "个购物币", receivemember.ID);
                        if (rowcount < 1)
                        {
                            return "操作失败";
                        }
                        break;
                    case 4://股权
                        rowcount = MemberCapitalDetailDAL.UpdateSharesCurrency(0 - transfernumber, "转账支出" + transfernumber + "个股权币", model.LaunchMemberID);
                        if (rowcount < 1)
                        {
                            return "操作失败";
                        }
                        rowcount = MemberCapitalDetailDAL.UpdateSharesCurrency(model.TransferNumber, "接收转入" + model.TransferNumber + "个股权币", receivemember.ID);
                        if (rowcount < 1)
                        {
                            return "操作失败";
                        }
                        break;
                    case 5://复利
                        rowcount = MemberCapitalDetailDAL.UpdateCompoundCurrency(0 - transfernumber, "转账支出" + transfernumber + "个复利币", model.LaunchMemberID);
                        if (rowcount < 1)
                        {
                            return "操作失败";
                        }
                        rowcount = MemberCapitalDetailDAL.UpdateCompoundCurrency(model.TransferNumber, "接收转入" + model.TransferNumber + "个复利币", receivemember.ID);
                        if (rowcount < 1)
                        {
                            return "操作失败";
                        }
                        break;
                    case 6://游戏
                        rowcount = MemberCapitalDetailDAL.UpdateGameCurrency(0 - transfernumber, "转账支出" + transfernumber + "个游戏币", model.LaunchMemberID);
                        if (rowcount < 1)
                        {
                            return "操作失败";
                        }
                        rowcount = MemberCapitalDetailDAL.UpdateGameCurrency(model.TransferNumber, "接收转入" + model.TransferNumber + "个游戏币", receivemember.ID);
                        if (rowcount < 1)
                        {
                            return "操作失败";
                        }
                        break;
                    default:
                        break;
                }
                #endregion
                #region 加入单据记录
                rowcount = MemberTransferOrderDAL.AddNewMemberTransferOrder(model);
                if (rowcount < 1)
                {
                    return "追加单据日志失败";
                }
                #endregion
                scope.Complete();
            }
            return result;
        }
        /// <summary>
        /// 会员间转赠报单币
        /// </summary>
        /// <param name="sourcememberid">转出账户</param>
        /// <param name="acceptmemberid">接收账户</param>
        /// <param name="count">数量</param>
        /// <returns></returns>
        public string MemberTransferFormCurreyNum(int sourcememberid, int acceptmemberid, decimal count)
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
                int rowcount = MemberExtendInfoDAL.UpdateMemberFormCurrey((decimal)0 - count, sourcememberid, "转账支出" + count + "个报单币");
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
        public string SystemDistributeFormCurrey(int memberid, decimal count)
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
        public MemberCapitalDetailModel GetMemberCapitalDetailByMemberID(int memberID)
        {
            return MemberCapitalDetailDAL.GetMemberCapitalDetailByMemberID(memberID);
        }
        /// <summary>
        /// 动态分配领导奖金
        /// </summary>
        /// <returns></returns>
        public string DistributeDynamicBonus()
        {
            int baseProportion = SystemConfigsBLL.GetConfigsValueByID(17).ParseToInt(300);//一期网站计算基数
            string distributionList = SystemConfigsBLL.GetConfigsValueByID(7);//动态分配比例值(%)
            int recommendProportion = SystemConfigsBLL.GetConfigsValueByID(2).ParseToInt(0);//推荐奖比例(%)
            int leaderProportion = SystemConfigsBLL.GetConfigsValueByID(3).ParseToInt(0);//领导奖比例(%)
            int reportProportion = SystemConfigsBLL.GetConfigsValueByID(4).ParseToInt(0);//报单奖比例(%)
            decimal syscost = SystemConfigsBLL.GetConfigsValueByID(11).ParseToDecimal(0);//平台管理费用
            decimal maxcommery = SystemConfigsBLL.GetConfigsValueByID(5).ParseToDecimal(0);//自动创建账户封顶复利币
            //查询所有的常规会员
            DataTable dt = MemberDAL.GetAllSimpleMembers();
            if (dt.Rows.Count < 1)
            {
                return "没有需要处理的会员";
            }
            //循环会员直行分配
            foreach (DataRow subitem in dt.Rows)
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        int memberid = subitem["ID"].ToString().ParseToInt(0);
                        string membercode = subitem["MemberCode"].ToString();
                        string membername = subitem["MemberName"].ToString();
                        List<MemberIterationInfoModel> recommendlist = MemberIterationInfoDAL.GetMemberIterationInfoByMemberId(memberid);
                        bool isreport = true;
                        if (recommendlist != null)
                        {
                            foreach (MemberIterationInfoModel item in recommendlist)
                            {
                                decimal totalnum = 0;
                                //查询是否报单中心
                                bool report = MemberDAL.GetMemberIsReportMember(item.SuperiorMemberID);
                                //查询会员的直推人数
                                int linerecommend = ReMemberRelationDAL.GetReMemberRelationCountByMemberid(item.SuperiorMemberID);
                                if (linerecommend < item.GenerationNum)
                                {
                                    continue;//若直推人数小于当前世代数，则无权拿到本次奖励
                                }
                                //if (item.GenerationNum == 1)//若为第一代，则需要计算推荐奖//推荐奖在激活的时候返还
                                //{
                                //    totalnum += (baseProportion * 0.015 * recommendProportion / 100).ToString().ParseToDecimal(0);
                                //}
                                totalnum += (baseProportion * 0.015 * leaderProportion / 100).ToString().ParseToDecimal(0);
                                if (report && isreport)
                                {
                                    totalnum += (baseProportion * 0.015 * reportProportion / 100).ToString().ParseToDecimal(0);
                                    isreport = false;
                                }
                                if (totalnum > 0)
                                {
                                    string[] listarry = distributionList.TrimEnd(',').Split(',');//依次为(积分,游戏币,购物币,股权币,复利币)
                                    if (listarry.Length < 5)
                                    {
                                        return "操作失败";
                                    }
                                    DynamicRewardModel dynamicmodel = new DynamicRewardModel();
                                    dynamicmodel.CompoundCurrency = totalnum * listarry[4].ParseToDecimal(0) / 100;
                                    dynamicmodel.GameCurrency = totalnum * listarry[1].ParseToDecimal(0) / 100;
                                    dynamicmodel.LStatus = 1;
                                    dynamicmodel.MemberID = item.SuperiorMemberID;
                                    dynamicmodel.MemberName = item.SuperiorMemberName;
                                    dynamicmodel.MemberPoints = totalnum * listarry[0].ParseToDecimal(0) / 100;
                                    dynamicmodel.SharesCurrency = totalnum * listarry[3].ParseToDecimal(0) / 100;
                                    dynamicmodel.ShoppingCurrency = totalnum * listarry[2].ParseToDecimal(0) / 100;
                                    dynamicmodel.SourceMemberID = memberid;
                                    dynamicmodel.SourceMemberName = membername;
                                    dynamicmodel.Ltype = 2;
                                    //加入待充值表
                                    int drows = DynamicRewardDAL.AddNewDynamicReward(dynamicmodel);
                                    if (drows < 1)
                                    {
                                        return "操作失败";
                                    }
                                }
                            }
                            //释放动态奖金
                            int rowcount = DynamicRewardDAL.ReleaseDynamicRewardByType(memberid, 2, "得到来自会员的注册激活动态奖励");
                            //扣减平台管理费
                            rowcount = MemberCapitalDetailDAL.UpdateMemberISDeSysCostFromDynamicReward(memberid, syscost);
                            //更改释放状态
                            rowcount = DynamicRewardDAL.UpdateDynamicRewardStatusByType(memberid, 2);
                            //创建终极账户
                            rowcount = MemberDAL.AddNewMemberInfoByDynamicReward(memberid, maxcommery);
                        }
                        scope.Complete();
                    }
                }
                catch { continue; }
            }
            return "1";
        }
    }
}

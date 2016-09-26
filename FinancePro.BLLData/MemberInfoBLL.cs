using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using FinancePro.Common;
using FinancePro.DALData;
using FinancePro.DataModels;

namespace FinancePro.BLLData
{
    public class MemberInfoBLL
    {
        /// <summary>
        /// 注册新会员
        /// </summary>
        /// <param name="model"></param>
        /// <param name="type">正常注册会员</param>
        /// <returns></returns>
        public string AddNewMemberInfo(MemberInfoModel model, int type)
        {
            string result = "1";
            #region 读取系统配置信息
            int maxsamemembercount = SystemConfigsBLL.GetConfigsValueByID(9).ParseToInt(10);//同一身份允许最多出现账户数
            string secrectstr = SystemConfigsBLL.GetConfigsValueByID(16);//网站加密字符串
            int reportnumbernum = SystemConfigsBLL.GetConfigsValueByID(8).ParseToInt(9);//成为报单中心所需直推人数
            int recommendProportion = SystemConfigsBLL.GetConfigsValueByID(2).ParseToInt(0);//推荐奖比例(%)
            int leaderProportion = SystemConfigsBLL.GetConfigsValueByID(3).ParseToInt(0);//领导奖比例(%)
            int reportProportion = SystemConfigsBLL.GetConfigsValueByID(4).ParseToInt(0);//报单奖比例(%)
            int baseProportion = SystemConfigsBLL.GetConfigsValueByID(17).ParseToInt(300);//一期网站计算基数
            string distributionList = SystemConfigsBLL.GetConfigsValueByID(7);//动态分配比例值(%)
            #endregion
            #region 注册前验证
            //验证会员信息
            if (string.IsNullOrWhiteSpace(model.MemberIDNumber) || string.IsNullOrWhiteSpace(model.MemberName) || string.IsNullOrWhiteSpace(model.MemberBankCode) || string.IsNullOrWhiteSpace(model.MemberBankName))
            {
                return "会员信息不完整！";
            }
            MemberInfoModel recommendmember = MemberDAL.GetBriefSingleMemberModel(model.SourceMemberCode);
            if (type == 1)//若注册为标准用户
            {
                //验证身份证编号是否已经超过限制
                if (MemberDAL.GetMemberCountByMemberIDNumber(model.MemberIDNumber) > maxsamemembercount)
                {
                    return "此会员身份信息已经注册过多次";
                }
                if (recommendmember == null)
                {
                    return "推荐会员编号无效";
                }
                if (recommendmember.MemberStatus == 3)
                {
                    return "推荐账户已被注销";
                }
            }
            #endregion
            #region 插入之前处理会员信息
            model.MemberLogPwd = DESEncrypt.Encrypt("666666", secrectstr);//加密网站登陆密码
            string code = "";
            if (type == 1)//标准会员,编号为14位
            {
                code = CreateNewCode();
                model.SourceMemberCode = "";
            }
            else if (type == 2)//衍生账户
            {
                code = CreateNewCode(model.SourceMemberCode);
                model.IsDerivativeMember = 1;
            }
            else //终极账户
            {
                code = model.SourceMemberCode + "-1";
                model.IsFinalMember = 1;
            }
            model.MemberType = type;
            model.MemberCode = code;
            #endregion
            using (TransactionScope scope = new TransactionScope())
            {
                //扣减会员激活用的
                //插入会员表
                int memberid = MemberDAL.AddNewMember(model);
                if (memberid < 1)
                {
                    return "操作失败";
                }
                //初始化关联新表
                MemberExtendInfoModel extendmodel = new MemberExtendInfoModel();
                extendmodel.FormCurreyNum = 0;
                extendmodel.MemberCode = model.MemberCode;
                extendmodel.MemberID = memberid;
                extendmodel.MemberName = model.MemberName;
                MemberExtendInfoDAL.AddNewMemberExtendInfo(extendmodel);
                MemberCapitalDetailModel capitalmodel = new MemberCapitalDetailModel();
                capitalmodel.CompoundCurrency = 0;
                capitalmodel.GameCurrency = 0;
                capitalmodel.MemberCode = model.MemberCode;
                capitalmodel.MemberID = memberid;
                capitalmodel.MemberName = model.MemberName;
                capitalmodel.MemberPoints = 0;
                capitalmodel.SharesCurrency = 0;
                capitalmodel.ShoppingCurrency = 0;
                MemberCapitalDetailDAL.AddNewMemberCapitalDetail(capitalmodel);
                if (type == 1)
                {
                    //插入推荐表
                    ReMemberRelationModel recommendmodel = new ReMemberRelationModel();
                    recommendmodel.BeRecommMemberCode = model.MemberCode;
                    recommendmodel.BeRecommMemberID = memberid;
                    recommendmodel.BeRecommMemberName = model.MemberName;
                    recommendmodel.RecommendMemberCode = recommendmember.MemberCode;
                    recommendmodel.RecommendMemberID = recommendmember.ID;
                    recommendmodel.RecommendMemberName = recommendmember.MemberName;
                    recommendmodel.RecommendStatus = 1;
                    recommendmodel.RecommendTime = DateTime.Now;
                    int rowcount = ReMemberRelationDAL.AddNewReMemberRelation(recommendmodel);
                    if (rowcount < 1)
                    {
                        return "操作失败";
                    }
                    //插入世代信息表
                    int upmemberid = memberid;
                    int dai = 1;
                    do
                    {
                        ReMemberRelationModel relationmodel = ReMemberRelationDAL.GetReMemberRelationByBeRecommMemberID(upmemberid);
                        if (relationmodel == null)
                        {
                            break;
                        }
                        else
                        {
                            upmemberid = relationmodel.RecommendMemberID;
                        }
                        MemberIterationInfoModel iterationmodel = new MemberIterationInfoModel();
                        iterationmodel.GenerationNum = dai;
                        iterationmodel.MemberCode = model.MemberCode;
                        iterationmodel.MemberID = memberid;
                        iterationmodel.MemberName = model.MemberName;
                        iterationmodel.RAddTime = DateTime.Now;
                        iterationmodel.RStatus = 1;
                        iterationmodel.SuperiorMemberCode = relationmodel.RecommendMemberCode;
                        iterationmodel.SuperiorMemberID = relationmodel.RecommendMemberID;
                        iterationmodel.SuperiorMemberName = relationmodel.RecommendMemberName;
                        int rows = MemberIterationInfoDAL.AddNewMemberIterationInfo(iterationmodel);
                        if (rows < 1)
                        {
                            return "操作失败";
                        }
                        dai++;
                    }
                    while (upmemberid > 0);
                }
                //判断上级是否达到报单中心，若达到则修改性质
                int recommendnum = ReMemberRelationDAL.GetReMemberRelationCountByMemberid(recommendmember.ID);
                if (recommendnum >= reportnumbernum)
                {
                    int rows = MemberDAL.UpdateMemberIsReportMember(recommendmember.ID, 1);
                    if (rows < 1)
                    {
                        return "操作失败";
                    }
                }
                //计算动态的资金分配，记入DynamicReward表
                List<MemberIterationInfoModel> recommendlist = MemberIterationInfoDAL.GetMemberIterationInfoByMemberId(memberid);
                bool isreport = true;
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
                    if (item.GenerationNum == 1)//若为第一代，则需要计算推荐奖
                    {
                        totalnum += baseProportion * recommendProportion / 100;
                    }
                    totalnum += baseProportion * leaderProportion / 100;
                    if (report && isreport)
                    {
                        totalnum += baseProportion * reportProportion / 100;
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
                        dynamicmodel.MemberID = item.MemberID;
                        dynamicmodel.MemberName = item.MemberName;
                        dynamicmodel.MemberPoints = totalnum * listarry[0].ParseToDecimal(0) / 100;
                        dynamicmodel.SharesCurrency = totalnum * listarry[3].ParseToDecimal(0) / 100;
                        dynamicmodel.ShoppingCurrency = totalnum * listarry[2].ParseToDecimal(0) / 100;
                        dynamicmodel.SourceMemberID = memberid;
                        dynamicmodel.SourceMemberName = model.MemberName;
                        //加入待充值表
                        int drows = DynamicRewardDAL.AddNewDynamicReward(dynamicmodel);
                        if (drows < 1)
                        {
                            return "操作失败";
                        }
                    }
                }
                scope.Complete();
            }
            return result;
        }
        /// <summary>
        /// 激活会员
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public string ActiveMenber(int memberid)
        {
            string result = "";
            #region 读取系统配置信息
            int pointProportion = SystemConfigsBLL.GetConfigsValueByID(18).ParseToInt(0);//静态分配积分占比(%)
            int gameProportion = SystemConfigsBLL.GetConfigsValueByID(19).ParseToInt(0);//静态分配游戏币占比(%)
            int baseProportion = SystemConfigsBLL.GetConfigsValueByID(17).ParseToInt(300);//一期网站计算基数
            #endregion
            #region 读取会员信息
            MemberInfoModel member = MemberDAL.GetBriefSingleMemberModel(memberid);//该会员信息
            ReMemberRelationModel relationmember = null;//推荐人信息
            MemberInfoModel SourceMemberInfo = null;//来源会员信息
            if (member.MemberType == 1)
            {
                relationmember = ReMemberRelationDAL.GetReMemberRelationByBeRecommMemberID(memberid);//上级推荐人信息
            }
            if (member.MemberType == 2)
            {
                SourceMemberInfo = MemberDAL.GetBriefSingleMemberModel(member.SourceMemberCode);
            }
            MemberCapitalDetailModel memberCapital = null;//查询会员的资产信息
            if (relationmember != null)
            {
                memberCapital = MemberCapitalDetailDAL.GetMemberCapitalDetailByMemberID(relationmember.RecommendMemberID);
            }
            if (SourceMemberInfo != null)
            {
                memberCapital = MemberCapitalDetailDAL.GetMemberCapitalDetailByMemberID(SourceMemberInfo.ID);
            }
            #endregion
            using (TransactionScope scope = new TransactionScope())
            {
                //更改会员状态
                int rowcount = MemberDAL.UpdateMemberStatus(memberid, 2);
                if (rowcount < 1)
                {
                    return "操作失败";
                }
                if (member.MemberType == 2)//如果为衍生账户
                {
                    //激活来源账户的附属账户
                    //扣除来源账户的游戏币和复利币
                }
                else if (member.MemberType == 1)//扣减推荐人的报单币 复利币和游戏币
                {
                    //扣减推荐人的报单币 复利币和游戏币
                }
                //释放动态奖金
                rowcount = DynamicRewardDAL.ReleaseDynamicReward(memberid, "得到来自会员的注册动态奖励");
                if (rowcount < 1)
                {
                    return "操作失败";
                }
                rowcount = DynamicRewardDAL.UpdateDynamicRewardStatus(memberid);
                if (rowcount < 1)
                {
                    return "操作失败";
                }
                //计算静态资金并返还
                decimal gamecurrey = baseProportion * pointProportion / 100;
                decimal commingProportion = baseProportion * gameProportion / 100;
                rowcount = MemberCapitalDetailDAL.UpdateCompoundCurrencyAndGameCurrency(gamecurrey, commingProportion, "注册返还静态奖励金额", memberid);
                scope.Complete();
            }
            return result;
        }
        /// <summary>
        /// 产生新会员编号
        /// </summary>
        /// <returns></returns>
        private string CreateNewCode()
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            int re = new Random().Next(1111, 1899);
            string code = DateTime.Now.ToString("HHmmss") + (year + re).ToString() + (month + 87).ToString() + (day + 66).ToString();
            return code;
        }
        /// <summary>
        /// 产生新会员编号
        /// </summary>
        /// <returns></returns>
        private string CreateNewCode(string oldnumber)
        {
            if (string.IsNullOrWhiteSpace(oldnumber))
            {
                return "";
            }
            if (oldnumber.Length == 14)
            {
                return oldnumber + "1";
            }
            else if (oldnumber.Contains("-"))
            {
                int pindex = oldnumber.IndexOf('-');
                string oldnum = oldnumber.Substring(0, 14);
                string fullstr = oldnumber.Substring(0, pindex);
                if (fullstr.Length == 14)
                {
                    return oldnum + "1";
                }
                else
                {
                    int num = (fullstr.Replace(oldnum, "")).ParseToInt(0);
                    return oldnum + (num + 1).ToString();
                }
            }
            else
            {
                string oldnum = oldnumber.Substring(0, 14);
                int num = (oldnumber.Replace(oldnum, "")).ParseToInt(0);
                return oldnum + (num + 1).ToString();
            }
        }
    }
}

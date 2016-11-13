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
    /// <summary>
    /// 系统会员的操作结构类
    /// </summary>
    public class MemberInfoBLL
    {
        /// <summary>
        /// 会员登陆入口
        /// </summary>
        /// <param name="membercode"></param>
        /// <param name="loginpwd"></param>
        /// <param name="loginresult"></param>
        /// <returns></returns>
        public MemberInfoModel MemberLogin(string membercode, string loginpwd, out string loginresult)
        {
            MemberInfoModel member = MemberDAL.GetBriefSingleMemberModelForLogin(membercode);
            if (member == null)
            {
                loginresult = "无此会员";
                return null;//查无此会员
            }
            if (member.MemberStatus == 1)
            {
                loginresult = "该账户没有激活";
                return null;
            }
            if (member.MemberStatus == 3)
            {
                loginresult = "该账户已被冻结";
                return null;
            }
            if (member.MemberLogPwd != loginpwd)
            {
                loginresult = "登录密码不正确";
                return null;
            }
            loginresult = "1";
            return member;
        }
        /// <summary>
        /// 注册新会员
        /// </summary>
        /// <param name="model"></param>
        /// <param name="type">正常注册会员</param>
        /// <returns></returns>
        public string AddNewMemberInfo(MemberInfoModel model, int type)
        {
            string result = "1";
            int newmemberid = 0;
            #region 读取系统配置信息
            int maxsamemembercount = SystemConfigsBLL.GetConfigsValueByID(9).ParseToInt(10);//同一身份允许最多出现账户数
            string secrectstr = SystemConfigsBLL.GetConfigsValueByID(16);//网站加密字符串
            int reportnumbernum = SystemConfigsBLL.GetConfigsValueByID(8).ParseToInt(9);//成为报单中心所需直推人数
            int recommendProportion = SystemConfigsBLL.GetConfigsValueByID(2).ParseToInt(0);//推荐奖比例(%)
            int leaderProportion = SystemConfigsBLL.GetConfigsValueByID(3).ParseToInt(0);//领导奖比例(%)
            int reportProportion = SystemConfigsBLL.GetConfigsValueByID(4).ParseToInt(0);//报单奖比例(%)
            int baseProportion = SystemConfigsBLL.GetConfigsValueByID(17).ParseToInt(300);//一期网站计算基数
            string distributionList = SystemConfigsBLL.GetConfigsValueByID(7);//动态分配比例值(%)
            int isAutoActive = SystemConfigsBLL.GetConfigsValueByID(22).ParseToInt(1);//是否自动激活衍生账户
            #endregion
            #region 注册前验证
            //验证会员信息
            if (string.IsNullOrWhiteSpace(model.MemberIDNumber) || string.IsNullOrWhiteSpace(model.MemberName) || string.IsNullOrWhiteSpace(model.MemberBankCode) || string.IsNullOrWhiteSpace(model.MemberBankName))
            {
                return "会员信息不完整！";
            }
            MemberInfoModel recommendmember = null;
            if (!string.IsNullOrWhiteSpace(model.SourceMemberCode))
            {
                recommendmember = MemberDAL.GetBriefSingleMemberModel(model.SourceMemberCode);
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
                    if (recommendmember.IsFinalMember == 1)
                    {
                        return "推荐账户为终极账户，无法继续创建账户";
                    }
                }
            }
            else
            {
                int count = MemberDAL.GetMemberCount(0);
                if (count > 0)//若系统已经存在账户，则新注册的必须输入来源会员账户
                {
                    return "会员推荐人为空";
                }
            }
            #endregion
            #region 插入之前处理会员信息
            model.MemberLogPwd = DESEncrypt.Encrypt(model.MemberLogPwd, secrectstr);//加密网站登陆密码
            model.MemberSecondPwd = DESEncrypt.Encrypt(model.MemberSecondPwd, secrectstr);//加密网站登陆密码
            model.MemberStatus = 1;
            string code = "";
            if (type == 1)//标准会员
            {
                model.SourceMemberCode = "";
            }
            else if (type == 2)//衍生账户
            {
                model.IsDerivativeMember = 1;
            }
            else //终极账户
            {
                code = model.SourceMemberCode + "-1";
                model.IsFinalMember = 1;
                model.MemberCode = "JLZ" + code;
            }
            model.MemberType = type;
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
                newmemberid = memberid;//赋值新的会员ID
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
                int rowcount = 0;
                if (type == 1)
                {
                    if (recommendmember != null)
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
                        rowcount = ReMemberRelationDAL.AddNewReMemberRelation(recommendmodel);
                        if (rowcount < 1)
                        {
                            return "操作失败";
                        }
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
                if (recommendmember != null)
                {
                    int recommendnum = ReMemberRelationDAL.GetReMemberRelationCountByMemberid(recommendmember.ID);
                    if (recommendnum >= reportnumbernum)
                    {
                        int rows = MemberDAL.UpdateMemberIsReportMember(recommendmember.ID, 1);
                        if (rows < 1)
                        {
                            return "操作失败";
                        }
                    }
                }
                //计算动态的资金分配，记入DynamicReward表
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
                            dynamicmodel.MemberID = item.SuperiorMemberID;
                            dynamicmodel.MemberName = item.SuperiorMemberName;
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
                }
                //更改会员编号的状态为已使用
                rowcount = MemberCodeBLL.UpdateMemberCodeStatus(model.MemberCodeID);
                if (rowcount < 1)
                {
                    return "操作失败";
                }
                scope.Complete();
            }
            if (type == 2 && isAutoActive == 1)
            {
                string activeresult = ActiveMenber(newmemberid);
                if (activeresult != "1")
                {
                    return "会员注册成功，但激活失败，请手动激活";
                }
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
            string result = "1";
            #region 读取系统配置信息
            int pointProportion = SystemConfigsBLL.GetConfigsValueByID(18).ParseToInt(0);//静态分配积分占比(%)
            int gameProportion = SystemConfigsBLL.GetConfigsValueByID(19).ParseToInt(0);//静态分配游戏币占比(%)
            int baseProportion = SystemConfigsBLL.GetConfigsValueByID(17).ParseToInt(300);//一期网站计算基数
            decimal syscost = SystemConfigsBLL.GetConfigsValueByID(11).ParseToDecimal(0);//平台管理费用
            decimal maxcommery = SystemConfigsBLL.GetConfigsValueByID(5).ParseToDecimal(0);//平台管理费用
            string isautosoon = SystemConfigsBLL.GetConfigsValueByID(26);//是否立即返还会员的金钱
            #endregion
            #region 读取会员信息
            MemberInfoModel member = MemberDAL.GetBriefSingleMemberModel(memberid);//该会员信息
            ReMemberRelationModel relationmember = null;//推荐人信息
            MemberInfoModel SourceMemberInfo = null;//来源会员信息
            MemberInfoModel ChildMemberInfo = null;//子账户信息
            MemberExtendInfoModel memberextendinfo = null;//会员扩展信息
            MemberCapitalDetailModel memberCapital = null;//查询会员的资产信息
            if (member.MemberType == 1)
            {
                relationmember = ReMemberRelationDAL.GetReMemberRelationByBeRecommMemberID(memberid);//上级推荐人信息
                if (relationmember != null)
                {
                    memberextendinfo = MemberExtendInfoDAL.GetMemberExtendInfoByMemberID(relationmember.RecommendMemberID);
                    memberCapital = MemberCapitalDetailDAL.GetMemberCapitalDetailByMemberID(relationmember.RecommendMemberID);
                }
            }
            if (member.MemberType == 2)
            {
                SourceMemberInfo = MemberDAL.GetBriefSingleMemberModel(member.SourceMemberCode);//来源会员信息
                ChildMemberInfo = MemberDAL.GetBriefSingleMemberModelBySourceMemberCode(member.SourceMemberCode);
                if (SourceMemberInfo != null)
                {
                    memberextendinfo = MemberExtendInfoDAL.GetMemberExtendInfoByMemberID(SourceMemberInfo.ID);
                    memberCapital = MemberCapitalDetailDAL.GetMemberCapitalDetailByMemberID(SourceMemberInfo.ID);
                }
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
                    if (ChildMemberInfo != null)
                    {
                        //激活来源账户的附属账户
                        rowcount = MemberDAL.UpdateMemberStatus(ChildMemberInfo.ID, 2);
                        if (rowcount < 1)
                        {
                            return "操作失败";
                        }
                        if (isautosoon == "1")
                        {
                            //计算子账户的返还信息(游戏币和复利币)
                            decimal Cgamecurrey = baseProportion * pointProportion / 100;
                            decimal CcommingProportion = baseProportion * gameProportion / 100;
                            rowcount = MemberCapitalDetailDAL.UpdateCompoundCurrencyAndGameCurrency(Cgamecurrey, CcommingProportion, "注册返还静态奖励金额", ChildMemberInfo.ID);
                            if (rowcount < 1)
                            {
                                return "操作失败";
                            }
                        }
                    }
                }
                //扣除来源账户的游戏币和报单币(激活账户需要扣除200游戏币100报单币或者积分)
                #region 计算信息
                if (memberextendinfo != null)
                {
                    if (memberextendinfo.FormCurreyNum > 100 && memberCapital.GameCurrency > 200)//当会员的相关信息足够时
                    {
                        //扣减报单币
                        rowcount = MemberExtendInfoDAL.UpdateMemberFormCurrey(0 - 100, memberextendinfo.MemberID, "激活会员消耗100报单币");
                        if (rowcount < 1)
                        {
                            return "操作失败";
                        }
                        //扣减游戏币
                        rowcount = MemberCapitalDetailDAL.UpdateGameCurrency(0 - 200, "激活会员消耗", memberextendinfo.MemberID);
                        if (rowcount < 1)
                        {
                            return "操作失败";
                        }
                    }
                    else if (memberextendinfo.FormCurreyNum < 100 || memberCapital.GameCurrency < 200)
                    {
                        decimal Surplus = 0;
                        if (memberextendinfo.FormCurreyNum < 100)
                        {
                            Surplus += 100 - memberextendinfo.FormCurreyNum;
                        }
                        if (memberCapital.GameCurrency < 200)
                        {
                            Surplus += 200 - memberCapital.GameCurrency;
                        }
                        if (memberCapital.MemberPoints < Surplus)
                        {
                            return "操作失败";
                        }
                        if (memberextendinfo.FormCurreyNum > 0)
                        {
                            rowcount = MemberExtendInfoDAL.UpdateMemberFormCurrey(0 - 100, memberextendinfo.MemberID, "激活会员消耗100报单币");
                            if (rowcount < 1)
                            {
                                return "操作失败";
                            }
                        }
                        if (memberCapital.GameCurrency > 0)
                        {
                            rowcount = MemberCapitalDetailDAL.UpdateGameCurrency(0 - 200, "激活会员消耗", memberextendinfo.MemberID);
                            if (rowcount < 1)
                            {
                                return "操作失败";
                            }
                        }
                        rowcount = MemberCapitalDetailDAL.UpdateMemberPoints(0 - Surplus, "激活会员消耗", memberextendinfo.MemberID);
                        if (rowcount < 1)
                        {
                            return "操作失败";
                        }
                    }
                }
                #endregion
                //释放动态奖金
                rowcount = DynamicRewardDAL.ReleaseDynamicReward(memberid, "得到来自会员的注册激活动态奖励");
                //扣减平台管理费
                rowcount = MemberCapitalDetailDAL.UpdateMemberISDeSysCostFromDynamicReward(memberid, syscost);
                //if (rowcount < 1)
                //{
                //    return "操作失败";
                //}
                //更改释放状态
                rowcount = DynamicRewardDAL.UpdateDynamicRewardStatus(memberid);
                rowcount = MemberDAL.AddNewMemberInfoByDynamicReward(memberid, maxcommery);
                //if (rowcount < 1)
                //{
                //    return "操作失败";
                //}
                if (isautosoon == "1")
                {
                    //计算静态资金并返还
                    decimal gamecurrey = baseProportion * pointProportion / 100;
                    decimal commingProportion = baseProportion * gameProportion / 100;
                    rowcount = MemberCapitalDetailDAL.UpdateCompoundCurrencyAndGameCurrency(gamecurrey, commingProportion, "注册返还静态奖励金额", memberid);
                    if (rowcount < 1)
                    {
                        return "操作失败";
                    }
                }
                scope.Complete();
            }
            return result;
        }
        /// <summary>
        /// 分页查询会员列表
        /// </summary>
        /// <param name="searchmodel">查询条件</param>
        /// <param name="totalrowcount">总记录数</param>
        /// <returns></returns>
        public List<MemberInfoModel> GetMemberListForPage(MemberInfoModel searchmodel, out int totalrowcount)
        {
            return MemberDAL.GetMemberListForPage(searchmodel, out totalrowcount);
        }
        /// <summary>
        /// 更改会员的登陆密码
        /// </summary>
        /// <param name="memberid">会员ID</param>
        /// <param name="logpwd">登陆密码</param>
        /// <returns></returns>
        public int UpdateMemberLogpwd(int memberid, string logpwd)
        {
            string secrectstr = SystemConfigsBLL.GetConfigsValueByID(16);//网站加密字符串
            logpwd = DESEncrypt.Encrypt(logpwd, secrectstr);
            return MemberDAL.UpdateMemberLogpwd(memberid, logpwd);
        }
        /// <summary>
        /// 更改会员的二级密码
        /// </summary>
        /// <param name="memberid">会员ID</param>
        /// <param name="logpwd">登陆密码</param>
        /// <returns></returns>
        public int UpdateMemberMemberSecondPwd(int memberid, string secpwd)
        {
            string secrectstr = SystemConfigsBLL.GetConfigsValueByID(16);//网站加密字符串
            secpwd = DESEncrypt.Encrypt(secpwd, secrectstr);
            return MemberDAL.UpdateMemberMemberSecondPwd(memberid, secpwd);
        }
        /// <summary>
        /// 更改会员的状态信息
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateMemberStatus(int memberid, int status)
        {
            return MemberDAL.UpdateMemberStatus(memberid, status);
        }
        /// <summary>
        /// 得到会员的基本信息
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public MemberInfoModel GetSingleMemberModel(int memberid)
        {
            return MemberDAL.GetSingleMemberModel(memberid);
        }
        /// <summary>
        /// 更改会员的信息
        /// </summary>
        /// <param name="updatemodel"></param>
        /// <returns></returns>
        public int UpdateMemberInfoByMemberID(MemberInfoModel updatemodel)
        {
            return MemberDAL.UpdateMemberInfo(updatemodel);
        }
        /// <summary>
        /// 得到会员的报单币信息
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public int GetMemberFormCurreyNum(int memberid)
        {
            return MemberExtendInfoDAL.GetMemberExtendInfoByMemberID(memberid).FormCurreyNum;
        }
        /// <summary>
        /// 根据推荐人正向查找直推会员
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public List<MemberInfoModel> GetRecommendListByRecommendMemberId(int memberid)
        {
            return ReMemberRelationDAL.GetRecommendListByRecommendMemberId(memberid);
        }
        /// <summary>
        /// 根据推荐人正向查找直推会员
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public List<RecommendMap> GetRecommendListByRecommendMemberId2(int memberid)
        {
            return ReMemberRelationDAL.GetRecommendListByRecommendMemberId2(memberid);
        }
        /// <summary>
        /// 按照来源会员编号查询会员信息
        /// </summary>
        /// <param name="membercode"></param>
        /// <returns></returns>
        public List<MemberInfoModel> GetChildCountListBySourceMemberCode(string membercode)
        {
            return MemberDAL.GetChildCountListBySourceMemberCode(membercode);
        }
        /// <summary>
        /// 查询会员的直推人数
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public int recommendint(int memberid)
        {
            return ReMemberRelationDAL.GetReMemberRelationCountByMemberid(memberid);
        }
    }
}

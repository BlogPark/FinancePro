using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancePro.DALData;
using FinancePro.DataModels;

namespace FinancePro.BLLData
{
    public class WebHomeindexBLL
    {
        /// <summary>
        /// 读取首页所需要的数据集合
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public WebHomeDataModel GetHomeData(int memberid)
        {
            WebHomeDataModel model = new WebHomeDataModel();
            model.Memberinfo = MemberDAL.GetBriefSingleMemberModel(memberid);//会员信息
            model.MemberExtendInfo = MemberExtendInfoDAL.GetMemberExtendInfoByMemberID(memberid);//会员扩展信息
            model.MemberCapital = MemberCapitalDetailDAL.GetMemberCapitalDetailByMemberID(memberid);//会员资产信息
            model.AdminSiteNews = AdminSiteNewsDAL.GetModelListByUserID(memberid, 10);//网站公告
            model.ContactMessages = AdminSiteNewsDAL.GetContractMessage(memberid);//会员消息
            int capitalrowcount = 0;
            model.MemberCapitalLog = MemberCapitalLogDAL.GetMemberCapitalLogByMemberID(memberid,1,10,out capitalrowcount);//会员资金变动记录
            int formcurreyrowcount = 0;
            model.MemberFormCurreyLog = MemberFormCurreyLogDAL.GetMemberFormCurreyByMemberID(memberid,1,10,out formcurreyrowcount);//会员报单币操作记录
            int teamtotalcount=0;
            model.MemberRelation = ReMemberRelationDAL.GetReMemberRelationListByPositive(memberid, out teamtotalcount);//会员团队树信息
            model.TeamTotalCount = teamtotalcount;
            model.RecommendCount = ReMemberRelationDAL.GetReMemberRelationCountByMemberid(memberid);//会员直推人数;
            return model;
        }
    }
}

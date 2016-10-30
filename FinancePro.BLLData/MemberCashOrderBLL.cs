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
    /// 提现功能结构类
    /// </summary>
    public class MemberCashOrderBLL
    {
        /// <summary>
        /// 新增提现单据
        /// </summary>
        /// <param name="Ordermodel"></param>
        /// <returns></returns>
        public string AddNewMemberCashOrder(MemberCashOrderModel Ordermodel)
        {
            string result = "1";
            #region 读取相关配置项
            int mindiffday = SystemConfigsBLL.GetConfigsValueByID(12).ParseToInt(15);//提现最小间隔天数
            int maxCashProportion = SystemConfigsBLL.GetConfigsValueByID(13).ParseToInt(50);//提现最高比例
            int baseNum = SystemConfigsBLL.GetConfigsValueByID(20).ParseToInt(30);//提现的基数信息
            int commission = SystemConfigsBLL.GetConfigsValueByID(10).ParseToInt(20);//提现的手续费用
            int mincashmoney = SystemConfigsBLL.GetConfigsValueByID(21).ParseToInt(0);//限制最低提现额度
            #endregion
            #region 读取相关数据项
            MemberCashOrderModel LastCashorder = MemberCashOrderDAL.GetLastMemberCashOrderByMemberId(Ordermodel.MemberID);//上次提现信息
            MemberCapitalDetailModel capitalDetail = MemberCapitalDetailDAL.GetMemberCapitalDetailByMemberID(Ordermodel.MemberID);//会员的资产信息
            #endregion
            #region 验证信息
            if (LastCashorder.DiffDay < mindiffday)
            {
                return "提现过于频繁！";
            }
            if (Ordermodel.CashNum < mincashmoney)
            {
                return "提现小于平台最小提现值";
            }
            if (Ordermodel.CashNum > (capitalDetail.MemberPoints * maxCashProportion / 100))
            {
                return "提现超过了平台限制最大值";
            }
            if (Ordermodel.CashNum % baseNum > 0)
            {
                return "提现金额应为" + baseNum + "的整数倍";
            }
            #endregion
            #region 开始写入信息
            using (TransactionScope scope = new TransactionScope())
            {
                //扣减会员相应积分
                int rowcount = MemberCapitalDetailDAL.UpdateMemberPoints(0 - Ordermodel.CashNum, "申请提现" + Ordermodel.CashNum + "元", Ordermodel.MemberID);
                if (rowcount < 1)
                {
                    return "操作失败";
                }
                Ordermodel.CommissionNum = Ordermodel.CashNum * commission / 100;//提现手续费用
                Ordermodel.CashNum = Ordermodel.CashNum - (Ordermodel.CashNum * commission / 100);//修改提现金额
                //添加数据信息
                rowcount = MemberCashOrderDAL.AddNewMemberCashOrder(Ordermodel);
                if (rowcount < 1)
                {
                    return "操作失败";
                }
                scope.Complete();
            }
            #endregion
            return result;
        }
        /// <summary>
        /// 更改单据的状态
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public int UpdateMemberCashOrderStatus(int orderid, int statusnum)
        {
            return MemberCashOrderDAL.UpdateMemberCashOrderStatus(orderid, statusnum);
        }
        /// <summary>
        /// 批量修改提现单据状态
        /// </summary>
        /// <param name="orderids">单据ID列表</param>
        /// <param name="status">状态值</param>
        /// <returns></returns>
        public string UpdateBeachMemberCashOrderStatus(string orderids, int status)
        {
            if (string.IsNullOrWhiteSpace(orderids))
            {
                return "没有传入需要更改的单据";
            }
            string[] orderidlist = orderids.TrimEnd(',').Split(',');
            int rowcount = 0;
            string errorlist = "";
            foreach (string item in orderidlist)
            {
                rowcount = MemberCashOrderDAL.UpdateMemberCashOrderStatus(item.ParseToInt(0), status);
                if (rowcount < 1)
                {
                    errorlist += item + ",";
                }
            }
            if (errorlist == "")
            {
                return "1";
            }
            else
            {
                return errorlist;
            }
        }
        /// <summary>
        /// 分页查询会员提现记录
        /// </summary>
        /// <param name="model"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public List<MemberCashOrderModel> GetAllMemberCashByPage(MemberCashOrderModel model, out int totalrowcount)
        {
            return MemberCashOrderDAL.GetAllMemberCashByPage(model, out totalrowcount);
        }
        /// <summary>
        /// 会员Id查询提现信息
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public List<MemberCashOrderModel> GetMemberCashByMemberId(int memberid, int pageindex, int pagesize, out int totalrowcount)
        {
            return MemberCashOrderDAL.GetMemberCashByMemberId(memberid, pageindex, pagesize, out totalrowcount);
        }
    }
}

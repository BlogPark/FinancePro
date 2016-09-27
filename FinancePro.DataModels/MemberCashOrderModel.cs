using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FinancePro.DataModels
{
    [Serializable]
    [DataContract]
    public class MemberCashOrderModel
    {

        #region 原始字段
        private int _id;
        /// <summary>
        /// ID
        /// </summary>		
        [DataMember]
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _cashordercode;
        /// <summary>
        /// 提现单据编号
        /// </summary>		
        [DataMember]
        public string CashOrderCode
        {
            get { return _cashordercode; }
            set { _cashordercode = value; }
        }
        private int _memberid;
        /// <summary>
        /// 会员ID
        /// </summary>		
        [DataMember]
        public int MemberID
        {
            get { return _memberid; }
            set { _memberid = value; }
        }
        private string _membername;
        /// <summary>
        /// 会员名字
        /// </summary>		
        [DataMember]
        public string MemberName
        {
            get { return _membername; }
            set { _membername = value; }
        }
        private string _membercode;
        /// <summary>
        /// 会员编号
        /// </summary>		
        [DataMember]
        public string MemberCode
        {
            get { return _membercode; }
            set { _membercode = value; }
        }
        private decimal _cashnum;
        /// <summary>
        /// 申请金额
        /// </summary>		
        [DataMember]
        public decimal CashNum
        {
            get { return _cashnum; }
            set { _cashnum = value; }
        }
        private decimal _commissionnum;
        /// <summary>
        /// 手续费用
        /// </summary>		
        [DataMember]
        public decimal CommissionNum
        {
            get { return _commissionnum; }
            set { _commissionnum = value; }
        }     
        private decimal _finishcashnum;
        /// <summary>
        /// 完成金额
        /// </summary>		
        [DataMember]
        public decimal FinishCashNum
        {
            get { return _finishcashnum; }
            set { _finishcashnum = value; }
        }
        private int _cstatus = 0;
        /// <summary>
        /// 申请状态(1 新申请 2 已打款 3 已驳回)
        /// </summary>		
        [DataMember]
        public int CStatus
        {
            get { return _cstatus; }
            set { _cstatus = value; }
        }
        private DateTime _addtime;
        /// <summary>
        /// 申请时间
        /// </summary>		
        [DataMember]
        public DateTime AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        private string _cashbankname;
        /// <summary>
        /// 提现银行名称
        /// </summary>		
        [DataMember]
        public string CashBankName
        {
            get { return _cashbankname; }
            set { _cashbankname = value; }
        }
        private string _cashbankcode;
        /// <summary>
        /// 提现银行卡号
        /// </summary>		
        [DataMember]
        public string CashBankCode
        {
            get { return _cashbankcode; }
            set { _cashbankcode = value; }
        }
        #endregion

        #region 扩展字段
        /// <summary>
        /// 状态名称
        /// </summary>
        public string CStatusName { get; set; }
        /// <summary>
        /// 页容量
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }
        /// <summary>
        /// 页索引
        /// </summary>
        [DataMember]
        public int PageIndex { get; set; }
        /// <summary>
        /// 相差天数
        /// </summary>
        [DataMember]
        public int DiffDay { get; set; }
        #endregion
    }
}

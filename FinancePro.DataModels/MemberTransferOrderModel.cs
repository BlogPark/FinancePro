using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FinancePro.DataModels
{
    /// <summary>
    /// 会员转账列表
    /// </summary>
    [Serializable]
    [DataContract]
    public class MemberTransferOrderModel
    {

        #region 原始字段
        private int _id;
        /// <summary>
        /// 自增ID
        /// </summary>		
        [DataMember]
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        private int _launchmemberid;
        /// <summary>
        /// 发起会员ID
        /// </summary>		
        [DataMember]
        public int LaunchMemberID
        {
            get { return _launchmemberid; }
            set { _launchmemberid = value; }
        }
        private string _launchmembercode;
        /// <summary>
        /// 发起会员编号
        /// </summary>		
        [DataMember]
        public string LaunchMemberCode
        {
            get { return _launchmembercode; }
            set { _launchmembercode = value; }
        }
        private int _receivememberid;
        /// <summary>
        /// 接受会员ID
        /// </summary>		
        [DataMember]
        public int ReceiveMemberID
        {
            get { return _receivememberid; }
            set { _receivememberid = value; }
        }
        private string _receivemembercode;
        /// <summary>
        /// 接受会员编号
        /// </summary>		
        [DataMember]
        public string ReceiveMemberCode
        {
            get { return _receivemembercode; }
            set { _receivemembercode = value; }
        }
        private int _transfertype;
        /// <summary>
        /// 转账类型(1 报单币 2 积分 3 购物币 4 股权币 5 复利币 6 游戏币)
        /// </summary>		
        [DataMember]
        public int TransferType
        {
            get { return _transfertype; }
            set { _transfertype = value; }
        }
        private decimal _transfernumber;
        /// <summary>
        /// 转账金额
        /// </summary>		
        [DataMember]
        public decimal TransferNumber
        {
            get { return _transfernumber; }
            set { _transfernumber = value; }
        }
        private string _transferremark;
        /// <summary>
        /// 转账备注
        /// </summary>		
        [DataMember]
        public string TransferRemark
        {
            get { return _transferremark; }
            set { _transferremark = value; }
        }
        private decimal _counterfee;
        /// <summary>
        /// 手续费(暂无用)
        /// </summary>		
        [DataMember]
        public decimal CounterFee
        {
            get { return _counterfee; }
            set { _counterfee = value; }
        }
        private DateTime _addtime;
        /// <summary>
        /// 转账时间
        /// </summary>		
        [DataMember]
        public DateTime AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        private int _transferresult;
        /// <summary>
        /// 转账结果
        /// </summary>		
        [DataMember]
        public int TransferResult
        {
            get { return _transferresult; }
            set { _transferresult = value; }
        } 
        #endregion

        #region 扩展字段
        [DataMember]
        public string TransferTypeName { get; set; }
        #endregion
    }
}

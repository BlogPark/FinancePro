using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FinancePro.DataModels
{
    /// <summary>
    /// 会员资产明细
    /// </summary>
    [Serializable]
    [DataContract]
    public class MemberCapitalDetailModel
    {
        #region 原始字段
        private int _id;
        /// <summary>
        /// 自增标识
        /// </summary>		
        [DataMember]
        public int ID
        {
            get { return _id; }
            set { _id = value; }
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
        private decimal _gamecurrency;
        /// <summary>
        /// 游戏币
        /// </summary>		
        [DataMember]
        public decimal GameCurrency
        {
            get { return _gamecurrency; }
            set { _gamecurrency = value; }
        }
        private decimal _sharescurrency;
        /// <summary>
        /// 股权币
        /// </summary>		
        [DataMember]
        public decimal SharesCurrency
        {
            get { return _sharescurrency; }
            set { _sharescurrency = value; }
        }
        private decimal _shoppingcurrency;
        /// <summary>
        /// 购物币
        /// </summary>		
        [DataMember]
        public decimal ShoppingCurrency
        {
            get { return _shoppingcurrency; }
            set { _shoppingcurrency = value; }
        }
        private decimal _memberpoints;
        /// <summary>
        /// 会员积分
        /// </summary>		
        [DataMember]
        public decimal MemberPoints
        {
            get { return _memberpoints; }
            set { _memberpoints = value; }
        }
        private decimal _compoundcurrency;
        /// <summary>
        /// 复利币(报单币)
        /// </summary>		
        [DataMember]
        public decimal CompoundCurrency
        {
            get { return _compoundcurrency; }
            set { _compoundcurrency = value; }
        }
        private int _isdesyscost;
        /// <summary>
        /// 是否已扣减平台管理费
        /// </summary>		
        [DataMember]
        public int ISDeSysCost
        {
            get { return _isdesyscost; }
            set { _isdesyscost = value; }
        }
        private decimal _totalassignedpoints;
        /// <summary>
        /// 累计分配积分
        /// </summary>		
        [DataMember]
        public decimal TotalAssignedPoints
        {
            get { return _totalassignedpoints; }
            set { _totalassignedpoints = value; }
        }
        private int _iscreadchild;
        /// <summary>
        /// 是否已创建子账户
        /// </summary>		
        [DataMember]
        public int ISCreadChild
        {
            get { return _iscreadchild; }
            set { _iscreadchild = value; }
        }
        private decimal _desyscost;
        /// <summary>
        /// 平台管理费
        /// </summary>		
        [DataMember]
        public decimal DeSysCost
        {
            get { return _desyscost; }
            set { _desyscost = value; }
        }        
        #endregion

        #region 扩展字段
        /// <summary>
        /// 页索引
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页容量
        /// </summary>
        public int PageSize { get; set; }
        #endregion
    }
}

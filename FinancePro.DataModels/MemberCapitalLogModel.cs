using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FinancePro.DataModels
{
    /// <summary>
    /// 资产变动记录
    /// </summary>
    [Serializable]
    [DataContract]
    public class MemberCapitalLogModel
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
        private decimal _bmemberpoints;
        /// <summary>
        /// 变动前积分
        /// </summary>		
        [DataMember]
        public decimal BMemberPoints
        {
            get { return _bmemberpoints; }
            set { _bmemberpoints = value; }
        }
        private decimal _nmemberpoints;
        /// <summary>
        /// 最新积分
        /// </summary>		
        [DataMember]
        public decimal NMemberPoints
        {
            get { return _nmemberpoints; }
            set { _nmemberpoints = value; }
        }
        private decimal _bgamecurrency;
        /// <summary>
        /// 变动前游戏币
        /// </summary>		
        [DataMember]
        public decimal BGameCurrency
        {
            get { return _bgamecurrency; }
            set { _bgamecurrency = value; }
        }
        private decimal _ngamecurrency;
        /// <summary>
        /// 最新游戏币
        /// </summary>		
        [DataMember]
        public decimal NGameCurrency
        {
            get { return _ngamecurrency; }
            set { _ngamecurrency = value; }
        }
        private decimal _bsharescurrency;
        /// <summary>
        /// 变动前股权币
        /// </summary>		
        [DataMember]
        public decimal BSharesCurrency
        {
            get { return _bsharescurrency; }
            set { _bsharescurrency = value; }
        }
        private decimal _nsharescurrency;
        /// <summary>
        /// 最新股权币
        /// </summary>		
        [DataMember]
        public decimal NSharesCurrency
        {
            get { return _nsharescurrency; }
            set { _nsharescurrency = value; }
        }
        private decimal _bshoppingcurrency;
        /// <summary>
        /// 变动前购物币
        /// </summary>		
        [DataMember]
        public decimal BShoppingCurrency
        {
            get { return _bshoppingcurrency; }
            set { _bshoppingcurrency = value; }
        }
        private decimal _nshoppingcurrency;
        /// <summary>
        /// 最新购物币
        /// </summary>		
        [DataMember]
        public decimal NShoppingCurrency
        {
            get { return _nshoppingcurrency; }
            set { _nshoppingcurrency = value; }
        }
        private decimal _bcompoundcurrency;
        /// <summary>
        /// 变动前复利币(报单币)
        /// </summary>		
        [DataMember]
        public decimal BCompoundCurrency
        {
            get { return _bcompoundcurrency; }
            set { _bcompoundcurrency = value; }
        }
        private decimal _ncompoundcurrency;
        /// <summary>
        /// 最新复利币(报单币)
        /// </summary>		
        [DataMember]
        public decimal NCompoundCurrency
        {
            get { return _ncompoundcurrency; }
            set { _ncompoundcurrency = value; }
        }
        private string _logremark;
        /// <summary>
        /// 备注信息
        /// </summary>		
        [DataMember]
        public string LogRemark
        {
            get { return _logremark; }
            set { _logremark = value; }
        }
        private DateTime _addtime;
        /// <summary>
        /// 添加时间
        /// </summary>		
        [DataMember]
        public DateTime AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        } 
        #endregion
        #region 扩展字段

        #endregion

    }
}

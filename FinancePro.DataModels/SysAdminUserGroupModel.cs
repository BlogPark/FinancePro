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
    public class SysAdminUserGroupModel
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
        private string _groupname;
        /// <summary>
        /// GroupName
        /// </summary>		
        [DataMember]
        public string GroupName
        {
            get { return _groupname; }
            set { _groupname = value; }
        }
        private string _groupalt;
        /// <summary>
        /// GroupAlt
        /// </summary>		
        [DataMember]
        public string GroupAlt
        {
            get { return _groupalt; }
            set { _groupalt = value; }
        }
        private int _groupstatus;
        /// <summary>
        /// GroupStatus
        /// </summary>		
        [DataMember]
        public int GroupStatus
        {
            get { return _groupstatus; }
            set { _groupstatus = value; }
        }
        private DateTime _addtime;
        /// <summary>
        /// Addtime
        /// </summary>		
        [DataMember]
        public DateTime Addtime
        {
            get { return _addtime; }
            set { _addtime = value; }
        } 
        #endregion
        #region 扩展字段
        /// <summary>
        /// 操作类型
        /// </summary>
        [DataMember]
        public int Type { get; set; }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FinancePro.DataModels
{
    /// <summary>
    /// 系统配置
    /// </summary>
    [Serializable]
    [DataContract]
    public class SystemConfigsModel
    {
        #region 原始字段
        private int _id;
        /// <summary>
        /// 标识符
        /// </summary>		
        [DataMember]
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _configname;
        /// <summary>
        /// 配置项名称
        /// </summary>		
        [DataMember]
        public string ConfigName
        {
            get { return _configname; }
            set { _configname = value; }
        }
        private string _configvalue;
        /// <summary>
        /// 配置项值
        /// </summary>		
        [DataMember]
        public string ConfigValue
        {
            get { return _configvalue; }
            set { _configvalue = value; }
        }
        private string _configremark;
        /// <summary>
        /// 配置项备注
        /// </summary>		
        [DataMember]
        public string ConfigRemark
        {
            get { return _configremark; }
            set { _configremark = value; }
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
        private int _configstatus;
        /// <summary>
        /// 配置项状态(1 使用 2 禁用)
        /// </summary>		
        [DataMember]
        public int ConfigStatus
        {
            get { return _configstatus; }
            set { _configstatus = value; }
        }
        private int _isadmin;
        /// <summary>
        /// 是否管理员显示
        /// </summary>		
        [DataMember]
        public int IsAdmin
        {
            get { return _isadmin; }
            set { _isadmin = value; }
        } 
        #endregion
        #region 扩展字段
        /// <summary>
        /// 状态名称
        /// </summary>
        public string ConfigStatusName { get; set; }
        #endregion
    }
}

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
    public class SysAdminGrouprMenuModel
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
        private int _gid;
        /// <summary>
        /// GID
        /// </summary>		
        [DataMember]
        public int GID
        {
            get { return _gid; }
            set { _gid = value; }
        }
        private string _gname;
        /// <summary>
        /// GName
        /// </summary>		
        [DataMember]
        public string GName
        {
            get { return _gname; }
            set { _gname = value; }
        }
        private int _mid;
        /// <summary>
        /// MID
        /// </summary>		
        [DataMember]
        public int MID
        {
            get { return _mid; }
            set { _mid = value; }
        }
        private string _mname;
        /// <summary>
        /// MName
        /// </summary>		
        [DataMember]
        public string MName
        {
            get { return _mname; }
            set { _mname = value; }
        }
        private int _mtype;
        /// <summary>
        /// MType
        /// </summary>		
        [DataMember]
        public int MType
        {
            get { return _mtype; }
            set { _mtype = value; }
        }
        private int _permissiontype;
        /// <summary>
        /// PermissionType
        /// </summary>		
        [DataMember]
        public int PermissionType
        {
            get { return _permissiontype; }
            set { _permissiontype = value; }
        }
        private DateTime _addtime;
        /// <summary>
        /// AddTime
        /// </summary>		
        [DataMember]
        public DateTime AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        private int _isedit;
        /// <summary>
        /// IsEdit
        /// </summary>		
        [DataMember]
        public int IsEdit
        {
            get { return _isedit; }
            set { _isedit = value; }
        } 
        #endregion
        #region 扩展字段
        /// <summary>
        /// 上级ID
        /// </summary>
        [DataMember]
        public int FatherID { get; set; }
        /// <summary>
        /// 菜单类型名称
        /// </summary>
        [DataMember]
        public string MenuTypeName { get; set; }
        /// <summary>
        /// 菜单权限名称
        /// </summary>
        [DataMember]
        public string PermissionTypeName { get; set; }
        /// <summary>
        /// 允许修改编辑
        /// </summary>
        [DataMember]
        public string IsEditName { get; set; }
        #endregion
    }
}

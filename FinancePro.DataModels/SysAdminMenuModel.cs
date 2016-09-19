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
    public class SysAdminMenuModel
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
        private string _menuname;
        /// <summary>
        /// MenuName
        /// </summary>		
        [DataMember]
        public string MenuName
        {
            get { return _menuname; }
            set { _menuname = value; }
        }
        private int _fatherid;
        /// <summary>
        /// FatherID
        /// </summary>		
        [DataMember]
        public int FatherID
        {
            get { return _fatherid; }
            set { _fatherid = value; }
        }
        private string _menualt;
        /// <summary>
        /// MenuAlt
        /// </summary>		
        [DataMember]
        public string MenuAlt
        {
            get { return _menualt; }
            set { _menualt = value; }
        }
        private string _fathername;
        /// <summary>
        /// FatherName
        /// </summary>		
        [DataMember]
        public string FatherName
        {
            get { return _fathername; }
            set { _fathername = value; }
        }
        private string _linkurl;
        /// <summary>
        /// LinkUrl
        /// </summary>		
        [DataMember]
        public string LinkUrl
        {
            get { return _linkurl; }
            set { _linkurl = value; }
        }
        private int _menustatus;
        /// <summary>
        /// MenuStatus
        /// </summary>		
        [DataMember]
        public int MenuStatus
        {
            get { return _menustatus; }
            set { _menustatus = value; }
        }
        private int _sortindex;
        /// <summary>
        /// SortIndex
        /// </summary>		
        [DataMember]
        public int SortIndex
        {
            get { return _sortindex; }
            set { _sortindex = value; }
        }
        private int _menutype;
        /// <summary>
        /// MenuType
        /// </summary>		
        [DataMember]
        public int MenuType
        {
            get { return _menutype; }
            set { _menutype = value; }
        }
        private string _controllername;
        /// <summary>
        /// ControllerName
        /// </summary>		
        [DataMember]
        public string ControllerName
        {
            get { return _controllername; }
            set { _controllername = value; }
        }
        private string _actionname;
        /// <summary>
        /// ActionName
        /// </summary>		
        [DataMember]
        public string ActionName
        {
            get { return _actionname; }
            set { _actionname = value; }
        }
        private string _areaname;
        /// <summary>
        /// AreaName
        /// </summary>		
        [DataMember]
        public string AreaName
        {
            get { return _areaname; }
            set { _areaname = value; }
        }
        private string _menuicon;
        /// <summary>
        /// MenuIcon
        /// </summary>		
        [DataMember]
        public string MenuIcon
        {
            get { return _menuicon; }
            set { _menuicon = value; }
        } 
        #endregion
        #region 扩展字段
        /// <summary>
        /// 权限类型(1 查看 2 编辑  3修改 4 删除)
        /// </summary>
        [DataMember]
        public int PermissionType { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        [DataMember]
        public int Type { get; set; }
        /// <summary>
        /// 是否具有权限
        /// </summary>
        [DataMember]
        public string IsHave { get; set; }
        #endregion
    }
}

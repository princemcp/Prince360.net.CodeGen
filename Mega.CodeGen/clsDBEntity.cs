using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mega.CodeGen
{
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "Mega.CodeGen")]
    public class clsColumnEntity : clsBaseColumnEntity
    {
        [XmlAttribute(AttributeName = "TableName")]
        public string TableName { get; set; }

        [XmlAttribute(AttributeName = "TableAliasName")]
        public string TableAliasName { get; set; }

        [XmlAttribute(AttributeName = "IsMasterTable")]
        public bool IsMasterTable { get; set; }

        [XmlAttribute(AttributeName = "ColumnName")]
        public string ColumnName { get; set; }

        [XmlAttribute(AttributeName = "RefColumnName")]
        public string RefColumnName { get; set; }

        [XmlAttribute(AttributeName = "ColumnAliasName")]
        public string ColumnAliasName { get; set; }

        [XmlAttribute(AttributeName = "Precsion")]
        public string Precsion { get; set; }

        [XmlAttribute(AttributeName = "Scale")]
        public string Scale { get; set; }

        [XmlAttribute(AttributeName = "Lenght")]
        public string Lenght { get; set; }

        [XmlAttribute(AttributeName = "ColumnDBType")]
        public string ColumnDBType { get; set; }

        [XmlAttribute(AttributeName = "ColumnDotNetType")]
        public string ColumnDotNetType { get; set; }

        [XmlAttribute(AttributeName = "ColumnIsNull")]
        public bool ColumnIsNull { get; set; }

        [XmlAttribute(AttributeName = "ReferenceTableName")]
        public string ReferenceTableName { get; set; }



        [XmlAttribute(AttributeName = "SelectForGetSP")]
        public bool SelectForGetSP { get; set; }

        [XmlAttribute(AttributeName = "RefarenceToTable")]
        public string[] RefarenceToTable { get; set; }
    }


    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "Mega.CodeGen")]
    public class clsTableRefEntity
    {
        [XmlAttribute(AttributeName = "TableName")]
        public string TableName { get; set; }

        [XmlAttribute(AttributeName = "TableAliasName")]
        public string TableAliasName { get; set; }

        [XmlAttribute(AttributeName = "RefColumnName")]
        public string RefColumnName { get; set; }

        [XmlAttribute(AttributeName = "ThisColumnName")]
        public string ThisColumnName { get; set; }

        [XmlAttribute(AttributeName = "RefIsNull")]
        public bool RefIsNull { get; set; }

    }


    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "Mega.CodeGen")]
    public class clsBaseColumnEntity : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        [XmlAttribute(AttributeName = "GetSPParam")]
        public bool GetSPParam { get; set; }

        [XmlAttribute(AttributeName = "SearchSPParam")]
        public bool SearchSPParam { get; set; }

        [XmlAttribute(AttributeName = "SPOrderBy")]
        public bool SPOrderBy { get; set; }

        [XmlAttribute(AttributeName = "HasReference")]
        public bool HasReference { get; set; }

        [XmlAttribute(AttributeName = "IsHidden")]
        public bool IsHidden { get; set; }

        [XmlAttribute(AttributeName = "Lable")]
        public string Lable { get; set; }
    }

    [Serializable]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "Mega.CodeGen")]
    public class clsAllTableListEntity
    {
        private string _tableName;

        [XmlAttribute(AttributeName = "TableName")]
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        private List<clsTableRefEntity> _tableRefEntity = new List<clsTableRefEntity>();

        [XmlElement("TableRefEntity")]
        public List<clsTableRefEntity> TableRefEntity
        {
            get { return _tableRefEntity; }
            set { _tableRefEntity = value; }
        }

        private List<clsColumnEntity> _columnEntity = new List<clsColumnEntity>();

        [XmlElement("ColumnEntity")]
        public List<clsColumnEntity> ColumnEntity
        {
            get { return _columnEntity; }
            set { _columnEntity = value; }
        }

        private string _objTreeNode;

        [XmlAttribute(AttributeName = "ObjTreeNode")]
        public string ObjTreeNode
        {
            get { return _objTreeNode; }
            set { _objTreeNode = value; }
        }
    }


    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "Mega.CodeGen")]
    public class SystemConfigurationEntity
    {
        [XmlAttribute(AttributeName = "SQLServerName")]
        public string SQLServerName { get; set; }

        [XmlAttribute(AttributeName = "SQLServerUserName")]
        public string SQLServerUserName { get; set; }

        [XmlAttribute(AttributeName = "SQLServerPassword")]
        public string SQLServerPassword { get; set; }

    }
}
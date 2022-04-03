using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mega.CodeGen.WebAPI.netCore
{
    public class Entity
    {
        public bool EntityEngineMethod(IList<clsTableRefEntity> objclsTableRefEntityList, IList<clsColumnEntity> objclsColumnEntityList, string nameSpace, string tableName, string path, string DataBaseName)
        {
            path = path + "\\Entities";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\" + tableName + "Entity.cs";

            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);

            StreamWriter w = new StreamWriter(fs);
            try
            {
                w.WriteLine(GenerateString(objclsTableRefEntityList, objclsColumnEntityList, nameSpace, tableName, DataBaseName));
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                w.Close();
                fs.Close();
            }
            return true;
        }

        private string GenerateString(IList<clsTableRefEntity> objclsTableRefEntityList, IList<clsColumnEntity> objclsColumnEntityList, string nameSpace, string tableName, string DataBaseName)
        {
            string strQry = "";
            for (int i = objclsColumnEntityList.Count - 1; i >= 0; i--)
            {
                if (objclsColumnEntityList[i].IsMasterTable == false)
                {
                    objclsColumnEntityList.RemoveAt(i);
                }
            }

            strQry = "///////////////////////////////////////////////////////////////////////////////\n";
            strQry += "//      Author      : SM Habib Ullah -- Prince\n";
            strQry += "//      Web         : https://www.Prince360.net\n";
            strQry += "//      Date        : " + DateTime.Now.ToString("dd-MM-yyyy") + "     \n";
            strQry += "//      File name   : " + tableName + "Entity.cs      \n";
            strQry += "///////////////////////////////////////////////////////////////////////////////\n\n";

            strQry += "using System;\n";
            strQry += "using System.Collections.Generic;\n";
            strQry += "using System.Data;\n";
            strQry += "using System.Runtime.Serialization;\n";
            strQry += "using "+ nameSpace.Split('.')[0] + ".Entities;\n";
            
            
            strQry += "\n\nnamespace " + nameSpace + "\n";
            strQry += "{\n";
            strQry += "\t[Serializable]\n";
            strQry += "\t[DataContract(Name = \"" + tableName + "Entity\", Namespace = \"http://www.meganetict.com/types\")]\n";
            strQry += "\tpublic partial class " + tableName + "Entity : BaseEntity\n";
            strQry += "\t{\n";
            strQry += "\n";
            // strQry += "\n\n\n\n\t\t#region Base Table \n\n";
            strQry += "\t\t#region public properties\n\n";

            for (int i = 0; i < objclsColumnEntityList.Count; i++)
            {
                string nullable = "";
                //if (objclsColumnEntityList[i].ColumnIsNull == false)
                //{
                //    nullable = "";
                //}
                //else 
                
                if (objclsColumnEntityList[i].ColumnDotNetType != "string" )
                {
                    nullable = "?";
                }
                if (objclsColumnEntityList[i].ColumnName == "TS"
                   || objclsColumnEntityList[i].ColumnName == "CreatedDate"
                   || objclsColumnEntityList[i].ColumnName == "CreatedBy"
                   || objclsColumnEntityList[i].ColumnName == "UpdatedDate"
                   || objclsColumnEntityList[i].ColumnName == "UpdatedBy"
                   || objclsColumnEntityList[i].ColumnName == "TagID"
                    )
                {
                    continue;
                }
                strQry += "\t\t[DataMember]\t\t\n";
                strQry += "\t\tpublic " + objclsColumnEntityList[i].ColumnDotNetType + " " + nullable + " " + objclsColumnEntityList[i].ColumnName.Substring(0, 1).ToLower() + objclsColumnEntityList[i].ColumnName.Substring(1, objclsColumnEntityList[i].ColumnName.Length - 1) + " { get; set;}\t\t\n";
            }

            strQry += "\n\t\t#endregion\n\n";

            strQry += "\t\t#region Master Details Lists\n\n";
            if (objclsColumnEntityList[0].RefarenceToTable != null)
            {
                strQry += "\t\t" + GetMasterDetailsEntity(objclsColumnEntityList, tableName, "\n\n\t\t");
                strQry = strQry.Substring(0, strQry.Length - 3);
            }
            strQry += "\n\t\t#endregion\n";

            strQry += "\n\t\t#region Constructor\n";

            strQry += "\n\t\tpublic "+ tableName + "Entity()\n";
            strQry += "\t\t\t: base()\n";
            strQry += "\t\t{\n";
            strQry += "\t\t}\n";

            strQry += "\n\t\tpublic " + tableName + "Entity(IDataReader reader)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\tthis.LoadFromReader(reader);\n";
            strQry += "\t\t}\n";


            strQry += "\n\t\tprotected void LoadFromReader(IDataReader reader)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\tif (reader != null && !reader.IsClosed)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tthis.BaseSecurityParam = new SecurityCapsule();\n";
            strQry += "" + GetReaderList(objclsColumnEntityList, tableName, "\t\t\t\t", false);
            strQry += "\t\t\t}\n";
            strQry += "\t\t}\n";


            strQry += "\n\t\t#endregion\n";
            strQry += "\n";
           
            
           
            strQry += "\t}\n}";

            return strQry;
        }

        public string GetMasterDetailsEntity(IList<clsColumnEntity> objclsColumnEntityList, string tableName, string tab)
        {
            string strQry = "";
            if (objclsColumnEntityList[0].RefarenceToTable != null)
            {
                for (int i = 0; i <= objclsColumnEntityList[0].RefarenceToTable.Length - 1; i++)
                {
                    string refToTableName = objclsColumnEntityList[0].RefarenceToTable[i].Split(':')[0].Split('.')[2].ToString();
                    if (refToTableName.Trim() == tableName.Trim())
                    {
                        continue;
                    }
                    if (objclsColumnEntityList[i].ColumnDBType.Contains("identity"))
                    {

                    }

                    strQry += "[DataMember]\n";
                    strQry += "\t\tpublic List<" + refToTableName + "Entity> " + refToTableName + "List { get; set; }" + tab;
                }
            }
            return strQry;
        }
        private string GetReaderList(IList<clsColumnEntity> objclsColumnEntityList, string tableName, string tab, bool collection)
        {
            string strQry = "";
            if (collection == false)
            {
                for (int i = 0; i < objclsColumnEntityList.Count; i++)
                {

                    if (objclsColumnEntityList[i].ColumnDBType != "image")
                    {
                        if (objclsColumnEntityList[i].IsMasterTable == true)
                        {
                            if (objclsColumnEntityList[i].ColumnName == "TS"
                               || objclsColumnEntityList[i].ColumnName == "CreatedDate"
                               || objclsColumnEntityList[i].ColumnName == "CreatedBy"
                               || objclsColumnEntityList[i].ColumnName == "UpdatedDate"
                               || objclsColumnEntityList[i].ColumnName == "UpdatedBy"
                               || objclsColumnEntityList[i].ColumnName == "TagID"
                               )
                            {
                                strQry += tab + "if (!reader.IsDBNull(reader.GetOrdinal(\"" + objclsColumnEntityList[i].ColumnAliasName.Trim() + "\"))) this.BaseSecurityParam." + objclsColumnEntityList[i].ColumnName.Substring(0, 1).ToLower() + objclsColumnEntityList[i].ColumnName.Substring(1, objclsColumnEntityList[i].ColumnName.Length - 1) + " = reader." + GetSqlDataType(objclsColumnEntityList[i].ColumnDBType) + "(reader.GetOrdinal(\"" + objclsColumnEntityList[i].ColumnAliasName.Trim() + "\"));\n";
                                continue;
                            }
                            strQry += tab + "if (!reader.IsDBNull(reader.GetOrdinal(\"" + objclsColumnEntityList[i].ColumnAliasName.Trim() + "\"))) " + objclsColumnEntityList[i].ColumnName.Substring(0, 1).ToLower() + objclsColumnEntityList[i].ColumnName.Substring(1, objclsColumnEntityList[i].ColumnName.Length - 1) + " = reader." + GetSqlDataType(objclsColumnEntityList[i].ColumnDBType) + "(reader.GetOrdinal(\"" + objclsColumnEntityList[i].ColumnAliasName.Trim() + "\"));";
                        }
                        else
                        {
                            //strQry += tab + objclsColumnEntityList[i].ColumnAliasName.Trim() + tab + tab + tab + objclsColumnEntityList[i].ColumnDBType.Replace("identity", "") + " ";
                        }



                        strQry += "\n";
                    }

                }
                strQry += tab + "CurrentState = EntityState.Unchanged;\n";
            }
            else
            {

            }
            return strQry;
        }

        private string GetSqlDataType(string datatype)
        {
            datatype = datatype.Replace("identity", "").Trim();
            if (datatype == "bigint") return "GetInt64";
            if (datatype == "uniqueidentifier") return "GetGuid";
            if (datatype == "varchar") return "GetString";
            if (datatype == "nvarchar") return "GetString";
            if (datatype == "int") return "GetInt32";
            if (datatype == "bit") return "GetBoolean";
            if (datatype == "datetime") return "GetDateTime";
            if (datatype == "timestamp") return "GetInt64";
            if (datatype == "varbinary") return "ParseStrictByteArray";
            if (datatype == "decimal") return "GetDecimal";
            if (datatype == "numeric") return "GetDecimal";
            return "Unknown";
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Mega.CodeGen.MVC2Engine
{
    public class EntityRefEngine
    {
        public bool EntityRefEngineMethod(IList<clsTableRefEntity> objclsTableRefEntityList, IList<clsColumnEntity> objclsColumnEntityList, string nameSpace, string tableName, string path, string DataBaseName)
        {
            path = path + "\\BO.ReferenceEntity";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\" + tableName + "EntityRef.cs";

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
                if (objclsColumnEntityList[i].IsMasterTable == true)
                {
                    objclsColumnEntityList.RemoveAt(i);
                }
            }

            strQry = "///////////////////////////////////////////////////////////////////////////////\n";
            strQry += "//      Author      : Maxima Prince              \n";
            strQry += "//      Date        : " + DateTime.Now + "     \n";
            strQry += "//      File name   : " + tableName + "Entity.cs     \n";
            strQry += "///////////////////////////////////////////////////////////////////////////////\n\n";

            strQry += "using System;\n";
            strQry += "using System.ComponentModel;\n";
            strQry += "using System.ComponentModel.DataAnnotations;\n";
            strQry += "using NEXTIT.ERP.Message;\n";
            strQry += "\n\nnamespace " + nameSpace + ".BO\n";
            strQry += "\n{\n";
            strQry += "\tpublic partial class " + tableName + "EntityRef : " + tableName + "Entity\n";
            strQry += "\t{\n";
            strQry += "\n\n\n\n\t\t#region Base Table \n\n";
            strQry += "\t\t#region private variables\n\n";

            for (int i = 0; i < objclsColumnEntityList.Count; i++)
            {
                string nullable = "";
                if (objclsColumnEntityList[i].ColumnIsNull == false)
                {
                    nullable = "";
                }
                else if (objclsColumnEntityList[i].ColumnDotNetType != "string" && objclsColumnEntityList[i].ColumnIsNull == true && objclsColumnEntityList[i].ColumnDotNetType != "byte[]")
                {
                    nullable = "?";
                }
                strQry += "\t\tprivate " + objclsColumnEntityList[i].ColumnDotNetType + " " + nullable + " _" + objclsColumnEntityList[i].ColumnAliasName.Substring(0, 1).ToLower() + objclsColumnEntityList[i].ColumnAliasName.Substring(1, objclsColumnEntityList[i].ColumnAliasName.Length - 1) + ";\t\t\n";
            }

            strQry += "\n\t\t#endregion\n";
            strQry += "\n\n\n";
            strQry += "\t\t#region public properties\n\n";

            for (int i = 0; i < objclsColumnEntityList.Count; i++)
            {
                string nullable = "";

                if (objclsColumnEntityList[i].ColumnIsNull == false)
                {
                    nullable = "";
                }
                else if (objclsColumnEntityList[i].ColumnDotNetType != "string" && objclsColumnEntityList[i].ColumnIsNull == true && objclsColumnEntityList[i].ColumnDotNetType != "byte[]")
                {
                    nullable = "?";
                }

                strQry += "\t\t[DisplayName(\"" + objclsColumnEntityList[i].ColumnAliasName + "\")]\n";
                if (objclsColumnEntityList[i].ColumnIsNull == false)
                {
                    strQry += "\t\t[Required(ErrorMessage = \"" + objclsColumnEntityList[i].ColumnAliasName + " is required.\")]\n";
                }
                if (objclsColumnEntityList[i].ColumnDotNetType == "string")
                {
                    strQry += "\t\t[StringLength(" + objclsColumnEntityList[i].Lenght + ", ErrorMessage = \"" + objclsColumnEntityList[i].ColumnAliasName + " must be under  " + objclsColumnEntityList[i].Lenght + " characters\")]\n";
                }
                else if (objclsColumnEntityList[i].ColumnDotNetType == "long")
                {
                    strQry += "\t\t[Range(0,long.MaxValue,ErrorMessage=\"" + objclsColumnEntityList[i].ColumnAliasName + " must be between 0 and " + long.MaxValue + "\")]\n";
                }
                else if (objclsColumnEntityList[i].ColumnDotNetType == "int")
                {
                    strQry += "\t\t[Range(0,int.MaxValue,ErrorMessage=\"" + objclsColumnEntityList[i].ColumnAliasName + " must be between 0 and " + int.MaxValue + "\")]\n";
                }
                else if (objclsColumnEntityList[i].ColumnDotNetType == "double")
                {
                    strQry += "\t\t[Range(0,double.MaxValue,ErrorMessage=\"" + objclsColumnEntityList[i].ColumnAliasName + " must be between 0 and " + double.MaxValue + "\")]\n";
                }
                strQry += "\t\t[ValidationGroup(\"" + tableName + "\")]\n";
                strQry += "\t\tpublic " + objclsColumnEntityList[i].ColumnDotNetType + " " + nullable + " " + objclsColumnEntityList[i].ColumnAliasName + "\n\t\t{\n";
                strQry += "\t\t\tget { return " + "_" + objclsColumnEntityList[i].ColumnAliasName.Substring(0, 1).ToLower() + objclsColumnEntityList[i].ColumnAliasName.Substring(1, objclsColumnEntityList[i].ColumnAliasName.Length - 1) + "; }\n";
                strQry += "\t\t\tset {" + " _" + objclsColumnEntityList[i].ColumnAliasName.Substring(0, 1).ToLower() + objclsColumnEntityList[i].ColumnAliasName.Substring(1, objclsColumnEntityList[i].ColumnAliasName.Length - 1) + " = value; }\n";
                strQry += "\t\t}\n\n";
            }
            strQry += "\t\t#endregion\n\n";
            strQry += "\t\t#endregion\n\n";

            ////////////////////////////////////////////////////////////////////////
            // For Search 

            strQry += "\n\n\n\n\t\t#region Collection Attrebure \n\n";

            strQry += "\t\t#region private variables\n\n";


            for (int i = 0; i < objclsColumnEntityList.Count; i++)
            {
                strQry += "\t\tprivate string " + " _" + objclsColumnEntityList[i].ColumnAliasName.Substring(0, 1).ToLower() + objclsColumnEntityList[i].ColumnAliasName.Substring(1, objclsColumnEntityList[i].ColumnAliasName.Length - 1) + "Collection;\t\t\n";
            }

            strQry += "\n\t\t#endregion\n";
            strQry += "\n\n\n";
            strQry += "\t\t#region public properties\n\n";
            for (int i = 0; i < objclsColumnEntityList.Count; i++)
            {

                strQry += "\t\tpublic string " + " " + objclsColumnEntityList[i].ColumnAliasName + "Collection\n\t\t{\n";
                strQry += "\t\t\tget { return " + "_" + objclsColumnEntityList[i].ColumnAliasName.Substring(0, 1).ToLower() + objclsColumnEntityList[i].ColumnAliasName.Substring(1, objclsColumnEntityList[i].ColumnAliasName.Length - 1) + "Collection; }\n";
                strQry += "\t\t\tset {" + " _" + objclsColumnEntityList[i].ColumnAliasName.Substring(0, 1).ToLower() + objclsColumnEntityList[i].ColumnAliasName.Substring(1, objclsColumnEntityList[i].ColumnAliasName.Length - 1) + "Collection = value; }\n";
                strQry += "\t\t}\n\n";
            }
            strQry += "\t\t#endregion\n\n";
            strQry += "\t\t#endregion\n\n";
            strQry += "\t}\n}";

            return strQry;
        }
    }
}

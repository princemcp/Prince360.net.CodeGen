using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Mega.CodeGen.MVC2Engine
{
    public class ModelEngine
    {
        public bool ModelEngineMethod(IList<clsTableRefEntity> objclsTableRefEntityList, IList<clsColumnEntity> objclsColumnEntityList, string nameSpace, string tableName, string path, string DataBaseName)
        {
            path = path + "\\Model";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\View" + tableName + ".cs";

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

            strQry += "///////////////////////////////////////////////////////////////////////////////\n";
            strQry += "//      Author      : Maxima Prince                                           \n";
            strQry += "//      Date        : " + DateTime.Now + "                                    \n";
            strQry += "//      File name   : View" + tableName + ".cs                                \n";
            strQry += "//////////////////////////////////////////////////////////////////////////////\n\n";

            strQry += "using System.Collections.Generic;\n";
            strQry += "using System.ComponentModel.DataAnnotations;\n";
            strQry += "using System.ComponentModel;\n";
            strQry += "using NEXTIT.ERP.Message;\n";
            strQry += "using " + nameSpace + ".BO;\n";
            strQry += "using System.Web.Mvc;\n\n";
            strQry += "namespace " + nameSpace + ".MODEL\n";
            strQry += "{\n";
            strQry += "\tpublic partial class View" + tableName + "\n";
            strQry += "\t{\n";

            strQry += "\n\t\tprivate " + tableName + "Entity _" + tableName.Substring(0, 1).ToLower() + tableName.Substring(1, tableName.Length - 1) + "  = new " + tableName + "Entity();\n\n";
            strQry += "\t\tpublic " + tableName + "Entity " + tableName + "\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\tget { return " + "_" + tableName.Substring(0, 1).ToLower() + tableName.Substring(1, tableName.Length - 1) + "; }\n";
            strQry += "\t\t\tset {" + "_" + tableName.Substring(0, 1).ToLower() + tableName.Substring(1, tableName.Length - 1) + " = value; }\n";
            strQry += "\t\t}\n\n";

            for (int i = 0; i < objclsColumnEntityList.Count; i++)
            {
                if (objclsColumnEntityList[i].IsMasterTable == true)
                {
                    if (objclsColumnEntityList[i].HasReference == true && objclsColumnEntityList[i].IsHidden == false)
                    {
                        string variable = objclsColumnEntityList[i].ReferenceTableName + "_" + objclsColumnEntityList[i].RefColumnName + "_" + objclsColumnEntityList[i].ColumnAliasName;
                        strQry += "\n\t\tprivate List<SelectListItem> " + "_" + variable.Substring(0, 1).ToLower() + variable.Substring(1, variable.Length - 1) + ";\n\n";
                        strQry += "\t\t[DisplayName(\"Select " + objclsColumnEntityList[i].Lable + "\")]\n";
                        if (objclsColumnEntityList[i].ColumnIsNull == false)
                        {
                            //strQry += "\t\t[Required(AllowEmptyStrings = false, ErrorMessage = \"Please Select " + objclsColumnEntityList[i].Lable+ "\")]\n";
                            strQry += "\t\t[Range(1, long.MaxValue, ErrorMessage = \"Please Select " + objclsColumnEntityList[i].Lable + "\")]\n";
                        }
                        else
                        {
                            strQry += "\t\t[Range(0, long.MaxValue, ErrorMessage = \"Please Select " + objclsColumnEntityList[i].Lable + "\")]\n";
                        }
                        //strQry += "\t\t[Range(1, long.MaxValue, ErrorMessage = \"Please Select " + objclsColumnEntityList[i].Lable+ "\")]\n";
                        strQry += "\t\t[ValidationGroup(\"" + tableName + "\")]\n";
                        strQry += "\t\tpublic List<SelectListItem> ddl" + variable.Trim() + "\n";
                        strQry += "\t\t{\n";
                        strQry += "\t\t\tget { return " + "_" + variable.Substring(0, 1).ToLower() + variable.Substring(1, variable.Length - 1) + "; }\n";
                        strQry += "\t\t\tset { " + "_" + variable.Substring(0, 1).ToLower() + variable.Substring(1, variable.Length - 1) + " = value; }\n";
                        strQry += "\t\t}\n\n";
                    }
                }
            }
            strQry += "\t}\n";
            strQry += "}\n";

            return strQry;
        }
    }
}

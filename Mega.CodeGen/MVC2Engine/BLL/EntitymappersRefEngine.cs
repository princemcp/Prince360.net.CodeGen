using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Mega.CodeGen.MVC2Engine
{
    public class EntitymappersRefEngine
    {
        public bool EntitymappersRefEngineMethod(IList<clsTableRefEntity> objclsTableRefEntityList, IList<clsColumnEntity> objclsColumnEntityList, string nameSpace, string tableName, string path, string DataBaseName)
        {
            path = path + "\\Entitymappers.ReferenceMap";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\EM" + tableName + "Ref.cs";

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

            //for (int i = objclsColumnEntityList.Count - 1; i >= 0; i--)
            //{
            //    if (objclsColumnEntityList[i].IsMasterTable == true)
            //    {
            //        objclsColumnEntityList.RemoveAt(i);
            //    }
            //}

            strQry += "///////////////////////////////////////////////////////////////////////////////\n";
            strQry += "//      Author      : Maxima Prince              \n";
            strQry += "//      Date        : " + DateTime.Now + "     \n";
            strQry += "//      File name   : EM" + tableName + "Ref.cs     \n";
            strQry += "///////////////////////////////////////////////////////////////////////////////\n\n";

            strQry += "using System;";
            strQry += "\n\nnamespace " + nameSpace + ".ENTITYMAPPERS\n";
            strQry += "\n{\n";
            strQry += "\tpublic partial class EM" + tableName + "Ref\n";
            strQry += "\t{\n";
            strQry += "\t\tpublic static BO." + tableName + "EntityRef SetToBusinessObject(DATA.spGet" + tableName + "_Result obj" + tableName + "Entity)\n\t\t{\n";
            strQry += "\t\t\tBO." + tableName + "EntityRef obj" + tableName + " = new BO." + tableName + "EntityRef();\n\n";
            for (int i = 0; i < objclsColumnEntityList.Count; i++)
            {
                strQry += "\t\t\tobj" + tableName + "." + objclsColumnEntityList[i].ColumnAliasName + " = obj" + tableName + "Entity." + objclsColumnEntityList[i].ColumnAliasName + ";\n";
            }
            strQry += "\n\t\t\treturn obj" + tableName + ";\n\t\t}\n\n";

            strQry += "\n\t}\n";
            strQry += "}\n";

            return strQry;
        }
    }
}

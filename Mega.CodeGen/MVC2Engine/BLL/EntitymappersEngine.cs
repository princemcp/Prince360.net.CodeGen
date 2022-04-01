using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Mega.CodeGen.MVC2Engine
{
    public class EntitymappersEngine
    {
        public bool EntitymappersEngineMethod(IList<clsTableRefEntity> objclsTableRefEntityList, IList<clsColumnEntity> objclsColumnEntityList, string nameSpace, string tableName, string path, string DataBaseName)
        {
            path = path + "\\Entitymappers";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\EM" + tableName + ".cs";

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

            strQry += "///////////////////////////////////////////////////////////////////////////////\n";
            strQry += "//      Author      : Maxima Prince              \n";
            strQry += "//      Date        : " + DateTime.Now + "     \n";
            strQry += "//      File name   : EM" + tableName + ".cs     \n";
            strQry += "///////////////////////////////////////////////////////////////////////////////\n\n";

            strQry += "using System;";
            strQry += "\n\nnamespace " + nameSpace + ".ENTITYMAPPERS\n";
            strQry += "\n{\n";
            strQry += "\tpublic partial class EM" + tableName + "\n";
            strQry += "\t{\n";
            
            
            strQry += "\t\tpublic static BO." + tableName + "Entity SetToBusinessObject(DATA." + tableName + " obj" + tableName + "Entity)\n\t\t{\n";
            strQry += "\t\t\tBO." + tableName + "Entity obj" + tableName + " = new BO." + tableName + "Entity();\n\n";
            for (int i = 0; i < objclsColumnEntityList.Count; i++)
            {
                strQry += "\t\t\tobj" + tableName + "." + objclsColumnEntityList[i].ColumnName + " = obj" + tableName + "Entity." + objclsColumnEntityList[i].ColumnName + ";\n";
            }
            strQry += "\n\t\t\treturn obj" + tableName + ";\n\t\t}\n\n";
            
            
            strQry += "\t\tpublic static DATA." + tableName + " SetToEntity(DATA." + tableName + " entity, BO." + tableName + "Entity model)\n\t\t{\n";
            for (int i = 0; i < objclsColumnEntityList.Count; i++)
            {
                strQry += "\t\t\tentity." + objclsColumnEntityList[i].ColumnName + " = model." + objclsColumnEntityList[i].ColumnName + ";\n";
            }
            strQry += "\n\t\t\treturn entity;\n\t\t}\n";
            
            strQry += "\n\t}\n";
            strQry += "}\n";

            return strQry;
        }
    }
}

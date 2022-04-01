using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Mega.CodeGen.MVC2Engine
{
    public class IDALEngine
    {
        public bool IDALEngineMethod(IList<clsTableRefEntity> objclsTableRefEntityList, IList<clsColumnEntity> objclsColumnEntityList, string nameSpace, string tableName, string path, string DataBaseName)
        {
            path = path + "\\IDAL";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\I" + tableName + ".cs";

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

            strQry = "///////////////////////////////////////////////////////////////////////////////\n";
            strQry += "//      Author      : Maxima Prince              \n";
            strQry += "//      Date        : " + DateTime.Now + "     \n";
            strQry += "//      File name   : I" + tableName + ".cs      \n";
            strQry += "///////////////////////////////////////////////////////////////////////////////\n\n";


            strQry += "using System;\n";
            strQry += "using System.Collections.Generic;\n";
            strQry += "\n\nnamespace " + nameSpace + ".IDAL\n";
            strQry += "{\n";
            strQry += "\tpublic partial interface I" + tableName + "\n";
            strQry += "\t{\n\n";
            strQry += "\t\tint Insert(BO." + tableName + "Entity " + "obj" + tableName + "Entity);\n\n";
            strQry += "\t\tint Update(" + objclsColumnEntityList[0].ColumnDotNetType + " " + objclsColumnEntityList[0].ColumnName + ", BO." + tableName + "Entity " + "obj" + tableName + "Entity);\n\n";
            strQry += "\t\tint Update(BO." + tableName + "Entity " + "objO" + tableName + "Entity, BO." + tableName + "Entity " + "objM" + tableName + "Entity);\n\n";
            strQry += "\t\tint Delete(" + objclsColumnEntityList[0].ColumnDotNetType + " " + objclsColumnEntityList[0].ColumnName + ");\n\n";
            strQry += "\t\tint Delete(BO." + tableName + "Entity " + "obj" + tableName + "Entity);\n\n";
            strQry += "\t\tIList<BO." + tableName + "Entity> Get" + tableName + "By" + objclsColumnEntityList[0].ColumnName.ToUpper() + "(" + objclsColumnEntityList[0].ColumnDotNetType + " " + objclsColumnEntityList[0].ColumnName + ");\n\n";
            strQry += "\t\tIList<BO." + tableName + "Entity> Get" + tableName + "By" + objclsColumnEntityList[0].ColumnName.ToUpper() + "WithPaging(BO." + tableName + "Entity obj" + tableName + "Entity);\n\n";
            strQry += "\t\tIList<BO." + tableName + "Entity> Get" + tableName + "s();\n\n";
            strQry += "\t\tIList<BO." + tableName + "Entity> Get" + tableName + "sWithPaging(BO." + tableName + "Entity obj" + tableName + "Entity);\n\n";
            strQry += "\t\tIList<BO." + tableName + "Entity> Get" + tableName + "ByOther(string colNameValue);\n\n";
            strQry += "\t\tIList<BO." + tableName + "Entity> Get" + tableName + "ByOtherWithPaging(string colNameValue, BO." + tableName + "Entity obj" + tableName + "Entity);\n";
            strQry += "\n\t\tIList<BO." + tableName + "EntityRef> Get" + tableName + "SP(BO." + tableName + "EntityRef obj_" + tableName + "EntityRef);\n";
            strQry += "\n\t\tIList<BO." + tableName + "EntityRef> Search" + tableName + "SP(BO." + tableName + "EntityRef obj_" + tableName + "EntityRef);";

            strQry += "\n\t}\n";

            strQry += "}\n";

            return strQry;
        }
    }
}

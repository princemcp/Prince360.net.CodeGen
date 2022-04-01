using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Mega.CodeGen.MVC2Engine
{
    public class BLLEngine
    {
        public bool EntityEngineMethod(IList<clsTableRefEntity> objclsTableRefEntityList, IList<clsColumnEntity> objclsColumnEntityList, string nameSpace, string tableName, string path, string DataBaseName)
        {
            path = path + "\\BLL";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\B" + tableName + ".cs";

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
            strQry += "//      File name   : B" + tableName + ".cs     \n";
            strQry += "///////////////////////////////////////////////////////////////////////////////\n\n";


            strQry += "using System;\n";
            strQry += "using System.Collections.Generic;\n";
            strQry += "using System.Linq;\n";
            strQry += "using System.Text;\n";
            strQry += "\n\nnamespace " + nameSpace + ".BLL\n";
            strQry += "{\n";
            strQry += "\tpublic partial class B" + tableName + " : IDAL.I" + tableName + "\n";
            strQry += "\t{\n";
            strQry += "\t\tprivate IDAL.I" + tableName + " _i" + tableName + " = null;\n\n";
            strQry += "\t\t#region CONSTRUCTORS\n\n";
            strQry += "\t\tpublic B" + tableName + "() : this ( new DAL.D" + tableName + "())\n";
            strQry += "\t\t{\n";
            strQry += "\t\t}\n";
            strQry += "\t\tpublic B" + tableName + "(IDAL.I" + tableName + " Element)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\tthis._i" + tableName + " = Element;\n";
            strQry += "\t\t}\n\n";
            strQry += "\t\t#endregion\n\n";
            
            strQry += "\t\tpublic int Insert (BO." + tableName + "Entity " + "obj" + tableName + "Entity)\n\t\t{\n";
            strQry += "\t\t\ttry\n\t\t\t{\n";
            strQry += "\t\t\t\treturn _i" + tableName + ".Insert(" + "obj" + tableName + "Entity);\n";
            strQry += "\t\t\t}\n\t\t\tcatch(Exception ex)\n\t\t\t{\n\t\t\t\tthrow ex;\n\t\t\t}\n";
            strQry += "\t\t}\n\n";
            
            strQry += "\t\tpublic int Update (" + objclsColumnEntityList[0].ColumnDotNetType + " " + objclsColumnEntityList[0].ColumnName + ", BO." + tableName + "Entity " + "obj" + tableName + "Entity)\n\t\t{\n";
            strQry += "\t\t\ttry\n\t\t\t{\n";
            strQry += "\t\t\t\treturn _i" + tableName + ".Update(" + objclsColumnEntityList[0].ColumnName + ", " + "obj" + tableName + "Entity);\n";
            strQry += "\t\t\t}\n\t\t\tcatch(Exception ex)\n\t\t\t{\n\t\t\t\tthrow ex;\n\t\t\t}\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\tpublic int Update (BO." + tableName + "Entity " + "objO" + tableName + "Entity, BO." + tableName + "Entity " + "objM" + tableName + "Entity)\n\t\t{\n";
            strQry += "\t\t\ttry\n\t\t\t{\n";
            strQry += "\t\t\t\treturn _i" + tableName + ".Update(objO" + tableName + "Entity, " + "objM" + tableName + "Entity);\n";
            strQry += "\t\t\t}\n\t\t\tcatch(Exception ex)\n\t\t\t{\n\t\t\t\tthrow ex;\n\t\t\t}\n";
            strQry += "\t\t}\n\n";


            strQry += "\t\tpublic int Delete (" + objclsColumnEntityList[0].ColumnDotNetType + " " + objclsColumnEntityList[0].ColumnName + ")\n\t\t{\n";
            strQry += "\t\t\ttry\n\t\t\t{\n";
            strQry += "\t\t\t\treturn _i" + tableName + ".Delete(" + objclsColumnEntityList[0].ColumnName + ");\n";
            strQry += "\t\t\t}\n\t\t\tcatch(Exception ex)\n\t\t\t{\n\t\t\t\tthrow ex;\n\t\t\t}\n";
            strQry += "\t\t}\n\n";
            strQry += "\t\tpublic int Delete (BO." + tableName + "Entity " + "obj" + tableName + "Entity)\n\t\t{\n";
            strQry += "\t\t\ttry\n\t\t\t{\n";
            strQry += "\t\t\t\treturn _i" + tableName + ".Delete(obj" + tableName + "Entity);\n";
            strQry += "\t\t\t}\n\t\t\tcatch(Exception ex)\n\t\t\t{\n\t\t\t\tthrow ex;\n\t\t\t}\n";
            strQry += "\t\t}\n\n";


            strQry += "\t\tpublic IList<BO." + tableName + "Entity> Get" + tableName + "By" + objclsColumnEntityList[0].ColumnName.ToUpper() + "(" + objclsColumnEntityList[0].ColumnDotNetType + " " + objclsColumnEntityList[0].ColumnName + ")\n\t\t{\n";
            strQry += "\t\t\ttry\n\t\t\t{\n";
            strQry += "\t\t\t\treturn _i" + tableName + ".Get" + tableName + "By" + objclsColumnEntityList[0].ColumnName.ToUpper() + "(" + objclsColumnEntityList[0].ColumnName + ");\n";
            strQry += "\t\t\t}\n\t\t\tcatch(Exception ex)\n\t\t\t{\n\t\t\t\tthrow ex;\n\t\t\t}\n";
            strQry += "\t\t}\n\n";
            strQry += "\t\tpublic IList<BO." + tableName + "Entity> Get" + tableName + "By" + objclsColumnEntityList[0].ColumnName.ToUpper() + "WithPaging(BO." + tableName + "Entity obj" + tableName + "Entity)\n\t\t{\n";
            strQry += "\t\t\ttry\n\t\t\t{\n";
            strQry += "\t\t\t\treturn _i" + tableName + ".Get" + tableName + "By" + objclsColumnEntityList[0].ColumnName.ToUpper() + "WithPaging( obj" + tableName + "Entity);\n";
            strQry += "\t\t\t}\n\t\t\tcatch(Exception ex)\n\t\t\t{\n\t\t\t\tthrow ex;\n\t\t\t}\n";
            strQry += "\t\t}\n\n";


            strQry += "\t\tpublic IList<BO." + tableName + "Entity> Get" + tableName + "s()\n\t\t{\n";
            strQry += "\t\t\ttry\n\t\t\t{\n";
            strQry += "\t\t\t\treturn _i" + tableName + ".Get" + tableName + "s();\n";
            strQry += "\t\t\t}\n\t\t\tcatch(Exception ex)\n\t\t\t{\n\t\t\t\tthrow ex;\n\t\t\t}\n";
            strQry += "\t\t}\n\n";
            strQry += "\t\tpublic IList<BO." + tableName + "Entity> Get" + tableName + "sWithPaging(BO." + tableName + "Entity obj" + tableName + "Entity)\n\t\t{\n";
            strQry += "\t\t\ttry\n\t\t\t{\n";
            strQry += "\t\t\t\treturn _i" + tableName + ".Get" + tableName + "sWithPaging( obj" + tableName + "Entity);\n";
            strQry += "\t\t\t}\n\t\t\tcatch(Exception ex)\n\t\t\t{\n\t\t\t\tthrow ex;\n\t\t\t}\n";
            strQry += "\t\t}\n\n";


            strQry += "\t\tpublic IList<BO." + tableName + "Entity> Get" + tableName + "ByOther(string colNameValue)\n\t\t{\n";
            strQry += "\t\t\ttry\n\t\t\t{\n";
            strQry += "\t\t\t\treturn _i" + tableName + ".Get" + tableName + "ByOther(colNameValue);\n";
            strQry += "\t\t\t}\n\t\t\tcatch(Exception ex)\n\t\t\t{\n\t\t\t\tthrow ex;\n\t\t\t}\n";
            strQry += "\t\t}\n\n";
            strQry += "\t\tpublic IList<BO." + tableName + "Entity> Get" + tableName + "ByOtherWithPaging(string colNameValue, BO." + tableName + "Entity obj" + tableName + "Entity)\n\t\t{\n";
            strQry += "\t\t\ttry\n\t\t\t{\n";
            strQry += "\t\t\t\treturn _i" + tableName + ".Get" + tableName + "ByOtherWithPaging(colNameValue, obj" + tableName + "Entity);\n";
            strQry += "\t\t\t}\n\t\t\tcatch(Exception ex)\n\t\t\t{\n\t\t\t\tthrow ex;\n\t\t\t}\n";
            strQry += "\t\t}\n";


            strQry += "\n\t\tpublic IList<BO." + tableName + "EntityRef> Get" + tableName + "SP(BO." + tableName + "EntityRef obj_" + tableName + "EntityRef)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\treturn _i" + tableName + ".Get" + tableName + "SP(obj_" + tableName + "EntityRef);\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch(Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tthrow ex;\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t}\n";


            strQry += "\n\t\tpublic IList<BO." + tableName + "EntityRef> Search" + tableName + "SP(BO." + tableName + "EntityRef obj_" + tableName + "EntityRef)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\treturn _i" + tableName + ".Search" + tableName + "SP(obj_" + tableName + "EntityRef);\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch(Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tthrow ex;\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t}\n";


            strQry += "\t}\n";
            strQry += "}\n";

            return strQry;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mega.CodeGen.WebAPI.netCore
{
    public class FCC
    {
        public bool FCCEngineMethod(IList<clsTableRefEntity> objclsTableRefEntityList, IList<clsColumnEntity> objclsColumnEntityList, string nameSpace, string tableName, string path, string DataBaseName)
        {
            path = path + "\\FCC";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\" + tableName + "FCC.cs";

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
            strQry += "//      Author      : SM Habib Ullah -- Prince\n";
            strQry += "//      Web         : https://www.Prince360.net\n";
            strQry += "//      Date        : " + DateTime.Now.ToString("dd-MM-yyyy") + "     \n";
            strQry += "//      File name   : " + tableName + "FCC.cs      \n";
            strQry += "///////////////////////////////////////////////////////////////////////////////\n\n";


            strQry += "using " + nameSpace.Split('.')[0] + ".BLL." + nameSpace.Split('.')[2] + ";\n";
            strQry += "using " + nameSpace.Split('.')[0] + ".Entities;\n";
            strQry += "using " + nameSpace.Split('.')[0] + ".iBLL." + nameSpace.Split('.')[2] + ";\n";
            strQry += "\n\nnamespace " + nameSpace + "\n";
            strQry += "{\n";
            strQry += "\tpublic class " + tableName + "FCC\n";
            strQry += "\t{\n\n";
            strQry += "\t\t#region constructor \n\n";
            strQry += "\t\tpublic " + tableName + "FCC()\n";
            strQry += "\t\t{\n";
            strQry += "\t\t}\n\n";
            strQry += "\t\t#endregion constructor \n\n";
            strQry += "\t\tpublic static i" + tableName + "_BLL GetFacadeCreate(AppCapsule appCapsule)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\treturn new " + tableName + "_BLL(appCapsule);\n";
            strQry += "\t\t}\n\n";


            strQry += "\t}\n";

            strQry += "}\n";

            return strQry;
        }

        public string GetMasterDetailsMethods(IList<clsColumnEntity> objclsColumnEntityList, string tableName, string tab)
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
                    strQry += "long SaveMasterDet" + refToTableName + "(" + tableName + "Entity masterEntity, IList<" + refToTableName + "Entity> listAdded, IList<" + refToTableName + "Entity> listUpdated, IList<" + refToTableName + "Entity> listDeleted);" + tab;
                }
            }
            return strQry;
        }


    }
}


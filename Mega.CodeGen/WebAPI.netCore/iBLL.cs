using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mega.CodeGen.WebAPI.netCore
{
    public class iBLL
    {
        public bool iBLLEngineMethod(IList<clsTableRefEntity> objclsTableRefEntityList, IList<clsColumnEntity> objclsColumnEntityList, string nameSpace, string tableName, string path, string DataBaseName)
        {
            path = path + "\\iBLL";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\i" + tableName + "_BLL.cs";

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
            strQry += "//      File name   : i" + tableName + "_BLL.cs      \n";
            strQry += "///////////////////////////////////////////////////////////////////////////////\n\n";


            strQry += "using " + nameSpace.Split('.')[0] + ".Entities." + nameSpace.Split('.')[2] + ";\n";
            strQry += "using System.Collections.Generic;\n";
            strQry += "\n\nnamespace " + nameSpace + "\n";
            strQry += "{\n";
            strQry += "\tpublic partial interface i" + tableName + "_BLL\n";
            strQry += "\t{\n\n";
            strQry += "\t\t#region Save Update Delete List with Single Entity\n\n";
            strQry += "\t\tlong Add(" + tableName + "Entity entity);\n\n";
            strQry += "\t\tlong Update(" + tableName + "Entity entity);\n\n";
            strQry += "\t\tlong Delete(" + tableName + "Entity entity);\n\n";
            strQry += "\t\tlong SaveList(List<" + tableName + "Entity> allLists);\n\n";
            strQry += "\t\t#endregion Save Update Delete List\n\n";
            strQry += "\t\t#region GetAll\n\n";
            strQry += "\t\tIList<" + tableName + "Entity> GetAll(" + tableName + "Entity entity);\n\n";
            strQry += "\t\tIList<" + tableName + "Entity> GetAllByPages(" + tableName + "Entity entity);\n\n";
            strQry += "\t\t#endregion GetAll\n\n";
            strQry += "\t\t#region SaveMasterDetails\n\n";
            strQry += "\t\t" + GetMasterDetailsMethods(objclsColumnEntityList, tableName, "\n\n\t\t");
            if (objclsColumnEntityList[0].RefarenceToTable != null)
            {
                strQry += "long SaveMasterDetails(" + tableName + "Entity masterEntity);\n";
            }
            strQry += "\n\t\t#endregion SaveMasterDetails\n\n";
            


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
                    strQry += "long SaveMasterDet" + refToTableName + "(" + tableName + "Entity masterEntity, List<" + refToTableName + "Entity> DetailList);" + tab;
                }
            }
            return strQry;
        }


    }
}


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mega.CodeGen.WebAPI.netCore
{
    class iDAL_Ext
    {
        public bool iDAL_ExtEngineMethod(IList<clsTableRefEntity> objclsTableRefEntityList, IList<clsColumnEntity> objclsColumnEntityList, string nameSpace, string tableName, string path, string DataBaseName)
        {
            path = path + "\\iDAL_Ext";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\i" + tableName + ".cs";

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
            strQry += "//      File name   : i" + tableName + ".cs      \n";
            strQry += "///////////////////////////////////////////////////////////////////////////////\n\n";


            strQry += "using " + nameSpace.Split('.')[0] + ".Entities." + nameSpace.Split('.')[2] + ";\n";
            strQry += "using System.Collections.Generic;\n";
            strQry += "\n\nnamespace " + nameSpace + "\n";
            strQry += "{\n";
            strQry += "\tpublic partial interface i" + tableName + "\n";
            strQry += "\t{\n\n";
            strQry += "\t\t#region Update_Ext Delete_Ext List \n\n";
           
            strQry += "\t\tlong Update_Ext(" + tableName + "Entity entity);\n\n";
            strQry += "\t\tlong Delete_Ext(" + tableName + "Entity entity);\n\n";
            //strQry += "\t\t--long SaveList(IList<" + tableName + "Entity> listAdded, IList<" + tableName + "Entity> listUpdated, IList<" + tableName + "Entity> listDeleted);\n\n";
            strQry += "\t\t#endregion Save Update Delete List\n\n";
            strQry += "\t\t#region GetAll\n\n";
            strQry += "\t\tIList<" + tableName + "Entity_Ext> GetAll_Ext(" + tableName + "Entity entity);\n\n";
            strQry += "\t\tIList<" + tableName + "Entity_Ext> GetAllByPages_Ext(" + tableName + "Entity entity);\n\n";
            strQry += "\t\t#endregion GetAll\n\n";
            strQry += "\t\t#region SaveMasterDetails\n\n";
            strQry += "\t\t" + GetMasterDetailsMethods(objclsColumnEntityList, tableName, "\n\n\t\t");

          

            strQry += "#endregion SaveMasterDetails\n\n";


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
                    strQry += "long SaveMasterDet" + refToTableName + "_Ext(" + tableName + "Entity masterEntity, IList<" + refToTableName + "Entity> listAdded, IList<" + refToTableName + "Entity> listUpdated, IList<" + refToTableName + "Entity> listDeleted);" + tab;
                }
            }
            return strQry;
        }


    }
}


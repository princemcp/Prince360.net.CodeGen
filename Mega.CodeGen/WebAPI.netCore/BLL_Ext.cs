using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mega.CodeGen.WebAPI.netCore
{
    public class BLL_Ext
    {
        public bool BLL_ExtEngineMethod(IList<clsTableRefEntity> objclsTableRefEntityList, IList<clsColumnEntity> objclsColumnEntityList, string nameSpace, string tableName, string path, string DataBaseName)
        {
            path = path + "\\BLL_Ext";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\" + tableName + "_BLL.cs";

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
            strQry += "//      File name   : " + tableName + "_BLL.cs      \n";
            strQry += "///////////////////////////////////////////////////////////////////////////////\n\n";

            strQry += "using " + nameSpace.Split('.')[0] + ".Entities;\n";
            strQry += "using " + nameSpace.Split('.')[0] + ".Entities." + nameSpace.Split('.')[2] + ";\n";
            strQry += "using " + nameSpace.Split('.')[0] + ".iBLL." + nameSpace.Split('.')[2] + ";\n";
            strQry += "using " + nameSpace.Split('.')[0] + ".iDAL." + nameSpace.Split('.')[2] + ";\n";
            strQry += "using System.Collections.Generic;\n";
            strQry += "\n\nnamespace " + nameSpace + "\n";
            strQry += "{\n";
            strQry += "\tpublic partial class " + tableName + "_BLL\n";
            strQry += "\t{\n";

            strQry += "\t\t#region Update_Ext Delete_Ext List \n\n";

          

            strQry += "\t\tlong i" + tableName + "_BLL.Update_Ext(" + tableName + "Entity entity)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\treturn ((i" + tableName + ")new DAL." + nameSpace.Split('.')[2] + "." + tableName + "(base.appCapsule)).Update_Ext(entity);\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\tlong i" + tableName + "_BLL.Delete_Ext(" + tableName + "Entity entity)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\treturn ((i" + tableName + ")new DAL." + nameSpace.Split('.')[2] + "." + tableName + "(base.appCapsule)).Delete_Ext(entity);\n";
            strQry += "\t\t}\n\n";

     


            strQry += "\t\t#endregion Save Update Delete List\n\n";
            strQry += "\t\t#region GetAll_Ext\n\n";
            strQry += "\t\tIList<" + tableName + "Entity_Ext> i" + tableName + "_BLL.GetAll_Ext(" + tableName + "Entity entity)\n\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\treturn ((i" + tableName + ")new DAL." + nameSpace.Split('.')[2] + "." + tableName + "(base.appCapsule)).GetAll_Ext(entity);\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\tIList<" + tableName + "Entity_Ext> i" + tableName + "_BLL.GetAllByPages_Ext(" + tableName + "Entity entity)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\treturn ((i" + tableName + ")new DAL." + nameSpace.Split('.')[2] + "." + tableName + "(base.appCapsule)).GetAll_Ext(entity);\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\t#endregion GetAl_Extl\n\n";
            strQry += "\t\t#region SaveMasterDetails\n\n";
            strQry += "\t\t" + GetMasterDetailsMethods(objclsColumnEntityList, tableName, "\n\n\t\t", nameSpace.Split('.')[2]);
            strQry += "#endregion SaveMasterDetails\n\n";


            strQry += "\t}\n";

            strQry += "}\n";

            return strQry;
        }

        public string GetMasterDetailsMethods(IList<clsColumnEntity> objclsColumnEntityList, string tableName, string tab, string moduleName)
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
                    strQry += "long i" + tableName + "_BLL.SaveMasterDet" + refToTableName + "_Ext(" + tableName + "Entity masterEntity, List<" + refToTableName + "Entity> DetailList)\n";
                    strQry += "\t\t{\n";
                    strQry += "\t\t\tDetailList.ForEach(P => P.BaseSecurityParam = new SecurityCapsule());\n";
                    strQry += "\t\t\tDetailList.ForEach(P => P.BaseSecurityParam = masterEntity.BaseSecurityParam);\n";
                    strQry += "\t\t\tif (masterEntity.CurrentState == BaseEntity.EntityState.Deleted) { DetailList.ForEach(p => p.CurrentState = BaseEntity.EntityState.Deleted); }\n";
                    strQry += "\t\t\tIList<" + refToTableName + "Entity> listAdded = DetailList.FindAll(Item => Item.CurrentState == BaseEntity.EntityState.Added);\n";
                    strQry += "\t\t\tIList<" + refToTableName + "Entity> listUpdated = DetailList.FindAll(Item => Item.CurrentState == BaseEntity.EntityState.Changed);\n";
                    strQry += "\t\t\tIList<" + refToTableName + "Entity> listDeleted = DetailList.FindAll(Item => Item.CurrentState == BaseEntity.EntityState.Deleted);\n";
                    strQry += "\t\t\treturn ((i" + tableName + ")new DAL." + moduleName + "." + tableName + "(base.appCapsule)).SaveMasterDet" + refToTableName + "_Ext(masterEntity, listAdded, listUpdated, listDeleted);\n";
                    strQry += "\t\t}" + tab;
                }
            }
            return strQry;
        }


    }
}



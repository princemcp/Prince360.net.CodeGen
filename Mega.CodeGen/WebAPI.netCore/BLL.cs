using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mega.CodeGen.WebAPI.netCore
{
    public class BLL
    {
        public bool BLLEngineMethod(IList<clsTableRefEntity> objclsTableRefEntityList, IList<clsColumnEntity> objclsColumnEntityList, string nameSpace, string tableName, string path, string DataBaseName)
        {
            path = path + "\\BLL";
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
            strQry += "\tpublic partial class " + tableName + "_BLL : BaseBLL, i" + tableName + "_BLL\n";
            strQry += "\t{\n";
            strQry += "\t\t#region Constructors\n\n";
            strQry += "\t\tprivate string ClassName = \"" + tableName + "_BLL\";\n\n";
            strQry += "\t\tpublic " + tableName + "_BLL(AppCapsule appCapsule)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\tbase.appCapsule = appCapsule;\n";
            strQry += "\t\t}\n\n";
            strQry += "\t\tprivate string SourceOfException(string methodName)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\treturn \"Class name: \" + ClassName + \" and Method name: \" + methodName;\n";
            strQry += "\t\t}\n\n";
            strQry += "\t\t#endregion Constructors\n\n";

            strQry += "\t\t#region Save Update Delete List with Single Entity\n\n";

            strQry += "\t\tlong i" + tableName + "_BLL.Add(" + tableName + "Entity entity)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\treturn ((i" + tableName + ")new DAL." + nameSpace.Split('.')[2] + "." + tableName + "(base.appCapsule)).Add(entity);\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\tlong i" + tableName + "_BLL.Update(" + tableName + "Entity entity)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\treturn ((i" + tableName + ")new DAL." + nameSpace.Split('.')[2] + "." + tableName + "(base.appCapsule)).Update(entity);\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\tlong i" + tableName + "_BLL.Delete(" + tableName + "Entity entity)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\treturn ((i" + tableName + ")new DAL." + nameSpace.Split('.')[2] + "." + tableName + "(base.appCapsule)).Delete(entity);\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\tlong i" + tableName + "_BLL.SaveList(List<" + tableName + "Entity> allLists)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\tIList<" + tableName + "Entity> listAdded = allLists.FindAll(Item => Item.CurrentState == BaseEntity.EntityState.Added);\n";
            strQry += "\t\t\tIList<" + tableName + "Entity> listUpdated = allLists.FindAll(Item => Item.CurrentState == BaseEntity.EntityState.Changed);\n";
            strQry += "\t\t\tIList<" + tableName + "Entity> listDeleted = allLists.FindAll(Item => Item.CurrentState == BaseEntity.EntityState.Deleted);\n";
            strQry += "\t\t\treturn ((i" + tableName + ")new DAL." + nameSpace.Split('.')[2] + "." + tableName + "(base.appCapsule)).SaveList(listAdded, listUpdated, listDeleted);\n";
            strQry += "\t\t}\n\n";

            
            strQry += "\t\t#endregion Save Update Delete List\n\n";
            strQry += "\t\t#region GetAll\n\n";
            strQry += "\t\tIList<" + tableName + "Entity> i" + tableName + "_BLL.GetAll(" + tableName + "Entity entity)\n\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\treturn ((i" + tableName + ")new DAL." + nameSpace.Split('.')[2] + "." + tableName + "(base.appCapsule)).GetAll(entity);\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\tIList<" + tableName + "Entity> i" + tableName + "_BLL.GetAllByPages(" + tableName + "Entity entity)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\treturn ((i" + tableName + ")new DAL." + nameSpace.Split('.')[2] + "." + tableName + "(base.appCapsule)).GetAllByPages(entity);\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\t#endregion GetAll\n\n";
            strQry += "\t\t#region SaveMasterDetails\n\n";
            strQry += "\t\t" + GetMasterDetailsMethods(objclsColumnEntityList, tableName, "\n\n\t\t",   nameSpace.Split('.')[2] );
            if (objclsColumnEntityList[0].RefarenceToTable != null)
            {
                strQry += "long i" + tableName + "_BLL.SaveMasterDetails(" + tableName + "Entity masterEntity)\n";
                strQry += "\t\t{";
                strQry += "\t\t" + GetMasterDetails(objclsColumnEntityList, tableName, "\n\n\t\t", nameSpace.Split('.')[2]);
                strQry += "\t\t\treturn ((i" + tableName + ")new DAL." + nameSpace.Split('.')[2] + "." + tableName + "(base.appCapsule)).SaveMasterDetails(masterEntity,\n";
                strQry += "\t\t\t\t\t\t\t" + GetMasterDetailsMethodsParams(objclsColumnEntityList, tableName, "\n\t\t\t\t\t\t\t");
                strQry += "\n\t\t\t\t\t);";
                strQry += "\n\t\t}\n";
            }

            strQry += "\n\t\t#endregion SaveMasterDetails\n\n";


            strQry += "\t}\n";

            strQry += "}\n";

            return strQry;
        }

        public string GetMasterDetailsMethodsParams(IList<clsColumnEntity> objclsColumnEntityList, string tableName, string tab)
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
                    strQry += refToTableName + "listAdded," + tab;
                    strQry += refToTableName + "listUpdated," + tab;
                    strQry += refToTableName + "listDeleted," + tab;
                    
                }
            }
            strQry = strQry.Substring(0, strQry.Length - 9);
            return strQry;
        }


        public string GetMasterDetails(IList<clsColumnEntity> objclsColumnEntityList, string tableName, string tab, string moduleName)
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
                    strQry += "\n\t\t\tList<" + refToTableName + "Entity> " + refToTableName + "List = masterEntity." + refToTableName + "List;\n";
                    strQry += "\t\t\t"+ refToTableName + "List.ForEach(P => P.BaseSecurityParam = new SecurityCapsule());\n";
                    strQry += "\t\t\t" + refToTableName + "List.ForEach(P => P.BaseSecurityParam = masterEntity.BaseSecurityParam);\n";
                    strQry += "\t\t\tif (masterEntity.CurrentState == BaseEntity.EntityState.Deleted) { " + refToTableName + "List.ForEach(p => p.CurrentState = BaseEntity.EntityState.Deleted); }\n";
                    strQry += "\t\t\tIList<" + refToTableName + "Entity> " + refToTableName + "listAdded = " + refToTableName + "List.FindAll(Item => Item.CurrentState == BaseEntity.EntityState.Added);\n";
                    strQry += "\t\t\tIList<" + refToTableName + "Entity> " + refToTableName + "listUpdated = " + refToTableName + "List.FindAll(Item => Item.CurrentState == BaseEntity.EntityState.Changed);\n";
                    strQry += "\t\t\tIList<" + refToTableName + "Entity> " + refToTableName + "listDeleted = " + refToTableName + "List.FindAll(Item => Item.CurrentState == BaseEntity.EntityState.Deleted);\n";
                    //strQry += "\t\t\treturn ((i" + tableName + ")new DAL." + moduleName + "." + tableName + "(base.appCapsule)).SaveMasterDet" + refToTableName + "(masterEntity, listAdded, listUpdated, listDeleted);" + tab; ;
                }
            }
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
                    strQry += "long i" + tableName + "_BLL.SaveMasterDet" + refToTableName + "(" + tableName + "Entity masterEntity, List<" + refToTableName + "Entity> DetailList)\n" ;
                    strQry += "\t\t{\n";
                    strQry += "\t\t\tDetailList.ForEach(P => P.BaseSecurityParam = new SecurityCapsule());\n";
                    strQry += "\t\t\tDetailList.ForEach(P => P.BaseSecurityParam = masterEntity.BaseSecurityParam);\n";
                    strQry += "\t\t\tif (masterEntity.CurrentState == BaseEntity.EntityState.Deleted) { DetailList.ForEach(p => p.CurrentState = BaseEntity.EntityState.Deleted); }\n";
                    strQry += "\t\t\tIList<" + refToTableName + "Entity> listAdded = DetailList.FindAll(Item => Item.CurrentState == BaseEntity.EntityState.Added);\n";
                    strQry += "\t\t\tIList<" + refToTableName + "Entity> listUpdated = DetailList.FindAll(Item => Item.CurrentState == BaseEntity.EntityState.Changed);\n";
                    strQry += "\t\t\tIList<" + refToTableName + "Entity> listDeleted = DetailList.FindAll(Item => Item.CurrentState == BaseEntity.EntityState.Deleted);\n";
                    strQry += "\t\t\treturn ((i" + tableName + ")new DAL." + moduleName + "." + tableName + "(base.appCapsule)).SaveMasterDet" + refToTableName + "(masterEntity, listAdded, listUpdated, listDeleted);\n";
                    strQry += "\t\t}" + tab;
                }
            }
            return strQry;
        }


    }
}



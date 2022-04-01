using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mega.CodeGen.WebAPI.netCore
{
    public class DAL_Ext
    {
        public bool DAL_ExtEngineMethod(IList<clsTableRefEntity> objclsTableRefEntityList, IList<clsColumnEntity> objclsColumnEntityList, string nameSpace, string tableName, string path, string DataBaseName)
        {
            path = path + "\\DAL_Ext";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\" + tableName + ".cs";

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
            strQry += "//      File name   : " + tableName + ".cs      \n";
            strQry += "///////////////////////////////////////////////////////////////////////////////\n\n";

            strQry += "using " + nameSpace.Split('.')[0] + ".Entities;\n";
            strQry += "using " + nameSpace.Split('.')[0] + ".Entities." + nameSpace.Split('.')[2] + ";\n";
            strQry += "using " + nameSpace.Split('.')[0] + ".iDAL." + nameSpace.Split('.')[2] + ";\n";
            strQry += "using System;\n";
            strQry += "using System.Collections.Generic;\n";
            strQry += "using System.Data;\n";
            strQry += "using System.Data.Common;\n";
            strQry += "using System.Data.SqlClient;\n";
            strQry += "\n\nnamespace " + nameSpace + "\n";
            strQry += "{\n";
            strQry += "\tpublic partial class " + tableName + "\n";
            strQry += "\t{\n\n";
          

            strQry += "\t\tpublic List<DbParameter> FillParameters_Ext(" + tableName + "Entity_Ext entity, List<DbParameter> parameterList)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\treturn parameterList;\n";
            strQry += "\t\t}\n\n";


            strQry += "\t\t#region Save Update Delete List with Single Entity\n\n";

            

            strQry += "\t\tlong i" + tableName + ".Update_Ext(" + tableName + "Entity entity)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\tlong returnCode = 0;\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tconst string SP = \"" + tableName + "_Upd_Ext\";\n";
            strQry += "\t\t\t\tList<DbParameter> parameterList = FillParameters(entity, false);\n";
            strQry += "\t\t\t\tparameterList.Add(AddOutputParameter());\n";
            strQry += "\t\t\t\tFillSequrityParameters(parameterList, entity.BaseSecurityParam, false);\n";
            strQry += "\t\t\t\treturnCode = base.ExecuteNonQuery(entity, SP, parameterList, CommandType.StoredProcedure);\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch (Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tthrow ex;\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\treturn returnCode;\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\tlong i" + tableName + ".Delete_Ext(" + tableName + "Entity entity)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\tlong returnCode = 0;\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tconst string SP = \"" + tableName + "_Del_Ext\";\n";
            strQry += "\t\t\t\tList<DbParameter> parameterList = FillParameters(entity, true);\n";
            strQry += "\t\t\t\tparameterList.Add(AddOutputParameter());\n";
            strQry += "\t\t\t\tFillSequrityParameters(parameterList, entity.BaseSecurityParam, false);\n";
            strQry += "\t\t\t\treturnCode = base.ExecuteNonQuery(entity, SP, parameterList, CommandType.StoredProcedure);\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch (Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tthrow ex;\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\treturn returnCode;\n";
            strQry += "\t\t}\n\n";

           

            strQry += "\t\tpublic long SaveList_Ext(SqlConnection connection, SqlTransaction transaction, IList<" + tableName + "Entity> listAdded, IList<" + tableName + "Entity> listUpdated, IList<" + tableName + "Entity> listDeleted)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\tlong returnCode = 0;\n";
            //strQry += "\t\t\tInt64 PrimaryKeyMaster = -99;\n";

            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tthrow new NotImplementedException(\"Method { SaveList_Ext } need to be implemented.\");\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch (Exception ex)\n";
            strQry += "\t\t\t{\n";

            strQry += "\t\t\t\tthrow ex;\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tfinally\n";
            strQry += "\t\t\t{\n";

            strQry += "\t\t\t}\n";
            strQry += "\t\t\treturn returnCode;\n";
            strQry += "\t\t}\n";

            strQry += "\t\t#endregion Save Update Delete List\n\n";
            strQry += "\t\t#region GetAll_Ext\n\n";
            strQry += "\t\tIList<" + tableName + "Entity_Ext> i" + tableName + ".GetAll_Ext(" + tableName + "Entity entity)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\tList<" + tableName + "Entity_Ext> dalaList = new List<" + tableName + "Entity_Ext>();\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tList<DbParameter> parameterList = FillParameters(entity, false);\n";
            strQry += "\t\t\t\tusing (DbDataReader dataReader = base.GetDataReader(entity, \"" + tableName + "_GA_Ext\", parameterList, CommandType.StoredProcedure))\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\tif (dataReader != null && dataReader.HasRows)\n";
            strQry += "\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\twhile (dataReader.Read())\n";
            strQry += "\t\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\t\t" + tableName + "Entity_Ext entityObj = new " + tableName + "Entity_Ext(dataReader);\n";
            strQry += "\t\t\t\t\t\t\tdalaList.Add(entityObj);\n";
            strQry += "\t\t\t\t\t\t}\n";
            strQry += "\t\t\t\t\t}\n";
            strQry += "\t\t\t\t\tdataReader.Close();\n";
            strQry += "\t\t\t\t}\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch (Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tthrow ex;\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\treturn dalaList;\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\tIList<" + tableName + "Entity_Ext> i" + tableName + ".GetAllByPages_Ext(" + tableName + "Entity entity)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\tList<" + tableName + "Entity_Ext> dalaList = new List<" + tableName + "Entity_Ext>();\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tDbConnection connection = base.GetConnection();\n";
            strQry += "\t\t\t\tDbCommand cmd = base.GetCommand(connection, \"" + tableName + "_GAPg_Ext\", CommandType.StoredProcedure);\n";
            strQry += "\t\t\t\t\ttry\n";
            strQry += "\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\tList<DbParameter> parameterList = FillParameters(entity, false);\n";
            strQry += "\t\t\t\t\t\tparameterList.Add(AddSortExpressionParameter(entity));\n";
            strQry += "\t\t\t\t\t\tparameterList.Add(AddPageSizeParameter(entity));\n";
            strQry += "\t\t\t\t\t\tparameterList.Add(AddCurrentPageParameter(entity));\n";
            strQry += "\t\t\t\t\t\tparameterList.Add(AddTotalRecordParameter(entity));\n";
            strQry += "\t\t\t\t\t\tif (parameterList != null && parameterList.Count > 0)\n";
            strQry += "\t\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\t\tcmd.Parameters.AddRange(parameterList.ToArray());\n";
            strQry += "\t\t\t\t\t\t}\n";

            strQry += "\t\t\t\t\t\tusing (DbDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))\n";
            strQry += "\t\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\t\tif (dataReader != null && dataReader.HasRows)\n";
            strQry += "\t\t\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\t\t\twhile (dataReader.Read())\n";
            strQry += "\t\t\t\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\t\t\t\t" + tableName + "Entity_Ext entityObj = new " + tableName + "Entity_Ext(dataReader);\n";
            strQry += "\t\t\t\t\t\t\t\t\tdalaList.Add(entityObj);\n";
            strQry += "\t\t\t\t\t\t\t\t}\n";
            strQry += "\t\t\t\t\t\t\t}\n";
            strQry += "\t\t\t\t\t\t\tdataReader.Close();\n";
            strQry += "\t\t\t\t\t\t}\n";
            strQry += "\t\t\t\t\t\tif (entity.CurrentPage > 0)\n";
            strQry += "\t\t\t\t\t\t\tentity.TotalRecord = Convert.ToInt64(cmd.Parameters[\"@TotalRecord\"].Value.ToString());\n";
            strQry += "\t\t\t\t\t}\n";
            strQry += "\t\t\t\t\tcatch (Exception ex)\n";
            strQry += "\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\tthrow ex;\n";
            strQry += "\t\t\t\t\t}\n";
            strQry += "\t\t\t\t\tfinally\n";
            strQry += "\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\tcmd.Dispose();\n";
            strQry += "\t\t\t\t\t\tconnection.Dispose();\n";
            strQry += "\t\t\t\t\t}\n";
            strQry += "\t\t\t\t}\n";
            strQry += "\t\t\t\tcatch (Exception ex)\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\tthrow ex;\n";
            strQry += "\t\t\t\t}\n";
            strQry += "\t\t\treturn dalaList;\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\t#endregion GetAll_Ext\n\n";
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
                    strQry += "long i" + tableName + ".SaveMasterDet" + refToTableName + "_Ext(" + tableName + "Entity masterEntity, IList<" + refToTableName + "Entity> listAdded, IList<" + refToTableName + "Entity> listUpdated, IList<" + refToTableName + "Entity> listDeleted)" + "\n";
                    strQry += GenerateMasterDetailsMethod(tableName, refToTableName, objclsColumnEntityList[0].ColumnName.Substring(0, 1).ToLower() + objclsColumnEntityList[0].ColumnName.Substring(1, objclsColumnEntityList[0].ColumnName.Length - 1)) + tab;
                }
            }
            return strQry;
        }

        public string GetFillParam(IList<clsColumnEntity> objclsColumnEntityList, string tableName, string tab)
        {
            string strQry = "";
            int x = 0;
            foreach (var item in objclsColumnEntityList)
            {
                if (item.IsMasterTable == true)
                {
                    x++;
                    if (item.ColumnName == "TS"
                   || item.ColumnName == "CreatedDate"
                   || item.ColumnName == "CreatedBy"
                   || item.ColumnName == "UpdatedDate"
                   || item.ColumnName == "UpdatedBy"
                   || item.ColumnName == "TagID"
                    )
                    {
                        continue;
                    }
                    if (item.ColumnDotNetType == "string")
                    {
                        if (x == 2)
                        {
                            strQry += "if (forDelete) return parameterList;" + tab; ;
                        }
                        strQry += "if (!(string.IsNullOrEmpty(entity." + item.ColumnName.Substring(0, 1).ToLower() + item.ColumnName.Substring(1, item.ColumnName.Length - 1) + "))) { parameterList.Add(GetParameter(\"@" + item.ColumnName + "\", entity." + item.ColumnName.Substring(0, 1).ToLower() + item.ColumnName.Substring(1, item.ColumnName.Length - 1) + ")); }" + tab;
                    }
                    else if (item.ColumnDotNetType == "bool")
                    {
                        if (x == 2)
                        {
                            strQry += "if (forDelete) return parameterList;" + tab; ;
                        }
                        strQry += "if (entity." + item.ColumnName.Substring(0, 1).ToLower() + item.ColumnName.Substring(1, item.ColumnName.Length - 1) + " != null) { parameterList.Add(GetParameter(\"@" + item.ColumnName + "\", entity." + item.ColumnName.Substring(0, 1).ToLower() + item.ColumnName.Substring(1, item.ColumnName.Length - 1) + ".ToString())); }" + tab;
                    }
                    else
                    {
                        if (x == 2)
                        {
                            strQry += "if (forDelete) return parameterList;" + tab; ;
                        }
                        strQry += "if (entity." + item.ColumnName.Substring(0, 1).ToLower() + item.ColumnName.Substring(1, item.ColumnName.Length - 1) + ".HasValue) { parameterList.Add(GetParameter(\"@" + item.ColumnName + "\", entity." + item.ColumnName.Substring(0, 1).ToLower() + item.ColumnName.Substring(1, item.ColumnName.Length - 1) + ".ToString())); }" + tab;
                    }

                }
            }
            return strQry;
        }

        public string GenerateMasterDetailsMethod(string tableName, string refToTableName, string pkName)
        {
            string strQry = "";

            strQry += "\t\t{\n";
            strQry += "\t\t\tlong returnCode = 0;\n";
            //strQry += "\t\t\tInt64 PrimaryKeyMaster = -99;\n";
           
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tthrow new NotImplementedException(\"Method { i"+ tableName + ".SaveMasterDet" + refToTableName + "_Ext } need to be implemented.\");\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch (Exception ex)\n";
            strQry += "\t\t\t{\n";
          
            strQry += "\t\t\t\tthrow ex;\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tfinally\n";
            strQry += "\t\t\t{\n";
        
            strQry += "\t\t\t}\n";
            strQry += "\t\t\treturn returnCode;\n";
            strQry += "\t\t}\n";

            return strQry;
        }

    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mega.CodeGen.WebAPI.netCore
{
    public class DAL

    {
        public bool DALEngineMethod(IList<clsTableRefEntity> objclsTableRefEntityList, IList<clsColumnEntity> objclsColumnEntityList, string nameSpace, string tableName, string path, string DataBaseName)
        {
            path = path + "\\DAL";
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
            strQry += "\tpublic partial class " + tableName + ": BaseDataAccess, i" + tableName + "\n";
            strQry += "\t{\n\n";
            strQry += "\t\t#region Constructors\n\n";
            strQry += "\t\tprivate string ClassName = \"" + tableName + "\";\n\n";
            strQry += "\t\tpublic " + tableName + "(AppCapsule appCapsule)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\tbase.appCapsule = appCapsule;\n";
            strQry += "\t\t}\n\n";
            strQry += "\t\tprivate string SourceOfException(string methodName)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\treturn \"Class name: \" + ClassName + \" and Method name: \" + methodName;\n";
            strQry += "\t\t}\n\n";
            strQry += "\t\t#endregion Constructors\n\n";

            strQry += "\t\tpublic List<DbParameter> FillParameters(" + tableName + "Entity entity, bool forDelete = false)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\tList<DbParameter> parameterList = new List<DbParameter>();\n";
            strQry += "\t\t\t" + GetFillParam(objclsColumnEntityList, tableName, "\n\t\t\t") + "";
            strQry += "return parameterList;\n";
            strQry += "\t\t}\n\n";


            strQry += "\t\t#region Save Update Delete List with Single Entity\n\n";

            strQry += "\t\tlong i" + tableName + ".Add(" + tableName + "Entity entity)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\tlong returnCode = 0;\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tconst string SP = \"" + tableName + "_Ins\";\n";
            strQry += "\t\t\t\tList<DbParameter> parameterList = FillParameters(entity, false);\n";
            strQry += "\t\t\t\tparameterList.Add(AddOutputParameter());\n";
            strQry += "\t\t\t\tFillSequrityParameters(parameterList, entity.BaseSecurityParam);\n";
            strQry += "\t\t\t\treturnCode = base.ExecuteNonQuery(entity, SP, parameterList, CommandType.StoredProcedure);\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch (Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tthrow ex;\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\treturn returnCode;\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\tlong i" + tableName + ".Update(" + tableName + "Entity entity)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\tlong returnCode = 0;\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tconst string SP = \"" + tableName + "_Upd\";\n";
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

            strQry += "\t\tlong i" + tableName + ".Delete(" + tableName + "Entity entity)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\tlong returnCode = 0;\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tconst string SP = \"" + tableName + "_Del\";\n";
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

            strQry += "\t\tlong i" + tableName + ".SaveList(IList<" + tableName + "Entity> listAdded, IList<" + tableName + "Entity> listUpdated, IList<" + tableName + "Entity> listDeleted)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\tlong returnCode = 0;\n";
            strQry += "\t\t\tconst string SPInsert = \"" + tableName + "_Ins\";\n";
            strQry += "\t\t\tconst string SPUpdate = \"" + tableName + "_Upd\";\n";
            strQry += "\t\t\tconst string SPDelete = \"" + tableName + "_Del\";\n";
            strQry += "\t\t\tSqlConnection connection = GetConnection();\n";
            strQry += "\t\t\tSqlTransaction transaction = connection.BeginTransaction();\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tif (listDeleted.Count > 0)\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\tforeach (" + tableName + "Entity entity in listDeleted)\n";
            strQry += "\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\tList<DbParameter> parameterList = FillParameters(entity, true);\n";
            strQry += "\t\t\t\t\t\tparameterList.Add(AddOutputParameter());\n";
            strQry += "\t\t\t\t\t\tFillSequrityParameters(parameterList, entity.BaseSecurityParam, false);\n";
            strQry += "\t\t\t\t\t\treturnCode = base.ExecuteNonQuery(entity, SPDelete, parameterList, connection, transaction, CommandType.StoredProcedure);\n";
            strQry += "\t\t\t\t\t\tif (returnCode < 0)\n";
            strQry += "\t\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\t\tthrow new ArgumentException(\"Error in transaction.\");\n";
            strQry += "\t\t\t\t\t\t}\n";
            strQry += "\t\t\t\t\t}\n";
            strQry += "\t\t\t\t}\n";
            strQry += "\t\t\t\tif (listUpdated.Count > 0)\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\tforeach (" + tableName + "Entity entity in listUpdated)\n";
            strQry += "\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\tList<DbParameter> parameterList = FillParameters(entity, false);\n";
            strQry += "\t\t\t\t\t\tparameterList.Add(AddOutputParameter());\n";
            strQry += "\t\t\t\t\t\tFillSequrityParameters(parameterList, entity.BaseSecurityParam, false);\n";
            strQry += "\t\t\t\t\t\treturnCode = base.ExecuteNonQuery(entity, SPUpdate, parameterList, connection, transaction, CommandType.StoredProcedure);\n";
            strQry += "\t\t\t\t\t\tif (returnCode < 0)\n";
            strQry += "\t\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\t\tthrow new ArgumentException(\"Error in transaction.\");\n";
            strQry += "\t\t\t\t\t\t}\n";
            strQry += "\t\t\t\t\t}\n";
            strQry += "\t\t\t\t}\n";
            strQry += "\t\t\t\tif (listAdded.Count > 0)\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\tforeach (" + tableName + "Entity entity in listAdded)\n";
            strQry += "\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\tList<DbParameter> parameterList = FillParameters(entity, false);\n";
            strQry += "\t\t\t\t\t\tparameterList.Add(AddOutputParameter());\n";
            strQry += "\t\t\t\t\t\tFillSequrityParameters(parameterList, entity.BaseSecurityParam);\n";
            strQry += "\t\t\t\t\t\treturnCode = base.ExecuteNonQuery(entity, SPInsert, parameterList, connection, transaction, CommandType.StoredProcedure);\n";
            strQry += "\t\t\t\t\t\tif (returnCode < 0)\n";
            strQry += "\t\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\t\tthrow new ArgumentException(\"Error in transaction.\");\n";
            strQry += "\t\t\t\t\t\t}\n";
            strQry += "\t\t\t\t\t}\n";
            strQry += "\t\t\t\t}\n";
            strQry += "\t\t\t\ttransaction.Commit();\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch (Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\ttransaction.Rollback();\n";
            strQry += "\t\t\t\tthrow ex;\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tfinally\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\ttransaction.Dispose();;\n";
            strQry += "\t\t\t\tconnection.Close();\n";
            strQry += "\t\t\t\tconnection.Dispose();\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\treturn returnCode;\n";
            strQry += "\t\t}\n\n";
           
            strQry += "\t\tpublic long SaveList(SqlConnection connection, SqlTransaction transaction, IList<" + tableName + "Entity> listAdded, IList<" + tableName + "Entity> listUpdated, IList<" + tableName + "Entity> listDeleted)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\tlong returnCode = 0;\n";
            strQry += "\t\t\tconst string SPInsert = \"" + tableName + "_Ins\";\n";
            strQry += "\t\t\tconst string SPUpdate = \"" + tableName + "_Upd\";\n";
            strQry += "\t\t\tconst string SPDelete = \"" + tableName + "_Del\";\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tif (listDeleted.Count > 0)\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\tforeach (" + tableName + "Entity entity in listDeleted)\n";
            strQry += "\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\tList<DbParameter> parameterList = FillParameters(entity, true);\n";
            strQry += "\t\t\t\t\t\tparameterList.Add(AddOutputParameter());\n";
            strQry += "\t\t\t\t\t\tFillSequrityParameters(parameterList, entity.BaseSecurityParam, false);\n";
            strQry += "\t\t\t\t\t\treturnCode = base.ExecuteNonQuery(entity, SPDelete, parameterList, connection, transaction, CommandType.StoredProcedure);\n";
            strQry += "\t\t\t\t\t\tif (returnCode < 0)\n";
            strQry += "\t\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\t\tthrow new ArgumentException(\"Error in transaction.\");\n";
            strQry += "\t\t\t\t\t\t}\n";
            strQry += "\t\t\t\t\t}\n";
            strQry += "\t\t\t\t}\n";
            strQry += "\t\t\t\tif (listUpdated.Count > 0)\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\tforeach (" + tableName + "Entity entity in listUpdated)\n";
            strQry += "\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\tList<DbParameter> parameterList = FillParameters(entity, false);\n";
            strQry += "\t\t\t\t\t\tparameterList.Add(AddOutputParameter());\n";
            strQry += "\t\t\t\t\t\tFillSequrityParameters(parameterList, entity.BaseSecurityParam, false);\n";
            strQry += "\t\t\t\t\t\treturnCode = base.ExecuteNonQuery(entity, SPUpdate, parameterList, connection, transaction, CommandType.StoredProcedure);\n";
            strQry += "\t\t\t\t\t\tif (returnCode < 0)\n";
            strQry += "\t\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\t\tthrow new ArgumentException(\"Error in transaction.\");\n";
            strQry += "\t\t\t\t\t\t}\n";
            strQry += "\t\t\t\t\t}\n";
            strQry += "\t\t\t\t}\n";
            strQry += "\t\t\t\tif (listAdded.Count > 0)\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\tforeach (" + tableName + "Entity entity in listAdded)\n";
            strQry += "\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\tList<DbParameter> parameterList = FillParameters(entity, false);\n";
            strQry += "\t\t\t\t\t\tparameterList.Add(AddOutputParameter());\n";
            strQry += "\t\t\t\t\t\tFillSequrityParameters(parameterList, entity.BaseSecurityParam);\n";
            strQry += "\t\t\t\t\t\treturnCode = base.ExecuteNonQuery(entity, SPInsert, parameterList, connection, transaction, CommandType.StoredProcedure);\n";
            strQry += "\t\t\t\t\t\tif (returnCode < 0)\n";
            strQry += "\t\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\t\tthrow new ArgumentException(\"Error in transaction.\");\n";
            strQry += "\t\t\t\t\t\t}\n";
            strQry += "\t\t\t\t\t}\n";
            strQry += "\t\t\t\t}\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch (Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tthrow ex;\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\treturn returnCode;\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\t#endregion Save Update Delete List\n\n";
            strQry += "\t\t#region GetAll\n\n";
            strQry += "\t\tIList<" + tableName + "Entity> i" + tableName + ".GetAll(" + tableName + "Entity entity)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\tList<" + tableName + "Entity> dalaList = new List<" + tableName + "Entity>();\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tList<DbParameter> parameterList = FillParameters(entity, false);\n";
            strQry += "\t\t\t\tparameterList.Add(AddSortExpressionParameter(entity));\n";
            strQry += "\t\t\t\tusing (DbDataReader dataReader = base.GetDataReader(entity, \"" + tableName + "_GA\", parameterList, CommandType.StoredProcedure))\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\tif (dataReader != null && dataReader.HasRows)\n";
            strQry += "\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\twhile (dataReader.Read())\n";
            strQry += "\t\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\t\t" + tableName + "Entity entityObj = new " + tableName + "Entity(dataReader);\n";
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

            strQry += "\t\tIList<" + tableName + "Entity> i" + tableName + ".GetAllByPages(" + tableName + "Entity entity)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\tList<" + tableName + "Entity> dalaList = new List<" + tableName + "Entity>();\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tDbConnection connection = base.GetConnection();\n";
            strQry += "\t\t\t\tDbCommand cmd = base.GetCommand(connection, \"" + tableName + "_GAPg\", CommandType.StoredProcedure);\n";
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
            strQry += "\t\t\t\t\t\t\t\t\t" + tableName + "Entity entityObj = new " + tableName + "Entity(dataReader);\n";
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

            strQry += "\t\t#endregion GetAll\n\n";
            strQry += "\t\t#region SaveMasterDetails\n\n";
            strQry += "\t\t" + GetMasterDetailsMethods(objclsColumnEntityList, tableName, "\n\n\t\t");


            if (objclsColumnEntityList[0].RefarenceToTable != null)
            {
                strQry += "long i" + tableName + ".SaveMasterDetails(" + tableName + "Entity masterEntity,\n";
                strQry += "\t\t\t\t\t\t\t" + GetMasterDetailsMethodsParams(objclsColumnEntityList, tableName, "\n\t\t\t\t\t\t\t");
                strQry = strQry.Substring(0, strQry.Length - 9);
                strQry += "\n\t\t\t\t\t\t\t)\n";
                strQry += "\t\t{\n";
                strQry += "\t\t\tlong returnCode = 0;\n";
                strQry += "\t\t\tstring SP = \"\";\n";
                strQry += "\t\t\tconst string MasterSPInsert = \"" + tableName + "_Ins\";\n";
                strQry += "\t\t\tconst string MasterSPUpdate = \"" + tableName + "_Upd\";\n";
                strQry += "\t\t\tconst string MasterSPDelete = \"" + tableName + "_Del\";\n";
                strQry += "\t\t\tif (masterEntity.CurrentState == BaseEntity.EntityState.Added)\n";
                strQry += "\t\t\t\tSP = MasterSPInsert;\n";
                strQry += "\t\t\telse if (masterEntity.CurrentState == BaseEntity.EntityState.Changed)\n";
                strQry += "\t\t\t\tSP = MasterSPUpdate;\n";
                strQry += "\t\t\telse if (masterEntity.CurrentState == BaseEntity.EntityState.Deleted)\n";
                strQry += "\t\t\t\tSP = MasterSPDelete;\n";
                strQry += "\t\t\telse\n";
                strQry += "\t\t\t{\n";
                strQry += "\t\t\t\tthrow new Exception(\"Nothing to save.\");\n";
                strQry += "\t\t\t}\n";
                strQry += "\t\t\tSqlConnection connection = GetConnection();\n";
                strQry += "\t\t\tSqlTransaction transaction = connection.BeginTransaction();\n";
                strQry += "\t\t\ttry\n";
                strQry += "\t\t\t{\n";
                strQry += "\t\t\t\tList<DbParameter> parameterList = new List<DbParameter>();\n";
                strQry += "\t\t\t\tif (masterEntity.CurrentState == BaseEntity.EntityState.Added || masterEntity.CurrentState == BaseEntity.EntityState.Changed)\n";
                strQry += "\t\t\t\t{\n";
                strQry += "\t\t\t\t\tparameterList = FillParameters(masterEntity, false);\n";
                strQry += "\t\t\t\t}\n";
                strQry += "\t\t\t\telse\n";
                strQry += "\t\t\t\t{\n";
                strQry += "\t\t\t\t\tparameterList = FillParameters(masterEntity, true);\n";
                strQry += "\t\t\t\t}\n";
                strQry += "\t\t\t\tparameterList.Add(AddOutputParameter());\n";
                strQry += "\t\t\t\tif (masterEntity.CurrentState == BaseEntity.EntityState.Added) { FillSequrityParameters(parameterList, masterEntity.BaseSecurityParam); }\n";
                strQry += "\t\t\t\telse { FillSequrityParameters(parameterList, masterEntity.BaseSecurityParam, false); }\n";
                strQry += "\t\t\t\tif (masterEntity.CurrentState != BaseEntity.EntityState.Deleted)\n";
                strQry += "\t\t\t\t{\n";
                strQry += "\t\t\t\t\tif (returnCode < 0)\n";
                strQry += "\t\t\t\t\t{\n";
                strQry += "\t\t\t\t\treturnCode = base.ExecuteNonQuery(masterEntity, SP, parameterList, connection, transaction, CommandType.StoredProcedure);\n";
                strQry += "\t\t\t\t\t\tthrow new ArgumentException(\"Error in transaction.\");\n";
                strQry += "\t\t\t\t\t}\n";
                strQry += "\t\t\t\t}\n";
                strQry += "\t\t\t\telse\n";
                strQry += "\t\t\t\t{\n";
                strQry += "\t\t\t\t\treturnCode = 1;\n";
                strQry += "\t\t\t\t}\n";
                strQry += "\t\t\t\tif (returnCode > 0)\n";
                strQry += "\t\t\t\t{\n";
                strQry += "\t\t\t\t\tif (masterEntity.CurrentState != BaseEntity.EntityState.Deleted)\n";
                strQry += "\t\t\t\t\t{\n";
                strQry += GetMasterDetailsForeach(objclsColumnEntityList, tableName, "");
                strQry += "\t\t\t\t\t}\n";
                strQry += "\t\t\t\t\t" + GetMasterDetailsCallMethods(objclsColumnEntityList, tableName, "\n\t\t\t\t\t");
                strQry = strQry.Substring(0, strQry.Length - 1);
                strQry += "}\n";
                strQry += "\t\t\t\tif(masterEntity.CurrentState == BaseEntity.EntityState.Deleted)\n";
                strQry += "\t\t\t\t\treturnCode = base.ExecuteNonQuery(masterEntity, MasterSPDelete, parameterList, connection, transaction, CommandType.StoredProcedure);\n";
                strQry += "\t\t\t\ttransaction.Commit();\n";
                strQry += "\t\t\t}\n";
                strQry += "\t\t\tcatch (Exception ex)\n";
                strQry += "\t\t\t{\n";
                strQry += "\t\t\t\ttransaction.Rollback();\n";
                strQry += "\t\t\t\tthrow ex;\n";
                strQry += "\t\t\t}\n";
                strQry += "\t\t\tfinally\n";
                strQry += "\t\t\t{\n";
                strQry += "\t\t\t\ttransaction.Dispose();\n";
                strQry += "\t\t\t\tconnection.Close();\n";
                strQry += "\t\t\t\tconnection.Dispose();\n";
                strQry += "\t\t\t}\n";
                strQry += "\t\t\treturn returnCode;\n";
                strQry += "\t\t}\n";
            }
            strQry += "\n\t\t#endregion SaveMasterDetails\n\n";


            strQry += "\t}\n";

            strQry += "}\n";

            return strQry;
        }

        public string GetMasterDetailsForeach(IList<clsColumnEntity> objclsColumnEntityList, string tableName, string tab)
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
                    strQry += "\t\t\t\t\t\tforeach (var item in "+ refToTableName + "listAdded)\n";
                    strQry += "\t\t\t\t\t\t{\n";
                    strQry += "\t\t\t\t\t\t\titem." + objclsColumnEntityList[0].ColumnName.Substring(0, 1).ToLower() + objclsColumnEntityList[0].ColumnName.Substring(1, objclsColumnEntityList[0].ColumnName.Length - 1) + " = masterEntity.ReturnKey;\n";
                    strQry += "\t\t\t\t\t\t}\n";
                }
            }
            return strQry;
        }

        public string GetMasterDetailsCallMethods(IList<clsColumnEntity> objclsColumnEntityList, string tableName, string tab)
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
                    strQry += "" + refToTableName + " obj" + refToTableName + "DAL = new " + refToTableName + "(this.appCapsule);" + tab;
                    strQry += "obj" + refToTableName + "DAL.SaveList(connection, transaction, " + refToTableName + "listAdded, " + refToTableName + "listUpdated, " + refToTableName + "listDeleted);" + tab;
                }
            }
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
                    strQry += "IList<" + refToTableName + "Entity> " + refToTableName + "listAdded," + tab;
                    strQry += "IList<" + refToTableName + "Entity> " + refToTableName + "listUpdated," + tab;
                    strQry += "IList<" + refToTableName + "Entity> " + refToTableName + "listDeleted," + tab;
                }
            }
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
                    strQry += "long i" + tableName + ".SaveMasterDet" + refToTableName + "(" + tableName + "Entity masterEntity, IList<" + refToTableName + "Entity> listAdded, IList<" + refToTableName + "Entity> listUpdated, IList<" + refToTableName + "Entity> listDeleted)" + "\n";
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
                    else if (item.ColumnDotNetType == "bool" || item.ColumnDotNetType == "byte[]" )
                    {
                        if (x == 2)
                        {
                            strQry += "if (forDelete) return parameterList;" + tab; ;
                        }
                        strQry += "if (entity." + item.ColumnName.Substring(0, 1).ToLower() + item.ColumnName.Substring(1, item.ColumnName.Length - 1) + " != null) { parameterList.Add(GetParameter(\"@" + item.ColumnName + "\", entity." + item.ColumnName.Substring(0, 1).ToLower() + item.ColumnName.Substring(1, item.ColumnName.Length - 1) + ")); }" + tab;
                    }
                    else
                    {
                        if (x == 2)
                        {
                            strQry += "if (forDelete) return parameterList;" + tab; ;
                        }
                        strQry += "if (entity." + item.ColumnName.Substring(0, 1).ToLower() + item.ColumnName.Substring(1, item.ColumnName.Length - 1) + ".HasValue) { parameterList.Add(GetParameter(\"@" + item.ColumnName + "\", entity." + item.ColumnName.Substring(0, 1).ToLower() + item.ColumnName.Substring(1, item.ColumnName.Length - 1) + ")); }" + tab;
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
            strQry += "\t\t\tstring SP = \"\";\n";
            strQry += "\t\t\tconst string MasterSPInsert = \""+ tableName + "_Ins\";\n";
            strQry += "\t\t\tconst string MasterSPUpdate = \"" + tableName + "_Upd\";\n";
            strQry += "\t\t\tconst string MasterSPDelete = \"" + tableName + "_Del\";\n";
            strQry += "\t\t\tif (masterEntity.CurrentState == BaseEntity.EntityState.Added)\n";
            strQry += "\t\t\t\tSP = MasterSPInsert;\n";
            strQry += "\t\t\telse if (masterEntity.CurrentState == BaseEntity.EntityState.Changed)\n";
            strQry += "\t\t\t\tSP = MasterSPUpdate;\n";
            strQry += "\t\t\telse if (masterEntity.CurrentState == BaseEntity.EntityState.Deleted)\n";
            strQry += "\t\t\t\tSP = MasterSPDelete;\n";
            strQry += "\t\t\telse\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tthrow new Exception(\"Nothing to save.\");\n";
            strQry += "\t\t\t}\n";

            strQry += "\t\t\tSqlConnection connection = GetConnection();\n";
            strQry += "\t\t\tSqlTransaction transaction = connection.BeginTransaction();\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tList<DbParameter> parameterList = new List<DbParameter>();\n";
            strQry += "\t\t\t\tif (masterEntity.CurrentState == BaseEntity.EntityState.Added || masterEntity.CurrentState == BaseEntity.EntityState.Changed)\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\tparameterList = FillParameters(masterEntity, false);\n";
            strQry += "\t\t\t\t}\n";
            strQry += "\t\t\t\telse\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\tparameterList = FillParameters(masterEntity, true);\n";
            strQry += "\t\t\t\t}\n";

            strQry += "\t\t\t\tparameterList.Add(AddOutputParameter());\n";
            strQry += "\t\t\t\tif (masterEntity.CurrentState == BaseEntity.EntityState.Added) { FillSequrityParameters(parameterList, masterEntity.BaseSecurityParam); }\n";
            strQry += "\t\t\t\telse { FillSequrityParameters(parameterList, masterEntity.BaseSecurityParam, false); }\n";
            
            strQry += "\t\t\t\tif (masterEntity.CurrentState != BaseEntity.EntityState.Deleted)\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\treturnCode = base.ExecuteNonQuery(masterEntity, SP, parameterList, connection, transaction, CommandType.StoredProcedure);\n";
            strQry += "\t\t\t\t\tif (returnCode < 0)\n";
            strQry += "\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\tthrow new ArgumentException(\"Error in transaction.\");\n";
            strQry += "\t\t\t\t\t}\n";
            strQry += "\t\t\t\t}\n";
            strQry += "\t\t\t\telse\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\treturnCode = 1;\n";
            strQry += "\t\t\t\t}\n";
            strQry += "\t\t\t\tif (returnCode > 0)\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\tif (masterEntity.CurrentState != BaseEntity.EntityState.Deleted)\n";
            strQry += "\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\tforeach (var item in listAdded)\n";
            strQry += "\t\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\t\titem."+ pkName + " = masterEntity.ReturnKey;\n";
            strQry += "\t\t\t\t\t\t}\n";
            strQry += "\t\t\t\t\t}\n";
            strQry += "\t\t\t\t\t" + refToTableName + " objDAL = new " + refToTableName + "(this.appCapsule);\n";
            strQry += "\t\t\t\t\tobjDAL.SaveList(connection, transaction, listAdded, listUpdated, listDeleted);\n";
            strQry += "\t\t\t\t}\n";
            strQry += "\t\t\t\tif(masterEntity.CurrentState == BaseEntity.EntityState.Deleted)\n";
            strQry += "\t\t\t\t\treturnCode = base.ExecuteNonQuery(masterEntity, MasterSPDelete, parameterList, connection, transaction, CommandType.StoredProcedure);\n";

            strQry += "\t\t\t\ttransaction.Commit();\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch (Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\ttransaction.Rollback();\n";
            strQry += "\t\t\t\tthrow ex;\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tfinally\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\ttransaction.Dispose();\n";
            strQry += "\t\t\t\tconnection.Close();\n";
            strQry += "\t\t\t\tconnection.Dispose();\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\treturn returnCode;\n";
            strQry += "\t\t}\n";

            return strQry;
        }

    }
}
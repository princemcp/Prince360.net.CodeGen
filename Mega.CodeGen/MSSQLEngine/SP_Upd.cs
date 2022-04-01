using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mega.CodeGen.MSSQLEngine
{
    public class SP_Upd
    {
        const string spNameSurfix = "_Upd";
        int colval = 0;
        public bool GetSPEngineMethod(IList<clsTableRefEntity> objclsTableRefEntityList, IList<clsColumnEntity> objclsColumnEntityList, string nameSpace, string tableName, string path, string DataBaseName)
        {
            path = path + "\\MSSQL.SQLScript";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\" + tableName + spNameSurfix + ".sql";

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
            strQry += "\nUSE [" + DataBaseName + "]\n";
            strQry += "GO\n\n";

            //strQry += "\nIF EXISTS (SELECT * FROM [dbo].[sysobjects]\n";
            //strQry += "\t\tWHERE ID = object_id(N'[dbo].[" + tableName + "_GA]')\n";
            //strQry += "\t\tAND OBJECTPROPERTY(id, N'IsProcedure') = 1)\n";
            //strQry += "\tDROP PROCEDURE [dbo].[spGet" + tableName + "]\n";
            //strQry += "GO\n\n";
            strQry += "CREATE PROCEDURE " + tableName + spNameSurfix + "\n\n";
            strQry += "--\t******************************************************\n";
            strQry += "--\t   Author : SM Habib Ullah -- Prince\n";
            strQry += "--\t   Web    : https://www.Prince360.net\n";
            strQry += "--\t   Date   :\t" + DateTime.Now.ToString("dd-MM-yyyy") + "\n";
            strQry += "--\t   Table  :\t" + tableName + "\n";
            strQry += "--\t   SP Name:\t" + tableName + spNameSurfix + "\n";
            strQry += "--\t******************************************************\n\n\n";


            strQry += GetSPParam(objclsColumnEntityList, tableName, "\t", false);

            strQry += "\t@RETURN_KEY       bigint = null output,\n";

            strQry = strQry.Substring(0, strQry.Length - 2) + "\n\nAS\n\nBegin\n";

            strQry += "\n\tUPDATE " + tableName + " \n\t";
            strQry += "\tSET \n\t\t\t";
            strQry += GetUpdateColumnList(objclsColumnEntityList, tableName, "\t\t\t");
            strQry = strQry.Substring(0, strQry.Length - 3) + "\n";

            strQry += "\t\tWhere\n";

            strQry += "\t\t\t" + GetWhereClause(objclsColumnEntityList, tableName, "");
            strQry = strQry.Substring(0, strQry.Length - 2) + "\n";
            strQry += "\t\tSET @RETURN_KEY = @" + objclsColumnEntityList[colval].ColumnName + "\n";


            strQry += "\nEND\n\n\n";



            return strQry;
        }

        private string GetSPParam(IList<clsColumnEntity> objclsColumnEntityList, string tableName, string tab, bool collection)
        {
            string strQry = "";
            if (collection == false)
            {
                for (int i = 0; i < objclsColumnEntityList.Count; i++)
                {
                    
                    if (objclsColumnEntityList[i].ColumnDBType != "image")
                    {
                        if (objclsColumnEntityList[i].IsMasterTable == true)
                        {
                            if (objclsColumnEntityList[i].ColumnName.Contains("CreatedDate")
                            || objclsColumnEntityList[i].ColumnName.Contains("CreatedBy"))
                            {
                                continue;
                            }

                            if (objclsColumnEntityList[i].ColumnDBType == "timestamp")
                            {
                                objclsColumnEntityList[i].ColumnDBType = "bigint";
                                colval = i;
                            }
                            strQry += tab + "@" + objclsColumnEntityList[i].ColumnName.Trim() + tab + tab + tab + objclsColumnEntityList[i].ColumnDBType.Replace("identity", "") + " ";
                        }
                        else
                        {
                            // strQry += tab + "@" + objclsColumnEntityList[i].ColumnAliasName.Trim() + tab + tab + tab + objclsColumnEntityList[i].ColumnDBType.Replace("identity", "") + " ";
                        }
                        if (objclsColumnEntityList[i].IsMasterTable == true)
                        {
                            
                            if ((objclsColumnEntityList[i].ColumnDBType == "text")
                            || (objclsColumnEntityList[i].ColumnDBType == "nchar")
                            || (objclsColumnEntityList[i].ColumnDBType == "nvarchar")
                            || (objclsColumnEntityList[i].ColumnDBType == "ntext")
                            || (objclsColumnEntityList[i].ColumnDBType == "varchar")
                            || (objclsColumnEntityList[i].ColumnDBType == "decimal")
                            || (objclsColumnEntityList[i].ColumnDBType == "char"))
                            {
                                if (int.Parse(objclsColumnEntityList[i].Lenght.Trim()) > 8000)
                                {
                                    strQry += " = null";
                                }
                                else if (objclsColumnEntityList[i].ColumnDBType == "decimal")
                                {
                                    strQry += "(" + objclsColumnEntityList[i].Precsion.Trim() + "," + objclsColumnEntityList[i].Scale.Trim() + ")" + " = null";
                                }
                                else if (objclsColumnEntityList[i].ColumnDBType == "nvarchar")
                                {
                                    strQry += "(" + objclsColumnEntityList[i].Precsion.Trim() + ")" + " = null";
                                }
                                else
                                {
                                    strQry += "(" + objclsColumnEntityList[i].Lenght.Trim() + ")" + " = null";
                                }
                            }
                            else
                            {
                                strQry += " = null";
                            }

                            strQry += ",\n";
                        }
                    }
                }
                objclsColumnEntityList[colval].ColumnDBType = "timestamp";
                
            }
            else
            {
                strQry += "\n\n" + tab + "-- For search with collection - - - \n";
                for (int i = 0; i < objclsColumnEntityList.Count; i++)
                {
                    if (objclsColumnEntityList[i].SearchSPParam == true)
                    {
                        if (objclsColumnEntityList[i].ColumnDBType != "image")
                        {
                            if (objclsColumnEntityList[i].IsMasterTable == true)
                            {
                                strQry += tab + "@" + objclsColumnEntityList[i].ColumnName.Trim() + "Collection" + tab + tab + tab + " ";
                            }
                            else
                            {
                                strQry += tab + "@" + objclsColumnEntityList[i].ColumnAliasName.Trim() + "Collection" + tab + tab + tab + " ";
                            }
                            strQry += "varchar (1000) = null";
                            strQry += ",\n";
                        }
                    }
                }
            }
            return strQry;
        }

        private string GetUpdateColumnList(IList<clsColumnEntity> objclsColumnEntityList, string tableName, string tab)
        {
            string strQry = "";
            for (int i = 0; i < objclsColumnEntityList.Count; i++)
            {
                if (objclsColumnEntityList[i].ColumnDBType.Contains("identity") || objclsColumnEntityList[i].ColumnDBType.Contains("timestamp"))
                {
                    continue;
                }
                if (objclsColumnEntityList[i].ColumnName.Contains("CreatedDate")
                || objclsColumnEntityList[i].ColumnName.Contains("CreatedBy"))
                {
                    continue;
                }
                if (objclsColumnEntityList[i].IsMasterTable == true)
                {
                    strQry += "" + objclsColumnEntityList[i].TableName.Trim() + "." + objclsColumnEntityList[i].ColumnName.Trim() + " = @" + objclsColumnEntityList[i].ColumnName.Trim() + ",\n" + tab + "";
                }
                else
                {
                    // strQry += objclsColumnEntityList[i].TableAliasName.Trim() + "." + objclsColumnEntityList[i].ColumnName.Trim() + " AS " + objclsColumnEntityList[i].ColumnAliasName.Trim() + ",\n" + tab + "";
                }
            }
            strQry = strQry.Substring(0, strQry.Length - 2);
            return strQry;
        }

        private string GetInsertColumnParamList(IList<clsColumnEntity> objclsColumnEntityList, string tableName, string tab)
        {
            string strQry = "";
            for (int i = 0; i < objclsColumnEntityList.Count; i++)
            {
                if (objclsColumnEntityList[i].ColumnDBType.Contains("identity") || objclsColumnEntityList[i].ColumnDBType.Contains("timestamp"))
                {
                    continue;
                }
                if (objclsColumnEntityList[i].IsMasterTable == true)
                {
                    strQry += "@" + objclsColumnEntityList[i].ColumnName.Trim() + ",\n" + tab + "";
                }
                else
                {
                    // strQry += objclsColumnEntityList[i].TableAliasName.Trim() + "." + objclsColumnEntityList[i].ColumnName.Trim() + " AS " + objclsColumnEntityList[i].ColumnAliasName.Trim() + ",\n" + tab + "";
                }
            }
            strQry = strQry.Substring(0, strQry.Length - 2);
            return strQry;
        }

        private string GetOrderByClause(IList<clsColumnEntity> objclsColumnEntityList, string tableName, string tab)
        {
            string strQry = "";
            for (int i = 0; i < objclsColumnEntityList.Count; i++)
            {
                if (objclsColumnEntityList[i].SPOrderBy == true)
                {
                    if (objclsColumnEntityList[i].ColumnDBType != "image")
                    {
                        //if (i == 0)
                        //{
                        //    strQry += tab + "CASE WHEN @OrderBy = '" + objclsColumnEntityList[i].ColumnName.Trim() + "' AND @OrderByDirection = 'DESC' THEN " + objclsColumnEntityList[i].TableName + "." + objclsColumnEntityList[i].ColumnName.Trim() + " END DESC,\n";
                        //    strQry += tab + "CASE WHEN @OrderBy = '" + objclsColumnEntityList[i].ColumnName.Trim() + "' AND @OrderByDirection = 'ASC' THEN " + objclsColumnEntityList[i].TableName + "." + objclsColumnEntityList[i].ColumnName.Trim() + " END ASC ,\n";
                        //    strQry = strQry.Substring(0, strQry.Length - 2);
                        //    strQry += "\n";
                        //}
                        //else
                        //{
                        if (objclsColumnEntityList[i].IsMasterTable == true)
                        {
                            strQry += tab + "CASE WHEN @SortExpression = '" + objclsColumnEntityList[i].ColumnName.Trim() + " DESC' THEN " + objclsColumnEntityList[i].TableName + "." + objclsColumnEntityList[i].ColumnName.Trim() + " END DESC,\n";
                            strQry += tab + "CASE WHEN @SortExpression = '" + objclsColumnEntityList[i].ColumnName.Trim() + " ASC' THEN " + objclsColumnEntityList[i].TableName + "." + objclsColumnEntityList[i].ColumnName.Trim() + " END ASC ,\n";
                        }
                        else
                        {
                            strQry += tab + "CASE WHEN @SortExpression = '" + objclsColumnEntityList[i].ColumnAliasName.Trim() + " DESC' THEN " + objclsColumnEntityList[i].TableAliasName + "." + objclsColumnEntityList[i].ColumnName.Trim() + " END DESC,\n";
                            strQry += tab + "CASE WHEN @SortExpression = '" + objclsColumnEntityList[i].ColumnAliasName.Trim() + " ASC' THEN " + objclsColumnEntityList[i].TableAliasName + "." + objclsColumnEntityList[i].ColumnName.Trim() + " END ASC ,\n";
                        }
                        //}
                    }
                }
            }
            strQry = strQry.Substring(0, strQry.Length - 2);
            return strQry;
        }

        private string TableJoinString(IList<clsTableRefEntity> objclsTableRefEntityList, string tabs, string tableName)
        {
            string str = "";

            if (objclsTableRefEntityList != null && objclsTableRefEntityList.Count > 0)
            {
                for (int i = 0; i < objclsTableRefEntityList.Count; i++)
                {
                    if (objclsTableRefEntityList[i].RefIsNull == false)
                    {
                        str += tabs + "INNER JOIN dbo." + objclsTableRefEntityList[i].TableName + " AS " + objclsTableRefEntityList[i].TableAliasName + " ON " + "" + tableName + "." + objclsTableRefEntityList[i].ThisColumnName + " = ";
                    }
                    else
                    {
                        str += tabs + "LEFT JOIN dbo." + objclsTableRefEntityList[i].TableName + " AS " + objclsTableRefEntityList[i].TableAliasName + " ON " + "" + tableName + "." + objclsTableRefEntityList[i].ThisColumnName + " = ";
                    }
                    str += objclsTableRefEntityList[i].TableAliasName + "." + objclsTableRefEntityList[i].RefColumnName + "\n";
                }
            }
            return str;
        }

        private string GetWhereClause(IList<clsColumnEntity> objclsColumnEntityList, string tableName, string tab)
        {
            string strQry = "";
            int RowNumber = 0;
            for (int i = 0; i < objclsColumnEntityList.Count; i++)
            {
                if (objclsColumnEntityList[i].ColumnDBType.Contains("identity") )
                {
                    colval = i;
                    strQry += "" + objclsColumnEntityList[i].TableName.Trim() + "." + objclsColumnEntityList[i].ColumnName.Trim() + " = @" + objclsColumnEntityList[i].ColumnName.Trim() + ",\n" + tab + "";
                    break;
                }
            }


            return strQry;
        }

        private string GetWhereClauseSubString(clsColumnEntity objclsColumnEntity, string tableName)
        {
            string DBType = objclsColumnEntity.ColumnDBType;
            string strQry = "";

            if (objclsColumnEntity.ColumnIsNull == false)
            {
                if (DBType == "datetime")
                {
                    strQry = " = '1/1/1754 12:00:00 AM'";
                }
                else if (DBType == "datetime2")
                {
                    strQry = " = '1/1/0001 12:00:00 AM'";
                }
                else if (DBType == "date")
                {
                    strQry = " = '1/1/0001'";
                }
                else if (DBType == "varchar")
                {
                    strQry = "is Null";
                }
                else
                {
                    strQry = "= 0";
                }
            }
            else if (objclsColumnEntity.ColumnIsNull == true)
            {
                strQry = "is Null";
            }



            //if (objclsColumnEntity.ColumnIsNull == false && DBType != "datetime")
            //{
            //    if (objclsColumnEntity.IsMasterTable == true)
            //    {
            //        if (DBType == "varchar")
            //        {
            //            strQry = "is Null";
            //        }
            //        else
            //        {
            //            strQry = " = 0";
            //        }
            //    }
            //    else
            //    {
            //        if (DBType == "varchar")
            //        {
            //            strQry = " = 0";
            //        }
            //        else
            //        {
            //            strQry = "is Null";
            //        }
            //    }
            //}
            //else if (objclsColumnEntity.ColumnIsNull == false && DBType == "datetime")
            //{
            //    if (objclsColumnEntity.IsMasterTable == true)
            //    {
            //        strQry = " = '1/1/1754 12:00:00 AM'";
            //    }
            //    else
            //    {
            //        strQry = " = '1/1/1754 12:00:00 AM'";
            //    }
            //}
            //else if (objclsColumnEntity.ColumnIsNull == true)
            //{
            //    strQry = "is Null";
            //}
            return strQry;
        }

        private string GetWhereCollectionClause(IList<clsColumnEntity> objclsColumnEntityList, string tableName, string tab)
        {
            int RowNumber = 0;
            string strQry = "";
            for (int i = 0; i < objclsColumnEntityList.Count; i++)
            {
                if (objclsColumnEntityList[i].SearchSPParam == true)
                {
                    if (objclsColumnEntityList[i].ColumnDBType != "image")
                    {
                        string And = "and ";
                        if (RowNumber == 0)
                        {
                            And = "";
                        }
                        if (objclsColumnEntityList[i].IsMasterTable == true)
                        {
                            strQry += "" + tab + "\t" + And + "CASE WHEN @" + objclsColumnEntityList[i].ColumnName + "Collection Is Null THEN 1 WHEN " + "dbo." + objclsColumnEntityList[i].TableName + "." + objclsColumnEntityList[i].ColumnName + " in  (select * from Split(@" + objclsColumnEntityList[i].ColumnName + "Collection, ',')) THEN 1 ELSE 0 END = 1" + "\n";
                        }
                        else
                        {
                            strQry += "" + tab + "\t" + And + "CASE WHEN @" + objclsColumnEntityList[i].ColumnAliasName + "Collection Is Null THEN 1 WHEN " + objclsColumnEntityList[i].TableAliasName + "." + objclsColumnEntityList[i].ColumnName + " in  (select * from Split(@" + objclsColumnEntityList[i].ColumnAliasName + "Collection, ',')) THEN 1 ELSE 0 END = 1" + "\n";
                        }
                        RowNumber++;
                    }
                }
            }
            return strQry;
        }
    }
}


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mega.CodeGen.MSSQLEngine
{
    public class GetSPEngine
    {
        public bool GetSPEngineMethod(IList<clsTableRefEntity> objclsTableRefEntityList, IList<clsColumnEntity> objclsColumnEntityList, string nameSpace, string tableName, string path, string DataBaseName)
        {
            path = path + "\\MSSQL.SQLScript";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\spGet" + tableName + ".sql";

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

            strQry += "\nIF EXISTS (SELECT * FROM [dbo].[sysobjects]\n";
            strQry += "\t\tWHERE ID = object_id(N'[dbo].[spGet" + tableName + "]')\n";
            strQry += "\t\tAND OBJECTPROPERTY(id, N'IsProcedure') = 1)\n";
            strQry += "\tDROP PROCEDURE [dbo].[spGet" + tableName + "]\n";
            strQry += "GO\n\n";
            strQry += "CREATE PROCEDURE spGet" + tableName + "\n\n";
            strQry += "--\t******************************************************\n";
            strQry += "--\t   Author : SM Habib Ullah -- Prince\n";
            strQry += "--\t   Web    : https://www.Prince360.net\n";
            strQry += "--\t   Date   :\t" + DateTime.Now.ToString("dd-MM-yyyy") + "\n";
            strQry += "--\t   Table  :\t" + tableName + "\n";
            strQry += "--\t   SP Name:\t spGet" + tableName + "\n";
            strQry += "--\t******************************************************\n\n\n";

            strQry += "\t@QryOption \t\t\tint,\n";
            strQry += GetSPParam(objclsColumnEntityList, tableName, "\t", false);

            strQry += "\n\t@PageSize          int = null,\n";
            strQry += "\t@CurrentPage       int = null,\n";
            strQry += "\t@ItemCount         int = null output,\n";
            strQry += "\t@OrderBy           varchar(100) = null,\n";
            strQry += "\t@OrderByDirection  varchar(4) = null,\n";
            strQry += "\t@QueryString       varchar(Max) = null,\n";

            strQry = strQry.Substring(0, strQry.Length - 2) + "\n\nAS\n\nBegin\n";


            strQry += "\tDECLARE @GetByAll Int\n";
            strQry += "\tSET @GetByAll = 1\n";
            strQry += "\tDECLARE @GetByAllWithPaging Int\n";
            strQry += "\tSET @GetByAllWithPaging = 2\n";
            strQry += "\tDECLARE @GetByQueryString Int\n";
            strQry += "\tSET @GetByQueryString = 3\n";

            strQry += "\n\tDECLARE @UpperBand int, @LowerBand int\n\n";

            //--
            strQry += "\n";
            //strQry += SetNullToParam(DtRef, DtRef_M, tableName, "\t");


            strQry += "\tIF @OrderBy = null\n";
            strQry += "\t\tSET @OrderBy ='" + objclsColumnEntityList[0].ColumnName.Trim() + "'\n";
            strQry += "\tIF @OrderByDirection = null\n";
            strQry += "\t\tSET @OrderByDirection ='ASC'\n";

            strQry += "\n\n";

            strQry += "\t--------------------------------------------\n";
            strQry += "\t--		When @QryOption = 1 <Start>  (@GetByAll)\n";
            strQry += "\t--------------------------------------------\n";
            strQry += "\tIF @QryOption = @GetByAll";
            strQry += "\n\tBEGIN\n";
            strQry += "\t\tSELECT \n\t\t\t";
            strQry += GetSelectColumnList(objclsColumnEntityList, tableName, "\t\t\t");
            strQry += "ROW_NUMBER() OVER\n";
            strQry += "\t\t\t\t(\n";
            strQry += "\t\t\t\t\tORDER BY\n";
            strQry += GetOrderByClause(objclsColumnEntityList, tableName, "\t\t\t\t\t\t");
            strQry += "\n\t\t\t\t) as RowNumber";
            strQry += " \n\t\tFROM \n\t\t\t" + "[dbo].[" + tableName + "]\n";
            if (objclsTableRefEntityList.Count != 0)
            {
                strQry += TableJoinString(objclsTableRefEntityList, "\t\t\t\t", tableName);
            }
            strQry += "\t\tWHERE \n";

            strQry += GetWhereClause(objclsColumnEntityList, tableName, "\t\t\t");
            strQry += "\t\tSET @ItemCount = @@rowcount";
            strQry += "\n\tEND\n";
            strQry += "\t----------------------------------------------------------------------\n";
            strQry += "\t--		@QryOption = 1 </Ends>  (@GetByAll)\n";
            strQry += "\t----------------------------------------------------------------------\n\n\n";

            strQry += "\t----------------------------------------------------------------------\n";
            strQry += "\t--		When @QryOption = 2 <Start>  (@GetByAllWithPaging)\n";
            strQry += "\t----------------------------------------------------------------------";
            strQry += "\n\tELSE IF @QryOption = @GetByAllWithPaging";
            strQry += "\n\tBEGIN\n";
            strQry += "\t\tSET @ItemCount = \n\t\t\t(\n";
            strQry += "\t\t\t\tSELECT \n\t\t\t\t\tCOUNT(*)\n";
            strQry += " \t\t\t\tFROM \n\t\t\t\t\t" + "[dbo].[" + tableName + "]\n";
            if (objclsTableRefEntityList.Count != 0)
            {
                strQry += TableJoinString(objclsTableRefEntityList, "\t\t\t\t\t\t", tableName);
            }
            strQry += "\t\t\t\tWHERE \n";
            strQry += GetWhereClause(objclsColumnEntityList, tableName, "\t\t\t\t\t");
            strQry += "\t\t\t)\n";
            strQry += "\t\tSET @LowerBand  = (@CurrentPage - 1) * @PageSize\n";
            strQry += "\t\tSET @UpperBand  = (@CurrentPage * @PageSize) + 1\n\n";

            strQry += "\t\tBEGIN\n";
            strQry += "\t\t\tWITH tempPaged" + tableName + " AS\n";
            strQry += "\t\t\t(\n";

            strQry += "\t\t\t\tSELECT \n\t\t\t\t\t";
            strQry += GetSelectColumnList(objclsColumnEntityList, tableName, "\t\t\t\t\t");
            strQry += "ROW_NUMBER() OVER\n";
            strQry += "\t\t\t\t\t(\n";
            strQry += "\t\t\t\t\t\tORDER BY\n";
            strQry += GetOrderByClause(objclsColumnEntityList, tableName, "\t\t\t\t\t\t\t");
            strQry += "\n\t\t\t\t\t) as RowNumber";
            strQry += "\n\t\t\t\tFROM \n\t\t\t\t\t" + "[dbo].[" + tableName + "]\n";
            if (objclsTableRefEntityList.Count != 0)
            {
                strQry += TableJoinString(objclsTableRefEntityList, "\t\t\t\t\t\t", tableName);
            }
            strQry += "\t\t\t\tWHERE \n";

            strQry += GetWhereClause(objclsColumnEntityList, tableName, "\t\t\t\t\t");
            strQry += "\t\t\t)\n";
            strQry += "\t\t\tSELECT * \n";
            strQry += "\t\t\tFROM tempPaged" + tableName + "\n";
            strQry += "\t\t\tWHERE RowNumber > @LowerBand AND RowNumber < @UpperBand\n";
            strQry += "\t\t\tSELECT @ItemCount as ItemCount\n";
            strQry += "\n\t\tEND";
            strQry += "\n\tEND";
            strQry += "\n\t----------------------------------------------------------------------\n";
            strQry += "\t--		@QryOption = 2 </Ends>  (@GetByAllWithPaging)\n";
            strQry += "\t----------------------------------------------------------------------\n\n\n";

            strQry += "\t----------------------------------------------------------------------\n";
            strQry += "\t--		When @QryOption = 3 <Start>  (@GetByQueryString)\n";
            strQry += "\t----------------------------------------------------------------------";
            strQry += "\n\tELSE IF @QryOption = @GetByQueryString";
            strQry += "\n\tBEGIN\n";
            strQry += "\n\tDeclare @SQLString varchar(Max)\n";
            strQry += "\n\tSet @SQLString = '\n";
            strQry += "\t\tSELECT \n\t\t\t";
            strQry += GetSelectColumnList(objclsColumnEntityList, tableName, "\t\t\t");
            strQry += "ROW_NUMBER() OVER (ORDER BY dbo." + objclsColumnEntityList[0].TableName + "." + objclsColumnEntityList[0].ColumnName + " ASC) as RowNumber";
            strQry += " \n\t\tFROM \n\t\t\t" + "[" + tableName + "]\n";
            if (objclsTableRefEntityList.Count != 0)
            {
                strQry += TableJoinString(objclsTableRefEntityList, "\t\t\t\t", tableName);
            }
            strQry += "\t\t'\n";
            strQry += "\t\tSet @SQLString = @SQLString + @QueryString\n";
            strQry += "\t\tEXEC (@SQLString)\n";
            strQry += "\t\tSET @ItemCount = @@rowcount";
            strQry += "\n\tEND";
            strQry += "\n\t----------------------------------------------------------------------\n";
            strQry += "\t--		@QryOption = 3 </Ends>  (@GetByQueryString)\n";
            strQry += "\t----------------------------------------------------------------------\n\n\n";


            strQry += "\nEND\n\n\n";


            //strQry += "GO\n";

            //strQry += "\nIF EXISTS (SELECT * FROM [dbo].[sysobjects]\n";
            //strQry += "\t\tWHERE ID = object_id(N'[dbo].[spSearch" + tableName + "]')\n";
            //strQry += "\t\tAND OBJECTPROPERTY(id, N'IsProcedure') = 1)\n";
            //strQry += "\tDROP PROCEDURE [dbo].[spSearch" + tableName + "]\n";
            //strQry += "GO\n\n";
            //strQry += "CREATE PROCEDURE spSearch" + tableName + "\n\n";
            //strQry += "--\t******************************************************\n";
            //strQry += "--\t   Author : Maxima Prince\n";
            //strQry += "--\t   Date   :\t" + DateTime.Now + "\n";
            //strQry += "--\t******************************************************\n\n\n";

            //strQry += "\t@QryOption \t\t\tint,\n";


            //strQry += GetSPParam(objclsColumnEntityList, tableName, "\t", true);
            //strQry += "\n\t@PageSize          int = null,\n";
            //strQry += "\t@CurrentPage       int = null,\n";
            //strQry += "\t@ItemCount         int = null output,\n";
            //strQry += "\t@OrderBy           varchar(100) = null,\n";
            //strQry += "\t@OrderByDirection  varchar(4) = null,\n";
            //strQry += "\t@QueryString       varchar(Max) = null,\n";

            //strQry = strQry.Substring(0, strQry.Length - 2) + "\n\nAS\n\nBegin\n";


            //strQry += "\tDECLARE @GetByCollection Int\n";
            //strQry += "\tSET @GetByCollection = 1\n";
            //strQry += "\tDECLARE @GetByCollectionWithPaging Int\n";
            //strQry += "\tSET @GetByCollectionWithPaging = 2\n";

            //strQry += "\n\tDECLARE @UpperBand int, @LowerBand int\n\n";

            ////--
            //strQry += "\n";
            ////strQry += SetNullToParam(DtRef, DtRef_M, tableName, "\t");


            //strQry += "\tIF @OrderBy = null\n";
            //strQry += "\t\tSET @OrderBy ='" + objclsColumnEntityList[0].ColumnName + "'\n";
            //strQry += "\tIF @OrderByDirection = null\n";
            //strQry += "\t\tSET @OrderByDirection ='ASC'\n";

            //strQry += "\n\n";

            //strQry += "\t----------------------------------------------------------------------\n";
            //strQry += "\t--		When @QryOption = 1 <Start>  (@GetByCollection)\n";
            //strQry += "\t----------------------------------------------------------------------\n";
            //strQry += "\tIF @QryOption = @GetByCollection";
            //strQry += "\n\tBEGIN\n";
            //strQry += "\t\tSELECT \n\t\t\t";
            //strQry += GetSelectColumnList(objclsColumnEntityList, tableName, "\t\t\t");
            //strQry += "ROW_NUMBER() OVER\n";
            //strQry += "\t\t\t\t(\n";
            //strQry += "\t\t\t\t\tORDER BY\n";
            //strQry += GetOrderByClause(objclsColumnEntityList, tableName, "\t\t\t\t\t\t");
            //strQry += "\n\t\t\t\t) as RowNumber";
            //strQry += " \n\t\tFROM \n\t\t\t" + "[dbo].[" + tableName + "]\n";
            //if (objclsTableRefEntityList.Count != 0)
            //{
            //    strQry += TableJoinString(objclsTableRefEntityList, "\t\t\t\t", tableName);
            //}
            //strQry += "\t\tWHERE \n";

            //strQry += GetWhereCollectionClause(objclsColumnEntityList, tableName, "\t\t\t");
            //strQry += "\t\tSET @ItemCount = @@rowcount";
            //strQry += "\n\tEND\n";
            //strQry += "\n\t----------------------------------------------------------------------\n";
            //strQry += "\t--		@QryOption = 1 </Ends>  (@GetByCollection)\n";
            //strQry += "\t----------------------------------------------------------------------\n\n\n";


            //strQry += "\t----------------------------------------------------------------------\n";
            //strQry += "\t--		When @QryOption = 2 <Start>  (@GetByCollectionWithPaging)\n";
            //strQry += "\t----------------------------------------------------------------------";
            //strQry += "\n\tELSE IF @QryOption = @GetByCollectionWithPaging";
            //strQry += "\n\tBEGIN\n";
            //strQry += "\t\tSET @ItemCount = \n\t\t\t(\n";
            //strQry += "\t\t\t\tSELECT \n\t\t\t\t\tCOUNT(*)\n";
            //strQry += " \t\t\t\tFROM \n\t\t\t\t\t" + "[dbo].[" + tableName + "]\n";
            //if (objclsTableRefEntityList.Count != 0)
            //{
            //    strQry += TableJoinString(objclsTableRefEntityList, "\t\t\t\t\t\t", tableName);
            //}
            //strQry += "\t\t\t\tWHERE \n";
            //strQry += GetWhereCollectionClause(objclsColumnEntityList, tableName, "\t\t\t\t\t");
            //strQry += "\t\t\t)\n";
            //strQry += "\t\tSET @LowerBand  = (@CurrentPage - 1) * @PageSize\n";
            //strQry += "\t\tSET @UpperBand  = (@CurrentPage * @PageSize) + 1\n\n";

            //strQry += "\t\tBEGIN\n";
            //strQry += "\t\t\tWITH tempSearchPaged" + tableName + " AS\n";
            //strQry += "\t\t\t(\n";

            //strQry += "\t\t\t\tSELECT \n\t\t\t\t\t";
            //strQry += GetSelectColumnList(objclsColumnEntityList, tableName, "\t\t\t\t\t");
            //strQry += "ROW_NUMBER() OVER\n";
            //strQry += "\t\t\t\t\t(\n";
            //strQry += "\t\t\t\t\t\tORDER BY\n";
            //strQry += GetOrderByClause(objclsColumnEntityList, tableName, "\t\t\t\t\t\t\t");
            //strQry += "\n\t\t\t\t\t) as RowNumber";
            //strQry += "\n\t\t\t\tFROM \n\t\t\t\t\t" + "[dbo].[" + tableName + "]\n";
            //if (objclsTableRefEntityList.Count != 0)
            //{
            //    strQry += TableJoinString(objclsTableRefEntityList, "\t\t\t\t\t\t", tableName);
            //}
            //strQry += "\t\t\t\tWHERE \n";

            //strQry += GetWhereCollectionClause(objclsColumnEntityList, tableName, "\t\t\t\t\t");
            //strQry += "\t\t\t)\n";
            //strQry += "\t\t\tSELECT * \n";
            //strQry += "\t\t\tFROM tempSearchPaged" + tableName + "\n";
            //strQry += "\t\t\tWHERE RowNumber > @LowerBand AND RowNumber < @UpperBand\n";
            //strQry += "\t\t\tSELECT @ItemCount as ItemCount\n";
            //strQry += "\n\t\tEND";
            //strQry += "\n\tEND";
            //strQry += "\n\t----------------------------------------------------------------------\n";
            //strQry += "\t--		@QryOption = 2 </Ends>  (@GetByCollectionWithPaging)\n";
            //strQry += "\t----------------------------------------------------------------------\n\n\n";


            //strQry += "\nEND\n\n\n";
            return strQry;
        }

        private string GetSPParam(IList<clsColumnEntity> objclsColumnEntityList, string tableName, string tab, bool collection)
        {
            string strQry = "";
            if (collection == false)
            {
                for (int i = 0; i < objclsColumnEntityList.Count; i++)
                {
                    if (objclsColumnEntityList[i].GetSPParam == true)
                    {
                        if (objclsColumnEntityList[i].ColumnDBType != "image")
                        {
                            if (objclsColumnEntityList[i].IsMasterTable == true)
                            {
                                strQry += tab + "@" + objclsColumnEntityList[i].ColumnName.Trim() + tab + tab + tab + objclsColumnEntityList[i].ColumnDBType.Replace("identity", "") + " ";
                            }
                            else
                            {
                                strQry += tab + "@" + objclsColumnEntityList[i].ColumnAliasName.Trim() + tab + tab + tab + objclsColumnEntityList[i].ColumnDBType.Replace("identity", "") + " ";
                            }

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
                                    strQry += " = null";
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

        private string GetSelectColumnList(IList<clsColumnEntity> objclsColumnEntityList, string tableName, string tab)
        {
            string strQry = "";
            for (int i = 0; i < objclsColumnEntityList.Count; i++)
            {
                if (objclsColumnEntityList[i].IsMasterTable == true)
                {
                    strQry += "dbo." + objclsColumnEntityList[i].TableName.Trim() + "." + objclsColumnEntityList[i].ColumnName.Trim() + ",\n" + tab + "";
                }
                else
                {
                    strQry += objclsColumnEntityList[i].TableAliasName.Trim() + "." + objclsColumnEntityList[i].ColumnName.Trim() + " AS " + objclsColumnEntityList[i].ColumnAliasName.Trim() + ",\n" + tab + "";
                }
            }
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
                            strQry += tab + "CASE WHEN @OrderBy = '" + objclsColumnEntityList[i].ColumnName.Trim() + "' AND @OrderByDirection = 'DESC' THEN " + objclsColumnEntityList[i].TableName + "." + objclsColumnEntityList[i].ColumnName.Trim() + " END DESC,\n";
                            strQry += tab + "CASE WHEN @OrderBy = '" + objclsColumnEntityList[i].ColumnName.Trim() + "' AND @OrderByDirection = 'ASC' THEN " + objclsColumnEntityList[i].TableName + "." + objclsColumnEntityList[i].ColumnName.Trim() + " END ASC ,\n";
                        }
                        else
                        {
                            strQry += tab + "CASE WHEN @OrderBy = '" + objclsColumnEntityList[i].ColumnAliasName.Trim() + "' AND @OrderByDirection = 'DESC' THEN " + objclsColumnEntityList[i].TableAliasName + "." + objclsColumnEntityList[i].ColumnName.Trim() + " END DESC,\n";
                            strQry += tab + "CASE WHEN @OrderBy = '" + objclsColumnEntityList[i].ColumnAliasName.Trim() + "' AND @OrderByDirection = 'ASC' THEN " + objclsColumnEntityList[i].TableAliasName + "." + objclsColumnEntityList[i].ColumnName.Trim() + " END ASC ,\n";
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
                        str += tabs + "INNER JOIN dbo." + objclsTableRefEntityList[i].TableName + " AS " + objclsTableRefEntityList[i].TableAliasName + " ON " + "dbo." + tableName + "." + objclsTableRefEntityList[i].ThisColumnName + " = ";
                    }
                    else
                    {
                        str += tabs + "LEFT JOIN dbo." + objclsTableRefEntityList[i].TableName + " AS " + objclsTableRefEntityList[i].TableAliasName + " ON " + "dbo." + tableName + "." + objclsTableRefEntityList[i].ThisColumnName + " = ";
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
                if (objclsColumnEntityList[i].GetSPParam == true)
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
                            strQry += "" + tab + "\t" + And + "CASE WHEN @" + objclsColumnEntityList[i].ColumnName + " " + GetWhereClauseSubString(objclsColumnEntityList[i], tableName) + " THEN 1 WHEN " + "dbo." + objclsColumnEntityList[i].TableName + "." + objclsColumnEntityList[i].ColumnName + " = @" + objclsColumnEntityList[i].ColumnName + " THEN 1 ELSE 0 END = 1" + "\n";
                        }
                        else
                        {
                            strQry += "" + tab + "\t" + And + "CASE WHEN @" + objclsColumnEntityList[i].ColumnAliasName + " " + GetWhereClauseSubString(objclsColumnEntityList[i], tableName) + " THEN 1 WHEN " + "" + objclsColumnEntityList[i].TableAliasName + "." + objclsColumnEntityList[i].ColumnName + " = @" + objclsColumnEntityList[i].ColumnAliasName + " THEN 1 ELSE 0 END = 1" + "\n";
                        }
                        RowNumber++;
                    }
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

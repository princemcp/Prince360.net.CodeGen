using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Mega.CodeGen.MVC2Engine
{
    public class MD_JQScriptEngine
    {
        public bool MD_JQScriptEngineMethod(string nameSpace, ListBox MasterTableList, ListBox ChildTableList, string path)
        {
            path = path + "\\MasterDetail\\JQScripts";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\" + ((clsAllTableListEntity)MasterTableList.Items[0]).TableName + ".js";

            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);

            StreamWriter w = new StreamWriter(fs);
            try
            {
                IList<clsAllTableListEntity> objclsMasterTableListEntity = new List<clsAllTableListEntity>();
                foreach (clsAllTableListEntity list in MasterTableList.Items)
                {
                    objclsMasterTableListEntity.Add(list);
                }
                IList<clsAllTableListEntity> objclsChildTableListEntity = new List<clsAllTableListEntity>();
                foreach (clsAllTableListEntity list in ChildTableList.Items)
                {
                    objclsChildTableListEntity.Add(list);
                }

                w.WriteLine(GenerateMasterTableString(nameSpace, objclsMasterTableListEntity, objclsChildTableListEntity));
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

        private string GenerateMasterTableString(string nameSpace, IList<clsAllTableListEntity> objclsMasterTableListEntity, IList<clsAllTableListEntity> objclsChildTableListEntity)
        {
            string strQry = "";

            strQry += "///////////////////////////////////////////////////////////////////////////////\n";
            strQry += "//      Author      : Maxima Prince              \n";
            strQry += "//      Date        : " + DateTime.Now + "     \n";
            strQry += "//      File name   : " + objclsMasterTableListEntity[0].TableName + ".js     \n";
            strQry += "///////////////////////////////////////////////////////////////////////////////\n\n";

            strQry += "\nfunction LoadForm" + objclsMasterTableListEntity[0].TableName + "_ViewPage() {\n";
            strQry += "\ttry {\n";
            strQry += "\t\tLoad" + objclsMasterTableListEntity[0].TableName + "Grid();\n";
            //strQry += "\t\tDeleteEvent();\n";
            strQry += "\t}\n";
            strQry += "\tcatch (e) {\n";
            strQry += "\t\tHandleErrorMessage(e);\n";
            strQry += "\t}\n";
            strQry += "\tfinally { }\n";
            strQry += "}\n\n";

            strQry += "function LoadForm" + objclsMasterTableListEntity[0].TableName + "_EditPage() {\n";
            strQry += "\ttry {\n";
            strQry += "\t\tEditEvent();\n";
            //strQry += "\t\tSetDateTempletWithTextBox();\n";
            foreach (clsAllTableListEntity List in objclsChildTableListEntity)
            {
                strQry += "\t\tLoad" + List.TableName + "Grid(\"" + List.TableName + "_Grid\", $(\"#View" + List.TableName + "_" + List.TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[0].ColumnAliasName + "\").val() == null ? \"0\" : $(\"#View" + List.TableName + "_" + List.TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[0].ColumnAliasName + "\").val(), false);\n";
            }
            strQry += "\t}\n";
            strQry += "\tcatch (e) {\n";
            strQry += "\t\tHandleErrorMessage(e);\n";
            strQry += "\t}\n";
            strQry += "\tfinally { }\n";
            strQry += "}\n\n";

            strQry += "function LoadForm" + objclsMasterTableListEntity[0].TableName + "_CreatePage() {\n";
            strQry += "\ttry {\n";
            strQry += "\t\tSaveEvent();\n";
            //strQry += "\t\tSetDateTempletWithTextBox();\n";
            foreach (clsAllTableListEntity List in objclsChildTableListEntity)
            {
                strQry += "\t\tLoad" + List.TableName + "Grid(\"" + List.TableName + "_Grid\",0, false);\n";
            }
            strQry += "\t}\n";
            strQry += "\tcatch (e) {\n";
            strQry += "\t\tHandleErrorMessage(e);\n";
            strQry += "\t}\n";
            strQry += "\tfinally { }\n";
            strQry += "}\n\n";

            strQry += "function Load" + objclsMasterTableListEntity[0].TableName + "Grid() {\n";
            strQry += "\t$(\"#" + objclsMasterTableListEntity[0].TableName + "_Grid\").jqGrid({\n";
            strQry += "\t\turl: \"/" + objclsMasterTableListEntity[0].TableName + "/Get" + objclsMasterTableListEntity[0].TableName + "BySP\",\n";
            strQry += "\t\tdatatype: \"json\",\n";
            strQry += "\t\tmtype: 'POST',\n";
            strQry += "\t\tcolNames: [";
            for (int i = 0; i < objclsMasterTableListEntity[0].ColumnEntity.Count; i++)
            {
                strQry += " '" + objclsMasterTableListEntity[0].ColumnEntity[i].Lable + "', \n\t\t\t\t ";
            }
            strQry += " '', '' ";
            strQry += "],\n";
            strQry += "\t\tcolModel: [\n";

            for (int i = 0; i < objclsMasterTableListEntity[0].ColumnEntity.Count; i++)
            {
                if (i == 0)
                {
                    strQry += "\t\t\t{ name: '" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "', index: '" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "', key: true, hidden: true },\n";
                }
                else if (i > 0)
                {
                    if (objclsMasterTableListEntity[0].ColumnEntity[i].HasReference == true || objclsMasterTableListEntity[0].ColumnEntity[i].IsHidden == true)
                    {
                        strQry += "\t\t\t{ name: '" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "', index: '" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "', width: 150, align: 'left', hidden: true },\n";
                    }
                    else
                    {
                        strQry += "\t\t\t{ name: '" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "', index: '" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "', width: 150, align: 'left', hidden: false },\n";
                    }
                }
            }
            strQry += "\t\t\t{ name: '', index: 'Edit', width: 30, align: 'center'},\n";
            strQry += "\t\t\t{ name: '', index: 'Delete', width: 30, align: 'center'}\n";
            strQry += "\t\t\t],\n";
            strQry += "\t\tpager: $('#" + objclsMasterTableListEntity[0].TableName + "_Pager'),                        //pager div\n";
            strQry += "\t\trowNum: 10,                                //default page size\n";
            strQry += "\t\trowList: [10, 20, 30, 40, 50],                 //option of page size\n";
            strQry += "\t\twidth: '650',                              //grid width\n";
            strQry += "\t\theight: \"100%\",                          //grid height\n";
            strQry += "\t\tsortname: '" + objclsMasterTableListEntity[0].ColumnEntity[0].ColumnAliasName + "',                     //default sort column name\n";
            strQry += "\t\tsortorder: \"desc\",                       //sorting order\n";
            strQry += "\t\tviewrecords: true,                         //by default records show?\n";
            strQry += "\t\tmultiselect: false,                        //checkbox list\n";
            strQry += "\t\timgpath: '/Content/themes/steel/images',   //set icon in grid\n";
            strQry += "\t\tcaption: \"" + objclsMasterTableListEntity[0].TableName + " List\",                    //grid title\n";
            strQry += "\t\tsubGrid: true,                              //subgrid show/hide\n";
            strQry += "\t\tsubGridRowExpanded: function (subgrid_id, row_id) {\n";
            strQry += "\t\t\tLoadSubGridBy" + objclsMasterTableListEntity[0].TableName + "(subgrid_id, row_id);\n\t\t},\n";
            strQry += "\t\tloadComplete: function () { },\n";
            strQry += "\t\tloadError: function (xhr, status, str) {   //function calling when grid load exception occured \n";
            strQry += "\t\t\t$(\"#divMsg\").html(xhr.msg);           //set div text by error message\n";
            strQry += "\t\t},\n";
            strQry += "\t\terrorCell: function () {                   //function calling when cell exception occured\n";
            strQry += "\t\t\t $(\"#divMsg\").html('An error has occurred while processing your request.');\n";
            strQry += "\t\t}\n";
            strQry += "\t});\n";
            strQry += "}\n\n";

            strQry += "function LoadSubGridBy" + objclsMasterTableListEntity[0].TableName + "(subgrid_id, row_id) {\n";
            strQry += "\t$(\"#\" + subgrid_id).html(";
            foreach (clsAllTableListEntity List in objclsChildTableListEntity)
            {
                strQry += "\"<table id='\" + subgrid_id + \"" + List.TableName + "_Grid' class='scroll'>\"";
            }
            strQry += ");\n";
            foreach (clsAllTableListEntity List in objclsChildTableListEntity)
            {
                strQry += "\tLoad" + List.TableName + "Grid(subgrid_id + '" + List.TableName + "_Grid', row_id, true);\n";
            }
            strQry += "}\n\n";

            strQry += "function ReLoad" + objclsMasterTableListEntity[0].TableName + "Grid() {\n";
            strQry += "\tvar url = \"/" + objclsMasterTableListEntity[0].TableName + "/Get" + objclsMasterTableListEntity[0].TableName + "BySP\";\n";
            strQry += "\t$(\"#" + objclsMasterTableListEntity[0].TableName + "_Grid\").setGridParam({ url: url });\n";
            strQry += "\t$(\"#" + objclsMasterTableListEntity[0].TableName + "_Grid\").trigger(\"reloadGrid\");\n";
            strQry += "}\n\n";

            strQry += "function SaveEvent() {\n";
            strQry += "\t$(\"#btnSave\").click(function () {\n";
            strQry += "\t\ttry {\n";
            strQry += "\t\t\tif (Sys.Mvc.FormContext.validateGroup('frm_" + objclsMasterTableListEntity[0].TableName + "', '" + objclsMasterTableListEntity[0].TableName + "', true)) {\n";
            strQry += "\t\t\t\t\tSaveCollection" + objclsMasterTableListEntity[0].TableName + "(\"/" + objclsMasterTableListEntity[0].TableName + "/Create\");\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t}\n";
            strQry += "\t\tcatch (e) {\n";
            strQry += "\t\t\tHandleErrorMessage(e);\n";
            strQry += "\t\t}\n";
            strQry += "\t\tfinally { }\n";
            strQry += "\t});\n";
            strQry += "}\n\n";

            strQry += "function EditEvent() {\n";
            strQry += "\t$(\"#btnSave\").click(function () {\n";
            strQry += "\t\ttry {\n";
            strQry += "\t\t\tif (Sys.Mvc.FormContext.validateGroup('frm_" + objclsMasterTableListEntity[0].TableName + "', '" + objclsMasterTableListEntity[0].TableName + "', true)) {\n";
            strQry += "\t\t\t\t\tSaveCollection" + objclsMasterTableListEntity[0].TableName + "(\"/" + objclsMasterTableListEntity[0].TableName + "/Edit\");\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t}\n";
            strQry += "\t\tcatch (e) {\n";
            strQry += "\t\t\tHandleErrorMessage(e);\n";
            strQry += "\t\t}\n";
            strQry += "\t\tfinally { }\n";
            strQry += "\t});\n";
            strQry += "}\n\n";


            strQry += "function SelectedID() {\n";
            strQry += "\tvar grid = $(\"#" + objclsMasterTableListEntity[0].TableName + "_Grid\").jqGrid();\n";
            strQry += "\tvar rowKey = grid.getGridParam(\"selarrrow\")\n";
            strQry += "\treturn rowKey;\n";
            strQry += "\t}\n\n";

            strQry += "\nfunction DeleteRecords(" + objclsMasterTableListEntity[0].ColumnEntity[0].ColumnAliasName + ") {\n";
            strQry += "\tvar agree = confirm(\"Are you sure you want to delete?\");\n";
            strQry += "\tif (agree) {\n";
            strQry += "\t\t$.post(\"/" + objclsMasterTableListEntity[0].TableName + "/Delete\", { " + objclsMasterTableListEntity[0].ColumnEntity[0].ColumnAliasName + ": " + objclsMasterTableListEntity[0].ColumnEntity[0].ColumnAliasName + " }, function (data) { }, \"json\");\n";
            strQry += "\t\tReLoad" + objclsMasterTableListEntity[0].TableName + "Grid();\n";
            strQry += "\t}\n";
            strQry += "}\n\n";

            strQry += "function GetCollection" + objclsMasterTableListEntity[0].TableName + "() {\n";
            for (int i = 0; i < objclsMasterTableListEntity[0].ColumnEntity.Count; i++)
            {
                if (objclsMasterTableListEntity[0].ColumnEntity[i].IsMasterTable == true)
                {
                    if (objclsMasterTableListEntity[0].ColumnEntity[i].ColumnIsNull == false)
                    {
                        if (objclsMasterTableListEntity[0].ColumnEntity[i].HasReference == true && objclsMasterTableListEntity[0].ColumnEntity[i].IsHidden == false)
                        {
                            //string ddl = "#View" + objclsMasterTableListEntity[0].TableName + "ddl" + objclsMasterTableListEntity[0].ColumnEntity[i].ReferenceTableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].RefColumnName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName;
                            string ddl = "#View" + objclsMasterTableListEntity[0].TableName + "_ddl" + objclsMasterTableListEntity[0].ColumnEntity[i].ReferenceTableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].RefColumnName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName;
                            strQry += "\tthis." + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + " = $(\"" + ddl + "\").val() == null || $(\"" + ddl + "\").val() ==\"\" ? \"0\" : $(\"" + ddl + "\").val();\n";
                        }
                        else if (objclsMasterTableListEntity[0].ColumnEntity[i].IsHidden == true)
                        {
                            //string ddl = "#View" + objclsMasterTableListEntity[0].TableName + "_hdd" + objclsMasterTableListEntity[0].ColumnEntity[i].ReferenceTableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].RefColumnName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName;
                            //if (objclsMasterTableListEntity[0].ColumnEntity[i].ReferenceTableName == "")
                            //{
                            //string ddl = "#View" + objclsMasterTableListEntity[0].TableName + "_hdd_" + objclsMasterTableListEntity[0].ColumnEntity[i].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName;
                            //}
                            string ddl = "#View" + objclsMasterTableListEntity[0].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName;
                            if (objclsMasterTableListEntity[0].ColumnEntity[i].ColumnDotNetType == "string")
                            {
                                strQry += "\tthis." + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + " = $(\"" + ddl + "\").val() == null || $(\"" + ddl + "\").val() ==\"\" ? \"\" : $(\"" + ddl + "\").val();\n";
                            }
                            else if (objclsMasterTableListEntity[0].ColumnEntity[i].ColumnDotNetType == "DateTime")
                            {
                                strQry += "\tthis." + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + " = $(\"" + ddl + "\").val() == null || $(\"" + ddl + "\").val() ==\"\" ? GetCurrentDate() : $(\"" + ddl + "\").val();\n";
                            }
                            else
                            {
                                strQry += "\tthis." + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + " = $(\"" + ddl + "\").val() == null || $(\"" + ddl + "\").val() ==\"\" ? \"0\" : $(\"" + ddl + "\").val();\n";
                            }
                        }
                        else if (objclsMasterTableListEntity[0].ColumnEntity[i].HasReference == false && objclsMasterTableListEntity[0].ColumnEntity[i].IsHidden == false)
                        {
                            if (objclsMasterTableListEntity[0].ColumnEntity[i].ColumnDotNetType == "string")
                            {
                                strQry += "\tthis." + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + " = $(\"#View" + objclsMasterTableListEntity[0].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "\").val() == null || $(\"#View" + objclsMasterTableListEntity[0].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "\").val() == \"\" ? \"\" : $(\"#View" + objclsMasterTableListEntity[0].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "\").val();\n";
                            }
                            else if (objclsMasterTableListEntity[0].ColumnEntity[i].ColumnDotNetType == "DateTime")
                            {
                                strQry += "\tthis." + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + " = $(\"#View" + objclsMasterTableListEntity[0].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "\").val() == null || $(\"#View" + objclsMasterTableListEntity[0].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "\").val() == \"\" ? GetCurrentDate() : $(\"#View" + objclsMasterTableListEntity[0].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "\").val();\n";
                            }
                            else
                            {
                                strQry += "\tthis." + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + " = $(\"#View" + objclsMasterTableListEntity[0].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "\").val() == null || $(\"#View" + objclsMasterTableListEntity[0].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "\").val() == \"\" ? \"0\" : $(\"#View" + objclsMasterTableListEntity[0].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "\").val();\n";
                            }
                        }
                    }
                    else
                    {
                        if (objclsMasterTableListEntity[0].ColumnEntity[i].HasReference == true && objclsMasterTableListEntity[0].ColumnEntity[i].IsHidden == false)
                        {
                            string ddl = "#View" + objclsMasterTableListEntity[0].TableName + "_ddl" + objclsMasterTableListEntity[0].ColumnEntity[i].ReferenceTableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].RefColumnName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName;
                            strQry += "\tthis." + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + " = $(\"" + ddl + "\").val() == null || $(\"" + ddl + "\").val() ==\"0\" ? \"\" : $(\"" + ddl + "\").val();\n";
                        }
                        else if (objclsMasterTableListEntity[0].ColumnEntity[i].IsHidden == true)
                        {
                            //string ddl = "#View" + objclsMasterTableListEntity[0].TableName + "_hdd" + objclsMasterTableListEntity[0].ColumnEntity[i].ReferenceTableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].RefColumnName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName;
                            //if (objclsMasterTableListEntity[0].ColumnEntity[i].ReferenceTableName == "")
                            //{
                            //    ddl = "#View" + objclsMasterTableListEntity[0].TableName + "_hdd_" + objclsMasterTableListEntity[0].ColumnEntity[i].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName;
                            //}
                            string ddl = "#View" + objclsMasterTableListEntity[0].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName;
                            strQry += "\tthis." + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + " = $(\"" + ddl + "\").val() == null || $(\"" + ddl + "\").val() ==\"0\" ? \"\" : $(\"" + ddl + "\").val();\n";
                        }
                        else if (objclsMasterTableListEntity[0].ColumnEntity[i].HasReference == false && objclsMasterTableListEntity[0].ColumnEntity[i].IsHidden == false)
                        {
                            if (objclsMasterTableListEntity[0].ColumnEntity[i].ColumnDotNetType == "string")
                            {
                                strQry += "\tthis." + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + " = $(\"#View" + objclsMasterTableListEntity[0].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "\").val() == null || $(\"#View" + objclsMasterTableListEntity[0].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "\").val() == \"\" ? \"\" : $(\"#View" + objclsMasterTableListEntity[0].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "\").val();\n";
                            }
                            else if (objclsMasterTableListEntity[0].ColumnEntity[i].ColumnDotNetType == "DateTime")
                            {
                                strQry += "\tthis." + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + " = $(\"#View" + objclsMasterTableListEntity[0].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "\").val() == null || $(\"#View" + objclsMasterTableListEntity[0].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "\").val() == \"\" ? \"\" : $(\"#View" + objclsMasterTableListEntity[0].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "\").val();\n";
                            }
                            else
                            {
                                strQry += "\tthis." + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + " = $(\"#View" + objclsMasterTableListEntity[0].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "\").val() == null || $(\"#View" + objclsMasterTableListEntity[0].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "\").val() == \"0\" ? \"\" : $(\"#View" + objclsMasterTableListEntity[0].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "\").val();\n";
                            }
                        }

                    }
                }
            }
            strQry += "}\n\n";

            strQry += "function ResetCollection" + objclsMasterTableListEntity[0].TableName + "() {\n";
            for (int i = 0; i < objclsMasterTableListEntity[0].ColumnEntity.Count; i++)
            {
                if (objclsMasterTableListEntity[0].ColumnEntity[i].IsMasterTable == true)
                {
                    if (objclsMasterTableListEntity[0].ColumnEntity[i].HasReference == true && objclsMasterTableListEntity[0].ColumnEntity[i].IsHidden == false)
                    {
                        string ddl = "#View" + objclsMasterTableListEntity[0].TableName + "_ddl" + objclsMasterTableListEntity[0].ColumnEntity[i].ReferenceTableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].RefColumnName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName;
                        strQry += "\t$(\"" + ddl + "\").val(\"\");\n";
                    }
                    else if (objclsMasterTableListEntity[0].ColumnEntity[i].IsHidden == true)
                    {
                        string ddl = "#View" + objclsMasterTableListEntity[0].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName;
                        strQry += "\t$(\"" + ddl + "\").val(\"\");\n";
                    }
                    else if (objclsMasterTableListEntity[0].ColumnEntity[i].HasReference == false && objclsMasterTableListEntity[0].ColumnEntity[i].IsHidden == false)
                    {
                        strQry += "\t$(\"#View" + objclsMasterTableListEntity[0].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].TableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "\").val(\"\");\n";
                    }
                }
            }
            strQry += "\tSys.Mvc.FormContext.validateGroup('frm_" + objclsMasterTableListEntity[0].TableName + "', '" + objclsMasterTableListEntity[0].TableName + "', false);";
            strQry += "\n}\n\n";


            strQry += "function SaveCollection" + objclsMasterTableListEntity[0].TableName + "(posturl) {\n";
            strQry += "\t//Master Table " + objclsMasterTableListEntity[0].TableName + "\n";
            strQry += "\tvar obj" + objclsMasterTableListEntity[0].TableName + " = new GetCollection" + objclsMasterTableListEntity[0].TableName + "();\n";
            foreach (clsAllTableListEntity List in objclsChildTableListEntity)
            {
                strQry += "\t//Child Table " + List.TableName + "\n";
                strQry += "\tfor (var i = " + List.TableName + "Collection.length - 1; i >= 0; i--) {\n";
                strQry += "\t\t" + List.TableName + "Collection[i].Edit = '';\n";
                strQry += "\t\t" + List.TableName + "Collection[i].Delete = '';\n";
                strQry += "\t\tif (" + List.TableName + "Collection[i].id == \"0\" && " + List.TableName + "Collection[i].Tag == 3) {\n";
                strQry += "\t\t\t" + List.TableName + "Collection.splice(i, 1);\n";
                strQry += "\t\t\tcontinue;\n";
                strQry += "\t\t}\n";
                strQry += "\t\tif (" + List.TableName + "Collection[i].Tag == \"0\") {\n";
                strQry += "\t\t\t" + List.TableName + "Collection.splice(i, 1);\n";
                strQry += "\t\t\tcontinue;\n";
                strQry += "\t\t}\n";
                strQry += "\t\tif (" + List.TableName + "Collection[i].id == \"0\" && " + List.TableName + "Collection[i].Tag == 2) {\n";
                strQry += "\t\t\t" + List.TableName + "Collection[i].Tag = 1;\n";
                strQry += "\t\t\tcontinue;\n";
                strQry += "\t\t}\n";
                strQry += "\t}\n";
                strQry += "\tobj" + objclsMasterTableListEntity[0].TableName + "." + List.TableName + "Entity = " + List.TableName + "Collection;\n";
            }
            strQry += "\tvar objSave" + objclsMasterTableListEntity[0].TableName + " = $.toJSON(obj" + objclsMasterTableListEntity[0].TableName + ");           //convert objItemTypes to json type\n";
            strQry += "\t$.post(posturl, { jsonData: objSave" + objclsMasterTableListEntity[0].TableName + " }, function (response, status, xhr) {\n";
            strQry += "\t}, \"json\");\n";
            strQry += "\tResetCollection" + objclsMasterTableListEntity[0].TableName + "();\n";
            strQry += "}\n\n";

            strQry += "//Extra Methods <Start> \n\n";

            //strQry += "function SetDateTempletWithTextBox() {\n";
            //strQry += "//\t$(\"# TextboxName \").datepicker({\n";
            //strQry += "//\t\tchangeMonth: true,\n";
            //strQry += "//\t\tchangeYear: true,\n";
            //strQry += "//\t\tshowOn: \"button\",\n";
            //strQry += "//\t\tbuttonImage: \"../../Content/dateimages/calendar.gif\",\n";
            //strQry += "//\t\tbuttonImageOnly: true\n";
            //strQry += "//\t});\n";
            //strQry += "}\n\n";

            strQry += "function GetCurrentDate() {\n";
            strQry += "\tvar cDate = new Date();\n";
            strQry += "\tvar displayDate = (cDate.getMonth() + 1) + '/' + (cDate.getDate()) + '/' + cDate.getFullYear();\n";
            strQry += "\treturn displayDate;\n";
            strQry += "}\n\n";

            //strQry += "\nfunction DeleteCollection" + objclsMasterTableListEntity[0].TableName + "() {\n";
            //strQry += "\tvar " + objclsMasterTableListEntity[0].TableName + "grid = $(\"#" + objclsMasterTableListEntity[0].TableName + "grid\").jqGrid();          //get grid row collection\n";
            //strQry += "\tvar rowKey = " + objclsMasterTableListEntity[0].TableName + "grid.getGridParam(\"selarrrow\")               //selected row key no as array\n";
            //strQry += "\tvar agree = confirm(\"Are you sure you want to delete?\");\n";
            //strQry += "\tif (agree) {\n";
            //strQry += "\t\t for (var d = 0; d < rowKey.length; d++) {\n";
            //strQry += "\t\t$.ajax({\n";
            //strQry += "\t\t\turl: \"/" + objclsMasterTableListEntity[0].TableName + "/Delete?" + objclsMasterTableListEntity[0].ColumnEntity[0].ColumnAliasName + "=\" + d." + objclsMasterTableListEntity[0].ColumnEntity[0].ColumnAliasName + ",\n";
            //strQry += "\t\t\tdatatype: 'json',\n";
            //strQry += "\t\t\tmtype: 'POST',\n";
            //strQry += "\t\t\tsuccess: function (data) {  },\n";
            //strQry += "\t\t\tError: function (response, status, xhr) {\n";
            //strQry += "\t\t\t\tif (status == \"error\") {\n";
            //strQry += "\t\t\t\t\talert(xhr.status + \" \" + xhr.statusText);\n";
            //strQry += "\t\t\t\t}\n";
            //strQry += "\t\t\t}\n";
            //strQry += "\t\t});\n\t}\n}\n";
            //strQry += "\tReLoad" + objclsMasterTableListEntity[0].TableName + "Grid();\n";
            //strQry += "}\n\n";

            strQry += "function HandleErrorMessage(errorMessage) {\n";
            strQry += "\tvar message = \"<div style='color:Red;'>\" + errorMessage + '</div>';\n";
            strQry += "\t$(\"#divMsg\").html(message);\n";
            strQry += "}\n\n";

            strQry += "//Extra Methods ";

            foreach (clsAllTableListEntity List in objclsChildTableListEntity)
            {
                strQry += GetChildCode(objclsMasterTableListEntity[0].TableName, List);
            }

            return strQry;
        }

        private string GetChildCode(string MasterTable, clsAllTableListEntity objclsChildTableListEntity)
        {
            string strQry = "";

            strQry += "//#############  Child Table code for ------------ " + objclsChildTableListEntity.TableName + "\n\n";
            strQry += "// Global Variable for " + objclsChildTableListEntity.TableName + "\n\n";
            strQry += "var " + objclsChildTableListEntity.TableName + "CurrentID;\n";
            strQry += "var " + objclsChildTableListEntity.TableName + "Collection = [];\n";
            strQry += "// End of Global Variable for " + objclsChildTableListEntity.TableName + "\n";

            strQry += GetChild_GRID_Code(MasterTable, objclsChildTableListEntity);

            strQry += GetChild_CollectionForGRID_Code(MasterTable, objclsChildTableListEntity);

            strQry += GetChild_CollectionFromGRID_Code(MasterTable, objclsChildTableListEntity);

            strQry += "//Add Entity To GRID ------------>>>>>>>>>>>>>>>> " + objclsChildTableListEntity.TableName + "\n\n";
            strQry += "function Add" + objclsChildTableListEntity.TableName + "ToGrid() {\n";
            strQry += "\ttry {\n";
            strQry += "\t\tif (Sys.Mvc.FormContext.validateGroup('frm_" + MasterTable + "', '" + objclsChildTableListEntity.TableName + "', true)) {\n";
            strQry += "\t\t\tvar grid = $(\"#" + objclsChildTableListEntity.TableName + "_Grid\").jqGrid();\n";
            strQry += "\t\t\tvar ids = grid.getDataIDs();\n";
            strQry += "\t\t\tif ($('#btn" + objclsChildTableListEntity.TableName + "AddItem').val() != 'Update') {\n";
            strQry += "\t\t\t\tvar data = new GetCollection" + objclsChildTableListEntity.TableName + "ForGrid(" + objclsChildTableListEntity.TableName + "Collection.length);\n";
            strQry += "\t\t\t\tdata.Tag = 1;\n";
            strQry += "\t\t\t\tgrid.addRowData(ids.length, data);\n";
            strQry += "\t\t\t\t" + objclsChildTableListEntity.TableName + "Collection.push(data);\n";
            strQry += "\t\t\t\tReset" + objclsChildTableListEntity.TableName + "();\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\telse {\n";
            strQry += "\t\t\t\t" + objclsChildTableListEntity.TableName + "Collection[" + objclsChildTableListEntity.TableName + "CurrentID] = new GetCollection" + objclsChildTableListEntity.TableName + "ForGrid(" + objclsChildTableListEntity.TableName + "CurrentID);\n";
            strQry += "\t\t\t\t" + objclsChildTableListEntity.TableName + "Collection[" + objclsChildTableListEntity.TableName + "CurrentID].Tag = 2;\n";
            strQry += "\t\t\t\tgrid.clearGridData();\n";
            strQry += "\t\t\t\tfor (var i = 0; i < " + objclsChildTableListEntity.TableName + "Collection.length; i++) {\n";
            strQry += "\t\t\t\t\tif (" + objclsChildTableListEntity.TableName + "Collection[i].Tag != 3) {\n";
            strQry += "\t\t\t\t\t\tgrid.addRowData(i + 1, " + objclsChildTableListEntity.TableName + "Collection[i]);\n";
            strQry += "\t\t\t\t\t}\n";
            strQry += "\t\t\t\t}\n";
            strQry += "\t\t\t\t$('#btn" + objclsChildTableListEntity.TableName + "AddItem').val('Add Item');\n";
            strQry += "\t\t\t\tReset" + objclsChildTableListEntity.TableName + "();\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t}\n";
            strQry += "\t}\n";
            strQry += "\tcatch (e) {\n";
            strQry += "\t\tHandleErrorMessage(e);\n";
            strQry += "\t}\n";
            strQry += "\tfinally { }\n";
            strQry += "}\n\n";
            strQry += "//End of Add Entity To GRID -----------------\n\n";

            strQry += "//Edit Event From GRID ------------>>>>>>>>>>>>>>>> " + objclsChildTableListEntity.TableName + "\n\n";
            strQry += "function Edit" + objclsChildTableListEntity.TableName + "Records(id) {\n";
            strQry += "\ttry {\n";
            strQry += "\t\tSetCollection" + objclsChildTableListEntity.TableName + "(" + objclsChildTableListEntity.TableName + "Collection[id]);\n";
            strQry += "\t\t$('#btn" + objclsChildTableListEntity.TableName + "AddItem').val('Update');\n";
            strQry += "\t\t" + objclsChildTableListEntity.TableName + "CurrentID = id.toString();\n";
            strQry += "\t}\n";
            strQry += "\tcatch (e) {\n";
            strQry += "\t\tHandleErrorMessage(e);\n";
            strQry += "\t}\n";
            strQry += "\tfinally { }\n";
            strQry += "}\n\n";
            strQry += "//End of Edit Event From GRID-----------------\n\n";

            strQry += "//Delete Event From GRID ------------>>>>>>>>>>>>>>>>" + objclsChildTableListEntity.TableName + "\n\n";
            strQry += "function Delete" + objclsChildTableListEntity.TableName + "Records(id) {\n";
            strQry += "\ttry {\n";
            strQry += "\t\tvar grid = $(\"#" + objclsChildTableListEntity.TableName + "_Grid\").jqGrid();\n";
            strQry += "\t\t" + objclsChildTableListEntity.TableName + "Collection[id].Tag = 3;\n";
            strQry += "\t\tgrid.clearGridData();\n";
            strQry += "\t\tfor (var i = 0; i < " + objclsChildTableListEntity.TableName + "Collection.length; i++) {\n";
            strQry += "\t\t\tif (" + objclsChildTableListEntity.TableName + "Collection[i].Tag != 3) {\n";
            strQry += "\t\t\t\tgrid.addRowData(i + 1, " + objclsChildTableListEntity.TableName + "Collection[i]);\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t}\n";
            strQry += "\t}\n";
            strQry += "\tcatch (e) {\n";
            strQry += "\t\tHandleErrorMessage(e);\n";
            strQry += "\t}\n";
            strQry += "\tfinally { }\n";
            strQry += "}\n\n";
            strQry += "//End of Delete Event From GRID-----------------\n\n";

            strQry += GetChild_SetCollection_Code(MasterTable, objclsChildTableListEntity);

            strQry += GetChild_ResetCollection_Code(MasterTable, objclsChildTableListEntity);

            return strQry;
        }

        private string GetChild_GRID_Code(string MasterTable, clsAllTableListEntity objclsChildTableListEntity)
        {
            string strQry = "\n";
            strQry += "//Child Grid for ------------>>>>>>>>>>>>>>>> " + objclsChildTableListEntity.TableName + "\n\n";
            strQry += "function Load" + objclsChildTableListEntity.TableName + "Grid(Grid_ID, id, hide) {\n";
            strQry += "\t$(\"#\" + Grid_ID).jqGrid({\n";
            strQry += "\t\turl: \"/" + MasterTable + "/Get" + objclsChildTableListEntity.TableName + "BySP?id=\" + id,\n";
            strQry += "\t\tdatatype: \"json\",\n";
            strQry += "\t\tmtype: 'POST',\n";
            strQry += "\t\tcolNames: [";
            for (int i = 0; i < objclsChildTableListEntity.ColumnEntity.Count; i++)
            {
                strQry += " '" + objclsChildTableListEntity.ColumnEntity[i].Lable + "', \n\t\t\t\t ";
            }
            strQry += " '', '' ";
            strQry += "],\n";
            strQry += "\t\tcolModel: [\n";

            for (int i = 0; i < objclsChildTableListEntity.ColumnEntity.Count; i++)
            {
                if (i == 0)
                {
                    strQry += "\t\t\t{ name: '" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "', index: '" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "', key: true, hidden: true },\n";
                }
                else if (i > 0)
                {
                    if (objclsChildTableListEntity.ColumnEntity[i].HasReference == true || objclsChildTableListEntity.ColumnEntity[i].IsHidden == true)
                    {
                        strQry += "\t\t\t{ name: '" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "', index: '" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "', width: 150, align: 'left', hidden: true },\n";
                    }
                    else
                    {
                        strQry += "\t\t\t{ name: '" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "', index: '" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "', width: 150, align: 'left', hidden: false },\n";
                    }
                }
            }
            strQry += "\t\t\t{ name: 'rownumber', index: 'rownumber', width: 150, align: 'left', hidden: true },\n";
            strQry += "\t\t\t{ name: 'Tag', index: 'Tag', width: 150, align: 'left', hidden: true },\n";

            strQry += "\t\t\t{ name: 'Edit', index: 'Edit', width: 50, align: 'center', hidden: hide},\n";
            strQry += "\t\t\t{ name: 'Delete', index: 'Delete', width: 70, align: 'center', hidden: hide}\n";
            strQry += "\t\t\t],\n";
            //strQry += "\t\tpager: $('#" + objclsChildTableListEntity.TableName + "_Pager'),                        //pager div\n";
            //strQry += "\t\trowNum: 10,                                //default page size\n";
            //strQry += "\t\trowList: [10, 20, 30, 40, 50],                 //option of page size\n";
            strQry += "\t\twidth: '650',                              //grid width\n";
            strQry += "\t\theight: \"100%\",                          //grid height\n";
            strQry += "\t\tsortname: '" + objclsChildTableListEntity.ColumnEntity[0].ColumnAliasName + "',                     //default sort column name\n";
            strQry += "\t\tsortorder: \"desc\",                       //sorting order\n";
            strQry += "\t\tviewrecords: true,                         //by default records show?\n";
            strQry += "\t\tmultiselect: false,                        //checkbox list\n";
            strQry += "\t\timgpath: '/Content/themes/steel/images',   //set icon in grid\n";
            strQry += "\t\tcaption: \"" + objclsChildTableListEntity.TableName + " List\",                    //grid title\n";
            strQry += "\t\tloadComplete: function () { " + objclsChildTableListEntity.TableName + "Collection = Get" + objclsChildTableListEntity.TableName + "CollectionFromGRID(); },\n";
            strQry += "\t\tloadError: function (xhr, status, str) {   //function calling when grid load exception occured \n";
            strQry += "\t\t\t$(\"#divMsg\").html(xhr.msg);           //set div text by error message\n";
            strQry += "\t\t},\n";
            strQry += "\t\terrorCell: function () {                   //function calling when cell exception occured\n";
            strQry += "\t\t\t $(\"#divMsg\").html('An error has occurred while processing your request.');\n";
            strQry += "\t\t}\n";
            strQry += "\t});\n";
            strQry += "}\n\n";
            strQry += "//End of Child Grid -------- " + objclsChildTableListEntity.TableName + "-----------------\n\n";
            return strQry;
        }

        private string GetChild_CollectionForGRID_Code(string MasterTable, clsAllTableListEntity objclsChildTableListEntity)
        {
            string strQry = "\n";
            strQry += "//Get Collection For GRID ------------>>>>>>>>>>>>>>>>" + objclsChildTableListEntity.TableName + "\n\n";
            strQry += "function GetCollection" + objclsChildTableListEntity.TableName + "ForGrid(" + objclsChildTableListEntity.TableName + "CurrentID) {\n";
            strQry += "\tif (" + objclsChildTableListEntity.TableName + "CurrentID == null) {\n";
            strQry += "\t\t//" + objclsChildTableListEntity.TableName + "CurrentID = parseInt($(\"#" + objclsChildTableListEntity.TableName + "_Grid\").getDataIDs().length).toString();\n";
            strQry += "\t\t" + objclsChildTableListEntity.TableName + "CurrentID = " + objclsChildTableListEntity.TableName + "Collection.length;\n";
            strQry += "\t}\n";

            for (int i = 0; i < objclsChildTableListEntity.ColumnEntity.Count; i++)
            {
                if (objclsChildTableListEntity.ColumnEntity[i].IsMasterTable == true)
                {
                    if (objclsChildTableListEntity.ColumnEntity[i].ColumnIsNull == false)
                    {
                        if (objclsChildTableListEntity.ColumnEntity[i].HasReference == true && objclsChildTableListEntity.ColumnEntity[i].IsHidden == false)
                        {
                            //string ddl = "#View" + objclsChildTableListEntity.TableName + "ddl" + objclsChildTableListEntity.ColumnEntity[i].ReferenceTableName + "_" + objclsChildTableListEntity.ColumnEntity[i].RefColumnName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName;
                            string ddl = "#View" + objclsChildTableListEntity.TableName + "_ddl" + objclsChildTableListEntity.ColumnEntity[i].ReferenceTableName + "_" + objclsChildTableListEntity.ColumnEntity[i].RefColumnName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName;
                            strQry += "\tthis." + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + " = $(\"" + ddl + "\").val() == null || $(\"" + ddl + "\").val() ==\"\" ? \"0\" : $(\"" + ddl + "\").val();\n";
                        }
                        else if (objclsChildTableListEntity.ColumnEntity[i].IsHidden == true)
                        {

                            //string ddl = "#View" + objclsChildTableListEntity.TableName + "_hdd" + objclsChildTableListEntity.ColumnEntity[i].ReferenceTableName + "_" + objclsChildTableListEntity.ColumnEntity[i].RefColumnName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName;
                            //if (objclsChildTableListEntity.ColumnEntity[i].ReferenceTableName == "")
                            //{
                            //string ddl = "#View" + objclsChildTableListEntity.TableName + "_hdd_" + objclsChildTableListEntity.ColumnEntity[i].TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName;
                            //}
                            string ddl = "#View" + objclsChildTableListEntity.TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName;
                            if (objclsChildTableListEntity.ColumnEntity[i].ColumnDotNetType == "string")
                            {
                                strQry += "\tthis." + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + " = $(\"" + ddl + "\").val() == null || $(\"" + ddl + "\").val() ==\"\" ? \"\" : $(\"" + ddl + "\").val();\n";
                            }
                            else if (objclsChildTableListEntity.ColumnEntity[i].ColumnDotNetType == "DateTime")
                            {
                                strQry += "\tthis." + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + " = $(\"" + ddl + "\").val() == null || $(\"" + ddl + "\").val() ==\"\" ? GetCurrentDate() : $(\"" + ddl + "\").val();\n";
                            }
                            else
                            {
                                strQry += "\tthis." + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + " = $(\"" + ddl + "\").val() == null || $(\"" + ddl + "\").val() ==\"\" ? \"0\" : $(\"" + ddl + "\").val();\n";
                            }
                        }
                        else if (objclsChildTableListEntity.ColumnEntity[i].HasReference == false && objclsChildTableListEntity.ColumnEntity[i].IsHidden == false)
                        {
                            if (objclsChildTableListEntity.ColumnEntity[i].ColumnDotNetType == "string")
                            {
                                strQry += "\tthis." + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + " = $(\"#View" + objclsChildTableListEntity.TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "\").val() == null || $(\"#View" + objclsChildTableListEntity.TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "\").val() == \"\" ? \"\" : $(\"#View" + objclsChildTableListEntity.TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "\").val();\n";
                            }
                            else if (objclsChildTableListEntity.ColumnEntity[i].ColumnDotNetType == "DateTime")
                            {
                                strQry += "\tthis." + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + " = $(\"#View" + objclsChildTableListEntity.TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "\").val() == null || $(\"#View" + objclsChildTableListEntity.TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "\").val() == \"\" ? GetCurrentDate() : $(\"#View" + objclsChildTableListEntity.TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "\").val();\n";
                            }
                            else
                            {
                                strQry += "\tthis." + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + " = $(\"#View" + objclsChildTableListEntity.TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "\").val() == null || $(\"#View" + objclsChildTableListEntity.TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "\").val() == \"\" ? \"0\" : $(\"#View" + objclsChildTableListEntity.TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "\").val();\n";
                            }
                        }
                    }
                    else
                    {
                        if (objclsChildTableListEntity.ColumnEntity[i].HasReference == true && objclsChildTableListEntity.ColumnEntity[i].IsHidden == false)
                        {
                            string ddl = "#View" + objclsChildTableListEntity.TableName + "_ddl" + objclsChildTableListEntity.ColumnEntity[i].ReferenceTableName + "_" + objclsChildTableListEntity.ColumnEntity[i].RefColumnName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName;
                            strQry += "\tthis." + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + " = $(\"" + ddl + "\").val() == null || $(\"" + ddl + "\").val() ==\"0\" ? \"\" : $(\"" + ddl + "\").val();\n";
                        }
                        else if (objclsChildTableListEntity.ColumnEntity[i].IsHidden == true)
                        {
                            //string ddl = "#View" + objclsChildTableListEntity.TableName + "_hdd" + objclsChildTableListEntity.ColumnEntity[i].ReferenceTableName + "_" + objclsChildTableListEntity.ColumnEntity[i].RefColumnName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName;
                            //if (objclsChildTableListEntity.ColumnEntity[i].ReferenceTableName == "")
                            //{
                            //    ddl = "#View" + objclsChildTableListEntity.TableName + "_hdd_" + objclsChildTableListEntity.ColumnEntity[i].TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName;
                            //}
                            string ddl = "#View" + objclsChildTableListEntity.TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName;
                            strQry += "\tthis." + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + " = $(\"" + ddl + "\").val() == null || $(\"" + ddl + "\").val() ==\"0\" ? \"\" : $(\"" + ddl + "\").val();\n";
                        }
                        else if (objclsChildTableListEntity.ColumnEntity[i].HasReference == false && objclsChildTableListEntity.ColumnEntity[i].IsHidden == false)
                        {
                            if (objclsChildTableListEntity.ColumnEntity[i].ColumnDotNetType == "string")
                            {
                                strQry += "\tthis." + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + " = $(\"#View" + objclsChildTableListEntity.TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "\").val() == null || $(\"#View" + objclsChildTableListEntity.TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "\").val() == \"\" ? \"\" : $(\"#View" + objclsChildTableListEntity.TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "\").val();\n";
                            }
                            else if (objclsChildTableListEntity.ColumnEntity[i].ColumnDotNetType == "DateTime")
                            {
                                strQry += "\tthis." + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + " = $(\"#View" + objclsChildTableListEntity.TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "\").val() == null || $(\"#View" + objclsChildTableListEntity.TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "\").val() == \"\" ? \"\" : $(\"#View" + objclsChildTableListEntity.TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "\").val();\n";
                            }
                            else
                            {
                                strQry += "\tthis." + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + " = $(\"#View" + objclsChildTableListEntity.TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "\").val() == null || $(\"#View" + objclsChildTableListEntity.TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "\").val() == \"0\" ? \"\" : $(\"#View" + objclsChildTableListEntity.TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "\").val();\n";
                            }
                        }

                    }
                }
                else
                {
                    strQry += "\tthis." + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + " = $(\"#Related_Dropdown_list_name\").val() == \"0\" ? \"\" : $(\"#Related_Dropdown_list_name option:selected\").text();\n";
                }
            }

            strQry += "\tthis.rownumber = " + objclsChildTableListEntity.TableName + "CurrentID.toString();\n";
            strQry += "\tthis.Tag = 1;\n";
            strQry += "\tthis.Edit = \"<a style=\\\"text-decoration: underline; color: Blue; cursor: pointer;\\\" onclick=\\\"Edit" + objclsChildTableListEntity.TableName + "Records('\" + " + objclsChildTableListEntity.TableName + "CurrentID + \"')\\\"><img id=\\\"img1\\\" src=\\\"../../Content/images/edit_icon.png\\\" /></a>\";\n";
            strQry += "\tthis.Delete = \"<a style=\\\"text-decoration: underline; color: Blue; cursor: pointer;\\\" onclick=\\\"Delete" + objclsChildTableListEntity.TableName + "Records('\" + " + objclsChildTableListEntity.TableName + "CurrentID + \"')\\\"><img id=\\\"img2\\\" src=\\\"../../Content/images/delete_icon.png\\\" /></a>\";\n";
            strQry += "\t" + objclsChildTableListEntity.TableName + "CurrentID = null;\n";
            strQry += "}\n\n";
            strQry += "//End of Get " + objclsChildTableListEntity.TableName + " Collection For GRID -----------------\n\n";
            return strQry;
        }

        private string GetChild_CollectionFromGRID_Code(string MasterTable, clsAllTableListEntity objclsChildTableListEntity)
        {
            string strQry = "\n";

            strQry += "//Get Collection From GRID ------------>>>>>>>>>>>>>>>> " + objclsChildTableListEntity.TableName + "\n\n";
            strQry += "function Get" + objclsChildTableListEntity.TableName + "CollectionFromGRID() {\n";
            strQry += "\tvar grid = $(\"#" + objclsChildTableListEntity.TableName + "_Grid\").jqGrid();\n";
            strQry += "\tvar ids = grid.getDataIDs();\n";
            strQry += "\tvar obj" + objclsChildTableListEntity.TableName + "Collection = [];\n";
            strQry += "\tfor (var i = 0; i < ids.length; i++) {\n";
            strQry += "\t\tvar obj" + objclsChildTableListEntity.TableName + " = new GetCollection" + objclsChildTableListEntity.TableName + "ForGrid(null);\n";

            for (int i = 0; i < objclsChildTableListEntity.ColumnEntity.Count; i++)
            {
                strQry += "\t\tobj" + objclsChildTableListEntity.TableName + "." + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + " = grid.getCell(ids[i], \"" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "\");\n";
            }
            strQry += "\t\tobj" + objclsChildTableListEntity.TableName + ".rownumber = i.toString();\n";
            strQry += "\t\tobj" + objclsChildTableListEntity.TableName + ".Tag = grid.getCell(ids[i], \"Tag\");\n";

            strQry += "\t\tobj" + objclsChildTableListEntity.TableName + ".Edit = \"<a style=\\\"text-decoration: underline; color: Blue; cursor: pointer;\\\" onclick=\\\"Edit" + objclsChildTableListEntity.TableName + "Records('\" + i + \"')\\\">Edit</a>\";\n";
            strQry += "\t\tobj" + objclsChildTableListEntity.TableName + ".Delete = \"<a style=\\\"text-decoration: underline; color: Blue; cursor: pointer;\\\" onclick=\\\"Delete" + objclsChildTableListEntity.TableName + "Records('\" + i + \"')\\\">Delete</a>\";\n";

            strQry += "\t\tobj" + objclsChildTableListEntity.TableName + "Collection.push(obj" + objclsChildTableListEntity.TableName + ");\n";
            strQry += "\t}\n";
            strQry += "\treturn obj" + objclsChildTableListEntity.TableName + "Collection;\n";
            strQry += "}\n\n";
            strQry += "//End of Get " + objclsChildTableListEntity.TableName + " Collection From GRID -----------------\n\n";

            return strQry;
        }



        private string GetChild_SetCollection_Code(string MasterTable, clsAllTableListEntity objclsChildTableListEntity)
        {
            string strQry = "\n";
            strQry += "////Set Collection of ------------>>>>>>>>>>>>>>>> " + objclsChildTableListEntity.TableName + "\n\n";
            strQry += "function SetCollection" + objclsChildTableListEntity.TableName + "(obj" + objclsChildTableListEntity.TableName + ") {\n";
            for (int i = 0; i < objclsChildTableListEntity.ColumnEntity.Count; i++)
            {
                if (objclsChildTableListEntity.ColumnEntity[i].IsMasterTable == true)
                {
                    if (objclsChildTableListEntity.ColumnEntity[i].HasReference == true && objclsChildTableListEntity.ColumnEntity[i].IsHidden == false)
                    {
                        string ddl = "#View" + objclsChildTableListEntity.TableName + "_ddl" + objclsChildTableListEntity.ColumnEntity[i].ReferenceTableName + "_" + objclsChildTableListEntity.ColumnEntity[i].RefColumnName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName;

                        //string ddl = "#View" + objclsChildTableListEntity.TableName + "_ddl" + FtblcName[j].ToString().Split(':')[1].ToString() + "_" + FtblcName[j].ToString().Split(':')[2].ToString() + "_" + Cdt.Rows[i]["Column_Name"];
                        strQry += "\t$(\"" + ddl + "\").val(obj" + objclsChildTableListEntity.TableName + "." + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + ");\n";
                    }
                    else
                    {
                        strQry += "\t$(\"#View" + objclsChildTableListEntity.TableName + "_" + objclsChildTableListEntity.TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "\").val(obj" + objclsChildTableListEntity.TableName + "." + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + ");\n";
                    }
                }
            }
            strQry += "}\n\n";
            strQry += "//End of Set Collection-----------------\n\n";
            return strQry;
        }

        private string GetChild_ResetCollection_Code(string MasterTable, clsAllTableListEntity objclsChildTableListEntity)
        {
            string strQry = "\n";


            strQry += "//Reset Funtion of table >>>>>>>>>>>>>>>>>> " + objclsChildTableListEntity.TableName + "\n\n";
            strQry += "function Reset" + objclsChildTableListEntity.TableName + "(obj" + objclsChildTableListEntity.TableName + ") {\n";
            for (int i = 0; i < objclsChildTableListEntity.ColumnEntity.Count; i++)
            {
                if (objclsChildTableListEntity.ColumnEntity[i].IsMasterTable == true)
                {
                    if (objclsChildTableListEntity.ColumnEntity[i].HasReference == true && objclsChildTableListEntity.ColumnEntity[i].IsHidden == false)
                    {
                        string ddl = "#View" + objclsChildTableListEntity.TableName + "_ddl" + objclsChildTableListEntity.ColumnEntity[i].ReferenceTableName + "_" + objclsChildTableListEntity.ColumnEntity[i].RefColumnName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName;
                        strQry += "\t$(\"" + ddl + "\").val(\"0\");\n";
                    }
                    else
                    {
                        strQry += "\t$(\"#View" + objclsChildTableListEntity.TableName + "_" + objclsChildTableListEntity.TableName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "\").val(\"\");\n";
                    }
                }
            }
            strQry += "\t$('#btn" + objclsChildTableListEntity.TableName + "AddItem').val('Add Item');\n";
            strQry += "\tSys.Mvc.FormContext.validateGroup('frm_" + objclsChildTableListEntity.TableName + "', '" + objclsChildTableListEntity.TableName + "', false);\n";
            strQry += "}\n\n";
            strQry += "//End of Reset Funtion ----------------------\n\n";

            return strQry;
        }
    }
}

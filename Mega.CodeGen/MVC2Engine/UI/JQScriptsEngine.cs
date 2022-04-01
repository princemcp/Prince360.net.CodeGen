using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Mega.CodeGen.MVC2Engine
{
    public class JQScriptsEngine
    {
        public bool JQScriptsEngineMethod(IList<clsTableRefEntity> objclsTableRefEntityList, IList<clsColumnEntity> objclsColumnEntityList, string nameSpace, string tableName, string path, string DataBaseName)
        {
            path = path + "\\JQScripts";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\" + tableName + ".js";

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
            strQry += "///////////////////////////////////////////////////////////////////////////////\n";
            strQry += "//      Author      : Maxima Prince              \n";
            strQry += "//      Date        : " + DateTime.Now + "     \n";
            strQry += "//      File name   : " + tableName + ".js     \n";
            strQry += "///////////////////////////////////////////////////////////////////////////////\n\n";

            strQry += "\nfunction LoadForm" + tableName + "_ViewPage() {\n";
            strQry += "\ttry {\n";
            strQry += "\t\tLoad" + tableName + "Grid();\n";
            //strQry += "\t\tDeleteEvent();\n";
            strQry += "\t}\n";
            strQry += "\tcatch (e) {\n";
            strQry += "\t\tHandleMessage(e);\n";
            strQry += "\t}\n";
            strQry += "\tfinally { }\n";
            strQry += "}\n\n";

            strQry += "function LoadForm" + tableName + "_EditPage() {\n";
            strQry += "\ttry {\n";
            strQry += "\t\tEditEvent();\n";
            //strQry += "\t\tSetDateTempletWithTextBox();\n";
            strQry += "\t}\n";
            strQry += "\tcatch (e) {\n";
            strQry += "\t\tHandleMessage(e);\n";
            strQry += "\t}\n";
            strQry += "\tfinally { }\n";
            strQry += "}\n\n";

            strQry += "function LoadForm" + tableName + "_CreatePage() {\n";
            strQry += "\ttry {\n";
            strQry += "\t\tSaveEvent();\n";
            //strQry += "\t\tSetDateTempletWithTextBox();\n";
            strQry += "\t}\n";
            strQry += "\tcatch (e) {\n";
            strQry += "\t\tHandleMessage(e);\n";
            strQry += "\t}\n";
            strQry += "\tfinally { }\n";
            strQry += "}\n\n";

            strQry += "function Load" + tableName + "Grid() {\n";
            strQry += "\t$(\"#" + tableName + "_Grid\").jqGrid({\n";
            strQry += "\t\turl: \"/" + tableName + "/Get" + tableName + "BySP\",\n";
            strQry += "\t\tdatatype: \"json\",\n";
            strQry += "\t\tmtype: 'POST',\n";
            strQry += "\t\tcolNames: [";
            for (int i = 0; i < objclsColumnEntityList.Count; i++)
            {
                strQry += " '" + objclsColumnEntityList[i].Lable + "', \n\t\t\t\t ";
            }
            strQry += " '', '' ";
            strQry += "],\n";
            strQry += "\t\tcolModel: [\n";

            for (int i = 0; i < objclsColumnEntityList.Count; i++)
            {
                if (i == 0)
                {
                    strQry += "\t\t\t{ name: '" + objclsColumnEntityList[i].ColumnAliasName + "', index: '" + objclsColumnEntityList[i].ColumnAliasName + "', key: true, hidden: true },\n";
                }
                else if (i > 0)
                {
                    if (objclsColumnEntityList[i].HasReference == true || objclsColumnEntityList[i].IsHidden == true)
                    {
                        strQry += "\t\t\t{ name: '" + objclsColumnEntityList[i].ColumnAliasName + "', index: '" + objclsColumnEntityList[i].ColumnAliasName + "', width: 150, align: 'left', hidden: true },\n";
                    }
                    else
                    {
                        strQry += "\t\t\t{ name: '" + objclsColumnEntityList[i].ColumnAliasName + "', index: '" + objclsColumnEntityList[i].ColumnAliasName + "', width: 150, align: 'left', hidden: false },\n";
                    }
                }
            }
            strQry += "\t\t\t{ name: '', index: 'Edit', width: 30, align: 'center'},\n";
            strQry += "\t\t\t{ name: '', index: 'Delete', width: 30, align: 'center'}\n";
            strQry += "\t\t\t],\n";
            strQry += "\t\tpager: $('#" + tableName + "_Pager'),                        //pager div\n";
            strQry += "\t\trowNum: 10,                                //default page size\n";
            strQry += "\t\trowList: [10, 20, 30, 40, 50],                 //option of page size\n";
            strQry += "\t\twidth: '420',                              //grid width\n";
            strQry += "\t\theight: \"100%\",                          //grid height\n";
            strQry += "\t\tsortname: '" + objclsColumnEntityList[1].ColumnAliasName + "',                     //default sort column name\n";
            strQry += "\t\tsortorder: \"desc\",                       //sorting order\n";
            strQry += "\t\tviewrecords: true,                         //by default records show?\n";
            strQry += "\t\tmultiselect: false,                        //checkbox list\n";
            strQry += "\t\timgpath: '/Content/themes/steel/images',   //set icon in grid\n";
            strQry += "\t\tcaption: \"" + tableName + " List\",                    //grid title\n";
            strQry += "\t\tloadComplete: function () { },\n";
            strQry += "\t\tloadError: function (xhr, status, str) {   //function calling when grid load exception occured \n";
            strQry += "\t\t\tHandleMessage(xhr.msg);           //set div text by error message\n";
            strQry += "\t\t},\n";
            strQry += "\t\terrorCell: function () {                   //function calling when cell exception occured\n";
            strQry += "\t\t\tHandleMessage('An error has occurred while processing your request.');\n";
            strQry += "\t\t}\n";
            strQry += "\t});\n";
            strQry += "}\n\n";

            strQry += "function ReLoad" + tableName + "Grid() {\n";
            strQry += "\tvar url = \"/" + tableName + "/Get" + tableName + "BySP\";\n";
            strQry += "\t$(\"#" + tableName + "_Grid\").setGridParam({ url: url });\n";
            strQry += "\t$(\"#" + tableName + "_Grid\").trigger(\"reloadGrid\");\n";
            strQry += "}\n\n";

            strQry += "function SaveEvent() {\n";
            strQry += "\t$(\"#btnSave\").click(function () {\n";
            strQry += "\t\tif (Sys.Mvc.FormContext.validateGroup('frm_" + tableName + "', '" + tableName + "', true)) {\n";
            strQry += "\t\t\tSaveCollection" + tableName + "(\"/" + tableName + "/Create\");\n";
            strQry += "\t\t}\n";
            strQry += "\t});\n";
            strQry += "}\n\n";

            strQry += "function EditEvent() {\n";
            strQry += "\t$(\"#btnEdit\").click(function () {\n";
            strQry += "\t\tif (Sys.Mvc.FormContext.validateGroup('frm_" + tableName + "', '" + tableName + "', true)) {\n";
            strQry += "\t\t\tSaveCollection" + tableName + "(\"/" + tableName + "/Edit\");\n";
            strQry += "\t\t}\n";
            strQry += "\t});\n";
            strQry += "}\n\n";

            //strQry += "function DeleteEvent() {\n";
            //strQry += "\t$(\"#btnDelete\").click(function () {\n";
            //strQry += "\t\tvar rowKey = SelectedID();\n";
            //strQry += "\t\tvar agree = confirm(\"Are you sure you want to delete selected row?\");\n";
            //strQry += "\t\tif (agree) {\n";
            //strQry += "\t\t\tfor (var i = 0; i < rowKey.length; i++) {\n";
            //strQry += "\t\t\t\tDeleteRecords(rowKey[i]);\n";
            //strQry += "\t\t\t}\n";
            //strQry += "\t\tReLoad" + tableName + "Grid();\n";
            //strQry += "\t\t}\n";
            //strQry += "\t});\n";
            //strQry += "}\n\n";

            

            //strQry += "function SelectedID() {\n";
            //strQry += "\tvar grid = $(\"#" + tableName + "_Grid\").jqGrid();\n";
            //strQry += "\tvar rowKey = grid.getGridParam(\"selarrrow\")\n";
            //strQry += "\treturn rowKey;\n";
            //strQry += "\t}\n\n";

            strQry += "\nfunction DeleteRecords(" + objclsColumnEntityList[0].ColumnAliasName + ") {\n";
            strQry += "\tvar agree = confirm(\"Are you sure you want to delete?\");\n";
            strQry += "\tif (agree) {\n";
            strQry += "\t\t$.post(\"/" + tableName + "/Delete\", { " + objclsColumnEntityList[0].ColumnAliasName + ": " + objclsColumnEntityList[0].ColumnAliasName + " }, function (data) { }, \"json\");\n";
            strQry += "\t\tReLoad" + tableName + "Grid();\n";
            strQry += "\t}\n";
            strQry += "}\n\n";



            strQry += "function GetCollection" + tableName + "() {\n";
            for (int i = 0; i < objclsColumnEntityList.Count; i++)
            {
                if (objclsColumnEntityList[i].IsMasterTable == true)
                {
                    if (objclsColumnEntityList[i].ColumnIsNull == false)
                    {
                        if (objclsColumnEntityList[i].HasReference == true && objclsColumnEntityList[i].IsHidden == false)
                        {
                            string ddl = "#ddl" + objclsColumnEntityList[i].ReferenceTableName + "_" + objclsColumnEntityList[i].RefColumnName + "_" + objclsColumnEntityList[i].ColumnAliasName;
                            strQry += "\tthis." + objclsColumnEntityList[i].ColumnAliasName + " = $(\"" + ddl + "\").val() == null || $(\"" + ddl + "\").val() ==\"\" ? \"0\" : $(\"" + ddl + "\").val();\n";
                        }
                        else if (objclsColumnEntityList[i].IsHidden == true)
                        {
                            //string ddl = "#hdd" + objclsColumnEntityList[i].ReferenceTableName + "_" + objclsColumnEntityList[i].RefColumnName + "_" + objclsColumnEntityList[i].ColumnAliasName;
                            //if (objclsColumnEntityList[i].ReferenceTableName == "")
                            //{
                            //string ddl = "#hdd_" + objclsColumnEntityList[i].TableName + "_" + objclsColumnEntityList[i].ColumnAliasName;
                            //}
                            string ddl = "#" + objclsColumnEntityList[i].TableName + "_" + objclsColumnEntityList[i].ColumnAliasName;
                            if (objclsColumnEntityList[i].ColumnDotNetType == "string")
                            {
                                strQry += "\tthis." + objclsColumnEntityList[i].ColumnAliasName + " = $(\"" + ddl + "\").val() == null || $(\"" + ddl + "\").val() ==\"\" ? \"\" : $(\"" + ddl + "\").val();\n";
                            }
                            else if (objclsColumnEntityList[i].ColumnDotNetType == "DateTime")
                            {
                                strQry += "\tthis." + objclsColumnEntityList[i].ColumnAliasName + " = $(\"" + ddl + "\").val() == null || $(\"" + ddl + "\").val() ==\"\" ? GetCurrentDate() : $(\"" + ddl + "\").val();\n";
                            }
                            else
                            {
                                strQry += "\tthis." + objclsColumnEntityList[i].ColumnAliasName + " = $(\"" + ddl + "\").val() == null || $(\"" + ddl + "\").val() ==\"\" ? \"0\" : $(\"" + ddl + "\").val();\n";
                            }
                        }
                        else if (objclsColumnEntityList[i].HasReference == false && objclsColumnEntityList[i].IsHidden == false)
                        {
                            if (objclsColumnEntityList[i].ColumnDotNetType == "string")
                            {
                                strQry += "\tthis." + objclsColumnEntityList[i].ColumnAliasName + " = $(\"#" + objclsColumnEntityList[i].TableName + "_" + objclsColumnEntityList[i].ColumnAliasName + "\").val() == null || $(\"#" + objclsColumnEntityList[i].TableName + "_" + objclsColumnEntityList[i].ColumnAliasName + "\").val() == \"\" ? \"\" : $(\"#" + objclsColumnEntityList[i].TableName + "_" + objclsColumnEntityList[i].ColumnAliasName + "\").val();\n";
                            }
                            else if (objclsColumnEntityList[i].ColumnDotNetType == "DateTime")
                            {
                                strQry += "\tthis." + objclsColumnEntityList[i].ColumnAliasName + " = $(\"#" + objclsColumnEntityList[i].TableName + "_" + objclsColumnEntityList[i].ColumnAliasName + "\").val() == null || $(\"#" + objclsColumnEntityList[i].TableName + "_" + objclsColumnEntityList[i].ColumnAliasName + "\").val() == \"\" ? GetCurrentDate() : $(\"#" + objclsColumnEntityList[i].TableName + "_" + objclsColumnEntityList[i].ColumnAliasName + "\").val();\n";
                            }
                            else
                            {
                                strQry += "\tthis." + objclsColumnEntityList[i].ColumnAliasName + " = $(\"#" + objclsColumnEntityList[i].TableName + "_" + objclsColumnEntityList[i].ColumnAliasName + "\").val() == null || $(\"#" + objclsColumnEntityList[i].TableName + "_" + objclsColumnEntityList[i].ColumnAliasName + "\").val() == \"\" ? \"0\" : $(\"#" + objclsColumnEntityList[i].TableName + "_" + objclsColumnEntityList[i].ColumnAliasName + "\").val();\n";
                            }
                        }
                    }
                    else
                    {
                        if (objclsColumnEntityList[i].HasReference == true && objclsColumnEntityList[i].IsHidden == false)
                        {
                            string ddl = "#ddl" + objclsColumnEntityList[i].ReferenceTableName + "_" + objclsColumnEntityList[i].RefColumnName + "_" + objclsColumnEntityList[i].ColumnAliasName;
                            strQry += "\tthis." + objclsColumnEntityList[i].ColumnAliasName + " = $(\"" + ddl + "\").val() == null || $(\"" + ddl + "\").val() ==\"0\" ? \"\" : $(\"" + ddl + "\").val();\n";
                        }
                        else if (objclsColumnEntityList[i].IsHidden == true)
                        {
                            //string ddl = "#hdd" + objclsColumnEntityList[i].ReferenceTableName + "_" + objclsColumnEntityList[i].RefColumnName + "_" + objclsColumnEntityList[i].ColumnAliasName;
                            //if (objclsColumnEntityList[i].ReferenceTableName == "")
                            //{
                            //    ddl = "#hdd_" + objclsColumnEntityList[i].TableName + "_" + objclsColumnEntityList[i].ColumnAliasName;
                            //}
                            string ddl = "#" + objclsColumnEntityList[i].TableName + "_" + objclsColumnEntityList[i].ColumnAliasName;
                            strQry += "\tthis." + objclsColumnEntityList[i].ColumnAliasName + " = $(\"" + ddl + "\").val() == null || $(\"" + ddl + "\").val() ==\"0\" ? \"\" : $(\"" + ddl + "\").val();\n";
                        }
                        else if (objclsColumnEntityList[i].HasReference == false && objclsColumnEntityList[i].IsHidden == false)
                        {
                            if (objclsColumnEntityList[i].ColumnDotNetType == "string")
                            {
                                strQry += "\tthis." + objclsColumnEntityList[i].ColumnAliasName + " = $(\"#" +  objclsColumnEntityList[i].TableName  + "_" + objclsColumnEntityList[i].ColumnAliasName + "\").val() == null || $(\"#" + objclsColumnEntityList[i].TableName  + "_" +  objclsColumnEntityList[i].ColumnAliasName + "\").val() == \"\" ? \"\" : $(\"#" + objclsColumnEntityList[i].TableName  + "_" +  objclsColumnEntityList[i].ColumnAliasName + "\").val();\n";
                            }
                            else if (objclsColumnEntityList[i].ColumnDotNetType == "DateTime")
                            {
                                strQry += "\tthis." + objclsColumnEntityList[i].ColumnAliasName + " = $(\"#" + objclsColumnEntityList[i].TableName  + "_" + objclsColumnEntityList[i].ColumnAliasName + "\").val() == null || $(\"#" +  objclsColumnEntityList[i].TableName  + "_" + objclsColumnEntityList[i].ColumnAliasName + "\").val() == \"\" ? \"\" : $(\"#" +  objclsColumnEntityList[i].TableName  + "_" + objclsColumnEntityList[i].ColumnAliasName + "\").val();\n";
                            }
                            else
                            {
                                strQry += "\tthis." + objclsColumnEntityList[i].ColumnAliasName + " = $(\"#" + objclsColumnEntityList[i].TableName  + "_" +  objclsColumnEntityList[i].ColumnAliasName + "\").val() == null || $(\"#" +  objclsColumnEntityList[i].TableName  + "_" + objclsColumnEntityList[i].ColumnAliasName + "\").val() == \"0\" ? \"\" : $(\"#" + objclsColumnEntityList[i].TableName  + "_" +  objclsColumnEntityList[i].ColumnAliasName + "\").val();\n";
                            }
                        }

                    }
                }
            }
            strQry += "}\n\n";

            strQry += "function ResetCollection" + tableName + "() {\n";
            for (int i = 0; i < objclsColumnEntityList.Count; i++)
            {
                if (objclsColumnEntityList[i].IsMasterTable == true)
                {
                    if (objclsColumnEntityList[i].HasReference == true && objclsColumnEntityList[i].IsHidden == false)
                    {
                        string ddl = "#ddl" + objclsColumnEntityList[i].ReferenceTableName + "_" + objclsColumnEntityList[i].RefColumnName + "_" + objclsColumnEntityList[i].ColumnAliasName;
                        strQry += "\t$(\"" + ddl + "\").val(\"\");\n";
                    }
                    else if (objclsColumnEntityList[i].IsHidden == true)
                    {
                        string ddl = "#" + objclsColumnEntityList[i].TableName + "_" + objclsColumnEntityList[i].ColumnAliasName;
                        strQry += "\t$(\"" + ddl + "\").val(\"\");\n";
                    }
                    else if (objclsColumnEntityList[i].HasReference == false && objclsColumnEntityList[i].IsHidden == false)
                    {
                        strQry += "\t$(\"#"+objclsColumnEntityList[i].TableName + "_" + objclsColumnEntityList[i].ColumnAliasName+ "\").val(\"\");\n";
                    }
                }
            }
            strQry += "\tSys.Mvc.FormContext.validateGroup('frm_" + tableName + "', '" + tableName + "', false);";
            strQry += "\n}\n\n";



            strQry += "function SaveCollection" + tableName + "(posturl) {\n";
            strQry += "\ttry {\n";

            strQry += "\t\tvar obj" + tableName + " = new GetCollection" + tableName + "();\n";
            strQry += "\t\tvar objSave" + tableName + " = $.toJSON(obj" + tableName + ");           //convert objItemTypes to json type\n";
            strQry += "\t\t$.post(posturl, { jsonData: objSave" + tableName + " }, function (response, status, xhr) {\n";
            strQry += "\t\t}, \"json\");\n";
            strQry += "\t\tResetCollection" + tableName + "();\n";

            strQry += "\t}\n";
            strQry += "\tcatch(e) {\n";
            strQry += "\t\tHandleMessage(e);\n";
            strQry += "\t}\n";
            strQry += "\tfinally { }\n";
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


            //strQry += "\nfunction DeleteCollection" + tableName + "() {\n";
            //strQry += "\tvar " + tableName + "grid = $(\"#" + tableName + "grid\").jqGrid();          //get grid row collection\n";
            //strQry += "\tvar rowKey = " + tableName + "grid.getGridParam(\"selarrrow\")               //selected row key no as array\n";
            //strQry += "\tvar agree = confirm(\"Are you sure you want to delete?\");\n";
            //strQry += "\tif (agree) {\n";
            //strQry += "\t\tfor (var d = 0; d < rowKey.length; d++) {\n";
            //strQry += "\t\t\t$.ajax({\n";
            //strQry += "\t\t\t\turl: \"/" + tableName + "/Delete?" + objclsColumnEntityList[0].ColumnAliasName + "=\" + d." + objclsColumnEntityList[0].ColumnAliasName + ",\n";
            //strQry += "\t\t\t\tdatatype: 'json',\n";
            //strQry += "\t\t\t\tmtype: 'POST',\n";
            //strQry += "\t\t\t\tsuccess: function (data) {  },\n";
            //strQry += "\t\t\t\tError: function (response, status, xhr) {\n";
            //strQry += "\t\t\t\t\tif (status == \"error\") {\n";
            //strQry += "\t\t\t\t\t\talert(xhr.status + \" \" + xhr.statusText);\n";
            //strQry += "\t\t\t\t\t}\n";
            //strQry += "\t\t\t\t}\n";
            //strQry += "\t\t\t});\n\t\t}\n\t}\n";
            //strQry += "\tReLoad" + tableName + "Grid();\n";
            //strQry += "}\n\n";

            strQry += "function HandleMessage(errorMessage) {\n";
            strQry += "\tvar message = \"<div style='color:Red;'>\" + errorMessage + '</div>';\n";
            strQry += "\t$(\"#divMsg\").html(message);\n";
            strQry += "}\n\n";

            strQry += "//Extra Methods ";

            return strQry;
        }
    }
}

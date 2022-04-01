using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Mega.CodeGen.MVC2Engine
{
    public class Edit
    {
        public bool EditMethod(IList<clsTableRefEntity> objclsTableRefEntityList, IList<clsColumnEntity> objclsColumnEntityList, string nameSpace, string tableName, string path, string DataBaseName)
        {
            path = path + "\\View\\" + tableName + "";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\Edit.aspx";

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

            strQry += "<%@ Page Title=\"" + tableName + "\" Language=\"C#\" MasterPageFile=\"~/Views/Shared/Child.Master\" Inherits=\"System.Web.Mvc.ViewPage<" + nameSpace + ".MODEL.View" + tableName + ">\" %>\n";

            strQry += "<%-- /////////////////////////////////////////////////////////////////////////////// --%>\n";
            strQry += "<%-- //      Author      : Maxima Prince                                                   --%>\n";
            strQry += "<%-- //      Date        : " + DateTime.Now + "                                      --%>\n";
            strQry += "<%-- //      File name   : Create.aspx                              --%>\n";
            strQry += "<%-- ////////////////////////////////////////////////////////////////////////////// --%>\n\n";

            strQry += "<asp:Content ID=\"HContent\" ContentPlaceHolderID=\"head\" runat=\"server\">\n";
            strQry += "\n\t<script src=\"../../Scripts/CustomJScript/" + tableName + ".js\" type=\"text/javascript\"></script>\n";
            strQry += "\t<script type=\"text/javascript\">\n";
            strQry += "\t\t$(document).ready(function () {\n";
            strQry += "\t\t\tLoadForm" + tableName + "_EditPage();\n";
            strQry += "\t\t});\n";
            strQry += "\t</script>\n";
            strQry += "</asp:Content>\n";
            strQry += "<asp:Content ID=\"MContent\" ContentPlaceHolderID=\"MainContent\" runat=\"server\">\n";
            strQry += "\t<% Html.EnableClientValidation(); %>\n";
            strQry += "\t<% using (Html.BeginForm(\"" + tableName + "\", \"Create\", FormMethod.Post, new { id = \"frm_" + tableName + "\" })) %>\n";
            strQry += "\t<% { %>\n";
            strQry += "\t<%: Html.AntiForgeryToken()%>\n";
            strQry += "\t<%: Html.ValidationSummary()%>\n";
            strQry += "\t<div class=\"mainbody\">\n";
            strQry += "\t<%: Html.HiddenFor(model => model." + tableName + "." + objclsColumnEntityList[0].ColumnAliasName + ") %>\n";
            for (int i = 1; i < objclsColumnEntityList.Count; i++)
            {
                if (objclsColumnEntityList[i].IsMasterTable == true)
                {
                    if (objclsColumnEntityList[i].IsHidden == true)
                    {
                        strQry += "\t<%: Html.HiddenFor(model => model." + tableName + "." + objclsColumnEntityList[i].ColumnAliasName + ") %>\n";
                    }
                }
            }
            strQry += "\t<table cellpadding=\"0\" cellspacing=\"0\" align=\"center\">\n";
            strQry += "\t\t<tr>\n";
            strQry += "\t\t\t<td>\n";
            strQry += "\t\t\t\t<div class=\"modbox\" style=\"width: 430px;\">\n";
            strQry += "\t\t\t\t\t<b class=\"rnd_modtitle\"><b class=\"rnd1\"></b><b class=\"rnd2\"></b><b class=\"rnd3\"></b></b>\n";
            strQry += "\t\t\t\t\t<div class=\"modtitle\">" + tableName + "</div>\n";
            strQry += "\t\t\t\t\t\t<div class=\"modboxin\">\n";
            strQry += "\t\t\t\t\t\t\t<table cellpadding=\"0\" cellspacing=\"0\" style=\"width: 400px; padding-left: 30px;\">\n";



            for (int i = 1; i < objclsColumnEntityList.Count; i++)
            {

                //strQry += "\t\t\t<label for=\"lbl" + dt.Rows[i]["Column_Name"] + "\">" + dt.Rows[i]["Column_Name"].ToString().Substring(0, dt.Rows[i]["Column_Name"].ToString().Length - 2) + "</label>\n";
                if (objclsColumnEntityList[i].IsMasterTable == true)
                {
                    if (objclsColumnEntityList[i].HasReference == true && objclsColumnEntityList[i].IsHidden == false)
                    {
                        strQry += "\t\t\t\t\t\t\t\t<tr>\n";
                        strQry += "\t\t\t\t\t\t\t\t\t<td height=\"30px\" style=\"font-family: Tahoma; font-size: 11px\">\n";
                        strQry += "\t\t\t\t\t\t\t\t\t\t" + objclsColumnEntityList[i].Lable + " :\n";
                        strQry += "\t\t\t\t\t\t\t\t\t</td>\n";
                        strQry += "\t\t\t\t\t\t\t\t\t<td>\n";
                        string DropDownListName = objclsColumnEntityList[i].ReferenceTableName + "_" + objclsColumnEntityList[i].RefColumnName + "_" + objclsColumnEntityList[i].ColumnAliasName;
                        strQry += "\t\t\t\t\t\t\t\t\t\t<%: Html.DropDownList(\"ddl" + DropDownListName + "\", Model.ddl" + DropDownListName + ", new { @class = \"slctbx\", Style = \"width:100%;\" }) %>\n";
                        strQry += "\t\t\t\t\t\t\t\t\t\t<%: Html.ValidationMessageFor(model => model.ddl" + DropDownListName + ",\" \") %>\n";
                        strQry += "\t\t\t\t\t\t\t\t\t</td>\n";
                        strQry += "\t\t\t\t\t\t\t\t</tr>\n";
                    }
                    if (objclsColumnEntityList[i].HasReference == false && objclsColumnEntityList[i].IsHidden == false)
                    {
                        strQry += "\t\t\t\t\t\t\t\t<tr>\n";
                        strQry += "\t\t\t\t\t\t\t\t\t<td height=\"30px\" style=\"font-family: Tahoma; font-size: 11px\">\n";
                        strQry += "\t\t\t\t\t\t\t\t\t\t" + objclsColumnEntityList[i].ColumnAliasName + " :\n";
                        strQry += "\t\t\t\t\t\t\t\t\t</td>\n";
                        strQry += "\t\t\t\t\t\t\t\t\t<td>\n";

                        strQry += "\t\t\t\t\t\t\t\t\t\t<%: Html.TextBoxFor(model => model." + tableName + "." + objclsColumnEntityList[i].ColumnAliasName + ") %>\n";
                        strQry += "\t\t\t\t\t\t\t\t\t\t<%: Html.ValidationMessageFor(model => model." + tableName + "." + objclsColumnEntityList[i].ColumnAliasName + ",\" \") %>\n";
                        strQry += "\t\t\t\t\t\t\t\t\t</td>\n";
                        strQry += "\t\t\t\t\t\t\t\t</tr>\n";
                    }
                }
            }

            strQry += "\t\t\t\t\t\t</table>\n";
            strQry += "\t\t\t\t\t\t</div>\n";
            strQry += "\t\t\t\t\t</div>\n";
            strQry += "\t\t\t\t</td>\n";
            strQry += "\t\t\t</tr>\n";

            strQry += "\t\t\t<tr>\n";
            strQry += "\t\t\t\t<td>\n";

            strQry += "\t\t\t\t\t<div class=\"modbox\" style=\"width: 430px; text-align:left;\">\n";
            strQry += "\t\t\t\t\t<b class=\"rnd_modtitle\"><b class=\"rnd4\"></b><b class=\"rnd5\"></b><b class=\"rnd6\"></b></b>\n";
            strQry += "\t\t\t\t\t<div class=\"Bmodtitle\"></div>\n";
            strQry += "\t\t\t\t\t<div class=\"modboxin\">\n";
            strQry += "\t\t\t\t\t<div class=\"buttondiv\">\n";
            strQry += "\t\t\t\t\t\t<input type=\"button\" value=\"Back\" class=\"back\" onclick=\"location.href='<%: Url.Action(\"Index\", \"" + tableName + "\") %>'\" />\n";
            strQry += "\t\t\t\t\t\t<input type=\"button\" class=\"save\" id=\"btnEdit\" value=\"Save\" />\n";
            strQry += "\t\t\t\t\t\t<input type=\"reset\" class=\"rest\" id=\"btnReset\" value=\"Reset\" />\n";
            strQry += "\t\t\t\t\t</div>\n";
            strQry += "\t\t\t\t\t</div>\n";
            strQry += "\t\t\t\t\t<b class=\"rnd_modboxin\"><b class=\"rnd3\"></b><b class=\"rnd2\"></b><b class=\"rnd1\"></b></b>\n";
            strQry += "\t\t\t\t\t</div>\n";
            strQry += "\t\t\t\t</td>\n";
            strQry += "\t\t\t</tr>\n";
            strQry += "\t\t</table>\n";
            strQry += "\t</div>\n";
            strQry += "\t<% } %>\n";
            strQry += "</asp:Content>\n";

            return strQry;
        }
    }
}

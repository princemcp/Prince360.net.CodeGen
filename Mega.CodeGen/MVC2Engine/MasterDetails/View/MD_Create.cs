using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Mega.CodeGen.MVC2Engine
{
    class MD_Create
    {
        public bool MD_CreateMethod(string nameSpace, ListBox MasterTableList, ListBox ChildTableList, string path)
        {
            path = path + "\\MasterDetail\\View";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\Create.aspx";

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

            strQry += "<%@ Page Title=\"" + objclsMasterTableListEntity[0].TableName + "MasterDetails\" Language=\"C#\" MasterPageFile=\"~/Views/Shared/Child.Master\" Inherits=\"System.Web.Mvc.ViewPage<" + nameSpace + ".MODEL.View" + objclsMasterTableListEntity[0].TableName + "All>\" %>\n";

            strQry += "<%-- /////////////////////////////////////////////////////////////////////////////// --%>\n";
            strQry += "<%-- //      Author      : Maxima Prince                                                   --%>\n";
            strQry += "<%-- //      Date        : " + DateTime.Now + "                                      --%>\n";
            strQry += "<%-- //      File name   : Create.aspx                              --%>\n";
            strQry += "<%-- ////////////////////////////////////////////////////////////////////////////// --%>\n\n";
            strQry += "<asp:Content ID=\"HContent\" ContentPlaceHolderID=\"head\" runat=\"server\">\n";
            strQry += "\t<script src=\"../../Scripts/CustomJScript/" + objclsMasterTableListEntity[0].TableName + ".js\" type=\"text/javascript\"></script>\n";
            strQry += "\t<script type=\"text/javascript\">\n";
            strQry += "\t\t$(document).ready(function () {\n";
            strQry += "\t\t\tLoadForm" + objclsMasterTableListEntity[0].TableName + "MasterDetails_CreatePage();\n";
            strQry += "\t\t});\n";
            strQry += "\t</script>\n";
            strQry += "</asp:Content>\n";
            strQry += "<asp:Content ID=\"MContent\" ContentPlaceHolderID=\"MainContent\" runat=\"server\">\n";
            strQry += "\t<% Html.EnableClientValidation(); %>\n";
            strQry += "\t<% using (Html.BeginForm(\"" + objclsMasterTableListEntity[0].TableName + "\", \"Create\", FormMethod.Post, new { id = \"frm_" + objclsMasterTableListEntity[0].TableName + "\" })) %>\n";
            strQry += "\t<% { %>\n";
            strQry += "\t<%: Html.AntiForgeryToken()%>\n";
            strQry += "\t<%: Html.ValidationSummary()%>\n";
            strQry += "\t<div id=\"divTitle\" class=\"group_box_head\">\n";
            strQry += "\t\tCreate " + objclsMasterTableListEntity[0].TableName + " Master Details\n";
            strQry += "\t</div>\n";
            strQry += "\t<div id=\"divCombo\" class=\"group_box_body\">\n";
            strQry += "\t\t<%: Html.HiddenFor(model => model.View" + objclsMasterTableListEntity[0].TableName + "." + objclsMasterTableListEntity[0].TableName + "." + objclsMasterTableListEntity[0].ColumnEntity[0].ColumnAliasName + ") %>\n";

            for (int i = 1; i < objclsMasterTableListEntity[0].ColumnEntity.Count; i++)
            {
                if (objclsMasterTableListEntity[0].ColumnEntity[i].IsMasterTable == true)
                {
                    if (objclsMasterTableListEntity[0].ColumnEntity[i].IsHidden == true)
                    {
                        strQry += "\t\t<%: Html.HiddenFor(model => model.View" + objclsMasterTableListEntity[0].TableName + "." + objclsMasterTableListEntity[0].TableName + "." + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + ") %>\n";
                    }
                }
            }

            foreach (clsAllTableListEntity List in objclsChildTableListEntity)
            {
                strQry += "\t\t<%: Html.HiddenFor(model => model.View" + List.ColumnEntity[0].TableName + "." + List.ColumnEntity[0].TableName + "." + List.ColumnEntity[0].ColumnAliasName + ") %>\n";
                for (int i = 1; i < List.ColumnEntity.Count; i++)
                {
                    if (List.ColumnEntity[i].IsHidden == true)
                    {
                        strQry += "\t\t<%: Html.HiddenFor(model => model.View" + List.ColumnEntity[i].TableName + "." + List.ColumnEntity[i].TableName + "." + List.ColumnEntity[i].ColumnAliasName + ") %>\n";
                    }
                }
            }

            strQry += "\t\t<table width=\"800px;\">\n";
            strQry += "\t\t\t<tr>\n";


            int d = objclsMasterTableListEntity[0].ColumnEntity.Count / 2 + 1;
            //Breark
            strQry += "\t\t\t\t<td>\n";
            strQry += "\t\t\t\t\t<fieldset style=\"width: 400px;\">\n";
            strQry += "\t\t\t\t\t\t<table width=\"400px;\">\n";
            for (int i = 1; i < d; i++)
            {
                if (objclsMasterTableListEntity[0].ColumnEntity[i].IsMasterTable == true)
                {
                    if (objclsMasterTableListEntity[0].ColumnEntity[i].IsHidden == false)
                    {
                        strQry += "\t\t\t\t\t\t\t<tr>\n";
                        strQry += "\t\t\t\t\t\t\t\t<td style=\"width: 120px;\">\n";
                        strQry += "\t\t\t\t\t\t\t\t\t" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "\n";
                        strQry += "\t\t\t\t\t\t\t\t</td>\n";
                        strQry += "\t\t\t\t\t\t\t\t<td style=\"width: 280px;\">\n";

                        if (objclsMasterTableListEntity[0].ColumnEntity[i].HasReference == true && objclsMasterTableListEntity[0].ColumnEntity[i].IsHidden == false)
                        {
                            string DropDownListName = "ddl" + objclsMasterTableListEntity[0].ColumnEntity[i].ReferenceTableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].RefColumnName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "";
                            strQry += "\t\t\t\t\t\t\t\t\t<%: Html.DropDownList(\"View" + objclsMasterTableListEntity[0].TableName + "." + DropDownListName + "\", Model.View" + objclsMasterTableListEntity[0].TableName + "." + DropDownListName + ", new { @class = \"slctbx\", Style = \"width:100%;\" }) %>\n";
                            strQry += "\t\t\t\t\t\t\t\t\t<%: Html.ValidationMessageFor(model => model.View" + objclsMasterTableListEntity[0].TableName + "." + DropDownListName + ",\" \") %>\n";
                        }
                        else if (objclsMasterTableListEntity[0].ColumnEntity[i].HasReference == false && objclsMasterTableListEntity[0].ColumnEntity[i].IsHidden == false)
                        {
                            strQry += "\t\t\t\t\t\t\t\t\t<%: Html.TextBoxFor(model => model.View" + objclsMasterTableListEntity[0].TableName + "." + objclsMasterTableListEntity[0].TableName + "." + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + ", new { @class = \"txt\", style = \"width:100%;\" })%>\n";
                            strQry += "\t\t\t\t\t\t\t\t\t<%: Html.ValidationMessageFor(model => model.View" + objclsMasterTableListEntity[0].TableName + "." + objclsMasterTableListEntity[0].TableName + "." + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + ", \" \")%>\n";
                        }
                        strQry += "\t\t\t\t\t\t\t\t</td>\n";
                        strQry += "\t\t\t\t\t\t\t</tr>\n";
                    }
                }
            }
            strQry += "\t\t\t\t\t\t</table>\n";
            strQry += "\t\t\t\t\t</fieldset>\n";
            strQry += "\t\t\t\t</td>\n";

            strQry += "\t\t\t\t<td>\n";
            strQry += "\t\t\t\t\t<fieldset style=\"width: 400px;\">\n";
            strQry += "\t\t\t\t\t\t<table width=\"400px;\">\n";
            for (int i = d; i <= objclsMasterTableListEntity[0].ColumnEntity.Count - 1; i++)
            {
                if (objclsMasterTableListEntity[0].ColumnEntity[i].IsMasterTable == true)
                {
                    if (objclsMasterTableListEntity[0].ColumnEntity[i].IsHidden == false)
                    {
                        strQry += "\t\t\t\t\t\t\t<tr>\n";
                        strQry += "\t\t\t\t\t\t\t\t<td style=\"width: 120px;\">\n";
                        strQry += "\t\t\t\t\t\t\t\t\t" + objclsMasterTableListEntity[0].ColumnEntity[i].Lable + "\n";
                        strQry += "\t\t\t\t\t\t\t\t</td>\n";
                        strQry += "\t\t\t\t\t\t\t\t<td style=\"width: 280px;\">\n";

                        if (objclsMasterTableListEntity[0].ColumnEntity[i].HasReference == true && objclsMasterTableListEntity[0].ColumnEntity[i].IsHidden == false)
                        {
                            string DropDownListName = "ddl" + objclsMasterTableListEntity[0].ColumnEntity[i].ReferenceTableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].RefColumnName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "";
                            strQry += "\t\t\t\t<%: Html.DropDownList(\"View" + objclsMasterTableListEntity[0].TableName + "." + DropDownListName + "\", Model.View" + objclsMasterTableListEntity[0].TableName + "." + DropDownListName + ", new { @class = \"slctbx\", Style = \"width:100%;\" }) %>\n";
                            strQry += "\t\t\t\t<%: Html.ValidationMessageFor(model => model.View" + objclsMasterTableListEntity[0].TableName + "." + DropDownListName + ",\" \") %>\n";
                        }
                        else if (objclsMasterTableListEntity[0].ColumnEntity[i].HasReference == false && objclsMasterTableListEntity[0].ColumnEntity[i].IsHidden == false)
                        {

                            strQry += "\t\t\t\t\t\t\t\t\t<%: Html.TextBoxFor(model => model.View" + objclsMasterTableListEntity[0].TableName + "." + objclsMasterTableListEntity[0].TableName + "." + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + ", new { @class = \"txt\", style = \"width:100%;\" })%>\n";
                            strQry += "\t\t\t\t\t\t\t\t\t<%: Html.ValidationMessageFor(model => model.View" + objclsMasterTableListEntity[0].TableName + "." + objclsMasterTableListEntity[0].TableName + "." + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + ", \" \")%>\n";

                        }

                        strQry += "\t\t\t\t\t\t\t\t</td>\n";
                        strQry += "\t\t\t\t\t\t\t</tr>\n";
                    }
                }
            }
            strQry += "\t\t\t\t\t\t</table>\n";
            strQry += "\t\t\t\t\t</fieldset>\n";
            strQry += "\t\t\t\t</td>\n";

            //End Break

            strQry += "\t\t\t</tr>\n";
            strQry += "\t\t</table>\n";
            strQry += "\t\t<br />\n";
            strQry += "\t\t<br />\n";
            strQry += "\t</div>\n";


            foreach (clsAllTableListEntity List in objclsChildTableListEntity)
            {
                strQry += GetChildCode(objclsMasterTableListEntity[0].TableName, List);
            }

            strQry += "\t<div>\n";
            strQry += "\t\t<%:Html.ActionLink(\"Back\", \"Index\", \"" + objclsMasterTableListEntity[0].TableName + "\")%>\n";
            strQry += "\t\t<input type=\"button\" id=\"btnSave\" value=\"Save\" />\n";
            strQry += "\t\t<input type=\"button\" id=\"btnReset\" value=\"Reset\" />\n";
            strQry += "\t</div>\n";
            strQry += "\t<% } %>\n";
            strQry += "</asp:Content>\n";
            return strQry;
        }

        private string GetChildCode(string MasterTable, clsAllTableListEntity objclsChildTableListEntity)
        {
            string strQry = "";

            strQry += "\t<div class=\"modbox\" style=\"width: 1500px;\">\n";
            strQry += "\t\t<b class=\"rnd_modtitle\"><b class=\"rnd1\"></b><b class=\"rnd2\"></b><b class=\"rnd3\"></b></b>\n";
            strQry += "\t\t<div class=\"modtitle\">\n";
            strQry += "\t\t" + objclsChildTableListEntity.TableName + "\n";
            strQry += "\t\t</div>\n";
            strQry += "\t\t<table cellpadding=\"0\" cellspacing=\"0\">\n";
            strQry += "\t\t\t<tr>\n";
            for (int i = 1; i <= objclsChildTableListEntity.ColumnEntity.Count - 1; i++)
            {
                if (objclsChildTableListEntity.ColumnEntity[i].IsMasterTable == true)
                {
                    if (objclsChildTableListEntity.ColumnEntity[i].IsHidden == false)
                    {
                        strQry += "\t\t\t\t<td style=\"width: 100px;\" colspan=\"2\">\n";
                        //strQry += "\t\t\t\t<span id=\"lbl" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "\">" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "</span>\n";
                        strQry += "\t\t\t\t\t<span>" + objclsChildTableListEntity.ColumnEntity[i].Lable + "</span>\n";
                        strQry += "\t\t\t\t</td>\n";
                    }
                }
            }
            strQry += "\t\t\t\t<td style=\"width: 100px;\" colspan=\"2\">\n";
            strQry += "\t\t\t\t</td>\n";
            strQry += "\t\t\t</tr>\n";
            strQry += "\t\t\t<tr>\n";
            for (int i = 1; i <= objclsChildTableListEntity.ColumnEntity.Count - 1; i++)
            {
                if (objclsChildTableListEntity.ColumnEntity[i].IsMasterTable == true)
                {
                    if (objclsChildTableListEntity.ColumnEntity[i].IsHidden == false)
                    {
                        if (objclsChildTableListEntity.ColumnEntity[i].HasReference == true && objclsChildTableListEntity.ColumnEntity[i].IsHidden == false)
                        {
                            strQry += "\t\t\t\t<td style=\"width: 90px;\">\n";
                            string DropDownListName = "ddl" + objclsChildTableListEntity.ColumnEntity[i].ReferenceTableName + "_" + objclsChildTableListEntity.ColumnEntity[i].RefColumnName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "";
                            strQry += "\t\t\t\t\t<%: Html.DropDownList(\"View" + objclsChildTableListEntity.TableName + "." + DropDownListName + "\", Model.View" + objclsChildTableListEntity.TableName + "." + DropDownListName + ", new { @class = \"slctbxWithGrid\", Style = \"width:100%;\" }) %>\n";
                            strQry += "\t\t\t\t</td>\n";
                            strQry += "\t\t\t\t<td style=\"width: 10px;\">\n";
                            strQry += "\t\t\t\t\t<%: Html.ValidationMessageFor(model => model.View" + objclsChildTableListEntity.TableName + ".ddl" + objclsChildTableListEntity.ColumnEntity[i].ReferenceTableName + "_" + objclsChildTableListEntity.ColumnEntity[i].RefColumnName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + ",\" \") %>\n";
                            strQry += "\t\t\t\t</td>\n";
                        }
                        if (objclsChildTableListEntity.ColumnEntity[i].HasReference == false && objclsChildTableListEntity.ColumnEntity[i].IsHidden == false)
                        {
                            strQry += "\t\t\t\t<td style=\"width: 90px;\">\n";
                            strQry += "\t\t\t\t\t<%: Html.TextBoxFor(model => model.View" + objclsChildTableListEntity.TableName + "." + objclsChildTableListEntity.TableName + "." + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + ", new { @class = \"txt\", style = \"width:100%;\" })%>\n";
                            strQry += "\t\t\t\t</td>\n";
                            strQry += "\t\t\t\t<td style=\"width: 10px;\">\n";
                            strQry += "\t\t\t\t\t<%: Html.ValidationMessageFor(model => model.View" + objclsChildTableListEntity.TableName + "." + objclsChildTableListEntity.TableName + "." + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + ", \" \")%>\n";
                            strQry += "\t\t\t\t</td>\n";
                        }
                    }
                }
            }
            strQry += "\t\t\t\t<td style=\"width: 10px;\">\n";

            strQry += "\t\t\t\t</td>\n";
            strQry += "\t\t\t\t<td style=\"width: 90px;\">\n";
            strQry += "\t\t\t\t\t<input id=\"btn" + objclsChildTableListEntity.TableName + "AddItem\" type=\"button\" value=\"Add Item\" onclick=\"Add" + objclsChildTableListEntity.TableName + "ToGrid()\" />\n";
            strQry += "\t\t\t\t\t<input id=\"btn" + objclsChildTableListEntity.TableName + "Reset\" type=\"button\" value=\"Reset\" onclick=\"Reset" + objclsChildTableListEntity.TableName + "()\" />\n";
            strQry += "\t\t\t\t</td>\n";
            strQry += "\t\t\t</tr>\n";
            strQry += "\t\t</table>\n";
            strQry += "\t\t<div id=\"" + objclsChildTableListEntity.TableName + "\">\n";
            strQry += "\t\t\t<div>\n";
            strQry += "\t\t\t\t<table id=\"" + objclsChildTableListEntity.TableName + "_Grid\" class=\"scroll\" cellpadding=\"0\" cellspacing=\"0\"></table>\n";
            strQry += "\t\t\t\t<div id=\"" + objclsChildTableListEntity.TableName + "_Pager\" class=\"scroll\" style=\"text-align: center;\"></div>\n";
            strQry += "\t\t\t</div>\n";
            strQry += "\t\t</div>\n";
            strQry += "\t</div>\n";
            strQry += "\t<br />\n";
            strQry += "\t<br />\n";

            return strQry;
        }
    }
}

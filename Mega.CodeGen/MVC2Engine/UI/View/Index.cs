using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Mega.CodeGen.MVC2Engine
{
    public class Index
    {
        public bool IndexMethod(IList<clsTableRefEntity> objclsTableRefEntityList, IList<clsColumnEntity> objclsColumnEntityList, string nameSpace, string tableName, string path, string DataBaseName)
        {
             path = path + "\\View\\" + tableName + "";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\Index.aspx";

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
            strQry += "<%-- //      File name   : Index.aspx                              --%>\n";
            strQry += "<%-- ////////////////////////////////////////////////////////////////////////////// --%>\n\n";

            strQry += "<asp:Content ID=\"Content1\" ContentPlaceHolderID=\"MainContent\" runat=\"server\">\n";
            strQry += "\n\t<script src=\"../../Scripts/CustomJScript/" + tableName + ".js\" type=\"text/javascript\"></script>\n";
            strQry += "\t<script type=\"text/javascript\">\n";
            strQry += "\t\t$(document).ready(function () {\n";
            strQry += "\t\t\tLoadForm" + tableName + "_ViewPage();\n";
            strQry += "\t\t});\n";
            strQry += "\t</script>\n";
            strQry += "\t<div class=\"mainbody\">\n";

            strQry += "\t\t<table cellpadding=\"0\" cellspacing=\"0\" align=\"center\">\n";
            strQry += "\t\t\t<tr>\n";
            strQry += "\t\t\t\t<td align=\"right\" valign=\"top\" style=\"text-align:left;\">\n";
            strQry += "\t\t\t\t\t<input type=\"button\" class=\"" + tableName + "\" id=\"btnCreate\" value=\"Add " + tableName + "\" onclick=\"location.href='<%: Url.Action(\"Create\", \"" + tableName + "\") %>'\"/>\n";
            strQry += "\t\t\t\t</td>\n";
            strQry += "\t\t</tr>\n";
            strQry += "\t\t\t<tr>\n";
            strQry += "\t\t\t\t<td align=\"right\" valign=\"top\">\n";
            strQry += "\t\t\t\t</td>\n";
            strQry += "\t\t\t</tr>\n";
            strQry += "\t\t\t<tr>\n";

            strQry += "\t\t\t\t<td align=\"right\" valign=\"top\">\n";
            strQry += "\t\t\t\t\t<div class=\"modbox\" style=\"width: 430px; text-align:left;\">\n";
            strQry += "\t\t\t\t\t<b class=\"rnd_modtitle\"><b class=\"rnd1\"></b><b class=\"rnd2\"></b><b class=\"rnd3\"></b></b>\n";
            strQry += "\t\t\t\t\t<div class=\"modtitle\">\n";
            strQry += "\t\t\t\t\t\t" + tableName + "\n";
            strQry += "\t\t\t\t\t</div>\n";
            strQry += "\t\t\t\t\t<div class=\"modboxin\">\n";
            //strQry += "\t\t\t\t\t</div>\n";

            strQry += "\t\t\t\t\t<div>\n";
            strQry += "\t\t\t\t\t\t<table id=\"" + tableName + "_Grid\" class=\"scroll\" cellpadding=\"0\" cellspacing=\"0\">\n";
            strQry += "\t\t\t\t\t\t</table>\n";
            strQry += "\t\t\t\t\t\t<div id=\"" + tableName + "_Pager\" class=\"scroll\" style=\"text-align: center;\">\n";
            strQry += "\t\t\t\t\t\t</div>\n";
            strQry += "\t\t\t\t\t</div>\n";
            strQry += "\t\t\t\t</div>\n";
            strQry += "\t\t\t\t\t<b class=\"rnd_modboxin\"><b class=\"rnd3\"></b><b class=\"rnd2\"></b><b class=\"rnd1\"></b></b>\n";
            strQry += "\t\t\t\t</div>\n";
            strQry += "\t\t\t\t</td>\n";
            strQry += "\t\t\t</tr>\n";

            strQry += "\t\t\t<tr>\n";
            strQry += "\t\t\t\t<td align=\"right\" valign=\"top\"></td>\n";
            strQry += "\t\t\t</tr>\n";
            strQry += "\t\t</table>\n";

            strQry += "\t</div>\n";
            strQry += "</asp:Content>\n";

            return strQry;
        }
    }
}

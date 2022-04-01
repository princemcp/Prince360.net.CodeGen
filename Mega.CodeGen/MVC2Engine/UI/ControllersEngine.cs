using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Mega.CodeGen.MVC2Engine
{
    public class ControllersEngine
    {
        public bool ControllersEngineMethod(IList<clsTableRefEntity> objclsTableRefEntityList, IList<clsColumnEntity> objclsColumnEntityList, string nameSpace, string tableName, string path, string DataBaseName)
        {
            path = path + "\\Controllers";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\" + tableName + "Controller.cs";

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
            strQry += "//      File name   : " + tableName + "Controller.cs     \n";
            strQry += "///////////////////////////////////////////////////////////////////////////////\n\n";

            strQry += "using System;\n";
            strQry += "using System.Collections.Generic;\n";
            strQry += "using System.Linq;\n";
            strQry += "using System.Web.Mvc;\n";
            strQry += "using System.Web.Script.Serialization;\n";
            strQry += "using NEXTIT.ERP.COMMON;\n\n";
            strQry += "\nnamespace " + nameSpace + ".WebClient.Controllers\n";
            strQry += "{\n\n";


            strQry += "\t[CompressFilter(Order = 1)]\n";
            strQry += "\tpublic partial class " + tableName + "Controller : BaseController\n";
            strQry += "\t{\n\n";


            strQry += "\t\t#region Global variable Declaration\n\n";
            strQry += "\t\tprivate IDAL.I" + tableName + " obj" + tableName + " = null;\n";
            strQry += "\t\tprivate BO." + tableName + "Entity obj" + tableName + "Entity = null;\n";
            //strQry += "\t\tprivate SessionWrapper _session = new SessionWrapper();\n";
            strQry += "\t\tJavaScriptSerializer jss = new JavaScriptSerializer();\n";
            strQry += "\t\tprivate NEXTIT.KNITDYE.YS.WEBCLIENT.SetupService.SetupServiceClient _iSetUPServiceClient = null;\n\n";
            strQry += "\t\t#endregion\n\n";

            strQry += "\t\t#region Controller\n\n";
            strQry += "\t\tpublic " + tableName + "Controller()\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\tobj" + tableName + " = new BLL.B" + tableName + "();\n";
            strQry += "\t\t\tobj" + tableName + "Entity = new BO." + tableName + "Entity();\n";
            strQry += "\t\t\t_iSetUPServiceClient = new WEBCLIENT.SetupService.SetupServiceClient();\n";
            strQry += "\t\t}\n\n";
            strQry += "\t\t#endregion\n\n";

            strQry += "\t\tpublic ActionResult Index()\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\treturn View();\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\tpublic ActionResult Get" + tableName + "BySP(string sidx, string sord, int page, int Rows)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\tIList<BO." + tableName + "EntityRef> obj" + tableName + "EntityRef = new List<BO." + tableName + "EntityRef>();\n";
            strQry += "\t\t\tBO." + tableName + "EntityRef objEnt = new BO." + tableName + "EntityRef();\n";
            strQry += "\t\t\tobjEnt.QryOption = 2;\n";
            strQry += "\t\t\tobjEnt.PageSize = Rows;\n";
            strQry += "\t\t\tobjEnt.CurrentPage = page;\n";
            strQry += "\t\t\tobjEnt.OrderBy = sidx;\n";
            strQry += "\t\t\tobjEnt.OrderByDirection = sord;\n";

            strQry += "\t\t\t//Collection of Item types form database table " + tableName + "\n";
            strQry += "\t\t\tobj" + tableName + "EntityRef = obj" + tableName + ".Get" + tableName + "SP(objEnt);\n";

            strQry += "\t\t\t//No of total records\n";
            strQry += "\t\t\tint totalRecords =  (int)objEnt.Count;\n";
            strQry += "\t\t\t//Calculate total no of page  \n";
            strQry += "\t\t\tint totalPages = (int)Math.Ceiling((float)totalRecords / (float)Rows);\n";

            strQry += "\t\t\tvar getdata = new\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\ttotal = totalPages,\n";
            strQry += "\t\t\t\tpage,\n";
            strQry += "\t\t\t\trecords = totalRecords,\n";
            strQry += "\t\t\t\trows = (\n";
            strQry += "\t\t\t\t\tfrom p in obj" + tableName + "EntityRef\n";
            strQry += "\t\t\t\t\tselect new\n";
            strQry += "\t\t\t\t\t{\n";




            string array = "";


            for (int i = 0; i < objclsColumnEntityList.Count; i++)
            {
                if (objclsColumnEntityList[i].ColumnDotNetType == "string")
                {
                    array = array + "\t\t\t\t\t\t\tp." + objclsColumnEntityList[i].ColumnAliasName + ",\n";
                }
                else
                {
                    array = array + "\t\t\t\t\t\t\tConvert.ToString(p." + objclsColumnEntityList[i].ColumnAliasName + "),\n";
                }
            }
            strQry += "\t\t\t\t\t\t cell = new string[] { \n" + array;
            strQry += "\t\t\t\t\t\t\tjQueryGridHelper.GenerateEditButton(p." + objclsColumnEntityList[0].ColumnAliasName + ".ToString(), \"Edit\",\"<img id=\\\"img1\\\" title=\\\"Edit\\\" src=\\\"../../Content/images/edit_icon.png\\\" />\"),\n";
            strQry += "\t\t\t\t\t\t\tjQueryGridHelper.GenerateLinkButton(p." + objclsColumnEntityList[0].ColumnAliasName + ".ToString(),\"<img id=\\\"img1\\\" title=\\\"Delete\\\" src=\\\"../../Content/images/delete_icon.png\\\" />\",\"DeleteRecords\") }\n";
            strQry += "\t\t\t\t\t}).ToArray()\n";
            strQry += "\t\t\t};\n";
            strQry += "\t\t\treturn Json(getdata);\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\tpublic ActionResult Create()\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\treturn View(CommonPagePopulate());\n";
            strQry += "\t\t}\n\n";


            strQry += "\t\t[HttpPost]\n";
            strQry += "\t\tpublic ActionResult Create(string jsonData)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tDBQuery dbid = new DBQuery();\n";
            strQry += "\t\t\t\tobj" + tableName + "Entity = jss.Deserialize<BO." + tableName + "Entity>((string)jsonData);\n";

            strQry += "\t\t\t\tobj" + tableName + "Entity.id = _dbid.GetTablekeyID(\"" + tableName + "\", _locationID);\n";
            strQry += "\t\t\t\tobj" + tableName + "Entity.InsertUser = _currentUser;\n";
            strQry += "\t\t\t\tobj" + tableName + "Entity.LocationID = _locationID;\n";
            strQry += "\t\t\t\tobj" + tableName + "Entity.LastUpdate = DateTime.Now;\n";

            strQry += "\t\t\t\tobj" + tableName + ".Insert(obj" + tableName + "Entity);\n";
            strQry += "\t\t\t\treturn RedirectToAction(\"Index\");\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch(Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tthrow ex;\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\tpublic ActionResult Edit(long " + objclsColumnEntityList[0].ColumnAliasName + ")\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\treturn View(EditPagePopulate(" + objclsColumnEntityList[0].ColumnAliasName + "));\n";
            strQry += "\t\t}\n\n";


            strQry += "\t\t[HttpPost]\n";
            strQry += "\t\tpublic ActionResult Edit(string jsonData, FormCollection form)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tobj" + tableName + "Entity = jss.Deserialize<BO." + tableName + "Entity>((string)jsonData);\n";

            strQry += "\t\t\t\tobj" + tableName + "Entity.EditUser = _currentUser;\n";
            strQry += "\t\t\t\tobj" + tableName + "Entity.LastUpdate = DateTime.Now;\n";

            strQry += "\t\t\t\tobj" + tableName + ".Update(obj" + tableName + "Entity." + objclsColumnEntityList[0].ColumnAliasName + ", obj" + tableName + "Entity);\n";
            strQry += "\t\t\t\treturn RedirectToAction(\"Index\");\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch(Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\treturn View();\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\t[HttpPost]\n";
            strQry += "\t\tpublic ActionResult Delete(long " + objclsColumnEntityList[0].ColumnAliasName + ")\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tobj" + tableName + ".Delete(" + objclsColumnEntityList[0].ColumnAliasName + ");\n";
            strQry += "\t\t\t\treturn RedirectToAction(\"Index\");\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch(Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\treturn View();\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\tprivate MODEL.View" + tableName + " EditPagePopulate(long " + objclsColumnEntityList[0].ColumnAliasName + ")\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tMODEL.View" + tableName + " viewData = new MODEL.View" + tableName + "();\n";
            strQry += "\t\t\t\tviewData." + tableName + " = obj" + tableName + ".Get" + tableName + "ByID(id)[0];\n\n";


            for (int j = 0; j < objclsTableRefEntityList.Count; j++)
            {
                if (objclsTableRefEntityList[j].TableName == "SET_CommonElement")
                {
                    strQry += "\t\t\t\tviewData.ddl" + objclsTableRefEntityList[j].TableName + "_" + objclsTableRefEntityList[j].RefColumnName + "_" + objclsTableRefEntityList[j].ThisColumnName + " = DDL.PopulateDropdownList(_iSetUPServiceClient.GetCommonElementByType(0).ToList(), \"" + objclsTableRefEntityList[j].RefColumnName + "\", \"ElementName\", true, viewData." + tableName + "." + objclsTableRefEntityList[j].ThisColumnName + ".ToString()).ToList();\n";
                }
                else
                {
                    strQry += "\t\t\t\tviewData.ddl" + objclsTableRefEntityList[j].TableName + "_" + objclsTableRefEntityList[j].RefColumnName + "_" + objclsTableRefEntityList[j].ThisColumnName + " = DDL.PopulateDropdownList(new BLL.B" + objclsTableRefEntityList[j].TableName + "().Get" + objclsTableRefEntityList[j].TableName + "s().ToList(), \"" + objclsTableRefEntityList[j].RefColumnName + "\", \"" + objclsTableRefEntityList[j].ThisColumnName + "\", true, viewData." + tableName + "." + objclsTableRefEntityList[j].ThisColumnName + ".ToString()).ToList();\n";
                }
            }

            strQry += "\n\t\t\t\treturn viewData;\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch(Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tthrow ex;\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t}\n\n";



            strQry += "\t\tprivate MODEL.View" + tableName + " CommonPagePopulate()\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tMODEL.View" + tableName + " viewData = new MODEL.View" + tableName + "();\n";

            for (int j = 0; j < objclsTableRefEntityList.Count; j++)
            {
                if (objclsTableRefEntityList[j].TableName == "SET_CommonElement")
                {
                    strQry += "\t\t\t\tviewData.ddl" + objclsTableRefEntityList[j].TableName + "_" + objclsTableRefEntityList[j].RefColumnName + "_" + objclsTableRefEntityList[j].ThisColumnName + " = DDL.PopulateDropdownList(_iSetUPServiceClient.GetCommonElementByType(0).ToList(), \"" + objclsTableRefEntityList[j].RefColumnName + "\", \"ElementName\").ToList();\n";
                }
                else
                {
                    strQry += "\t\t\t\tviewData.ddl" + objclsTableRefEntityList[j].TableName + "_" + objclsTableRefEntityList[j].RefColumnName + "_" + objclsTableRefEntityList[j].ThisColumnName + " = DDL.PopulateDropdownList(new BLL.B" + objclsTableRefEntityList[j].TableName + "().Get" + objclsTableRefEntityList[j].TableName + "s().ToList(), \"" + objclsTableRefEntityList[j].RefColumnName + "\", \"" + objclsTableRefEntityList[j].ThisColumnName + "\").ToList();\n";
                }
            }
            strQry += "\t\t\t\treturn viewData;\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch(Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tthrow ex;\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t}\n\n";


            strQry += "\n\t}\n";
            strQry += "}";

            return strQry;
        }
    }
}

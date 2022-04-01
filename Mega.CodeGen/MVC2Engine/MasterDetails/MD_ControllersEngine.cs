using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Mega.CodeGen.MVC2Engine
{
    public class MD_ControllersEngine
    {
        public bool MD_ControllersEngineMethod(string nameSpace, ListBox MasterTableList, ListBox ChildTableList, string path)
        {
            path = path + "\\MasterDetail\\Controllers";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\" + ((clsAllTableListEntity)MasterTableList.Items[0]).TableName + "Controller.cs";

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
            strQry += "//      File name   : " + objclsMasterTableListEntity[0].TableName + "Controller.cs     \n";
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
            strQry += "\tpublic partial class " + objclsMasterTableListEntity[0].TableName + "Controller : BaseController\n";
            strQry += "\t{\n\n";


            strQry += "\t\t#region Global variable Declaration\n\n";
            strQry += "\t\tprivate IDAL.I" + objclsMasterTableListEntity[0].TableName + " obj" + objclsMasterTableListEntity[0].TableName + " = null;\n";
            strQry += "\t\tprivate BO." + objclsMasterTableListEntity[0].TableName + "Entity obj" + objclsMasterTableListEntity[0].TableName + "Entity = null;\n";
            foreach (clsAllTableListEntity List in objclsChildTableListEntity)
            {
                strQry += "\t\tprivate IDAL.I" + List.TableName + " obj" + List.TableName + " = null;\n";
                strQry += "\t\tprivate BO." + List.TableName + "Entity obj" + List.TableName + "Entity = null;\n";
            }
            strQry += "\t\tJavaScriptSerializer jss = new JavaScriptSerializer();\n";
            strQry += "\t\tprivate " + nameSpace + ".WEBCLIENT.SetupService.SetupServiceClient _iSetUPServiceClient = null;\n\n";
            strQry += "\t\t#endregion\n\n";

            strQry += "\t\t#region Constructor\n\n";
            strQry += "\t\tpublic " + objclsMasterTableListEntity[0].TableName + "Controller()\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\tobj" + objclsMasterTableListEntity[0].TableName + " = new BLL.B" + objclsMasterTableListEntity[0].TableName + "();\n";
            strQry += "\t\t\tobj" + objclsMasterTableListEntity[0].TableName + "Entity = new BO." + objclsMasterTableListEntity[0].TableName + "Entity();\n";
            foreach (clsAllTableListEntity List in objclsChildTableListEntity)
            {
                strQry += "\t\t\tobj" + List.TableName + " = new BLL.B" + List.TableName + "();\n";
                strQry += "\t\t\tobj" + List.TableName + "Entity = new BO." + List.TableName + "Entity();\n";
            }
            strQry += "\t\t\t_iSetUPServiceClient = new WEBCLIENT.SetupService.SetupServiceClient();\n";
            strQry += "\t\t}\n\n";
            strQry += "\t\t#endregion\n\n";

            strQry += "\t\tpublic ActionResult Index()\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\treturn View();\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\tpublic ActionResult Get" + objclsMasterTableListEntity[0].TableName + "BySP(string sidx, string sord, int page, int Rows)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\tIList<BO." + objclsMasterTableListEntity[0].TableName + "EntityRef> obj" + objclsMasterTableListEntity[0].TableName + "EntityRef = new List<BO." + objclsMasterTableListEntity[0].TableName + "EntityRef>();\n";
            strQry += "\t\t\tBO." + objclsMasterTableListEntity[0].TableName + "EntityRef obj" + objclsMasterTableListEntity[0].TableName + "Ent = new BO." + objclsMasterTableListEntity[0].TableName + "EntityRef();\n";
            strQry += "\t\t\tobj" + objclsMasterTableListEntity[0].TableName + "Ent.QryOption = 2;\n";
            strQry += "\t\t\tobj" + objclsMasterTableListEntity[0].TableName + "Ent.PageSize = Rows;\n";
            strQry += "\t\t\tobj" + objclsMasterTableListEntity[0].TableName + "Ent.CurrentPage = page;\n";
            strQry += "\t\t\tobj" + objclsMasterTableListEntity[0].TableName + "Ent.OrderBy = sidx;\n";
            strQry += "\t\t\tobj" + objclsMasterTableListEntity[0].TableName + "Ent.OrderByDirection = sord;\n";
            strQry += "\t\t\t//Collection of Item types form database table " + objclsMasterTableListEntity[0].TableName + "\n";
            strQry += "\t\t\tobj" + objclsMasterTableListEntity[0].TableName + "EntityRef = obj" + objclsMasterTableListEntity[0].TableName + ".Get" + objclsMasterTableListEntity[0].TableName + "SP(obj" + objclsMasterTableListEntity[0].TableName + "Ent);\n";

            strQry += "\t\t\t//No of total records\n";
            strQry += "\t\t\tint totalRecords = (int)obj" + objclsMasterTableListEntity[0].TableName + "Ent.Count;\n";
            strQry += "\t\t\t//Calculate total no of page  \n";
            strQry += "\t\t\tint totalPages = (int)Math.Ceiling((float)totalRecords / (float)Rows);\n";

            strQry += "\t\t\tvar getdata = new\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\ttotal = totalPages,\n";
            strQry += "\t\t\t\tpage,\n";
            strQry += "\t\t\t\trecords = totalRecords,\n";
            strQry += "\t\t\t\trows = (\n";
            strQry += "\t\t\t\t\tfrom p in obj" + objclsMasterTableListEntity[0].TableName + "EntityRef\n";
            strQry += "\t\t\t\t\tselect new\n";
            strQry += "\t\t\t\t\t{\n";


            string array = "";

            for (int i = 0; i < objclsMasterTableListEntity[0].ColumnEntity.Count; i++)
            {
                if (objclsMasterTableListEntity[0].ColumnEntity[i].ColumnDotNetType == "string")
                {
                    array = array + "\t\t\t\t\t\t\tp." + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + ",\n";
                }
                else
                {
                    array = array + "\t\t\t\t\t\t\tConvert.ToString(p." + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "),\n";
                }
            }
            strQry += "\t\t\t\t\t\t cell = new string[] { \n" + array;

            strQry += "\t\t\t\t\t\t\tjQueryGridHelper.GenerateEditButton(p." + objclsMasterTableListEntity[0].ColumnEntity[0].ColumnAliasName + ".ToString(),\"Edit\",\"<img id=\\\"imgEdit" + objclsMasterTableListEntity[0].TableName + "\\\"  src=\\\"../../Content/images/edit_icon.png\\\" title=\\\"Edit\\\"/>\"),\n";
            strQry += "\t\t\t\t\t\t\tjQueryGridHelper.GenerateLinkButton(p." + objclsMasterTableListEntity[0].ColumnEntity[0].ColumnAliasName + ".ToString(),\"<img id=\\\"imgDelete" + objclsMasterTableListEntity[0].TableName + "\\\" src=\\\"../../Content/images/delete_icon.png\\\" title=\\\"Delete\\\"/>\",\"DeleteRecords\") }\n";

            strQry += "\t\t\t\t\t\t\t//jQueryGridHelper.GenerateEditButton(p." + objclsMasterTableListEntity[0].ColumnEntity[0].ColumnAliasName + ".ToString(), Convert.ToString(p.Status)== \"0\" ? \"<img id=\\\"imgEdit" + objclsMasterTableListEntity[0].TableName + "\\\" src=\\\"../../Content/images/edit_icon.png\\\" title=\\\"Edit\\\" />\" : \"<img id=\\\"imgEdit" + objclsMasterTableListEntity[0].TableName + "\\\"  title=\\\"Not Editable\\\" src=\\\"../../Content/images/disableEditImg.png\\\" />\", Convert.ToString(p.Status) == \"0\" ? \"#\":\"#\"),\n";
            strQry += "\t\t\t\t\t\t\t//jQueryGridHelper.GenerateLinkButton(p." + objclsMasterTableListEntity[0].ColumnEntity[0].ColumnAliasName + ".ToString(), Convert.ToString(p.Status)== \"0\" ? \"<img id=\\\"imgDelete" + objclsMasterTableListEntity[0].TableName + "\\\" src=\\\"../../Content/images/delete_icon.png\\\" title=\\\"Delete\\\" />\" : \"<img id=\\\"imgDelete" + objclsMasterTableListEntity[0].TableName + "\\\"  title=\\\"Not Deleteable\\\" src=\\\"../../Content/images/disableDeleteImg.png\\\" />\", Convert.ToString(p.Status) == \"0\" ? \"DeleteRecords\":\"#\"),\n";

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
            strQry += "\t\t\t\tobj" + objclsMasterTableListEntity[0].TableName + "Entity = jss.Deserialize<BO." + objclsMasterTableListEntity[0].TableName + "Entity>((string)jsonData);\n";
            //strQry += "\t\t\t\tobj" + objclsMasterTableListEntity[0].TableName + "Entity." + objclsMasterTableListEntity[0].ColumnEntity[0].ColumnAliasName + " = dbid.GetTablekeyID(\"" + objclsMasterTableListEntity[0].TableName + "\", _locationID);\n";
            //strQry += "\t\t\t\tobj" + objclsMasterTableListEntity[0].TableName + "Entity.InsertUser = _insertUserID;\n";
            //strQry += "\t\t\t\tobj" + objclsMasterTableListEntity[0].TableName + "Entity.LocationID = _locationID;\n";
            //strQry += "\t\t\t\tobj" + objclsMasterTableListEntity[0].TableName + "Entity.LastUpdate = DateTime.Now;\n";
            strQry += "\t\t\t\tobj" + objclsMasterTableListEntity[0].TableName + ".Insert(obj" + objclsMasterTableListEntity[0].TableName + "Entity);\n";
            strQry += "\t\t\t\treturn RedirectToAction(\"Index\");\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch(Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tthrow ex;\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\tpublic ActionResult Edit(long " + objclsMasterTableListEntity[0].ColumnEntity[0].ColumnAliasName + ")\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\treturn View(EditPagePopulate(" + objclsMasterTableListEntity[0].ColumnEntity[0].ColumnAliasName + "));\n";
            strQry += "\t\t}\n\n";


            strQry += "\t\t[HttpPost]\n";
            strQry += "\t\tpublic ActionResult Edit(string jsonData, FormCollection form)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tobj" + objclsMasterTableListEntity[0].TableName + "Entity = jss.Deserialize<BO." + objclsMasterTableListEntity[0].TableName + "Entity>((string)jsonData);\n";
            //strQry += "\t\t\t\tobj" + objclsMasterTableListEntity[0].TableName + "Entity.InsertUser = _insertUserID;\n";
            //strQry += "\t\t\t\tobj" + objclsMasterTableListEntity[0].TableName + "Entity.LocationID = _locationID;\n";
            //strQry += "\t\t\t\tobj" + objclsMasterTableListEntity[0].TableName + "Entity.LastUpdate = DateTime.Now;\n";
            strQry += "\t\t\t\tobj" + objclsMasterTableListEntity[0].TableName + ".Update(obj" + objclsMasterTableListEntity[0].TableName + "Entity." + objclsMasterTableListEntity[0].ColumnEntity[0].ColumnAliasName + ", obj" + objclsMasterTableListEntity[0].TableName + "Entity);\n";
            strQry += "\t\t\t\treturn RedirectToAction(\"Index\");\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch(Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\treturn View();\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\t[HttpPost]\n";
            strQry += "\t\tpublic ActionResult Delete(long " + objclsMasterTableListEntity[0].ColumnEntity[0].ColumnAliasName + ")\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tobj" + objclsMasterTableListEntity[0].TableName + ".Delete(" + objclsMasterTableListEntity[0].ColumnEntity[0].ColumnAliasName + ");\n";
            strQry += "\t\t\t\treturn RedirectToAction(\"Index\");\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch(Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\treturn View();\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\t//This function get the information of the master part of the " + objclsMasterTableListEntity[0].TableName + " information and then populate the child table information\n";
            strQry += "\t\tprivate MODEL.View" + objclsMasterTableListEntity[0].TableName + "All CommonPagePopulate()\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tMODEL.View" + objclsMasterTableListEntity[0].TableName + "All viewData = new MODEL.View" + objclsMasterTableListEntity[0].TableName + "All();\n";
            for (int i = 0; i < objclsMasterTableListEntity[0].ColumnEntity.Count; i++)
            {

                if (objclsMasterTableListEntity[0].ColumnEntity[i].HasReference == true && objclsMasterTableListEntity[0].ColumnEntity[i].IsHidden == false)
                {
                    //strQry += "\t\t\t\t//List<BO." + objclsMasterTableListEntity[0].TableName + "Entity> obj" + objclsMasterTableListEntity[0].TableName + " = objSET_Country.GetSET_Countrys().ToList<BO.SET_CountryEntity>();\n";
                    if (objclsMasterTableListEntity[0].ColumnEntity[i].ReferenceTableName == "SET_CommonElement")
                    {
                        strQry += "\t\t\t\tviewData.View" + objclsMasterTableListEntity[0].TableName + ".ddl" + objclsMasterTableListEntity[0].ColumnEntity[i].ReferenceTableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].RefColumnName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + " = DDL.PopulateDropdownList(_iSetUPServiceClient.GetCommonElementByType(0).ToList(), \"" + objclsMasterTableListEntity[0].ColumnEntity[i].RefColumnName + "\", \"ElementName\").ToList();\n";
                    }
                    else
                    {
                        strQry += "\t\t\t\tviewData.View" + objclsMasterTableListEntity[0].TableName + ".ddl" + objclsMasterTableListEntity[0].ColumnEntity[i].ReferenceTableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].RefColumnName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + " = DDL.PopulateDropdownList(new BLL.B" + objclsMasterTableListEntity[0].ColumnEntity[i].ReferenceTableName + "().Get" + objclsMasterTableListEntity[0].ColumnEntity[i].ReferenceTableName + "s().ToList(), \"" + objclsMasterTableListEntity[0].ColumnEntity[i].RefColumnName + "\", \"" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "\").ToList();\n";
                    }
                }
            }
            strQry += "\t\t\t\t//Populate the Child item information\n";
            foreach (clsAllTableListEntity List in objclsChildTableListEntity)
            {
                strQry += "\t\t\t\tCommonPage" + List.TableName + "Populate(viewData);      //Populate the " + List.TableName + " table \n";
            }
            strQry += "\t\t\t\treturn viewData;\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch(Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tthrow ex;\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\tprivate MODEL.View" + objclsMasterTableListEntity[0].TableName + "All EditPagePopulate(long " + objclsMasterTableListEntity[0].ColumnEntity[0].ColumnAliasName + ")\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tMODEL.View" + objclsMasterTableListEntity[0].TableName + "All viewData = new MODEL.View" + objclsMasterTableListEntity[0].TableName + "All();\n";
            strQry += "\t\t\t\tviewData.View" + objclsMasterTableListEntity[0].TableName + "." + objclsMasterTableListEntity[0].TableName + " = obj" + objclsMasterTableListEntity[0].TableName + ".Get" + objclsMasterTableListEntity[0].TableName + "ByID(id)[0];\n\n";
            for (int i = 0; i < objclsMasterTableListEntity[0].ColumnEntity.Count; i++)
            {

                if (objclsMasterTableListEntity[0].ColumnEntity[i].HasReference == true && objclsMasterTableListEntity[0].ColumnEntity[i].IsHidden == false)
                {
                    //strQry += "\t\t\t\t//List<BO." + objclsMasterTableListEntity[0].TableName + "Entity> obj" + objclsMasterTableListEntity[0].TableName + " = objSET_Country.GetSET_Countrys().ToList<BO.SET_CountryEntity>();\n";
                    if (objclsMasterTableListEntity[0].ColumnEntity[i].ReferenceTableName == "SET_CommonElement")
                    {
                        strQry += "\t\t\t\tviewData.View" + objclsMasterTableListEntity[0].TableName + ".ddl" + objclsMasterTableListEntity[0].ColumnEntity[i].ReferenceTableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].RefColumnName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + " = DDL.PopulateDropdownList(_iSetUPServiceClient.GetCommonElementByType(0).ToList(), \"" + objclsMasterTableListEntity[0].ColumnEntity[i].RefColumnName + "\", \"ElementName\", true, viewData.View" + objclsMasterTableListEntity[0].TableName + "." + objclsMasterTableListEntity[0].TableName + "." + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + ".ToString()).ToList();\n";
                    }
                    else
                    {
                        strQry += "\t\t\t\tviewData.View" + objclsMasterTableListEntity[0].TableName + ".ddl" + objclsMasterTableListEntity[0].ColumnEntity[i].ReferenceTableName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].RefColumnName + "_" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + " = DDL.PopulateDropdownList(new BLL.B" + objclsMasterTableListEntity[0].ColumnEntity[i].ReferenceTableName + "().Get" + objclsMasterTableListEntity[0].ColumnEntity[i].ReferenceTableName + "s().ToList(), \"" + objclsMasterTableListEntity[0].ColumnEntity[i].RefColumnName + "\", \"" + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + "\", true, viewData." + objclsMasterTableListEntity[0].TableName + "." + objclsMasterTableListEntity[0].ColumnEntity[i].ColumnAliasName + ".ToString()).ToList();\n";
                    }
                }
            }

            foreach (clsAllTableListEntity List in objclsChildTableListEntity)
            {
                strQry += "\t\t\t\tCommonPage" + List.TableName + "Populate(viewData);\n";
            }
            strQry += "\t\t\t\treturn viewData;\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch(Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tthrow ex;\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t}\n\n";

            foreach (clsAllTableListEntity List in objclsChildTableListEntity)
            {
                strQry += GetChildCode(objclsMasterTableListEntity[0].TableName, List);
            }

            strQry += "\n\t}\n";
            strQry += "}";

            return strQry;
        }

        private string GetChildCode(string MasterTable, clsAllTableListEntity objclsChildTableListEntity)
        {
            string strQry = "\n\t\t#region Child Tables Code\n\n";

            strQry += "\t\t#region Child Table " + objclsChildTableListEntity.TableName + " Grid Population\n\n";
            //str += "\t\tpublic ActionResult Get" + objclsChildTableListEntity.TableName + "BySP(long id, string sidx, string sord, int page, int Rows)\n";
            strQry += "\t\tpublic ActionResult Get" + objclsChildTableListEntity.TableName + "BySP(long id, string sidx, string sord)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\tIList<BO." + objclsChildTableListEntity.TableName + "EntityRef> obj" + objclsChildTableListEntity.TableName + "EntityRef = new List<BO." + objclsChildTableListEntity.TableName + "EntityRef>();\n";
            strQry += "\t\t\tBO." + objclsChildTableListEntity.TableName + "EntityRef obj" + objclsChildTableListEntity.TableName + "Ent = new BO." + objclsChildTableListEntity.TableName + "EntityRef();\n";
            strQry += "\t\t\tobj" + objclsChildTableListEntity.TableName + "Ent.QryOption = 1;\n";
            //str += "\t\t\tobj" + objclsChildTableListEntity.TableName + "Ent.PageSize = Rows;\n";
            //str += "\t\t\tobj" + objclsChildTableListEntity.TableName + "Ent.CurrentPage = page;\n";
            strQry += "\t\t\tobj" + objclsChildTableListEntity.TableName + "Ent.OrderBy = sidx;\n";
            strQry += "\t\t\tobj" + objclsChildTableListEntity.TableName + "Ent.OrderByDirection = sord;\n";
            strQry += "\t\t\t//Collection of Item types form database table " + objclsChildTableListEntity.TableName + "\n";
            strQry += "\t\t\tobj" + objclsChildTableListEntity.TableName + "EntityRef = obj" + objclsChildTableListEntity.TableName + ".Get" + objclsChildTableListEntity.TableName + "SP(obj" + objclsChildTableListEntity.TableName + "Ent);\n";

            strQry += "\t\t\t//No of total records\n";
            strQry += "\t\t\tint totalRecords = (int)obj" + objclsChildTableListEntity.TableName + "Ent.Count;\n";
            strQry += "\t\t\tint rownumber = 0;\n";
            strQry += "\t\t\t//Calculate total no of page  \n";
            strQry += "\t\t\tint totalPages = 1;   // (int)Math.Ceiling((float)totalRecords / (float)Rows);\n";

            strQry += "\t\t\tvar getdata = new\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\ttotal = totalPages,\n";
            strQry += "\t\t\t\tpage = 1,\n";
            strQry += "\t\t\t\trecords = totalRecords,\n";
            strQry += "\t\t\t\trows = (\n";
            strQry += "\t\t\t\t\tfrom p in obj" + objclsChildTableListEntity.TableName + "EntityRef\n";
            strQry += "\t\t\t\t\tselect new\n";
            strQry += "\t\t\t\t\t{\n";


            string array = "";

            for (int i = 0; i < objclsChildTableListEntity.ColumnEntity.Count; i++)
            {
                if (objclsChildTableListEntity.ColumnEntity[i].ColumnDotNetType == "string")
                {
                    array = array + "\t\t\t\t\t\t\tp." + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + ",\n";
                }
                else
                {
                    array = array + "\t\t\t\t\t\t\tConvert.ToString(p." + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "),\n";
                }
            }

            strQry += "\t\t\t\t\t\t cell = new string[] { \n" + array;
            strQry += "\t\t\t\t\t\t\tConvert.ToString(rownumber++),\n";
            strQry += "\t\t\t\t\t\t\tp.Tag.ToString(),\n";
            strQry += "\t\t\t\t\t\t\tjQueryGridHelper.GenerateLinkButton((rownumber-1).ToString(),\"<img id=\\\"imgEdit" + objclsChildTableListEntity.TableName + "\\\" src=\\\"../../Content/images/edit_icon.png\\\" title=\\\"Edit\\\" />\",\"Edit" + objclsChildTableListEntity.TableName + "Records\"),\n";
            strQry += "\t\t\t\t\t\t\tjQueryGridHelper.GenerateLinkButton((rownumber-1).ToString(),\"<img id=\\\"imgDelete" + objclsChildTableListEntity.TableName + "\\\" src=\\\"../../Content/images/delete_icon.png\\\" title=\\\"Delete\\\"/>\",\"Delete" + objclsChildTableListEntity.TableName + "Records\") }\n";

            strQry += "\t\t\t\t\t\t\t//jQueryGridHelper.GenerateLinkButton((rownumber-1).ToString(), Convert.ToString(p.Status)== \"0\" ? \"<img id=\\\"imgEdit" + objclsChildTableListEntity.TableName + "\\\" src=\\\"../../Content/images/edit_icon.png\\\" title=\\\"Edit\\\" />\" : \"<img id=\\\"imgEdit" + objclsChildTableListEntity.TableName + "\\\"  title=\\\"Not Editable\\\" src=\\\"../../Content/images/disableEditImg.png\\\" />\", Convert.ToString(p.Status) == \"0\" ? \"Edit" + objclsChildTableListEntity.TableName + "Records\":\"#\"),\n";
            strQry += "\t\t\t\t\t\t\t//jQueryGridHelper.GenerateLinkButton((rownumber-1).ToString(), Convert.ToString(p.Status)== \"0\" ? \"<img id=\\\"imgDelete" + objclsChildTableListEntity.TableName + "\\\" src=\\\"../../Content/images/delete_icon.png\\\" title=\\\"Delete\\\" />\" : \"<img id=\\\"imgDelete" + objclsChildTableListEntity.TableName + "\\\"  title=\\\"Not Deleteable\\\" src=\\\"../../Content/images/disableDeleteImg.png\\\" />\", Convert.ToString(p.Status) == \"0\" ? \"Delete" + objclsChildTableListEntity.TableName + "Records\":\"#\"),\n";

            strQry += "\t\t\t\t\t}).ToArray()\n";
            strQry += "\t\t\t};\n";
            strQry += "\t\t\treturn Json(getdata);\n";
            strQry += "\t\t}\n\n";
            strQry += "\t\t#endregion\n\n";

            strQry += "\t\t#region Child Table " + objclsChildTableListEntity.TableName + " CommonPage Population\n\n";
            strQry += "\t\tprivate MODEL.View" + MasterTable + "All CommonPage" + objclsChildTableListEntity.TableName + "Populate(MODEL.View" + MasterTable + "All viewData)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            for (int i = 0; i < objclsChildTableListEntity.ColumnEntity.Count; i++)
            {

                if (objclsChildTableListEntity.ColumnEntity[i].HasReference == true && objclsChildTableListEntity.ColumnEntity[i].IsHidden == false)
                {
                    //strQry += "\t\t\t\t//List<BO." + objclsChildTableListEntity.TableName + "Entity> obj" + objclsChildTableListEntity.TableName + " = objSET_Country.GetSET_Countrys().ToList<BO.SET_CountryEntity>();\n";
                    if (objclsChildTableListEntity.ColumnEntity[i].ReferenceTableName == "SET_CommonElement")
                    {
                        strQry += "\t\t\t\tviewData.View" + objclsChildTableListEntity.TableName + ".ddl" + objclsChildTableListEntity.ColumnEntity[i].ReferenceTableName + "_" + objclsChildTableListEntity.ColumnEntity[i].RefColumnName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + " = DDL.PopulateDropdownList(_iSetUPServiceClient.GetCommonElementByType(0).ToList(), \"" + objclsChildTableListEntity.ColumnEntity[i].RefColumnName + "\", \"ElementName\").ToList();\n";
                    }
                    else
                    {
                        strQry += "\t\t\t\tviewData.View" + objclsChildTableListEntity.TableName + ".ddl" + objclsChildTableListEntity.ColumnEntity[i].ReferenceTableName + "_" + objclsChildTableListEntity.ColumnEntity[i].RefColumnName + "_" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + " = DDL.PopulateDropdownList(new BLL.B" + objclsChildTableListEntity.ColumnEntity[i].ReferenceTableName + "().Get" + objclsChildTableListEntity.ColumnEntity[i].ReferenceTableName + "s().ToList(), \"" + objclsChildTableListEntity.ColumnEntity[i].RefColumnName + "\", \"" + objclsChildTableListEntity.ColumnEntity[i].ColumnAliasName + "\").ToList();\n";
                    }
                }
            }

            strQry += "\t\t\t\treturn viewData;\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch(Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tthrow ex;\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t}\n\n";
            strQry += "\t\t#endregion\n\n";



            strQry += "\t\t#endregion End of Child tables Code\n\n\n";

            return strQry;
        }
    }
}

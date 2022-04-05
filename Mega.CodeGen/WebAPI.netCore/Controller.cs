using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mega.CodeGen.WebAPI.netCore
{
    class Controller
    {
        public bool ControllerEngineMethod(IList<clsTableRefEntity> objclsTableRefEntityList, IList<clsColumnEntity> objclsColumnEntityList, string nameSpace, string tableName, string path, string DataBaseName)
        {
            path = path + "\\Controller";
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

            strQry = "///////////////////////////////////////////////////////////////////////////////\n";
            strQry += "//      Author      : SM Habib Ullah -- Prince\n";
            strQry += "//      Web         : https://www.Prince360.net\n";
            strQry += "//      Date        : " + DateTime.Now.ToString("dd-MM-yyyy") + "     \n";
            strQry += "//      File name   : " + tableName + "Controller.cs      \n";
            strQry += "///////////////////////////////////////////////////////////////////////////////\n\n";

            strQry += "using " + nameSpace.Split('.')[0] + ".API.Helper;\n";
            strQry += "using " + nameSpace.Split('.')[0] + ".Entities;\n";
            strQry += "using " + nameSpace.Split('.')[0] + ".Entities." + nameSpace.Split('.')[2] + ";\n";
            strQry += "using " + nameSpace.Split('.')[0] + ".MsgContainer;\n";
            strQry += "using Microsoft.AspNetCore.Mvc;\n";
            strQry += "using System;\n";
            strQry += "using System.Collections.Generic;\n";
            strQry += "using System.Linq;\n";
            strQry += "\n\nnamespace " + nameSpace.Split('.')[0] + ".API.Controllers." + nameSpace.Split('.')[2] + "\n";
            strQry += "{\n";
            strQry += "\t[AppPermission]\n";
            strQry += "\t[Route(\"api/" + nameSpace.Split('.')[2] + "/[controller]\")]\n";
            strQry += "\tpublic class " + tableName + "Controller : BaseController\n";
            strQry += "\t{\n\n";
            strQry += "\t\t#region GetAll\n\n";

            strQry += "\t\t[HttpGet(\"GetAll\")]\n";
            strQry += "\t\tpublic ActionResult<IEnumerable<string>> GetAll([FromQuery] " + tableName + "Entity_Ext param)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tvar data = " + nameSpace.Split('.')[0] + ".FCC." + nameSpace.Split('.')[2] + "." + tableName + "FCC.GetFacadeCreate(base.appCapsule).GetAll(param).ToList();\n";
            strQry += "\t\t\t\tvar getdata = new\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\tStatus = \"success\",\n";
            strQry += "\t\t\t\t\tstatusmsg = objMsg.PerseMessageForInformation(clsMsgContainerHelper.FormAction.DataFetch),\n";
            strQry += "\t\t\t\t\tdata = data.Select(x => new\n";
            strQry += "\t\t\t\t\t{\n";
            strQry += GetSelectFields(objclsColumnEntityList, "x" );
            strQry += "\t\t\t\t\t\tx.CurrentState\n";
            strQry += "\t\t\t\t\t}).ToArray()\n";
            strQry += "\t\t\t\t};\n";
            strQry += "\t\t\t\treturn Ok(getdata);\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch (Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tvar getdata = new\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\tStatus = \"fail\",\n";
            strQry += "\t\t\t\t\tstatusmsg = Helps.GetErrorMsg(ex)\n";
            strQry += "\t\t\t\t};\n";
            strQry += "\t\t\t\treturn NotFound(getdata);\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\t[HttpGet(\"GetAll_Pg\")]\n";
            strQry += "\t\tpublic ActionResult<IEnumerable<string>> GetAll_Pg([FromQuery] " + tableName + "Entity_Ext param)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tvar data = " + nameSpace.Split('.')[0] + ".FCC." + nameSpace.Split('.')[2] + "." + tableName + "FCC.GetFacadeCreate(base.appCapsule).GetAllByPages(param).ToList();\n";
            strQry += "\t\t\t\tvar getdata = new\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\tStatus = \"success\",\n";
            strQry += "\t\t\t\t\tstatusmsg = objMsg.PerseMessageForInformation(clsMsgContainerHelper.FormAction.DataFetch),\n";
            strQry += "\t\t\t\t\tTotalRecord = param.TotalRecord,\n";
            strQry += "\t\t\t\t\tPageSize = param.PageSize,\n";
            strQry += "\t\t\t\t\tCurrentPage = param.CurrentPage,\n";
            strQry += "\t\t\t\t\tdata = data.Select(x => new\n";
            strQry += "\t\t\t\t\t{\n";
            strQry += GetSelectFields(objclsColumnEntityList, "x");
            strQry += "\t\t\t\t\t\tx.CurrentState\n";
            
            //strQry = strQry.Substring(0, strQry.Length - 2) + "\n";
            strQry += "\t\t\t\t\t}).ToArray()\n";
            strQry += "\t\t\t\t};\n";
            strQry += "\t\t\t\treturn Ok(getdata);\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch (Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tvar getdata = new\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\tStatus = \"fail\",\n";
            strQry += "\t\t\t\t\tstatusmsg = Helps.GetErrorMsg(ex)\n";
            strQry += "\t\t\t\t};\n";
            strQry += "\t\t\t\treturn NotFound(getdata);\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\t#endregion GetAll\n\n";

            strQry += "\t\t#region Save Update Delete List \n\n";

            strQry += "\t\t[HttpPost(\"Insert\")]\n";
            strQry += "\t\tpublic IActionResult Insert([FromBody] " + tableName + "Entity_Ext param)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tparam.BaseSecurityParam = new SecurityCapsule();\n";
            strQry += "\t\t\t\tparam.BaseSecurityParam = getSecurityCapsule();\n";
            strQry += "\t\t\t\tlong data = " + nameSpace.Split('.')[0] + ".FCC." + nameSpace.Split('.')[2] + "." + tableName + "FCC.GetFacadeCreate(base.appCapsule).Add(param);\n";
            strQry += "\t\t\t\tparam." + objclsColumnEntityList[colval].ColumnName.Substring(0, 1).ToLower() + objclsColumnEntityList[colval].ColumnName.Substring(1, objclsColumnEntityList[colval].ColumnName.Length - 1) + " = param.ReturnKey;\n";
            strQry += "\t\t\t\tvar getdata = new\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\tStatus = \"success\",\n";
            strQry += "\t\t\t\t\tstatusmsg = objMsg.PerseMessageForInformation(clsMsgContainerHelper.FormAction.Add),\n";
            strQry += "\t\t\t\t\tdata = new\n";
            strQry += "\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\tparam." + objclsColumnEntityList[colval].ColumnName.Substring(0, 1).ToLower() + objclsColumnEntityList[colval].ColumnName.Substring(1, objclsColumnEntityList[colval].ColumnName.Length - 1) + "\n";
            strQry += "\t\t\t\t\t}\n";
            strQry += "\t\t\t\t};\n";
            strQry += "\t\t\t\treturn Ok(getdata);\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch (Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tvar getdata = new\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\tStatus = \"fail\",\n";
            strQry += "\t\t\t\t\tstatusmsg = Helps.GetErrorMsg(ex)\n";
            strQry += "\t\t\t\t};\n";
            strQry += "\t\t\t\treturn NotFound(getdata);\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\t[HttpPost(\"Update\")]\n";
            strQry += "\t\tpublic IActionResult Update([FromBody] " + tableName + "Entity_Ext param)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tparam.BaseSecurityParam = new SecurityCapsule();\n";
            strQry += "\t\t\t\tparam.BaseSecurityParam = getSecurityCapsule();\n";
            strQry += "\t\t\t\tlong data = " + nameSpace.Split('.')[0] + ".FCC." + nameSpace.Split('.')[2] + "." + tableName + "FCC.GetFacadeCreate(base.appCapsule).Update(param);\n";
            strQry += "\t\t\t\tparam." + objclsColumnEntityList[colval].ColumnName.Substring(0, 1).ToLower() + objclsColumnEntityList[colval].ColumnName.Substring(1, objclsColumnEntityList[colval].ColumnName.Length - 1) + " = param.ReturnKey;\n";
            strQry += "\t\t\t\tvar getdata = new\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\tStatus = \"success\",\n";
            strQry += "\t\t\t\t\tstatusmsg = objMsg.PerseMessageForInformation(clsMsgContainerHelper.FormAction.Edit),\n";
            strQry += "\t\t\t\t\tdata = new\n";
            strQry += "\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\tparam." + objclsColumnEntityList[colval].ColumnName.Substring(0, 1).ToLower() + objclsColumnEntityList[colval].ColumnName.Substring(1, objclsColumnEntityList[colval].ColumnName.Length - 1) + "\n";
            strQry += "\t\t\t\t\t}\n";
            strQry += "\t\t\t\t};\n";
            strQry += "\t\t\t\treturn Ok(getdata);\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch (Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tvar getdata = new\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\tStatus = \"fail\",\n";
            strQry += "\t\t\t\t\tstatusmsg = Helps.GetErrorMsg(ex)\n";
            strQry += "\t\t\t\t};\n";
            strQry += "\t\t\t\treturn NotFound(getdata);\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t}\n\n";

            strQry += "\t\t[HttpPost(\"Delete\")]\n";
            strQry += "\t\tpublic IActionResult Delete([FromBody] " + tableName + "Entity_Ext param)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tparam.BaseSecurityParam = new SecurityCapsule();\n";
            strQry += "\t\t\t\tparam.BaseSecurityParam = getSecurityCapsule();\n";
            strQry += "\t\t\t\tlong data = " + nameSpace.Split('.')[0] + ".FCC." + nameSpace.Split('.')[2] + "." + tableName + "FCC.GetFacadeCreate(base.appCapsule).Delete(param);\n";
            strQry += "\t\t\t\tparam." + objclsColumnEntityList[colval].ColumnName.Substring(0, 1).ToLower() + objclsColumnEntityList[colval].ColumnName.Substring(1, objclsColumnEntityList[colval].ColumnName.Length - 1) + " = param.ReturnKey;\n";
            strQry += "\t\t\t\tvar getdata = new\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\tStatus = \"success\",\n";
            strQry += "\t\t\t\t\tstatusmsg = objMsg.PerseMessageForInformation(clsMsgContainerHelper.FormAction.Delete),\n";
            strQry += "\t\t\t\t\tdata = new\n";
            strQry += "\t\t\t\t\t{\n";
            strQry += "\t\t\t\t\t\tparam." + objclsColumnEntityList[colval].ColumnName.Substring(0, 1).ToLower() + objclsColumnEntityList[colval].ColumnName.Substring(1, objclsColumnEntityList[colval].ColumnName.Length - 1) + "\n";
            strQry += "\t\t\t\t\t}\n";
            strQry += "\t\t\t\t};\n";
            strQry += "\t\t\t\treturn Ok(getdata);\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch (Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tvar getdata = new\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\tStatus = \"fail\",\n";
            strQry += "\t\t\t\t\tstatusmsg = Helps.GetErrorMsg(ex)\n";
            strQry += "\t\t\t\t};\n";
            strQry += "\t\t\t\treturn NotFound(getdata);\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t}\n\n";


            strQry += "\t\t#endregion Save Update Delete List\n\n";
           
            strQry += "\t\t#region SaveMasterDetails\n\n";
            if (objclsColumnEntityList[0].RefarenceToTable != null)
            {
                strQry += "\t\t[HttpPost(\"SaveMasterDet\")]\n";
                strQry += "\t\tpublic IActionResult SaveMasterDet([FromBody] " + tableName + "Entity_Ext param)\n";
                strQry += "\t\t{\n";
                strQry += "\t\t\ttry\n";
                strQry += "\t\t\t{\n";
                strQry += "\t\t\t\tparam.BaseSecurityParam = new SecurityCapsule();\n";
                strQry += "\t\t\t\tparam.BaseSecurityParam = getSecurityCapsule();\n";
                strQry += "\t\t\t\tlong data = " + nameSpace.Split('.')[0] + ".FCC." + nameSpace.Split('.')[2] + "." + tableName + "FCC.GetFacadeCreate(base.appCapsule).SaveMasterDetails(param);\n";
                strQry += "\t\t\t\tparam." + objclsColumnEntityList[colval].ColumnName.Substring(0, 1).ToLower() + objclsColumnEntityList[colval].ColumnName.Substring(1, objclsColumnEntityList[colval].ColumnName.Length - 1) + " = param.ReturnKey;\n";
                strQry += "\t\t\t\tvar getdata = new\n";
                strQry += "\t\t\t\t{\n";
                strQry += "\t\t\t\t\tStatus = \"success\",\n";
                strQry += "\t\t\t\t\tstatusmsg = objMsg.PerseMessageForInformation(clsMsgContainerHelper.FormAction.Add),\n";
                strQry += "\t\t\t\t\tdata = new\n";
                strQry += "\t\t\t\t\t{\n";
                strQry += "\t\t\t\t\t\tparam." + objclsColumnEntityList[colval].ColumnName.Substring(0, 1).ToLower() + objclsColumnEntityList[colval].ColumnName.Substring(1, objclsColumnEntityList[colval].ColumnName.Length - 1) + "\n";
                strQry += "\t\t\t\t\t}\n";
                strQry += "\t\t\t\t};\n";
                strQry += "\t\t\t\treturn Ok(getdata);\n";
                strQry += "\t\t\t}\n";
                strQry += "\t\t\tcatch (Exception ex)\n";
                strQry += "\t\t\t{\n";
                strQry += "\t\t\t\tvar getdata = new\n";
                strQry += "\t\t\t\t{\n";
                strQry += "\t\t\t\t\tStatus = \"fail\",\n";
                strQry += "\t\t\t\t\tstatusmsg = Helps.GetErrorMsg(ex)\n";
                strQry += "\t\t\t\t};\n";
                strQry += "\t\t\t\treturn NotFound(getdata);\n";
                strQry += "\t\t\t}\n";
                strQry += "\t\t}\n";
            }
            strQry += "\n\t\t#endregion SaveMasterDetails\n\n";


            strQry += "\t}\n";

            strQry += "}\n";

            return strQry;
        }


        public string GetSelectFields(IList<clsColumnEntity> objclsColumnEntityList, string prefix)
        {
            string strQry = "";

            for (int i = 0; i < objclsColumnEntityList.Count; i++)
            {
                if (objclsColumnEntityList[i].ColumnName == "TS"
                   || objclsColumnEntityList[i].ColumnName == "CreatedDate"
                   || objclsColumnEntityList[i].ColumnName == "CreatedBy"
                   || objclsColumnEntityList[i].ColumnName == "UpdatedDate"
                   || objclsColumnEntityList[i].ColumnName == "UpdatedBy"
                   || objclsColumnEntityList[i].ColumnName == "TagID"
                    )
                {
                    continue;
                }
                if (objclsColumnEntityList[i].IsMasterTable == true)
                    strQry += "\t\t\t\t\t\t" + prefix + "." + objclsColumnEntityList[i].ColumnName.Substring(0, 1).ToLower() + objclsColumnEntityList[i].ColumnName.Substring(1, objclsColumnEntityList[i].ColumnName.Length - 1) + ",\n";
            }
            return strQry;
        }

        int colval = 0;
        public string GetMasterDetailsMethods(IList<clsColumnEntity> objclsColumnEntityList, string tableName, string nameSpace, string tab)
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
                    if (objclsColumnEntityList[i].ColumnDBType.Contains("identity"))
                    {
                        colval = i;
                    }
                  
                }
            }
            return strQry;
        }


    }
}



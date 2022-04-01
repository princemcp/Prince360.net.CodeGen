using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Mega.CodeGen.MVC2Engine
{
    public class DALEngine
    {
        public bool DALEngineMethod(IList<clsTableRefEntity> objclsTableRefEntityList, IList<clsColumnEntity> objclsColumnEntityList, string nameSpace, string tableName, string path, string DataBaseName)
        {
            path = path + "\\DAL";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\D" + tableName + ".cs";

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
            strQry += "//      File name   : D" + tableName + ".cs     \n";
            strQry += "///////////////////////////////////////////////////////////////////////////////\n\n";

            strQry += "using System;";
            strQry += "using System.Linq;";
            strQry += "using System.Linq.Dynamic;";
            strQry += "using System.Text;";
            strQry += "using System.Collections.Generic;";
            strQry += "\n\nnamespace " + nameSpace + ".DAL\n";
            strQry += "{\n";

            strQry += "\tpublic partial class D" + tableName + " : IDAL.I" + tableName + "\n";
            strQry += "\t{\n";
            strQry += "\t\tDATA." + nameSpace.Replace('.', '_') + "_Entities dc = new DATA." + nameSpace.Replace('.', '_') + "_Entities();\n\n";

            strQry += "\t\tpublic int Insert (BO." + tableName + "Entity " + "obj" + tableName + "Entity)\n\t\t{\n";
            strQry += "\t\t\ttry\n\t\t\t{\n";
            strQry += "\t\t\t\t//Set the valus from the Business Domain object to the Data Entity Set\n";
            strQry += "\t\t\t\tDATA." + tableName + " " + "entity = ENTITYMAPPERS.EM" + tableName + ".SetToEntity(new DATA." + tableName + "(), " + "obj" + tableName + "Entity);\n";
            strQry += "\t\t\t\t//Add the object to Entity Set\n";
            strQry += "\t\t\t\tdc.AddTo" + tableName + "(entity);\n";
            strQry += "\t\t\t\t//Save the Entity Set\n";
            strQry += "\t\t\t\treturn dc.SaveChanges();\n";
            strQry += "\t\t\t}\n\t\t\tcatch(Exception ex)\n\t\t\t{\n\t\t\t\tthrow ex;\n\t\t\t}\n";
            strQry += "\t\t}\n\n";



            strQry += "\t\tpublic int Update (" + objclsColumnEntityList[0].ColumnDotNetType + " " + objclsColumnEntityList[0].ColumnName + ", BO." + tableName + "Entity " + "obj" + tableName + "Entity)\n\t\t{\n";
            strQry += "\t\t\ttry\n\t\t\t{\n";
            strQry += "\t\t\t\t//Set the valus from the Business Domain object to the Data Entity Set\n";
            strQry += "\t\t\t\tDATA." + tableName + " " + "Centity = ENTITYMAPPERS.EM" + tableName + ".SetToEntity(new DATA." + tableName + "(), " + "obj" + tableName + "Entity);\n";
            strQry += "\t\t\t\t//Get the Original Entity\n";
            strQry += "\t\t\t\tvar Oentity = dc." + tableName + ".First(m => m." + objclsColumnEntityList[0].ColumnName + " == " + objclsColumnEntityList[0].ColumnName + ");\n";
            strQry += "\t\t\t\t//Update the old value with the current values of a change-tracked entity\n";
            strQry += "\t\t\t\tdc.ApplyCurrentValues(\"" + tableName + "\", Centity);\n";
            strQry += "\t\t\t\tdc.ApplyOriginalValues(\"" + tableName + "\", Oentity);\n";
            strQry += "\t\t\t\t//Save the Entity Set\n";
            strQry += "\t\t\t\treturn dc.SaveChanges();\n";
            strQry += "\t\t\t}\n\t\t\tcatch(Exception ex)\n\t\t\t{\n\t\t\t\tthrow ex;\n\t\t\t}\n";
            strQry += "\t\t}\n\n";


            strQry += "\t\tpublic int Update (BO." + tableName + "Entity " + "objO" + tableName + "Entity, BO." + tableName + "Entity " + "objM" + tableName + "Entity)\n\t\t{\n";
            strQry += "\t\t\ttry\n\t\t\t{\n";
            strQry += "\t\t\t\t//Update the old values with the current values of a change-tracked entity\n";
            strQry += "\t\t\t\tdc.ApplyCurrentValues(\"" + tableName + "\", objM" + tableName + "Entity);\n";
            strQry += "\t\t\t\tdc.ApplyOriginalValues(\"" + tableName + "\", objO" + tableName + "Entity);\n";
            strQry += "\t\t\t\t//Save the Entity Set\n";
            strQry += "\t\t\t\treturn dc.SaveChanges();\n";
            strQry += "\t\t\t}\n\t\t\tcatch(Exception ex)\n\t\t\t{\n\t\t\t\tthrow ex;\n\t\t\t}\n";
            strQry += "\t\t}\n\n";


            strQry += "\t\tpublic int Delete (" + objclsColumnEntityList[0].ColumnDotNetType + " " + objclsColumnEntityList[0].ColumnName + ")\n\t\t{\n";
            strQry += "\t\t\ttry\n\t\t\t{\n";
            strQry += "\t\t\t\tvar deleteElement = dc." + tableName + ".First(m => m." + objclsColumnEntityList[0].ColumnName + " == " + objclsColumnEntityList[0].ColumnName + ");\n";
            strQry += "\t\t\t\tdc.DeleteObject(deleteElement);\n";
            strQry += "\t\t\t\t//Save the Entity Set\n";
            strQry += "\t\t\t\treturn dc.SaveChanges();\n";
            strQry += "\t\t\t}\n\t\t\tcatch(Exception ex)\n\t\t\t{\n\t\t\t\tthrow ex;\n\t\t\t}\n";
            strQry += "\t\t}\n\n";


            strQry += "\t\tpublic int Delete (BO." + tableName + "Entity " + "obj" + tableName + "Entity)\n\t\t{\n";
            strQry += "\t\t\ttry\n\t\t\t{\n";
            strQry += "\t\t\t\tdc.DeleteObject(obj" + tableName + "Entity);\n";
            strQry += "\t\t\t\t//Save the Entity Set\n";
            strQry += "\t\t\t\treturn dc.SaveChanges();\n";
            strQry += "\t\t\t}\n\t\t\tcatch(Exception ex)\n\t\t\t{\n\t\t\t\tthrow ex;\n\t\t\t}\n";
            strQry += "\t\t}\n\n";


            strQry += "\t\tpublic IList<BO." + tableName + "Entity> Get" + tableName + "By" + objclsColumnEntityList[0].ColumnName.ToUpper() + "(" + objclsColumnEntityList[0].ColumnDotNetType + " " + objclsColumnEntityList[0].ColumnName + ")\n\t\t{\n";
            strQry += "\t\t\ttry\n\t\t\t{\n";
            strQry += "\t\t\t\t//Get the values for the ID and set to EntitySet\n";
            strQry += "\t\t\t\tList< DATA." + tableName + "> obj" + tableName + " = dc." + tableName + ".ToList<DATA." + tableName + ">().Where(m => m." + objclsColumnEntityList[0].ColumnName + " == " + objclsColumnEntityList[0].ColumnName + ").ToList();\n";
            strQry += "\t\t\t\t//Instance of the Business Domain Object\n";
            strQry += "\t\t\t\tIList<BO." + tableName + "Entity> listObj = new List<BO." + tableName + "Entity>();\n";
            strQry += "\t\t\t\t//Set the Values to Domain Object and return the object\n";
            strQry += "\t\t\t\tforeach(DATA." + tableName + " e in obj" + tableName + ")\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\tlistObj.Add(ENTITYMAPPERS.EM" + tableName + ".SetToBusinessObject(e));\n";
            strQry += "\t\t\t\t}\n";
            strQry += "\t\t\t\treturn listObj;\n";
            strQry += "\t\t\t}\n\t\t\tcatch(Exception ex)\n\t\t\t{\n\t\t\t\tthrow ex;\n\t\t\t}\n";
            strQry += "\t\t}\n\n";


            strQry += "\t\tpublic IList<BO." + tableName + "Entity> Get" + tableName + "By" + objclsColumnEntityList[0].ColumnName.ToUpper() + "WithPaging(BO." + tableName + "Entity obj" + tableName + "Entity)\n\t\t{\n";
            strQry += "\t\t\ttry\n\t\t\t{\n";
            strQry += "\t\t\t\t//Get the values for the ID and set to EntitySet\n";
            strQry += "\t\t\t\tList<DATA." + tableName + "> obj" + tableName + " = dc." + tableName + ".OrderBy(\"it.\" + obj" + tableName + "Entity.OrderBy + \" \" + obj" + tableName + "Entity.OrderByDirection).ToList<DATA." + tableName + ">().Where(m => m.id == obj" + tableName + "Entity.id).Skip(obj" + tableName + "Entity.CurrentPage * obj" + tableName + "Entity.PageSize).Take(obj" + tableName + "Entity.PageSize).ToList();\n";
            strQry += "\t\t\t\t//Instance of the Business Domain Object\n";
            strQry += "\t\t\t\tIList<BO." + tableName + "Entity> listObj = new List<BO." + tableName + "Entity>();\n";
            strQry += "\t\t\t\t//Set the Values to Domain Object and return the object\n";
            strQry += "\t\t\t\tforeach(DATA." + tableName + " e in obj" + tableName + ")\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\tlistObj.Add(ENTITYMAPPERS.EM" + tableName + ".SetToBusinessObject(e));\n";
            strQry += "\t\t\t\t}\n";
            strQry += "\t\t\t\treturn listObj;\n";
            strQry += "\t\t\t}\n\t\t\tcatch(Exception ex)\n\t\t\t{\n\t\t\t\tthrow ex;\n\t\t\t}\n";
            strQry += "\t\t}\n\n";


            strQry += "\t\tpublic IList<BO." + tableName + "Entity> Get" + tableName + "s()\n\t\t{\n";
            strQry += "\t\t\ttry\n\t\t\t{\n";
            strQry += "\t\t\t\t//Get the values for the ID and set to EntitySet\n";
            strQry += "\t\t\t\tIList<DATA." + tableName + "> obj" + tableName + " = dc." + tableName + ".OrderBy(m=>m." + objclsColumnEntityList[1].ColumnName + ").ToList<DATA." + tableName + ">();\n";
            strQry += "\t\t\t\t//Set the Values to Domain Object and return the object\n";
            strQry += "\t\t\t\treturn obj" + tableName + ".Select(c => ENTITYMAPPERS.EM" + tableName + ".SetToBusinessObject(c)).ToList<BO." + tableName + "Entity>();\n";
            strQry += "\t\t\t}\n\t\t\tcatch(Exception ex)\n\t\t\t{\n\t\t\t\tthrow ex;\n\t\t\t}\n";
            strQry += "\t\t}\n\n";


            strQry += "\t\tpublic IList<BO." + tableName + "Entity> Get" + tableName + "sWithPaging(BO." + tableName + "Entity obj" + tableName + "Entity)\n\t\t{\n";
            strQry += "\t\t\ttry\n\t\t\t{\n";
            strQry += "\t\t\t\t//Instance of the Business Domain Object\n";
            strQry += "\t\t\t\tIList<DATA." + tableName + "> obj" + tableName + " = dc." + tableName + ".OrderBy(\"it.\" + obj" + tableName + "Entity.OrderBy + \" \" + obj" + tableName + "Entity.OrderByDirection).ToList<DATA." + tableName + ">().Skip(obj" + tableName + "Entity.CurrentPage * obj" + tableName + "Entity.PageSize).Take(obj" + tableName + "Entity.PageSize).ToList();\n";
            strQry += "\t\t\t\t//Set the Values to Domain Object and return the object\n";
            strQry += "\t\t\t\treturn obj" + tableName + ".Select(c => ENTITYMAPPERS.EM" + tableName + ".SetToBusinessObject(c)).ToList<BO." + tableName + "Entity>();\n";
            strQry += "\t\t\t}\n\t\t\tcatch(Exception ex)\n\t\t\t{\n\t\t\t\tthrow ex;\n\t\t\t}\n";
            strQry += "\t\t}\n\n";


            strQry += "\t\tpublic IList<BO." + tableName + "Entity> Get" + tableName + "ByOther(string colNameValue)\n\t\t{\n";
            strQry += "\t\t\ttry\n\t\t\t{\n";
            strQry += "\t\t\t\t//Get the values for the ID and set to EntitySet\n";
            strQry += "\t\t\t\tList< DATA." + tableName + "> obj" + tableName + " = dc." + tableName + ".Where(colNameValue).ToList();\n";
            strQry += "\t\t\t\t//Instance of the Business Domain Object\n";
            strQry += "\t\t\t\tIList<BO." + tableName + "Entity> listObj = new List<BO." + tableName + "Entity>();\n";
            strQry += "\t\t\t\t//Set the Values to Domain Object and return the object\n";
            strQry += "\t\t\t\tforeach(DATA." + tableName + " e in obj" + tableName + ")\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\tlistObj.Add(ENTITYMAPPERS.EM" + tableName + ".SetToBusinessObject(e));\n";
            strQry += "\t\t\t\t}\n";
            strQry += "\t\t\t\treturn listObj;\n";
            strQry += "\t\t\t}\n\t\t\tcatch(Exception ex)\n\t\t\t{\n\t\t\t\tthrow ex;\n\t\t\t}\n";
            strQry += "\t\t}\n\n";


            strQry += "\t\tpublic IList<BO." + tableName + "Entity> Get" + tableName + "ByOtherWithPaging(string colNameValue, BO." + tableName + "Entity obj" + tableName + "Entity)\n\t\t{\n";
            strQry += "\t\t\ttry\n\t\t\t{\n";
            strQry += "\t\t\t\t//Get the values for the ID and set to EntitySet\n";
            strQry += "\t\t\t\tList<DATA." + tableName + "> obj" + tableName + " = dc." + tableName + ".Where(colNameValue).Skip(obj" + tableName + "Entity.CurrentPage * obj" + tableName + "Entity.PageSize).Take(obj" + tableName + "Entity.PageSize).ToList();\n";
            strQry += "\t\t\t\t//Instance of the Business Domain Object\n";
            strQry += "\t\t\t\tIList<BO." + tableName + "Entity> listObj = new List<BO." + tableName + "Entity>();\n";
            strQry += "\t\t\t\t//Set the Values to Domain Object and return the object\n";
            strQry += "\t\t\t\tforeach(DATA." + tableName + " e in obj" + tableName + ")\n";
            strQry += "\t\t\t\t{\n";
            strQry += "\t\t\t\t\tlistObj.Add(ENTITYMAPPERS.EM" + tableName + ".SetToBusinessObject(e));\n";
            strQry += "\t\t\t\t}\n";
            strQry += "\t\t\t\treturn listObj;\n";
            strQry += "\t\t\t}\n\t\t\tcatch(Exception ex)\n\t\t\t{\n\t\t\t\tthrow ex;\n\t\t\t}\n";
            strQry += "\t\t}\n";


            strQry += "\t\tpublic IList<BO." + tableName + "EntityRef> Get" + tableName + "SP(BO." + tableName + "EntityRef obj_" + tableName + "EntityRef)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tSystem.Data.Objects.ObjectParameter ItemCount = new System.Data.Objects.ObjectParameter(\"ItemCount\", System.TypeCode.Int32);\n";
            strQry += "\t\t\t\t//List<DATA.spGet" + tableName + "_Result> objspGet" + tableName + "_Result = dc.spGet" + tableName + "(qryOption: obj_" + tableName + "EntityRef.QryOption, itemCount: ItemCount,";

            strQry += GetSPParam(objclsColumnEntityList, "") + "\n";

            strQry += "\t\t\t\t//\t\t\t\t\tpageSize: obj_" + tableName + "EntityRef.PageSize, currentPage: obj_" + tableName + "EntityRef.CurrentPage, orderBy: obj_" + tableName + "EntityRef.OrderBy, orderByDirection: obj_" + tableName + "EntityRef.OrderByDirection, queryString: obj_" + tableName + "EntityRef.QueryString).ToList();\n";
            strQry += "\t\t\t\t//obj_" + tableName + "EntityRef.Count = (int)ItemCount.Value;\n";
            strQry += "\t\t\t\t//return objspGet" + tableName + "_Result.Select(c => ENTITYMAPPERS.EM" + tableName + "Ref.SetToBusinessObject(c)).ToList<BO." + tableName + "EntityRef>();\n";
            strQry += "\t\t\t\treturn new List<BO." + tableName + "EntityRef>();\n\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch(Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tthrow ex;\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t}\n";


            strQry += "\t\tpublic IList<BO." + tableName + "EntityRef> Search" + tableName + "SP(BO." + tableName + "EntityRef obj_" + tableName + "EntityRef)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\ttry\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tSystem.Data.Objects.ObjectParameter ItemCount = new System.Data.Objects.ObjectParameter(\"ItemCount\", System.TypeCode.Int32);\n";
            strQry += "\t\t\t\t//List<DATA.spSearch" + tableName + "_Result> objspSearch" + tableName + "_Result = dc.spSearch" + tableName + "(qryOption: obj_" + tableName + "EntityRef.QryOption, itemCount: ItemCount,";
            strQry += SearchSPParam(objclsColumnEntityList, "Collection") + "\n";
            strQry += "\t\t\t\t//\t\t\t\t\tpageSize: obj_" + tableName + "EntityRef.PageSize, currentPage: obj_" + tableName + "EntityRef.CurrentPage, orderBy: obj_" + tableName + "EntityRef.OrderBy, orderByDirection: obj_" + tableName + "EntityRef.OrderByDirection, queryString: obj_" + tableName + "EntityRef.QueryString).ToList();\n";
            strQry += "\t\t\t\t//obj_" + tableName + "EntityRef.Count = (int)ItemCount.Value;\n";
            strQry += "\t\t\t\t//return objspSearch" + tableName + "_Result.Select(c => ENTITYMAPPERS.EM" + tableName + "Ref.SetToBusinessObject(c)).ToList<BO." + tableName + "EntityRef>();\n";
            strQry += "\t\t\t\treturn new List<BO." + tableName + "EntityRef>();\n\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t\tcatch(Exception ex)\n";
            strQry += "\t\t\t{\n";
            strQry += "\t\t\t\tthrow ex;\n";
            strQry += "\t\t\t}\n";
            strQry += "\t\t}\n";


            strQry += "\t}\n";
            strQry += "}\n";

            return strQry;
        }


        public string GetSPParam(IList<clsColumnEntity> objclsColumnEntityList, string surfix)
        {
            string strQry = "";
            for (int i = 0; i < objclsColumnEntityList.Count; i++)
            {
                if (objclsColumnEntityList[i].ColumnDotNetType != "byte[]")
                {
                    if (objclsColumnEntityList[i].GetSPParam == true)
                    {
                        if (objclsColumnEntityList[i].IsMasterTable == true)
                        {
                            strQry += "\n\t\t\t\t//\t\t\t\t\t" + objclsColumnEntityList[i].ColumnName.Substring(0, 1).ToLower() + objclsColumnEntityList[i].ColumnName.Substring(1, objclsColumnEntityList[i].ColumnName.Length - 1) + surfix;
                            if (objclsColumnEntityList[i].ColumnDBType == "datetime" && objclsColumnEntityList[i].ColumnIsNull == false)
                            {
                                if (surfix == "")
                                {
                                    strQry += ": obj_" + objclsColumnEntityList[0].TableName + "EntityRef." + objclsColumnEntityList[i].ColumnName + " == Convert.ToDateTime(\"1/1/0001\") ? Convert.ToDateTime(\"1/1/1754\") : obj_" + objclsColumnEntityList[0].TableName + "EntityRef." + objclsColumnEntityList[i].ColumnName + ", ";
                                }
                                else
                                {
                                    strQry += ": obj_" + objclsColumnEntityList[0].TableName + "EntityRef." + objclsColumnEntityList[i].ColumnName + surfix +", ";
                                }
                            }
                            else
                            {
                                strQry += ": obj_" + objclsColumnEntityList[0].TableName + "EntityRef." + objclsColumnEntityList[i].ColumnName + surfix +", ";
                            }
                        }
                        else
                        {
                            strQry += "\n\t\t\t\t//\t\t\t\t\t" + objclsColumnEntityList[i].ColumnAliasName.Substring(0, 1).ToLower() + objclsColumnEntityList[i].ColumnAliasName.Substring(1, objclsColumnEntityList[i].ColumnAliasName.Length - 1) + surfix;
                            if (objclsColumnEntityList[i].ColumnDBType == "datetime" && objclsColumnEntityList[i].ColumnIsNull == false)
                            {
                                if (surfix == "")
                                {
                                    strQry += ": obj_" + objclsColumnEntityList[0].TableName + "EntityRef." + objclsColumnEntityList[i].ColumnAliasName + " == Convert.ToDateTime(\"1/1/0001\") ? Convert.ToDateTime(\"1/1/1754\") : obj_" + objclsColumnEntityList[0].TableName + "EntityRef." + objclsColumnEntityList[i].ColumnAliasName + ", ";
                                }
                                else
                                {
                                    strQry += ": obj_" + objclsColumnEntityList[0].TableName + "EntityRef." + objclsColumnEntityList[i].ColumnAliasName + surfix +", ";
                                }
                            }
                            else
                            {
                                strQry += ": obj_" + objclsColumnEntityList[0].TableName + "EntityRef." + objclsColumnEntityList[i].ColumnAliasName + surfix +", ";
                            }
                        }
                    }
                }
            }
            return strQry;
        }


        public string SearchSPParam(IList<clsColumnEntity> objclsColumnEntityList, string surfix)
        {
            string strQry = "";
            for (int i = 0; i < objclsColumnEntityList.Count; i++)
            {
                if (objclsColumnEntityList[i].ColumnDotNetType != "byte[]")
                {
                    if (objclsColumnEntityList[i].SearchSPParam == true)
                    {
                        if (objclsColumnEntityList[i].IsMasterTable == true)
                        {
                            strQry += "\n\t\t\t\t//\t\t\t\t\t" + objclsColumnEntityList[i].ColumnName.Substring(0, 1).ToLower() + objclsColumnEntityList[i].ColumnName.Substring(1, objclsColumnEntityList[i].ColumnName.Length - 1) + surfix;
                            if (objclsColumnEntityList[i].ColumnDBType == "datetime" && objclsColumnEntityList[i].ColumnIsNull == false)
                            {
                                if (surfix == "")
                                {
                                    strQry += ": obj_" + objclsColumnEntityList[0].TableName + "EntityRef." + objclsColumnEntityList[i].ColumnName + " == Convert.ToDateTime(\"1/1/0001\") ? Convert.ToDateTime(\"1/1/1754\") : obj_" + objclsColumnEntityList[0].TableName + "EntityRef." + objclsColumnEntityList[i].ColumnName + ", ";
                                }
                                else
                                {
                                    strQry += ": obj_" + objclsColumnEntityList[0].TableName + "EntityRef." + objclsColumnEntityList[i].ColumnName + surfix + ", ";
                                }
                            }
                            else
                            {
                                strQry += ": obj_" + objclsColumnEntityList[0].TableName + "EntityRef." + objclsColumnEntityList[i].ColumnName + surfix + ", ";
                            }
                        }
                        else
                        {
                            strQry += "\n\t\t\t\t//\t\t\t\t\t" + objclsColumnEntityList[i].ColumnAliasName.Substring(0, 1).ToLower() + objclsColumnEntityList[i].ColumnAliasName.Substring(1, objclsColumnEntityList[i].ColumnAliasName.Length - 1) + surfix;
                            if (objclsColumnEntityList[i].ColumnDBType == "datetime" && objclsColumnEntityList[i].ColumnIsNull == false)
                            {
                                if (surfix == "")
                                {
                                    strQry += ": obj_" + objclsColumnEntityList[0].TableName + "EntityRef." + objclsColumnEntityList[i].ColumnAliasName + " == Convert.ToDateTime(\"1/1/0001\") ? Convert.ToDateTime(\"1/1/1754\") : obj_" + objclsColumnEntityList[0].TableName + "EntityRef." + objclsColumnEntityList[i].ColumnAliasName + ", ";
                                }
                                else
                                {
                                    strQry += ": obj_" + objclsColumnEntityList[0].TableName + "EntityRef." + objclsColumnEntityList[i].ColumnAliasName + surfix + ", ";
                                }
                            }
                            else
                            {
                                strQry += ": obj_" + objclsColumnEntityList[0].TableName + "EntityRef." + objclsColumnEntityList[i].ColumnAliasName + surfix + ", ";
                            }
                        }
                    }
                }
            }
            return strQry;
        }
           
    
    }
}

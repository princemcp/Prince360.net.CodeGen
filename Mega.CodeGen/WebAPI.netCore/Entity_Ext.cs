using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mega.CodeGen.WebAPI.netCore
{
    public class Entity_Ext
    {
        public bool Entity_ExtEngineMethod(IList<clsTableRefEntity> objclsTableRefEntityList, IList<clsColumnEntity> objclsColumnEntityList, string nameSpace, string tableName, string path, string DataBaseName)
        {
            path = path + "\\Entities_Ext";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\" + tableName + "Entity_Ext.cs";

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
            for (int i = objclsColumnEntityList.Count - 1; i >= 0; i--)
            {
                if (objclsColumnEntityList[i].IsMasterTable == false)
                {
                    objclsColumnEntityList.RemoveAt(i);
                }
            }

            strQry = "///////////////////////////////////////////////////////////////////////////////\n";
            strQry += "//      Author      : SM Habib Ullah -- Prince\n";
            strQry += "//      Web         : https://www.Prince360.net\n";
            strQry += "//      Date        : " + DateTime.Now.ToString("dd-MM-yyyy") + "     \n";
            strQry += "//      File name   : " + tableName + "Entity_Ext.cs      \n";
            strQry += "///////////////////////////////////////////////////////////////////////////////\n\n";

            //strQry += "using System.Collections.Generic;\n";
            strQry += "using System.Data;\n";
            //strQry += "using " + nameSpace.Split('.')[0] + ".Entities;\n";
            //strQry += "using System.Runtime.Serialization;\n";
            //strQry += "using " + nameSpace + ";\n";


            strQry += "\n\nnamespace " + nameSpace + "\n";
            strQry += "{\n";
            strQry += "\tpublic partial class " + tableName + "Entity_Ext : " + tableName + "Entity\n";
            strQry += "\t{\n";
            strQry += "\n";
            // strQry += "\n\n\n\n\t\t#region Base Table \n\n";
           
            
            strQry += "\t\t#region public properties\n\n";
            strQry += "\t\t/// <summary>\n";
            strQry += "\t\t/// WRITE EXTRA ATTRIBUTE HERE\n";
            strQry += "\t\t/// </summary>\n";
            strQry += "\t\t//Example:\n";
            strQry += "\t\t//public int? myproperty { get; set; }\n";

            strQry += "\n\t\t#endregion\n";
            strQry += "\n\t\t#region Constructor\n";

            strQry += "\n\t\tpublic " + tableName + "Entity_Ext()\n";
            strQry += "\t\t\t: base()\n";
            strQry += "\t\t{\n";
            strQry += "\t\t}\n";

            strQry += "\n\t\tpublic " + tableName + "Entity_Ext(IDataReader reader)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\tbase.LoadFromReader(reader);\n";
            strQry += "\t\t\tthis.LoadFromReader_Ext(reader);\n";
            strQry += "\t\t}\n";


            strQry += "\n\t\tprotected void LoadFromReader_Ext(IDataReader reader)\n";
            strQry += "\t\t{\n";
            strQry += "\t\t\t/// <summary>\n";
            strQry += "\t\t\t/// WRITE EXTRA ATTRIBUTE HERE\n";
            strQry += "\t\t\t/// </summary>\n";
            strQry += "\t\t\t//Example:\n";
            strQry += "\t\t\t//if (!reader.IsDBNull(reader.GetOrdinal(\"myproperty\"))) properties = reader.GetInt64(reader.GetOrdinal(\"myproperty\"));\n";
            strQry += "\t\t}\n";


            strQry += "\n\t\t#endregion\n";
            strQry += "\n";



            strQry += "\t}\n}";

            return strQry;
        }

      

    }
}

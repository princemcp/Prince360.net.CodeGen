using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Mega.CodeGen
{
    class clsCommon
    {
        public string[] RefarenceToTable { get; set; }

        public DataSet GetDatabaseList(SqlConnection myCon)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("sp_helpdb", myCon);
                DataSet dsDatabase = new DataSet();
                dsDatabase.Clear();

                myCon.Open();
                adapter.Fill(dsDatabase);

                myCon.Close();
                return dsDatabase;
            }
            catch (Exception ex)
            {
                myCon.Dispose();
                throw ex;
            }
        }

        public DataTable GetColumnInfo(string TableName, SqlConnection myCon)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("exec sp_columns [" + TableName + "]", myCon);
            DataSet ds = new DataSet();
            ds.Clear();
            try
            {
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
                throw ex;
            }
            DataTable fdt = ds.Tables[0].Copy();
            //foreach (DataRow dr in fdt.Rows)
            //{
            //    //dr["Column_name"] = (TableName.Trim() + "_" + dr["Column_name"].ToString()).Trim();
            //    dr["TYPE_NAME"] = dr["TYPE_NAME"].ToString().Replace("identity", "");
            //}
            return fdt;
        }


        public string GetTableConstraint(string TableName, SqlConnection myCon)
        {
            string F_Table = "";
            try
            {

                string Identity = null;

                SqlDataAdapter adapter = new SqlDataAdapter("exec sp_help [" + TableName + "]", myCon);
                DataSet ds = new DataSet();
                ds.Clear();
                adapter.Fill(ds);
                if (ds.Tables[2].Rows[0][0].ToString() != "No identity column defined.")
                    Identity = ds.Tables[2].Rows[0][0].ToString();

                if (ds.Tables.Count < 4)
                {
                    return F_Table;
                }

                DataTable Fdt = ds.Tables[6];

                DataRow[] drr = Fdt.Select("constraint_type='PRIMARY KEY (clustered)'");
                Fdt.Rows.Remove(drr[0]);
                drr = Fdt.Select("constraint_type like '%DEFAULT on column%'");
                if (drr.Length > 0)
                {
                    foreach (DataRow dr in drr)
                    {
                        Fdt.Rows.Remove(dr);
                    }
                }
                drr = Fdt.Select("constraint_type like '%UNIQUE%'");
                if (drr.Length > 0)
                {
                    foreach (DataRow dr in drr)
                    {
                        Fdt.Rows.Remove(dr);
                    }
                }
                F_Table = "";
                string NullAble = "";
                if (Fdt.Rows.Count > 0)
                {

                    foreach (DataRow dr in Fdt.Rows)
                    {

                        if (!dr["constraint_keys"].ToString().Contains("dbo."))
                        {
                            NullAble = ds.Tables[1].Select("Column_name='" + dr["constraint_keys"].ToString() + "'")[0][6].ToString();  //Nullable"

                            F_Table = F_Table + dr["constraint_keys"].ToString().Trim() + ":";
                            if (NullAble == "no")
                            {
                                NullAble = "false";
                            }
                            else
                            {
                                NullAble = "true";
                            }
                        }
                        if (dr["constraint_keys"].ToString().Contains("dbo."))
                        {
                            string[] str = (dr["constraint_keys"].ToString()).Split('.');
                            str = str[2].ToString().Split('(');
                            F_Table = F_Table + str[0].ToString().Trim() + ":" + str[1].ToString().Replace(')', ' ').Trim() + ":" + NullAble + ",";
                        }
                    }

                    string[] FstrT = F_Table.Split(',');
                    F_Table = "";
                    for (int i = 0; i <= FstrT.Length - 2; i++)
                    {
                        if (FstrT[i].ToString().Split(':')[1] != TableName)
                        {
                            F_Table += FstrT[i].ToString().Trim() + ",";
                        }
                    }
                }
                try
                {
                    RefarenceToTable = null;
                    DataTable refToTable = ds.Tables[7];
                    RefarenceToTable = new string[refToTable.Rows.Count];
                    for (int j = 0; j <= refToTable.Rows.Count - 1; j++)
                    {
                        RefarenceToTable[j] = refToTable.Rows[j][0].ToString();
                    }
                }
                catch
                {

                }
                
                return F_Table;
            }
            catch (Exception ex)
            {
                // throw ex;
            }
            return F_Table;
        }

        public string DotNetDataTypeConvertion(int dType)
        {
            string type = "Varchar";
            switch (dType)
            {
                case -1:
                    type = "string";
                    break;
                case -2:
                    type = "Timestamp";
                    break;
                case -3:
                    type = "VarBinary";
                    break;
                case -4:
                    type = "byte[]";
                    break;
                case -5:
                    type = "long";
                    break;
                case -6:
                    type = "int";
                    break;
                case -7:
                    type = "bool";
                    break;
                case -8:
                    type = "string";
                    break;
                case -9:
                    type = "string";
                    break;
                case -10:
                    type = "string";
                    break;
                case -11:
                    type = "UniqueIdentifier";
                    break;
                case 1:
                    type = "string";
                    break;
                case 2:
                    type = "decimal";
                    break;
                case 3:
                    type = "decimal";
                    break;
                case 4:
                    type = "int";
                    break;
                case 5:
                    type = "Int16";
                    break;
                case 6:
                    type = "double";
                    break;
                case 7:
                    type = "float";
                    break;
                case 11:
                    type = "DateTime";
                    break;
                case 12:
                    type = "string";
                    break;

                default:
                    break;
            }
            return type;
        }

        public static string DataTypeConvertion(int dType)
        {
            string type = "Varchar";
            switch (dType)
            {
                case -1:
                    type = "Text";
                    break;
                case -2:
                    type = "Timestamp";
                    break;
                case -3:
                    type = "VarBinary";
                    break;
                case -4:
                    type = "Image";
                    break;
                case -5:
                    type = "BigInt";
                    break;
                case -6:
                    type = "TinyInt";
                    break;
                case -7:
                    type = "Bit";
                    break;
                case -8:
                    type = "NChar";
                    break;
                case -9:
                    type = "NVarChar";
                    break;
                case -10:
                    type = "NText";
                    break;
                case -11:
                    type = "UniqueIdentifier";
                    break;
                case 1:
                    type = "Char";
                    break;
                case 2:
                    type = "Decimal";
                    break;
                case 3:
                    type = "Decimal";
                    break;
                case 4:
                    type = "Int";
                    break;
                case 5:
                    type = "SmallInt";
                    break;
                case 6:
                    type = "Float";
                    break;
                case 7:
                    type = "Real";
                    break;
                case 11:
                    type = "DateTime";
                    break;
                case 12:
                    type = "VarChar";
                    break;

                default:
                    break;
            }
            return type;
        }

        public DataTable GetSPColumnInfo(string SPName, SqlConnection myCon)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("exec sp_helptext [" + SPName + "]", myCon);
            DataSet ds = new DataSet();
            ds.Clear();
            try
            {
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
                throw ex;
            }
            DataTable fdt = ds.Tables[0].Copy();
            return fdt;
        }
    }

    /// <summary>
    /// Mithilfe dieser kleinen Klasse, kann der aufbau des Treeviews in eine Xml Datei exportiert
    /// und natürlich auch wieder in einen TreeView Importiert werden.
    /// </summary>
    /// <example>
    /// <code lang="C#">
    /// TreeView tmpTreeview = new TreeView();
    /// 
    /// XmlHandler xmlHandler = new XmlHandler();
    /// //treeview TO Xml
    /// xmlHandler.ExportTreeToXmlFile(tmpTreeview, "C:\\temp\\tmpTreeView.xml");
    /// </code>
    /// </example>
    public class XmlHandler
    {
        static XmlDocument xmlDocument;

        /// <summary>
        /// Initialisiert eine neue Instanz der MultiClipboard Klasse.
        /// </summary>
        public XmlHandler()
        {
        }

        /// <summary>
        /// Den inhalt des TreeViews in eine xml Datei exportieren
        /// </summary>
        /// <param name="treeView">Der TreeView der exportiert werden soll</param>
        /// <param name="path">Ein  Pfad unter dem die Xml Datei entstehen soll</param>
        public static string TreeViewToXml(TreeView treeView, String path)
        {
            xmlDocument = new XmlDocument();
            xmlDocument.AppendChild(xmlDocument.CreateElement("ROOT"));
            XmlRekursivExport(xmlDocument.DocumentElement, treeView.Nodes);
            return xmlDocument.InnerXml;// (path);
        }

        /// <summary>
        /// Eine vorher Exportierte Xml Datei wieder in ein TreeView importieren
        /// </summary>
        /// <param name="path">Der Quellpfad der Xml Datei</param>
        /// <param name="treeView">Ein TreeView in dem der Inhalt der Xml Datei wieder angezeigt werden soll</param>
        /// <exception cref="FileNotFoundException">gibt an das die Datei nicht gefunden werden konnte</exception>
        public static void XmlToTreeView(String path, TreeView treeView)
        {
            xmlDocument = new XmlDocument();

            xmlDocument.LoadXml(path);
            treeView.Nodes.Clear();
            XmlRekursivImport(treeView.Nodes, xmlDocument.DocumentElement.ChildNodes);
        }

        private static XmlNode XmlRekursivExport(XmlNode nodeElement, TreeNodeCollection treeNodeCollection)
        {
            XmlNode xmlNode = null;
            foreach (TreeNode treeNode in treeNodeCollection)
            {
                xmlNode = xmlDocument.CreateElement("TreeViewNode");

                xmlNode.Attributes.Append(xmlDocument.CreateAttribute("value"));
                xmlNode.Attributes["value"].Value = treeNode.Text;
                xmlNode.Attributes.Append(xmlDocument.CreateAttribute("Checked"));
                xmlNode.Attributes["Checked"].Value = treeNode.Checked.ToString();

                if (nodeElement != null)
                    nodeElement.AppendChild(xmlNode);

                if (treeNode.Nodes.Count > 0)
                {
                    XmlRekursivExport(xmlNode, treeNode.Nodes);
                }
            }
            return xmlNode;
        }

        private static void XmlRekursivImport(TreeNodeCollection elem, XmlNodeList xmlNodeList)
        {
            TreeNode treeNode;
            foreach (XmlNode myXmlNode in xmlNodeList)
            {
                treeNode = new TreeNode(myXmlNode.Attributes["value"].Value);

                string CheckedtreeNode = myXmlNode.Attributes["Checked"].Value;
                treeNode.Checked = Convert.ToBoolean(CheckedtreeNode);
                if (myXmlNode.ChildNodes.Count > 0)
                {
                    XmlRekursivImport(treeNode.Nodes, myXmlNode.ChildNodes);
                }
                elem.Add(treeNode);
            }
        }
    }
}



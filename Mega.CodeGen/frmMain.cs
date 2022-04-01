using Mega.CodeGen.MSSQLEngine;
using Mega.CodeGen.MVC2Engine;
using Mega.CodeGen.WebAPI.netCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Mega.CodeGen
{
    public partial class frmMain : Form
    {
        public string dataSource;
        public string InitialCatalog;
        public string UserName;
        public string Password;
        public string path;
        public string[] RefarenceToTable { get; set; }

        IList<clsAllTableListEntity> objclsAllTableListEntity;
        IList<clsColumnEntity> objclsColumnEntityList;
        IList<clsTableRefEntity> objclsTableRefEntityList;

        public frmMain()
        {
            InitializeComponent();

            objclsAllTableListEntity = new List<clsAllTableListEntity>();
            objclsColumnEntityList = new List<clsColumnEntity>();
            objclsTableRefEntityList = new List<clsTableRefEntity>();
            path = "d:\\Generated Code\\Work";
        }

        private SqlConnection GetPrimaryConnection()
        {
            string con = "Data Source = " + dataSource + ";User Id = " + UserName + "; Password =" + Password + ";";
            SqlConnection myCon = new SqlConnection(con);
            return myCon;
        }

        private SqlConnection GetConnection()
        {
            string con = "Data Source = " + dataSource + "; Initial Catalog = " + InitialCatalog + "; User Id = " + UserName + "; Password =" + Password + ";";
            SqlConnection myCon = new SqlConnection(con);
            if (myCon.State.ToString() == "Closed")
            {
                myCon.Open();
            }
            return myCon;
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            this.dataSource = txtDataSource.Text;
            this.UserName = txtUserName.Text;
            this.Password = txtPassword.Text;

            clsCommon objclsCommon = new clsCommon();

            DataSet dsDatabase = objclsCommon.GetDatabaseList(GetPrimaryConnection());
            cmbDatabase.Items.Clear();
            for (int i = 0; i < dsDatabase.Tables[0].Rows.Count; i++)
            {
                cmbDatabase.Items.Add(dsDatabase.Tables[0].Rows[i]["name"].ToString());
            }
            cmbDatabase.Items.Insert(0, "[Select]");
            cmbDatabase.SelectedIndex = 0;
            SaveSystemConfigaration();

        }

        private void cmbDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDatabase.SelectedIndex != 0)
            {
                SqlConnection myCon = GetConnection();
                this.InitialCatalog = cmbDatabase.Text;
                SqlCommand myCmd = new SqlCommand("use " + InitialCatalog, myCon);

                string strQry = "sp_tables @table_owner='dbo'";
                SqlDataAdapter ad = new SqlDataAdapter(strQry, myCon);
                DataSet dsTables = new DataSet();

                try
                {
                    myCmd.ExecuteNonQuery();
                    ad.Fill(dsTables);
                    //this.cmbTables.DataSource = dsTables.Tables[0];
                    //cmbTables.DisplayMember = "TABLE_NAME";
                    tvTableList.Nodes.Clear();
                    //tvMTableList.Nodes.Clear();
                    foreach (DataRow dr in dsTables.Tables[0].Rows)
                    {
                        //path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        if (dr["TABLE_TYPE"].ToString() == "TABLE")
                        {
                            if (dr["TABLE_NAME"].ToString() != "sysdiagrams")
                            {
                                TreeNode n = new TreeNode();
                                n.Text = dr["TABLE_NAME"].ToString();
                                // GetTableCol(n.Text, n, myCon);
                                tvTableList.Nodes.Add(n);
                                //tvMTableList.Nodes.Add(dr["TABLE_NAME"].ToString());
                            }
                        }
                    }


                    // Load SP List
                    //tvSPList.Nodes.Clear();
                    //myCmd = new SqlCommand("use " + InitialCatalog, myCon);
                    //strQry = "sp_stored_procedures @sp_owner='dbo'";
                    //ad = new SqlDataAdapter(strQry, myCon);
                    //dsTables = new DataSet();
                    //myCmd.ExecuteNonQuery();
                    //ad.Fill(dsTables);
                    //foreach (DataRow dr in dsTables.Tables[0].Rows)
                    //{
                    //    if (!dr["PROCEDURE_NAME"].ToString().Contains("sp_"))
                    //    {
                    //        if (!dr["PROCEDURE_NAME"].ToString().Contains("fn_"))
                    //        {

                    //            TreeNode n = new TreeNode();
                    //            n.Text = dr["PROCEDURE_NAME"].ToString().Split(';')[0];
                    //            GetSPParam(n.Text, n, myCon);
                    //            tvSPList.Nodes.Add(n);
                    //        }
                    //    }
                    //}

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                finally
                {
                    myCon.Close();
                }
            }
        }

        private void GetSPParam(string SPName, TreeNode node, SqlConnection myCon)
        {
            SqlCommand myCmd = new SqlCommand("use " + InitialCatalog, myCon);
            string strQry = "sp_sproc_columns '" + SPName + "'";
            SqlDataAdapter ad = new SqlDataAdapter(strQry, myCon);
            DataSet dsTables = new DataSet();

            try
            {
                myCmd.ExecuteNonQuery();
                ad.Fill(dsTables);
                foreach (DataRow dr in dsTables.Tables[0].Rows)
                {
                    if (!dr["COLUMN_NAME"].ToString().Contains("RETURN_VALUE"))
                    {
                        TreeNode n = new TreeNode();
                        n.Text = dr["COLUMN_NAME"].ToString() + " " + dr["TYPE_NAME"].ToString() + "(" + dr["LENGTH"].ToString() + "),Nullable:" + dr["IS_NULLABLE"].ToString();
                        node.Nodes.Add(n);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void GetTableCol(string TableName, TreeNode node, SqlConnection myCon, string ThisColumnName, string RefColumnName, bool nullable)
        {
            clsCommon objclsCommon = new clsCommon();
            DataTable dt = objclsCommon.GetColumnInfo(TableName, myCon);
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode n = new TreeNode();
                bool? nu = null;
                if (nullable == true)
                {
                    nu = nullable;
                }
                else
                {
                    if (dr["IS_NULLABLE"].ToString() == "NO")
                    {
                        nu = false;
                    }
                    else
                    {
                        nu = true;
                    }
                }
                string PrecisionAndScale = ",Precsion:" + dr["PRECISION"].ToString() + ",Scale:" + dr["SCALE"].ToString() + ",Lenght:" + dr["LENGTH"].ToString();

                if (RefColumnName != "")
                {
                    if (rbRTRC.Checked)
                    {
                        n.Text = dr["COLUMN_NAME"].ToString() + ",AliasName:" + TableName + "_" + dr["COLUMN_NAME"].ToString() + ",DBtype:" + dr["TYPE_NAME"].ToString() + ",DotNetType:" + objclsCommon.DotNetDataTypeConvertion(Convert.ToInt32(dr["DATA_TYPE"].ToString())) + PrecisionAndScale + ",RefColumnName:" + RefColumnName + ",Nullable:" + nu.ToString();
                    }
                    else
                    {
                        n.Text = dr["COLUMN_NAME"].ToString() + ",AliasName:" + TableName + "_" + dr["COLUMN_NAME"].ToString() + "_" + ThisColumnName + ",DBtype:" + dr["TYPE_NAME"].ToString() + ",DotNetType:" + objclsCommon.DotNetDataTypeConvertion(Convert.ToInt32(dr["DATA_TYPE"].ToString())) + PrecisionAndScale + ",RefColumnName:" + RefColumnName + ",Nullable:" + nu.ToString();
                    }
                }
                else
                {
                    n.Text = dr["COLUMN_NAME"].ToString() + ",AliasName:" + dr["COLUMN_NAME"].ToString() + ",DBtype:" + dr["TYPE_NAME"].ToString() + ",DotNetType:" + objclsCommon.DotNetDataTypeConvertion(Convert.ToInt32(dr["DATA_TYPE"].ToString())) + PrecisionAndScale + ",RefColumnName:,Nullable:" + nu.ToString();
                }
                n.Checked = true;
                node.Nodes.Add(n);
            }
        }

        private void btnGetTableInfo_Click(object sender, EventArgs e)
        {
            clsCommon objclsCommon = new clsCommon();
            SqlConnection myCon = GetConnection();
            foreach (TreeNode n in tvTableList.Nodes)
            {
                if (n.Checked == true)
                {
                    tvTableInfo.Nodes.Clear();
                    TreeNode x = new TreeNode();
                    //Master Table
                    x.Text = n.Text;
                    x.Checked = true;
                    GetTableCol(n.Text, x, myCon, "", "", false);
                    tvTableInfo.Nodes.Add(x);

                    //Child Table
                    string[] RefTable = objclsCommon.GetTableConstraint(n.Text, myCon).Split(',');
                    RefarenceToTable = objclsCommon.RefarenceToTable;
                    for (int j = 0; j < RefTable.Length - 1; j++)
                    {
                        string ReferenceTableName = RefTable[j].ToString().Split(':')[1].ToString();
                        string ReferenceColumnName = RefTable[j].ToString().Split(':')[2].ToString();
                        string ThisColumnName = RefTable[j].ToString().Split(':')[0].ToString();
                        string IsNullable = RefTable[j].ToString().Split(':')[3].ToString();

                        x = new TreeNode();
                       
                            x.Text = ReferenceTableName + ",AliasName:" + ReferenceTableName + "_" + ThisColumnName + ",RefColumnName:" + ReferenceColumnName + ",ThisColumnName:" + ThisColumnName + ",Nullable:" + IsNullable;
                       
                        GetTableCol(ReferenceTableName, x, myCon, ThisColumnName, ReferenceColumnName, Convert.ToBoolean(IsNullable));
                        x.Checked = true;
                        tvTableInfo.Nodes.Add(x);

                    }

                }
            }
            tvTableInfo.ExpandAll();
            btnColapsExpanTreeView.Text = "Collapse";
        }



        private void btnSet_Click(object sender, EventArgs e)
        {
            objclsColumnEntityList = new List<clsColumnEntity>();
            objclsTableRefEntityList = new List<clsTableRefEntity>();
            foreach (TreeNode n in tvTableInfo.Nodes)
            {
                if (n.Checked == true)
                {
                    clsTableRefEntity objclsTableRefEntity = new clsTableRefEntity();
                    string[] TableDetails = n.Text.Split(',');
                    string TableName = TableDetails[0].ToString();
                    string TableAliasName = "";
                    if (TableDetails.Length < 2)
                    {

                    }
                    else
                    {
                        TableAliasName = TableDetails[1].ToString().Split(':')[1].ToString();
                    }
                    bool IsMasterTable = false;
                    if (TableAliasName == "")
                    {
                        IsMasterTable = true;
                        
                    }
                    if (IsMasterTable == false)
                    {
                        objclsTableRefEntity.TableName = TableName;
                        objclsTableRefEntity.TableAliasName = TableAliasName;
                        objclsTableRefEntity.RefColumnName = TableDetails[2].ToString().Split(':')[1].ToString();
                        objclsTableRefEntity.ThisColumnName = TableDetails[3].ToString().Split(':')[1].ToString();
                        objclsTableRefEntity.RefIsNull = Convert.ToBoolean(TableDetails[4].ToString().Split(':')[1].ToString());
                        
                        objclsTableRefEntityList.Add(objclsTableRefEntity);
                    }
                    int x = 0;
                    foreach (TreeNode cnode in n.Nodes)
                    {
                        if (cnode.Checked == true)
                        {
                            clsColumnEntity objclsColumnEntity = new clsColumnEntity();
                            string[] ColumnDetails = cnode.Text.Split(',');

                            objclsColumnEntity.TableName = TableName;
                            objclsColumnEntity.TableAliasName = TableAliasName;
                            objclsColumnEntity.IsMasterTable = IsMasterTable;

                            objclsColumnEntity.ColumnName = ColumnDetails[0].ToString();
                            objclsColumnEntity.ColumnAliasName = ColumnDetails[1].ToString().Split(':')[1].ToString();
                            objclsColumnEntity.RefColumnName = ColumnDetails[7].ToString().Split(':')[1].ToString();

                            objclsColumnEntity.Lable = objclsColumnEntity.ColumnAliasName;

                            objclsColumnEntity.Precsion = ColumnDetails[4].ToString().Split(':')[1].ToString();
                            objclsColumnEntity.Scale = ColumnDetails[5].ToString().Split(':')[1].ToString();
                            objclsColumnEntity.Lenght = ColumnDetails[6].ToString().Split(':')[1].ToString();

                            objclsColumnEntity.ColumnDBType = ColumnDetails[2].ToString().Split(':')[1].ToString();
                            objclsColumnEntity.ColumnDotNetType = ColumnDetails[3].ToString().Split(':')[1].ToString();
                            objclsColumnEntity.ColumnIsNull = Convert.ToBoolean(ColumnDetails[8].ToString().Split(':')[1].ToString());
                            objclsColumnEntity.SelectForGetSP = true;
                            objclsColumnEntity.RefarenceToTable = this.RefarenceToTable;
                            if (IsMasterTable == false)
                            {
                                objclsColumnEntity.SelectForGetSP = false;
                            }
                                if (x < 4 && IsMasterTable == true)
                            {
                                objclsColumnEntity.GetSPParam = true;
                                objclsColumnEntity.SearchSPParam = true;

                                objclsColumnEntity.SPOrderBy = true;
                            }

                            objclsColumnEntityList.Add(objclsColumnEntity);
                            x++;
                        }
                    }
                }
            }
            FindHasReferance();
            //dgView.DataSource = objclsColumnEntityList;
            //dgTableRef.DataSource = objclsTableRefEntityList;
            SETAllTableList(tvTableInfo.Nodes[0].Text, objclsTableRefEntityList, objclsColumnEntityList);
        }

        private void SETAllTableList(string TableName, IList<clsTableRefEntity> objclsTableRefEntityList, IList<clsColumnEntity> objclsColumnEntityList)
        {
            clsAllTableListEntity oclsAllTableListEntity = new clsAllTableListEntity();
            oclsAllTableListEntity.TableName = TableName;
            oclsAllTableListEntity.TableRefEntity = (List<clsTableRefEntity>)objclsTableRefEntityList;
            oclsAllTableListEntity.ColumnEntity = (List<clsColumnEntity>)objclsColumnEntityList;
            // oclsAllTableListEntity.ObjTreeNode = TreeViewToXml.exportToXml2(tvTableInfo, TableName);
            // oclsAllTableListEntity.ObjTreeNode = TreeViewToXml.Test();
            String Path = "abc.xml";

            oclsAllTableListEntity.ObjTreeNode = XmlHandler.TreeViewToXml(tvTableInfo, Path); ;

            if (objclsAllTableListEntity.Count >= 0)
            {
                List<clsAllTableListEntity> obj = objclsAllTableListEntity.Where(m => m.TableName == TableName).ToList();
                if (obj.Count == 0)
                {
                    objclsAllTableListEntity.Insert(0, oclsAllTableListEntity);
                }
                else
                {
                    obj[0].TableRefEntity = (List<clsTableRefEntity>)objclsTableRefEntityList;
                    obj[0].ColumnEntity = (List<clsColumnEntity>)objclsColumnEntityList;
                    obj[0].ObjTreeNode = XmlHandler.TreeViewToXml(tvTableInfo, Path); ;
                }
            }
            SETListBox(objclsAllTableListEntity);
        }

        private void SETListBox(IList<clsAllTableListEntity> obj)
        {
            lbSelectedTableList1.Items.Clear();
            lbSelectedTableList2.Items.Clear();
            foreach (var n in obj)
            {
                lbSelectedTableList1.Items.Add(n);
                lbSelectedTableList2.Items.Add(n);
            }
        }


        private void FindHasReferance()
        {
            List<clsTableRefEntity> objclsAllTableRefEntityList = new List<clsTableRefEntity>();
            foreach (TreeNode n in tvTableInfo.Nodes)
            {
                clsTableRefEntity objclsTableRefEntity = new clsTableRefEntity();
                string[] TableDetails = n.Text.Split(',');
                string TableName = TableDetails[0].ToString();
                string TableAliasName = "";
                if (TableDetails.Length < 2)
                {

                }
                else
                {
                    TableAliasName = TableDetails[1].ToString().Split(':')[1].ToString();
                }
                bool IsMasterTable = false;
                if (TableAliasName == "")
                {
                    IsMasterTable = true;
                }
                if (IsMasterTable == false)
                {
                    objclsTableRefEntity.TableName = TableName;
                    objclsTableRefEntity.TableAliasName = TableAliasName;
                    objclsTableRefEntity.RefColumnName = TableDetails[2].ToString().Split(':')[1].ToString();
                    objclsTableRefEntity.ThisColumnName = TableDetails[3].ToString().Split(':')[1].ToString();
                    objclsTableRefEntity.RefIsNull = Convert.ToBoolean(TableDetails[4].ToString().Split(':')[1].ToString());
                    objclsAllTableRefEntityList.Add(objclsTableRefEntity);
                }
            }
            foreach (var colList in objclsColumnEntityList)
            {
                if (colList.IsMasterTable == true)
                {
                    foreach (var AllTableList in objclsAllTableRefEntityList)
                    {
                        if (AllTableList.ThisColumnName == colList.ColumnName)
                        {
                            colList.HasReference = true;
                            colList.RefColumnName = AllTableList.RefColumnName;
                            colList.ReferenceTableName = AllTableList.TableName;
                            int x = 0;
                            foreach (var TableList in objclsTableRefEntityList)
                            {
                                if (TableList.TableName == AllTableList.TableName)
                                {
                                    x++;
                                }
                            }
                            if (x == 0)
                            {
                                colList.IsHidden = true;
                            }
                            else
                            {
                                colList.IsHidden = false;
                            }
                        }
                    }
                }
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            txtPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Generated Code";
            LoadPreviousWorkList();
            LoadSystemConfigaration();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            bool CreationStatus = false;
            string msg = "";
            foreach (clsAllTableListEntity TablelistEntity in objclsAllTableListEntity)
            {
                ListBox.SelectedObjectCollection SelectedTables = lbSelectedTableList2.SelectedItems;
                if (SelectedTables.Count <= 0)
                {
                    MessageBox.Show("No Table Selected!!!");
                    return;
                }
                foreach (clsAllTableListEntity SelectedTable in SelectedTables)
                {
                    if (TablelistEntity.TableName == SelectedTable.TableName)
                    {
                        if (TablelistEntity.ColumnEntity == null || TablelistEntity.ColumnEntity.Count < 1)
                        {
                            MessageBox.Show("No Table Selected!!!");
                            return;
                        }

                        // For SP Only ----------------------------------------------------
                        if (chkSPGet.Checked == true)
                        {
                            GetSPEngine objGetSPEngine = new GetSPEngine();
                            CreationStatus = objGetSPEngine.GetSPEngineMethod(TablelistEntity.TableRefEntity, TablelistEntity.ColumnEntity, txtNamespace.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "SPGet" + TablelistEntity.ColumnEntity[0].TableName + ".sql created.\n";
                            else
                                msg += "SPGet" + TablelistEntity.ColumnEntity[0].TableName + ".sql not created.\n";
                        }
                        if (chkSPSearch.Checked == true)
                        {
                            SearchSPEngine objSearchSPEngine = new SearchSPEngine();
                            CreationStatus = objSearchSPEngine.SearchSPEngineMethod(TablelistEntity.TableRefEntity, TablelistEntity.ColumnEntity, txtNamespace.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "SPSearch" + TablelistEntity.ColumnEntity[0].TableName + ".sql created.\n";
                            else
                                msg += "SPSearch" + TablelistEntity.ColumnEntity[0].TableName + ".sql not created.\n";
                        }
                        if (chkSP_GA.Checked == true)
                        {
                            SP_GA objGetSPEngine = new SP_GA();
                            CreationStatus = objGetSPEngine.GetSPEngineMethod(TablelistEntity.TableRefEntity, TablelistEntity.ColumnEntity, txtNamespace.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + "_GA.sql created.\n";
                            else
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + "_GA.sql not created.\n";
                        }
                        if (chkSP_GAPg.Checked == true)
                        {
                            SP_GAPg objGetSPEngine = new SP_GAPg();
                            CreationStatus = objGetSPEngine.GetSPEngineMethod(TablelistEntity.TableRefEntity, TablelistEntity.ColumnEntity, txtNamespace.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + "_GAPg.sql created.\n";
                            else
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + "_GAPg.sql not created.\n";
                        }
                        if (chkSP_Ins.Checked == true)
                        {
                            SP_Ins objGetSPEngine = new SP_Ins();
                            CreationStatus = objGetSPEngine.GetSPEngineMethod(TablelistEntity.TableRefEntity, TablelistEntity.ColumnEntity, txtNamespace.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + "_Ins.sql created.\n";
                            else
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + "_Ins.sql not created.\n";
                        }
                        if (chkSP_Upd.Checked == true)
                        {
                            SP_Upd objGetSPEngine = new SP_Upd();
                            CreationStatus = objGetSPEngine.GetSPEngineMethod(TablelistEntity.TableRefEntity, TablelistEntity.ColumnEntity, txtNamespace.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + "_Upd.sql created.\n";
                            else
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + "_Upd.sql not created.\n";
                        }
                        if (chkSP_Del.Checked == true)
                        {
                            SP_Del objGetSPEngine = new SP_Del();
                            CreationStatus = objGetSPEngine.GetSPEngineMethod(TablelistEntity.TableRefEntity, TablelistEntity.ColumnEntity, txtNamespace.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + "_Del.sql created.\n";
                            else
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + "_Del.sql not created.\n";
                        }
                        //----------------------------------------------------------------------------


                        // For WebApiCore Only ----------------------------------------------------
                        // Entity
                        if (chkWebApiCoreEntity.Checked == true)
                        {
                            IList<clsColumnEntity> objLocalclsColumnEntityList = CloneList(TablelistEntity.ColumnEntity);
                            Entity objEntityEngine = new Entity();
                            CreationStatus = objEntityEngine.EntityEngineMethod(TablelistEntity.TableRefEntity, objLocalclsColumnEntityList, txtNamespace.Text + ".Entities." + txtModuleName.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += TablelistEntity.ColumnEntity[0].TableName + "Entity.cs created.\n";
                            else
                                msg += TablelistEntity.ColumnEntity[0].TableName + "Entity.cs not created.\n";
                        }

                        // iDAL
                        if (chkWebApiCoreIDAL.Checked == true)
                        {
                            IList<clsColumnEntity> objLocalclsColumnEntityList = CloneList(TablelistEntity.ColumnEntity);
                            iDAL objEntityEngine = new iDAL();
                            CreationStatus = objEntityEngine.iDALEngineMethod(TablelistEntity.TableRefEntity, objLocalclsColumnEntityList, txtNamespace.Text + ".iDAL." + txtModuleName.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "i" + TablelistEntity.ColumnEntity[0].TableName + ".cs created.\n";
                            else
                                msg += "i" + TablelistEntity.ColumnEntity[0].TableName + ".cs not created.\n";
                        }
                        
                        // DAL
                        if (chkWebApiCoreDAL.Checked == true)
                        {
                            IList<clsColumnEntity> objLocalclsColumnEntityList = CloneList(TablelistEntity.ColumnEntity);
                            DAL objEntityEngine = new DAL();
                            CreationStatus = objEntityEngine.DALEngineMethod(TablelistEntity.TableRefEntity, objLocalclsColumnEntityList, txtNamespace.Text + ".DAL." + txtModuleName.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + ".cs created.\n";
                            else
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + ".cs not created.\n";
                        }

                        // iBLL
                        if (chkWebApiCoreIBLL.Checked == true)
                        {
                            IList<clsColumnEntity> objLocalclsColumnEntityList = CloneList(TablelistEntity.ColumnEntity);
                            iBLL objEntityEngine = new iBLL();
                            CreationStatus = objEntityEngine.iBLLEngineMethod(TablelistEntity.TableRefEntity, objLocalclsColumnEntityList, txtNamespace.Text + ".iBLL." + txtModuleName.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "i" + TablelistEntity.ColumnEntity[0].TableName + "_BLL.cs created.\n";
                            else
                                msg += "i" + TablelistEntity.ColumnEntity[0].TableName + "_BLL.cs not created.\n";
                        }

                        // BLL
                        if (chkWebApiCoreBLL.Checked == true)
                        {
                            IList<clsColumnEntity> objLocalclsColumnEntityList = CloneList(TablelistEntity.ColumnEntity);
                            BLL objEntityEngine = new BLL();
                            CreationStatus = objEntityEngine.BLLEngineMethod(TablelistEntity.TableRefEntity, objLocalclsColumnEntityList, txtNamespace.Text + ".BLL." + txtModuleName.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + "_BLL.cs created.\n";
                            else
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + "_BLL.cs not created.\n";
                        }

                        // Entity_Ext
                        if (chkWebApiCoreEntity_Ext.Checked == true)
                        {
                            IList<clsColumnEntity> objLocalclsColumnEntityList = CloneList(TablelistEntity.ColumnEntity);
                            Entity_Ext objEntityEngine = new Entity_Ext();
                            CreationStatus = objEntityEngine.Entity_ExtEngineMethod(TablelistEntity.TableRefEntity, objLocalclsColumnEntityList, txtNamespace.Text + ".Entities." + txtModuleName.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += TablelistEntity.ColumnEntity[0].TableName + "Entity_Ext.cs created.\n";
                            else
                                msg += TablelistEntity.ColumnEntity[0].TableName + "Entity_Ext.cs not created.\n";
                        }


                        // iDAL_Ext
                        if (chkWebApiCoreIDAL_Ext.Checked == true)
                        {
                            IList<clsColumnEntity> objLocalclsColumnEntityList = CloneList(TablelistEntity.ColumnEntity);
                            iDAL_Ext objEntityEngine = new iDAL_Ext();
                            CreationStatus = objEntityEngine.iDAL_ExtEngineMethod(TablelistEntity.TableRefEntity, objLocalclsColumnEntityList, txtNamespace.Text + ".iDAL." + txtModuleName.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "i"+ TablelistEntity.ColumnEntity[0].TableName + ".cs(Ext) created.\n";
                            else
                                msg += "i" + TablelistEntity.ColumnEntity[0].TableName + ".cs(Ext) not created.\n";
                        }

                        // DAL_Ext
                        if (chkWebApiCoreDAL_Ext.Checked == true)
                        {
                            IList<clsColumnEntity> objLocalclsColumnEntityList = CloneList(TablelistEntity.ColumnEntity);
                            DAL_Ext objEntityEngine = new DAL_Ext();
                            CreationStatus = objEntityEngine.DAL_ExtEngineMethod(TablelistEntity.TableRefEntity, objLocalclsColumnEntityList, txtNamespace.Text + ".DAL." + txtModuleName.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += TablelistEntity.ColumnEntity[0].TableName + ".cs(Ext) created.\n";
                            else
                                msg += TablelistEntity.ColumnEntity[0].TableName + ".cs not(Ext) created.\n";
                        }

                        // iBLL_Ext
                        if (chkWebApiCoreIBLL_Ext.Checked == true)
                        {
                            IList<clsColumnEntity> objLocalclsColumnEntityList = CloneList(TablelistEntity.ColumnEntity);
                            iBLL_Ext objEntityEngine = new iBLL_Ext();
                            CreationStatus = objEntityEngine.iBLL_ExtEngineMethod(TablelistEntity.TableRefEntity, objLocalclsColumnEntityList, txtNamespace.Text + ".iBLL." + txtModuleName.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "i" + TablelistEntity.ColumnEntity[0].TableName + "_BLL.cs(Ext) created.\n";
                            else
                                msg += "i" + TablelistEntity.ColumnEntity[0].TableName + "_BLL.cs not(Ext) created.\n";
                        }

                        // BLL_Ext
                        if (chkWebApiCoreBLL_Ext.Checked == true)
                        {
                            IList<clsColumnEntity> objLocalclsColumnEntityList = CloneList(TablelistEntity.ColumnEntity);
                            BLL_Ext objEntityEngine = new BLL_Ext();
                            CreationStatus = objEntityEngine.BLL_ExtEngineMethod(TablelistEntity.TableRefEntity, objLocalclsColumnEntityList, txtNamespace.Text + ".BLL." + txtModuleName.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + "_BLL.cs(Ext) created.\n";
                            else
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + "_BLL.cs not(Ext) created.\n";
                        }

                        // FCC 

                        if (chkWebApiCoreFCC.Checked == true)
                        {
                            IList<clsColumnEntity> objLocalclsColumnEntityList = CloneList(TablelistEntity.ColumnEntity);
                            FCC objEntityEngine = new FCC();
                            CreationStatus = objEntityEngine.FCCEngineMethod(TablelistEntity.TableRefEntity, objLocalclsColumnEntityList, txtNamespace.Text + ".FCC." + txtModuleName.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + "FCC.cs(Ext) created.\n";
                            else
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + "FCC.cs not(Ext) created.\n";
                        }

                        // Controller
                        if (chkWebApiController.Checked == true)
                        {
                            IList<clsColumnEntity> objLocalclsColumnEntityList = CloneList(TablelistEntity.ColumnEntity);
                            Controller objEntityEngine = new Controller();
                            CreationStatus = objEntityEngine.ControllerEngineMethod(TablelistEntity.TableRefEntity, objLocalclsColumnEntityList, txtNamespace.Text + ".Controller." + txtModuleName.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + "Controller.cs created.\n";
                            else
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + "Controller.cs not created.\n";
                        }
                        //----------------------------------------------------------------------------

                        if (chkEntity.Checked == true)
                        {
                            IList<clsColumnEntity> objLocalclsColumnEntityList = CloneList(TablelistEntity.ColumnEntity);
                            EntityEngine objEntityEngine = new EntityEngine();
                            CreationStatus = objEntityEngine.EntityEngineMethod(TablelistEntity.TableRefEntity, objLocalclsColumnEntityList, txtNamespace.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += TablelistEntity.ColumnEntity[0].TableName + "Entity.cs created.\n";
                            else
                                msg += TablelistEntity.ColumnEntity[0].TableName + "Entity.cs not created.\n";
                        }
                        if (chkEntityRef.Checked == true)
                        {
                            IList<clsColumnEntity> objLocalclsColumnEntityList = CloneList(TablelistEntity.ColumnEntity);
                            EntityRefEngine objEntityRefEngine = new EntityRefEngine();
                            CreationStatus = objEntityRefEngine.EntityRefEngineMethod(TablelistEntity.TableRefEntity, objLocalclsColumnEntityList, txtNamespace.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += TablelistEntity.ColumnEntity[0].TableName + "EntityRef.cs created.\n";
                            else
                                msg += TablelistEntity.ColumnEntity[0].TableName + "EntityRef.cs not created.\n";
                        }
                        if (chkBLL.Checked == true)
                        {
                            IList<clsColumnEntity> objLocalclsColumnEntityList = CloneList(TablelistEntity.ColumnEntity);
                            BLLEngine objBLLEngine = new BLLEngine();
                            CreationStatus = objBLLEngine.EntityEngineMethod(TablelistEntity.TableRefEntity, objLocalclsColumnEntityList, txtNamespace.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "B" + TablelistEntity.ColumnEntity[0].TableName + ".cs created.\n";
                            else
                                msg += "B" + TablelistEntity.ColumnEntity[0].TableName + ".cs not created.\n";
                        }
                        if (chkEntityMappers.Checked == true)
                        {
                            IList<clsColumnEntity> objLocalclsColumnEntityList = CloneList(TablelistEntity.ColumnEntity);
                            EntitymappersEngine objEntitymappersEngine = new EntitymappersEngine();
                            CreationStatus = objEntitymappersEngine.EntitymappersEngineMethod(TablelistEntity.TableRefEntity, objLocalclsColumnEntityList, txtNamespace.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "EM" + TablelistEntity.ColumnEntity[0].TableName + ".cs created.\n";
                            else
                                msg += "EM" + TablelistEntity.ColumnEntity[0].TableName + ".cs not created.\n";
                        }
                        if (chkEntityMappersRef.Checked == true)
                        {
                            IList<clsColumnEntity> objLocalclsColumnEntityList = CloneList(TablelistEntity.ColumnEntity);
                            EntitymappersRefEngine objEntitymappersRefEngine = new EntitymappersRefEngine();
                            CreationStatus = objEntitymappersRefEngine.EntitymappersRefEngineMethod(TablelistEntity.TableRefEntity, objLocalclsColumnEntityList, txtNamespace.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "EM" + TablelistEntity.ColumnEntity[0].TableName + "Ref.cs created.\n";
                            else
                                msg += "EM" + TablelistEntity.ColumnEntity[0].TableName + "Ref.cs not created.\n";
                        }
                        if (chkIDAL.Checked == true)
                        {
                            IList<clsColumnEntity> objLocalclsColumnEntityList = CloneList(TablelistEntity.ColumnEntity);
                            IDALEngine objIDALEngine = new IDALEngine();
                            CreationStatus = objIDALEngine.IDALEngineMethod(TablelistEntity.TableRefEntity, objLocalclsColumnEntityList, txtNamespace.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "I" + TablelistEntity.ColumnEntity[0].TableName + ".cs created.\n";
                            else
                                msg += "I" + TablelistEntity.ColumnEntity[0].TableName + ".cs not created.\n";
                        }
                        if (chkDAL.Checked == true)
                        {
                            IList<clsColumnEntity> objLocalclsColumnEntityList = CloneList(TablelistEntity.ColumnEntity);
                            DALEngine objDALEngine = new DALEngine();
                            CreationStatus = objDALEngine.DALEngineMethod(TablelistEntity.TableRefEntity, objLocalclsColumnEntityList, txtNamespace.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "D" + TablelistEntity.ColumnEntity[0].TableName + ".cs created.\n";
                            else
                                msg += "D" + TablelistEntity.ColumnEntity[0].TableName + ".cs not created.\n";
                        }
                        if (chkControllers.Checked == true)
                        {
                            IList<clsColumnEntity> objLocalclsColumnEntityList = CloneList(TablelistEntity.ColumnEntity);
                            ControllersEngine objControllersEngine = new ControllersEngine();
                            CreationStatus = objControllersEngine.ControllersEngineMethod(TablelistEntity.TableRefEntity, objLocalclsColumnEntityList, txtNamespace.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + "Controller.cs created.\n";
                            else
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + "Controller.cs not created.\n";
                        }
                        if (chkJQScript.Checked == true)
                        {
                            IList<clsColumnEntity> objLocalclsColumnEntityList = CloneList(TablelistEntity.ColumnEntity);
                            JQScriptsEngine objJQScriptsEngine = new JQScriptsEngine();
                            CreationStatus = objJQScriptsEngine.JQScriptsEngineMethod(TablelistEntity.TableRefEntity, objLocalclsColumnEntityList, txtNamespace.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + ".js created.\n";
                            else
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + ".js not created.\n";
                        }
                        if (chkModel.Checked == true)
                        {
                            IList<clsColumnEntity> objLocalclsColumnEntityList = CloneList(TablelistEntity.ColumnEntity);
                            ModelEngine objModelEngine = new ModelEngine();
                            CreationStatus = objModelEngine.ModelEngineMethod(TablelistEntity.TableRefEntity, objLocalclsColumnEntityList, txtNamespace.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "View" + TablelistEntity.ColumnEntity[0].TableName + ".cs created.\n";
                            else
                                msg += "View" + TablelistEntity.ColumnEntity[0].TableName + ".cs not created.\n";
                        }
                        if (chkIndex.Checked == true)
                        {
                            IList<clsColumnEntity> objLocalclsColumnEntityList = CloneList(TablelistEntity.ColumnEntity);
                            Index objIndex = new Index();
                            CreationStatus = objIndex.IndexMethod(TablelistEntity.TableRefEntity, objLocalclsColumnEntityList, txtNamespace.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + "-- Index.aspx created.\n";
                            else
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + "-- Index.aspx not created.\n";
                        }
                        if (chkCreate.Checked == true)
                        {
                            IList<clsColumnEntity> objLocalclsColumnEntityList = CloneList(TablelistEntity.ColumnEntity);
                            Create objCreate = new Create();
                            CreationStatus = objCreate.CreateMethod(TablelistEntity.TableRefEntity, objLocalclsColumnEntityList, txtNamespace.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + "-- Create.aspx created.\n";
                            else
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + "-- Create.aspx not created.\n";
                        }
                        if (chkEdit.Checked == true)
                        {
                            IList<clsColumnEntity> objLocalclsColumnEntityList = CloneList(TablelistEntity.ColumnEntity);
                            Edit objEdit = new Edit();
                            CreationStatus = objEdit.EditMethod(TablelistEntity.TableRefEntity, objLocalclsColumnEntityList, txtNamespace.Text, TablelistEntity.ColumnEntity[0].TableName, txtPath.Text, cmbDatabase.Text);
                            if (CreationStatus)
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + "-- Edit.aspx created.\n";
                            else
                                msg += "" + TablelistEntity.ColumnEntity[0].TableName + "-- Edit.aspx not created.\n";
                        }
                    }
                }
            }
            if (chkMDJQScript.Checked == true)
            {
                MD_JQScriptEngine objMD_JQScriptEngine = new MD_JQScriptEngine();
                CreationStatus = objMD_JQScriptEngine.MD_JQScriptEngineMethod(txtNamespace.Text, lbMasterTableList, lbChildTableList, txtPath.Text);
                if (CreationStatus)
                    msg += "Master Details -- JS created.\n";
                else
                    msg += "Master Details -- JS not created.\n";
            }
            if (chkMDController.Checked == true)
            {
                MD_ControllersEngine objMD_ControllersEngine = new MD_ControllersEngine();
                CreationStatus = objMD_ControllersEngine.MD_ControllersEngineMethod(txtNamespace.Text, lbMasterTableList, lbChildTableList, txtPath.Text);
                if (CreationStatus)
                    msg += "Master Details -- Controller created.\n";
                else
                    msg += "Master Details -- Controller not created.\n";
            }
            if (chkMDCreate.Checked == true)
            {
                MD_Create objMD_Create = new MD_Create();
                CreationStatus = objMD_Create.MD_CreateMethod(txtNamespace.Text, lbMasterTableList, lbChildTableList, txtPath.Text);
                if (CreationStatus)
                    msg += "Master Details View -- Create.aspx created.\n";
                else
                    msg += "Master Details View -- Create.aspx not created.\n";
            }
            SaveCurrentTablesInfo();
            MessageBox.Show(msg);
        }


        public IList<clsColumnEntity> CloneList(IList<clsColumnEntity> source)
        {
            IList<clsColumnEntity> objLocalclsColumnEntityList = new List<clsColumnEntity>();
            foreach (clsColumnEntity obj in source)
            {
                objLocalclsColumnEntityList.Add((clsColumnEntity)obj.Clone());
            }
            return objLocalclsColumnEntityList;
        }

        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        

        private void btnColapsExpanTreeView_Click(object sender, EventArgs e)
        {
            if (btnColapsExpanTreeView.Text == "Collapse")
            {
                tvTableInfo.CollapseAll();
                btnColapsExpanTreeView.Text = "Expand";
            }
            else
            {
                tvTableInfo.ExpandAll();
                btnColapsExpanTreeView.Text = "Collapse";
            }
        }


        private void btnMVCSelectAll_Click(object sender, EventArgs e)
        {
            if (btnMVCSelectAll.Text == "Select All")
            {
                btnMVCSelectAll.Text = "Unselect All";
                Checked(true);
            }
            else
            {
                btnMVCSelectAll.Text = "Select All";
                Checked(false);
            }
        }
        private void Checked(bool Checked)
        {
            //chkSPSet.Checked = Checked;
            //chkSPGet.Checked = Checked;
            chkCreate.Checked = Checked;
            chkIndex.Checked = Checked;
            chkControllers.Checked = Checked;
            chkJQScript.Checked = Checked;
            chkBLL.Checked = Checked;
            chkDAL.Checked = Checked;
            chkIDAL.Checked = Checked;
            chkEntityMappers.Checked = Checked;
            chkEntity.Checked = Checked;
            chkModel.Checked = Checked;
            chkEntityRef.Checked = Checked;
            chkEntityMappersRef.Checked = Checked;
            chkEdit.Checked = Checked;
            //chkSUIcs.Checked = Checked;
            //chkUI.Checked = Checked;
        }

        private void lbSelectedTableList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            clsAllTableListEntity entity = (clsAllTableListEntity)lbSelectedTableList2.SelectedItem;
            if (entity != null)
            {
                List<clsAllTableListEntity> obj = objclsAllTableListEntity.Where(m => m.TableName == entity.TableName).ToList();
                dgView.DataSource = entity.ColumnEntity;
                dgTableRef.DataSource = entity.TableRefEntity;
            }
        }

        private void btnSetFinal_Click(object sender, EventArgs e)
        {
            clsAllTableListEntity entity = (clsAllTableListEntity)lbSelectedTableList2.SelectedItem;
            if (entity != null)
            {
                lbMasterTableList.Items.Remove(entity);
                lbChildTableList.Items.Remove(entity);
                List<clsAllTableListEntity> obj = objclsAllTableListEntity.Where(m => m.TableName == entity.TableName).ToList();
                entity.ColumnEntity = (List<clsColumnEntity>)dgView.DataSource;
            }
        }





        private void btnRemove_Click(object sender, EventArgs e)
        {
            clsAllTableListEntity entity = (clsAllTableListEntity)lbSelectedTableList2.SelectedItem;
            objclsAllTableListEntity.Remove(entity);
            lbSelectedTableList1.Items.Clear();
            lbSelectedTableList2.Items.Clear();
            lbMasterTableList.Items.Remove(entity);
            lbChildTableList.Items.Remove(entity);
            foreach (var n in objclsAllTableListEntity)
            {
                lbSelectedTableList1.Items.Add(n);
                lbSelectedTableList2.Items.Add(n);
            }

            dgView.DataSource = new List<clsColumnEntity>();
            dgTableRef.DataSource = new List<clsTableRefEntity>();
        }

        private void btnSetMaster_Click(object sender, EventArgs e)
        {
            clsAllTableListEntity entity = (clsAllTableListEntity)lbSelectedTableList2.SelectedItem;
            foreach (clsAllTableListEntity list in lbMasterTableList.Items)
            {
                if (list.TableName == entity.TableName)
                {
                    lbMasterTableList.Items.Remove(list);
                    break;
                }
            }
            lbMasterTableList.Items.Add(entity);
        }

        private void btnSetChild_Click(object sender, EventArgs e)
        {
            clsAllTableListEntity entity = (clsAllTableListEntity)lbSelectedTableList2.SelectedItem;
            foreach (clsAllTableListEntity list in lbSelectedTableList2.Items)
            {
                if (list.TableName == entity.TableName)
                {
                    lbChildTableList.Items.Remove(list);
                    break;
                }
            }
            lbChildTableList.Items.Add(entity);
        }

        private void btnSaveXML_Click(object sender, EventArgs e)
        {
            SaveCurrentTablesInfo();
        }

        private void SaveCurrentTablesInfo()
        {
            try
            {
                XDocument doc = new XDocument();
                SerializeParams(doc, objclsAllTableListEntity);


                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string Spath = path + "\\" + txtNamespace.Text + ".xml";

                doc.Save(Spath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadPreviousWorkList()
        {
            try
            {
                cmbFileList.Items.Add("Select");
                string[] filePaths = Directory.GetFiles(path);
                foreach (string s in filePaths)
                {
                    string[] fileName = s.Split('\\');
                    if (fileName[fileName.Length - 1] != "SystemConfiguration.xml")
                    {
                        cmbFileList.Items.Add(fileName[fileName.Length - 1]);
                    }
                }
                cmbFileList.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadPreviousWork(string NameSpace)
        {
            try
            {
                string Spath = path + "\\" + NameSpace + ".xml";

                XDocument doc = XDocument.Load(Spath);

                objclsAllTableListEntity = DeserializeParams(doc);
                SETListBox(objclsAllTableListEntity);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SerializeParams(XDocument doc, IList<clsAllTableListEntity> paramList)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(paramList.GetType());

            System.Xml.XmlWriter writer = doc.CreateWriter();

            serializer.Serialize(writer, paramList);

            writer.Close();
        }

        private List<clsAllTableListEntity> DeserializeParams(XDocument doc)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<clsAllTableListEntity>));

            System.Xml.XmlReader reader = doc.CreateReader();

            List<clsAllTableListEntity> result = (List<clsAllTableListEntity>)serializer.Deserialize(reader);
            reader.Close();

            return result;
        }

        private void btnLoadXML_Click(object sender, EventArgs e)
        {
            if (cmbFileList.Text != "Select")
            {
                string NameSpace = cmbFileList.Text.Replace(".xml", "");
                LoadPreviousWork(NameSpace);
                txtNamespace.Text = NameSpace;
            }
        }

        private void lbSelectedTableList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            clsAllTableListEntity entity = (clsAllTableListEntity)lbSelectedTableList1.SelectedItem;
            if (entity != null)
            {
                List<clsAllTableListEntity> obj = objclsAllTableListEntity.Where(m => m.TableName == entity.TableName).ToList();

                XmlHandler.XmlToTreeView(obj[0].ObjTreeNode, tvTableInfo);

                tvTableInfo.ExpandAll();
            }
        }

        private void SaveSystemConfigaration()
        {
            XDocument doc = new XDocument();
            SystemConfigurationEntity obj = new SystemConfigurationEntity();

            obj.SQLServerName = txtDataSource.Text;
            obj.SQLServerUserName = txtUserName.Text;
            obj.SQLServerPassword = txtPassword.Text;

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());

            System.Xml.XmlWriter writer = doc.CreateWriter();

            serializer.Serialize(writer, obj);

            writer.Close();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string Spath = path + "\\" + "SystemConfiguration.xml";

            doc.Save(Spath);
        }

        private void LoadSystemConfigaration()
        {
            string Spath = path + "\\" + "SystemConfiguration.xml";
            try
            {
                XDocument doc = XDocument.Load(Spath);

                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(SystemConfigurationEntity));

                System.Xml.XmlReader reader = doc.CreateReader();

                SystemConfigurationEntity result = (SystemConfigurationEntity)serializer.Deserialize(reader);
                reader.Close();

                txtDataSource.Text = result.SQLServerName;
                txtUserName.Text = result.SQLServerUserName;
                txtPassword.Text = result.SQLServerPassword;
            }
            catch (Exception ex)
            {

            }
        }

        private void btnRemoveMasterList_Click(object sender, EventArgs e)
        {
            clsAllTableListEntity entity = (clsAllTableListEntity)lbMasterTableList.SelectedItem;
            if (entity != null)
            {
                lbMasterTableList.Items.Remove(entity);
            }
        }

        private void btnRemoveChildList_Click(object sender, EventArgs e)
        {
            clsAllTableListEntity entity = (clsAllTableListEntity)lbChildTableList.SelectedItem;
            if (entity != null)
            {
                lbChildTableList.Items.Remove(entity);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnGetandSet_Click(object sender, EventArgs e)
        {
            btnGetTableInfo_Click(sender, e);
            btnSet_Click(sender, e);
        }

        private void BtnCheckSPAll_Click(object sender, EventArgs e)
        {
            if (btnCheckSPAll.Text == "Check All")
            {
                CheckAllSP(true);
                btnCheckSPAll.Text = "Uncheck All";
                return;
            }
            CheckAllSP(false);
            btnCheckSPAll.Text = "Check All";
        }

        private void CheckAllSP(bool val)
        {
            chkSP_GA.Checked = val;
            chkSP_GAPg.Checked = val;
            chkSP_Ins.Checked = val;
            chkSP_Upd.Checked = val;
            chkSP_Del.Checked = val;
        }

        private void BtnchkWebApiCoreCheck_Click(object sender, EventArgs e)
        {
            if (btnchkWebApiCoreCheck.Text == "Check All")
            {
                CheckAllWebApiCore(true);
                btnchkWebApiCoreCheck.Text = "Uncheck All";
                return;
            }
            CheckAllWebApiCore(false);
            btnchkWebApiCoreCheck.Text = "Check All";
        }

        private void CheckAllWebApiCore(bool val)
        {
            chkWebApiCoreEntity.Checked = val;
            chkWebApiCoreBLL.Checked = val;
            chkWebApiCoreDAL.Checked = val;
            chkWebApiCoreIDAL.Checked = val;
            chkWebApiCoreIBLL.Checked = val;
        }

        private void BtnchkWebApiCoreCheck_Ext_Click(object sender, EventArgs e)
        {
            if (btnchkWebApiCoreCheck_Ext.Text == "Check All")
            {
                CheckAllWebApiCore_Ext(true);
                btnchkWebApiCoreCheck_Ext.Text = "Uncheck All";
                return;
            }
            CheckAllWebApiCore_Ext(false);
            btnchkWebApiCoreCheck_Ext.Text = "Check All";
        }

        private void CheckAllWebApiCore_Ext(bool val)
        {
            chkWebApiCoreEntity_Ext.Checked = val;
            chkWebApiCoreBLL_Ext.Checked = val;
            chkWebApiCoreDAL_Ext.Checked = val;
            chkWebApiCoreIDAL_Ext.Checked = val;
            chkWebApiCoreIBLL_Ext.Checked = val;
        }
    }
}

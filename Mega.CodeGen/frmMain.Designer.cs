namespace Mega.CodeGen
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab = new System.Windows.Forms.TabControl();
            this.DBCOnnection = new System.Windows.Forms.TabPage();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.rbRTRC = new System.Windows.Forms.RadioButton();
            this.rbCCRC = new System.Windows.Forms.RadioButton();
            this.rbRCCC = new System.Windows.Forms.RadioButton();
            this.rbRTRCCC = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDatabase = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtDataSource = new System.Windows.Forms.TextBox();
            this.DBDetails = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGetandSet = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.lbSelectedTableList1 = new System.Windows.Forms.ListBox();
            this.btnColapsExpanTreeView = new System.Windows.Forms.Button();
            this.btnSet = new System.Windows.Forms.Button();
            this.btnGetTableInfo = new System.Windows.Forms.Button();
            this.tvTableInfo = new System.Windows.Forms.TreeView();
            this.tvTableList = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.View = new System.Windows.Forms.TabPage();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.btnLoadXML = new System.Windows.Forms.Button();
            this.cmbFileList = new System.Windows.Forms.ComboBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.btnSaveXML = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lbSelectedTableList2 = new System.Windows.Forms.ListBox();
            this.btnSetFinal = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnRemoveChildList = new System.Windows.Forms.Button();
            this.btnRemoveMasterList = new System.Windows.Forms.Button();
            this.btnSetChild = new System.Windows.Forms.Button();
            this.lbChildTableList = new System.Windows.Forms.ListBox();
            this.lbMasterTableList = new System.Windows.Forms.ListBox();
            this.btnSetMaster = new System.Windows.Forms.Button();
            this.dgTableRef = new System.Windows.Forms.DataGridView();
            this.dgView = new System.Windows.Forms.DataGridView();
            this.TableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TableAliasName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precsion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Scale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lenght = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsMasterTable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReferenceTableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RefColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAliasName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDBType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDotNetType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnIsNull = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.HasReference = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsHidden = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.GetSPParam1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SearchSPParam = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SPOrderBy = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SelectForGetSP = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Lable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Generate = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtModuleName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnFileOpen = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.MSSQL = new System.Windows.Forms.TabPage();
            this.btnCheckSPAll = new System.Windows.Forms.Button();
            this.chkSP_Del = new System.Windows.Forms.CheckBox();
            this.chkSP_Upd = new System.Windows.Forms.CheckBox();
            this.chkSP_Ins = new System.Windows.Forms.CheckBox();
            this.chkSP_GAPg = new System.Windows.Forms.CheckBox();
            this.chkSP_GA = new System.Windows.Forms.CheckBox();
            this.chkSPSearch = new System.Windows.Forms.CheckBox();
            this.chkSPGet = new System.Windows.Forms.CheckBox();
            this.WebAPINetCore = new System.Windows.Forms.TabPage();
            this.btnchkWebApiCoreCheck_Ext = new System.Windows.Forms.Button();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.chkWebApiCoreIBLL_Ext = new System.Windows.Forms.CheckBox();
            this.chkWebApiCoreIDAL_Ext = new System.Windows.Forms.CheckBox();
            this.chkWebApiCoreDAL_Ext = new System.Windows.Forms.CheckBox();
            this.chkWebApiCoreEntity_Ext = new System.Windows.Forms.CheckBox();
            this.chkWebApiCoreBLL_Ext = new System.Windows.Forms.CheckBox();
            this.btnchkWebApiCoreCheck = new System.Windows.Forms.Button();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.chkWebApiCoreFCC = new System.Windows.Forms.CheckBox();
            this.chkWebApiCoreIBLL = new System.Windows.Forms.CheckBox();
            this.chkWebApiCoreIDAL = new System.Windows.Forms.CheckBox();
            this.chkWebApiCoreDAL = new System.Windows.Forms.CheckBox();
            this.chkWebApiCoreEntity = new System.Windows.Forms.CheckBox();
            this.chkWebApiCoreBLL = new System.Windows.Forms.CheckBox();
            this.MVC = new System.Windows.Forms.TabPage();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.chkMDEdit = new System.Windows.Forms.CheckBox();
            this.chkMDCreate = new System.Windows.Forms.CheckBox();
            this.chkMDIndex = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.chkMDJQScript = new System.Windows.Forms.CheckBox();
            this.chkMDController = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.chkJQScript = new System.Windows.Forms.CheckBox();
            this.chkControllers = new System.Windows.Forms.CheckBox();
            this.chkModel = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkEdit = new System.Windows.Forms.CheckBox();
            this.chkCreate = new System.Windows.Forms.CheckBox();
            this.chkIndex = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.chkIDAL = new System.Windows.Forms.CheckBox();
            this.chkDAL = new System.Windows.Forms.CheckBox();
            this.chkEntityMappers = new System.Windows.Forms.CheckBox();
            this.chkEntityMappersRef = new System.Windows.Forms.CheckBox();
            this.chkBLL = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkEntityRef = new System.Windows.Forms.CheckBox();
            this.chkEntity = new System.Windows.Forms.CheckBox();
            this.btnMVCSelectAll = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.chkWebApiController = new System.Windows.Forms.CheckBox();
            this.tab.SuspendLayout();
            this.DBCOnnection.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.DBDetails.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.View.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTableRef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).BeginInit();
            this.Generate.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.MSSQL.SuspendLayout();
            this.WebAPINetCore.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.MVC.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab
            // 
            this.tab.Controls.Add(this.DBCOnnection);
            this.tab.Controls.Add(this.DBDetails);
            this.tab.Controls.Add(this.View);
            this.tab.Controls.Add(this.Generate);
            this.tab.Location = new System.Drawing.Point(1, 0);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(1227, 674);
            this.tab.TabIndex = 3;
            // 
            // DBCOnnection
            // 
            this.DBCOnnection.Controls.Add(this.groupBox13);
            this.DBCOnnection.Controls.Add(this.groupBox2);
            this.DBCOnnection.Location = new System.Drawing.Point(4, 22);
            this.DBCOnnection.Name = "DBCOnnection";
            this.DBCOnnection.Padding = new System.Windows.Forms.Padding(3);
            this.DBCOnnection.Size = new System.Drawing.Size(1219, 648);
            this.DBCOnnection.TabIndex = 0;
            this.DBCOnnection.Text = "DB Connection";
            this.DBCOnnection.UseVisualStyleBackColor = true;
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.rbRTRC);
            this.groupBox13.Controls.Add(this.rbCCRC);
            this.groupBox13.Controls.Add(this.rbRCCC);
            this.groupBox13.Controls.Add(this.rbRTRCCC);
            this.groupBox13.Location = new System.Drawing.Point(187, 291);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(864, 124);
            this.groupBox13.TabIndex = 2;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Referance Column Naming Convention";
            // 
            // rbRTRC
            // 
            this.rbRTRC.AutoSize = true;
            this.rbRTRC.Checked = true;
            this.rbRTRC.Location = new System.Drawing.Point(25, 29);
            this.rbRTRC.Name = "rbRTRC";
            this.rbRTRC.Size = new System.Drawing.Size(177, 17);
            this.rbRTRC.TabIndex = 3;
            this.rbRTRC.TabStop = true;
            this.rbRTRC.Text = "RTable Name + RColumn Name";
            this.rbRTRC.UseVisualStyleBackColor = true;
            // 
            // rbCCRC
            // 
            this.rbCCRC.AutoSize = true;
            this.rbCCRC.Location = new System.Drawing.Point(353, 29);
            this.rbCCRC.Name = "rbCCRC";
            this.rbCCRC.Size = new System.Drawing.Size(184, 17);
            this.rbCCRC.TabIndex = 2;
            this.rbCCRC.Text = "CColumn Name + RColumn Name";
            this.rbCCRC.UseVisualStyleBackColor = true;
            // 
            // rbRCCC
            // 
            this.rbRCCC.AutoSize = true;
            this.rbRCCC.Location = new System.Drawing.Point(25, 52);
            this.rbRCCC.Name = "rbRCCC";
            this.rbRCCC.Size = new System.Drawing.Size(184, 17);
            this.rbRCCC.TabIndex = 1;
            this.rbRCCC.Text = "RColumn Name + CColumn Name";
            this.rbRCCC.UseVisualStyleBackColor = true;
            // 
            // rbRTRCCC
            // 
            this.rbRTRCCC.AutoSize = true;
            this.rbRTRCCC.Location = new System.Drawing.Point(25, 75);
            this.rbRTRCCC.Name = "rbRTRCCC";
            this.rbRTRCCC.Size = new System.Drawing.Size(262, 17);
            this.rbRTRCCC.TabIndex = 0;
            this.rbRTRCCC.Text = "RTable Name + RColumn Name + CColumn Name";
            this.rbRTRCCC.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblStatus);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmbDatabase);
            this.groupBox2.Controls.Add(this.btnConnect);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtPassword);
            this.groupBox2.Controls.Add(this.txtUserName);
            this.groupBox2.Controls.Add(this.txtDataSource);
            this.groupBox2.Location = new System.Drawing.Point(187, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(864, 170);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DataBase Connectivity";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(741, 109);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(85, 13);
            this.lblStatus.TabIndex = 16;
            this.lblStatus.Text = "Disconnected";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(530, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Select Database";
            // 
            // cmbDatabase
            // 
            this.cmbDatabase.Location = new System.Drawing.Point(533, 133);
            this.cmbDatabase.Name = "cmbDatabase";
            this.cmbDatabase.Size = new System.Drawing.Size(293, 21);
            this.cmbDatabase.TabIndex = 14;
            this.cmbDatabase.SelectedIndexChanged += new System.EventHandler(this.cmbDatabase_SelectedIndexChanged);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(533, 19);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(293, 65);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "Connect";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(61, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "User Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(61, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Password";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(61, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Data Source";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(157, 71);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(209, 20);
            this.txtPassword.TabIndex = 2;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(157, 45);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(209, 20);
            this.txtUserName.TabIndex = 1;
            // 
            // txtDataSource
            // 
            this.txtDataSource.Location = new System.Drawing.Point(157, 19);
            this.txtDataSource.Name = "txtDataSource";
            this.txtDataSource.Size = new System.Drawing.Size(209, 20);
            this.txtDataSource.TabIndex = 0;
            // 
            // DBDetails
            // 
            this.DBDetails.Controls.Add(this.groupBox1);
            this.DBDetails.Location = new System.Drawing.Point(4, 22);
            this.DBDetails.Name = "DBDetails";
            this.DBDetails.Padding = new System.Windows.Forms.Padding(3);
            this.DBDetails.Size = new System.Drawing.Size(1219, 648);
            this.DBDetails.TabIndex = 1;
            this.DBDetails.Text = "DB Details";
            this.DBDetails.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGetandSet);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lbSelectedTableList1);
            this.groupBox1.Controls.Add(this.btnColapsExpanTreeView);
            this.groupBox1.Controls.Add(this.btnSet);
            this.groupBox1.Controls.Add(this.btnGetTableInfo);
            this.groupBox1.Controls.Add(this.tvTableInfo);
            this.groupBox1.Controls.Add(this.tvTableList);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1207, 636);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // btnGetandSet
            // 
            this.btnGetandSet.Location = new System.Drawing.Point(340, 129);
            this.btnGetandSet.Name = "btnGetandSet";
            this.btnGetandSet.Size = new System.Drawing.Size(51, 116);
            this.btnGetandSet.TabIndex = 28;
            this.btnGetandSet.Text = "Get and Set";
            this.btnGetandSet.UseVisualStyleBackColor = true;
            this.btnGetandSet.Click += new System.EventHandler(this.BtnGetandSet_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(394, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 13);
            this.label8.TabIndex = 27;
            this.label8.Text = "Select Table\'s Columns";
            // 
            // lbSelectedTableList1
            // 
            this.lbSelectedTableList1.DisplayMember = "TableName";
            this.lbSelectedTableList1.FormattingEnabled = true;
            this.lbSelectedTableList1.Location = new System.Drawing.Point(963, 84);
            this.lbSelectedTableList1.Name = "lbSelectedTableList1";
            this.lbSelectedTableList1.Size = new System.Drawing.Size(231, 537);
            this.lbSelectedTableList1.TabIndex = 26;
            this.lbSelectedTableList1.ValueMember = "TableName";
            // 
            // btnColapsExpanTreeView
            // 
            this.btnColapsExpanTreeView.Location = new System.Drawing.Point(397, 590);
            this.btnColapsExpanTreeView.Name = "btnColapsExpanTreeView";
            this.btnColapsExpanTreeView.Size = new System.Drawing.Size(146, 28);
            this.btnColapsExpanTreeView.TabIndex = 25;
            this.btnColapsExpanTreeView.Text = "Collapse";
            this.btnColapsExpanTreeView.UseVisualStyleBackColor = true;
            this.btnColapsExpanTreeView.Click += new System.EventHandler(this.btnColapsExpanTreeView_Click);
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(963, 32);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(231, 46);
            this.btnSet.TabIndex = 24;
            this.btnSet.Text = "Set Temporary List";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnGetTableInfo
            // 
            this.btnGetTableInfo.Location = new System.Drawing.Point(340, 74);
            this.btnGetTableInfo.Name = "btnGetTableInfo";
            this.btnGetTableInfo.Size = new System.Drawing.Size(51, 49);
            this.btnGetTableInfo.TabIndex = 22;
            this.btnGetTableInfo.Text = "Get >>";
            this.btnGetTableInfo.UseVisualStyleBackColor = true;
            this.btnGetTableInfo.Click += new System.EventHandler(this.btnGetTableInfo_Click);
            // 
            // tvTableInfo
            // 
            this.tvTableInfo.CheckBoxes = true;
            this.tvTableInfo.Location = new System.Drawing.Point(397, 32);
            this.tvTableInfo.Name = "tvTableInfo";
            this.tvTableInfo.Size = new System.Drawing.Size(554, 552);
            this.tvTableInfo.TabIndex = 21;
            // 
            // tvTableList
            // 
            this.tvTableList.CheckBoxes = true;
            this.tvTableList.Location = new System.Drawing.Point(13, 32);
            this.tvTableList.Name = "tvTableList";
            this.tvTableList.Size = new System.Drawing.Size(321, 586);
            this.tvTableList.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Select Table";
            // 
            // View
            // 
            this.View.Controls.Add(this.groupBox12);
            this.View.Controls.Add(this.groupBox9);
            this.View.Controls.Add(this.groupBox8);
            this.View.Controls.Add(this.dgTableRef);
            this.View.Controls.Add(this.dgView);
            this.View.Location = new System.Drawing.Point(4, 22);
            this.View.Name = "View";
            this.View.Size = new System.Drawing.Size(1219, 648);
            this.View.TabIndex = 2;
            this.View.Text = "View";
            this.View.UseVisualStyleBackColor = true;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.btnLoadXML);
            this.groupBox12.Controls.Add(this.cmbFileList);
            this.groupBox12.Location = new System.Drawing.Point(6, 586);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(553, 53);
            this.groupBox12.TabIndex = 38;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Load Previous Work";
            // 
            // btnLoadXML
            // 
            this.btnLoadXML.Location = new System.Drawing.Point(319, 19);
            this.btnLoadXML.Name = "btnLoadXML";
            this.btnLoadXML.Size = new System.Drawing.Size(159, 23);
            this.btnLoadXML.TabIndex = 1;
            this.btnLoadXML.Text = "Load XML";
            this.btnLoadXML.UseVisualStyleBackColor = true;
            this.btnLoadXML.Click += new System.EventHandler(this.btnLoadXML_Click);
            // 
            // cmbFileList
            // 
            this.cmbFileList.FormattingEnabled = true;
            this.cmbFileList.Location = new System.Drawing.Point(6, 19);
            this.cmbFileList.Name = "cmbFileList";
            this.cmbFileList.Size = new System.Drawing.Size(278, 21);
            this.cmbFileList.TabIndex = 0;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.btnSaveXML);
            this.groupBox9.Controls.Add(this.btnRemove);
            this.groupBox9.Controls.Add(this.lbSelectedTableList2);
            this.groupBox9.Controls.Add(this.btnSetFinal);
            this.groupBox9.Location = new System.Drawing.Point(590, 383);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(358, 253);
            this.groupBox9.TabIndex = 37;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Temporary List";
            // 
            // btnSaveXML
            // 
            this.btnSaveXML.Location = new System.Drawing.Point(268, 149);
            this.btnSaveXML.Name = "btnSaveXML";
            this.btnSaveXML.Size = new System.Drawing.Size(83, 92);
            this.btnSaveXML.TabIndex = 40;
            this.btnSaveXML.Text = "Save To XML";
            this.btnSaveXML.UseVisualStyleBackColor = true;
            this.btnSaveXML.Click += new System.EventHandler(this.btnSaveXML_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(268, 18);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(84, 23);
            this.btnRemove.TabIndex = 39;
            this.btnRemove.Text = "<< Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lbSelectedTableList2
            // 
            this.lbSelectedTableList2.DisplayMember = "TableName";
            this.lbSelectedTableList2.FormattingEnabled = true;
            this.lbSelectedTableList2.Location = new System.Drawing.Point(11, 18);
            this.lbSelectedTableList2.Name = "lbSelectedTableList2";
            this.lbSelectedTableList2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbSelectedTableList2.Size = new System.Drawing.Size(251, 225);
            this.lbSelectedTableList2.TabIndex = 37;
            this.lbSelectedTableList2.ValueMember = "TableName";
            this.lbSelectedTableList2.SelectedIndexChanged += new System.EventHandler(this.lbSelectedTableList2_SelectedIndexChanged);
            // 
            // btnSetFinal
            // 
            this.btnSetFinal.Location = new System.Drawing.Point(268, 47);
            this.btnSetFinal.Name = "btnSetFinal";
            this.btnSetFinal.Size = new System.Drawing.Size(84, 66);
            this.btnSetFinal.TabIndex = 36;
            this.btnSetFinal.Text = "Save ";
            this.btnSetFinal.UseVisualStyleBackColor = true;
            this.btnSetFinal.Click += new System.EventHandler(this.btnSetFinal_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnRemoveChildList);
            this.groupBox8.Controls.Add(this.btnRemoveMasterList);
            this.groupBox8.Controls.Add(this.btnSetChild);
            this.groupBox8.Controls.Add(this.lbChildTableList);
            this.groupBox8.Controls.Add(this.lbMasterTableList);
            this.groupBox8.Controls.Add(this.btnSetMaster);
            this.groupBox8.Location = new System.Drawing.Point(956, 383);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(255, 254);
            this.groupBox8.TabIndex = 36;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Master Details";
            // 
            // btnRemoveChildList
            // 
            this.btnRemoveChildList.Location = new System.Drawing.Point(13, 100);
            this.btnRemoveChildList.Name = "btnRemoveChildList";
            this.btnRemoveChildList.Size = new System.Drawing.Size(89, 23);
            this.btnRemoveChildList.TabIndex = 41;
            this.btnRemoveChildList.Text = "Remove >>";
            this.btnRemoveChildList.UseVisualStyleBackColor = true;
            this.btnRemoveChildList.Click += new System.EventHandler(this.btnRemoveChildList_Click);
            // 
            // btnRemoveMasterList
            // 
            this.btnRemoveMasterList.Location = new System.Drawing.Point(12, 47);
            this.btnRemoveMasterList.Name = "btnRemoveMasterList";
            this.btnRemoveMasterList.Size = new System.Drawing.Size(89, 23);
            this.btnRemoveMasterList.TabIndex = 40;
            this.btnRemoveMasterList.Text = "Remove >>";
            this.btnRemoveMasterList.UseVisualStyleBackColor = true;
            this.btnRemoveMasterList.Click += new System.EventHandler(this.btnRemoveMasterList_Click);
            // 
            // btnSetChild
            // 
            this.btnSetChild.Location = new System.Drawing.Point(13, 76);
            this.btnSetChild.Name = "btnSetChild";
            this.btnSetChild.Size = new System.Drawing.Size(88, 23);
            this.btnSetChild.TabIndex = 37;
            this.btnSetChild.Text = "Set Child >>";
            this.btnSetChild.UseVisualStyleBackColor = true;
            this.btnSetChild.Click += new System.EventHandler(this.btnSetChild_Click);
            // 
            // lbChildTableList
            // 
            this.lbChildTableList.DisplayMember = "TableName";
            this.lbChildTableList.FormattingEnabled = true;
            this.lbChildTableList.Location = new System.Drawing.Point(107, 76);
            this.lbChildTableList.Name = "lbChildTableList";
            this.lbChildTableList.Size = new System.Drawing.Size(134, 160);
            this.lbChildTableList.TabIndex = 36;
            this.lbChildTableList.ValueMember = "TableName";
            // 
            // lbMasterTableList
            // 
            this.lbMasterTableList.DisplayMember = "TableName";
            this.lbMasterTableList.FormattingEnabled = true;
            this.lbMasterTableList.Location = new System.Drawing.Point(107, 23);
            this.lbMasterTableList.Name = "lbMasterTableList";
            this.lbMasterTableList.Size = new System.Drawing.Size(131, 43);
            this.lbMasterTableList.TabIndex = 35;
            this.lbMasterTableList.ValueMember = "TableName";
            // 
            // btnSetMaster
            // 
            this.btnSetMaster.Location = new System.Drawing.Point(12, 23);
            this.btnSetMaster.Name = "btnSetMaster";
            this.btnSetMaster.Size = new System.Drawing.Size(89, 23);
            this.btnSetMaster.TabIndex = 34;
            this.btnSetMaster.Text = "Set Master >>";
            this.btnSetMaster.UseVisualStyleBackColor = true;
            this.btnSetMaster.Click += new System.EventHandler(this.btnSetMaster_Click);
            // 
            // dgTableRef
            // 
            this.dgTableRef.AllowUserToOrderColumns = true;
            this.dgTableRef.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTableRef.Location = new System.Drawing.Point(3, 371);
            this.dgTableRef.Name = "dgTableRef";
            this.dgTableRef.Size = new System.Drawing.Size(561, 209);
            this.dgTableRef.TabIndex = 1;
            // 
            // dgView
            // 
            this.dgView.AllowUserToAddRows = false;
            this.dgView.AllowUserToDeleteRows = false;
            this.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TableName,
            this.TableAliasName,
            this.Precsion,
            this.Scale,
            this.Lenght,
            this.IsMasterTable,
            this.ColumnName,
            this.ReferenceTableName,
            this.RefColumnName,
            this.ColumnAliasName,
            this.ColumnDBType,
            this.ColumnDotNetType,
            this.ColumnIsNull,
            this.HasReference,
            this.IsHidden,
            this.GetSPParam1,
            this.SearchSPParam,
            this.SPOrderBy,
            this.SelectForGetSP,
            this.Lable});
            this.dgView.Location = new System.Drawing.Point(3, 3);
            this.dgView.Name = "dgView";
            this.dgView.Size = new System.Drawing.Size(1213, 362);
            this.dgView.TabIndex = 0;
            // 
            // TableName
            // 
            this.TableName.DataPropertyName = "TableName";
            this.TableName.HeaderText = "Table Name";
            this.TableName.Name = "TableName";
            // 
            // TableAliasName
            // 
            this.TableAliasName.DataPropertyName = "TableAliasName";
            this.TableAliasName.HeaderText = "TableAliasName";
            this.TableAliasName.Name = "TableAliasName";
            this.TableAliasName.ReadOnly = true;
            this.TableAliasName.Visible = false;
            // 
            // Precsion
            // 
            this.Precsion.DataPropertyName = "Precsion";
            this.Precsion.HeaderText = "Precsion";
            this.Precsion.Name = "Precsion";
            this.Precsion.ReadOnly = true;
            this.Precsion.Visible = false;
            // 
            // Scale
            // 
            this.Scale.DataPropertyName = "Scale";
            this.Scale.HeaderText = "Scale";
            this.Scale.Name = "Scale";
            this.Scale.ReadOnly = true;
            this.Scale.Visible = false;
            // 
            // Lenght
            // 
            this.Lenght.DataPropertyName = "Lenght";
            this.Lenght.HeaderText = "Lenght";
            this.Lenght.Name = "Lenght";
            this.Lenght.ReadOnly = true;
            this.Lenght.Visible = false;
            // 
            // IsMasterTable
            // 
            this.IsMasterTable.DataPropertyName = "IsMasterTable";
            this.IsMasterTable.HeaderText = "Master Table";
            this.IsMasterTable.Name = "IsMasterTable";
            this.IsMasterTable.ReadOnly = true;
            this.IsMasterTable.Width = 80;
            // 
            // ColumnName
            // 
            this.ColumnName.DataPropertyName = "ColumnName";
            this.ColumnName.HeaderText = "ColumnName";
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnName.Visible = false;
            // 
            // ReferenceTableName
            // 
            this.ReferenceTableName.DataPropertyName = "ReferenceTableName";
            this.ReferenceTableName.HeaderText = "ReferenceTableName";
            this.ReferenceTableName.Name = "ReferenceTableName";
            this.ReferenceTableName.Visible = false;
            // 
            // RefColumnName
            // 
            this.RefColumnName.DataPropertyName = "RefColumnName";
            this.RefColumnName.HeaderText = "RefColumnName";
            this.RefColumnName.Name = "RefColumnName";
            this.RefColumnName.ReadOnly = true;
            this.RefColumnName.Visible = false;
            // 
            // ColumnAliasName
            // 
            this.ColumnAliasName.DataPropertyName = "ColumnAliasName";
            this.ColumnAliasName.HeaderText = "Col. Alias Name";
            this.ColumnAliasName.Name = "ColumnAliasName";
            this.ColumnAliasName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnAliasName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnDBType
            // 
            this.ColumnDBType.DataPropertyName = "ColumnDBType";
            this.ColumnDBType.HeaderText = "ColumnDBType";
            this.ColumnDBType.Name = "ColumnDBType";
            this.ColumnDBType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnDBType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnDBType.Visible = false;
            // 
            // ColumnDotNetType
            // 
            this.ColumnDotNetType.DataPropertyName = "ColumnDotNetType";
            this.ColumnDotNetType.HeaderText = "Col. DotNet Type";
            this.ColumnDotNetType.Name = "ColumnDotNetType";
            this.ColumnDotNetType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnDotNetType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnIsNull
            // 
            this.ColumnIsNull.DataPropertyName = "ColumnIsNull";
            this.ColumnIsNull.HeaderText = "Nullable";
            this.ColumnIsNull.Name = "ColumnIsNull";
            this.ColumnIsNull.ReadOnly = true;
            this.ColumnIsNull.Width = 50;
            // 
            // HasReference
            // 
            this.HasReference.DataPropertyName = "HasReference";
            this.HasReference.HeaderText = "Has Reference";
            this.HasReference.Name = "HasReference";
            this.HasReference.ReadOnly = true;
            this.HasReference.Width = 90;
            // 
            // IsHidden
            // 
            this.IsHidden.DataPropertyName = "IsHidden";
            this.IsHidden.HeaderText = "Hidden";
            this.IsHidden.Name = "IsHidden";
            this.IsHidden.Width = 50;
            // 
            // GetSPParam1
            // 
            this.GetSPParam1.DataPropertyName = "GetSPParam";
            this.GetSPParam1.HeaderText = "Get SP Param";
            this.GetSPParam1.Name = "GetSPParam1";
            this.GetSPParam1.Width = 80;
            // 
            // SearchSPParam
            // 
            this.SearchSPParam.DataPropertyName = "SearchSPParam";
            this.SearchSPParam.HeaderText = "SearchSPParam";
            this.SearchSPParam.Name = "SearchSPParam";
            this.SearchSPParam.Width = 90;
            // 
            // SPOrderBy
            // 
            this.SPOrderBy.DataPropertyName = "SPOrderBy";
            this.SPOrderBy.HeaderText = "SP Order By";
            this.SPOrderBy.Name = "SPOrderBy";
            this.SPOrderBy.Width = 80;
            // 
            // SelectForGetSP
            // 
            this.SelectForGetSP.DataPropertyName = "SelectForGetSP";
            this.SelectForGetSP.HeaderText = "SelectForGetSP";
            this.SelectForGetSP.Name = "SelectForGetSP";
            this.SelectForGetSP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SelectForGetSP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.SelectForGetSP.Width = 80;
            // 
            // Lable
            // 
            this.Lable.DataPropertyName = "Lable";
            this.Lable.HeaderText = "Lable";
            this.Lable.Name = "Lable";
            this.Lable.Width = 250;
            // 
            // Generate
            // 
            this.Generate.Controls.Add(this.groupBox4);
            this.Generate.Controls.Add(this.tabControl1);
            this.Generate.Location = new System.Drawing.Point(4, 22);
            this.Generate.Name = "Generate";
            this.Generate.Size = new System.Drawing.Size(1219, 648);
            this.Generate.TabIndex = 4;
            this.Generate.Text = "Generate";
            this.Generate.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtModuleName);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.btnFileOpen);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.txtNamespace);
            this.groupBox4.Controls.Add(this.txtPath);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(7, 449);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1205, 113);
            this.groupBox4.TabIndex = 35;
            this.groupBox4.TabStop = false;
            // 
            // txtModuleName
            // 
            this.txtModuleName.Location = new System.Drawing.Point(106, 77);
            this.txtModuleName.Name = "txtModuleName";
            this.txtModuleName.Size = new System.Drawing.Size(366, 20);
            this.txtModuleName.TabIndex = 34;
            this.txtModuleName.Text = "Security";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(12, 78);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 23);
            this.label9.TabIndex = 35;
            this.label9.Text = "Module Name";
            // 
            // btnFileOpen
            // 
            this.btnFileOpen.Location = new System.Drawing.Point(478, 24);
            this.btnFileOpen.Name = "btnFileOpen";
            this.btnFileOpen.Size = new System.Drawing.Size(24, 20);
            this.btnFileOpen.TabIndex = 28;
            this.btnFileOpen.Text = "...";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(10, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 23);
            this.label4.TabIndex = 32;
            this.label4.Text = "Class Destination";
            // 
            // txtNamespace
            // 
            this.txtNamespace.Location = new System.Drawing.Point(106, 51);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(366, 20);
            this.txtNamespace.TabIndex = 29;
            this.txtNamespace.Text = "Mega";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(106, 25);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(366, 20);
            this.txtPath.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 23);
            this.label3.TabIndex = 33;
            this.label3.Text = "Namespace";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.MSSQL);
            this.tabControl1.Controls.Add(this.WebAPINetCore);
            this.tabControl1.Controls.Add(this.MVC);
            this.tabControl1.Location = new System.Drawing.Point(3, 22);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1213, 421);
            this.tabControl1.TabIndex = 0;
            // 
            // MSSQL
            // 
            this.MSSQL.Controls.Add(this.btnCheckSPAll);
            this.MSSQL.Controls.Add(this.chkSP_Del);
            this.MSSQL.Controls.Add(this.chkSP_Upd);
            this.MSSQL.Controls.Add(this.chkSP_Ins);
            this.MSSQL.Controls.Add(this.chkSP_GAPg);
            this.MSSQL.Controls.Add(this.chkSP_GA);
            this.MSSQL.Controls.Add(this.chkSPSearch);
            this.MSSQL.Controls.Add(this.chkSPGet);
            this.MSSQL.Location = new System.Drawing.Point(4, 22);
            this.MSSQL.Name = "MSSQL";
            this.MSSQL.Padding = new System.Windows.Forms.Padding(3);
            this.MSSQL.Size = new System.Drawing.Size(1205, 395);
            this.MSSQL.TabIndex = 0;
            this.MSSQL.Text = "MSSQL";
            this.MSSQL.UseVisualStyleBackColor = true;
            // 
            // btnCheckSPAll
            // 
            this.btnCheckSPAll.Location = new System.Drawing.Point(992, 160);
            this.btnCheckSPAll.Name = "btnCheckSPAll";
            this.btnCheckSPAll.Size = new System.Drawing.Size(131, 33);
            this.btnCheckSPAll.TabIndex = 50;
            this.btnCheckSPAll.Text = "Check All";
            this.btnCheckSPAll.Click += new System.EventHandler(this.BtnCheckSPAll_Click);
            // 
            // chkSP_Del
            // 
            this.chkSP_Del.Location = new System.Drawing.Point(992, 130);
            this.chkSP_Del.Name = "chkSP_Del";
            this.chkSP_Del.Size = new System.Drawing.Size(197, 24);
            this.chkSP_Del.TabIndex = 49;
            this.chkSP_Del.Text = "Create SP_Del";
            // 
            // chkSP_Upd
            // 
            this.chkSP_Upd.Location = new System.Drawing.Point(992, 100);
            this.chkSP_Upd.Name = "chkSP_Upd";
            this.chkSP_Upd.Size = new System.Drawing.Size(197, 24);
            this.chkSP_Upd.TabIndex = 48;
            this.chkSP_Upd.Text = "Create SP_Upd";
            // 
            // chkSP_Ins
            // 
            this.chkSP_Ins.Location = new System.Drawing.Point(992, 70);
            this.chkSP_Ins.Name = "chkSP_Ins";
            this.chkSP_Ins.Size = new System.Drawing.Size(197, 24);
            this.chkSP_Ins.TabIndex = 47;
            this.chkSP_Ins.Text = "Create SP_Ins";
            // 
            // chkSP_GAPg
            // 
            this.chkSP_GAPg.Location = new System.Drawing.Point(992, 40);
            this.chkSP_GAPg.Name = "chkSP_GAPg";
            this.chkSP_GAPg.Size = new System.Drawing.Size(197, 24);
            this.chkSP_GAPg.TabIndex = 46;
            this.chkSP_GAPg.Text = "Create SP_GAPg";
            // 
            // chkSP_GA
            // 
            this.chkSP_GA.Location = new System.Drawing.Point(992, 10);
            this.chkSP_GA.Name = "chkSP_GA";
            this.chkSP_GA.Size = new System.Drawing.Size(197, 24);
            this.chkSP_GA.TabIndex = 45;
            this.chkSP_GA.Text = "Create SP_GA";
            // 
            // chkSPSearch
            // 
            this.chkSPSearch.Location = new System.Drawing.Point(13, 51);
            this.chkSPSearch.Name = "chkSPSearch";
            this.chkSPSearch.Size = new System.Drawing.Size(197, 24);
            this.chkSPSearch.TabIndex = 44;
            this.chkSPSearch.Text = "Create spSearch";
            // 
            // chkSPGet
            // 
            this.chkSPGet.Location = new System.Drawing.Point(13, 21);
            this.chkSPGet.Name = "chkSPGet";
            this.chkSPGet.Size = new System.Drawing.Size(197, 24);
            this.chkSPGet.TabIndex = 43;
            this.chkSPGet.Text = "Create spGet";
            // 
            // WebAPINetCore
            // 
            this.WebAPINetCore.Controls.Add(this.chkWebApiController);
            this.WebAPINetCore.Controls.Add(this.btnchkWebApiCoreCheck_Ext);
            this.WebAPINetCore.Controls.Add(this.groupBox15);
            this.WebAPINetCore.Controls.Add(this.btnchkWebApiCoreCheck);
            this.WebAPINetCore.Controls.Add(this.groupBox14);
            this.WebAPINetCore.Location = new System.Drawing.Point(4, 22);
            this.WebAPINetCore.Name = "WebAPINetCore";
            this.WebAPINetCore.Padding = new System.Windows.Forms.Padding(3);
            this.WebAPINetCore.Size = new System.Drawing.Size(1205, 395);
            this.WebAPINetCore.TabIndex = 3;
            this.WebAPINetCore.Text = "Web API (.Net Core)";
            this.WebAPINetCore.UseVisualStyleBackColor = true;
            // 
            // btnchkWebApiCoreCheck_Ext
            // 
            this.btnchkWebApiCoreCheck_Ext.Location = new System.Drawing.Point(513, 150);
            this.btnchkWebApiCoreCheck_Ext.Name = "btnchkWebApiCoreCheck_Ext";
            this.btnchkWebApiCoreCheck_Ext.Size = new System.Drawing.Size(131, 33);
            this.btnchkWebApiCoreCheck_Ext.TabIndex = 63;
            this.btnchkWebApiCoreCheck_Ext.Text = "Check All";
            this.btnchkWebApiCoreCheck_Ext.Click += new System.EventHandler(this.BtnchkWebApiCoreCheck_Ext_Click);
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.chkWebApiCoreIBLL_Ext);
            this.groupBox15.Controls.Add(this.chkWebApiCoreIDAL_Ext);
            this.groupBox15.Controls.Add(this.chkWebApiCoreDAL_Ext);
            this.groupBox15.Controls.Add(this.chkWebApiCoreEntity_Ext);
            this.groupBox15.Controls.Add(this.chkWebApiCoreBLL_Ext);
            this.groupBox15.Location = new System.Drawing.Point(507, 20);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(399, 100);
            this.groupBox15.TabIndex = 62;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "BLL";
            // 
            // chkWebApiCoreIBLL_Ext
            // 
            this.chkWebApiCoreIBLL_Ext.Location = new System.Drawing.Point(113, 19);
            this.chkWebApiCoreIBLL_Ext.Name = "chkWebApiCoreIBLL_Ext";
            this.chkWebApiCoreIBLL_Ext.Size = new System.Drawing.Size(152, 24);
            this.chkWebApiCoreIBLL_Ext.TabIndex = 51;
            this.chkWebApiCoreIBLL_Ext.Text = "Create iBLL Ext";
            // 
            // chkWebApiCoreIDAL_Ext
            // 
            this.chkWebApiCoreIDAL_Ext.Location = new System.Drawing.Point(6, 19);
            this.chkWebApiCoreIDAL_Ext.Name = "chkWebApiCoreIDAL_Ext";
            this.chkWebApiCoreIDAL_Ext.Size = new System.Drawing.Size(101, 24);
            this.chkWebApiCoreIDAL_Ext.TabIndex = 48;
            this.chkWebApiCoreIDAL_Ext.Text = "Create iDAL Ext";
            // 
            // chkWebApiCoreDAL_Ext
            // 
            this.chkWebApiCoreDAL_Ext.Location = new System.Drawing.Point(6, 49);
            this.chkWebApiCoreDAL_Ext.Name = "chkWebApiCoreDAL_Ext";
            this.chkWebApiCoreDAL_Ext.Size = new System.Drawing.Size(101, 24);
            this.chkWebApiCoreDAL_Ext.TabIndex = 49;
            this.chkWebApiCoreDAL_Ext.Text = "Create DAL Ext";
            // 
            // chkWebApiCoreEntity_Ext
            // 
            this.chkWebApiCoreEntity_Ext.Location = new System.Drawing.Point(222, 49);
            this.chkWebApiCoreEntity_Ext.Name = "chkWebApiCoreEntity_Ext";
            this.chkWebApiCoreEntity_Ext.Size = new System.Drawing.Size(143, 24);
            this.chkWebApiCoreEntity_Ext.TabIndex = 47;
            this.chkWebApiCoreEntity_Ext.Text = "Create Entity Ext";
            // 
            // chkWebApiCoreBLL_Ext
            // 
            this.chkWebApiCoreBLL_Ext.Location = new System.Drawing.Point(113, 49);
            this.chkWebApiCoreBLL_Ext.Name = "chkWebApiCoreBLL_Ext";
            this.chkWebApiCoreBLL_Ext.Size = new System.Drawing.Size(152, 24);
            this.chkWebApiCoreBLL_Ext.TabIndex = 50;
            this.chkWebApiCoreBLL_Ext.Text = "Create BLL Ext";
            // 
            // btnchkWebApiCoreCheck
            // 
            this.btnchkWebApiCoreCheck.Location = new System.Drawing.Point(35, 150);
            this.btnchkWebApiCoreCheck.Name = "btnchkWebApiCoreCheck";
            this.btnchkWebApiCoreCheck.Size = new System.Drawing.Size(131, 33);
            this.btnchkWebApiCoreCheck.TabIndex = 61;
            this.btnchkWebApiCoreCheck.Text = "Check All";
            this.btnchkWebApiCoreCheck.Click += new System.EventHandler(this.BtnchkWebApiCoreCheck_Click);
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.chkWebApiCoreFCC);
            this.groupBox14.Controls.Add(this.chkWebApiCoreIBLL);
            this.groupBox14.Controls.Add(this.chkWebApiCoreIDAL);
            this.groupBox14.Controls.Add(this.chkWebApiCoreDAL);
            this.groupBox14.Controls.Add(this.chkWebApiCoreEntity);
            this.groupBox14.Controls.Add(this.chkWebApiCoreBLL);
            this.groupBox14.Location = new System.Drawing.Point(35, 20);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(399, 100);
            this.groupBox14.TabIndex = 60;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "BLL";
            // 
            // chkWebApiCoreFCC
            // 
            this.chkWebApiCoreFCC.Location = new System.Drawing.Point(213, 19);
            this.chkWebApiCoreFCC.Name = "chkWebApiCoreFCC";
            this.chkWebApiCoreFCC.Size = new System.Drawing.Size(143, 24);
            this.chkWebApiCoreFCC.TabIndex = 52;
            this.chkWebApiCoreFCC.Text = "Create FCC";
            // 
            // chkWebApiCoreIBLL
            // 
            this.chkWebApiCoreIBLL.Location = new System.Drawing.Point(104, 19);
            this.chkWebApiCoreIBLL.Name = "chkWebApiCoreIBLL";
            this.chkWebApiCoreIBLL.Size = new System.Drawing.Size(152, 24);
            this.chkWebApiCoreIBLL.TabIndex = 51;
            this.chkWebApiCoreIBLL.Text = "Create iBLL";
            // 
            // chkWebApiCoreIDAL
            // 
            this.chkWebApiCoreIDAL.Location = new System.Drawing.Point(6, 19);
            this.chkWebApiCoreIDAL.Name = "chkWebApiCoreIDAL";
            this.chkWebApiCoreIDAL.Size = new System.Drawing.Size(92, 24);
            this.chkWebApiCoreIDAL.TabIndex = 48;
            this.chkWebApiCoreIDAL.Text = "Create iDAL";
            // 
            // chkWebApiCoreDAL
            // 
            this.chkWebApiCoreDAL.Location = new System.Drawing.Point(6, 49);
            this.chkWebApiCoreDAL.Name = "chkWebApiCoreDAL";
            this.chkWebApiCoreDAL.Size = new System.Drawing.Size(92, 24);
            this.chkWebApiCoreDAL.TabIndex = 49;
            this.chkWebApiCoreDAL.Text = "Create DAL";
            // 
            // chkWebApiCoreEntity
            // 
            this.chkWebApiCoreEntity.Location = new System.Drawing.Point(213, 49);
            this.chkWebApiCoreEntity.Name = "chkWebApiCoreEntity";
            this.chkWebApiCoreEntity.Size = new System.Drawing.Size(143, 24);
            this.chkWebApiCoreEntity.TabIndex = 47;
            this.chkWebApiCoreEntity.Text = "Create Entity";
            // 
            // chkWebApiCoreBLL
            // 
            this.chkWebApiCoreBLL.Location = new System.Drawing.Point(104, 49);
            this.chkWebApiCoreBLL.Name = "chkWebApiCoreBLL";
            this.chkWebApiCoreBLL.Size = new System.Drawing.Size(152, 24);
            this.chkWebApiCoreBLL.TabIndex = 50;
            this.chkWebApiCoreBLL.Text = "Create BLL";
            // 
            // MVC
            // 
            this.MVC.Controls.Add(this.groupBox10);
            this.MVC.Controls.Add(this.groupBox7);
            this.MVC.Controls.Add(this.groupBox6);
            this.MVC.Controls.Add(this.groupBox5);
            this.MVC.Controls.Add(this.btnMVCSelectAll);
            this.MVC.Location = new System.Drawing.Point(4, 22);
            this.MVC.Name = "MVC";
            this.MVC.Size = new System.Drawing.Size(1205, 395);
            this.MVC.TabIndex = 2;
            this.MVC.Text = "MVC2";
            this.MVC.UseVisualStyleBackColor = true;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.groupBox11);
            this.groupBox10.Controls.Add(this.checkBox4);
            this.groupBox10.Controls.Add(this.chkMDJQScript);
            this.groupBox10.Controls.Add(this.chkMDController);
            this.groupBox10.Location = new System.Drawing.Point(852, 21);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(209, 244);
            this.groupBox10.TabIndex = 61;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Master Details";
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.chkMDEdit);
            this.groupBox11.Controls.Add(this.chkMDCreate);
            this.groupBox11.Controls.Add(this.chkMDIndex);
            this.groupBox11.Location = new System.Drawing.Point(6, 138);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(195, 100);
            this.groupBox11.TabIndex = 47;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "View";
            // 
            // chkMDEdit
            // 
            this.chkMDEdit.Location = new System.Drawing.Point(18, 67);
            this.chkMDEdit.Name = "chkMDEdit";
            this.chkMDEdit.Size = new System.Drawing.Size(137, 23);
            this.chkMDEdit.TabIndex = 41;
            this.chkMDEdit.Text = "Create Edit";
            // 
            // chkMDCreate
            // 
            this.chkMDCreate.Location = new System.Drawing.Point(18, 43);
            this.chkMDCreate.Name = "chkMDCreate";
            this.chkMDCreate.Size = new System.Drawing.Size(137, 24);
            this.chkMDCreate.TabIndex = 40;
            this.chkMDCreate.Text = "Create Create";
            // 
            // chkMDIndex
            // 
            this.chkMDIndex.Location = new System.Drawing.Point(18, 19);
            this.chkMDIndex.Name = "chkMDIndex";
            this.chkMDIndex.Size = new System.Drawing.Size(137, 24);
            this.chkMDIndex.TabIndex = 39;
            this.chkMDIndex.Text = "Create Index";
            // 
            // checkBox4
            // 
            this.checkBox4.Location = new System.Drawing.Point(19, 81);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(152, 24);
            this.checkBox4.TabIndex = 50;
            this.checkBox4.Text = "Create Model";
            // 
            // chkMDJQScript
            // 
            this.chkMDJQScript.Location = new System.Drawing.Point(19, 29);
            this.chkMDJQScript.Name = "chkMDJQScript";
            this.chkMDJQScript.Size = new System.Drawing.Size(152, 24);
            this.chkMDJQScript.TabIndex = 48;
            this.chkMDJQScript.Text = "Create JQScript";
            // 
            // chkMDController
            // 
            this.chkMDController.Location = new System.Drawing.Point(19, 55);
            this.chkMDController.Name = "chkMDController";
            this.chkMDController.Size = new System.Drawing.Size(152, 24);
            this.chkMDController.TabIndex = 49;
            this.chkMDController.Text = "Create Controllers";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.chkJQScript);
            this.groupBox7.Controls.Add(this.chkControllers);
            this.groupBox7.Controls.Add(this.chkModel);
            this.groupBox7.Controls.Add(this.groupBox3);
            this.groupBox7.Location = new System.Drawing.Point(552, 21);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(224, 206);
            this.groupBox7.TabIndex = 60;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "UI";
            // 
            // chkJQScript
            // 
            this.chkJQScript.Location = new System.Drawing.Point(42, 19);
            this.chkJQScript.Name = "chkJQScript";
            this.chkJQScript.Size = new System.Drawing.Size(152, 24);
            this.chkJQScript.TabIndex = 51;
            this.chkJQScript.Text = "Create JQScript";
            // 
            // chkControllers
            // 
            this.chkControllers.Location = new System.Drawing.Point(42, 45);
            this.chkControllers.Name = "chkControllers";
            this.chkControllers.Size = new System.Drawing.Size(152, 24);
            this.chkControllers.TabIndex = 52;
            this.chkControllers.Text = "Create Controllers";
            // 
            // chkModel
            // 
            this.chkModel.Location = new System.Drawing.Point(42, 71);
            this.chkModel.Name = "chkModel";
            this.chkModel.Size = new System.Drawing.Size(152, 24);
            this.chkModel.TabIndex = 53;
            this.chkModel.Text = "Create Model";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkEdit);
            this.groupBox3.Controls.Add(this.chkCreate);
            this.groupBox3.Controls.Add(this.chkIndex);
            this.groupBox3.Location = new System.Drawing.Point(42, 96);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(162, 100);
            this.groupBox3.TabIndex = 57;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "View";
            // 
            // chkEdit
            // 
            this.chkEdit.Location = new System.Drawing.Point(18, 67);
            this.chkEdit.Name = "chkEdit";
            this.chkEdit.Size = new System.Drawing.Size(137, 23);
            this.chkEdit.TabIndex = 41;
            this.chkEdit.Text = "Create Edit";
            // 
            // chkCreate
            // 
            this.chkCreate.Location = new System.Drawing.Point(18, 43);
            this.chkCreate.Name = "chkCreate";
            this.chkCreate.Size = new System.Drawing.Size(137, 24);
            this.chkCreate.TabIndex = 40;
            this.chkCreate.Text = "Create Create";
            // 
            // chkIndex
            // 
            this.chkIndex.Location = new System.Drawing.Point(18, 19);
            this.chkIndex.Name = "chkIndex";
            this.chkIndex.Size = new System.Drawing.Size(137, 24);
            this.chkIndex.TabIndex = 39;
            this.chkIndex.Text = "Create Index";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.chkIDAL);
            this.groupBox6.Controls.Add(this.chkDAL);
            this.groupBox6.Controls.Add(this.chkEntityMappers);
            this.groupBox6.Controls.Add(this.chkEntityMappersRef);
            this.groupBox6.Controls.Add(this.chkBLL);
            this.groupBox6.Location = new System.Drawing.Point(106, 21);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(356, 100);
            this.groupBox6.TabIndex = 59;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "BLL";
            // 
            // chkIDAL
            // 
            this.chkIDAL.Location = new System.Drawing.Point(6, 19);
            this.chkIDAL.Name = "chkIDAL";
            this.chkIDAL.Size = new System.Drawing.Size(92, 24);
            this.chkIDAL.TabIndex = 48;
            this.chkIDAL.Text = "Create IDAL";
            // 
            // chkDAL
            // 
            this.chkDAL.Location = new System.Drawing.Point(6, 49);
            this.chkDAL.Name = "chkDAL";
            this.chkDAL.Size = new System.Drawing.Size(92, 24);
            this.chkDAL.TabIndex = 49;
            this.chkDAL.Text = "Create DAL";
            // 
            // chkEntityMappers
            // 
            this.chkEntityMappers.Location = new System.Drawing.Point(104, 19);
            this.chkEntityMappers.Name = "chkEntityMappers";
            this.chkEntityMappers.Size = new System.Drawing.Size(143, 24);
            this.chkEntityMappers.TabIndex = 47;
            this.chkEntityMappers.Text = "Create EntityMappers";
            // 
            // chkEntityMappersRef
            // 
            this.chkEntityMappersRef.Location = new System.Drawing.Point(104, 49);
            this.chkEntityMappersRef.Name = "chkEntityMappersRef";
            this.chkEntityMappersRef.Size = new System.Drawing.Size(152, 24);
            this.chkEntityMappersRef.TabIndex = 55;
            this.chkEntityMappersRef.Text = "Create EntityMappersRef";
            // 
            // chkBLL
            // 
            this.chkBLL.Location = new System.Drawing.Point(253, 19);
            this.chkBLL.Name = "chkBLL";
            this.chkBLL.Size = new System.Drawing.Size(152, 24);
            this.chkBLL.TabIndex = 50;
            this.chkBLL.Text = "Create BLL";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.chkEntityRef);
            this.groupBox5.Controls.Add(this.chkEntity);
            this.groupBox5.Location = new System.Drawing.Point(106, 127);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 212);
            this.groupBox5.TabIndex = 58;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Entity";
            // 
            // chkEntityRef
            // 
            this.chkEntityRef.Location = new System.Drawing.Point(15, 53);
            this.chkEntityRef.Name = "chkEntityRef";
            this.chkEntityRef.Size = new System.Drawing.Size(152, 24);
            this.chkEntityRef.TabIndex = 54;
            this.chkEntityRef.Text = "Create EntityRef";
            // 
            // chkEntity
            // 
            this.chkEntity.Location = new System.Drawing.Point(15, 23);
            this.chkEntity.Name = "chkEntity";
            this.chkEntity.Size = new System.Drawing.Size(152, 24);
            this.chkEntity.TabIndex = 46;
            this.chkEntity.Text = "Create Entity";
            // 
            // btnMVCSelectAll
            // 
            this.btnMVCSelectAll.Location = new System.Drawing.Point(701, 242);
            this.btnMVCSelectAll.Name = "btnMVCSelectAll";
            this.btnMVCSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnMVCSelectAll.TabIndex = 56;
            this.btnMVCSelectAll.Text = "Select All";
            this.btnMVCSelectAll.UseVisualStyleBackColor = true;
            this.btnMVCSelectAll.Click += new System.EventHandler(this.btnMVCSelectAll_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(1132, 680);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(84, 48);
            this.btnExit.TabIndex = 33;
            this.btnExit.Text = "&Exit";
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(981, 680);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(145, 48);
            this.btnGenerate.TabIndex = 32;
            this.btnGenerate.Text = "Generate Code";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // chkWebApiController
            // 
            this.chkWebApiController.Location = new System.Drawing.Point(947, 39);
            this.chkWebApiController.Name = "chkWebApiController";
            this.chkWebApiController.Size = new System.Drawing.Size(143, 24);
            this.chkWebApiController.TabIndex = 64;
            this.chkWebApiController.Text = "Create Controller";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 740);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.tab);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tab.ResumeLayout(false);
            this.DBCOnnection.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.DBDetails.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.View.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgTableRef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).EndInit();
            this.Generate.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.MSSQL.ResumeLayout(false);
            this.WebAPINetCore.ResumeLayout(false);
            this.groupBox15.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.MVC.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage DBCOnnection;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.RadioButton rbCCRC;
        private System.Windows.Forms.RadioButton rbRCCC;
        private System.Windows.Forms.RadioButton rbRTRCCC;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDatabase;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtDataSource;
        private System.Windows.Forms.TabPage DBDetails;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox lbSelectedTableList1;
        private System.Windows.Forms.Button btnColapsExpanTreeView;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Button btnGetTableInfo;
        private System.Windows.Forms.TreeView tvTableInfo;
        private System.Windows.Forms.TreeView tvTableList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage View;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.Button btnLoadXML;
        private System.Windows.Forms.ComboBox cmbFileList;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button btnSaveXML;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ListBox lbSelectedTableList2;
        private System.Windows.Forms.Button btnSetFinal;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnRemoveChildList;
        private System.Windows.Forms.Button btnRemoveMasterList;
        private System.Windows.Forms.Button btnSetChild;
        private System.Windows.Forms.ListBox lbChildTableList;
        private System.Windows.Forms.ListBox lbMasterTableList;
        private System.Windows.Forms.Button btnSetMaster;
        private System.Windows.Forms.DataGridView dgTableRef;
        private System.Windows.Forms.DataGridView dgView;
        private System.Windows.Forms.TabPage Generate;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnFileOpen;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage MVC;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.CheckBox chkMDEdit;
        private System.Windows.Forms.CheckBox chkMDCreate;
        private System.Windows.Forms.CheckBox chkMDIndex;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox chkMDJQScript;
        private System.Windows.Forms.CheckBox chkMDController;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox chkJQScript;
        private System.Windows.Forms.CheckBox chkControllers;
        private System.Windows.Forms.CheckBox chkModel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkEdit;
        private System.Windows.Forms.CheckBox chkCreate;
        private System.Windows.Forms.CheckBox chkIndex;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox chkIDAL;
        private System.Windows.Forms.CheckBox chkDAL;
        private System.Windows.Forms.CheckBox chkEntityMappers;
        private System.Windows.Forms.CheckBox chkEntityMappersRef;
        private System.Windows.Forms.CheckBox chkBLL;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox chkEntityRef;
        private System.Windows.Forms.CheckBox chkEntity;
        private System.Windows.Forms.Button btnMVCSelectAll;
        private System.Windows.Forms.TabPage MSSQL;
        private System.Windows.Forms.CheckBox chkSPSearch;
        private System.Windows.Forms.CheckBox chkSPGet;
        private System.Windows.Forms.CheckBox chkSP_Del;
        private System.Windows.Forms.CheckBox chkSP_Upd;
        private System.Windows.Forms.CheckBox chkSP_Ins;
        private System.Windows.Forms.CheckBox chkSP_GAPg;
        private System.Windows.Forms.CheckBox chkSP_GA;
        private System.Windows.Forms.DataGridViewTextBoxColumn TableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TableAliasName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precsion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Scale;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lenght;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsMasterTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReferenceTableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn RefColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAliasName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDBType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDotNetType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnIsNull;
        private System.Windows.Forms.DataGridViewCheckBoxColumn HasReference;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsHidden;
        private System.Windows.Forms.DataGridViewCheckBoxColumn GetSPParam1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SearchSPParam;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SPOrderBy;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectForGetSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lable;
        private System.Windows.Forms.RadioButton rbRTRC;
        private System.Windows.Forms.Button btnGetandSet;
        private System.Windows.Forms.Button btnCheckSPAll;
        private System.Windows.Forms.TabPage WebAPINetCore;
        private System.Windows.Forms.Button btnchkWebApiCoreCheck;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.CheckBox chkWebApiCoreIDAL;
        private System.Windows.Forms.CheckBox chkWebApiCoreDAL;
        private System.Windows.Forms.CheckBox chkWebApiCoreEntity;
        private System.Windows.Forms.CheckBox chkWebApiCoreBLL;
        private System.Windows.Forms.TextBox txtModuleName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkWebApiCoreIBLL;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.CheckBox chkWebApiCoreIBLL_Ext;
        private System.Windows.Forms.CheckBox chkWebApiCoreIDAL_Ext;
        private System.Windows.Forms.CheckBox chkWebApiCoreDAL_Ext;
        private System.Windows.Forms.CheckBox chkWebApiCoreEntity_Ext;
        private System.Windows.Forms.CheckBox chkWebApiCoreBLL_Ext;
        private System.Windows.Forms.Button btnchkWebApiCoreCheck_Ext;
        private System.Windows.Forms.CheckBox chkWebApiCoreFCC;
        private System.Windows.Forms.CheckBox chkWebApiController;
    }
}


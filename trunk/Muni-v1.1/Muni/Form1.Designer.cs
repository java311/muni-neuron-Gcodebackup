namespace Muni
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_cratemodel = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_openmodel = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_savemodel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_view_website = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_about = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Create_Model = new System.Windows.Forms.ToolStripButton();
            this.Open_Model = new System.Windows.Forms.ToolStripButton();
            this.Save_Model = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Reset_Camera = new System.Windows.Forms.ToolStripButton();
            this.Reset_Model = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.simpleOpenGlControl1 = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_view = new System.Windows.Forms.TabPage();
            this.view_resetcamera = new System.Windows.Forms.Button();
            this.view_resetrotation = new System.Windows.Forms.Button();
            this.view_points = new System.Windows.Forms.RadioButton();
            this.view_lines = new System.Windows.Forms.RadioButton();
            this.view_triangles = new System.Windows.Forms.RadioButton();
            this.view_length = new System.Windows.Forms.CheckBox();
            this.view_neuron = new System.Windows.Forms.CheckBox();
            this.view_container = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.view_axes = new System.Windows.Forms.CheckBox();
            this.view_apply = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.view_escz = new System.Windows.Forms.TextBox();
            this.view_escy = new System.Windows.Forms.TextBox();
            this.view_escx = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tab_color = new System.Windows.Forms.TabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.color_trackB = new System.Windows.Forms.TrackBar();
            this.label9 = new System.Windows.Forms.Label();
            this.color_trackG = new System.Windows.Forms.TrackBar();
            this.label8 = new System.Windows.Forms.Label();
            this.color_trackR = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.simpleOpenGlControl2 = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.view_change = new System.Windows.Forms.Button();
            this.color_combo = new System.Windows.Forms.ComboBox();
            this.tab_measure = new System.Windows.Forms.TabPage();
            this.measure_view_length = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.measure_microns = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.measure_pixels = new System.Windows.Forms.Label();
            this.measure_objetive = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.measure_result = new System.Windows.Forms.Label();
            this.mesure_resetboth = new System.Windows.Forms.Button();
            this.measure_back = new System.Windows.Forms.Button();
            this.measuer_ahead = new System.Windows.Forms.Button();
            this.measure_left = new System.Windows.Forms.Button();
            this.measure_reset = new System.Windows.Forms.Button();
            this.measure_down = new System.Windows.Forms.Button();
            this.measure_up = new System.Windows.Forms.Button();
            this.measure_right = new System.Windows.Forms.Button();
            this.tab_camera = new System.Windows.Forms.TabPage();
            this.label19 = new System.Windows.Forms.Label();
            this.camera_resetrotation = new System.Windows.Forms.Button();
            this.camera_panel1_backward = new System.Windows.Forms.Button();
            this.camera_panel1_foward = new System.Windows.Forms.Button();
            this.camera_panel1_left = new System.Windows.Forms.Button();
            this.camera_panel1_reset = new System.Windows.Forms.Button();
            this.camera_panel1_down = new System.Windows.Forms.Button();
            this.camera_panel1_up = new System.Windows.Forms.Button();
            this.camera_panel1_right = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.progress = new System.Windows.Forms.ToolStripProgressBar();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.memo1 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tab_view.SuspendLayout();
            this.tab_color.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.color_trackB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.color_trackG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.color_trackR)).BeginInit();
            this.tab_measure.SuspendLayout();
            this.tab_camera.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(792, 474);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.menuStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(891, 612);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(891, 20);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_cratemodel,
            this.menu_openmodel,
            this.menu_savemodel,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 16);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // menu_cratemodel
            // 
            this.menu_cratemodel.Image = ((System.Drawing.Image)(resources.GetObject("menu_cratemodel.Image")));
            this.menu_cratemodel.Name = "menu_cratemodel";
            this.menu_cratemodel.Size = new System.Drawing.Size(149, 22);
            this.menu_cratemodel.Text = "Create Model";
            this.menu_cratemodel.Click += new System.EventHandler(this.openImagesToolStripMenuItem_Click);
            // 
            // menu_openmodel
            // 
            this.menu_openmodel.Image = ((System.Drawing.Image)(resources.GetObject("menu_openmodel.Image")));
            this.menu_openmodel.Name = "menu_openmodel";
            this.menu_openmodel.Size = new System.Drawing.Size(149, 22);
            this.menu_openmodel.Text = "Open Model";
            this.menu_openmodel.Click += new System.EventHandler(this.menu_openmodel_Click);
            // 
            // menu_savemodel
            // 
            this.menu_savemodel.Enabled = false;
            this.menu_savemodel.Image = ((System.Drawing.Image)(resources.GetObject("menu_savemodel.Image")));
            this.menu_savemodel.Name = "menu_savemodel";
            this.menu_savemodel.Size = new System.Drawing.Size(149, 22);
            this.menu_savemodel.Text = "Save Model";
            this.menu_savemodel.Click += new System.EventHandler(this.menu_savemodel_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(146, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripMenuItem.Image")));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_view_website,
            this.toolStripSeparator3,
            this.menu_about});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 16);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // menu_view_website
            // 
            this.menu_view_website.Image = ((System.Drawing.Image)(resources.GetObject("menu_view_website.Image")));
            this.menu_view_website.Name = "menu_view_website";
            this.menu_view_website.Size = new System.Drawing.Size(149, 22);
            this.menu_view_website.Text = "View Website";
            this.menu_view_website.Click += new System.EventHandler(this.menu_view_website_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(146, 6);
            // 
            // menu_about
            // 
            this.menu_about.Image = ((System.Drawing.Image)(resources.GetObject("menu_about.Image")));
            this.menu_about.Name = "menu_about";
            this.menu_about.Size = new System.Drawing.Size(149, 22);
            this.menu_about.Text = "About";
            this.menu_about.Click += new System.EventHandler(this.menu_about_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Create_Model,
            this.Open_Model,
            this.Save_Model,
            this.toolStripSeparator1,
            this.Reset_Camera,
            this.Reset_Model});
            this.toolStrip1.Location = new System.Drawing.Point(0, 20);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(891, 39);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Create_Model
            // 
            this.Create_Model.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Create_Model.Image = ((System.Drawing.Image)(resources.GetObject("Create_Model.Image")));
            this.Create_Model.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Create_Model.Name = "Create_Model";
            this.Create_Model.Size = new System.Drawing.Size(36, 36);
            this.Create_Model.Text = "Create Model";
            this.Create_Model.Click += new System.EventHandler(this.openImagesToolStripMenuItem_Click);
            // 
            // Open_Model
            // 
            this.Open_Model.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Open_Model.Image = ((System.Drawing.Image)(resources.GetObject("Open_Model.Image")));
            this.Open_Model.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Open_Model.Name = "Open_Model";
            this.Open_Model.Size = new System.Drawing.Size(36, 36);
            this.Open_Model.Text = "Open Model";
            this.Open_Model.Click += new System.EventHandler(this.menu_openmodel_Click);
            // 
            // Save_Model
            // 
            this.Save_Model.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Save_Model.Enabled = false;
            this.Save_Model.Image = ((System.Drawing.Image)(resources.GetObject("Save_Model.Image")));
            this.Save_Model.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Save_Model.Name = "Save_Model";
            this.Save_Model.Size = new System.Drawing.Size(36, 36);
            this.Save_Model.Text = "Save Model";
            this.Save_Model.Click += new System.EventHandler(this.menu_savemodel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // Reset_Camera
            // 
            this.Reset_Camera.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Reset_Camera.Image = ((System.Drawing.Image)(resources.GetObject("Reset_Camera.Image")));
            this.Reset_Camera.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Reset_Camera.Name = "Reset_Camera";
            this.Reset_Camera.Size = new System.Drawing.Size(36, 36);
            this.Reset_Camera.Text = "Reset Camera Position";
            this.Reset_Camera.Click += new System.EventHandler(this.camera_panel1_reset_Click);
            // 
            // Reset_Model
            // 
            this.Reset_Model.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Reset_Model.Image = ((System.Drawing.Image)(resources.GetObject("Reset_Model.Image")));
            this.Reset_Model.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Reset_Model.Name = "Reset_Model";
            this.Reset_Model.Size = new System.Drawing.Size(36, 36);
            this.Reset_Model.Text = "Reset Model Rotation";
            this.Reset_Model.Click += new System.EventHandler(this.view_resetrotation_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(3, 63);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.simpleOpenGlControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(885, 526);
            this.splitContainer1.SplitterDistance = 323;
            this.splitContainer1.TabIndex = 4;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // simpleOpenGlControl1
            // 
            this.simpleOpenGlControl1.AccumBits = ((byte)(0));
            this.simpleOpenGlControl1.AutoCheckErrors = false;
            this.simpleOpenGlControl1.AutoFinish = false;
            this.simpleOpenGlControl1.AutoMakeCurrent = true;
            this.simpleOpenGlControl1.AutoSwapBuffers = true;
            this.simpleOpenGlControl1.BackColor = System.Drawing.Color.Black;
            this.simpleOpenGlControl1.ColorBits = ((byte)(32));
            this.simpleOpenGlControl1.DepthBits = ((byte)(16));
            this.simpleOpenGlControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simpleOpenGlControl1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.simpleOpenGlControl1.Location = new System.Drawing.Point(0, 0);
            this.simpleOpenGlControl1.Name = "simpleOpenGlControl1";
            this.simpleOpenGlControl1.Size = new System.Drawing.Size(885, 323);
            this.simpleOpenGlControl1.StencilBits = ((byte)(0));
            this.simpleOpenGlControl1.TabIndex = 1;
            this.simpleOpenGlControl1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.simpleOpenGlControl1_MouseWheel);
            this.simpleOpenGlControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.simpleOpenGlControl1_Paint);
            this.simpleOpenGlControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.simpleOpenGlControl1_MouseMove);
            this.simpleOpenGlControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.simpleOpenGlControl1_MouseClick);
            this.simpleOpenGlControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.simpleOpenGlControl1_MouseDown);
            this.simpleOpenGlControl1.Enter += new System.EventHandler(this.simpleOpenGlControl1_Enter);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_view);
            this.tabControl1.Controls.Add(this.tab_color);
            this.tabControl1.Controls.Add(this.tab_measure);
            this.tabControl1.Controls.Add(this.tab_camera);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Enabled = false;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(885, 199);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tab_view
            // 
            this.tab_view.Controls.Add(this.view_resetcamera);
            this.tab_view.Controls.Add(this.view_resetrotation);
            this.tab_view.Controls.Add(this.view_points);
            this.tab_view.Controls.Add(this.view_lines);
            this.tab_view.Controls.Add(this.view_triangles);
            this.tab_view.Controls.Add(this.view_length);
            this.tab_view.Controls.Add(this.view_neuron);
            this.tab_view.Controls.Add(this.view_container);
            this.tab_view.Controls.Add(this.label7);
            this.tab_view.Controls.Add(this.view_axes);
            this.tab_view.Controls.Add(this.view_apply);
            this.tab_view.Controls.Add(this.label5);
            this.tab_view.Controls.Add(this.view_escz);
            this.tab_view.Controls.Add(this.view_escy);
            this.tab_view.Controls.Add(this.view_escx);
            this.tab_view.Controls.Add(this.label4);
            this.tab_view.Controls.Add(this.label3);
            this.tab_view.Controls.Add(this.label2);
            this.tab_view.Controls.Add(this.label1);
            this.tab_view.ImageIndex = 0;
            this.tab_view.Location = new System.Drawing.Point(4, 31);
            this.tab_view.Name = "tab_view";
            this.tab_view.Padding = new System.Windows.Forms.Padding(3);
            this.tab_view.Size = new System.Drawing.Size(877, 164);
            this.tab_view.TabIndex = 0;
            this.tab_view.Text = "View";
            this.tab_view.UseVisualStyleBackColor = true;
            // 
            // view_resetcamera
            // 
            this.view_resetcamera.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.view_resetcamera.Image = ((System.Drawing.Image)(resources.GetObject("view_resetcamera.Image")));
            this.view_resetcamera.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.view_resetcamera.Location = new System.Drawing.Point(604, 43);
            this.view_resetcamera.Name = "view_resetcamera";
            this.view_resetcamera.Size = new System.Drawing.Size(133, 32);
            this.view_resetcamera.TabIndex = 21;
            this.view_resetcamera.Text = "Reset Camera";
            this.view_resetcamera.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.view_resetcamera.UseVisualStyleBackColor = true;
            this.view_resetcamera.Click += new System.EventHandler(this.camera_panel1_reset_Click);
            // 
            // view_resetrotation
            // 
            this.view_resetrotation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.view_resetrotation.Image = ((System.Drawing.Image)(resources.GetObject("view_resetrotation.Image")));
            this.view_resetrotation.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.view_resetrotation.Location = new System.Drawing.Point(604, 95);
            this.view_resetrotation.Name = "view_resetrotation";
            this.view_resetrotation.Size = new System.Drawing.Size(133, 32);
            this.view_resetrotation.TabIndex = 20;
            this.view_resetrotation.Text = "Reset Rotation";
            this.view_resetrotation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.view_resetrotation.UseVisualStyleBackColor = true;
            this.view_resetrotation.Click += new System.EventHandler(this.view_resetrotation_Click);
            // 
            // view_points
            // 
            this.view_points.AutoSize = true;
            this.view_points.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.view_points.Location = new System.Drawing.Point(305, 91);
            this.view_points.Name = "view_points";
            this.view_points.Size = new System.Drawing.Size(59, 19);
            this.view_points.TabIndex = 19;
            this.view_points.Text = "Points";
            this.view_points.UseVisualStyleBackColor = true;
            this.view_points.Click += new System.EventHandler(this.viewradiocheck);
            // 
            // view_lines
            // 
            this.view_lines.AutoSize = true;
            this.view_lines.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.view_lines.Location = new System.Drawing.Point(305, 66);
            this.view_lines.Name = "view_lines";
            this.view_lines.Size = new System.Drawing.Size(53, 19);
            this.view_lines.TabIndex = 18;
            this.view_lines.Text = "Lines";
            this.view_lines.UseVisualStyleBackColor = true;
            this.view_lines.Click += new System.EventHandler(this.viewradiocheck);
            // 
            // view_triangles
            // 
            this.view_triangles.AutoSize = true;
            this.view_triangles.Checked = true;
            this.view_triangles.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.view_triangles.Location = new System.Drawing.Point(305, 42);
            this.view_triangles.Name = "view_triangles";
            this.view_triangles.Size = new System.Drawing.Size(75, 19);
            this.view_triangles.TabIndex = 17;
            this.view_triangles.TabStop = true;
            this.view_triangles.Text = "Triangles";
            this.view_triangles.UseVisualStyleBackColor = true;
            this.view_triangles.Click += new System.EventHandler(this.viewradiocheck);
            // 
            // view_length
            // 
            this.view_length.AutoSize = true;
            this.view_length.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.view_length.Location = new System.Drawing.Point(432, 119);
            this.view_length.Name = "view_length";
            this.view_length.Size = new System.Drawing.Size(114, 19);
            this.view_length.TabIndex = 16;
            this.view_length.Text = "Length Markers";
            this.view_length.UseVisualStyleBackColor = true;
            this.view_length.Click += new System.EventHandler(this.viewradiocheck);
            // 
            // view_neuron
            // 
            this.view_neuron.AutoSize = true;
            this.view_neuron.Checked = true;
            this.view_neuron.CheckState = System.Windows.Forms.CheckState.Checked;
            this.view_neuron.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.view_neuron.Location = new System.Drawing.Point(432, 67);
            this.view_neuron.Name = "view_neuron";
            this.view_neuron.Size = new System.Drawing.Size(68, 19);
            this.view_neuron.TabIndex = 15;
            this.view_neuron.Text = "Neuron";
            this.view_neuron.UseVisualStyleBackColor = true;
            this.view_neuron.Click += new System.EventHandler(this.viewradiocheck);
            // 
            // view_container
            // 
            this.view_container.AutoSize = true;
            this.view_container.Checked = true;
            this.view_container.CheckState = System.Windows.Forms.CheckState.Checked;
            this.view_container.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.view_container.Location = new System.Drawing.Point(432, 93);
            this.view_container.Name = "view_container";
            this.view_container.Size = new System.Drawing.Size(104, 19);
            this.view_container.TabIndex = 14;
            this.view_container.Text = "Container box";
            this.view_container.UseVisualStyleBackColor = true;
            this.view_container.Click += new System.EventHandler(this.viewradiocheck);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(413, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 15);
            this.label7.TabIndex = 13;
            this.label7.Text = "Scene Objects :";
            // 
            // view_axes
            // 
            this.view_axes.AutoSize = true;
            this.view_axes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.view_axes.Location = new System.Drawing.Point(432, 43);
            this.view_axes.Name = "view_axes";
            this.view_axes.Size = new System.Drawing.Size(53, 19);
            this.view_axes.TabIndex = 12;
            this.view_axes.Text = "Axes";
            this.view_axes.UseVisualStyleBackColor = true;
            this.view_axes.Click += new System.EventHandler(this.viewradiocheck);
            // 
            // view_apply
            // 
            this.view_apply.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.view_apply.Image = ((System.Drawing.Image)(resources.GetObject("view_apply.Image")));
            this.view_apply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.view_apply.Location = new System.Drawing.Point(90, 93);
            this.view_apply.Name = "view_apply";
            this.view_apply.Size = new System.Drawing.Size(89, 34);
            this.view_apply.TabIndex = 11;
            this.view_apply.Text = "Apply ";
            this.view_apply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.view_apply.UseVisualStyleBackColor = true;
            this.view_apply.Click += new System.EventHandler(this.view_apply_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(286, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Model View :";
            // 
            // view_escz
            // 
            this.view_escz.Location = new System.Drawing.Point(209, 41);
            this.view_escz.Name = "view_escz";
            this.view_escz.Size = new System.Drawing.Size(37, 20);
            this.view_escz.TabIndex = 6;
            this.view_escz.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.view_escx_KeyPress);
            // 
            // view_escy
            // 
            this.view_escy.Location = new System.Drawing.Point(129, 41);
            this.view_escy.Name = "view_escy";
            this.view_escy.Size = new System.Drawing.Size(37, 20);
            this.view_escy.TabIndex = 5;
            this.view_escy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.view_escx_KeyPress);
            // 
            // view_escx
            // 
            this.view_escx.Location = new System.Drawing.Point(51, 41);
            this.view_escx.Name = "view_escx";
            this.view_escx.Size = new System.Drawing.Size(37, 20);
            this.view_escx.TabIndex = 4;
            this.view_escx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.view_escx_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(102, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Y :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(182, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Z :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "X :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Scene scale factor :";
            // 
            // tab_color
            // 
            this.tab_color.Controls.Add(this.label12);
            this.tab_color.Controls.Add(this.label11);
            this.tab_color.Controls.Add(this.label10);
            this.tab_color.Controls.Add(this.color_trackB);
            this.tab_color.Controls.Add(this.label9);
            this.tab_color.Controls.Add(this.color_trackG);
            this.tab_color.Controls.Add(this.label8);
            this.tab_color.Controls.Add(this.color_trackR);
            this.tab_color.Controls.Add(this.label6);
            this.tab_color.Controls.Add(this.simpleOpenGlControl2);
            this.tab_color.Controls.Add(this.view_change);
            this.tab_color.Controls.Add(this.color_combo);
            this.tab_color.ImageIndex = 1;
            this.tab_color.Location = new System.Drawing.Point(4, 31);
            this.tab_color.Name = "tab_color";
            this.tab_color.Padding = new System.Windows.Forms.Padding(3);
            this.tab_color.Size = new System.Drawing.Size(877, 164);
            this.tab_color.TabIndex = 1;
            this.tab_color.Text = "Color";
            this.tab_color.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(397, 142);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(28, 15);
            this.label12.TabIndex = 12;
            this.label12.Text = "255";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(74, 141);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 15);
            this.label11.TabIndex = 11;
            this.label11.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(12, 113);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 15);
            this.label10.TabIndex = 10;
            this.label10.Text = "Blue : ";
            // 
            // color_trackB
            // 
            this.color_trackB.Location = new System.Drawing.Point(69, 103);
            this.color_trackB.Maximum = 255;
            this.color_trackB.Name = "color_trackB";
            this.color_trackB.Size = new System.Drawing.Size(352, 45);
            this.color_trackB.TabIndex = 9;
            this.color_trackB.Scroll += new System.EventHandler(this.color_trackB_Scroll);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(12, 85);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 15);
            this.label9.TabIndex = 8;
            this.label9.Text = "Green : ";
            // 
            // color_trackG
            // 
            this.color_trackG.Location = new System.Drawing.Point(69, 74);
            this.color_trackG.Maximum = 255;
            this.color_trackG.Name = "color_trackG";
            this.color_trackG.Size = new System.Drawing.Size(352, 45);
            this.color_trackG.TabIndex = 7;
            this.color_trackG.Scroll += new System.EventHandler(this.color_trackG_Scroll);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 15);
            this.label8.TabIndex = 6;
            this.label8.Text = "Red :";
            // 
            // color_trackR
            // 
            this.color_trackR.Location = new System.Drawing.Point(69, 47);
            this.color_trackR.Maximum = 255;
            this.color_trackR.Name = "color_trackR";
            this.color_trackR.Size = new System.Drawing.Size(352, 45);
            this.color_trackR.TabIndex = 5;
            this.color_trackR.Scroll += new System.EventHandler(this.color_trackR_Scroll);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(543, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "Actual Color";
            // 
            // simpleOpenGlControl2
            // 
            this.simpleOpenGlControl2.AccumBits = ((byte)(0));
            this.simpleOpenGlControl2.AutoCheckErrors = false;
            this.simpleOpenGlControl2.AutoFinish = false;
            this.simpleOpenGlControl2.AutoMakeCurrent = true;
            this.simpleOpenGlControl2.AutoSwapBuffers = true;
            this.simpleOpenGlControl2.BackColor = System.Drawing.Color.Black;
            this.simpleOpenGlControl2.ColorBits = ((byte)(32));
            this.simpleOpenGlControl2.DepthBits = ((byte)(16));
            this.simpleOpenGlControl2.Location = new System.Drawing.Point(490, 3);
            this.simpleOpenGlControl2.Name = "simpleOpenGlControl2";
            this.simpleOpenGlControl2.Size = new System.Drawing.Size(181, 160);
            this.simpleOpenGlControl2.StencilBits = ((byte)(0));
            this.simpleOpenGlControl2.TabIndex = 4;
            // 
            // view_change
            // 
            this.view_change.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.view_change.Image = ((System.Drawing.Image)(resources.GetObject("view_change.Image")));
            this.view_change.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.view_change.Location = new System.Drawing.Point(259, 7);
            this.view_change.Name = "view_change";
            this.view_change.Size = new System.Drawing.Size(89, 34);
            this.view_change.TabIndex = 3;
            this.view_change.Text = "Change ";
            this.view_change.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.view_change.UseVisualStyleBackColor = true;
            this.view_change.Click += new System.EventHandler(this.view_change_Click);
            // 
            // color_combo
            // 
            this.color_combo.FormattingEnabled = true;
            this.color_combo.Items.AddRange(new object[] {
            "Neuron",
            "Background",
            "Length Markers",
            "Container Box"});
            this.color_combo.Location = new System.Drawing.Point(33, 15);
            this.color_combo.Name = "color_combo";
            this.color_combo.Size = new System.Drawing.Size(149, 21);
            this.color_combo.TabIndex = 0;
            this.color_combo.SelectedIndexChanged += new System.EventHandler(this.color_combo_SelectedIndexChanged);
            // 
            // tab_measure
            // 
            this.tab_measure.Controls.Add(this.measure_view_length);
            this.tab_measure.Controls.Add(this.label18);
            this.tab_measure.Controls.Add(this.measure_microns);
            this.tab_measure.Controls.Add(this.label16);
            this.tab_measure.Controls.Add(this.label17);
            this.tab_measure.Controls.Add(this.measure_pixels);
            this.tab_measure.Controls.Add(this.measure_objetive);
            this.tab_measure.Controls.Add(this.label15);
            this.tab_measure.Controls.Add(this.label14);
            this.tab_measure.Controls.Add(this.label13);
            this.tab_measure.Controls.Add(this.measure_result);
            this.tab_measure.Controls.Add(this.mesure_resetboth);
            this.tab_measure.Controls.Add(this.measure_back);
            this.tab_measure.Controls.Add(this.measuer_ahead);
            this.tab_measure.Controls.Add(this.measure_left);
            this.tab_measure.Controls.Add(this.measure_reset);
            this.tab_measure.Controls.Add(this.measure_down);
            this.tab_measure.Controls.Add(this.measure_up);
            this.tab_measure.Controls.Add(this.measure_right);
            this.tab_measure.ImageIndex = 2;
            this.tab_measure.Location = new System.Drawing.Point(4, 31);
            this.tab_measure.Name = "tab_measure";
            this.tab_measure.Size = new System.Drawing.Size(877, 164);
            this.tab_measure.TabIndex = 3;
            this.tab_measure.Text = "Measure";
            this.tab_measure.UseVisualStyleBackColor = true;
            // 
            // measure_view_length
            // 
            this.measure_view_length.AutoSize = true;
            this.measure_view_length.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.measure_view_length.Location = new System.Drawing.Point(19, 14);
            this.measure_view_length.Name = "measure_view_length";
            this.measure_view_length.Size = new System.Drawing.Size(145, 19);
            this.measure_view_length.TabIndex = 29;
            this.measure_view_length.Text = "View Length Markers";
            this.measure_view_length.UseVisualStyleBackColor = true;
            this.measure_view_length.CheckedChanged += new System.EventHandler(this.measure_view_length_CheckedChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(702, 38);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 20);
            this.label18.TabIndex = 28;
            this.label18.Text = "microns";
            // 
            // measure_microns
            // 
            this.measure_microns.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.measure_microns.ForeColor = System.Drawing.SystemColors.ControlText;
            this.measure_microns.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.measure_microns.Location = new System.Drawing.Point(619, 38);
            this.measure_microns.Name = "measure_microns";
            this.measure_microns.Size = new System.Drawing.Size(77, 20);
            this.measure_microns.TabIndex = 27;
            this.measure_microns.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(740, 120);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 20);
            this.label16.TabIndex = 26;
            this.label16.Text = "microns";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(538, 38);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(75, 20);
            this.label17.TabIndex = 25;
            this.label17.Text = "pixels are";
            // 
            // measure_pixels
            // 
            this.measure_pixels.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.measure_pixels.ForeColor = System.Drawing.SystemColors.ControlText;
            this.measure_pixels.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.measure_pixels.Location = new System.Drawing.Point(451, 38);
            this.measure_pixels.Name = "measure_pixels";
            this.measure_pixels.Size = new System.Drawing.Size(77, 20);
            this.measure_pixels.TabIndex = 24;
            this.measure_pixels.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // measure_objetive
            // 
            this.measure_objetive.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.measure_objetive.ForeColor = System.Drawing.SystemColors.ControlText;
            this.measure_objetive.Location = new System.Drawing.Point(628, 119);
            this.measure_objetive.Name = "measure_objetive";
            this.measure_objetive.Size = new System.Drawing.Size(106, 20);
            this.measure_objetive.TabIndex = 23;
            this.measure_objetive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(451, 119);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(184, 20);
            this.label15.TabIndex = 22;
            this.label15.Text = "Objetive Magnification :  ";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(650, 75);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 20);
            this.label14.TabIndex = 21;
            this.label14.Text = "microns";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(451, 75);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(81, 20);
            this.label13.TabIndex = 20;
            this.label13.Text = "Distance : ";
            // 
            // measure_result
            // 
            this.measure_result.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.measure_result.ForeColor = System.Drawing.SystemColors.ControlText;
            this.measure_result.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.measure_result.Location = new System.Drawing.Point(538, 75);
            this.measure_result.Name = "measure_result";
            this.measure_result.Size = new System.Drawing.Size(106, 20);
            this.measure_result.TabIndex = 19;
            this.measure_result.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mesure_resetboth
            // 
            this.mesure_resetboth.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.mesure_resetboth.Image = ((System.Drawing.Image)(resources.GetObject("mesure_resetboth.Image")));
            this.mesure_resetboth.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.mesure_resetboth.Location = new System.Drawing.Point(272, 95);
            this.mesure_resetboth.Name = "mesure_resetboth";
            this.mesure_resetboth.Size = new System.Drawing.Size(104, 32);
            this.mesure_resetboth.TabIndex = 18;
            this.mesure_resetboth.Text = "Reset Both";
            this.mesure_resetboth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mesure_resetboth.UseVisualStyleBackColor = true;
            this.mesure_resetboth.Click += new System.EventHandler(this.mesure_resetboth_Click);
            // 
            // measure_back
            // 
            this.measure_back.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.measure_back.Image = ((System.Drawing.Image)(resources.GetObject("measure_back.Image")));
            this.measure_back.Location = new System.Drawing.Point(67, 90);
            this.measure_back.Name = "measure_back";
            this.measure_back.Size = new System.Drawing.Size(46, 37);
            this.measure_back.TabIndex = 17;
            this.toolTip1.SetToolTip(this.measure_back, "Move the length marker back (Z)");
            this.measure_back.UseVisualStyleBackColor = true;
            this.measure_back.Click += new System.EventHandler(this.measure_back_Click);
            // 
            // measuer_ahead
            // 
            this.measuer_ahead.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.measuer_ahead.Image = ((System.Drawing.Image)(resources.GetObject("measuer_ahead.Image")));
            this.measuer_ahead.Location = new System.Drawing.Point(69, 48);
            this.measuer_ahead.Name = "measuer_ahead";
            this.measuer_ahead.Size = new System.Drawing.Size(44, 37);
            this.measuer_ahead.TabIndex = 16;
            this.toolTip1.SetToolTip(this.measuer_ahead, "Move the length marker ahead (Z)");
            this.measuer_ahead.UseVisualStyleBackColor = true;
            this.measuer_ahead.Click += new System.EventHandler(this.measuer_ahead_Click);
            // 
            // measure_left
            // 
            this.measure_left.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.measure_left.Image = ((System.Drawing.Image)(resources.GetObject("measure_left.Image")));
            this.measure_left.Location = new System.Drawing.Point(19, 90);
            this.measure_left.Name = "measure_left";
            this.measure_left.Size = new System.Drawing.Size(44, 37);
            this.measure_left.TabIndex = 13;
            this.toolTip1.SetToolTip(this.measure_left, "Move the length marker left (X)");
            this.measure_left.UseVisualStyleBackColor = true;
            this.measure_left.Click += new System.EventHandler(this.measure_left_Click);
            // 
            // measure_reset
            // 
            this.measure_reset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.measure_reset.Image = ((System.Drawing.Image)(resources.GetObject("measure_reset.Image")));
            this.measure_reset.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.measure_reset.Location = new System.Drawing.Point(272, 50);
            this.measure_reset.Name = "measure_reset";
            this.measure_reset.Size = new System.Drawing.Size(104, 32);
            this.measure_reset.TabIndex = 15;
            this.measure_reset.Text = "Reset";
            this.measure_reset.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.measure_reset.UseVisualStyleBackColor = true;
            this.measure_reset.Click += new System.EventHandler(this.measure_reset_Click);
            // 
            // measure_down
            // 
            this.measure_down.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.measure_down.Image = ((System.Drawing.Image)(resources.GetObject("measure_down.Image")));
            this.measure_down.Location = new System.Drawing.Point(196, 90);
            this.measure_down.Name = "measure_down";
            this.measure_down.Size = new System.Drawing.Size(44, 37);
            this.measure_down.TabIndex = 12;
            this.toolTip1.SetToolTip(this.measure_down, "Move the length marker down (Y)");
            this.measure_down.UseVisualStyleBackColor = true;
            this.measure_down.Click += new System.EventHandler(this.measure_down_Click);
            // 
            // measure_up
            // 
            this.measure_up.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.measure_up.Image = ((System.Drawing.Image)(resources.GetObject("measure_up.Image")));
            this.measure_up.Location = new System.Drawing.Point(196, 48);
            this.measure_up.Name = "measure_up";
            this.measure_up.Size = new System.Drawing.Size(44, 37);
            this.measure_up.TabIndex = 11;
            this.toolTip1.SetToolTip(this.measure_up, "Move the length marker up (Y)");
            this.measure_up.UseVisualStyleBackColor = true;
            this.measure_up.Click += new System.EventHandler(this.measure_up_Click);
            // 
            // measure_right
            // 
            this.measure_right.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.measure_right.Image = ((System.Drawing.Image)(resources.GetObject("measure_right.Image")));
            this.measure_right.Location = new System.Drawing.Point(119, 90);
            this.measure_right.Name = "measure_right";
            this.measure_right.Size = new System.Drawing.Size(46, 36);
            this.measure_right.TabIndex = 14;
            this.toolTip1.SetToolTip(this.measure_right, "Move the length marker right (X)");
            this.measure_right.UseVisualStyleBackColor = true;
            this.measure_right.Click += new System.EventHandler(this.measure_right_Click);
            // 
            // tab_camera
            // 
            this.tab_camera.Controls.Add(this.memo1);
            this.tab_camera.Controls.Add(this.label19);
            this.tab_camera.Controls.Add(this.camera_resetrotation);
            this.tab_camera.Controls.Add(this.camera_panel1_backward);
            this.tab_camera.Controls.Add(this.camera_panel1_foward);
            this.tab_camera.Controls.Add(this.camera_panel1_left);
            this.tab_camera.Controls.Add(this.camera_panel1_reset);
            this.tab_camera.Controls.Add(this.camera_panel1_down);
            this.tab_camera.Controls.Add(this.camera_panel1_up);
            this.tab_camera.Controls.Add(this.camera_panel1_right);
            this.tab_camera.ImageIndex = 3;
            this.tab_camera.Location = new System.Drawing.Point(4, 31);
            this.tab_camera.Name = "tab_camera";
            this.tab_camera.Size = new System.Drawing.Size(877, 164);
            this.tab_camera.TabIndex = 2;
            this.tab_camera.Text = "Camera";
            this.tab_camera.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(9, 14);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(150, 15);
            this.label19.TabIndex = 22;
            this.label19.Text = "Camera position controls :";
            // 
            // camera_resetrotation
            // 
            this.camera_resetrotation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.camera_resetrotation.Image = ((System.Drawing.Image)(resources.GetObject("camera_resetrotation.Image")));
            this.camera_resetrotation.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.camera_resetrotation.Location = new System.Drawing.Point(284, 97);
            this.camera_resetrotation.Name = "camera_resetrotation";
            this.camera_resetrotation.Size = new System.Drawing.Size(137, 37);
            this.camera_resetrotation.TabIndex = 21;
            this.camera_resetrotation.Text = "Reset Rotation";
            this.camera_resetrotation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.camera_resetrotation.UseVisualStyleBackColor = true;
            this.camera_resetrotation.Click += new System.EventHandler(this.view_resetrotation_Click);
            // 
            // camera_panel1_backward
            // 
            this.camera_panel1_backward.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.camera_panel1_backward.Image = ((System.Drawing.Image)(resources.GetObject("camera_panel1_backward.Image")));
            this.camera_panel1_backward.Location = new System.Drawing.Point(62, 96);
            this.camera_panel1_backward.Name = "camera_panel1_backward";
            this.camera_panel1_backward.Size = new System.Drawing.Size(44, 37);
            this.camera_panel1_backward.TabIndex = 10;
            this.toolTip1.SetToolTip(this.camera_panel1_backward, "Move Camera Back");
            this.camera_panel1_backward.UseVisualStyleBackColor = true;
            this.camera_panel1_backward.Click += new System.EventHandler(this.camera_panel1_backward_Click);
            // 
            // camera_panel1_foward
            // 
            this.camera_panel1_foward.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.camera_panel1_foward.Image = ((System.Drawing.Image)(resources.GetObject("camera_panel1_foward.Image")));
            this.camera_panel1_foward.Location = new System.Drawing.Point(62, 54);
            this.camera_panel1_foward.Name = "camera_panel1_foward";
            this.camera_panel1_foward.Size = new System.Drawing.Size(44, 37);
            this.camera_panel1_foward.TabIndex = 9;
            this.toolTip1.SetToolTip(this.camera_panel1_foward, "Move camera Ahead");
            this.camera_panel1_foward.UseVisualStyleBackColor = true;
            this.camera_panel1_foward.Click += new System.EventHandler(this.camera_panel1_foward_Click);
            // 
            // camera_panel1_left
            // 
            this.camera_panel1_left.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.camera_panel1_left.Image = ((System.Drawing.Image)(resources.GetObject("camera_panel1_left.Image")));
            this.camera_panel1_left.Location = new System.Drawing.Point(12, 96);
            this.camera_panel1_left.Name = "camera_panel1_left";
            this.camera_panel1_left.Size = new System.Drawing.Size(44, 37);
            this.camera_panel1_left.TabIndex = 6;
            this.toolTip1.SetToolTip(this.camera_panel1_left, "Rotate camera to the left");
            this.camera_panel1_left.UseVisualStyleBackColor = true;
            this.camera_panel1_left.Click += new System.EventHandler(this.camera_panel1_left_Click);
            // 
            // camera_panel1_reset
            // 
            this.camera_panel1_reset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.camera_panel1_reset.Image = ((System.Drawing.Image)(resources.GetObject("camera_panel1_reset.Image")));
            this.camera_panel1_reset.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.camera_panel1_reset.Location = new System.Drawing.Point(284, 54);
            this.camera_panel1_reset.Name = "camera_panel1_reset";
            this.camera_panel1_reset.Size = new System.Drawing.Size(137, 37);
            this.camera_panel1_reset.TabIndex = 8;
            this.camera_panel1_reset.Text = "Reset Camera";
            this.camera_panel1_reset.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.camera_panel1_reset.UseVisualStyleBackColor = true;
            this.camera_panel1_reset.Click += new System.EventHandler(this.camera_panel1_reset_Click);
            // 
            // camera_panel1_down
            // 
            this.camera_panel1_down.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.camera_panel1_down.Image = ((System.Drawing.Image)(resources.GetObject("camera_panel1_down.Image")));
            this.camera_panel1_down.Location = new System.Drawing.Point(189, 96);
            this.camera_panel1_down.Name = "camera_panel1_down";
            this.camera_panel1_down.Size = new System.Drawing.Size(44, 37);
            this.camera_panel1_down.TabIndex = 5;
            this.toolTip1.SetToolTip(this.camera_panel1_down, "Rotate camera Down");
            this.camera_panel1_down.UseVisualStyleBackColor = true;
            this.camera_panel1_down.Click += new System.EventHandler(this.camera_panel1_down_Click);
            // 
            // camera_panel1_up
            // 
            this.camera_panel1_up.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.camera_panel1_up.Image = ((System.Drawing.Image)(resources.GetObject("camera_panel1_up.Image")));
            this.camera_panel1_up.Location = new System.Drawing.Point(189, 54);
            this.camera_panel1_up.Name = "camera_panel1_up";
            this.camera_panel1_up.Size = new System.Drawing.Size(44, 37);
            this.camera_panel1_up.TabIndex = 4;
            this.toolTip1.SetToolTip(this.camera_panel1_up, "Rotate camera Up");
            this.camera_panel1_up.UseVisualStyleBackColor = true;
            this.camera_panel1_up.Click += new System.EventHandler(this.camera_panel1_up_Click);
            // 
            // camera_panel1_right
            // 
            this.camera_panel1_right.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.camera_panel1_right.Image = ((System.Drawing.Image)(resources.GetObject("camera_panel1_right.Image")));
            this.camera_panel1_right.Location = new System.Drawing.Point(112, 96);
            this.camera_panel1_right.Name = "camera_panel1_right";
            this.camera_panel1_right.Size = new System.Drawing.Size(44, 37);
            this.camera_panel1_right.TabIndex = 7;
            this.toolTip1.SetToolTip(this.camera_panel1_right, "Rotate camera to the right");
            this.camera_panel1_right.UseVisualStyleBackColor = true;
            this.camera_panel1_right.Click += new System.EventHandler(this.camera_panel1_right_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "eye32.gif");
            this.imageList1.Images.SetKeyName(1, "colors32.gif");
            this.imageList1.Images.SetKeyName(2, "ruler32.gif");
            this.imageList1.Images.SetKeyName(3, "camera32.gif");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status,
            this.progress});
            this.statusStrip1.Location = new System.Drawing.Point(0, 592);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(891, 20);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "Idle";
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(25, 15);
            this.status.Text = "Idle";
            // 
            // progress
            // 
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(250, 14);
            this.progress.Visible = false;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Modelo 3D (*.m3d)|*.m3d";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Modelo 3D (*.m3d)|*.m3d";
            // 
            // memo1
            // 
            this.memo1.Location = new System.Drawing.Point(514, 3);
            this.memo1.Multiline = true;
            this.memo1.Name = "memo1";
            this.memo1.Size = new System.Drawing.Size(312, 144);
            this.memo1.TabIndex = 23;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 612);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Muni";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tab_view.ResumeLayout(false);
            this.tab_view.PerformLayout();
            this.tab_color.ResumeLayout(false);
            this.tab_color.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.color_trackB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.color_trackG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.color_trackR)).EndInit();
            this.tab_measure.ResumeLayout(false);
            this.tab_measure.PerformLayout();
            this.tab_camera.ResumeLayout(false);
            this.tab_camera.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_cratemodel;
        private Tao.Platform.Windows.SimpleOpenGlControl simpleOpenGlControl1;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton Create_Model;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_view;
        private System.Windows.Forms.TabPage tab_color;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox view_escz;
        private System.Windows.Forms.TextBox view_escy;
        private System.Windows.Forms.TextBox view_escx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button view_change;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox color_combo;
        private System.Windows.Forms.Button view_apply;
        private Tao.Platform.Windows.SimpleOpenGlControl simpleOpenGlControl2;
        private System.Windows.Forms.CheckBox view_length;
        private System.Windows.Forms.CheckBox view_neuron;
        private System.Windows.Forms.CheckBox view_container;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox view_axes;
        private System.Windows.Forms.RadioButton view_triangles;
        private System.Windows.Forms.RadioButton view_points;
        private System.Windows.Forms.RadioButton view_lines;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar color_trackR;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TrackBar color_trackB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TrackBar color_trackG;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolStripMenuItem menu_savemodel;
        private System.Windows.Forms.ToolStripButton Save_Model;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem menu_openmodel;
        private System.Windows.Forms.ToolStripButton Open_Model;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabPage tab_camera;
        private System.Windows.Forms.TabPage tab_measure;
        private System.Windows.Forms.Button camera_panel1_backward;
        private System.Windows.Forms.Button camera_panel1_foward;
        private System.Windows.Forms.Button camera_panel1_left;
        private System.Windows.Forms.Button camera_panel1_reset;
        private System.Windows.Forms.Button camera_panel1_down;
        private System.Windows.Forms.Button camera_panel1_up;
        private System.Windows.Forms.Button camera_panel1_right;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.ToolStripProgressBar progress;
        private System.Windows.Forms.Button view_resetrotation;
        private System.Windows.Forms.Button camera_resetrotation;
        private System.Windows.Forms.Button measure_back;
        private System.Windows.Forms.Button measuer_ahead;
        private System.Windows.Forms.Button measure_left;
        private System.Windows.Forms.Button measure_reset;
        private System.Windows.Forms.Button measure_down;
        private System.Windows.Forms.Button measure_up;
        private System.Windows.Forms.Button measure_right;
        private System.Windows.Forms.Button mesure_resetboth;
        private System.Windows.Forms.Label measure_result;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label measure_objetive;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label measure_microns;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label measure_pixels;
        private System.Windows.Forms.CheckBox measure_view_length;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton Reset_Camera;
        private System.Windows.Forms.ToolStripButton Reset_Model;
        private System.Windows.Forms.Button view_resetcamera;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_view_website;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menu_about;
        private System.Windows.Forms.TextBox memo1;


    }
}


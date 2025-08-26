
namespace Manual_Screen_Renderer
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnPickSky = new System.Windows.Forms.Button();
            this.btnPickRainbow = new System.Windows.Forms.Button();
            this.btnPickPipe = new System.Windows.Forms.Button();
            this.btnPickLight = new System.Windows.Forms.Button();
            this.btnPickLColor = new System.Windows.Forms.Button();
            this.btnPickIndex = new System.Windows.Forms.Button();
            this.btnPickEColor = new System.Windows.Forms.Button();
            this.btnDecompose = new System.Windows.Forms.Button();
            this.btnColorPicker = new System.Windows.Forms.Button();
            this.btnRendered = new System.Windows.Forms.Button();
            this.txtRendered = new System.Windows.Forms.TextBox();
            this.lblRendered = new System.Windows.Forms.Label();
            this.btnCompose = new System.Windows.Forms.Button();
            this.btnSky = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btnShading = new System.Windows.Forms.Button();
            this.lblDepth = new System.Windows.Forms.Label();
            this.btnRainbow = new System.Windows.Forms.Button();
            this.txtDepth = new System.Windows.Forms.TextBox();
            this.btnPipe = new System.Windows.Forms.Button();
            this.btnLight = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLColor = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnIndex = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnEColor = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnDepth = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSky = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtShading = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtRainbow = new System.Windows.Forms.TextBox();
            this.txtPipe = new System.Windows.Forms.TextBox();
            this.txtEColor = new System.Windows.Forms.TextBox();
            this.txtLight = new System.Windows.Forms.TextBox();
            this.txtIndex = new System.Windows.Forms.TextBox();
            this.txtLColor = new System.Windows.Forms.TextBox();
            this.pnlWorkspace = new Manual_Screen_Renderer.ScrollingPanel();
            this.pbxWorkspace = new Manual_Screen_Renderer.PictureBoxWithInterpolationMode();
            this.lblCursorCoords = new System.Windows.Forms.Label();
            this.btnEditSky = new System.Windows.Forms.Button();
            this.btnEditShading = new System.Windows.Forms.Button();
            this.btnEditRainbow = new System.Windows.Forms.Button();
            this.btnEditPipe = new System.Windows.Forms.Button();
            this.btnEditLight = new System.Windows.Forms.Button();
            this.btnEditLColor = new System.Windows.Forms.Button();
            this.btnEditIndex = new System.Windows.Forms.Button();
            this.btnEditEColor = new System.Windows.Forms.Button();
            this.btnEditDepth = new System.Windows.Forms.Button();
            this.lblMaxLayer = new System.Windows.Forms.Label();
            this.lblMinLayer = new System.Windows.Forms.Label();
            this.nudMaxLayer = new System.Windows.Forms.NumericUpDown();
            this.nudMinLayer = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveACopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.lblMessages = new System.Windows.Forms.Label();
            this.nudDepth = new System.Windows.Forms.NumericUpDown();
            this.btnPickShading = new System.Windows.Forms.Button();
            this.btnShowRendered = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlWorkspace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxWorkspace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxLayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinLayer)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDepth)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.nudDepth);
            this.splitContainer1.Panel1.Controls.Add(this.btnPickSky);
            this.splitContainer1.Panel1.Controls.Add(this.btnPickShading);
            this.splitContainer1.Panel1.Controls.Add(this.btnPickRainbow);
            this.splitContainer1.Panel1.Controls.Add(this.btnPickPipe);
            this.splitContainer1.Panel1.Controls.Add(this.btnPickLight);
            this.splitContainer1.Panel1.Controls.Add(this.btnPickLColor);
            this.splitContainer1.Panel1.Controls.Add(this.btnPickIndex);
            this.splitContainer1.Panel1.Controls.Add(this.btnPickEColor);
            this.splitContainer1.Panel1.Controls.Add(this.btnDecompose);
            this.splitContainer1.Panel1.Controls.Add(this.btnColorPicker);
            this.splitContainer1.Panel1.Controls.Add(this.btnRendered);
            this.splitContainer1.Panel1.Controls.Add(this.txtRendered);
            this.splitContainer1.Panel1.Controls.Add(this.lblRendered);
            this.splitContainer1.Panel1.Controls.Add(this.btnCompose);
            this.splitContainer1.Panel1.Controls.Add(this.btnSky);
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.btnShading);
            this.splitContainer1.Panel1.Controls.Add(this.lblDepth);
            this.splitContainer1.Panel1.Controls.Add(this.btnRainbow);
            this.splitContainer1.Panel1.Controls.Add(this.txtDepth);
            this.splitContainer1.Panel1.Controls.Add(this.btnPipe);
            this.splitContainer1.Panel1.Controls.Add(this.btnLight);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.btnLColor);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.btnIndex);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.btnEColor);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.btnDepth);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.txtSky);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.txtShading);
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            this.splitContainer1.Panel1.Controls.Add(this.txtRainbow);
            this.splitContainer1.Panel1.Controls.Add(this.txtPipe);
            this.splitContainer1.Panel1.Controls.Add(this.txtEColor);
            this.splitContainer1.Panel1.Controls.Add(this.txtLight);
            this.splitContainer1.Panel1.Controls.Add(this.txtIndex);
            this.splitContainer1.Panel1.Controls.Add(this.txtLColor);
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            this.splitContainer1.Panel2.Controls.Add(this.btnShowRendered);
            this.splitContainer1.Panel2.Controls.Add(this.pnlWorkspace);
            this.splitContainer1.Panel2.Controls.Add(this.lblCursorCoords);
            this.splitContainer1.Panel2.Controls.Add(this.btnEditSky);
            this.splitContainer1.Panel2.Controls.Add(this.btnEditShading);
            this.splitContainer1.Panel2.Controls.Add(this.btnEditRainbow);
            this.splitContainer1.Panel2.Controls.Add(this.btnEditPipe);
            this.splitContainer1.Panel2.Controls.Add(this.btnEditLight);
            this.splitContainer1.Panel2.Controls.Add(this.btnEditLColor);
            this.splitContainer1.Panel2.Controls.Add(this.btnEditIndex);
            this.splitContainer1.Panel2.Controls.Add(this.btnEditEColor);
            this.splitContainer1.Panel2.Controls.Add(this.btnEditDepth);
            this.splitContainer1.Panel2.Controls.Add(this.lblMaxLayer);
            this.splitContainer1.Panel2.Controls.Add(this.lblMinLayer);
            this.splitContainer1.Panel2.Controls.Add(this.nudMaxLayer);
            this.splitContainer1.Panel2.Controls.Add(this.nudMinLayer);
            this.splitContainer1.TabStop = false;
            // 
            // btnPickSky
            // 
            this.btnPickSky.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.btnPickSky, "btnPickSky");
            this.btnPickSky.Name = "btnPickSky";
            this.btnPickSky.TabStop = false;
            this.btnPickSky.UseVisualStyleBackColor = false;
            this.btnPickSky.Click += new System.EventHandler(this.btnPickSky_Click);
            // 
            // btnPickRainbow
            // 
            this.btnPickRainbow.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.btnPickRainbow, "btnPickRainbow");
            this.btnPickRainbow.Name = "btnPickRainbow";
            this.btnPickRainbow.TabStop = false;
            this.btnPickRainbow.UseVisualStyleBackColor = false;
            this.btnPickRainbow.Click += new System.EventHandler(this.btnPickRainbow_Click);
            // 
            // btnPickPipe
            // 
            this.btnPickPipe.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.btnPickPipe, "btnPickPipe");
            this.btnPickPipe.Name = "btnPickPipe";
            this.btnPickPipe.TabStop = false;
            this.btnPickPipe.UseVisualStyleBackColor = false;
            this.btnPickPipe.Click += new System.EventHandler(this.btnPickPipe_Click);
            // 
            // btnPickLight
            // 
            this.btnPickLight.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.btnPickLight, "btnPickLight");
            this.btnPickLight.Name = "btnPickLight";
            this.btnPickLight.TabStop = false;
            this.btnPickLight.UseVisualStyleBackColor = false;
            this.btnPickLight.Click += new System.EventHandler(this.btnPickLight_Click);
            // 
            // btnPickLColor
            // 
            this.btnPickLColor.BackColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.btnPickLColor, "btnPickLColor");
            this.btnPickLColor.Name = "btnPickLColor";
            this.btnPickLColor.TabStop = false;
            this.btnPickLColor.UseVisualStyleBackColor = false;
            this.btnPickLColor.Click += new System.EventHandler(this.btnPickLColor_Click);
            // 
            // btnPickIndex
            // 
            this.btnPickIndex.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.btnPickIndex, "btnPickIndex");
            this.btnPickIndex.Name = "btnPickIndex";
            this.btnPickIndex.TabStop = false;
            this.btnPickIndex.UseVisualStyleBackColor = false;
            this.btnPickIndex.Click += new System.EventHandler(this.btnPickIndex_Click);
            // 
            // btnPickEColor
            // 
            this.btnPickEColor.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.btnPickEColor, "btnPickEColor");
            this.btnPickEColor.Name = "btnPickEColor";
            this.btnPickEColor.TabStop = false;
            this.btnPickEColor.UseVisualStyleBackColor = false;
            this.btnPickEColor.Click += new System.EventHandler(this.btnPickEColor_Click);
            // 
            // btnDecompose
            // 
            resources.ApplyResources(this.btnDecompose, "btnDecompose");
            this.btnDecompose.Name = "btnDecompose";
            this.btnDecompose.TabStop = false;
            this.toolTip.SetToolTip(this.btnDecompose, resources.GetString("btnDecompose.ToolTip"));
            this.btnDecompose.UseVisualStyleBackColor = true;
            this.btnDecompose.Click += new System.EventHandler(this.btnDecompose_Click);
            // 
            // btnColorPicker
            // 
            this.btnColorPicker.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnColorPicker, "btnColorPicker");
            this.btnColorPicker.Name = "btnColorPicker";
            this.btnColorPicker.TabStop = false;
            this.btnColorPicker.UseVisualStyleBackColor = false;
            this.btnColorPicker.Click += new System.EventHandler(this.btnColorPicker_Click);
            // 
            // btnRendered
            // 
            resources.ApplyResources(this.btnRendered, "btnRendered");
            this.btnRendered.Name = "btnRendered";
            this.btnRendered.TabStop = false;
            this.btnRendered.UseVisualStyleBackColor = false;
            this.btnRendered.Click += new System.EventHandler(this.btnRendered_Click);
            // 
            // txtRendered
            // 
            this.txtRendered.AllowDrop = true;
            resources.ApplyResources(this.txtRendered, "txtRendered");
            this.txtRendered.Name = "txtRendered";
            this.txtRendered.TabStop = false;
            // 
            // lblRendered
            // 
            resources.ApplyResources(this.lblRendered, "lblRendered");
            this.lblRendered.Name = "lblRendered";
            // 
            // btnCompose
            // 
            resources.ApplyResources(this.btnCompose, "btnCompose");
            this.btnCompose.Name = "btnCompose";
            this.btnCompose.TabStop = false;
            this.toolTip.SetToolTip(this.btnCompose, resources.GetString("btnCompose.ToolTip"));
            this.btnCompose.UseVisualStyleBackColor = true;
            this.btnCompose.Click += new System.EventHandler(this.btnCompose_Click);
            // 
            // btnSky
            // 
            resources.ApplyResources(this.btnSky, "btnSky");
            this.btnSky.Name = "btnSky";
            this.btnSky.TabStop = false;
            this.btnSky.UseVisualStyleBackColor = false;
            this.btnSky.Click += new System.EventHandler(this.btnSky_Click);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // btnShading
            // 
            resources.ApplyResources(this.btnShading, "btnShading");
            this.btnShading.Name = "btnShading";
            this.btnShading.TabStop = false;
            this.btnShading.UseVisualStyleBackColor = false;
            this.btnShading.Click += new System.EventHandler(this.btnShading_Click);
            // 
            // lblDepth
            // 
            resources.ApplyResources(this.lblDepth, "lblDepth");
            this.lblDepth.Name = "lblDepth";
            // 
            // btnRainbow
            // 
            resources.ApplyResources(this.btnRainbow, "btnRainbow");
            this.btnRainbow.Name = "btnRainbow";
            this.btnRainbow.TabStop = false;
            this.btnRainbow.UseVisualStyleBackColor = false;
            this.btnRainbow.Click += new System.EventHandler(this.btnRainbow_Click);
            // 
            // txtDepth
            // 
            this.txtDepth.AllowDrop = true;
            resources.ApplyResources(this.txtDepth, "txtDepth");
            this.txtDepth.Name = "txtDepth";
            this.txtDepth.TabStop = false;
            // 
            // btnPipe
            // 
            resources.ApplyResources(this.btnPipe, "btnPipe");
            this.btnPipe.Name = "btnPipe";
            this.btnPipe.TabStop = false;
            this.btnPipe.UseVisualStyleBackColor = false;
            this.btnPipe.Click += new System.EventHandler(this.btnPipe_Click);
            // 
            // btnLight
            // 
            resources.ApplyResources(this.btnLight, "btnLight");
            this.btnLight.Name = "btnLight";
            this.btnLight.TabStop = false;
            this.btnLight.UseVisualStyleBackColor = false;
            this.btnLight.Click += new System.EventHandler(this.btnLight_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnLColor
            // 
            resources.ApplyResources(this.btnLColor, "btnLColor");
            this.btnLColor.Name = "btnLColor";
            this.btnLColor.TabStop = false;
            this.btnLColor.UseVisualStyleBackColor = false;
            this.btnLColor.Click += new System.EventHandler(this.btnLColor_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // btnIndex
            // 
            resources.ApplyResources(this.btnIndex, "btnIndex");
            this.btnIndex.Name = "btnIndex";
            this.btnIndex.TabStop = false;
            this.btnIndex.UseVisualStyleBackColor = false;
            this.btnIndex.Click += new System.EventHandler(this.btnIndex_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // btnEColor
            // 
            resources.ApplyResources(this.btnEColor, "btnEColor");
            this.btnEColor.Name = "btnEColor";
            this.btnEColor.TabStop = false;
            this.btnEColor.UseVisualStyleBackColor = false;
            this.btnEColor.Click += new System.EventHandler(this.btnEColor_Click);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // btnDepth
            // 
            resources.ApplyResources(this.btnDepth, "btnDepth");
            this.btnDepth.Name = "btnDepth";
            this.btnDepth.TabStop = false;
            this.btnDepth.UseVisualStyleBackColor = false;
            this.btnDepth.Click += new System.EventHandler(this.btnDepth_Click);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // txtSky
            // 
            this.txtSky.AllowDrop = true;
            resources.ApplyResources(this.txtSky, "txtSky");
            this.txtSky.Name = "txtSky";
            this.txtSky.TabStop = false;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txtShading
            // 
            this.txtShading.AllowDrop = true;
            resources.ApplyResources(this.txtShading, "txtShading");
            this.txtShading.Name = "txtShading";
            this.txtShading.TabStop = false;
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // txtRainbow
            // 
            this.txtRainbow.AllowDrop = true;
            resources.ApplyResources(this.txtRainbow, "txtRainbow");
            this.txtRainbow.Name = "txtRainbow";
            this.txtRainbow.TabStop = false;
            // 
            // txtPipe
            // 
            this.txtPipe.AllowDrop = true;
            resources.ApplyResources(this.txtPipe, "txtPipe");
            this.txtPipe.Name = "txtPipe";
            this.txtPipe.TabStop = false;
            // 
            // txtEColor
            // 
            this.txtEColor.AllowDrop = true;
            resources.ApplyResources(this.txtEColor, "txtEColor");
            this.txtEColor.Name = "txtEColor";
            this.txtEColor.TabStop = false;
            this.txtEColor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEColor_KeyPress);
            // 
            // txtLight
            // 
            this.txtLight.AllowDrop = true;
            resources.ApplyResources(this.txtLight, "txtLight");
            this.txtLight.Name = "txtLight";
            this.txtLight.TabStop = false;
            // 
            // txtIndex
            // 
            this.txtIndex.AllowDrop = true;
            resources.ApplyResources(this.txtIndex, "txtIndex");
            this.txtIndex.Name = "txtIndex";
            this.txtIndex.TabStop = false;
            // 
            // txtLColor
            // 
            this.txtLColor.AllowDrop = true;
            resources.ApplyResources(this.txtLColor, "txtLColor");
            this.txtLColor.Name = "txtLColor";
            this.txtLColor.TabStop = false;
            // 
            // pnlWorkspace
            // 
            this.pnlWorkspace.Controls.Add(this.pbxWorkspace);
            resources.ApplyResources(this.pnlWorkspace, "pnlWorkspace");
            this.pnlWorkspace.Name = "pnlWorkspace";
            // 
            // pbxWorkspace
            // 
            this.pbxWorkspace.BackColor = System.Drawing.SystemColors.Control;
            this.pbxWorkspace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxWorkspace.Cursor = System.Windows.Forms.Cursors.Cross;
            resources.ApplyResources(this.pbxWorkspace, "pbxWorkspace");
            this.pbxWorkspace.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.pbxWorkspace.Name = "pbxWorkspace";
            this.pbxWorkspace.TabStop = false;
            this.pbxWorkspace.Click += new System.EventHandler(this.pbxWorkspace_Click);
            this.pbxWorkspace.MouseEnter += new System.EventHandler(this.pbxWorkspace_MouseEnter);
            this.pbxWorkspace.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbxWorkspace_MouseMove);
            // 
            // lblCursorCoords
            // 
            resources.ApplyResources(this.lblCursorCoords, "lblCursorCoords");
            this.lblCursorCoords.Name = "lblCursorCoords";
            // 
            // btnEditSky
            // 
            resources.ApplyResources(this.btnEditSky, "btnEditSky");
            this.btnEditSky.Name = "btnEditSky";
            this.toolTip.SetToolTip(this.btnEditSky, resources.GetString("btnEditSky.ToolTip"));
            this.btnEditSky.UseVisualStyleBackColor = true;
            this.btnEditSky.Click += new System.EventHandler(this.btnEditSky_Click);
            // 
            // btnEditShading
            // 
            resources.ApplyResources(this.btnEditShading, "btnEditShading");
            this.btnEditShading.Name = "btnEditShading";
            this.toolTip.SetToolTip(this.btnEditShading, resources.GetString("btnEditShading.ToolTip"));
            this.btnEditShading.UseVisualStyleBackColor = true;
            this.btnEditShading.Click += new System.EventHandler(this.btnEditShading_Click);
            // 
            // btnEditRainbow
            // 
            resources.ApplyResources(this.btnEditRainbow, "btnEditRainbow");
            this.btnEditRainbow.Name = "btnEditRainbow";
            this.toolTip.SetToolTip(this.btnEditRainbow, resources.GetString("btnEditRainbow.ToolTip"));
            this.btnEditRainbow.UseVisualStyleBackColor = true;
            this.btnEditRainbow.Click += new System.EventHandler(this.btnEditRainbow_Click);
            // 
            // btnEditPipe
            // 
            resources.ApplyResources(this.btnEditPipe, "btnEditPipe");
            this.btnEditPipe.Name = "btnEditPipe";
            this.toolTip.SetToolTip(this.btnEditPipe, resources.GetString("btnEditPipe.ToolTip"));
            this.btnEditPipe.UseVisualStyleBackColor = true;
            this.btnEditPipe.Click += new System.EventHandler(this.btnEditPipe_Click);
            // 
            // btnEditLight
            // 
            resources.ApplyResources(this.btnEditLight, "btnEditLight");
            this.btnEditLight.Name = "btnEditLight";
            this.toolTip.SetToolTip(this.btnEditLight, resources.GetString("btnEditLight.ToolTip"));
            this.btnEditLight.UseVisualStyleBackColor = true;
            this.btnEditLight.Click += new System.EventHandler(this.btnEditLight_Click);
            // 
            // btnEditLColor
            // 
            resources.ApplyResources(this.btnEditLColor, "btnEditLColor");
            this.btnEditLColor.Name = "btnEditLColor";
            this.toolTip.SetToolTip(this.btnEditLColor, resources.GetString("btnEditLColor.ToolTip"));
            this.btnEditLColor.UseVisualStyleBackColor = true;
            this.btnEditLColor.Click += new System.EventHandler(this.btnEditLColor_Click);
            // 
            // btnEditIndex
            // 
            resources.ApplyResources(this.btnEditIndex, "btnEditIndex");
            this.btnEditIndex.Name = "btnEditIndex";
            this.toolTip.SetToolTip(this.btnEditIndex, resources.GetString("btnEditIndex.ToolTip"));
            this.btnEditIndex.UseVisualStyleBackColor = true;
            this.btnEditIndex.Click += new System.EventHandler(this.btnEditIndex_Click);
            // 
            // btnEditEColor
            // 
            resources.ApplyResources(this.btnEditEColor, "btnEditEColor");
            this.btnEditEColor.Name = "btnEditEColor";
            this.toolTip.SetToolTip(this.btnEditEColor, resources.GetString("btnEditEColor.ToolTip"));
            this.btnEditEColor.UseVisualStyleBackColor = true;
            this.btnEditEColor.Click += new System.EventHandler(this.btnEditEColor_Click);
            // 
            // btnEditDepth
            // 
            resources.ApplyResources(this.btnEditDepth, "btnEditDepth");
            this.btnEditDepth.Name = "btnEditDepth";
            this.toolTip.SetToolTip(this.btnEditDepth, resources.GetString("btnEditDepth.ToolTip"));
            this.btnEditDepth.UseVisualStyleBackColor = true;
            this.btnEditDepth.Click += new System.EventHandler(this.btnEditDepth_Click);
            // 
            // lblMaxLayer
            // 
            resources.ApplyResources(this.lblMaxLayer, "lblMaxLayer");
            this.lblMaxLayer.Name = "lblMaxLayer";
            // 
            // lblMinLayer
            // 
            resources.ApplyResources(this.lblMinLayer, "lblMinLayer");
            this.lblMinLayer.Name = "lblMinLayer";
            // 
            // nudMaxLayer
            // 
            resources.ApplyResources(this.nudMaxLayer, "nudMaxLayer");
            this.nudMaxLayer.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudMaxLayer.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMaxLayer.Name = "nudMaxLayer";
            this.nudMaxLayer.TabStop = false;
            this.nudMaxLayer.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // nudMinLayer
            // 
            resources.ApplyResources(this.nudMinLayer, "nudMinLayer");
            this.nudMinLayer.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudMinLayer.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMinLayer.Name = "nudMinLayer";
            this.nudMinLayer.TabStop = false;
            this.nudMinLayer.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.saveACopyToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            resources.ApplyResources(this.newToolStripMenuItem, "newToolStripMenuItem");
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            resources.ApplyResources(this.saveAsToolStripMenuItem, "saveAsToolStripMenuItem");
            // 
            // saveACopyToolStripMenuItem
            // 
            this.saveACopyToolStripMenuItem.Name = "saveACopyToolStripMenuItem";
            resources.ApplyResources(this.saveACopyToolStripMenuItem, "saveACopyToolStripMenuItem");
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            resources.ApplyResources(this.viewToolStripMenuItem, "viewToolStripMenuItem");
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            this.colorDialog1.FullOpen = true;
            this.colorDialog1.SolidColorOnly = true;
            // 
            // lblMessages
            // 
            resources.ApplyResources(this.lblMessages, "lblMessages");
            this.lblMessages.Name = "lblMessages";
            // 
            // nudDepth
            // 
            resources.ApplyResources(this.nudDepth, "nudDepth");
            this.nudDepth.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudDepth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDepth.Name = "nudDepth";
            this.nudDepth.TabStop = false;
            this.nudDepth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnPickShading
            // 
            this.btnPickShading.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.btnPickShading, "btnPickShading");
            this.btnPickShading.Name = "btnPickShading";
            this.btnPickShading.TabStop = false;
            this.btnPickShading.UseVisualStyleBackColor = false;
            this.btnPickShading.Click += new System.EventHandler(this.btnPickShading_Click);
            // 
            // btnShowRendered
            // 
            resources.ApplyResources(this.btnShowRendered, "btnShowRendered");
            this.btnShowRendered.Name = "btnShowRendered";
            this.toolTip.SetToolTip(this.btnShowRendered, resources.GetString("btnShowRendered.ToolTip"));
            this.btnShowRendered.UseVisualStyleBackColor = true;
            this.btnShowRendered.Click += new System.EventHandler(this.btnShowRendered_Click);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblMessages);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.splitContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlWorkspace.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxWorkspace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxLayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinLayer)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDepth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblDepth;
        private System.Windows.Forms.Button btnCompose;
        private System.Windows.Forms.TextBox txtDepth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtEColor;
        private System.Windows.Forms.TextBox txtIndex;
        private System.Windows.Forms.TextBox txtLColor;
        private System.Windows.Forms.TextBox txtLight;
        private System.Windows.Forms.TextBox txtPipe;
        private System.Windows.Forms.TextBox txtRainbow;
        private System.Windows.Forms.TextBox txtShading;
        private System.Windows.Forms.TextBox txtSky;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnDepth;
        private System.Windows.Forms.Button btnEColor;
        private System.Windows.Forms.Button btnIndex;
        private System.Windows.Forms.Button btnLColor;
        private System.Windows.Forms.Button btnLight;
        private System.Windows.Forms.Button btnPipe;
        private System.Windows.Forms.Button btnRainbow;
        private System.Windows.Forms.Button btnShading;
        private System.Windows.Forms.Button btnSky;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnRendered;
        private System.Windows.Forms.TextBox txtRendered;
        private System.Windows.Forms.Label lblRendered;
        private Manual_Screen_Renderer.PictureBoxWithInterpolationMode pbxWorkspace;
        private PictureBoxWithInterpolationMode pbxWork;
        private System.Windows.Forms.NumericUpDown nudMaxLayer;
        private System.Windows.Forms.NumericUpDown nudMinLayer;
        private System.Windows.Forms.Label lblMinLayer;
        private System.Windows.Forms.Label lblMaxLayer;
        private System.Windows.Forms.Button btnEditSky;
        private System.Windows.Forms.Button btnEditShading;
        private System.Windows.Forms.Button btnEditRainbow;
        private System.Windows.Forms.Button btnEditPipe;
        private System.Windows.Forms.Button btnEditLight;
        private System.Windows.Forms.Button btnEditLColor;
        private System.Windows.Forms.Button btnEditIndex;
        private System.Windows.Forms.Button btnEditEColor;
        private System.Windows.Forms.Button btnEditDepth;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label lblCursorCoords;
        private ScrollingPanel pnlWorkspace;
        private System.Windows.Forms.Button btnColorPicker;
        private System.Windows.Forms.Button btnDecompose;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveACopyToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label lblMessages;
        private System.Windows.Forms.Button btnPickSky;
        private System.Windows.Forms.Button btnPickRainbow;
        private System.Windows.Forms.Button btnPickPipe;
        private System.Windows.Forms.Button btnPickLight;
        private System.Windows.Forms.Button btnPickLColor;
        private System.Windows.Forms.Button btnPickIndex;
        private System.Windows.Forms.Button btnPickEColor;
        private System.Windows.Forms.NumericUpDown nudDepth;
        private System.Windows.Forms.Button btnPickShading;
        private System.Windows.Forms.Button btnShowRendered;
    }
}


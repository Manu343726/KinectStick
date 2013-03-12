namespace InstructionEditor
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
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.deleteInstructionButton = new System.Windows.Forms.Button();
            this.newInstructionButton = new System.Windows.Forms.Button();
            this.profileList = new System.Windows.Forms.ListView();
            this.instructionHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.commandTypeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valueTypeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valueHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.shiftCheckBox = new System.Windows.Forms.CheckBox();
            this.controlCheckBox = new System.Windows.Forms.CheckBox();
            this.joystickValuePercentLabel = new System.Windows.Forms.Label();
            this.joystickValueLabel = new System.Windows.Forms.Label();
            this.joystickValue = new System.Windows.Forms.TrackBar();
            this.valueTypeCombo = new System.Windows.Forms.ComboBox();
            this.valueTypeLabel = new System.Windows.Forms.Label();
            this.commandTypeCombo = new System.Windows.Forms.ComboBox();
            this.instructionText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoPerfilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cargarPerfilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.guardarPrefilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GuardarComoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.perfilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compilarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.open = new System.Windows.Forms.OpenFileDialog();
            this.save = new System.Windows.Forms.SaveFileDialog();
            this.notify = new System.Windows.Forms.NotifyIcon(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.joystickValue)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.deleteInstructionButton);
            this.groupBox1.Controls.Add(this.newInstructionButton);
            this.groupBox1.Controls.Add(this.profileList);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(686, 434);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Prefil 1 (Edición)";
            // 
            // deleteInstructionButton
            // 
            this.deleteInstructionButton.Location = new System.Drawing.Point(476, 46);
            this.deleteInstructionButton.Name = "deleteInstructionButton";
            this.deleteInstructionButton.Size = new System.Drawing.Size(125, 21);
            this.deleteInstructionButton.TabIndex = 6;
            this.deleteInstructionButton.Text = "Eliminar instrucción";
            this.deleteInstructionButton.UseVisualStyleBackColor = true;
            this.deleteInstructionButton.Click += new System.EventHandler(this.deleteInstructionButton_Click);
            // 
            // newInstructionButton
            // 
            this.newInstructionButton.Location = new System.Drawing.Point(476, 19);
            this.newInstructionButton.Name = "newInstructionButton";
            this.newInstructionButton.Size = new System.Drawing.Size(125, 21);
            this.newInstructionButton.TabIndex = 5;
            this.newInstructionButton.Text = "Nueva instrucción...";
            this.newInstructionButton.UseVisualStyleBackColor = true;
            this.newInstructionButton.Click += new System.EventHandler(this.newInstructionButton_Click);
            // 
            // profileList
            // 
            this.profileList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.profileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.instructionHeader,
            this.commandTypeHeader,
            this.valueTypeHeader,
            this.valueHeader});
            this.profileList.FullRowSelect = true;
            this.profileList.GridLines = true;
            this.profileList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.profileList.HideSelection = false;
            this.profileList.Location = new System.Drawing.Point(6, 19);
            this.profileList.Name = "profileList";
            this.profileList.Size = new System.Drawing.Size(464, 290);
            this.profileList.TabIndex = 4;
            this.profileList.UseCompatibleStateImageBehavior = false;
            this.profileList.View = System.Windows.Forms.View.Details;
            this.profileList.SelectedIndexChanged += new System.EventHandler(this.profileList_SelectedIndexChanged);
            // 
            // instructionHeader
            // 
            this.instructionHeader.Text = "Instrucción";
            this.instructionHeader.Width = 144;
            // 
            // commandTypeHeader
            // 
            this.commandTypeHeader.Text = "Tipo de instrucción";
            this.commandTypeHeader.Width = 105;
            // 
            // valueTypeHeader
            // 
            this.valueTypeHeader.Text = "Eje/Botón/Tecla";
            this.valueTypeHeader.Width = 97;
            // 
            // valueHeader
            // 
            this.valueHeader.Text = "Valor/Modificadores";
            this.valueHeader.Width = 112;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.shiftCheckBox);
            this.groupBox2.Controls.Add(this.controlCheckBox);
            this.groupBox2.Controls.Add(this.joystickValuePercentLabel);
            this.groupBox2.Controls.Add(this.joystickValueLabel);
            this.groupBox2.Controls.Add(this.joystickValue);
            this.groupBox2.Controls.Add(this.valueTypeCombo);
            this.groupBox2.Controls.Add(this.valueTypeLabel);
            this.groupBox2.Controls.Add(this.commandTypeCombo);
            this.groupBox2.Controls.Add(this.instructionText);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(5, 315);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(675, 116);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // shiftCheckBox
            // 
            this.shiftCheckBox.AutoSize = true;
            this.shiftCheckBox.Location = new System.Drawing.Point(298, 85);
            this.shiftCheckBox.Name = "shiftCheckBox";
            this.shiftCheckBox.Size = new System.Drawing.Size(47, 17);
            this.shiftCheckBox.TabIndex = 15;
            this.shiftCheckBox.Text = "Shift";
            this.shiftCheckBox.UseVisualStyleBackColor = true;
            this.shiftCheckBox.CheckedChanged += new System.EventHandler(this.shiftCheckBox_CheckedChanged);
            // 
            // controlCheckBox
            // 
            this.controlCheckBox.AutoSize = true;
            this.controlCheckBox.Location = new System.Drawing.Point(298, 63);
            this.controlCheckBox.Name = "controlCheckBox";
            this.controlCheckBox.Size = new System.Drawing.Size(59, 17);
            this.controlCheckBox.TabIndex = 14;
            this.controlCheckBox.Text = "Control";
            this.controlCheckBox.UseVisualStyleBackColor = true;
            this.controlCheckBox.CheckedChanged += new System.EventHandler(this.controlCheckBox_CheckedChanged);
            // 
            // joystickValuePercentLabel
            // 
            this.joystickValuePercentLabel.Location = new System.Drawing.Point(432, 86);
            this.joystickValuePercentLabel.Name = "joystickValuePercentLabel";
            this.joystickValuePercentLabel.Size = new System.Drawing.Size(237, 18);
            this.joystickValuePercentLabel.TabIndex = 13;
            this.joystickValuePercentLabel.Text = "-100%                            0%                       +100%";
            // 
            // joystickValueLabel
            // 
            this.joystickValueLabel.Location = new System.Drawing.Point(432, 17);
            this.joystickValueLabel.Name = "joystickValueLabel";
            this.joystickValueLabel.Size = new System.Drawing.Size(136, 18);
            this.joystickValueLabel.TabIndex = 11;
            this.joystickValueLabel.Text = "Valor (Eje):";
            // 
            // joystickValue
            // 
            this.joystickValue.Location = new System.Drawing.Point(431, 38);
            this.joystickValue.Maximum = 100;
            this.joystickValue.Minimum = -100;
            this.joystickValue.Name = "joystickValue";
            this.joystickValue.Size = new System.Drawing.Size(238, 45);
            this.joystickValue.TabIndex = 12;
            this.joystickValue.TickFrequency = 10;
            this.joystickValue.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.joystickValue.Scroll += new System.EventHandler(this.joystickValue_Scroll);
            // 
            // valueTypeCombo
            // 
            this.valueTypeCombo.FormattingEnabled = true;
            this.valueTypeCombo.Location = new System.Drawing.Point(293, 37);
            this.valueTypeCombo.Name = "valueTypeCombo";
            this.valueTypeCombo.Size = new System.Drawing.Size(132, 21);
            this.valueTypeCombo.TabIndex = 11;
            this.valueTypeCombo.SelectedIndexChanged += new System.EventHandler(this.valueTypeCombo_SelectedIndexChanged);
            // 
            // valueTypeLabel
            // 
            this.valueTypeLabel.Location = new System.Drawing.Point(290, 16);
            this.valueTypeLabel.Name = "valueTypeLabel";
            this.valueTypeLabel.Size = new System.Drawing.Size(136, 18);
            this.valueTypeLabel.TabIndex = 10;
            this.valueTypeLabel.Text = "Tecla/Botón/Eje:";
            // 
            // commandTypeCombo
            // 
            this.commandTypeCombo.FormattingEnabled = true;
            this.commandTypeCombo.Location = new System.Drawing.Point(151, 37);
            this.commandTypeCombo.Name = "commandTypeCombo";
            this.commandTypeCombo.Size = new System.Drawing.Size(132, 21);
            this.commandTypeCombo.TabIndex = 9;
            this.commandTypeCombo.SelectedIndexChanged += new System.EventHandler(this.commandTypeCombo_SelectedIndexChanged);
            // 
            // instructionText
            // 
            this.instructionText.Location = new System.Drawing.Point(9, 37);
            this.instructionText.Name = "instructionText";
            this.instructionText.Size = new System.Drawing.Size(129, 20);
            this.instructionText.TabIndex = 8;
            this.instructionText.TextChanged += new System.EventHandler(this.instructionText_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(148, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "Tipo de comando:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "Instrucción:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 460);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(702, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(32, 17);
            this.statusLabel.Text = "Listo";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.perfilToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(702, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoPerfilToolStripMenuItem,
            this.cargarPerfilToolStripMenuItem,
            this.toolStripSeparator1,
            this.guardarPrefilToolStripMenuItem,
            this.GuardarComoToolStripMenuItem1,
            this.toolStripSeparator2,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // nuevoPerfilToolStripMenuItem
            // 
            this.nuevoPerfilToolStripMenuItem.Name = "nuevoPerfilToolStripMenuItem";
            this.nuevoPerfilToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.nuevoPerfilToolStripMenuItem.Text = "Nuevo perfil";
            this.nuevoPerfilToolStripMenuItem.Click += new System.EventHandler(this.nuevoPerfilToolStripMenuItem_Click);
            // 
            // cargarPerfilToolStripMenuItem
            // 
            this.cargarPerfilToolStripMenuItem.Name = "cargarPerfilToolStripMenuItem";
            this.cargarPerfilToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.cargarPerfilToolStripMenuItem.Text = "Cargar perfil";
            this.cargarPerfilToolStripMenuItem.Click += new System.EventHandler(this.cargarPerfilToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(156, 6);
            // 
            // guardarPrefilToolStripMenuItem
            // 
            this.guardarPrefilToolStripMenuItem.Name = "guardarPrefilToolStripMenuItem";
            this.guardarPrefilToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.guardarPrefilToolStripMenuItem.Text = "Guardar perfil";
            this.guardarPrefilToolStripMenuItem.Click += new System.EventHandler(this.guardarPrefilToolStripMenuItem_Click);
            // 
            // GuardarComoToolStripMenuItem1
            // 
            this.GuardarComoToolStripMenuItem1.Name = "GuardarComoToolStripMenuItem1";
            this.GuardarComoToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.GuardarComoToolStripMenuItem1.Text = "Guardar como...";
            this.GuardarComoToolStripMenuItem1.Click += new System.EventHandler(this.GuardarComoToolStripMenuItem1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(156, 6);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // perfilToolStripMenuItem
            // 
            this.perfilToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compilarToolStripMenuItem});
            this.perfilToolStripMenuItem.Name = "perfilToolStripMenuItem";
            this.perfilToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.perfilToolStripMenuItem.Text = "Perfil";
            // 
            // compilarToolStripMenuItem
            // 
            this.compilarToolStripMenuItem.Name = "compilarToolStripMenuItem";
            this.compilarToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.compilarToolStripMenuItem.Text = "Compilar...";
            this.compilarToolStripMenuItem.Click += new System.EventHandler(this.compilarToolStripMenuItem_Click);
            // 
            // open
            // 
            this.open.Filter = "Perfil de KinectStick|*.kpf";
            // 
            // save
            // 
            this.save.Filter = "Perfil de KinectStick|*.kpf";
            // 
            // notify
            // 
            this.notify.Icon = ((System.Drawing.Icon)(resources.GetObject("notify.Icon")));
            this.notify.Text = "KinectStick";
            this.notify.Visible = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 482);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(718, 520);
            this.Name = "Form1";
            this.Text = "KinectStick - Editor de perfiles";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.joystickValue)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button deleteInstructionButton;
        private System.Windows.Forms.Button newInstructionButton;
        private System.Windows.Forms.ListView profileList;
        private System.Windows.Forms.ColumnHeader instructionHeader;
        private System.Windows.Forms.ColumnHeader commandTypeHeader;
        private System.Windows.Forms.ColumnHeader valueTypeHeader;
        private System.Windows.Forms.ColumnHeader valueHeader;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox shiftCheckBox;
        private System.Windows.Forms.CheckBox controlCheckBox;
        private System.Windows.Forms.Label joystickValuePercentLabel;
        private System.Windows.Forms.Label joystickValueLabel;
        private System.Windows.Forms.TrackBar joystickValue;
        private System.Windows.Forms.ComboBox valueTypeCombo;
        private System.Windows.Forms.Label valueTypeLabel;
        private System.Windows.Forms.ComboBox commandTypeCombo;
        private System.Windows.Forms.TextBox instructionText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevoPerfilToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cargarPerfilToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarPrefilToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem perfilToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compilarToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog open;
        private System.Windows.Forms.SaveFileDialog save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem GuardarComoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.NotifyIcon notify;
        private System.Windows.Forms.Timer timer1;


    }
}


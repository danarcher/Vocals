namespace Vocals
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.processComboBox = new System.Windows.Forms.ComboBox();
            this.addCommandButton = new System.Windows.Forms.Button();
            this.profileComboBox = new System.Windows.Forms.ComboBox();
            this.addProfileButton = new System.Windows.Forms.Button();
            this.commandListBox = new System.Windows.Forms.ListBox();
            this.deleteProfileButton = new System.Windows.Forms.Button();
            this.deleteCommandButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.editCommandButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.refreshProcessListButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.advancedSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.startRecognitionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // processComboBox
            // 
            this.processComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.processComboBox.FormattingEnabled = true;
            this.processComboBox.Location = new System.Drawing.Point(29, 38);
            this.processComboBox.Name = "processComboBox";
            this.processComboBox.Size = new System.Drawing.Size(187, 21);
            this.processComboBox.TabIndex = 1;
            this.processComboBox.SelectedIndexChanged += new System.EventHandler(this.ProcessComboBox_SelectedIndexChanged);
            // 
            // addCommandButton
            // 
            this.addCommandButton.Location = new System.Drawing.Point(29, 19);
            this.addCommandButton.Name = "addCommandButton";
            this.addCommandButton.Size = new System.Drawing.Size(70, 23);
            this.addCommandButton.TabIndex = 1;
            this.addCommandButton.Text = "&Add";
            this.addCommandButton.UseVisualStyleBackColor = true;
            this.addCommandButton.Click += new System.EventHandler(this.AddCommandButton_Click);
            // 
            // profileComboBox
            // 
            this.profileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.profileComboBox.FormattingEnabled = true;
            this.profileComboBox.Location = new System.Drawing.Point(29, 52);
            this.profileComboBox.Name = "profileComboBox";
            this.profileComboBox.Size = new System.Drawing.Size(226, 21);
            this.profileComboBox.TabIndex = 0;
            this.profileComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // addProfileButton
            // 
            this.addProfileButton.Location = new System.Drawing.Point(29, 23);
            this.addProfileButton.Name = "addProfileButton";
            this.addProfileButton.Size = new System.Drawing.Size(98, 23);
            this.addProfileButton.TabIndex = 1;
            this.addProfileButton.Text = "Add profile";
            this.addProfileButton.UseVisualStyleBackColor = true;
            this.addProfileButton.Click += new System.EventHandler(this.AddProfile_Click);
            // 
            // commandListBox
            // 
            this.commandListBox.FormattingEnabled = true;
            this.commandListBox.Location = new System.Drawing.Point(29, 48);
            this.commandListBox.Name = "commandListBox";
            this.commandListBox.Size = new System.Drawing.Size(226, 134);
            this.commandListBox.TabIndex = 0;
            this.commandListBox.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // deleteProfileButton
            // 
            this.deleteProfileButton.Location = new System.Drawing.Point(151, 23);
            this.deleteProfileButton.Name = "deleteProfileButton";
            this.deleteProfileButton.Size = new System.Drawing.Size(104, 23);
            this.deleteProfileButton.TabIndex = 2;
            this.deleteProfileButton.Text = "Delete profile";
            this.deleteProfileButton.UseVisualStyleBackColor = true;
            this.deleteProfileButton.Click += new System.EventHandler(this.DeleteProfileButton_Click);
            // 
            // deleteCommandButton
            // 
            this.deleteCommandButton.Location = new System.Drawing.Point(185, 19);
            this.deleteCommandButton.Name = "deleteCommandButton";
            this.deleteCommandButton.Size = new System.Drawing.Size(70, 23);
            this.deleteCommandButton.TabIndex = 3;
            this.deleteCommandButton.Text = "&Delete";
            this.deleteCommandButton.UseVisualStyleBackColor = true;
            this.deleteCommandButton.Click += new System.EventHandler(this.DeleteCommandButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.addProfileButton);
            this.groupBox1.Controls.Add(this.deleteProfileButton);
            this.groupBox1.Controls.Add(this.profileComboBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 83);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Profiles";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.editCommandButton);
            this.groupBox2.Controls.Add(this.commandListBox);
            this.groupBox2.Controls.Add(this.deleteCommandButton);
            this.groupBox2.Controls.Add(this.addCommandButton);
            this.groupBox2.Location = new System.Drawing.Point(307, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(277, 190);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "&Commands";
            // 
            // editCommandButton
            // 
            this.editCommandButton.Location = new System.Drawing.Point(107, 19);
            this.editCommandButton.Name = "editCommandButton";
            this.editCommandButton.Size = new System.Drawing.Size(70, 23);
            this.editCommandButton.TabIndex = 2;
            this.editCommandButton.Text = "&Edit";
            this.editCommandButton.UseVisualStyleBackColor = true;
            this.editCommandButton.Click += new System.EventHandler(this.EditCommandButton_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.refreshProcessListButton);
            this.groupBox4.Controls.Add(this.processComboBox);
            this.groupBox4.Location = new System.Drawing.Point(12, 123);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(277, 94);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Application";
            // 
            // refreshProcessListButton
            // 
            this.refreshProcessListButton.Location = new System.Drawing.Point(222, 38);
            this.refreshProcessListButton.Name = "refreshProcessListButton";
            this.refreshProcessListButton.Size = new System.Drawing.Size(33, 23);
            this.refreshProcessListButton.TabIndex = 2;
            this.refreshProcessListButton.Text = "#";
            this.refreshProcessListButton.UseVisualStyleBackColor = true;
            this.refreshProcessListButton.Click += new System.EventHandler(this.RefreshProcessListButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.progressBar1);
            this.groupBox3.Controls.Add(this.richTextBox);
            this.groupBox3.Location = new System.Drawing.Point(12, 223);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(572, 217);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "logs";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(7, 19);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(543, 11);
            this.progressBar1.Step = 11;
            this.progressBar1.TabIndex = 1;
            // 
            // richTextBox
            // 
            this.richTextBox.Location = new System.Drawing.Point(6, 36);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.Size = new System.Drawing.Size(544, 175);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            // 
            // advancedSettingsToolStripMenuItem
            // 
            this.advancedSettingsToolStripMenuItem.Name = "advancedSettingsToolStripMenuItem";
            this.advancedSettingsToolStripMenuItem.Size = new System.Drawing.Size(117, 20);
            this.advancedSettingsToolStripMenuItem.Text = "Ad&vanced Settings";
            this.advancedSettingsToolStripMenuItem.Click += new System.EventHandler(this.AdvancedSettings_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.advancedSettingsToolStripMenuItem,
            this.startRecognitionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(597, 24);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // startRecognitionToolStripMenuItem
            // 
            this.startRecognitionToolStripMenuItem.Name = "startRecognitionToolStripMenuItem";
            this.startRecognitionToolStripMenuItem.Size = new System.Drawing.Size(121, 20);
            this.startRecognitionToolStripMenuItem.Text = "&Start Recognition >";
            this.startRecognitionToolStripMenuItem.Click += new System.EventHandler(this.StartRecognition_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 454);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Vocals";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ComboBox processComboBox;
        private System.Windows.Forms.Button addCommandButton;
        private System.Windows.Forms.ComboBox profileComboBox;
        private System.Windows.Forms.Button addProfileButton;
        private System.Windows.Forms.ListBox commandListBox;
        private System.Windows.Forms.Button deleteProfileButton;
        private System.Windows.Forms.Button deleteCommandButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.Button editCommandButton;
        private System.Windows.Forms.ToolStripMenuItem advancedSettingsToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button refreshProcessListButton;
        private System.Windows.Forms.ToolStripMenuItem startRecognitionToolStripMenuItem;
    }
}


/* 
 * 
 * DataZoneForm.Designer.cs
 * Author Stephanie Schutz for Xtricate.
 * 
 */

namespace DZT
{
    partial class DataZoneForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataZoneForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new DZT.TabControl_HiddenTabs();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.goButton = new System.Windows.Forms.Button();
            this.dataFormatComboBox = new System.Windows.Forms.ComboBox();
            this.endDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.dataFormatLabel = new System.Windows.Forms.Label();
            this.endDateLabel = new System.Windows.Forms.Label();
            this.startDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.startDateLabel = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.inRichTextBox = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.outRichTextBox = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.logRichTextBox = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(5, 475);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(826, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Enabled = false;
            this.toolStripButton1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(72, 22);
            this.toolStripButton1.Text = "Show Log";
            this.toolStripButton1.Visible = false;
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(5, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(826, 470);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(818, 444);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.typeComboBox);
            this.splitContainer1.Panel1.Controls.Add(this.goButton);
            this.splitContainer1.Panel1.Controls.Add(this.dataFormatComboBox);
            this.splitContainer1.Panel1.Controls.Add(this.endDateTimePicker);
            this.splitContainer1.Panel1.Controls.Add(this.dataFormatLabel);
            this.splitContainer1.Panel1.Controls.Add(this.endDateLabel);
            this.splitContainer1.Panel1.Controls.Add(this.startDateTimePicker);
            this.splitContainer1.Panel1.Controls.Add(this.startDateLabel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(812, 438);
            this.splitContainer1.SplitterDistance = 52;
            this.splitContainer1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(414, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Type";
            // 
            // typeComboBox
            // 
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Location = new System.Drawing.Point(417, 23);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(89, 21);
            this.typeComboBox.TabIndex = 12;
            // 
            // goButton
            // 
            this.goButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.goButton.Location = new System.Drawing.Point(758, 8);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(60, 38);
            this.goButton.TabIndex = 0;
            this.goButton.Text = "Go";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click_1);
            // 
            // dataFormatComboBox
            // 
            this.dataFormatComboBox.FormattingEnabled = true;
            this.dataFormatComboBox.Location = new System.Drawing.Point(512, 24);
            this.dataFormatComboBox.Name = "dataFormatComboBox";
            this.dataFormatComboBox.Size = new System.Drawing.Size(240, 21);
            this.dataFormatComboBox.TabIndex = 11;
            // 
            // endDateTimePicker
            // 
            this.endDateTimePicker.Location = new System.Drawing.Point(207, 24);
            this.endDateTimePicker.Name = "endDateTimePicker";
            this.endDateTimePicker.Size = new System.Drawing.Size(204, 20);
            this.endDateTimePicker.TabIndex = 10;
            // 
            // dataFormatLabel
            // 
            this.dataFormatLabel.AutoSize = true;
            this.dataFormatLabel.Location = new System.Drawing.Point(537, 9);
            this.dataFormatLabel.Name = "dataFormatLabel";
            this.dataFormatLabel.Size = new System.Drawing.Size(39, 13);
            this.dataFormatLabel.TabIndex = 9;
            this.dataFormatLabel.Text = "Format";
            // 
            // endDateLabel
            // 
            this.endDateLabel.AutoSize = true;
            this.endDateLabel.Location = new System.Drawing.Point(204, 8);
            this.endDateLabel.Name = "endDateLabel";
            this.endDateLabel.Size = new System.Drawing.Size(52, 13);
            this.endDateLabel.TabIndex = 8;
            this.endDateLabel.Text = "End Date";
            // 
            // startDateTimePicker
            // 
            this.startDateTimePicker.Location = new System.Drawing.Point(5, 25);
            this.startDateTimePicker.Name = "startDateTimePicker";
            this.startDateTimePicker.Size = new System.Drawing.Size(196, 20);
            this.startDateTimePicker.TabIndex = 5;
            this.startDateTimePicker.ValueChanged += new System.EventHandler(this.startDateTimePicker_ValueChanged);
            // 
            // startDateLabel
            // 
            this.startDateLabel.AutoSize = true;
            this.startDateLabel.Location = new System.Drawing.Point(2, 9);
            this.startDateLabel.Name = "startDateLabel";
            this.startDateLabel.Size = new System.Drawing.Size(55, 13);
            this.startDateLabel.TabIndex = 2;
            this.startDateLabel.Text = "Start Date";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Size = new System.Drawing.Size(812, 382);
            this.splitContainer2.SplitterDistance = 177;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.inRichTextBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(812, 177);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bank Statement";
            // 
            // inRichTextBox
            // 
            this.inRichTextBox.AcceptsTab = true;
            this.inRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inRichTextBox.Location = new System.Drawing.Point(5, 18);
            this.inRichTextBox.Name = "inRichTextBox";
            this.inRichTextBox.Size = new System.Drawing.Size(802, 154);
            this.inRichTextBox.TabIndex = 2;
            this.inRichTextBox.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.outRichTextBox);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox2.Size = new System.Drawing.Size(812, 201);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Xtricate Transaction Spreadsheet";
            // 
            // outRichTextBox
            // 
            this.outRichTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.outRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outRichTextBox.Location = new System.Drawing.Point(5, 18);
            this.outRichTextBox.Name = "outRichTextBox";
            this.outRichTextBox.ReadOnly = true;
            this.outRichTextBox.Size = new System.Drawing.Size(802, 178);
            this.outRichTextBox.TabIndex = 13;
            this.outRichTextBox.Text = "";
            this.outRichTextBox.WordWrap = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.logRichTextBox);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(818, 444);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // logRichTextBox
            // 
            this.logRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logRichTextBox.Location = new System.Drawing.Point(3, 3);
            this.logRichTextBox.Name = "logRichTextBox";
            this.logRichTextBox.ReadOnly = true;
            this.logRichTextBox.Size = new System.Drawing.Size(812, 438);
            this.logRichTextBox.TabIndex = 1;
            this.logRichTextBox.Text = "";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(386, 232);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(8, 20);
            this.textBox1.TabIndex = 0;
            // 
            // DataZoneForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 505);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(780, 543);
            this.Name = "DataZoneForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Xtricate - Data Zone Translator";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        public System.Windows.Forms.ToolStripLabel toolStripLabel1;
        //private System.Windows.Forms.TabControl tabControl1;
        private TabControl_HiddenTabs tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.ComboBox dataFormatComboBox;
        private System.Windows.Forms.DateTimePicker endDateTimePicker;
        private System.Windows.Forms.Label dataFormatLabel;
        private System.Windows.Forms.Label endDateLabel;
        private System.Windows.Forms.DateTimePicker startDateTimePicker;
        private System.Windows.Forms.Label startDateLabel;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox inRichTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox outRichTextBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox logRichTextBox;
        private System.Windows.Forms.TextBox textBox1;
    }
}
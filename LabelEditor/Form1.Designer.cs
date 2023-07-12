namespace LabelEditor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            loadToolStripMenuItem = new ToolStripMenuItem();
            printerToolStripMenuItem = new ToolStripMenuItem();
            tableLayoutPanel1 = new TableLayoutPanel();
            propertyGrid2 = new PropertyGrid();
            pictureBox1 = new PictureBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            addImageButton = new Button();
            addCode128Button = new Button();
            addDmButton = new Button();
            addTextButton = new Button();
            listBox1 = new ListBox();
            propertyGrid1 = new PropertyGrid();
            deleteElementButton = new Button();
            menuStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, printerToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1264, 24);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, saveToolStripMenuItem, loadToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(100, 22);
            newToolStripMenuItem.Text = "New";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(100, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(100, 22);
            loadToolStripMenuItem.Text = "Load";
            loadToolStripMenuItem.Click += loadToolStripMenuItem_Click;
            // 
            // printerToolStripMenuItem
            // 
            printerToolStripMenuItem.Name = "printerToolStripMenuItem";
            printerToolStripMenuItem.Size = new Size(54, 20);
            printerToolStripMenuItem.Text = "Printer";
            printerToolStripMenuItem.Click += printerToolStripMenuItem_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.Controls.Add(propertyGrid2, 0, 0);
            tableLayoutPanel1.Controls.Add(pictureBox1, 1, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 2, 0);
            tableLayoutPanel1.Controls.Add(listBox1, 3, 0);
            tableLayoutPanel1.Controls.Add(propertyGrid1, 3, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 24);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1264, 657);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // propertyGrid2
            // 
            propertyGrid2.Dock = DockStyle.Fill;
            propertyGrid2.Location = new Point(3, 3);
            propertyGrid2.Name = "propertyGrid2";
            propertyGrid2.Size = new Size(246, 322);
            propertyGrid2.TabIndex = 4;
            propertyGrid2.PropertyValueChanged += propertyGrid1_PropertyValueChanged;
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(255, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(626, 322);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(deleteElementButton, 0, 4);
            tableLayoutPanel2.Controls.Add(addImageButton, 0, 3);
            tableLayoutPanel2.Controls.Add(addCode128Button, 0, 2);
            tableLayoutPanel2.Controls.Add(addDmButton, 0, 1);
            tableLayoutPanel2.Controls.Add(addTextButton, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(887, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 5;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.Size = new Size(120, 322);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // addImageButton
            // 
            addImageButton.Dock = DockStyle.Fill;
            addImageButton.Location = new Point(3, 195);
            addImageButton.Name = "addImageButton";
            addImageButton.Size = new Size(114, 58);
            addImageButton.TabIndex = 3;
            addImageButton.Text = "Image";
            addImageButton.UseVisualStyleBackColor = true;
            addImageButton.Click += OnAddImageButtonClick;
            // 
            // addCode128Button
            // 
            addCode128Button.Dock = DockStyle.Fill;
            addCode128Button.Location = new Point(3, 131);
            addCode128Button.Name = "addCode128Button";
            addCode128Button.Size = new Size(114, 58);
            addCode128Button.TabIndex = 2;
            addCode128Button.Text = "Code128";
            addCode128Button.UseVisualStyleBackColor = true;
            addCode128Button.Click += OnAddCode128ButtonClick;
            // 
            // addDmButton
            // 
            addDmButton.Dock = DockStyle.Fill;
            addDmButton.Location = new Point(3, 67);
            addDmButton.Name = "addDmButton";
            addDmButton.Size = new Size(114, 58);
            addDmButton.TabIndex = 1;
            addDmButton.Text = "Datamatrix";
            addDmButton.UseVisualStyleBackColor = true;
            addDmButton.Click += OnAddDmButtonClick;
            // 
            // addTextButton
            // 
            addTextButton.Dock = DockStyle.Fill;
            addTextButton.Location = new Point(3, 3);
            addTextButton.Name = "addTextButton";
            addTextButton.Size = new Size(114, 58);
            addTextButton.TabIndex = 0;
            addTextButton.Text = "Text";
            addTextButton.UseVisualStyleBackColor = true;
            addTextButton.Click += OnAddTextButtonClick;
            // 
            // listBox1
            // 
            listBox1.Dock = DockStyle.Fill;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(1013, 3);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(248, 322);
            listBox1.TabIndex = 2;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // propertyGrid1
            // 
            propertyGrid1.Dock = DockStyle.Fill;
            propertyGrid1.Location = new Point(1013, 331);
            propertyGrid1.Name = "propertyGrid1";
            propertyGrid1.Size = new Size(248, 323);
            propertyGrid1.TabIndex = 3;
            propertyGrid1.PropertyValueChanged += propertyGrid1_PropertyValueChanged;
            // 
            // deleteElementButton
            // 
            deleteElementButton.Dock = DockStyle.Fill;
            deleteElementButton.Location = new Point(3, 259);
            deleteElementButton.Name = "deleteElementButton";
            deleteElementButton.Size = new Size(114, 60);
            deleteElementButton.TabIndex = 4;
            deleteElementButton.Text = "Delete";
            deleteElementButton.UseVisualStyleBackColor = true;
            deleteElementButton.Click += OnDeleteElementButtonClick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 681);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "е";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem printerToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox pictureBox1;
        private TableLayoutPanel tableLayoutPanel2;
        private Button addImageButton;
        private Button addCode128Button;
        private Button addDmButton;
        private Button addTextButton;
        private PropertyGrid propertyGrid2;
        private ListBox listBox1;
        private PropertyGrid propertyGrid1;
        private Button deleteElementButton;
    }
}
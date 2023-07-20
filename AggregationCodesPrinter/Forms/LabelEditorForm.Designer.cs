namespace AggregationCodesPrinter
{
    partial class LabelEditorForm
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
            printersToolStripMenuItem = new ToolStripMenuItem();
            editVariablesToolStripMenuItem = new ToolStripMenuItem();
            dataSourceToolStripMenuItem = new ToolStripMenuItem();
            tableLayoutPanel1 = new TableLayoutPanel();
            removeTaskButton = new Button();
            dataSourceGridView = new DataGridView();
            labelPropertyGrid = new PropertyGrid();
            renderedLabelPictureBox = new PictureBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            addEllipseButton = new Button();
            deleteElementButton = new Button();
            addImageButton = new Button();
            addCode128Button = new Button();
            addDmButton = new Button();
            addTextButton = new Button();
            labelElementsListBox = new ListBox();
            labelElementPropertyGrid = new PropertyGrid();
            printersListBox = new ListBox();
            printButton = new Button();
            label1 = new Label();
            numericUpDown1 = new NumericUpDown();
            addTaskButton = new Button();
            addRectangleButton = new Button();
            menuStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataSourceGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)renderedLabelPictureBox).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, printersToolStripMenuItem, editVariablesToolStripMenuItem, dataSourceToolStripMenuItem });
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
            fileToolStripMenuItem.Size = new Size(48, 20);
            fileToolStripMenuItem.Text = "Файл";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(133, 22);
            newToolStripMenuItem.Text = "Новый";
            newToolStripMenuItem.Click += OnNewToolStripMenuItemClick;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(133, 22);
            saveToolStripMenuItem.Text = "Сохранить";
            saveToolStripMenuItem.Click += OnSaveToolStripMenuItemClick;
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(133, 22);
            loadToolStripMenuItem.Text = "Загрузить";
            loadToolStripMenuItem.Click += OnLoadToolStripMenuItemClick;
            // 
            // printersToolStripMenuItem
            // 
            printersToolStripMenuItem.Name = "printersToolStripMenuItem";
            printersToolStripMenuItem.Size = new Size(76, 20);
            printersToolStripMenuItem.Text = "Принтеры";
            printersToolStripMenuItem.Click += OnPrintersToolStripMenuItemClick;
            // 
            // editVariablesToolStripMenuItem
            // 
            editVariablesToolStripMenuItem.Name = "editVariablesToolStripMenuItem";
            editVariablesToolStripMenuItem.Size = new Size(91, 20);
            editVariablesToolStripMenuItem.Text = "Переменные";
            editVariablesToolStripMenuItem.Click += OnEditVariablesToolStripMenuItemClick;
            // 
            // dataSourceToolStripMenuItem
            // 
            dataSourceToolStripMenuItem.Name = "dataSourceToolStripMenuItem";
            dataSourceToolStripMenuItem.Size = new Size(117, 20);
            dataSourceToolStripMenuItem.Text = "Источник данных";
            dataSourceToolStripMenuItem.Click += OnDataSourceToolStripMenuItemClick;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.Controls.Add(removeTaskButton, 0, 3);
            tableLayoutPanel1.Controls.Add(dataSourceGridView, 1, 2);
            tableLayoutPanel1.Controls.Add(labelPropertyGrid, 0, 0);
            tableLayoutPanel1.Controls.Add(renderedLabelPictureBox, 1, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 2, 0);
            tableLayoutPanel1.Controls.Add(labelElementsListBox, 3, 0);
            tableLayoutPanel1.Controls.Add(labelElementPropertyGrid, 3, 1);
            tableLayoutPanel1.Controls.Add(printersListBox, 0, 1);
            tableLayoutPanel1.Controls.Add(printButton, 3, 2);
            tableLayoutPanel1.Controls.Add(label1, 2, 2);
            tableLayoutPanel1.Controls.Add(numericUpDown1, 2, 3);
            tableLayoutPanel1.Controls.Add(addTaskButton, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 24);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tableLayoutPanel1.Size = new Size(1264, 610);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // removeTaskButton
            // 
            removeTaskButton.Dock = DockStyle.Fill;
            removeTaskButton.Location = new Point(3, 581);
            removeTaskButton.Name = "removeTaskButton";
            removeTaskButton.Size = new Size(246, 26);
            removeTaskButton.TabIndex = 17;
            removeTaskButton.Text = "Снять задачу";
            removeTaskButton.UseVisualStyleBackColor = true;
            removeTaskButton.Click += OnRemoveTaskButtonClick;
            // 
            // dataSourceGridView
            // 
            dataSourceGridView.AllowUserToAddRows = false;
            dataSourceGridView.AllowUserToDeleteRows = false;
            dataSourceGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataSourceGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataSourceGridView.Dock = DockStyle.Fill;
            dataSourceGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataSourceGridView.Location = new Point(255, 550);
            dataSourceGridView.Margin = new Padding(3, 2, 3, 2);
            dataSourceGridView.MultiSelect = false;
            dataSourceGridView.Name = "dataSourceGridView";
            dataSourceGridView.ReadOnly = true;
            dataSourceGridView.RowHeadersWidth = 51;
            tableLayoutPanel1.SetRowSpan(dataSourceGridView, 2);
            dataSourceGridView.RowTemplate.Height = 29;
            dataSourceGridView.ScrollBars = ScrollBars.None;
            dataSourceGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataSourceGridView.ShowEditingIcon = false;
            dataSourceGridView.Size = new Size(626, 58);
            dataSourceGridView.TabIndex = 15;
            // 
            // labelPropertyGrid
            // 
            labelPropertyGrid.Dock = DockStyle.Fill;
            labelPropertyGrid.Location = new Point(3, 3);
            labelPropertyGrid.Name = "labelPropertyGrid";
            labelPropertyGrid.Size = new Size(246, 268);
            labelPropertyGrid.TabIndex = 4;
            labelPropertyGrid.PropertyValueChanged += OnPropertyGridPropertyValueChanged;
            // 
            // renderedLabelPictureBox
            // 
            renderedLabelPictureBox.BorderStyle = BorderStyle.FixedSingle;
            renderedLabelPictureBox.Dock = DockStyle.Fill;
            renderedLabelPictureBox.Location = new Point(255, 3);
            renderedLabelPictureBox.Name = "renderedLabelPictureBox";
            tableLayoutPanel1.SetRowSpan(renderedLabelPictureBox, 2);
            renderedLabelPictureBox.Size = new Size(626, 542);
            renderedLabelPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            renderedLabelPictureBox.TabIndex = 0;
            renderedLabelPictureBox.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(addRectangleButton, 0, 4);
            tableLayoutPanel2.Controls.Add(addEllipseButton, 0, 5);
            tableLayoutPanel2.Controls.Add(deleteElementButton, 0, 6);
            tableLayoutPanel2.Controls.Add(addImageButton, 0, 3);
            tableLayoutPanel2.Controls.Add(addCode128Button, 0, 2);
            tableLayoutPanel2.Controls.Add(addDmButton, 0, 1);
            tableLayoutPanel2.Controls.Add(addTextButton, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(887, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 7;
            tableLayoutPanel1.SetRowSpan(tableLayoutPanel2, 2);
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(120, 542);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // addEllipseButton
            // 
            addEllipseButton.Dock = DockStyle.Fill;
            addEllipseButton.Location = new Point(3, 388);
            addEllipseButton.Name = "addEllipseButton";
            addEllipseButton.Size = new Size(114, 71);
            addEllipseButton.TabIndex = 5;
            addEllipseButton.Text = "Эллипс";
            addEllipseButton.UseVisualStyleBackColor = true;
            addEllipseButton.Click += OnAddEllipseButtonClick;
            // 
            // deleteElementButton
            // 
            deleteElementButton.Dock = DockStyle.Fill;
            deleteElementButton.Location = new Point(3, 465);
            deleteElementButton.Name = "deleteElementButton";
            deleteElementButton.Size = new Size(114, 74);
            deleteElementButton.TabIndex = 4;
            deleteElementButton.Text = "Удалить";
            deleteElementButton.UseVisualStyleBackColor = true;
            deleteElementButton.Click += OnDeleteElementButtonClick;
            // 
            // addImageButton
            // 
            addImageButton.Dock = DockStyle.Fill;
            addImageButton.Location = new Point(3, 234);
            addImageButton.Name = "addImageButton";
            addImageButton.Size = new Size(114, 71);
            addImageButton.TabIndex = 3;
            addImageButton.Text = "Изображение";
            addImageButton.UseVisualStyleBackColor = true;
            addImageButton.Click += OnAddImageButtonClick;
            // 
            // addCode128Button
            // 
            addCode128Button.Dock = DockStyle.Fill;
            addCode128Button.Location = new Point(3, 157);
            addCode128Button.Name = "addCode128Button";
            addCode128Button.Size = new Size(114, 71);
            addCode128Button.TabIndex = 2;
            addCode128Button.Text = "Code128";
            addCode128Button.UseVisualStyleBackColor = true;
            addCode128Button.Click += OnAddCode128ButtonClick;
            // 
            // addDmButton
            // 
            addDmButton.Dock = DockStyle.Fill;
            addDmButton.Location = new Point(3, 80);
            addDmButton.Name = "addDmButton";
            addDmButton.Size = new Size(114, 71);
            addDmButton.TabIndex = 1;
            addDmButton.Text = "DataMatrix";
            addDmButton.UseVisualStyleBackColor = true;
            addDmButton.Click += OnAddDmButtonClick;
            // 
            // addTextButton
            // 
            addTextButton.Dock = DockStyle.Fill;
            addTextButton.Location = new Point(3, 3);
            addTextButton.Name = "addTextButton";
            addTextButton.Size = new Size(114, 71);
            addTextButton.TabIndex = 0;
            addTextButton.Text = "Текст";
            addTextButton.UseVisualStyleBackColor = true;
            addTextButton.Click += OnAddTextButtonClick;
            // 
            // labelElementsListBox
            // 
            labelElementsListBox.Dock = DockStyle.Fill;
            labelElementsListBox.FormattingEnabled = true;
            labelElementsListBox.ItemHeight = 15;
            labelElementsListBox.Location = new Point(1013, 3);
            labelElementsListBox.Name = "labelElementsListBox";
            labelElementsListBox.Size = new Size(248, 268);
            labelElementsListBox.TabIndex = 2;
            labelElementsListBox.SelectedIndexChanged += OnLabelElementsListBoxSelectedIndexChanged;
            // 
            // labelElementPropertyGrid
            // 
            labelElementPropertyGrid.Dock = DockStyle.Fill;
            labelElementPropertyGrid.Location = new Point(1013, 277);
            labelElementPropertyGrid.Name = "labelElementPropertyGrid";
            labelElementPropertyGrid.Size = new Size(248, 268);
            labelElementPropertyGrid.TabIndex = 3;
            labelElementPropertyGrid.PropertyValueChanged += OnPropertyGridPropertyValueChanged;
            // 
            // printersListBox
            // 
            printersListBox.Dock = DockStyle.Fill;
            printersListBox.FormattingEnabled = true;
            printersListBox.ItemHeight = 15;
            printersListBox.Location = new Point(3, 277);
            printersListBox.Name = "printersListBox";
            printersListBox.Size = new Size(246, 268);
            printersListBox.TabIndex = 5;
            // 
            // printButton
            // 
            printButton.Dock = DockStyle.Fill;
            printButton.Location = new Point(1013, 551);
            printButton.Name = "printButton";
            tableLayoutPanel1.SetRowSpan(printButton, 2);
            printButton.Size = new Size(248, 56);
            printButton.TabIndex = 6;
            printButton.Text = "Запустить все задачи";
            printButton.UseVisualStyleBackColor = true;
            printButton.Click += OnPrintButtonClick;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(911, 555);
            label1.Name = "label1";
            label1.Size = new Size(72, 15);
            label1.TabIndex = 7;
            label1.Text = "Количество";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Anchor = AnchorStyles.None;
            numericUpDown1.Location = new Point(887, 582);
            numericUpDown1.Margin = new Padding(3, 2, 3, 2);
            numericUpDown1.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(120, 23);
            numericUpDown1.TabIndex = 8;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // addTaskButton
            // 
            addTaskButton.Dock = DockStyle.Fill;
            addTaskButton.Location = new Point(3, 551);
            addTaskButton.Name = "addTaskButton";
            addTaskButton.Size = new Size(246, 24);
            addTaskButton.TabIndex = 16;
            addTaskButton.Text = "Добавить задачу";
            addTaskButton.UseVisualStyleBackColor = true;
            addTaskButton.Click += OnAddTaskButtonClick;
            // 
            // addRectangleButton
            // 
            addRectangleButton.Dock = DockStyle.Fill;
            addRectangleButton.Location = new Point(3, 311);
            addRectangleButton.Name = "addRectangleButton";
            addRectangleButton.Size = new Size(114, 71);
            addRectangleButton.TabIndex = 6;
            addRectangleButton.Text = "Прямоугольник";
            addRectangleButton.UseVisualStyleBackColor = true;
            addRectangleButton.Click += OnAddRectangleButtonClick;
            // 
            // LabelEditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 634);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "LabelEditorForm";
            Text = "Label editor";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataSourceGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)renderedLabelPictureBox).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem printersToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox renderedLabelPictureBox;
        private TableLayoutPanel tableLayoutPanel2;
        private Button addImageButton;
        private Button addCode128Button;
        private Button addDmButton;
        private Button addTextButton;
        private PropertyGrid labelPropertyGrid;
        private ListBox labelElementsListBox;
        private PropertyGrid labelElementPropertyGrid;
        private Button deleteElementButton;
        private Button addEllipseButton;
        private ToolStripMenuItem editVariablesToolStripMenuItem;
        private ListBox printersListBox;
        private Button printButton;
        private ToolStripMenuItem dataSourceToolStripMenuItem;
        private Label label1;
        private NumericUpDown numericUpDown1;
        private DataGridView dataSourceGridView;
        private Button addTaskButton;
        private Button removeTaskButton;
        private Button addRectangleButton;
    }
}
namespace LabelEditor
{
    partial class VariableEditorForm
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
            classesListBox = new ListBox();
            variablesListBox = new ListBox();
            variablePropertyGrid = new PropertyGrid();
            createVariableButton = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            deleteVariableButton = new Button();
            label5 = new Label();
            label4 = new Label();
            label2 = new Label();
            label1 = new Label();
            objectPropertiesGridView = new DataGridView();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)objectPropertiesGridView).BeginInit();
            SuspendLayout();
            // 
            // classesListBox
            // 
            classesListBox.Dock = DockStyle.Fill;
            classesListBox.FormattingEnabled = true;
            classesListBox.ItemHeight = 15;
            classesListBox.Location = new Point(3, 48);
            classesListBox.Name = "classesListBox";
            tableLayoutPanel1.SetRowSpan(classesListBox, 2);
            classesListBox.Size = new Size(114, 399);
            classesListBox.TabIndex = 0;
            classesListBox.SelectedIndexChanged += OnClassesListBoxSelectedIndexChanged;
            // 
            // variablesListBox
            // 
            tableLayoutPanel1.SetColumnSpan(variablesListBox, 2);
            variablesListBox.Dock = DockStyle.Fill;
            variablesListBox.FormattingEnabled = true;
            variablesListBox.ItemHeight = 15;
            variablesListBox.Location = new Point(403, 93);
            variablesListBox.Name = "variablesListBox";
            variablesListBox.Size = new Size(194, 354);
            variablesListBox.TabIndex = 2;
            variablesListBox.SelectedIndexChanged += OnVariablesListBoxSelectedIndexChanged;
            // 
            // variablePropertyGrid
            // 
            variablePropertyGrid.Dock = DockStyle.Fill;
            variablePropertyGrid.Location = new Point(603, 48);
            variablePropertyGrid.Name = "variablePropertyGrid";
            tableLayoutPanel1.SetRowSpan(variablePropertyGrid, 2);
            variablePropertyGrid.Size = new Size(194, 399);
            variablePropertyGrid.TabIndex = 3;
            variablePropertyGrid.PropertyValueChanged += OnVariablePropertyGridPropertyValueChanged;
            // 
            // createVariableButton
            // 
            createVariableButton.Dock = DockStyle.Fill;
            createVariableButton.Location = new Point(403, 48);
            createVariableButton.Name = "createVariableButton";
            createVariableButton.Size = new Size(94, 39);
            createVariableButton.TabIndex = 4;
            createVariableButton.Text = "Добавить";
            createVariableButton.UseVisualStyleBackColor = true;
            createVariableButton.Click += OnCreateVariableButtonClick;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Controls.Add(deleteVariableButton, 3, 1);
            tableLayoutPanel1.Controls.Add(label5, 4, 0);
            tableLayoutPanel1.Controls.Add(label4, 2, 0);
            tableLayoutPanel1.Controls.Add(label2, 1, 0);
            tableLayoutPanel1.Controls.Add(variablePropertyGrid, 4, 1);
            tableLayoutPanel1.Controls.Add(variablesListBox, 2, 2);
            tableLayoutPanel1.Controls.Add(classesListBox, 0, 1);
            tableLayoutPanel1.Controls.Add(createVariableButton, 2, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(objectPropertiesGridView, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(3, 2, 3, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            tableLayoutPanel1.Size = new Size(800, 450);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // deleteVariableButton
            // 
            deleteVariableButton.Dock = DockStyle.Fill;
            deleteVariableButton.Location = new Point(503, 48);
            deleteVariableButton.Name = "deleteVariableButton";
            deleteVariableButton.Size = new Size(94, 39);
            deleteVariableButton.TabIndex = 12;
            deleteVariableButton.Text = "Удалить";
            deleteVariableButton.UseVisualStyleBackColor = true;
            deleteVariableButton.Click += OnDeleteVariableButtonClick;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Dock = DockStyle.Fill;
            label5.Location = new Point(603, 0);
            label5.Name = "label5";
            label5.Size = new Size(194, 45);
            label5.TabIndex = 10;
            label5.Text = "Переменная";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label4, 2);
            label4.Dock = DockStyle.Fill;
            label4.Location = new Point(403, 0);
            label4.Name = "label4";
            label4.Size = new Size(194, 45);
            label4.TabIndex = 9;
            label4.Text = "Переменные";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(123, 0);
            label2.Name = "label2";
            label2.Size = new Size(274, 45);
            label2.TabIndex = 7;
            label2.Text = "Свойство объекта";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(114, 45);
            label1.TabIndex = 6;
            label1.Text = "Целевой объект";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // objectPropertiesGridView
            // 
            objectPropertiesGridView.AllowUserToAddRows = false;
            objectPropertiesGridView.AllowUserToDeleteRows = false;
            objectPropertiesGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            objectPropertiesGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            objectPropertiesGridView.Dock = DockStyle.Fill;
            objectPropertiesGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
            objectPropertiesGridView.Location = new Point(123, 47);
            objectPropertiesGridView.Margin = new Padding(3, 2, 3, 2);
            objectPropertiesGridView.Name = "objectPropertiesGridView";
            objectPropertiesGridView.ReadOnly = true;
            objectPropertiesGridView.RowHeadersWidth = 51;
            tableLayoutPanel1.SetRowSpan(objectPropertiesGridView, 2);
            objectPropertiesGridView.RowTemplate.Height = 29;
            objectPropertiesGridView.ShowEditingIcon = false;
            objectPropertiesGridView.Size = new Size(274, 401);
            objectPropertiesGridView.TabIndex = 13;
            // 
            // VariableEditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "VariableEditorForm";
            Text = "Variable editor";
            FormClosing += OnVariableEditorFormFormClosing;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)objectPropertiesGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ListBox classesListBox;
        private ListBox variablesListBox;
        private PropertyGrid variablePropertyGrid;
        private Button createVariableButton;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label5;
        private Label label4;
        private Label label2;
        private Label label1;
        private Button deleteVariableButton;
        private DataGridView objectPropertiesGridView;
    }
}
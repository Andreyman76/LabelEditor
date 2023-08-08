namespace AggregationCodesPrinter
{
    partial class DataSourceSelectionForm
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
            selectButton = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            dataSourceGridView = new DataGridView();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataSourceGridView).BeginInit();
            SuspendLayout();
            // 
            // selectButton
            // 
            selectButton.Dock = DockStyle.Fill;
            selectButton.Location = new Point(323, 408);
            selectButton.Name = "selectButton";
            selectButton.Size = new Size(154, 39);
            selectButton.TabIndex = 0;
            selectButton.Text = "Выбрать";
            selectButton.UseVisualStyleBackColor = true;
            selectButton.Click += OnSelectButtonClick;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel1.Controls.Add(dataSourceGridView, 0, 0);
            tableLayoutPanel1.Controls.Add(selectButton, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tableLayoutPanel1.Size = new Size(800, 450);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // dataSourceGridView
            // 
            dataSourceGridView.AllowUserToAddRows = false;
            dataSourceGridView.AllowUserToDeleteRows = false;
            dataSourceGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataSourceGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tableLayoutPanel1.SetColumnSpan(dataSourceGridView, 3);
            dataSourceGridView.Dock = DockStyle.Fill;
            dataSourceGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataSourceGridView.Location = new Point(3, 2);
            dataSourceGridView.Margin = new Padding(3, 2, 3, 2);
            dataSourceGridView.MultiSelect = false;
            dataSourceGridView.Name = "dataSourceGridView";
            dataSourceGridView.ReadOnly = true;
            dataSourceGridView.RowHeadersWidth = 51;
            dataSourceGridView.RowTemplate.Height = 29;
            dataSourceGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataSourceGridView.ShowEditingIcon = false;
            dataSourceGridView.Size = new Size(794, 401);
            dataSourceGridView.TabIndex = 14;
            // 
            // DataSourceSelectionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "DataSourceSelectionForm";
            Text = "Data source selection";
            FormClosing += OnDataSourceSelectionFormClosing;
            Shown += DataSourceSelectionForm_Shown;
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataSourceGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button selectButton;
        private TableLayoutPanel tableLayoutPanel1;
        private DataGridView dataSourceGridView;
    }
}
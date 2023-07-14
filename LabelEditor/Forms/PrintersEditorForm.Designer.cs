namespace LabelEditor
{
    partial class PrintersEditorForm
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
            printersListBox = new ListBox();
            printerPropertyGrid = new PropertyGrid();
            addUsbPrinterButton = new Button();
            addNetPrinterButton = new Button();
            deletePrinter = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // printersListBox
            // 
            tableLayoutPanel1.SetColumnSpan(printersListBox, 2);
            printersListBox.Dock = DockStyle.Fill;
            printersListBox.FormattingEnabled = true;
            printersListBox.ItemHeight = 15;
            printersListBox.Location = new Point(3, 3);
            printersListBox.Name = "printersListBox";
            printersListBox.Size = new Size(393, 399);
            printersListBox.TabIndex = 0;
            printersListBox.SelectedIndexChanged += OnPrintersListBoxSelectedIndexChanged;
            // 
            // printerPropertyGrid
            // 
            tableLayoutPanel1.SetColumnSpan(printerPropertyGrid, 2);
            printerPropertyGrid.Dock = DockStyle.Fill;
            printerPropertyGrid.Location = new Point(402, 3);
            printerPropertyGrid.Name = "printerPropertyGrid";
            printerPropertyGrid.Size = new Size(395, 399);
            printerPropertyGrid.TabIndex = 1;
            printerPropertyGrid.PropertyValueChanged += OnPrinterPropertyGridPropertyValueChanged;
            // 
            // addUsbPrinterButton
            // 
            addUsbPrinterButton.Dock = DockStyle.Fill;
            addUsbPrinterButton.Location = new Point(3, 408);
            addUsbPrinterButton.Name = "addUsbPrinterButton";
            addUsbPrinterButton.Size = new Size(260, 39);
            addUsbPrinterButton.TabIndex = 2;
            addUsbPrinterButton.Text = "Добавить USB принтер";
            addUsbPrinterButton.UseVisualStyleBackColor = true;
            addUsbPrinterButton.Click += OnAddUsbPrinterButtonClick;
            // 
            // addNetPrinterButton
            // 
            tableLayoutPanel1.SetColumnSpan(addNetPrinterButton, 2);
            addNetPrinterButton.Dock = DockStyle.Fill;
            addNetPrinterButton.Location = new Point(269, 408);
            addNetPrinterButton.Name = "addNetPrinterButton";
            addNetPrinterButton.Size = new Size(260, 39);
            addNetPrinterButton.TabIndex = 3;
            addNetPrinterButton.Text = "Добавить сетевой принтер";
            addNetPrinterButton.UseVisualStyleBackColor = true;
            addNetPrinterButton.Click += OnAddNetPrinterButtonClick;
            // 
            // deletePrinter
            // 
            deletePrinter.Dock = DockStyle.Fill;
            deletePrinter.Location = new Point(535, 408);
            deletePrinter.Name = "deletePrinter";
            deletePrinter.Size = new Size(262, 39);
            deletePrinter.TabIndex = 4;
            deletePrinter.Text = "Удалить принтер";
            deletePrinter.UseVisualStyleBackColor = true;
            deletePrinter.Click += OnDeleteButtonClick;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666679F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666679F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel1.Controls.Add(addUsbPrinterButton, 0, 1);
            tableLayoutPanel1.Controls.Add(printersListBox, 0, 0);
            tableLayoutPanel1.Controls.Add(printerPropertyGrid, 2, 0);
            tableLayoutPanel1.Controls.Add(deletePrinter, 3, 1);
            tableLayoutPanel1.Controls.Add(addNetPrinterButton, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(800, 450);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // PrintersEditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "PrintersEditorForm";
            Text = "Printers editor";
            FormClosing += OnPrintersEditorFormFormClosing;
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListBox printersListBox;
        private PropertyGrid printerPropertyGrid;
        private Button addUsbPrinterButton;
        private Button addNetPrinterButton;
        private Button deletePrinter;
        private TableLayoutPanel tableLayoutPanel1;
    }
}
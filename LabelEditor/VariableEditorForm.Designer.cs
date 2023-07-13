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
            propertiesListBox = new ListBox();
            variablesListBox = new ListBox();
            variablePropertyGrid = new PropertyGrid();
            createVariableButton = new Button();
            SuspendLayout();
            // 
            // classesListBox
            // 
            classesListBox.FormattingEnabled = true;
            classesListBox.ItemHeight = 15;
            classesListBox.Location = new Point(12, 12);
            classesListBox.Name = "classesListBox";
            classesListBox.Size = new Size(191, 184);
            classesListBox.TabIndex = 0;
            classesListBox.SelectedIndexChanged += OnClassesListBoxSelectedIndexChanged;
            // 
            // propertiesListBox
            // 
            propertiesListBox.FormattingEnabled = true;
            propertiesListBox.ItemHeight = 15;
            propertiesListBox.Location = new Point(209, 12);
            propertiesListBox.Name = "propertiesListBox";
            propertiesListBox.Size = new Size(187, 184);
            propertiesListBox.TabIndex = 1;
            // 
            // variablesListBox
            // 
            variablesListBox.FormattingEnabled = true;
            variablesListBox.ItemHeight = 15;
            variablesListBox.Location = new Point(12, 202);
            variablesListBox.Name = "variablesListBox";
            variablesListBox.Size = new Size(191, 229);
            variablesListBox.TabIndex = 2;
            variablesListBox.SelectedIndexChanged += OnVariablesListBoxSelectedIndexChanged;
            // 
            // variablePropertyGrid
            // 
            variablePropertyGrid.Location = new Point(516, 156);
            variablePropertyGrid.Name = "variablePropertyGrid";
            variablePropertyGrid.Size = new Size(272, 282);
            variablePropertyGrid.TabIndex = 3;
            variablePropertyGrid.PropertyValueChanged += OnVariablePropertyGridPropertyValueChanged;
            // 
            // createVariableButton
            // 
            createVariableButton.Location = new Point(296, 243);
            createVariableButton.Name = "createVariableButton";
            createVariableButton.Size = new Size(120, 74);
            createVariableButton.TabIndex = 4;
            createVariableButton.Text = "Create";
            createVariableButton.UseVisualStyleBackColor = true;
            createVariableButton.Click += OnCreateVariableButtonClick;
            // 
            // VariableEditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(createVariableButton);
            Controls.Add(variablePropertyGrid);
            Controls.Add(variablesListBox);
            Controls.Add(propertiesListBox);
            Controls.Add(classesListBox);
            Name = "VariableEditorForm";
            Text = "VariableEditorForm";
            ResumeLayout(false);
        }

        #endregion

        private ListBox classesListBox;
        private ListBox propertiesListBox;
        private ListBox variablesListBox;
        private PropertyGrid variablePropertyGrid;
        private Button createVariableButton;
    }
}
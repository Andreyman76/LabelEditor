using LabelTemplate;

namespace LabelEditor
{
    public partial class LabelEditorForm : Form
    {
        private string _printerName = string.Empty;
        private LabelEditor _labelEditor = new()
        {
            RegisteredTypes = new()
            {
                typeof(LabelUtils),
                typeof(Gtin),
                typeof(Pallet)
            }
        };

        public LabelEditorForm()
        {
            InitializeComponent();
            _labelEditor.LoadVariablesFromJson();
            labelPropertyGrid.SelectedObject = _labelEditor.LabelTemplate;
            Redraw();
        }

        private void printerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new PrintDialog();
            var result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                _printerName = dialog.PrinterSettings.PrinterName;
                _labelEditor.GetCurrentLabel().Print(_printerName);
            }
        }

        private void OnSaveToolStripMenuItemClick(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog()
            {
                DefaultExt = "xml",
                Filter = "XML Files|*.xml;"
            };

            var result = dialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                var xml = _labelEditor.SaveLabelToXml();
                File.WriteAllText(dialog.FileName, xml);
            }
        }

        private void OnLoadToolStripMenuItemClick(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog()
            {
                DefaultExt = "xml",
                Filter = "XML Files|*.xml;"
            };

            var result = dialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                var xml = File.ReadAllText(dialog.FileName);
                _labelEditor.LoadLabelFromXml(xml);
            }

            UpdateListOfObjects();
            Redraw();
        }

        private void OnNewToolStripMenuItemClick(object sender, EventArgs e)
        {
            _labelEditor.LabelTemplate = new()
            {
                Size = new(22, 22),
                Dpi = 203
            };

            labelPropertyGrid.SelectedObject = _labelEditor.LabelTemplate;
            labelElementPropertyGrid.SelectedObject = null;
            UpdateListOfObjects();
            Redraw();
        }

        private void OnAddTextButtonClick(object sender, EventArgs e)
        {
            _labelEditor.LabelTemplate.Elements.Add(
                new LabelText()
                {
                    Name = "Text",
                    Text = "1234567890"
                }
                );
            UpdateListOfObjects();
            Redraw();
        }

        private void OnAddDmButtonClick(object sender, EventArgs e)
        {
            _labelEditor.LabelTemplate.Elements.Add(
               new LabelDataMatrix()
               {
                   Name = "DataMatrix",
                   Code = "${gs}0105449000203359215gHAvnw6TXwN4${gs}93dGVz",
                   Size = 20
               }
               );

            UpdateListOfObjects();
            Redraw();
        }

        private void OnAddCode128ButtonClick(object sender, EventArgs e)
        {
            _labelEditor.LabelTemplate.Elements.Add(
               new LabelCode128()
               {
                   Name = "Code128",
                   Code = "0123456789",
                   Size = new(20, 10)
               }
               );

            UpdateListOfObjects();
            Redraw();
        }

        private void OnAddImageButtonClick(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();

            var result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                var imageBytes = File.ReadAllBytes(dialog.FileName);

                _labelEditor.LabelTemplate.Elements.Add(
                new LabelImage()
                {
                    Name = "Image",
                    Size = new(20, 20),
                    ImageBytes = imageBytes
                }
                );

                UpdateListOfObjects();
                Redraw();
            }
        }

        private void OnAddEllipseButtonClick(object sender, EventArgs e)
        {
            _labelEditor.LabelTemplate.Elements.Add(
               new LabelEllipse()
               {
                   Name = "Ellipse",
                   Size = new(20, 20)
               }
               );

            UpdateListOfObjects();
            Redraw();
        }

        private void OnDeleteElementButtonClick(object sender, EventArgs e)
        {
            if (labelElementsListBox.SelectedIndex >= 0)
            {
                labelElementPropertyGrid.SelectedObject = null;
                _labelEditor.LabelTemplate.Elements.RemoveAt(labelElementsListBox.SelectedIndex);
            }

            UpdateListOfObjects();
            Redraw();
        }

        private void UpdateListOfObjects()
        {
            labelElementsListBox.Items.Clear();

            foreach (var element in _labelEditor.LabelTemplate.Elements)
            {
                labelElementsListBox.Items.Add(element.Name);
            }
        }

        private void Redraw()
        {
            renderedLabelPictureBox.BackColor = Color.Gray;
            renderedLabelPictureBox.Image = _labelEditor.GetCurrentLabel().GetImage();
        }

        private void OnLabelElementsListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (labelElementsListBox.SelectedIndex >= 0)
            {
                labelElementPropertyGrid.SelectedObject = _labelEditor.LabelTemplate.Elements[labelElementsListBox.SelectedIndex];
            }
        }

        private void OnPropertyGridPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            Redraw();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var printer = new ZplPrinter();

            //var ip = IPAddress.Parse("192.168.10.13");
            //var port = 9100;

            //printer.Connect(new(ip, port));

            //printer.Print(_labelEditor.GetCurrentLabel());

            //printer.Disconnect();
        }

        private void OnEditVariablesToolStripMenuItemClick(object sender, EventArgs e)
        {
            var varEditor = new VariableEditorForm(_labelEditor);

            varEditor.ShowDialog();
        }
    }
}
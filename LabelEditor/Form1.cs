using LabelTemplate;
using System.Diagnostics;
using System.Xml.Serialization;

namespace LabelEditor
{
    public partial class Form1 : Form
    {
        private string _printerName = string.Empty;
        private LabelEditor _labelEditor = new();

        public Form1()
        {
            InitializeComponent();
            _labelEditor.CurrentLabel = new()
            {
                Size = new(22, 22)
            };

            propertyGrid2.SelectedObject = _labelEditor.CurrentLabel;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void printerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new PrintDialog();
            var result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                _printerName = dialog.PrinterSettings.PrinterName;
                _labelEditor.CurrentLabel.Print(_printerName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();

            var result = dialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                //var xml = _labelEditor.SaveLabelToXml();
                //File.WriteAllText(dialog.FileName, xml);
                pictureBox1.Image.Save(dialog.FileName);
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();

            var result = dialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                var xml = File.ReadAllText(dialog.FileName);
                _labelEditor.LoadLabelFromXml(xml);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            _labelEditor.CurrentLabel.Elements.Add(
                new LabelText()
                {
                    Name = "Text",
                    Text = "1234567890"
                }
                );
            AddItemsToListOfObjects();
            Redraw();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _labelEditor.CurrentLabel.Elements.Add(
               new LabelDataMatrix()
               {
                   Name = "DM",
                   Code = "0105449000203359215gHAvnw6TXwN4\u001d93dGVz",
                   Size = 50
               }
               );

            AddItemsToListOfObjects();
            Redraw();
        }

        private void AddItemsToListOfObjects()
        {
            listBox1.Items.Clear();

            foreach (var element in _labelEditor.CurrentLabel.Elements)
            {
                listBox1.Items.Add(element.Name);
            }
        }

        private void Redraw()
        {
            pictureBox1.BackColor = Color.Gray;
            pictureBox1.Image = _labelEditor.CurrentLabel.GetImage();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _labelEditor.CurrentLabel.Elements.Add(
               new LabelCode128()
               {
                   Name = "Code128",
                   Code = "0123456789",
                   Size = new(100, 50)
               }
               );

            AddItemsToListOfObjects();
            Redraw();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _labelEditor.CurrentLabel.Elements.Add(
              new LabelImage()
              {
                  Name = "Image",
                  Size = new(50, 50)
              }
              );

            AddItemsToListOfObjects();
            Redraw();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                propertyGrid1.SelectedObject = _labelEditor.CurrentLabel.Elements[listBox1.SelectedIndex];
            }
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            Redraw();
        }
    }
}
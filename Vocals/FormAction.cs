using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Vocals
{
    public partial class FormAction : Form {
        Keys[] keyDataSource, keyDataSource2;

        public Actions Action { get; set; } = new Actions();
        
        public FormAction() {

            InitializeComponent();
            keyDataSource = (Keys[])Enum.GetValues(typeof(Keys)).Cast<Keys>();
            keyDataSource2 = (Keys[])Enum.GetValues(typeof(Keys)).Cast<Keys>();

            comboBox3.DataSource = keyDataSource2;
            comboBox2.DataSource = keyDataSource;
            comboBox1.DataSource = new string[]{"Key press","Timer"};

            numericUpDown1.DecimalPlaces = 2;
            numericUpDown1.Increment = 0.1M;
        }

        public FormAction(Actions a)
        {
            InitializeComponent();
            keyDataSource = (Keys[])Enum.GetValues(typeof(Keys)).Cast<Keys>();
            keyDataSource2 = (Keys[])Enum.GetValues(typeof(Keys)).Cast<Keys>();

            comboBox3.DataSource = keyDataSource2;
            comboBox2.DataSource = keyDataSource;
            comboBox1.DataSource = new string[] { "Key press", "Timer" };

            numericUpDown1.DecimalPlaces = 2;
            numericUpDown1.Increment = 0.1M;

            SetFromAction(a);
        }

        private void SetFromAction(Actions a)
        {
            comboBox3.SelectedItem = a.pre;
            comboBox2.SelectedItem = a.keys;
            numericUpDown1.Value = Convert.ToDecimal(a.timer);
            comboBox1.SelectedItem = a.type;

            checkBox1.Checked = a.keyModifier.HasFlag(Keys.Control);
            checkBox2.Checked = a.keyModifier.HasFlag(Keys.Shift);
            checkBox3.Checked = a.keyModifier.HasFlag(Keys.Alt);

            Action = a;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            Action.type= (string)comboBox1.SelectedItem;
            switch (Action.type) {
                case "Key press" :
                    numericUpDown1.Enabled = false;
                    comboBox2.Enabled = true;
                    checkBox1.Enabled = true;
                    checkBox2.Enabled = true;
                    checkBox3.Enabled = true;
                    break;
                case "Timer":
                    numericUpDown1.Enabled = true;
                    comboBox2.Enabled = false;
                    checkBox1.Enabled = false;
                    checkBox2.Enabled = false;
                    checkBox3.Enabled = false;
                    break;
                default :
                    break;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            Action.timer = (float)numericUpDown1.Value;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) {
            Action.keys = (Keys)comboBox2.SelectedItem;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Action.pre = (Keys)comboBox3.SelectedItem;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            Action.keyModifier &= ~Keys.Control;
            if (checkBox1.Checked) Action.keyModifier |= Keys.Control;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e) {
            Action.keyModifier &= ~Keys.Shift;
            if (checkBox1.Checked) Action.keyModifier |= Keys.Shift;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e) {
            Action.keyModifier &= ~Keys.Alt;
            if (checkBox1.Checked) Action.keyModifier |= Keys.Alt;
        }

        private void Detect_Click(object sender, EventArgs e)
        {
            using (var dialog = new FormDetect())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    SetFromAction(dialog.Action);
                }
            }
        }
    }
}

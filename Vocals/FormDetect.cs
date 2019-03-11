using System.Windows.Forms;

namespace Vocals
{
    public partial class FormDetect : Form
    {
        private bool anyKeyDown;
        private Keys modifier;
        private Keys firstKey;
        private Keys secondKey;

        public FormDetect()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        protected override bool IsInputKey(Keys keyData)
        {
            return true;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            anyKeyDown = true;
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
            else if (e.KeyCode == Keys.ControlKey)
            {
                modifier |= Keys.Control;
            }
            else if (e.KeyCode == Keys.ShiftKey)
            {
                modifier |= Keys.Shift;
            }
            else if (e.KeyCode == Keys.Menu)
            {
                modifier |= Keys.Alt;
            }
            else if (firstKey == Keys.None)
            {
                modifier = e.Modifiers;
                firstKey = e.KeyCode;
            }
            else
            {
                secondKey = e.KeyCode;
            }
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (!anyKeyDown)
            {
                base.OnKeyUp(e);
                return;
            }

            if (secondKey != Keys.None)
            {
                Action = new Actions("Key press", 0, modifier, firstKey, secondKey);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                Action = new Actions("Key press", 0, modifier, Keys.None, firstKey);
                DialogResult = DialogResult.OK;
                Close();
            }
            base.OnKeyUp(e);
        }

        public Actions Action { get; set; }
    }
}

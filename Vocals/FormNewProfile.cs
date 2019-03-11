using System.Windows.Forms;

namespace Vocals
{
    public partial class FormNewProfile : Form
    {
        public FormNewProfile()
        {
            InitializeComponent();
        }

        public string profileName
        {
            get => textBox1.Text;
            set => textBox1.Text = value;
        }
    }
}

using System.Windows.Forms;

namespace AlberEOL.UI.GraphicalComponents
{
    public partial class RelayControl : UserControl
    {
        public string ControlLabelText
        {
            get
            {
                return this.RelayControlLabel.Text;
            }
            set
            {
                this.RelayControlLabel.Text = value;
            }
        }
        public RelayControl()
        {
            InitializeComponent();
        }
    }
}

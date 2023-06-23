using System.Drawing;
using System.Windows.Forms;

namespace AlberEOL.UI.GraphicalComponents
{
    public partial class RegisterControl : UserControl
    {
        public string Bit0Text
        {
            get
            {
                return Bit0Label.Text;
            }
            set
            {
                Bit0Label.Text = value;
            }
        }

        public string Bit1Text
        {
            get
            {
                return Bit1Label.Text;
            }
            set
            {
                Bit1Label.Text = value;
            }
        }

        public string Bit2Text
        {
            get
            {
                return Bit2Label.Text;
            }
            set
            {
                Bit2Label.Text = value;
            }
        }

        public string Bit3Text
        {
            get
            {
                return Bit3Label.Text;
            }
            set
            {
                Bit3Label.Text = value;
            }
        }

        public string Bit4Text
        {
            get
            {
                return Bit4Label.Text;
            }
            set
            {
                Bit4Label.Text = value;
            }
        }

        public string Bit5Text
        {
            get
            {
                return Bit5Label.Text;
            }
            set
            {
                Bit5Label.Text = value;
            }
        }

        public string Bit6Text
        {
            get
            {
                return Bit6Label.Text;
            }
            set
            {
                Bit6Label.Text = value;
            }
        }

        public string Bit7Text
        {
            get
            {
                return Bit7Label.Text;
            }
            set
            {
                Bit7Label.Text = value;
            }
        }

        public string Title
        {
            get
            {
                return TitleLabel.Text;
            }
            set
            {
                TitleLabel.Text = value;
            }
        }

        private bool bit0Status;
        public bool Bit0Status
        {
            get
            {
                return bit0Status;
            }
            set
            {
                bit0Status = value;
                Bit0Label.BackColor = value ? Color.LimeGreen : Color.SlateGray;
            }
        }

        private bool bit1Status;
        public bool Bit1Status
        {
            get
            {
                return bit1Status;
            }
            set
            {
                bit1Status = value;
                Bit1Label.BackColor = value ? Color.LimeGreen : Color.SlateGray;
            }
        }

        private bool bit2Status;
        public bool Bit2Status
        {
            get
            {
                return bit2Status;
            }
            set
            {
                bit2Status = value;
                Bit2Label.BackColor = value ? Color.LimeGreen : Color.SlateGray;
            }
        }

        private bool bit3Status;
        public bool Bit3Status
        {
            get
            {
                return bit3Status;
            }
            set
            {
                bit3Status = value;
                Bit3Label.BackColor = value ? Color.LimeGreen : Color.SlateGray;
            }
        }

        private bool bit4Status;
        public bool Bit4Status
        {
            get
            {
                return bit4Status;
            }
            set
            {
                bit4Status = value;
                Bit4Label.BackColor = value ? Color.LimeGreen : Color.SlateGray;
            }
        }

        private bool bit5Status;
        public bool Bit5Status
        {
            get
            {
                return bit5Status;
            }
            set
            {
                bit5Status = value;
                Bit5Label.BackColor = value ? Color.LimeGreen : Color.SlateGray;
            }
        }

        private bool bit6Status;
        public bool Bit6Status
        {
            get
            {
                return bit6Status;
            }
            set
            {
                bit6Status = value;
                Bit6Label.BackColor = value ? Color.LimeGreen : Color.SlateGray;
            }
        }

        private bool bit7Status;
        public bool Bit7Status
        {
            get
            {
                return bit7Status;
            }
            set
            {
                bit7Status = value;
                Bit7Label.BackColor = value ? Color.LimeGreen : Color.SlateGray;
            }
        }

        public RegisterControl()
        {
            InitializeComponent();
        }
    }
}

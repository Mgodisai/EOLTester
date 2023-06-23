using System;
using System.Drawing;
using System.Windows.Forms;

namespace AlberEOL.UI.GraphicalComponents
{
    public partial class RamComponent : UserControl
    {
        private const int COL_CNT = 0x10;

        public int RowCount { get; private set; }

        public RamComponent()
        {
            InitializeComponent();
        }

        public void Init(ushort address, byte[] data)
        {
            lvwRam.Items.Clear();
            RowCount = (int)Math.Ceiling((decimal)data.Length / COL_CNT);
            for (int row = 0; row < RowCount; row++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.UseItemStyleForSubItems = false;
                lvi.Text = string.Format("0x{0:X4}", address + row * COL_CNT);
                for (int col = 0; col < COL_CNT; col++)
                {
                    ListViewItem.ListViewSubItem si = lvi.SubItems.Add("-");
                }
                lvwRam.Items.Add(lvi);
            }
            UpdateRam(data, null);
        }

        public void UpdateRam(byte[] data, bool[] highlight)
        {
            int i = 0;
            for (int row = 0; row < RowCount; row++)
            {
                ListViewItem lvi = lvwRam.Items[row];
                for (int col = 0; col < COL_CNT; col++)
                {
                    var check = row * COL_CNT + col;
                    if (i < data.Length)
                    {
                        ListViewItem.ListViewSubItem si = lvi.SubItems[col + 1];
                        si.Text = data[i].ToString("X2");
                        si.BackColor = highlight != null && highlight[i]
                            ? Color.Yellow
                            : Color.White;
                    }
                    i++;
                }
            }
        }
    }
}

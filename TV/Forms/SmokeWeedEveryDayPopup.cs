using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TV
{
    public partial class SmokeWeedEveryDayPopup : Form
    {
        public SmokeWeedEveryDayPopup()
        {
            InitializeComponent();
        }

        private void SmokeWeedEveryDayPopup_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            TopMost = true;
        }
    }
}

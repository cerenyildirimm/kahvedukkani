using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kahvedukkani
{
    public partial class AnaMenu : Form
    {
        public AnaMenu()
        {
            InitializeComponent();
        }

        private void btnUrunler_Click(object sender, EventArgs e)
        {
            FrmUrun form = new FrmUrun();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmKategori form = new FrmKategori();
            form.Show();
        }

        private void btnPersonel_Click(object sender, EventArgs e)
        {
            FrmPersonel form = new FrmPersonel();
            form.Show();
        }

        private void btnSiparisler_Click(object sender, EventArgs e)
        {
            FrmSiparis form = new FrmSiparis();
            form.Show();
        }
    }
}

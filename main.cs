using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Travel
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }
        private void btnReservasi_Click(object sender, EventArgs e)
        {
            reservasi reservasiform = new reservasi();
            reservasiform.Show();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            Admin adminForm = new Admin();
            adminForm.Show();
        }

        private void btnMobil_Click(object sender, EventArgs e)
        {
            mobil mobilForm = new mobil();
            mobilForm.Show();
        }

        private void main_Load(object sender, EventArgs e)
        {

        }
    }
}
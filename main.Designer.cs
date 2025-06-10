using System.Drawing;
using System.Windows.Forms;

namespace Travel
{
    partial class main
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnAdmin;
        private System.Windows.Forms.Button btnMobil;

        private void InitializeComponent()
        {
            btnAdmin = new Button();
            btnMobil = new Button();
            btnReservasi = new Button();
            SuspendLayout();
            // 
            // btnAdmin
            // 
            btnAdmin.Location = new Point(57, 53);
            btnAdmin.Margin = new Padding(3, 4, 3, 4);
            btnAdmin.Name = "btnAdmin";
            btnAdmin.Size = new Size(137, 53);
            btnAdmin.TabIndex = 0;
            btnAdmin.Text = "pelanggan";
            btnAdmin.UseVisualStyleBackColor = true;
            btnAdmin.Click += btnAdmin_Click;
            // 
            // btnMobil
            // 
            btnMobil.Location = new Point(265, 53);
            btnMobil.Margin = new Padding(3, 4, 3, 4);
            btnMobil.Name = "btnMobil";
            btnMobil.Size = new Size(137, 53);
            btnMobil.TabIndex = 1;
            btnMobil.Text = "Mobil";
            btnMobil.UseVisualStyleBackColor = true;
            btnMobil.Click += btnMobil_Click;
            // 
            // btnReservasi
            // 
            btnReservasi.Location = new Point(467, 53);
            btnReservasi.Margin = new Padding(3, 4, 3, 4);
            btnReservasi.Name = "btnReservasi";
            btnReservasi.Size = new Size(137, 53);
            btnReservasi.TabIndex = 2;
            btnReservasi.Text = "Reservasi";
            btnReservasi.UseVisualStyleBackColor = true;
            btnReservasi.Click += btnReservasi_Click;
            // 
            // main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(651, 161);
            Controls.Add(btnReservasi);
            Controls.Add(btnAdmin);
            Controls.Add(btnMobil);
            Margin = new Padding(3, 4, 3, 4);
            Name = "main";
            Text = "Main Menu";
            Load += main_Load;
            ResumeLayout(false);
        }

        private Button btnReservasi;
    }
}
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
            this.btnAdmin = new System.Windows.Forms.Button();
            this.btnMobil = new System.Windows.Forms.Button();
            this.btnReservasi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAdmin
            // 
            this.btnAdmin.BackColor = System.Drawing.Color.PapayaWhip;
            this.btnAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdmin.Location = new System.Drawing.Point(57, 62);
            this.btnAdmin.Name = "btnAdmin";
            this.btnAdmin.Size = new System.Drawing.Size(137, 42);
            this.btnAdmin.TabIndex = 0;
            this.btnAdmin.Text = "pelanggan";
            this.btnAdmin.UseVisualStyleBackColor = false;
            this.btnAdmin.Click += new System.EventHandler(this.btnAdmin_Click);
            // 
            // btnMobil
            // 
            this.btnMobil.BackColor = System.Drawing.Color.PapayaWhip;
            this.btnMobil.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMobil.Location = new System.Drawing.Point(265, 62);
            this.btnMobil.Name = "btnMobil";
            this.btnMobil.Size = new System.Drawing.Size(137, 42);
            this.btnMobil.TabIndex = 1;
            this.btnMobil.Text = "Mobil";
            this.btnMobil.UseVisualStyleBackColor = false;
            this.btnMobil.Click += new System.EventHandler(this.btnMobil_Click);
            // 
            // btnReservasi
            // 
            this.btnReservasi.BackColor = System.Drawing.Color.PapayaWhip;
            this.btnReservasi.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReservasi.Location = new System.Drawing.Point(467, 62);
            this.btnReservasi.Name = "btnReservasi";
            this.btnReservasi.Size = new System.Drawing.Size(137, 42);
            this.btnReservasi.TabIndex = 2;
            this.btnReservasi.Text = "Reservasi";
            this.btnReservasi.UseVisualStyleBackColor = false;
            this.btnReservasi.Click += new System.EventHandler(this.btnReservasi_Click);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Travel.Properties.Resources.Kumpulan_Background_Pastel_yang_Indah_dan_Elegan;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(682, 177);
            this.Controls.Add(this.btnReservasi);
            this.Controls.Add(this.btnAdmin);
            this.Controls.Add(this.btnMobil);
            this.Name = "main";
            this.Text = "Main Menu";
            this.ResumeLayout(false);

        }

        private Button btnReservasi;
    }
}
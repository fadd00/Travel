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
            btnAdmin.Location = new Point(50, 40);
            btnAdmin.Name = "btnAdmin";
            btnAdmin.Size = new Size(120, 40);
            btnAdmin.TabIndex = 0;
            btnAdmin.Text = "pelanggan";
            btnAdmin.UseVisualStyleBackColor = true;
            btnAdmin.Click += btnAdmin_Click;
            // 
            // btnMobil
            // 
            btnMobil.Location = new Point(232, 40);
            btnMobil.Name = "btnMobil";
            btnMobil.Size = new Size(120, 40);
            btnMobil.TabIndex = 1;
            btnMobil.Text = "Mobil";
            btnMobil.UseVisualStyleBackColor = true;
            btnMobil.Click += btnMobil_Click;
            // 
            // btnReservasi
            // 
            btnReservasi.Location = new Point(409, 40);
            btnReservasi.Name = "btnReservasi";
            btnReservasi.Size = new Size(120, 40);
            btnReservasi.TabIndex = 2;
            btnReservasi.Text = "Reservasi";
            btnReservasi.UseVisualStyleBackColor = true;
            btnReservasi.Click += btnReservasi_Click;
            // 
            // main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(570, 121);
            Controls.Add(btnReservasi);
            Controls.Add(btnAdmin);
            Controls.Add(btnMobil);
            Name = "main";
            Text = "Main Menu";
            ResumeLayout(false);
        }
        private Button btnReservasi;
    }
}
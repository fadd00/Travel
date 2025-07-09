namespace Travel
{
    partial class reservasi
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cbPelanggan;
        private System.Windows.Forms.ComboBox cbJadwal;
        private System.Windows.Forms.TextBox tJumlahTiket;
        private System.Windows.Forms.TextBox tTotalHarga;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;

        private void InitializeComponent()
        {
            cbPelanggan = new ComboBox();
            cbJadwal = new ComboBox();
            tJumlahTiket = new TextBox();
            tTotalHarga = new TextBox();
            cbStatus = new ComboBox();
            dataGridView1 = new DataGridView();
            btnTambah = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // cbPelanggan
            // 
            cbPelanggan.Location = new Point(30, 30);
            cbPelanggan.Name = "cbPelanggan";
            cbPelanggan.Size = new Size(200, 23);
            cbPelanggan.TabIndex = 0;
            cbPelanggan.SelectedIndexChanged += cbPelanggan_SelectedIndexChanged;
            // 
            // cbJadwal
            // 
            cbJadwal.Location = new Point(30, 70);
            cbJadwal.Name = "cbJadwal";
            cbJadwal.Size = new Size(200, 23);
            cbJadwal.TabIndex = 1;
            // 
            // tJumlahTiket
            // 
            tJumlahTiket.Location = new Point(30, 110);
            tJumlahTiket.Name = "tJumlahTiket";
            tJumlahTiket.PlaceholderText = "Jumlah Tiket";
            tJumlahTiket.Size = new Size(200, 23);
            tJumlahTiket.TabIndex = 2;
            // 
            // tTotalHarga
            // 
            tTotalHarga.Location = new Point(30, 150);
            tTotalHarga.Name = "tTotalHarga";
            tTotalHarga.PlaceholderText = "Total Harga";
            tTotalHarga.Size = new Size(200, 23);
            tTotalHarga.TabIndex = 3;
            // 
            // cbStatus
            // 
            cbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStatus.Items.AddRange(new object[] { "pending", "terbayar", "dibatalkan" });
            cbStatus.Location = new Point(30, 190);
            cbStatus.Name = "cbStatus";
            cbStatus.Size = new Size(200, 23);
            cbStatus.TabIndex = 4;
            // 
            // dataGridView1
            // 
            dataGridView1.Location = new Point(260, 30);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(400, 250);
            dataGridView1.TabIndex = 5;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // btnTambah
            // 
            btnTambah.Location = new Point(30, 230);
            btnTambah.Name = "btnTambah";
            btnTambah.Size = new Size(75, 23);
            btnTambah.TabIndex = 6;
            btnTambah.Text = "Tambah";
            btnTambah.Click += btnTambah_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(115, 230);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 7;
            btnUpdate.Text = "Update";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(200, 230);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "Delete";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(30, 270);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(245, 23);
            btnRefresh.TabIndex = 9;
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // reservasi
            // 
            ClientSize = new Size(700, 320);
            Controls.Add(cbPelanggan);
            Controls.Add(cbJadwal);
            Controls.Add(tJumlahTiket);
            Controls.Add(tTotalHarga);
            Controls.Add(cbStatus);
            Controls.Add(dataGridView1);
            Controls.Add(btnTambah);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnRefresh);
            Name = "reservasi";
            Text = "Form Reservasi";
            Load += reservasi_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

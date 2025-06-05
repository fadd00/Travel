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

            // cbPelanggan
            cbPelanggan.Location = new System.Drawing.Point(30, 30);
            cbPelanggan.Name = "cbPelanggan";
            cbPelanggan.Size = new System.Drawing.Size(200, 23);

            // cbJadwal
            cbJadwal.Location = new System.Drawing.Point(30, 70);
            cbJadwal.Name = "cbJadwal";
            cbJadwal.Size = new System.Drawing.Size(200, 23);

            // tJumlahTiket
            tJumlahTiket.Location = new System.Drawing.Point(30, 110);
            tJumlahTiket.Name = "tJumlahTiket";
            tJumlahTiket.Size = new System.Drawing.Size(200, 23);
            tJumlahTiket.PlaceholderText = "Jumlah Tiket";

            // tTotalHarga
            tTotalHarga.Location = new System.Drawing.Point(30, 150);
            tTotalHarga.Name = "tTotalHarga";
            tTotalHarga.Size = new System.Drawing.Size(200, 23);
            tTotalHarga.PlaceholderText = "Total Harga";

            // cbStatus
            cbStatus.Location = new System.Drawing.Point(30, 190);
            cbStatus.Name = "cbStatus";
            cbStatus.Size = new System.Drawing.Size(200, 23);
            cbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStatus.Items.AddRange(new object[] { "pending", "terbayar", "dibatalkan" });

            // dataGridView1
            dataGridView1.Location = new System.Drawing.Point(260, 30);
            dataGridView1.Size = new System.Drawing.Size(400, 250);
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.CellClick += dataGridView1_CellClick;

            // btnTambah
            btnTambah.Location = new System.Drawing.Point(30, 230);
            btnTambah.Size = new System.Drawing.Size(75, 23);
            btnTambah.Text = "Tambah";
            btnTambah.Click += btnTambah_Click;

            // btnUpdate
            btnUpdate.Location = new System.Drawing.Point(115, 230);
            btnUpdate.Size = new System.Drawing.Size(75, 23);
            btnUpdate.Text = "Update";
            btnUpdate.Click += btnUpdate_Click;

            // btnDelete
            btnDelete.Location = new System.Drawing.Point(200, 230);
            btnDelete.Size = new System.Drawing.Size(75, 23);
            btnDelete.Text = "Delete";
            btnDelete.Click += btnDelete_Click;

            // btnRefresh
            btnRefresh.Location = new System.Drawing.Point(30, 270);
            btnRefresh.Size = new System.Drawing.Size(245, 23);
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += btnRefresh_Click;

            // reservasi
            this.ClientSize = new System.Drawing.Size(700, 320);
            this.Controls.Add(cbPelanggan);
            this.Controls.Add(cbJadwal);
            this.Controls.Add(tJumlahTiket);
            this.Controls.Add(tTotalHarga);
            this.Controls.Add(cbStatus);
            this.Controls.Add(dataGridView1);
            this.Controls.Add(btnTambah);
            this.Controls.Add(btnUpdate);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnRefresh);
            this.Name = "reservasi";
            this.Text = "Form Reservasi";
            this.Load += reservasi_Load;
        }
    }
}

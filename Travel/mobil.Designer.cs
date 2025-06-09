namespace Travel
{
    partial class mobil
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox ttujuan;
        private System.Windows.Forms.DateTimePicker ttanggal;
        private System.Windows.Forms.TextBox twaktu;
        private System.Windows.Forms.TextBox tkapasitas;
        private System.Windows.Forms.TextBox tmerk;
        private System.Windows.Forms.TextBox tmodel;
        private System.Windows.Forms.TextBox tplat;
        private System.Windows.Forms.ComboBox tstatus;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox tharga; // Tambahan untuk harga
        private Button btnImport;

        private void InitializeComponent()
        {
            ttujuan = new TextBox();
            ttanggal = new DateTimePicker();
            twaktu = new TextBox();
            tkapasitas = new TextBox();
            tmerk = new TextBox();
            tmodel = new TextBox();
            tplat = new TextBox();
            tstatus = new ComboBox();
            dataGridView1 = new DataGridView();
            btnTambah = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();
            tharga = new TextBox();
            btnImport = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // ttujuan
            // 
            ttujuan.Location = new Point(27, 167);
            ttujuan.Name = "ttujuan";
            ttujuan.Size = new Size(200, 23);
            ttujuan.TabIndex = 0;
            ttujuan.Text = "tujuan";
            // 
            // ttanggal
            // 
            ttanggal.Format = DateTimePickerFormat.Short;
            ttanggal.Location = new Point(27, 53);
            ttanggal.Name = "ttanggal";
            ttanggal.Size = new Size(200, 23);
            ttanggal.TabIndex = 1;
            // 
            // twaktu
            // 
            twaktu.Location = new Point(27, 24);
            twaktu.Name = "twaktu";
            twaktu.Size = new Size(200, 23);
            twaktu.TabIndex = 2;
            twaktu.Text = "waktu";
            // 
            // tkapasitas
            // 
            tkapasitas.Location = new Point(27, 138);
            tkapasitas.Name = "tkapasitas";
            tkapasitas.Size = new Size(200, 23);
            tkapasitas.TabIndex = 3;
            tkapasitas.Text = "kapasitas";
            // 
            // tmerk
            // 
            tmerk.Location = new Point(27, 225);
            tmerk.Name = "tmerk";
            tmerk.Size = new Size(200, 23);
            tmerk.TabIndex = 4;
            tmerk.Text = "merek";
            // 
            // tmodel
            // 
            tmodel.Location = new Point(27, 82);
            tmodel.Name = "tmodel";
            tmodel.Size = new Size(200, 23);
            tmodel.TabIndex = 5;
            tmodel.Text = "model";
            // 
            // tplat
            // 
            tplat.Location = new Point(27, 196);
            tplat.Name = "tplat";
            tplat.Size = new Size(200, 23);
            tplat.TabIndex = 6;
            tplat.Text = "plat";
            // 
            // tstatus
            // 
            tstatus.DropDownStyle = ComboBoxStyle.DropDownList;
            tstatus.Items.AddRange(new object[] { "tersedia", "penuh", "batal" });
            tstatus.Location = new Point(345, 138);
            tstatus.Name = "tstatus";
            tstatus.Size = new Size(121, 23);
            tstatus.TabIndex = 7;
            // 
            // dataGridView1
            // 
            dataGridView1.Location = new Point(472, 12);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(240, 338);
            dataGridView1.TabIndex = 8;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // btnTambah
            // 
            btnTambah.Location = new Point(391, 108);
            btnTambah.Name = "btnTambah";
            btnTambah.Size = new Size(75, 23);
            btnTambah.TabIndex = 9;
            btnTambah.Text = "Tambah";
            btnTambah.Click += btnTambah_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(391, 70);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 10;
            btnUpdate.Text = "Update";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(391, 12);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 11;
            btnDelete.Text = "Delete";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(391, 41);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(75, 23);
            btnRefresh.TabIndex = 12;
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // tharga
            // 
            tharga.Location = new Point(27, 109);
            tharga.Name = "tharga";
            tharga.Size = new Size(200, 23);
            tharga.TabIndex = 13;
            tharga.Text = "harga";
            // 
            // btnImport
            // 
            btnImport.Location = new Point(375, 276);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(80, 30);
            btnImport.TabIndex = 14;
            btnImport.Text = "Import Excel";
            btnImport.Click += btnImport_Click;
            // 
            // mobil
            // 
            ClientSize = new Size(724, 376);
            Controls.Add(ttujuan);
            Controls.Add(ttanggal);
            Controls.Add(twaktu);
            Controls.Add(tkapasitas);
            Controls.Add(tmerk);
            Controls.Add(tmodel);
            Controls.Add(tplat);
            Controls.Add(tstatus);
            Controls.Add(tharga);
            Controls.Add(dataGridView1);
            Controls.Add(btnTambah);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnRefresh);
            Controls.Add(btnImport);
            Name = "mobil";
            Load += mobil_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }
    }
}
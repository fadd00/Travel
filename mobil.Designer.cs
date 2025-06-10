using System.Drawing; // Pastikan ini ada
using System.Windows.Forms; // Pastikan ini ada
using System; // Mungkin diperlukan untuk DateTimePicker

namespace Travel
{
    partial class mobil
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Deklarasi kontrol yang sesuai untuk Form mobil (data jadwal)
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
        private System.Windows.Forms.TextBox tharga;
        private Button btnImport;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ttujuan = new System.Windows.Forms.TextBox();
            this.ttanggal = new System.Windows.Forms.DateTimePicker();
            this.twaktu = new System.Windows.Forms.TextBox();
            this.tkapasitas = new System.Windows.Forms.TextBox();
            this.tmerk = new System.Windows.Forms.TextBox();
            this.tmodel = new System.Windows.Forms.TextBox();
            this.tplat = new System.Windows.Forms.TextBox();
            this.tstatus = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tharga = new System.Windows.Forms.TextBox();
            this.btnImport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ttujuan
            // 
            this.ttujuan.Location = new System.Drawing.Point(27, 167);
            this.ttujuan.Name = "ttujuan";
            this.ttujuan.Size = new System.Drawing.Size(200, 22);
            this.ttujuan.TabIndex = 0;
            this.ttujuan.Text = "tujuan";
            // 
            // ttanggal
            // 
            this.ttanggal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ttanggal.Location = new System.Drawing.Point(27, 53);
            this.ttanggal.Name = "ttanggal";
            this.ttanggal.Size = new System.Drawing.Size(200, 22);
            this.ttanggal.TabIndex = 1;
            // 
            // twaktu
            // 
            this.twaktu.Location = new System.Drawing.Point(27, 24);
            this.twaktu.Name = "twaktu";
            this.twaktu.Size = new System.Drawing.Size(200, 22);
            this.twaktu.TabIndex = 2;
            this.twaktu.Text = "waktu";
            // 
            // tkapasitas
            // 
            this.tkapasitas.Location = new System.Drawing.Point(27, 138);
            this.tkapasitas.Name = "tkapasitas";
            this.tkapasitas.Size = new System.Drawing.Size(200, 22);
            this.tkapasitas.TabIndex = 3;
            this.tkapasitas.Text = "kapasitas";
            // 
            // tmerk
            // 
            this.tmerk.Location = new System.Drawing.Point(27, 225);
            this.tmerk.Name = "tmerk";
            this.tmerk.Size = new System.Drawing.Size(200, 22);
            this.tmerk.TabIndex = 4;
            this.tmerk.Text = "merek";
            // 
            // tmodel
            // 
            this.tmodel.Location = new System.Drawing.Point(27, 82);
            this.tmodel.Name = "tmodel";
            this.tmodel.Size = new System.Drawing.Size(200, 22);
            this.tmodel.TabIndex = 5;
            this.tmodel.Text = "model";
            // 
            // tplat
            // 
            this.tplat.Location = new System.Drawing.Point(27, 196);
            this.tplat.Name = "tplat";
            this.tplat.Size = new System.Drawing.Size(200, 22);
            this.tplat.TabIndex = 6;
            this.tplat.Text = "plat";
            // 
            // tstatus
            // 
            this.tstatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tstatus.Items.AddRange(new object[] {
            "tersedia",
            "penuh",
            "batal"});
            this.tstatus.Location = new System.Drawing.Point(345, 138);
            this.tstatus.Name = "tstatus";
            this.tstatus.Size = new System.Drawing.Size(121, 24);
            this.tstatus.TabIndex = 7;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(472, 12);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(240, 338);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(391, 108);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(75, 23);
            this.btnTambah.TabIndex = 9;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(391, 70);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 10;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(391, 12);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(391, 41);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 12;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // tharga
            // 
            this.tharga.Location = new System.Drawing.Point(27, 109);
            this.tharga.Name = "tharga";
            this.tharga.Size = new System.Drawing.Size(200, 22);
            this.tharga.TabIndex = 13;
            this.tharga.Text = "harga";
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(375, 276);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(80, 30);
            this.btnImport.TabIndex = 14;
            this.btnImport.Text = "Import Excel";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // mobil
            // 
            this.ClientSize = new System.Drawing.Size(724, 376);
            this.Controls.Add(this.ttujuan);
            this.Controls.Add(this.ttanggal);
            this.Controls.Add(this.twaktu);
            this.Controls.Add(this.tkapasitas);
            this.Controls.Add(this.tmerk);
            this.Controls.Add(this.tmodel);
            this.Controls.Add(this.tplat);
            this.Controls.Add(this.tstatus);
            this.Controls.Add(this.tharga);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnImport);
            this.Name = "mobil";
            this.Text = "Form Jadwal Mobil";
            this.Load += new System.EventHandler(this.mobil_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
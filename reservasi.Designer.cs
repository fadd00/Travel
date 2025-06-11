using System.Drawing;
using System.Windows.Forms;

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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(reservasi));
            this.cbPelanggan = new System.Windows.Forms.ComboBox();
            this.cbJadwal = new System.Windows.Forms.ComboBox();
            this.tJumlahTiket = new System.Windows.Forms.TextBox();
            this.tTotalHarga = new System.Windows.Forms.TextBox();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbPelanggan
            // 
            this.cbPelanggan.FormattingEnabled = true;
            this.cbPelanggan.Location = new System.Drawing.Point(204, 75);
            this.cbPelanggan.Name = "cbPelanggan";
            this.cbPelanggan.Size = new System.Drawing.Size(154, 24);
            this.cbPelanggan.TabIndex = 0;
            this.cbPelanggan.SelectedIndexChanged += new System.EventHandler(this.cbPelanggan_SelectedIndexChanged);
            // 
            // cbJadwal
            // 
            this.cbJadwal.FormattingEnabled = true;
            this.cbJadwal.Location = new System.Drawing.Point(204, 115);
            this.cbJadwal.Name = "cbJadwal";
            this.cbJadwal.Size = new System.Drawing.Size(154, 24);
            this.cbJadwal.TabIndex = 1;
            // 
            // tJumlahTiket
            // 
            this.tJumlahTiket.Location = new System.Drawing.Point(204, 155);
            this.tJumlahTiket.Name = "tJumlahTiket";
            this.tJumlahTiket.Size = new System.Drawing.Size(154, 22);
            this.tJumlahTiket.TabIndex = 2;
            this.tJumlahTiket.Text = "Jumlah Tiket";
            // 
            // tTotalHarga
            // 
            this.tTotalHarga.Location = new System.Drawing.Point(204, 195);
            this.tTotalHarga.Name = "tTotalHarga";
            this.tTotalHarga.Size = new System.Drawing.Size(154, 22);
            this.tTotalHarga.TabIndex = 3;
            this.tTotalHarga.Text = "Total Harga";
            // 
            // cbStatus
            // 
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Items.AddRange(new object[] {
            "pending",
            "terbayar",
            "dibatalkan"});
            this.cbStatus.Location = new System.Drawing.Point(204, 235);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(154, 24);
            this.cbStatus.TabIndex = 4;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(379, 75);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(517, 274);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(113, 275);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(75, 31);
            this.btnTambah.TabIndex = 6;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(191, 275);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 31);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(283, 275);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 31);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Hapus";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(113, 312);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(245, 37);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "Segarkan Data";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Location = new System.Drawing.Point(113, 367);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(783, 174);
            this.reportViewer1.TabIndex = 10;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(110, 555);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(64, 16);
            this.lblMessage.TabIndex = 11;
            this.lblMessage.Text = "Message";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 16);
            this.label1.TabIndex = 12;
            this.label1.Text = "Nama Pelanggan";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "Tujuan";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(75, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "Jumlah Tiket";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(75, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 16);
            this.label4.TabIndex = 15;
            this.label4.Text = "Total Harga";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(75, 238);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 16);
            this.label5.TabIndex = 16;
            this.label5.Text = "Status";
            // 
            // reservasi
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1008, 603);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.tTotalHarga);
            this.Controls.Add(this.tJumlahTiket);
            this.Controls.Add(this.cbJadwal);
            this.Controls.Add(this.cbPelanggan);
            this.Name = "reservasi";
            this.Text = "Formulir Reservasi";
            this.Load += new System.EventHandler(this.reservasi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Label lblMessage;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
    }
}
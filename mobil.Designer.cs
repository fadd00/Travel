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
        private System.Windows.Forms.Button btnAnalisis;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mobil));
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
            this.btnAnalisis = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ttujuan
            // 
            this.ttujuan.Location = new System.Drawing.Point(189, 285);
            this.ttujuan.Multiline = true;
            this.ttujuan.Name = "ttujuan";
            this.ttujuan.Size = new System.Drawing.Size(220, 27);
            this.ttujuan.TabIndex = 0;
            this.ttujuan.Text = "tujuan";
            // 
            // ttanggal
            // 
            this.ttanggal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ttanggal.Location = new System.Drawing.Point(560, 163);
            this.ttanggal.Name = "ttanggal";
            this.ttanggal.Size = new System.Drawing.Size(314, 22);
            this.ttanggal.TabIndex = 1;
            // 
            // twaktu
            // 
            this.twaktu.Location = new System.Drawing.Point(189, 45);
            this.twaktu.Multiline = true;
            this.twaktu.Name = "twaktu";
            this.twaktu.Size = new System.Drawing.Size(220, 27);
            this.twaktu.TabIndex = 2;
            this.twaktu.Text = "waktu";
            // 
            // tkapasitas
            // 
            this.tkapasitas.Location = new System.Drawing.Point(189, 225);
            this.tkapasitas.Multiline = true;
            this.tkapasitas.Name = "tkapasitas";
            this.tkapasitas.Size = new System.Drawing.Size(220, 27);
            this.tkapasitas.TabIndex = 3;
            this.tkapasitas.Text = "kapasitas";
            // 
            // tmerk
            // 
            this.tmerk.Location = new System.Drawing.Point(560, 105);
            this.tmerk.Multiline = true;
            this.tmerk.Name = "tmerk";
            this.tmerk.Size = new System.Drawing.Size(314, 27);
            this.tmerk.TabIndex = 4;
            this.tmerk.Text = "merek";
            // 
            // tmodel
            // 
            this.tmodel.Location = new System.Drawing.Point(189, 105);
            this.tmodel.Multiline = true;
            this.tmodel.Name = "tmodel";
            this.tmodel.Size = new System.Drawing.Size(220, 27);
            this.tmodel.TabIndex = 5;
            this.tmodel.Text = "model";
            // 
            // tplat
            // 
            this.tplat.Location = new System.Drawing.Point(560, 45);
            this.tplat.Multiline = true;
            this.tplat.Name = "tplat";
            this.tplat.Size = new System.Drawing.Size(314, 27);
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
            this.tstatus.Location = new System.Drawing.Point(560, 227);
            this.tstatus.Name = "tstatus";
            this.tstatus.Size = new System.Drawing.Size(314, 24);
            this.tstatus.TabIndex = 7;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(131, 360);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(755, 187);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(438, 285);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(75, 30);
            this.btnTambah.TabIndex = 9;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(613, 285);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 30);
            this.btnUpdate.TabIndex = 10;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(703, 285);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(524, 284);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 30);
            this.btnRefresh.TabIndex = 12;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // tharga
            // 
            this.tharga.Location = new System.Drawing.Point(189, 165);
            this.tharga.Multiline = true;
            this.tharga.Name = "tharga";
            this.tharga.Size = new System.Drawing.Size(220, 27);
            this.tharga.TabIndex = 13;
            this.tharga.Text = "harga";
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(792, 285);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 30);
            this.btnImport.TabIndex = 14;
            this.btnImport.Text = "Import Excel";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnAnalisis
            // 
            this.btnAnalisis.Location = new System.Drawing.Point(878, 284);
            this.btnAnalisis.Name = "btnAnalisis";
            this.btnAnalisis.Size = new System.Drawing.Size(75, 30);
            this.btnAnalisis.TabIndex = 16;
            this.btnAnalisis.Text = "Analisis";
            this.btnAnalisis.UseVisualStyleBackColor = true;
            this.btnAnalisis.Click += new System.EventHandler(this.btnAnalisis_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(128, 550);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(64, 16);
            this.lblMessage.TabIndex = 17;
            this.lblMessage.Text = "Message";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "Waktu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(128, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 16);
            this.label2.TabIndex = 19;
            this.label2.Text = "Model";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(128, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "Harga";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(112, 230);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 16);
            this.label4.TabIndex = 21;
            this.label4.Text = "Kapasitas";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(129, 291);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 16);
            this.label5.TabIndex = 22;
            this.label5.Text = "Tujuan";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(455, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 16);
            this.label6.TabIndex = 23;
            this.label6.Text = "Plat Kendaraan";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(455, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 16);
            this.label7.TabIndex = 24;
            this.label7.Text = "Merek Mobil";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(455, 163);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 16);
            this.label8.TabIndex = 25;
            this.label8.Text = "Tanggal";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(455, 232);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 16);
            this.label9.TabIndex = 26;
            this.label9.Text = "Status";
            // 
            // mobil
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1008, 603);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMessage);
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
            this.Controls.Add(this.btnAnalisis);
            this.Name = "mobil";
            this.Text = "Form Jadwal Mobil";
            this.Load += new System.EventHandler(this.mobil_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblMessage;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
    }
}
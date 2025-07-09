using System.Drawing;
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
        private Button btnSelectAll; 
        private System.Windows.Forms.Button btnDeleteAll;


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
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ttujuan
            // 
            this.ttujuan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ttujuan.Location = new System.Drawing.Point(27, 275);
            this.ttujuan.Name = "ttujuan";
            this.ttujuan.Size = new System.Drawing.Size(200, 22);
            this.ttujuan.TabIndex = 0;
            // 
            // ttanggal
            // 
            this.ttanggal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ttanggal.Location = new System.Drawing.Point(27, 75);
            this.ttanggal.Name = "ttanggal";
            this.ttanggal.Size = new System.Drawing.Size(200, 22);
            this.ttanggal.TabIndex = 1;
            // 
            // twaktu
            // 
            this.twaktu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.twaktu.Location = new System.Drawing.Point(27, 25);
            this.twaktu.Name = "twaktu";
            this.twaktu.Size = new System.Drawing.Size(200, 22);
            this.twaktu.TabIndex = 2;
            // 
            // tkapasitas
            // 
            this.tkapasitas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tkapasitas.Location = new System.Drawing.Point(27, 225);
            this.tkapasitas.Name = "tkapasitas";
            this.tkapasitas.Size = new System.Drawing.Size(200, 22);
            this.tkapasitas.TabIndex = 3;
            // 
            // tmerk
            // 
            this.tmerk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tmerk.Location = new System.Drawing.Point(27, 375);
            this.tmerk.Name = "tmerk";
            this.tmerk.Size = new System.Drawing.Size(200, 22);
            this.tmerk.TabIndex = 4;
            // 
            // tmodel
            // 
            this.tmodel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tmodel.Location = new System.Drawing.Point(27, 125);
            this.tmodel.Name = "tmodel";
            this.tmodel.Size = new System.Drawing.Size(200, 22);
            this.tmodel.TabIndex = 5;
            // 
            // tplat
            // 
            this.tplat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tplat.Location = new System.Drawing.Point(27, 325);
            this.tplat.Name = "tplat";
            this.tplat.Size = new System.Drawing.Size(200, 22);
            this.tplat.TabIndex = 6;
            // 
            // tstatus
            // 
            this.tstatus.BackColor = System.Drawing.Color.Pink;
            this.tstatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tstatus.Items.AddRange(new object[] {
            "tersedia",
            "penuh",
            "batal"});
            this.tstatus.Location = new System.Drawing.Point(285, 273);
            this.tstatus.Name = "tstatus";
            this.tstatus.Size = new System.Drawing.Size(121, 24);
            this.tstatus.TabIndex = 7;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(432, 12);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(393, 441);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // btnTambah
            // 
            this.btnTambah.BackColor = System.Drawing.Color.Pink;
            this.btnTambah.Location = new System.Drawing.Point(306, 168);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(75, 23);
            this.btnTambah.TabIndex = 9;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = false;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.Pink;
            this.btnUpdate.Location = new System.Drawing.Point(306, 218);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 10;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Pink;
            this.btnDelete.Location = new System.Drawing.Point(306, 118);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Pink;
            this.btnRefresh.Location = new System.Drawing.Point(306, 67);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 12;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // tharga
            // 
            this.tharga.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tharga.Location = new System.Drawing.Point(27, 175);
            this.tharga.Name = "tharga";
            this.tharga.Size = new System.Drawing.Size(200, 22);
            this.tharga.TabIndex = 13;
            // 
            // btnImport
            // 
            this.btnImport.BackColor = System.Drawing.Color.Pink;
            this.btnImport.Location = new System.Drawing.Point(306, 378);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(80, 30);
            this.btnImport.TabIndex = 14;
            this.btnImport.Text = "Import Excel";
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.BackColor = System.Drawing.Color.Pink;
            this.btnSelectAll.Location = new System.Drawing.Point(286, 342);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(120, 30);
            this.btnSelectAll.TabIndex = 15;
            this.btnSelectAll.Text = "Analyze Query";
            this.btnSelectAll.UseVisualStyleBackColor = false;
            this.btnSelectAll.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.BackColor = System.Drawing.Color.Pink;
            this.btnDeleteAll.Location = new System.Drawing.Point(295, 313);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(100, 23);
            this.btnDeleteAll.TabIndex = 16;
            this.btnDeleteAll.Text = "Delete All";
            this.btnDeleteAll.UseVisualStyleBackColor = false;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(24, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "Waktu";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(24, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 16);
            this.label2.TabIndex = 18;
            this.label2.Text = "Tanggal";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(24, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 16);
            this.label3.TabIndex = 19;
            this.label3.Text = "Model";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(24, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 16);
            this.label4.TabIndex = 20;
            this.label4.Text = "Harga";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(24, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 16);
            this.label5.TabIndex = 21;
            this.label5.Text = "Kapasitas";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(24, 256);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 16);
            this.label6.TabIndex = 22;
            this.label6.Text = "Tujuan";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(24, 306);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 16);
            this.label7.TabIndex = 23;
            this.label7.Text = "Plat";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(24, 356);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 16);
            this.label8.TabIndex = 24;
            this.label8.Text = "Merek";
            // 
            // mobil
            // 
            this.BackgroundImage = global::Travel.Properties.Resources.Kumpulan_Background_Pastel_yang_Indah_dan_Elegan__1_;
            this.ClientSize = new System.Drawing.Size(837, 465);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDeleteAll);
            this.Controls.Add(this.btnSelectAll);
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

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
    }
}
namespace Travel
{
    partial class Admin
    {
        private System.Windows.Forms.TextBox tnama; // TextBox for Nama
        private System.Windows.Forms.TextBox tnohp; // TextBox for No hp
        private System.Windows.Forms.TextBox ttujuan; // TextBox for Tujuan
        private System.Windows.Forms.TextBox temail; // TextBox for Email
        private System.Windows.Forms.TextBox talamat; // TextBox for Alamat
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;

        private void InitializeComponent()
        {
            tnama = new TextBox();
            tnohp = new TextBox();
            ttujuan = new TextBox();
            temail = new TextBox();
            talamat = new TextBox();
            dataGridView1 = new DataGridView();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // tnama
            // 
            tnama.Location = new Point(57, 17);
            tnama.Name = "tnama";
            tnama.Size = new Size(329, 23);
            tnama.TabIndex = 0;
            // 
            // tnohp
            // 
            tnohp.Location = new Point(57, 47);
            tnohp.Name = "tnohp";
            tnohp.Size = new Size(329, 23);
            tnohp.TabIndex = 1;
            // 
            // ttujuan
            // 
            ttujuan.Location = new Point(57, 77);
            ttujuan.Name = "ttujuan";
            ttujuan.Size = new Size(329, 23);
            ttujuan.TabIndex = 2;
            // 
            // temail
            // 
            temail.Location = new Point(57, 107);
            temail.Name = "temail";
            temail.Size = new Size(329, 23);
            temail.TabIndex = 3;
            // 
            // talamat
            // 
            talamat.Location = new Point(57, 137);
            talamat.Name = "talamat";
            talamat.Size = new Size(329, 23);
            talamat.TabIndex = 4;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 178);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(495, 162);
            dataGridView1.TabIndex = 5;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(392, 12);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(115, 30);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnPesan_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(392, 52);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(115, 30);
            btnUpdate.TabIndex = 7;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(392, 92);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(115, 30);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(392, 132);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(115, 30);
            btnRefresh.TabIndex = 9;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 20);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 10;
            label1.Text = "Nama";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 140);
            label2.Name = "label2";
            label2.Size = new Size(45, 15);
            label2.TabIndex = 11;
            label2.Text = "Alamat";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 110);
            label3.Name = "label3";
            label3.Size = new Size(36, 15);
            label3.TabIndex = 12;
            label3.Text = "Email";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 80);
            label4.Name = "label4";
            label4.Size = new Size(43, 15);
            label4.TabIndex = 13;
            label4.Text = "Tujuan";
            label4.Click += label4_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 50);
            label5.Name = "label5";
            label5.Size = new Size(44, 15);
            label5.TabIndex = 14;
            label5.Text = "NO HP";
            // 
            // Admin
            // 
            ClientSize = new Size(529, 351);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(tnama);
            Controls.Add(tnohp);
            Controls.Add(ttujuan);
            Controls.Add(temail);
            Controls.Add(talamat);
            Controls.Add(dataGridView1);
            Controls.Add(btnAdd);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnRefresh);
            Name = "Admin";
            Text = "Travel Management";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
    }
}

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
            this.SuspendLayout(); 
            // 
            // btnAdmin
            // 
            this.btnAdmin.Location = new System.Drawing.Point(50, 40);
            this.btnAdmin.Name = "btnAdmin";
            this.btnAdmin.Size = new System.Drawing.Size(120, 40);
            this.btnAdmin.TabIndex = 0;
            this.btnAdmin.Text = "Admin";
            this.btnAdmin.UseVisualStyleBackColor = true;
            this.btnAdmin.Click += new System.EventHandler(this.btnAdmin_Click);
            // 
            // btnMobil
            // 
            this.btnMobil.Location = new System.Drawing.Point(200, 40);
            this.btnMobil.Name = "btnMobil";
            this.btnMobil.Size = new System.Drawing.Size(120, 40);
            this.btnMobil.TabIndex = 1;
            this.btnMobil.Text = "Mobil";
            this.btnMobil.UseVisualStyleBackColor = true;
            this.btnMobil.Click += new System.EventHandler(this.btnMobil_Click);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 121);
            this.Controls.Add(this.btnAdmin);
            this.Controls.Add(this.btnMobil);
            this.Name = "main";
            this.Text = "Main Menu";
            this.ResumeLayout(false);
        }
    }
}
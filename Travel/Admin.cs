using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Travel
{
    public partial class mobil : Form
    {
        static string connectionString = "Data Source=localhost;Initial Catalog=travel;Integrated Security=True";

        public mobil()
        {
            InitializeComponent();
        }

        private void mobil_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ClearForm()
        {
            ttujuan.Clear();
            ttanggal.Value = DateTime.Today;
            twaktu.Text = "";
            tharga.Text = "";
            tkapasitas.Text = "";
            tmerk.Text = "";
            tmodel.Text = "";
            tplat.Text = "";
            tstatus.SelectedIndex = 0;
            ttujuan.Focus();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM jadwal";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load data: " + ex.Message);
                }
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(ttujuan.Text))
            {
                MessageBox.Show("Tujuan wajib diisi!");
                ttujuan.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(twaktu.Text))
            {
                MessageBox.Show("Waktu wajib diisi!");
                twaktu.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(tharga.Text) || !decimal.TryParse(tharga.Text, out _))
            {
                MessageBox.Show("Harga wajib diisi dan harus angka desimal!");
                tharga.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(tkapasitas.Text) || !int.TryParse(tkapasitas.Text, out _))
            {
                MessageBox.Show("Kapasitas wajib diisi dan harus angka!");
                tkapasitas.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(tmerk.Text))
            {
                MessageBox.Show("Merk mobil wajib diisi!");
                tmerk.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(tmodel.Text))
            {
                MessageBox.Show("Model mobil wajib diisi!");
                tmodel.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(tplat.Text))
            {
                MessageBox.Show("Plat nomor wajib diisi!");
                tplat.Focus();
                return false;
            }
            if (tstatus.SelectedIndex < 0)
            {
                MessageBox.Show("Status wajib dipilih!");
                tstatus.Focus();
                return false;
            }
            return true;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO jadwal 
                            (tujuan, tanggal, waktu, harga, kapasitas, merk_mobil, model_mobil, plat_nomor, status)
                            VALUES (@tujuan, @tanggal, @waktu, @harga, @kapasitas, @merk, @model, @plat, @status)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tujuan", ttujuan.Text.Trim());
                    cmd.Parameters.AddWithValue("@tanggal", ttanggal.Value.Date);
                    cmd.Parameters.AddWithValue("@waktu", TimeSpan.Parse(twaktu.Text.Trim()));
                    cmd.Parameters.AddWithValue("@harga", decimal.Parse(tharga.Text.Trim()));
                    cmd.Parameters.AddWithValue("@kapasitas", int.Parse(tkapasitas.Text.Trim()));
                    cmd.Parameters.AddWithValue("@merk", tmerk.Text.Trim());
                    cmd.Parameters.AddWithValue("@model", tmodel.Text.Trim());
                    cmd.Parameters.AddWithValue("@plat", tplat.Text.Trim());
                    cmd.Parameters.AddWithValue("@status", tstatus.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data jadwal berhasil ditambah!");
                    ClearForm();
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menambah data: " + ex.Message);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang akan diupdate!");
                return;
            }
            if (!ValidateInput())
                return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"UPDATE jadwal SET 
                            tujuan=@tujuan, tanggal=@tanggal, waktu=@waktu, harga=@harga, kapasitas=@kapasitas, 
                            merk_mobil=@merk, model_mobil=@model, plat_nomor=@plat, status=@status
                            WHERE id_jadwal=@id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", dataGridView1.SelectedRows[0].Cells["id_jadwal"].Value);
                    cmd.Parameters.AddWithValue("@tujuan", ttujuan.Text.Trim());
                    cmd.Parameters.AddWithValue("@tanggal", ttanggal.Value.Date);
                    cmd.Parameters.AddWithValue("@waktu", TimeSpan.Parse(twaktu.Text.Trim()));
                    cmd.Parameters.AddWithValue("@harga", decimal.Parse(tharga.Text.Trim()));
                    cmd.Parameters.AddWithValue("@kapasitas", int.Parse(tkapasitas.Text.Trim()));
                    cmd.Parameters.AddWithValue("@merk", tmerk.Text.Trim());
                    cmd.Parameters.AddWithValue("@model", tmodel.Text.Trim());
                    cmd.Parameters.AddWithValue("@plat", tplat.Text.Trim());
                    cmd.Parameters.AddWithValue("@status", tstatus.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data jadwal berhasil diupdate!");
                    ClearForm();
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal update data: " + ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang akan dihapus!");
                return;
            }

            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM jadwal WHERE id_jadwal = @id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", dataGridView1.SelectedRows[0].Cells["id_jadwal"].Value);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data jadwal berhasil dihapus!");
                        ClearForm();
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal hapus data: " + ex.Message);
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
            MessageBox.Show("Data refreshed successfully!");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                ttujuan.Text = row.Cells["tujuan"].Value?.ToString();
                if (row.Cells["tanggal"].Value != null && DateTime.TryParse(row.Cells["tanggal"].Value.ToString(), out DateTime tgl))
                    ttanggal.Value = tgl;
                else
                    ttanggal.Value = DateTime.Today;
                twaktu.Text = row.Cells["waktu"].Value?.ToString();
                tharga.Text = row.Cells["harga"].Value?.ToString();
                tkapasitas.Text = row.Cells["kapasitas"].Value?.ToString();
                tmerk.Text = row.Cells["merk_mobil"].Value?.ToString();
                tmodel.Text = row.Cells["model_mobil"].Value?.ToString();
                tplat.Text = row.Cells["plat_nomor"].Value?.ToString();
                tstatus.SelectedItem = row.Cells["status"].Value?.ToString();
            }
        }
    }
}

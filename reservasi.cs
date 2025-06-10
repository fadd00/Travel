using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient; // Diubah dari MySql.Data.MySqlClient
using Microsoft.Reporting.WinForms; // Pastikan Anda memiliki referensi ke Microsoft.Reporting.WinForms 


namespace Travel
{
    public partial class reservasi : Form
    {
        // Ganti connectionString ini dengan yang sesuai untuk SQL Server Anda
        // Contoh: "Data Source=nama_server;Initial Catalog=nama_database;Integrated Security=True;" (Windows Authentication)
        // Contoh: "Data Source=nama_server;Initial Catalog=nama_database;User ID=user_sql;Password=password_sql;" (SQL Server Authentication)
        static string connectionString = "Data Source=AKMAL;Initial Catalog = Travel; Integrated Security = True";
        // Jika Anda menggunakan SQL Server Authentication, gunakan format ini:
        // static string connectionString = "Data Source=localhost;Initial Catalog=travel;User ID=your_sql_user;Password=your_sql_password;";


        public reservasi()
        {
            InitializeComponent();
        }

        private void reservasi_Load(object sender, EventArgs e)
        {
            LoadPelanggan();
            LoadJadwal();
            LoadData();
            ClearForm();
            SetupReportViewer();
            this.reportViewer1.RefreshReport();
        }

        private void SetupReportViewer()
        {
            string query = @"
            SELECT
                r.id_reservasi,
                p.nama AS NamaPelanggan,
                j.tujuan,
                j.tanggal,
                j.waktu,
                r.jumlah_tiket,
                r.total_harga
            FROM
                reservasi AS r
            INNER JOIN
                pelanggan AS p ON r.id_pelanggan = p.id_pelanggan
            INNER JOIN
                jadwal AS j ON r.id_jadwal = j.id_jadwal
            ORDER BY
                r.id_reservasi;"; // Ganti dengan query yang sesuai untuk laporan Anda

            DataTable dt = new DataTable();
            
            using (SqlConnection conn = new SqlConnection(connectionString)) // Diubah ke SqlConnection
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn); // Diubah ke SqlDataAdapter
                da.Fill(dt);
            }

            ReportDataSource rds = new ReportDataSource("TravelDataSet", dt); // Pastikan DataSet1 sesuai dengan yang ada di RDLC Anda
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.LocalReport.ReportPath = @"A:\\1. UMY\\1. KULIAH\\Semester 4 (Feb 2025-\\PABD\\Travel\\TravelReport.rdlc"; // Pastikan path ini sesuai dengan lokasi file RDLC Anda
            reportViewer1.RefreshReport();
            
        }

        private void LoadPelanggan()
        {
            cbPelanggan.Items.Clear();
            using (var conn = new SqlConnection(connectionString)) // Diubah ke SqlConnection
            {
                try
                {
                    conn.Open();
                    var cmd = new SqlCommand("SELECT id_pelanggan, nama FROM pelanggan", conn); // Diubah ke SqlCommand
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cbPelanggan.Items.Add(new ComboBoxItem(reader["nama"].ToString(), reader["id_pelanggan"].ToString()));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load pelanggan data: " + ex.Message);
                }
            }
        }

        private void LoadJadwal()
        {
            cbJadwal.Items.Clear();
            using (var conn = new SqlConnection(connectionString)) // Diubah ke SqlConnection
            {
                try
                {
                    conn.Open();
                    var cmd = new SqlCommand("SELECT id_jadwal, tujuan FROM jadwal", conn); // Diubah ke SqlCommand
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cbJadwal.Items.Add(new ComboBoxItem(reader["tujuan"].ToString(), reader["id_jadwal"].ToString()));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load jadwal data: " + ex.Message);
                }
            }
        }

        private void LoadData()
        {
            using (var conn = new SqlConnection(connectionString)) // Diubah ke SqlConnection
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM reservasi";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn); // Diubah ke SqlDataAdapter
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

        private void ClearForm()
        {
            cbPelanggan.SelectedIndex = -1;
            cbJadwal.SelectedIndex = -1;
            tJumlahTiket.Text = "";
            tTotalHarga.Text = "";
            // Pastikan cbStatus memiliki item yang diisi, jika tidak, ini bisa menyebabkan error
            // Misalnya: cbStatus.Items.AddRange(new string[] { "Pending", "Confirmed", "Cancelled" }); di constructor atau Load event
            if (cbStatus.Items.Count > 0)
            {
                cbStatus.SelectedIndex = 0; // Mengatur ke item pertama jika ada
            }
            else
            {
                // Jika cbStatus kosong, tambahkan item default atau biarkan -1
                // cbStatus.Items.Add("Pending");
                // cbStatus.SelectedIndex = 0;
            }

        }

        private bool ValidateInput()
        {
            if (cbPelanggan.SelectedIndex < 0)
            {
                MessageBox.Show("Pilih pelanggan!");
                cbPelanggan.Focus();
                return false;
            }
            if (cbJadwal.SelectedIndex < 0)
            {
                MessageBox.Show("Pilih jadwal!");
                cbJadwal.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(tJumlahTiket.Text) || !int.TryParse(tJumlahTiket.Text, out _))
            {
                MessageBox.Show("Jumlah tiket wajib diisi dan harus angka!");
                tJumlahTiket.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(tTotalHarga.Text) || !decimal.TryParse(tTotalHarga.Text, out _))
            {
                MessageBox.Show("Total harga wajib diisi dan harus angka desimal!");
                tTotalHarga.Focus();
                return false;
            }
            if (cbStatus.SelectedIndex < 0)
            {
                MessageBox.Show("Status wajib dipilih!");
                cbStatus.Focus();
                return false;
            }
            return true;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            using (var conn = new SqlConnection(connectionString)) // Diubah ke SqlConnection
            {
                try
                {
                    conn.Open();
                    // Untuk SQL Server, @parameter adalah standar
                    string query = @"INSERT INTO reservasi (id_pelanggan, id_jadwal, jumlah_tiket, total_harga, status)
                                     VALUES (@id_pelanggan, @id_jadwal, @jumlah_tiket, @total_harga, @status)";
                    var cmd = new SqlCommand(query, conn); // Diubah ke SqlCommand
                    cmd.Parameters.AddWithValue("@id_pelanggan", ((ComboBoxItem)cbPelanggan.SelectedItem).Value);
                    cmd.Parameters.AddWithValue("@id_jadwal", ((ComboBoxItem)cbJadwal.SelectedItem).Value);
                    cmd.Parameters.AddWithValue("@jumlah_tiket", int.Parse(tJumlahTiket.Text.Trim()));
                    cmd.Parameters.AddWithValue("@total_harga", decimal.Parse(tTotalHarga.Text.Trim()));
                    cmd.Parameters.AddWithValue("@status", cbStatus.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data reservasi berhasil ditambah!");
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

            using (var conn = new SqlConnection(connectionString)) // Diubah ke SqlConnection
            {
                try
                {
                    conn.Open();
                    // Untuk SQL Server, @parameter adalah standar
                    string query = @"UPDATE reservasi SET id_pelanggan=@id_pelanggan, id_jadwal=@id_jadwal, jumlah_tiket=@jumlah_tiket, 
                                     total_harga=@total_harga, status=@status WHERE id_reservasi=@id";
                    var cmd = new SqlCommand(query, conn); // Diubah ke SqlCommand
                    cmd.Parameters.AddWithValue("@id", dataGridView1.SelectedRows[0].Cells["id_reservasi"].Value);
                    cmd.Parameters.AddWithValue("@id_pelanggan", ((ComboBoxItem)cbPelanggan.SelectedItem).Value);
                    cmd.Parameters.AddWithValue("@id_jadwal", ((ComboBoxItem)cbJadwal.SelectedItem).Value);
                    cmd.Parameters.AddWithValue("@jumlah_tiket", int.Parse(tJumlahTiket.Text.Trim()));
                    cmd.Parameters.AddWithValue("@total_harga", decimal.Parse(tTotalHarga.Text.Trim()));
                    cmd.Parameters.AddWithValue("@status", cbStatus.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data reservasi berhasil diupdate!");
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
                using (var conn = new SqlConnection(connectionString)) // Diubah ke SqlConnection
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM reservasi WHERE id_reservasi = @id";
                        var cmd = new SqlCommand(query, conn); // Diubah ke SqlCommand
                        cmd.Parameters.AddWithValue("@id", dataGridView1.SelectedRows[0].Cells["id_reservasi"].Value);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data reservasi berhasil dihapus!");
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
                // Set pelanggan
                foreach (ComboBoxItem item in cbPelanggan.Items)
                {
                    if (item.Value == row.Cells["id_pelanggan"].Value.ToString())
                    {
                        cbPelanggan.SelectedItem = item;
                        break;
                    }
                }
                // Set jadwal
                foreach (ComboBoxItem item in cbJadwal.Items)
                {
                    if (item.Value == row.Cells["id_jadwal"].Value.ToString())
                    {
                        cbJadwal.SelectedItem = item;
                        break;
                    }
                }
                tJumlahTiket.Text = row.Cells["jumlah_tiket"].Value?.ToString();
                tTotalHarga.Text = row.Cells["total_harga"].Value?.ToString();

                // Pastikan cbStatus memiliki item yang sesuai
                string statusValue = row.Cells["status"].Value?.ToString();
                if (statusValue != null)
                {
                    int index = cbStatus.FindStringExact(statusValue);
                    if (index != -1)
                    {
                        cbStatus.SelectedIndex = index;
                    }
                    else
                    {
                        // Handle case where status from DB is not in ComboBox items
                        MessageBox.Show($"Status '{statusValue}' from database is not found in the status dropdown list. Please check your data or dropdown items.");
                        cbStatus.SelectedIndex = -1;
                    }
                }
                else
                {
                    cbStatus.SelectedIndex = -1; // Clear selection if status is null
                }
            }
        }

        private void cbPelanggan_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tidak ada perubahan yang diperlukan di sini untuk SQL Server
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }

    // Helper class for ComboBox value display (tidak ada perubahan)
    public class ComboBoxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public ComboBoxItem(string text, string value)
        {
            Text = text;
            Value = value;
        }
        public override string ToString()
        {
            return Text;
        }
    }
}
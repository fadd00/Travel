using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Travel
{
    public partial class reservasi : Form
    {
        static string connectionString = "Server=localhost;Database=travel;Uid=root";

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
        }

        private void LoadPelanggan()
        {
            cbPelanggan.Items.Clear();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT id_pelanggan, nama FROM pelanggan", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cbPelanggan.Items.Add(new ComboBoxItem(reader["nama"].ToString(), reader["id_pelanggan"].ToString()));
                    }
                }
            }
        }

        private void LoadJadwal()
        {
            cbJadwal.Items.Clear();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT id_jadwal, tujuan FROM jadwal", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cbJadwal.Items.Add(new ComboBoxItem(reader["tujuan"].ToString(), reader["id_jadwal"].ToString()));
                    }
                }
            }
        }

        private void LoadData()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM reservasi";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
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
            cbStatus.SelectedIndex = 0;
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

            using (var conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO reservasi (id_pelanggan, id_jadwal, jumlah_tiket, total_harga, status)
                                     VALUES (@id_pelanggan, @id_jadwal, @jumlah_tiket, @total_harga, @status)";
                    var cmd = new MySqlCommand(query, conn);
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

            using (var conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"UPDATE reservasi SET id_pelanggan=@id_pelanggan, id_jadwal=@id_jadwal, jumlah_tiket=@jumlah_tiket, 
                                     total_harga=@total_harga, status=@status WHERE id_reservasi=@id";
                    var cmd = new MySqlCommand(query, conn);
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
                using (var conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM reservasi WHERE id_reservasi = @id";
                        var cmd = new MySqlCommand(query, conn);
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
                cbStatus.SelectedItem = row.Cells["status"].Value?.ToString();
            }
        }
    }

    // Helper class for ComboBox value display
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

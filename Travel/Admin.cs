using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
namespace Travel
{
    public partial class Admin : Form
    {
        // Connection string - Using MySQL
        static string connectionString = "Server=localhost;Database=travel;Uid=root";

        public Admin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ClearForm()
        {
            tnama.Clear();
            tnohp.Clear();
            temail.Clear();
            talamat.Clear();
            tnama.Focus();
        }

        private void LoadData()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM pelanggan";
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

        // Input validation helper
        private bool ValidateInput()
        {
            // Nama wajib, hanya huruf dan spasi
            if (string.IsNullOrWhiteSpace(tnama.Text) || !Regex.IsMatch(tnama.Text, @"^[a-zA-Z\s]+$"))
            {
                MessageBox.Show("Nama wajib diisi dan hanya boleh huruf dan spasi!");
                tnama.Focus();
                return false;
            }

            // No HP wajib, hanya angka, minimal 10 digit
            if (string.IsNullOrWhiteSpace(tnohp.Text) || !Regex.IsMatch(tnohp.Text, @"^\d{10,}$"))
            {
                MessageBox.Show("No HP wajib diisi, hanya angka, minimal 10 digit!");
                tnohp.Focus();
                return false;
            }

            // Email wajib, format valid
            if (string.IsNullOrWhiteSpace(temail.Text) || !Regex.IsMatch(temail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email wajib diisi dan harus format email yang valid!");
                temail.Focus();
                return false;
            }

            // Alamat wajib
            if (string.IsNullOrWhiteSpace(talamat.Text))
            {
                MessageBox.Show("Alamat wajib diisi!");
                talamat.Focus();
                return false;
            }

            return true;
        }

        // Add new customer data
        private void btnPesan_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "INSERT INTO pelanggan (nama, telepon, email, alamat) " +
                                   "VALUES (@nama, @telepon, @email, @alamat)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nama", tnama.Text.Trim());
                    cmd.Parameters.AddWithValue("@telepon", tnohp.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", temail.Text.Trim());
                    cmd.Parameters.AddWithValue("@alamat", talamat.Text.Trim());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Customer data saved successfully!");
                    ClearForm();
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to save data: " + ex.Message);
                }
            }
        }

        // Update existing customer data
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang akan diupdate!");
                return;
            }

            if (!ValidateInput())
                return;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "UPDATE pelanggan SET nama = @nama, telepon = @telepon, email = @email, alamat = @alamat " +
                                   "WHERE id_pelanggan = @id_pelanggan";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id_pelanggan", dataGridView1.SelectedRows[0].Cells["id_pelanggan"].Value);
                    cmd.Parameters.AddWithValue("@nama", tnama.Text.Trim());
                    cmd.Parameters.AddWithValue("@telepon", tnohp.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", temail.Text.Trim());
                    cmd.Parameters.AddWithValue("@alamat", talamat.Text.Trim());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Customer data updated successfully!");
                    ClearForm();
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to update data: " + ex.Message);
                }
            }
        }

        // Delete customer data
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
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM pelanggan WHERE id_pelanggan = @id_pelanggan";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id_pelanggan", dataGridView1.SelectedRows[0].Cells["id_pelanggan"].Value);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Customer data deleted successfully!");
                        ClearForm();
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to delete data: " + ex.Message);
                    }
                }
            }
        }

        // Refresh the data grid
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
            MessageBox.Show("Data refreshed successfully!");
        }

        // Handle cell click in DataGridView to populate form fields
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                tnama.Text = row.Cells["nama"].Value?.ToString();
                tnohp.Text = row.Cells["telepon"].Value?.ToString();
                ttujuan.Text = row.Cells["tujuan"].Value?.ToString();
                temail.Text = row.Cells["email"].Value?.ToString();
                talamat.Text = row.Cells["alamat"].Value?.ToString();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}

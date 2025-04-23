using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Travel
{
    public partial class Admin : Form
    {
        // Connection string - Using SQL Server
        static string connectionString = "Data Source=FADD00;Database=Travel;Integrated Security=True";

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
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM pelanggan";
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

        // Add new customer data
        private void btnPesan_Click(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrEmpty(tnama.Text))
            {
                MessageBox.Show("Nama tidak boleh kosong!");
                return;
            }

            if (string.IsNullOrEmpty(tnohp.Text))
            {
                MessageBox.Show("No hp tidak boleh kosong!");
                return;
            }

            if (string.IsNullOrEmpty(temail.Text))
            {
                MessageBox.Show("Email tidak boleh kosong!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "INSERT INTO pelanggan (nama, telepon, email, alamat) " +
                                   "VALUES (@nama, @telepon, @email, @alamat)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nama", tnama.Text);
                    cmd.Parameters.AddWithValue("@telepon", tnohp.Text);
                    cmd.Parameters.AddWithValue("@email", temail.Text);
                    cmd.Parameters.AddWithValue("@alamat", talamat.Text);
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

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "UPDATE pelanggan SET nama = @nama, telepon = @telepon, email = @email, alamat = @alamat " +
                                   "WHERE id_pelanggan = @id_pelanggan";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id_pelanggan", dataGridView1.SelectedRows[0].Cells["id_pelanggan"].Value);
                    cmd.Parameters.AddWithValue("@nama", tnama.Text);
                    cmd.Parameters.AddWithValue("@telepon", tnohp.Text);
                    cmd.Parameters.AddWithValue("@email", temail.Text);
                    cmd.Parameters.AddWithValue("@alamat", talamat.Text);
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
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM pelanggan WHERE id_pelanggan = @id_pelanggan";
                        SqlCommand cmd = new SqlCommand(query, conn);
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

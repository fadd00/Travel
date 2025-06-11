using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Runtime.Caching; // Tambahkan ini jika Anda berencana menggunakan cache
using System.Text; // Tambahkan ini jika Anda berencana menggunakan StringBuilder
using System.Xml; // Tambahkan ini jika Anda berencana menggunakan XmlDocument
using System.Diagnostics; // Tambahkan ini jika Anda berencana menggunakan Debug/Trace

namespace Travel
{
    public partial class Admin : Form
    {
        // Connection string - Using SQL Server
        // You'll need to adjust "YourSqlServerName" and "YourDatabaseName"
        static string connectionString = "Data Source=AKMAL;Initial Catalog = Travel; Integrated Security = True";
        // Alternative for SQL Server Authentication:
        // static string connectionString = "Data Source=YourSqlServerName;Initial Catalog=YourDatabaseName;User ID=YourUser;Password=YourPassword;";

        // Tambahkan inisialisasi MemoryCache jika Anda berencana menggunakannya di form Admin
        // private readonly MemoryCache _cache = MemoryCache.Default;
        // private readonly CacheItemPolicy _policy = new CacheItemPolicy
        // {
        //     AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
        // };
        // private const string CacheKey = "PelangganData";


        public Admin()
        {
            InitializeComponent();
            // Inisialisasi lblMessage di sini jika diperlukan default text
            // lblMessage.Text = "Form siap digunakan.";
            // lblMessage.ForeColor = System.Drawing.Color.Black;
        }

        private void Admin_Load(object sender, EventArgs e) // Mengganti Form1_Load menjadi Admin_Load
        {
            LoadData();
            ClearForm();
        }

        private void ClearForm()
        {
            tnama.Clear();
            tnohp.Clear();
            temail.Clear();
            talamat.Clear();
            tnama.Focus();
            lblMessage.Text = "Formulir dikosongkan.";
            lblMessage.ForeColor = System.Drawing.Color.Black; // Reset warna pesan
        }

        private void LoadData()
        {

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM pelanggan";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(dt);
                    // _cache.Add(CacheKey, dt, _policy); // Tambahkan ke cache jika menggunakan cache
                    lblMessage.Text = "Data berhasil dimuat dari database.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Terjadi error database saat memuat data: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal memuat data karena error database.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi error tak terduga saat memuat data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal memuat data karena error tak terduga.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            // } // Akhir dari else blok cache
            dataGridView1.DataSource = dt;
        }

        // Input validation helper
        private bool ValidateInput()
        {
            // Nama wajib, hanya huruf dan spasi
            if (string.IsNullOrWhiteSpace(tnama.Text) || !Regex.IsMatch(tnama.Text, @"^[a-zA-Z\s]+$"))
            {
                MessageBox.Show("Nama wajib diisi dan hanya boleh huruf dan spasi!", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tnama.Focus();
                return false;
            }

            // No HP wajib, hanya angka, minimal 10 digit
            if (string.IsNullOrWhiteSpace(tnohp.Text) || !Regex.IsMatch(tnohp.Text, @"^\d{10,}$"))
            {
                MessageBox.Show("No HP wajib diisi, hanya angka, minimal 10 digit!", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tnohp.Focus();
                return false;
            }

            // Email wajib, format valid
            if (string.IsNullOrWhiteSpace(temail.Text) || !Regex.IsMatch(temail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email wajib diisi dan harus format email yang valid!", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                temail.Focus();
                return false;
            }

            // Alamat wajib
            if (string.IsNullOrWhiteSpace(talamat.Text))
            {
                MessageBox.Show("Alamat wajib diisi!", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open(); // Buka koneksi sebelum memulai transaksi
                SqlTransaction transaction = conn.BeginTransaction(); // Mulai transaksi

                try
                {
                    string query = "INSERT INTO pelanggan (nama, telepon, email, alamat) VALUES (@nama, @telepon, @email, @alamat)";
                    SqlCommand cmd = new SqlCommand(query, conn, transaction); // Kaitkan command dengan transaksi
                    cmd.Parameters.AddWithValue("@nama", tnama.Text.Trim());
                    cmd.Parameters.AddWithValue("@telepon", tnohp.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", temail.Text.Trim());
                    cmd.Parameters.AddWithValue("@alamat", talamat.Text.Trim());
                    cmd.ExecuteNonQuery();

                    transaction.Commit(); // Commit transaksi jika semua berhasil
                    MessageBox.Show("Data pelanggan berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblMessage.Text = "Data pelanggan baru berhasil ditambahkan!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    // _cache.Remove(CacheKey); // Hapus cache jika menggunakan cache
                    ClearForm();
                    LoadData();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback(); // Rollback pada error SQL
                    MessageBox.Show("Terjadi error database saat menyimpan data: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal menyimpan data karena error database (rollback).";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Rollback pada error lainnya
                    MessageBox.Show("Terjadi error tak terduga saat menyimpan data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal menyimpan data karena error tak terduga (rollback).";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close(); // Pastikan koneksi ditutup
                    }
                }
            }
        }

        // Update existing customer data
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang akan diupdate!", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput())
                return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open(); // Buka koneksi sebelum memulai transaksi
                SqlTransaction transaction = conn.BeginTransaction(); // Mulai transaksi

                try
                {
                    string query = "UPDATE pelanggan SET nama = @nama, telepon = @telepon, email = @email, alamat = @alamat WHERE id_pelanggan = @id_pelanggan";
                    SqlCommand cmd = new SqlCommand(query, conn, transaction); // Kaitkan command dengan transaksi
                    cmd.Parameters.AddWithValue("@id_pelanggan", dataGridView1.SelectedRows[0].Cells["id_pelanggan"].Value);
                    cmd.Parameters.AddWithValue("@nama", tnama.Text.Trim());
                    cmd.Parameters.AddWithValue("@telepon", tnohp.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", temail.Text.Trim());
                    cmd.Parameters.AddWithValue("@alamat", talamat.Text.Trim());
                    cmd.ExecuteNonQuery();

                    transaction.Commit(); // Commit transaksi jika semua berhasil
                    MessageBox.Show("Data pelanggan berhasil diupdate!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblMessage.Text = "Data pelanggan berhasil diperbarui!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    // _cache.Remove(CacheKey); // Hapus cache jika menggunakan cache
                    ClearForm();
                    LoadData();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback(); // Rollback pada error SQL
                    MessageBox.Show("Terjadi error database saat memperbarui data: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal memperbarui data karena error database (rollback).";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Rollback pada error lainnya
                    MessageBox.Show("Terjadi error tak terduga saat memperbarui data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal memperbarui data karena error tak terduga (rollback).";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close(); // Pastikan koneksi ditutup
                    }
                }
            }
        }

        // Delete customer data
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang akan dihapus!", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open(); // Buka koneksi sebelum memulai transaksi
                    SqlTransaction transaction = conn.BeginTransaction(); // Mulai transaksi

                    try
                    {
                        string query = "DELETE FROM pelanggan WHERE id_pelanggan = @id_pelanggan";
                        SqlCommand cmd = new SqlCommand(query, conn, transaction); // Kaitkan command dengan transaksi
                        cmd.Parameters.AddWithValue("@id_pelanggan", dataGridView1.SelectedRows[0].Cells["id_pelanggan"].Value);
                        cmd.ExecuteNonQuery();

                        transaction.Commit(); // Commit transaksi jika semua berhasil
                        MessageBox.Show("Data pelanggan berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblMessage.Text = "Data pelanggan berhasil dihapus!";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        // _cache.Remove(CacheKey); // Hapus cache jika menggunakan cache
                        ClearForm();
                        LoadData();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback(); // Rollback pada error SQL
                        MessageBox.Show("Terjadi error database saat menghapus data: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblMessage.Text = "Gagal menghapus data karena error database (rollback).";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // Rollback pada error lainnya
                        MessageBox.Show("Terjadi error tak terduga saat menghapus data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblMessage.Text = "Gagal menghapus data karena error tak terduga (rollback).";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    finally
                    {
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close(); // Pastikan koneksi ditutup
                        }
                    }
                }
            }
        }

        // Refresh the data grid
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // _cache.Remove(CacheKey); // Hapus cache untuk memaksa pemuatan ulang dari DB jika menggunakan cache
            LoadData();
            ClearForm();
            MessageBox.Show("Data berhasil di-refresh!", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblMessage.Text = "Data telah di-refresh.";
            lblMessage.ForeColor = System.Drawing.Color.Black;
        }

        // Handle cell click in DataGridView to populate form fields
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                tnama.Text = row.Cells["nama"].Value?.ToString();
                tnohp.Text = row.Cells["telepon"].Value?.ToString();
                temail.Text = row.Cells["email"].Value?.ToString();
                talamat.Text = row.Cells["alamat"].Value?.ToString();

                lblMessage.Text = "Data dipilih untuk diedit.";
                lblMessage.ForeColor = System.Drawing.Color.Blue;
            }
        }
    }
}
﻿using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient; // Changed from MySql.Data.MySqlClient
using System.Text.RegularExpressions;
using System.IO;

namespace Travel
{
    public partial class Admin : Form
    {
        Koneksi kn = new Koneksi();
        string strKonek; 

        // You'll need to adjust "YourSqlServerName" and "YourDatabaseName"
        static string connectionString = "Data Source=AKMAL;Initial Catalog = Travel; Integrated Security = True";
        // Alternative for SQL Server Authentication:
        // static string connectionString = "Server=YourSqlServerName;Database=travel;User ID=YourUsername;Password=YourPassword;";

        public Admin()
        {
            InitializeComponent();
            strKonek = kn.connectionString(); // Get connection string from Koneksi class
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
            
            {
                try
                {
                    SqlConnection conn = new SqlConnection(strKonek); // Initialize connection with the connection string
                    conn.Open();
                    string query = "SELECT * FROM pelanggan";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn); // Using SqlDataAdapter
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

            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    // Cek duplikat data
                    string checkQuery = @"SELECT COUNT(*) FROM pelanggan 
                                  WHERE nama = @nama AND telepon = @telepon AND email = @email AND alamat = @alamat";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn, transaction);
                    checkCmd.Parameters.AddWithValue("@nama", tnama.Text.Trim());
                    checkCmd.Parameters.AddWithValue("@telepon", tnohp.Text.Trim());
                    checkCmd.Parameters.AddWithValue("@email", temail.Text.Trim());
                    checkCmd.Parameters.AddWithValue("@alamat", talamat.Text.Trim());

                    int exists = (int)checkCmd.ExecuteScalar();
                    if (exists > 0)
                    {
                        MessageBox.Show("Data pelanggan sudah ada, tidak boleh duplikat!");
                        transaction.Rollback();
                        return;
                    }

                    // Jika tidak duplikat, lakukan insert
                    string query = "INSERT INTO pelanggan (nama, telepon, email, alamat) " +
                                   "VALUES (@nama, @telepon, @email, @alamat)";
                    SqlCommand cmd = new SqlCommand(query, conn, transaction);
                    cmd.Parameters.AddWithValue("@nama", tnama.Text.Trim());
                    cmd.Parameters.AddWithValue("@telepon", tnohp.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", temail.Text.Trim());
                    cmd.Parameters.AddWithValue("@alamat", talamat.Text.Trim());
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Customer data saved successfully!");
                    ClearForm();
                    LoadData();
                }
                catch (Exception ex)
                {
                    try { transaction.Rollback(); } catch { }
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

            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    string query = "UPDATE pelanggan SET nama = @nama, telepon = @telepon, email = @email, alamat = @alamat " +
                                   "WHERE id_pelanggan = @id_pelanggan";
                    SqlCommand cmd = new SqlCommand(query, conn, transaction);
                    cmd.Parameters.AddWithValue("@id_pelanggan", dataGridView1.SelectedRows[0].Cells["id_pelanggan"].Value);
                    cmd.Parameters.AddWithValue("@nama", tnama.Text.Trim());
                    cmd.Parameters.AddWithValue("@telepon", tnohp.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", temail.Text.Trim());
                    cmd.Parameters.AddWithValue("@alamat", talamat.Text.Trim());
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Customer data updated successfully!");
                    ClearForm();
                    LoadData();
                }
                catch (Exception ex)
                {
                    try { transaction.Rollback(); } catch { }
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
                using (SqlConnection conn = new SqlConnection(kn.connectionString())) // Using SqlConnection
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM pelanggan WHERE id_pelanggan = @id_pelanggan";
                        SqlCommand cmd = new SqlCommand(query, conn); // Using SqlCommand
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
                temail.Text = row.Cells["email"].Value?.ToString();
                talamat.Text = row.Cells["alamat"].Value?.ToString();
            }
        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }
    }
}
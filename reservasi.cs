using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;

namespace Travel
{
    public partial class reservasi : Form
    {

        static string connectionString = "Data Source=AKMAL;Initial Catalog = Travel; Integrated Security = True";

        public reservasi()
        {
            InitializeComponent();

            if (cbStatus.Items.Count == 0)
            {
                cbStatus.Items.AddRange(new string[] { "Pending", "Confirmed", "Cancelled" });
            }
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
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("TampilReservasi", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Terjadi error database saat memuat data laporan: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal memuat data laporan karena error database.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi error tak terduga saat memuat data laporan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal memuat data laporan karena error tak terduga.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }

            ReportDataSource rds = new ReportDataSource("TravelDataSet", dt);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.LocalReport.ReportPath = @"A:\\1. UMY\\1. KULIAH\\Semester 4 (Feb 2025-\\PABD\\Travel-bp\\TravelReport.rdlc";
            reportViewer1.RefreshReport();
        }

        private void LoadPelanggan()
        {
            cbPelanggan.Items.Clear();

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    var cmd = new SqlCommand("SELECT id_pelanggan, nama FROM pelanggan", conn);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cbPelanggan.Items.Add(new ComboBoxItem(reader["nama"].ToString(), reader["id_pelanggan"].ToString()));
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Terjadi error database saat memuat data pelanggan: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal memuat data pelanggan karena error database.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi error tak terduga saat memuat data pelanggan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal memuat data pelanggan karena error tak terduga.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void LoadJadwal()
        {
            cbJadwal.Items.Clear();
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    var cmd = new SqlCommand("SELECT id_jadwal, tujuan FROM jadwal", conn);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cbJadwal.Items.Add(new ComboBoxItem(reader["tujuan"].ToString(), reader["id_jadwal"].ToString()));
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Terjadi error database saat memuat data jadwal: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal memuat data jadwal karena error database.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi error tak terduga saat memuat data jadwal: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal memuat data jadwal karena error tak terduga.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void LoadData()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("TampilReservasi", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    lblMessage.Text = "Data berhasil dimuat.";
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
        }

        private void ClearForm()
        {
            cbPelanggan.SelectedIndex = -1;
            cbJadwal.SelectedIndex = -1;
            tJumlahTiket.Text = "";
            tTotalHarga.Text = "";
            if (cbStatus.Items.Count > 0)
            {
                cbStatus.SelectedIndex = 0;
            }
            lblMessage.Text = "Formulir dikosongkan.";
            lblMessage.ForeColor = System.Drawing.Color.Black;
        }

        private bool ValidateInput()
        {
            if (cbPelanggan.SelectedIndex < 0)
            {
                MessageBox.Show("Pilih pelanggan!", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbPelanggan.Focus();
                return false;
            }
            if (cbJadwal.SelectedIndex < 0)
            {
                MessageBox.Show("Pilih jadwal!", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbJadwal.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(tJumlahTiket.Text) || !int.TryParse(tJumlahTiket.Text, out _))
            {
                MessageBox.Show("Jumlah tiket wajib diisi dan harus angka!", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tJumlahTiket.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(tTotalHarga.Text) || !decimal.TryParse(tTotalHarga.Text, out _))
            {
                MessageBox.Show("Total harga wajib diisi dan harus angka desimal!", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tTotalHarga.Focus();
                return false;
            }
            if (cbStatus.SelectedIndex < 0)
            {
                MessageBox.Show("Status wajib dipilih!", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbStatus.Focus();
                return false;
            }
            return true;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    SqlCommand cmd = new SqlCommand("TambahReservasi", conn, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_pelanggan", ((ComboBoxItem)cbPelanggan.SelectedItem).Value);
                    cmd.Parameters.AddWithValue("@id_jadwal", ((ComboBoxItem)cbJadwal.SelectedItem).Value);
                    cmd.Parameters.AddWithValue("@jumlah_tiket", int.Parse(tJumlahTiket.Text.Trim()));
                    cmd.Parameters.AddWithValue("@total_harga", decimal.Parse(tTotalHarga.Text.Trim()));
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Data reservasi berhasil ditambah!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblMessage.Text = "Reservasi baru berhasil ditambahkan!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    ClearForm();
                    LoadData();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Terjadi error database saat menambah data: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal menambah data karena error database (rollback).";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Terjadi error tak terduga saat menambah data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal menambah data karena error tak terduga (rollback).";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang akan diupdate!", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInput())
                return;

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    SqlCommand cmd = new SqlCommand("UpdateReservasi", conn, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_reservasi", dataGridView1.SelectedRows[0].Cells["id_reservasi"].Value);
                    cmd.Parameters.AddWithValue("@jumlah_tiket", int.Parse(tJumlahTiket.Text.Trim()));
                    cmd.Parameters.AddWithValue("@total_harga", decimal.Parse(tTotalHarga.Text.Trim()));
                    cmd.Parameters.AddWithValue("@status", cbStatus.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Data reservasi berhasil diupdate!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblMessage.Text = "Data reservasi berhasil diperbarui!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    ClearForm();
                    LoadData();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Terjadi error database saat memperbarui data: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal memperbarui data karena error database (rollback).";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Terjadi error tak terduga saat memperbarui data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal memperbarui data karena error tak terduga (rollback).";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang akan dihapus!", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        SqlCommand cmd = new SqlCommand("HapusReservasi", conn, transaction);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_reservasi", dataGridView1.SelectedRows[0].Cells["id_reservasi"].Value);
                        cmd.ExecuteNonQuery();

                        transaction.Commit();
                        MessageBox.Show("Data reservasi berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblMessage.Text = "Data reservasi berhasil dihapus!";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        ClearForm();
                        LoadData();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Terjadi error database saat menghapus data: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblMessage.Text = "Gagal menghapus data karena error database (rollback).";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Terjadi error tak terduga saat menghapus data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblMessage.Text = "Gagal menghapus data karena error tak terduga (rollback).";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    finally
                    {
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
            MessageBox.Show("Data berhasil di-refresh!", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblMessage.Text = "Data telah di-refresh.";
            lblMessage.ForeColor = System.Drawing.Color.Black;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                foreach (ComboBoxItem item in cbPelanggan.Items)
                {
                    if (item.Value == row.Cells["id_pelanggan"].Value.ToString())
                    {
                        cbPelanggan.SelectedItem = item;
                        break;
                    }
                }
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
                        MessageBox.Show($"Status '{statusValue}' dari database tidak ditemukan di daftar dropdown status. Silakan periksa data atau item dropdown Anda.", "Ketidakcocokan Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cbStatus.SelectedIndex = -1;
                    }
                }
                else
                {
                    cbStatus.SelectedIndex = -1;
                }
                lblMessage.Text = "Data dipilih untuk diedit.";
                lblMessage.ForeColor = System.Drawing.Color.Blue;
            }
        }

        private void cbPelanggan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }

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
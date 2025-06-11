using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;

namespace Travel
{
    public partial class reservasi : Form
    {
        // Connection string to the SQL Server database.
        static string connectionString = "Data Source=AKMAL;Initial Catalog = Travel; Integrated Security = True";

        public reservasi()
        {
            InitializeComponent();
            // Populates the status ComboBox if it's empty.
            if (cbStatus.Items.Count == 0)
            {
                cbStatus.Items.AddRange(new string[] { "Pending", "Confirmed", "Cancelled" });
            }
        }

        private void reservasi_Load(object sender, EventArgs e)
        {
            // Loads initial data for ComboBoxes.
            LoadPelanggan();
            LoadJadwal();
            // Loads reservation data into the DataGridView.
            LoadData();
            // Clears the input form fields.
            ClearForm();
            // Sets up the report viewer with reservation data.
            SetupReportViewer();
            // Refreshes the report viewer to display the data.
            this.reportViewer1.RefreshReport();
        }

        // Configures and loads data into the ReportViewer.
        private void SetupReportViewer()
        {
            DataTable dt = new DataTable();
            // Establishes a connection to the database.
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    // Opens the database connection.
                    conn.Open();
                    // Creates a SqlCommand to execute the "TampilReservasi" stored procedure.
                    SqlCommand cmd = new SqlCommand("TampilReservasi", conn);
                    // Specifies that the command is a stored procedure.
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Uses SqlDataAdapter to fill the DataTable with data from the stored procedure.
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                // Handles SQL-specific errors.
                catch (SqlException ex)
                {
                    MessageBox.Show("Terjadi error database saat memuat data laporan: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal memuat data laporan karena error database.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                // Handles general errors.
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi error tak terduga saat memuat data laporan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal memuat data laporan karena error tak terduga.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }

            // Creates a ReportDataSource using the filled DataTable.
            // "TravelDataSet" must match the DataSet name in your RDLC report definition.
            ReportDataSource rds = new ReportDataSource("TravelDataSet", dt);
            reportViewer1.LocalReport.DataSources.Clear();
            // Adds the ReportDataSource to the local report.
            reportViewer1.LocalReport.DataSources.Add(rds);
            // Sets the path to the RDLC report file.
            reportViewer1.LocalReport.ReportPath = @"A:\\1. UMY\\1. KULIAH\\Semester 4 (Feb 2025-\\PABD\\Travel-bp\\TravelReport.rdlc";
            // Refreshes the report viewer to display the loaded data.
            reportViewer1.RefreshReport();
        }

        // Loads customer data into the pelanggan ComboBox.
        private void LoadPelanggan()
        {
            cbPelanggan.Items.Clear();
            // Establishes a connection to the database.
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    // Opens the database connection.
                    conn.Open();
                    // Creates a SqlCommand to select customer ID and name.
                    // This method uses a direct SQL query, not a stored procedure.
                    var cmd = new SqlCommand("SELECT id_pelanggan, nama FROM pelanggan", conn);
                    // Executes the command and reads the data.
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Adds each customer as a ComboBoxItem to the ComboBox.
                            cbPelanggan.Items.Add(new ComboBoxItem(reader["nama"].ToString(), reader["id_pelanggan"].ToString()));
                        }
                    }
                }
                // Handles SQL-specific errors.
                catch (SqlException ex)
                {
                    MessageBox.Show("Terjadi error database saat memuat data pelanggan: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal memuat data pelanggan karena error database.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                // Handles general errors.
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi error tak terduga saat memuat data pelanggan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal memuat data pelanggan karena error tak terduga.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        // Loads schedule data into the jadwal ComboBox.
        private void LoadJadwal()
        {
            cbJadwal.Items.Clear();
            // Establishes a connection to the database.
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    // Opens the database connection.
                    conn.Open();
                    // Creates a SqlCommand to select schedule ID and destination.
                    // This method uses a direct SQL query, not a stored procedure.
                    var cmd = new SqlCommand("SELECT id_jadwal, tujuan FROM jadwal", conn);
                    // Executes the command and reads the data.
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Adds each schedule as a ComboBoxItem to the ComboBox.
                            cbJadwal.Items.Add(new ComboBoxItem(reader["tujuan"].ToString(), reader["id_jadwal"].ToString()));
                        }
                    }
                }
                // Handles SQL-specific errors.
                catch (SqlException ex)
                {
                    MessageBox.Show("Terjadi error database saat memuat data jadwal: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal memuat data jadwal karena error database.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                // Handles general errors.
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi error tak terduga saat memuat data jadwal: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal memuat data jadwal karena error tak terduga.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        // Loads reservation data from the database into the DataGridView.
        private void LoadData()
        {
            // Establishes a connection to the database.
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    // Opens the database connection.
                    conn.Open();
                    // Creates a SqlCommand to execute the "TampilReservasi" stored procedure.
                    SqlCommand cmd = new SqlCommand("TampilReservasi", conn);
                    // Specifies that the command is a stored procedure.
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Uses SqlDataAdapter to fill a DataTable with data from the stored procedure.
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    // Sets the DataTable as the DataSource for the DataGridView.
                    dataGridView1.DataSource = dt;
                    lblMessage.Text = "Data berhasil dimuat.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                // Handles SQL-specific errors.
                catch (SqlException ex)
                {
                    MessageBox.Show("Terjadi error database saat memuat data: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal memuat data karena error database.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                // Handles general errors.
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi error tak terduga saat memuat data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal memuat data karena error tak terduga.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        // Clears all input fields on the form.
        private void ClearForm()
        {
            cbPelanggan.SelectedIndex = -1;
            cbJadwal.SelectedIndex = -1;
            tJumlahTiket.Text = "";
            tTotalHarga.Text = "";
            if (cbStatus.Items.Count > 0)
            {
                cbStatus.SelectedIndex = 0; // Default to "Pending" or the first item.
            }
            lblMessage.Text = "Formulir dikosongkan.";
            lblMessage.ForeColor = System.Drawing.Color.Black;
        }

        // Validates the input fields before submitting data.
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

        // Handles the click event for the "Tambah" (Add) button.
        private void btnTambah_Click(object sender, EventArgs e)
        {
            // Validates input before proceeding.
            if (!ValidateInput())
                return;

            // Establishes a connection to the database.
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Begins a database transaction to ensure atomicity.
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Creates a SqlCommand to execute the "TambahReservasi" stored procedure.
                    SqlCommand cmd = new SqlCommand("TambahReservasi", conn, transaction);
                    // Specifies that the command is a stored procedure.
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Adds parameters to the stored procedure.
                    cmd.Parameters.AddWithValue("@id_pelanggan", ((ComboBoxItem)cbPelanggan.SelectedItem).Value);
                    cmd.Parameters.AddWithValue("@id_jadwal", ((ComboBoxItem)cbJadwal.SelectedItem).Value);
                    cmd.Parameters.AddWithValue("@jumlah_tiket", int.Parse(tJumlahTiket.Text.Trim()));
                    cmd.Parameters.AddWithValue("@total_harga", decimal.Parse(tTotalHarga.Text.Trim()));
                    // The "TambahReservasi" stored procedure might not require a status parameter if it has a default.
                    // If status needs to be passed, the SP needs to be updated to accept @status.setup
                    // cmd.Parameters.AddWithValue("@status", cbStatus.SelectedItem.ToString()); // Uncomment if SP accepts status.

                    // Executes the stored procedure.
                    cmd.ExecuteNonQuery();

                    // Commits the transaction if the command executes successfully.
                    transaction.Commit();
                    MessageBox.Show("Data reservasi berhasil ditambah!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblMessage.Text = "Reservasi baru berhasil ditambahkan!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    // Clears the form and reloads data.
                    ClearForm();
                    LoadData();
                }
                // Handles SQL-specific errors during the transaction.
                catch (SqlException ex)
                {
                    // Rolls back the transaction in case of a database error.
                    transaction.Rollback();
                    MessageBox.Show("Terjadi error database saat menambah data: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal menambah data karena error database (rollback).";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                // Handles general errors during the transaction.
                catch (Exception ex)
                {
                    // Rolls back the transaction in case of an unexpected error.
                    transaction.Rollback();
                    MessageBox.Show("Terjadi error tak terduga saat menambah data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal menambah data karena error tak terduga (rollback).";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                finally
                {
                    // Ensures the database connection is closed.
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
        }

        // Handles the click event for the "Update" button.
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Checks if a row is selected in the DataGridView.
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang akan diupdate!", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Validates input before proceeding.
            if (!ValidateInput())
                return;

            // Establishes a connection to the database.
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Begins a database transaction.
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Creates a SqlCommand to execute the "UpdateReservasi" stored procedure.
                    SqlCommand cmd = new SqlCommand("UpdateReservasi", conn, transaction);
                    // Specifies that the command is a stored procedure.
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Adds parameters to the stored procedure.
                    cmd.Parameters.AddWithValue("@id_reservasi", dataGridView1.SelectedRows[0].Cells["id_reservasi"].Value);
                    cmd.Parameters.AddWithValue("@jumlah_tiket", int.Parse(tJumlahTiket.Text.Trim()));
                    cmd.Parameters.AddWithValue("@total_harga", decimal.Parse(tTotalHarga.Text.Trim()));
                    cmd.Parameters.AddWithValue("@status", cbStatus.SelectedItem.ToString());
                    // The "UpdateReservasi" SP might not allow changing id_pelanggan or id_jadwal.
                    // If these fields need to be updatable, the SP must be modified.

                    // Executes the stored procedure.
                    cmd.ExecuteNonQuery();

                    // Commits the transaction if the command executes successfully.
                    transaction.Commit();
                    MessageBox.Show("Data reservasi berhasil diupdate!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblMessage.Text = "Data reservasi berhasil diperbarui!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    // Clears the form and reloads data.
                    ClearForm();
                    LoadData();
                }
                // Handles SQL-specific errors during the transaction.
                catch (SqlException ex)
                {
                    // Rolls back the transaction in case of a database error.
                    transaction.Rollback();
                    MessageBox.Show("Terjadi error database saat memperbarui data: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal memperbarui data karena error database (rollback).";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                // Handles general errors during the transaction.
                catch (Exception ex)
                {
                    // Rolls back the transaction in case of an unexpected error.
                    transaction.Rollback();
                    MessageBox.Show("Terjadi error tak terduga saat memperbarui data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal memperbarui data karena error tak terduga (rollback).";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                finally
                {
                    // Ensures the database connection is closed.
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
        }

        // Handles the click event for the "Delete" button.
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Checks if a row is selected in the DataGridView.
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang akan dihapus!", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirms the delete operation with the user.
            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Establishes a connection to the database.
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Begins a database transaction.
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // Creates a SqlCommand to execute the "HapusReservasi" stored procedure.
                        SqlCommand cmd = new SqlCommand("HapusReservasi", conn, transaction);
                        // Specifies that the command is a stored procedure.
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Adds the ID of the reservation to be deleted as a parameter.
                        cmd.Parameters.AddWithValue("@id_reservasi", dataGridView1.SelectedRows[0].Cells["id_reservasi"].Value);
                        // Executes the stored procedure.
                        cmd.ExecuteNonQuery();

                        // Commits the transaction if the command executes successfully.
                        transaction.Commit();
                        MessageBox.Show("Data reservasi berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblMessage.Text = "Data reservasi berhasil dihapus!";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        // Clears the form and reloads data.
                        ClearForm();
                        LoadData();
                    }
                    // Handles SQL-specific errors during the transaction.
                    catch (SqlException ex)
                    {
                        // Rolls back the transaction in case of a database error.
                        transaction.Rollback();
                        MessageBox.Show("Terjadi error database saat menghapus data: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblMessage.Text = "Gagal menghapus data karena error database (rollback).";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    // Handles general errors during the transaction.
                    catch (Exception ex)
                    {
                        // Rolls back the transaction in case of an unexpected error.
                        transaction.Rollback();
                        MessageBox.Show("Terjadi error tak terduga saat menghapus data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblMessage.Text = "Gagal menghapus data karena error tak terduga (rollback).";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    finally
                    {
                        // Ensures the database connection is closed.
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }

        // Handles the click event for the "Refresh" button.
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Reloads data into the DataGridView.
            LoadData();
            // Clears the input form.
            ClearForm();
            MessageBox.Show("Data berhasil di-refresh!", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblMessage.Text = "Data telah di-refresh.";
            lblMessage.ForeColor = System.Drawing.Color.Black;
        }

        // Handles the cell click event in the DataGridView to populate form fields for editing.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensures a valid row is clicked.
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Populates the pelanggan ComboBox.
                // Iterates through ComboBox items to find a match with the 'id_pelanggan' from the selected row.
                foreach (ComboBoxItem item in cbPelanggan.Items)
                {
                    if (item.Value == row.Cells["id_pelanggan"].Value.ToString())
                    {
                        cbPelanggan.SelectedItem = item;
                        break;
                    }
                }
                // Populates the jadwal ComboBox.
                // Iterates through ComboBox items to find a match with the 'id_jadwal' from the selected row.
                foreach (ComboBoxItem item in cbJadwal.Items)
                {
                    if (item.Value == row.Cells["id_jadwal"].Value.ToString())
                    {
                        cbJadwal.SelectedItem = item;
                        break;
                    }
                }
                // Populates the text fields with data from the selected row.
                tJumlahTiket.Text = row.Cells["jumlah_tiket"].Value?.ToString();
                tTotalHarga.Text = row.Cells["total_harga"].Value?.ToString();

                // Populates the status ComboBox.
                string statusValue = row.Cells["status"].Value?.ToString();
                if (statusValue != null)
                {
                    // Finds the index of the status string in the ComboBox items.
                    int index = cbStatus.FindStringExact(statusValue);
                    if (index != -1)
                    {
                        cbStatus.SelectedIndex = index;
                    }
                    else
                    {
                        // Handles cases where the status from the database doesn't match any ComboBox item.
                        MessageBox.Show($"Status '{statusValue}' dari database tidak ditemukan di daftar dropdown status. Silakan periksa data atau item dropdown Anda.", "Ketidakcocokan Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cbStatus.SelectedIndex = -1; // No selection or default.
                    }
                }
                else
                {
                    cbStatus.SelectedIndex = -1; // No status value.
                }
                lblMessage.Text = "Data dipilih untuk diedit.";
                lblMessage.ForeColor = System.Drawing.Color.Blue;
            }
        }

        // Event handler for Pelanggan ComboBox selection change (currently empty).
        private void cbPelanggan_SelectedIndexChanged(object sender, EventArgs e)
        {
            // This method can be used to implement logic when the selected customer changes.
        }

        // Event handler for ReportViewer load (currently empty).
        private void reportViewer1_Load(object sender, EventArgs e)
        {
            // This method can be used to perform actions when the ReportViewer control is loaded.
        }
    }

    // Helper class to store display text and underlying value for ComboBox items.
    public class ComboBoxItem
    {
        public string Text { get; set; }  // Text displayed in the ComboBox.
        public string Value { get; set; } // Actual value associated with the item (e.g., an ID).

        public ComboBoxItem(string text, string value)
        {
            Text = text;
            Value = value;
        }

        // Overrides ToString() to display the Text property in the ComboBox.
        public override string ToString()
        {
            return Text;
        }
    }
}
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Runtime.Caching;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace Travel
{
    public partial class mobil : Form
    {
        static string connectionString = "Data Source=AKMAL;Initial Catalog=Travel;Integrated Security=True;";
        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
        };
        private const string CacheKey = "JadwalData";

        public mobil()
        {
            InitializeComponent();
            // Initialize tstatus items if not done in designer
            if (tstatus.Items.Count == 0)
            {
                tstatus.Items.AddRange(new string[] { "tersedia", "penuh", "batal" });
            }
        }

        private void mobil_Load(object sender, EventArgs e)
        {
            // Call method to ensure indexes exist when the form loads
            EnsureIndexes();
            LoadData();
            ClearForm();
        }

        private void ClearForm()
        {
            ttujuan.Clear();
            ttanggal.Value = DateTime.Today;
            twaktu.Text = ""; // Assuming twaktu is a MaskedTextBox or similar for TIME format HH:mm
            tharga.Text = "";
            tkapasitas.Text = "";
            tmerk.Text = "";
            tmodel.Text = "";
            tplat.Text = "";
            // Ensure tstatus has items before attempting to set SelectedIndex
            if (tstatus.Items.Count > 0)
            {
                tstatus.SelectedIndex = 0;
            }
            ttujuan.Focus();
            lblMessage.Text = "Formulir dikosongkan.";
            lblMessage.ForeColor = System.Drawing.Color.Black; // Reset message color
        }

        private void LoadData()
        {
            DataTable dt;
            if (_cache.Contains(CacheKey))
            {
                dt = _cache.Get(CacheKey) as DataTable;
                lblMessage.Text = "Data dimuat dari cache.";
                lblMessage.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "SELECT * FROM jadwal";
                        SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                        adapter.Fill(dt);
                        _cache.Add(CacheKey, dt, _policy);
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
            }
            dataGridView1.DataSource = dt;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(ttujuan.Text)) { MessageBox.Show("Tujuan wajib diisi!", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning); ttujuan.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(twaktu.Text)) { MessageBox.Show("Waktu wajib diisi!", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning); twaktu.Focus(); return false; }
            if (!TimeSpan.TryParse(twaktu.Text, out _)) { MessageBox.Show("Format waktu tidak valid! Gunakan HH:mm", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning); twaktu.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(tharga.Text) || !decimal.TryParse(tharga.Text, out _)) { MessageBox.Show("Harga wajib diisi dan harus angka desimal!", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning); tharga.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(tkapasitas.Text) || !int.TryParse(tkapasitas.Text, out _)) { MessageBox.Show("Kapasitas wajib diisi dan harus angka!", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning); tkapasitas.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(tmerk.Text)) { MessageBox.Show("Merk mobil wajib diisi!", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning); tmerk.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(tmodel.Text)) { MessageBox.Show("Model mobil wajib diisi!", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning); tmodel.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(tplat.Text)) { MessageBox.Show("Plat nomor wajib diisi!", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning); tplat.Focus(); return false; }
            if (tstatus.SelectedIndex < 0) { MessageBox.Show("Status wajib dipilih!", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning); tstatus.Focus(); return false; }
            return true;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    string query = @"INSERT INTO jadwal (tujuan, tanggal, waktu, harga, kapasitas, merk_mobil, model_mobil, plat_nomor, status) VALUES (@tujuan, @tanggal, @waktu, @harga, @kapasitas, @merk, @model, @plat, @status)";
                    SqlCommand cmd = new SqlCommand(query, conn, transaction);
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

                    transaction.Commit();
                    MessageBox.Show("Data jadwal berhasil ditambah!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblMessage.Text = "Jadwal baru berhasil ditambahkan!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    _cache.Remove(CacheKey);
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
            if (dataGridView1.SelectedRows.Count == 0) { MessageBox.Show("Pilih data yang akan diupdate!", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (!ValidateInput()) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    string query = @"UPDATE jadwal SET tujuan=@tujuan, tanggal=@tanggal, waktu=@waktu, harga=@harga, kapasitas=@kapasitas, merk_mobil=@merk, model_mobil=@model, plat_nomor=@plat, status=@status WHERE id_jadwal=@id";
                    SqlCommand cmd = new SqlCommand(query, conn, transaction);
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

                    transaction.Commit();
                    MessageBox.Show("Data jadwal berhasil diupdate!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblMessage.Text = "Data jadwal berhasil diperbarui!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    _cache.Remove(CacheKey);
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
            if (dataGridView1.SelectedRows.Count == 0) { MessageBox.Show("Pilih data yang akan dihapus!", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        string query = "DELETE FROM jadwal WHERE id_jadwal = @id";
                        SqlCommand cmd = new SqlCommand(query, conn, transaction);
                        cmd.Parameters.AddWithValue("@id", dataGridView1.SelectedRows[0].Cells["id_jadwal"].Value);
                        cmd.ExecuteNonQuery();

                        transaction.Commit();
                        MessageBox.Show("Data jadwal berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblMessage.Text = "Data jadwal berhasil dihapus!";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        _cache.Remove(CacheKey);
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
            _cache.Remove(CacheKey);
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
                ttujuan.Text = row.Cells["tujuan"].Value?.ToString();
                if (row.Cells["tanggal"].Value != null && DateTime.TryParse(row.Cells["tanggal"].Value.ToString(), out DateTime tgl))
                    ttanggal.Value = tgl;
                else
                    ttanggal.Value = DateTime.Today;

                object waktuValue = row.Cells["waktu"].Value;
                if (waktuValue is TimeSpan ts)
                {
                    twaktu.Text = ts.ToString(@"hh\:mm");
                }
                else if (waktuValue != null && TimeSpan.TryParse(waktuValue.ToString(), out TimeSpan parsedTs))
                {
                    twaktu.Text = parsedTs.ToString(@"hh\:mm");
                }
                else
                {
                    twaktu.Text = waktuValue?.ToString() ?? string.Empty;
                }

                tharga.Text = row.Cells["harga"].Value?.ToString();
                tkapasitas.Text = row.Cells["kapasitas"].Value?.ToString();
                tmerk.Text = row.Cells["merk_mobil"].Value?.ToString();
                tmodel.Text = row.Cells["model_mobil"].Value?.ToString();
                tplat.Text = row.Cells["plat_nomor"].Value?.ToString();

                string statusValue = row.Cells["status"].Value?.ToString();
                if (statusValue != null)
                {
                    int index = tstatus.FindStringExact(statusValue);
                    if (index != -1)
                    {
                        tstatus.SelectedIndex = index;
                    }
                    else
                    {
                        MessageBox.Show($"Status '{statusValue}' dari database tidak ditemukan di daftar dropdown status. Silakan periksa data atau item dropdown Anda.", "Ketidakcocokan Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tstatus.SelectedIndex = -1;
                    }
                }
                else
                {
                    tstatus.SelectedIndex = -1;
                }
                lblMessage.Text = "Data dipilih untuk diedit.";
                lblMessage.ForeColor = System.Drawing.Color.Blue;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Title = "Pilih File Excel";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        PreviewData(ofd.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error membuka file: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblMessage.Text = $"Gagal membuka file: {ex.Message}";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void PreviewData(string filePath)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("tujuan", typeof(string));
                dt.Columns.Add("tanggal", typeof(DateTime));
                dt.Columns.Add("waktu", typeof(string));
                dt.Columns.Add("harga", typeof(decimal));
                dt.Columns.Add("kapasitas", typeof(int));
                dt.Columns.Add("merk_mobil", typeof(string));
                dt.Columns.Add("model_mobil", typeof(string));
                dt.Columns.Add("plat_nomor", typeof(string));
                dt.Columns.Add("status", typeof(string));

                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook workbook;
                    if (Path.GetExtension(filePath).ToLower() == ".xlsx") { workbook = new XSSFWorkbook(fs); }
                    else { throw new Exception("Format file tidak didukung. Gunakan .xlsx"); }

                    ISheet sheet = workbook.GetSheetAt(0);
                    // Start from row 1 to skip header (assuming header is in row 0)
                    for (int row = 1; row <= sheet.LastRowNum; row++)
                    {
                        IRow excelRow = sheet.GetRow(row);
                        if (excelRow != null)
                        {
                            DataRow dataRow = dt.NewRow();
                            dataRow["tujuan"] = excelRow.GetCell(0)?.ToString() ?? string.Empty;
                            // NPOI reads dates as numeric, need to convert from numeric date to DateTime
                            if (excelRow.GetCell(1) != null && excelRow.GetCell(1).CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(excelRow.GetCell(1)))
                            {
                                dataRow["tanggal"] = excelRow.GetCell(1).DateCellValue;
                            }
                            else if (DateTime.TryParse(excelRow.GetCell(1)?.ToString(), out var tgl))
                            {
                                dataRow["tanggal"] = tgl;
                            }
                            else
                            {
                                dataRow["tanggal"] = DateTime.Today;
                            }

                            dataRow["waktu"] = excelRow.GetCell(2)?.ToString() ?? string.Empty;
                            dataRow["harga"] = decimal.TryParse(excelRow.GetCell(3)?.ToString(), out var harga) ? harga : 0;
                            dataRow["kapasitas"] = int.TryParse(excelRow.GetCell(4)?.ToString(), out var kapasitas) ? kapasitas : 0;
                            dataRow["merk_mobil"] = excelRow.GetCell(5)?.ToString() ?? string.Empty;
                            dataRow["model_mobil"] = excelRow.GetCell(6)?.ToString() ?? string.Empty;
                            dataRow["plat_nomor"] = excelRow.GetCell(7)?.ToString() ?? string.Empty;
                            dataRow["status"] = excelRow.GetCell(8)?.ToString() ?? string.Empty;
                            dt.Rows.Add(dataRow);
                        }
                    }
                }

                PreviewForm previewForm = new PreviewForm(dt, connectionString);
                if (previewForm.ShowDialog() == DialogResult.OK)
                {
                    _cache.Remove(CacheKey);
                    LoadData();
                    MessageBox.Show("Data jadwal berhasil diimport!", "Import Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblMessage.Text = "Data jadwal berhasil diimport!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMessage.Text = "Import data dibatalkan.";
                    lblMessage.ForeColor = System.Drawing.Color.Orange;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error membaca file: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblMessage.Text = $"Gagal membaca file untuk preview: {ex.Message}";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void ShowQueryStatistics(string sqlQuery)
        {
            var resultBuilder = new StringBuilder();
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    // Capture messages from SET STATISTICS
                    conn.InfoMessage += (s, e) => {
                        resultBuilder.AppendLine(e.Message);
                    };

                    conn.Open();

                    // Enable statistics
                    using (var cmdStatsOn = new SqlCommand("SET STATISTICS IO ON; SET STATISTICS TIME ON;", conn))
                    {
                        cmdStatsOn.ExecuteNonQuery();
                    }

                    // Execute the actual query
                    using (var cmdQuery = new SqlCommand(sqlQuery, conn))
                    {
                        if (sqlQuery.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
                        {
                            using (var reader = cmdQuery.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    // Consume data
                                }
                            }
                        }
                        else
                        {
                            cmdQuery.ExecuteNonQuery(); // For INSERT, UPDATE, DELETE
                        }
                    }

                    // Disable statistics
                    using (var cmdStatsOff = new SqlCommand("SET STATISTICS IO OFF; SET STATISTICS TIME OFF;", conn))
                    {
                        cmdStatsOff.ExecuteNonQuery();
                    }

                    if (resultBuilder.Length > 0)
                    {
                        MessageBox.Show(resultBuilder.ToString(), "Query Performance Statistics");
                        lblMessage.Text = "Statistik performa query berhasil diambil.";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        MessageBox.Show("Tidak ada statistik performa yang dihasilkan.", "Query Performance Statistics");
                        lblMessage.Text = "Tidak ada statistik performa yang dihasilkan.";
                        lblMessage.ForeColor = System.Drawing.Color.Orange;
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Terjadi error database saat mengambil statistik query: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal mengambil statistik query karena error database.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi error tak terduga saat mengambil statistik query: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal mengambil statistik query karena error tak terduga.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void btnAnalisis_Click(object sender, EventArgs e)
        {
            var queryToAnalyze = "SELECT * FROM jadwal WHERE kapasitas > 3 ORDER BY tanggal DESC";
            ShowQueryStatistics(queryToAnalyze);
        }
        private void EnsureIndexes()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    var indexScript = @"
                        IF OBJECT_ID('dbo.jadwal', 'U') IS NOT NULL
                        BEGIN
                            IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='idx_jadwal_tujuan' AND object_id = OBJECT_ID('dbo.jadwal'))
                                CREATE NONCLUSTERED INDEX idx_jadwal_tujuan ON dbo.jadwal (tujuan);

                            IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='idx_jadwal_tanggal' AND object_id = OBJECT_ID('dbo.jadwal'))
                                CREATE NONCLUSTERED INDEX idx_jadwal_tanggal ON dbo.jadwal (tanggal);

                            IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='idx_jadwal_tujuan_tanggal' AND object_id = OBJECT_ID('dbo.jadwal'))
                                CREATE NONCLUSTERED INDEX idx_jadwal_tujuan_tanggal ON dbo.jadwal (tujuan, tanggal);
                        END";
                    using (var cmd = new SqlCommand(indexScript, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    lblMessage.Text = "Indeks untuk tabel jadwal berhasil diverifikasi/dibuat.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Terjadi error database saat memastikan indeks jadwal: " + ex.Message, "Error Indeks", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal memastikan indeks jadwal karena error database.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi error tak terduga saat memastikan indeks jadwal: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblMessage.Text = "Gagal memastikan indeks jadwal karena error tak terduga.";
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
}
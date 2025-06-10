using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient; // Diubah dari MySql.Data.MySqlClient
using System.Text.RegularExpressions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Runtime.Caching;
using System.Text;
using System.Xml;

namespace Travel
{
    public partial class mobil : Form
    {
        // Ganti dengan connection string MS SQL Server Anda
        static string connectionString = "Data Source=AKMAL;Initial Catalog = Travel; Integrated Security = True";
        // Atau untuk Windows Authentication:
        // static string connectionString = "Server=your_server_name;Database=travel;Trusted_Connection=True;";

        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
        };
        private const string CacheKey = "JadwalData";

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
            DataTable dt;
            if (_cache.Contains(CacheKey))
            {
                dt = _cache.Get(CacheKey) as DataTable;
            }
            else
            {
                dt = new DataTable();
                // Menggunakan SqlConnection
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "SELECT * FROM jadwal";
                        // Menggunakan SqlDataAdapter
                        SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                        adapter.Fill(dt);
                        _cache.Add(CacheKey, dt, _policy);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to load data: " + ex.Message);
                    }
                }
            }
            dataGridView1.DataSource = dt;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(ttujuan.Text)) { MessageBox.Show("Tujuan wajib diisi!"); ttujuan.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(twaktu.Text)) { MessageBox.Show("Waktu wajib diisi!"); twaktu.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(tharga.Text) || !decimal.TryParse(tharga.Text, out _)) { MessageBox.Show("Harga wajib diisi dan harus angka desimal!"); tharga.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(tkapasitas.Text) || !int.TryParse(tkapasitas.Text, out _)) { MessageBox.Show("Kapasitas wajib diisi dan harus angka!"); tkapasitas.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(tmerk.Text)) { MessageBox.Show("Merk mobil wajib diisi!"); tmerk.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(tmodel.Text)) { MessageBox.Show("Model mobil wajib diisi!"); tmodel.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(tplat.Text)) { MessageBox.Show("Plat nomor wajib diisi!"); tplat.Focus(); return false; }
            if (tstatus.SelectedIndex < 0) { MessageBox.Show("Status wajib dipilih!"); tstatus.Focus(); return false; }
            return true;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            // Menggunakan SqlConnection
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO jadwal (tujuan, tanggal, waktu, harga, kapasitas, merk_mobil, model_mobil, plat_nomor, status) VALUES (@tujuan, @tanggal, @waktu, @harga, @kapasitas, @merk, @model, @plat, @status)";
                    // Menggunakan SqlCommand
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
                    _cache.Remove(CacheKey);
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
            if (dataGridView1.SelectedRows.Count == 0) { MessageBox.Show("Pilih data yang akan diupdate!"); return; }
            if (!ValidateInput()) return;
            // Menggunakan SqlConnection
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"UPDATE jadwal SET tujuan=@tujuan, tanggal=@tanggal, waktu=@waktu, harga=@harga, kapasitas=@kapasitas, merk_mobil=@merk, model_mobil=@model, plat_nomor=@plat, status=@status WHERE id_jadwal=@id";
                    // Menggunakan SqlCommand
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
                    _cache.Remove(CacheKey);
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
            if (dataGridView1.SelectedRows.Count == 0) { MessageBox.Show("Pilih data yang akan dihapus!"); return; }
            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Menggunakan SqlConnection
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM jadwal WHERE id_jadwal = @id";
                        // Menggunakan SqlCommand
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", dataGridView1.SelectedRows[0].Cells["id_jadwal"].Value);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data jadwal berhasil dihapus!");
                        _cache.Remove(CacheKey);
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
            _cache.Remove(CacheKey);
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

        // Fungsi Impor dan Preview tidak berubah secara signifikan karena logika
        // pembacaan Excel dan interaksi dengan PreviewForm tetap sama.
        // Perubahan hanya pada connectionString yang diteruskan ke PreviewForm.
        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Excel Files|.xlsx;.xls";
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
                    for (int row = 1; row <= sheet.LastRowNum; row++)
                    {
                        IRow excelRow = sheet.GetRow(row);
                        if (excelRow != null)
                        {
                            DataRow dataRow = dt.NewRow();
                            dataRow["tujuan"] = excelRow.GetCell(0)?.ToString() ?? string.Empty;
                            dataRow["tanggal"] = DateTime.TryParse(excelRow.GetCell(1)?.ToString(), out var tgl) ? tgl : DateTime.Today;
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

                // Asumsikan PreviewForm juga diubah untuk menggunakan SqlConnection
                PreviewForm previewForm = new PreviewForm(dt, connectionString);
                if (previewForm.ShowDialog() == DialogResult.OK)
                {
                    _cache.Remove(CacheKey);
                    LoadData();
                    MessageBox.Show("Data jadwal berhasil diimport!", "Import Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    AnalyzeQuery("SELECT * FROM jadwal WHERE tujuan LIKE 'Yogyakarta%'");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error membaca file: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Diubah untuk MS SQL Server
        private void AnalyzeQuery(string sqlQuery)
        {
            // Untuk SQL Server, kita menggunakan SET SHOWPLAN_XML ON
            var explainQuery = $"SET SHOWPLAN_XML ON; {sqlQuery}; SET SHOWPLAN_XML OFF;";
            var resultBuilder = new StringBuilder();

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(explainQuery, conn))
                    {
                        // Eksekusi mengembalikan XML plan.
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            // Mempercantik tampilan XML untuk MessageBox
                            XmlDocument xmlDoc = new XmlDocument();
                            xmlDoc.LoadXml(result.ToString());
                            using (StringWriter sw = new StringWriter())
                            {
                                XmlTextWriter xtw = new XmlTextWriter(sw);
                                xtw.Formatting = Formatting.Indented;
                                xmlDoc.WriteTo(xtw);
                                resultBuilder.Append(sw.ToString());
                            }
                        }
                        else
                        {
                            resultBuilder.Append("Tidak ada execution plan yang dihasilkan.");
                        }
                    }
                    MessageBox.Show(resultBuilder.ToString(), "Query Execution Plan");
                }
                catch (Exception ex)
                {
                    // Tangkap error jika query itu sendiri salah
                    MessageBox.Show("Gagal menganalisis query: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            var heavyQuery = "SELECT * FROM jadwal WHERE tujuan LIKE 'Yogyakarta%'";
            AnalyzeQuery(heavyQuery);
        }
    }
}
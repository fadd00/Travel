using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
namespace Travel
{
    public partial class mobil : Form
    {
        static string connectionString = "Server=localhost;Database=travel;Uid=root";

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
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM jadwal";
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

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(ttujuan.Text))
            {
                MessageBox.Show("Tujuan wajib diisi!");
                ttujuan.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(twaktu.Text))
            {
                MessageBox.Show("Waktu wajib diisi!");
                twaktu.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(tharga.Text) || !decimal.TryParse(tharga.Text, out _))
            {
                MessageBox.Show("Harga wajib diisi dan harus angka desimal!");
                tharga.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(tkapasitas.Text) || !int.TryParse(tkapasitas.Text, out _))
            {
                MessageBox.Show("Kapasitas wajib diisi dan harus angka!");
                tkapasitas.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(tmerk.Text))
            {
                MessageBox.Show("Merk mobil wajib diisi!");
                tmerk.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(tmodel.Text))
            {
                MessageBox.Show("Model mobil wajib diisi!");
                tmodel.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(tplat.Text))
            {
                MessageBox.Show("Plat nomor wajib diisi!");
                tplat.Focus();
                return false;
            }
            if (tstatus.SelectedIndex < 0)
            {
                MessageBox.Show("Status wajib dipilih!");
                tstatus.Focus();
                return false;
            }
            return true;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO jadwal 
                            (tujuan, tanggal, waktu, harga, kapasitas, merk_mobil, model_mobil, plat_nomor, status)
                            VALUES (@tujuan, @tanggal, @waktu, @harga, @kapasitas, @merk, @model, @plat, @status)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
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

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"UPDATE jadwal SET 
                            tujuan=@tujuan, tanggal=@tanggal, waktu=@waktu, harga=@harga, kapasitas=@kapasitas, 
                            merk_mobil=@merk, model_mobil=@model, plat_nomor=@plat, status=@status
                            WHERE id_jadwal=@id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
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
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM jadwal WHERE id_jadwal = @id";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", dataGridView1.SelectedRows[0].Cells["id_jadwal"].Value);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data jadwal berhasil dihapus!");
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
                ttujuan.Text = row.Cells["tujuan"].Value?.ToString();
                // Set ttanggal.Value if possible, else fallback to today
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

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Excel Files|*.xlsx;*.xls";
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

                    if (Path.GetExtension(filePath).ToLower() == ".xlsx")
                    {
                        workbook = new XSSFWorkbook(fs);
                    }
                    else
                    {
                        throw new Exception("Format file tidak didukung. Gunakan .xlsx");
                    }

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

                // Tampilkan PreviewForm tanpa cache
                PreviewForm previewForm = new PreviewForm(dt, connectionString);
                if (previewForm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    MessageBox.Show("Data jadwal berhasil diimport!", "Import Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error membaca file: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


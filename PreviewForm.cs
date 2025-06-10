using Org.BouncyCastle.Asn1.Cmp; // Ini tidak berhubungan dengan database, mungkin sisa dari library lain
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient; // INI PENTING UNTUK SQL SERVER
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement; // Ini juga mungkin sisa dari desain UI

namespace Travel
{
    public partial class PreviewForm : Form
    {
        private readonly DataTable dataToImport;
        private readonly string connectionString;

        // Asumsi PreviewForm.Designer.cs memiliki:
        // private System.Windows.Forms.DataGridView dgvPreview;
        // private System.Windows.Forms.Label lblStatus;
        // private System.Windows.Forms.ProgressBar progressBar;
        // private System.Windows.Forms.Button btnImportData;
        // private System.Windows.Forms.Button btnCancel;


        public PreviewForm(DataTable dataToImport, string connectionString)
        {
            InitializeComponent();
            this.dataToImport = dataToImport;
            this.connectionString = connectionString;
        }

        private void PreviewForm_Load(object sender, EventArgs e)
        {
            // Set up the DataGridView
            dgvPreview.DataSource = dataToImport;

            // Adjust column headers and autosize columns
            dgvPreview.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            // Show count of records in status label
            lblStatus.Text = $"Jumlah data yang akan diimpor: {dataToImport.Rows.Count}";
        }

        private void btnImportData_Click(object sender, EventArgs e)
        {
            // Confirm before import
            DialogResult result = MessageBox.Show(
                $"Yakin ingin mengimpor {dataToImport.Rows.Count} data ke database?",
                "Konfirmasi Import",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int successCount = 0;
                    int errorCount = 0;
                    StringBuilder errorMessages = new StringBuilder(); // Menggunakan StringBuilder untuk efisiensi string

                    // Track progress
                    progressBar.Minimum = 0;
                    progressBar.Maximum = dataToImport.Rows.Count;
                    progressBar.Value = 0;
                    progressBar.Visible = true;
                    Application.DoEvents(); // Pastikan progress bar terlihat sebelum memulai

                    // Optimize by using batch processing with transaction
                    using (SqlConnection connection = new SqlConnection(connectionString)) // Diubah ke SqlConnection
                    {
                        connection.Open();

                        // Start a transaction for better performance with bulk imports
                        using (SqlTransaction transaction = connection.BeginTransaction()) // Diubah ke SqlTransaction
                        {
                            try
                            {
                                string insertQuery = @"INSERT INTO jadwal 
                                     (tujuan, tanggal, waktu, harga, kapasitas, merk_mobil, model_mobil, plat_nomor, status)
                                     VALUES (@tujuan, @tanggal, @waktu, @harga, @kapasitas, @merk, @model, @plat, @status)";

                                using (SqlCommand cmd = new SqlCommand(insertQuery, connection, transaction)) // Diubah ke SqlCommand
                                {
                                    // Definisikan parameter dengan tipe data SQL Server yang sesuai
                                    // Gunakan SqlDbType untuk kontrol tipe data yang lebih baik
                                    cmd.Parameters.Add("@tujuan", SqlDbType.NVarChar, 255); // Sesuaikan panjang jika perlu
                                    cmd.Parameters.Add("@tanggal", SqlDbType.Date);
                                    cmd.Parameters.Add("@waktu", SqlDbType.Time); // Untuk tipe TIME di SQL Server
                                    cmd.Parameters.Add("@harga", SqlDbType.Decimal); // Untuk DECIMAL di SQL Server
                                    cmd.Parameters.Add("@kapasitas", SqlDbType.Int);
                                    cmd.Parameters.Add("@merk", SqlDbType.NVarChar, 255);
                                    cmd.Parameters.Add("@model", SqlDbType.NVarChar, 255);
                                    cmd.Parameters.Add("@plat", SqlDbType.NVarChar, 50); // Sesuaikan panjang jika perlu
                                    cmd.Parameters.Add("@status", SqlDbType.NVarChar, 50);

                                    // Process each row
                                    foreach (DataRow row in dataToImport.Rows)
                                    {
                                        try
                                        {
                                            // Validate row before import
                                            string validationMessage = ValidateRow(row);
                                            if (!string.IsNullOrEmpty(validationMessage))
                                            {
                                                errorCount++;
                                                errorMessages.AppendLine($"Baris {successCount + errorCount}: {validationMessage}");
                                                continue; // Skip this row
                                            }

                                            // Convert and assign parameter values. Handle DBNull.Value for optional fields
                                            cmd.Parameters["@tujuan"].Value = row["tujuan"] == DBNull.Value ? (object)DBNull.Value : row["tujuan"].ToString();
                                            cmd.Parameters["@tanggal"].Value = row["tanggal"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDateTime(row["tanggal"]).Date;

                                            // Konversi waktu: TimeSpan.Parse dari string excel, atau DBNull
                                            if (row["waktu"] == DBNull.Value || string.IsNullOrWhiteSpace(row["waktu"].ToString()))
                                            {
                                                cmd.Parameters["@waktu"].Value = DBNull.Value;
                                            }
                                            else
                                            {
                                                // Pastikan format waktu dari Excel bisa di-parse oleh TimeSpan
                                                // Contoh: "14:30" atau "14:30:00"
                                                TimeSpan parsedTime;
                                                if (TimeSpan.TryParse(row["waktu"].ToString(), out parsedTime))
                                                {
                                                    cmd.Parameters["@waktu"].Value = parsedTime;
                                                }
                                                else
                                                {
                                                    // Handle case where time parsing fails, e.g., log or assign DBNull
                                                    errorCount++;
                                                    errorMessages.AppendLine($"Baris {successCount + errorCount} (Waktu): Format waktu '{row["waktu"]}' tidak valid.");
                                                    continue;
                                                }
                                            }

                                            cmd.Parameters["@harga"].Value = row["harga"] == DBNull.Value ? (object)DBNull.Value : Convert.ToDecimal(row["harga"]);
                                            cmd.Parameters["@kapasitas"].Value = row["kapasitas"] == DBNull.Value ? (object)DBNull.Value : Convert.ToInt32(row["kapasitas"]);
                                            cmd.Parameters["@merk"].Value = row["merk_mobil"] == DBNull.Value ? (object)DBNull.Value : row["merk_mobil"].ToString();
                                            cmd.Parameters["@model"].Value = row["model_mobil"] == DBNull.Value ? (object)DBNull.Value : row["model_mobil"].ToString();
                                            cmd.Parameters["@plat"].Value = row["plat_nomor"] == DBNull.Value ? (object)DBNull.Value : row["plat_nomor"].ToString();
                                            cmd.Parameters["@status"].Value = row["status"] == DBNull.Value ? (object)DBNull.Value : row["status"].ToString();

                                            // Execute insert query
                                            cmd.ExecuteNonQuery();

                                            successCount++;
                                        }
                                        catch (Exception ex)
                                        {
                                            errorCount++;
                                            errorMessages.AppendLine($"Baris {successCount + errorCount + errorMessages.Length}: {ex.Message}");
                                        }
                                        finally
                                        {
                                            // Update progress bar
                                            progressBar.Value = Math.Min(successCount + errorCount, progressBar.Maximum);
                                            Application.DoEvents(); // Allow UI to update
                                        }
                                    }
                                }

                                // Commit the transaction if everything was successful or partially successful
                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                // Roll back the transaction if a major error occurred during transaction
                                transaction.Rollback();
                                throw new Exception("Terjadi kesalahan fatal saat mengimpor data. Transaksi dibatalkan: " + ex.Message);
                            }
                        }
                    }

                    // Show import results
                    string finalMessage = $"Import selesai!\n\nBerhasil: {successCount} data\nGagal: {errorCount} data";

                    if (errorCount > 0)
                    {
                        finalMessage += $"\n\nDetail error:\n{errorMessages.ToString()}";
                        MessageBox.Show(finalMessage, "Hasil Import (Ada Kesalahan)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show(finalMessage, "Hasil Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Close form with OK result if at least one record was imported successfully
                    if (successCount > 0)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saat mengimpor data: {ex.Message}", "Import Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    progressBar.Visible = false;
                }
            }
        }

        private string ValidateRow(DataRow row)
        {
            // Pastikan kolom ada sebelum mengaksesnya
            if (!dataToImport.Columns.Contains("tujuan") || string.IsNullOrWhiteSpace(row["tujuan"]?.ToString()))
                return "Kolom 'tujuan' tidak ditemukan atau kosong.";

            if (!dataToImport.Columns.Contains("tanggal") || row["tanggal"] == DBNull.Value || !DateTime.TryParse(row["tanggal"].ToString(), out _))
                return "Kolom 'tanggal' tidak ditemukan atau tanggal tidak valid.";

            if (!dataToImport.Columns.Contains("waktu") || string.IsNullOrWhiteSpace(row["waktu"]?.ToString()))
                return "Kolom 'waktu' tidak ditemukan atau kosong.";
            else
            {
                // Tambahkan validasi format waktu yang lebih ketat jika perlu
                if (!TimeSpan.TryParse(row["waktu"].ToString(), out _))
                    return $"Format waktu '{row["waktu"]}' tidak valid. Gunakan HH:mm atau HH:mm:ss.";
            }

            if (!dataToImport.Columns.Contains("harga") || row["harga"] == DBNull.Value || !decimal.TryParse(row["harga"].ToString(), out _))
                return "Kolom 'harga' tidak ditemukan atau harga tidak valid.";

            if (!dataToImport.Columns.Contains("kapasitas") || row["kapasitas"] == DBNull.Value || !int.TryParse(row["kapasitas"].ToString(), out _))
                return "Kolom 'kapasitas' tidak ditemukan atau kapasitas tidak valid.";

            if (!dataToImport.Columns.Contains("merk_mobil") || string.IsNullOrWhiteSpace(row["merk_mobil"]?.ToString()))
                return "Kolom 'merk_mobil' tidak ditemukan atau kosong.";

            if (!dataToImport.Columns.Contains("model_mobil") || string.IsNullOrWhiteSpace(row["model_mobil"]?.ToString()))
                return "Kolom 'model_mobil' tidak ditemukan atau kosong.";

            if (!dataToImport.Columns.Contains("plat_nomor") || string.IsNullOrWhiteSpace(row["plat_nomor"]?.ToString()))
                return "Kolom 'plat_nomor' tidak ditemukan atau kosong.";

            if (!dataToImport.Columns.Contains("status") || string.IsNullOrWhiteSpace(row["status"]?.ToString()))
                return "Kolom 'status' tidak ditemukan atau kosong.";

            return string.Empty;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
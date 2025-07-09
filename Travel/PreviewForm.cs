using ExcelDataReader;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Travel
{
    public partial class PreviewForm : Form
    {
        private readonly DataTable dataToImport;
        private readonly string connectionString;

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
                    string errorMessages = "";

                    // Track progress
                    progressBar.Minimum = 0;
                    progressBar.Maximum = dataToImport.Rows.Count;
                    progressBar.Value = 0;
                    progressBar.Visible = true;

                    // Optimize by using batch processing with transaction
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        // Start a transaction for better performance with bulk imports
                        using (MySqlTransaction transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                // Create a reusable command
                                using (MySqlCommand cmd = new MySqlCommand("AddMahasiswa", connection, transaction))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    // Create parameters once
                                    cmd.Parameters.Add("p_NIM", MySqlDbType.VarChar);
                                    cmd.Parameters.Add("p_Nama", MySqlDbType.VarChar);
                                    cmd.Parameters.Add("p_Email", MySqlDbType.VarChar);
                                    cmd.Parameters.Add("p_Telepon", MySqlDbType.VarChar);
                                    cmd.Parameters.Add("p_Alamat", MySqlDbType.Text);

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
                                                errorMessages += $"Baris {successCount + errorCount}: {validationMessage}\n";
                                                continue;
                                            }

                                            // Update parameter values
                                            cmd.Parameters["p_NIM"].Value = row["NIM"].ToString();
                                            cmd.Parameters["p_Nama"].Value = row["Nama"].ToString();
                                            cmd.Parameters["p_Email"].Value = row["Email"].ToString();
                                            cmd.Parameters["p_Telepon"].Value = row["Telepon"].ToString();
                                            cmd.Parameters["p_Alamat"].Value = row["Alamat"].ToString();

                                            // Execute stored procedure
                                            cmd.ExecuteNonQuery();

                                            successCount++;
                                        }
                                        catch (Exception ex)
                                        {
                                            errorCount++;
                                            errorMessages += $"Baris {successCount + errorCount}: {ex.Message}\n";
                                        }
                                        finally
                                        {
                                            // Update progress bar
                                            progressBar.Value = Math.Min(successCount + errorCount, progressBar.Maximum);
                                            Application.DoEvents(); // Allow UI to update
                                        }
                                    }
                                }

                                // Commit the transaction if everything was successful
                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                // Roll back the transaction if an error occurred
                                transaction.Rollback();
                                throw new Exception("Transaction rolled back: " + ex.Message);
                            }
                        }
                    }

                    // Show import results
                    string message = $"Import selesai!\n\nBerhasil: {successCount} data\nGagal: {errorCount} data";

                    if (errorCount > 0)
                    {
                        message += $"\n\nDetail error:\n{errorMessages}";
                    }

                    MessageBox.Show(message, "Hasil Import", MessageBoxButtons.OK,
                        errorCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

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
            // Validate NIM (required and must be unique)
            if (string.IsNullOrWhiteSpace(row["NIM"].ToString()))
            {
                return "NIM tidak boleh kosong.";
            }

            // Validate NIM length (example: must be exactly 8-15 characters)
            if (row["NIM"].ToString().Length < 8 || row["NIM"].ToString().Length > 15)
            {
                return "NIM harus terdiri dari 8-15 karakter.";
            }

            // Validate Nama (required)
            if (string.IsNullOrWhiteSpace(row["Nama"].ToString()))
            {
                return "Nama tidak boleh kosong.";
            }

            // Validate Email (required and must be valid format)
            string email = row["Email"].ToString();
            if (string.IsNullOrWhiteSpace(email))
            {
                return "Email tidak boleh kosong.";
            }

            // Simple email validation
            if (!email.Contains("@") || !email.Contains("."))
            {
                return "Format email tidak valid.";
            }

            // Validate Telepon (required)
            if (string.IsNullOrWhiteSpace(row["Telepon"].ToString()))
            {
                return "Telepon tidak boleh kosong.";
            }

            // Check if NIM already exists in database
            if (NimExistsInDatabase(row["NIM"].ToString()))
            {
                return $"NIM {row["NIM"]} sudah ada dalam database.";
            }

            // All validations passed
            return string.Empty;
        }

        private bool NimExistsInDatabase(string nim)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM mahasiswa WHERE NIM = @nim", connection))
                {
                    cmd.Parameters.AddWithValue("@nim", nim);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
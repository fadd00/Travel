using System.Runtime.CompilerServices;

namespace Travel
{
    public partial class Form1 : Form
    {
        static string connectionString = string.Format(
        "Server=127.0.0.1; database=travel; UID=root; Password=[].");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ClearForm()
        {
            tadmin.clear();
            tname.clear();
            tphone.clear();
            taddress.clear();

        }
        private void LoadData() {
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = "SELECT * FROM travel";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
    }
}

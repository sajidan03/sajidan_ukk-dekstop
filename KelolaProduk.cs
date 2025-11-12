using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sajidan_ukk_dekstop
{
    public partial class KelolaProduk : Form
    {
        SqlConnection koneksi = Connections.Connect();
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter sda;
        string filename = "";
        string lokasi = "C:\\Sajidan\\C#\\sajidan_ukk-dekstop\\assets";
        public KelolaProduk()
        {
            InitializeComponent();
            show();
        }
        public void show()
        {
            try
            {
                if (koneksi.State == ConnectionState.Closed)
                {
                    koneksi.Open();
                }
                cmd = new SqlCommand("SELECT * FROM products", koneksi);
                dt = new DataTable("products");
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                dgv.DataSource = dt;
            }
            catch
            {
            }
            finally
            {
                if (koneksi.State == ConnectionState.Open)
                {
                    koneksi.Close();
                }
            }
        }
        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 1)
            {
                DataGridViewRow rw = dgv.Rows[e.RowIndex];
                nama.Text = rw.Cells["nama_produk"].Value.ToString();
                harga.Text = rw.Cells["harga"].Value.ToString();
                nama.Text = rw.Cells["nama_produk"].Value.ToString();
                string image = rw.Cells["gambar_produk"].Value.ToString();
                string fullpath = Path.Combine(lokasi, image);
                if (File.Exists(fullpath))
                {
                    pictureBox.Image = Image.FromFile(fullpath);
                }
            }
        }

        private void guna2NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {

        }

        private void guna2NumericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel6_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (nama.Text.Length == 0 && deskripsi.Text.Length == 0)
            {
                MessageBox.Show("Harap isi semua kolom!");
                return;
            }
            else
            {
                koneksi.Open();
                cmd = new SqlCommand("INSERT INTO products VALUES (@id,@nama, @harga, @stok, @deskripsi, @gambar, @tgl)", koneksi);
                cmd.Parameters.AddWithValue("id", Sessions.id);
                cmd.Parameters.AddWithValue("nama", nama.Text.Trim());
                cmd.Parameters.AddWithValue("harga", harga.Text.Trim());
                cmd.Parameters.AddWithValue("stok", stok.Value);
                cmd.Parameters.AddWithValue("deskripsi", deskripsi.Text.Trim());
                cmd.Parameters.AddWithValue("gambar", filename);
                cmd.Parameters.AddWithValue("tgl", DateTime.Now);
                cmd.ExecuteNonQuery();
                koneksi.Close();
                MessageBox.Show("Input berhasil!");
                show();
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            if (opf.ShowDialog() == DialogResult.OK)
            {
                filename = Path.GetFileName(opf.FileName);
                pictureBox.Image = Image.FromFile(opf.FileName);
                string fullPath = Path.Combine(lokasi, filename);
                if (!File.Exists(fullPath))
                {
                    File.Copy(opf.FileName, fullPath);
                }
            }
        }

        private void KelolaProduk_Load(object sender, EventArgs e)
        {
            show();

        }

        private void guna2TextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}

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
using ClosedXML.Excel;

namespace sajidan_ukk_dekstop
{
    public partial class KelolaProduk : Form
    {
        SqlConnection koneksi = Connections.Connect();
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter sda;
        SqlDataReader rd;
        string filename = "";
        string lokasi = "C:\\Sajidan\\UKK\\sajidan_ukk-dekstop\\assets";
        public KelolaProduk()
        {
            InitializeComponent();
            show();
            labelNama.Text = Sessions.username;
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
            if (e.RowIndex >= 0)
            {
                DataGridViewRow rw = dgv.Rows[e.RowIndex];
                nama.Text = rw.Cells["nama_produk"].Value.ToString();
                deskripsi.Text = rw.Cells["deskripsi"].Value.ToString();
                harga.Text = rw.Cells["harga"].Value.ToString();
                nama.Text = rw.Cells["nama_produk"].Value.ToString();
                stok.Value = Convert.ToInt32(rw.Cells["stok"].Value.ToString());
                idp.Text = rw.Cells["id_produk"].Value.ToString();
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
            DialogResult result = MessageBox.Show("Apakah anda yakin ingin menghapus data ini?", "Peringatan", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes) {
                koneksi.Open();
                cmd = new SqlCommand("DELETE FROM products WHERE id_produk = @id", koneksi);
                cmd.Parameters.AddWithValue("@id", idp.Text);
                cmd.ExecuteNonQuery();
                koneksi.Close();
                MessageBox.Show("Data berhasil dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                show();
                chartLoad();

            }
            else
            {
                return;
            }
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
                chartLoad();
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (nama.Text.Length == 0 && deskripsi.Text.Length == 0)
            {
                MessageBox.Show("Harap isi semua kolom!");
                return;
            }
            else
            {
                koneksi.Open();
                cmd = new SqlCommand("UPDATE products SET id_user = @id , nama_produk = @nama, harga = @harga, stok = @stok, deskripsi = @deskripsi, gambar_produk = @gambar, tanggal_upload =  @tgl WHERE id_produk = @idp", koneksi);
                cmd.Parameters.AddWithValue("idp", idp.Text);
                cmd.Parameters.AddWithValue("id", Sessions.id);
                cmd.Parameters.AddWithValue("nama", nama.Text.Trim());
                cmd.Parameters.AddWithValue("harga", harga.Text.Trim());
                cmd.Parameters.AddWithValue("stok", stok.Value);
                cmd.Parameters.AddWithValue("deskripsi", deskripsi.Text.Trim());
                cmd.Parameters.AddWithValue("gambar", filename);
                cmd.Parameters.AddWithValue("tgl", DateTime.Now);
                cmd.ExecuteNonQuery();
                koneksi.Close();
                MessageBox.Show("Update berhasil!");
                show();
                chartLoad();
            }
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
            chartLoad();
        }
        public void chartLoad()
        {
            koneksi.Open();
            cmd = new SqlCommand("SELECT * FROM products", koneksi);
            rd = cmd.ExecuteReader();
            chart1.Series.Clear();
            chart1.Series.Add("Stok & Daftar produk");
            int i = 0;
            while (rd.Read())
            {
                int stok = Convert.ToInt32(rd["stok"]);
                string nama = rd["nama_produk"].ToString();
                chart1.Series[i].Points.AddXY(nama, stok);
                //i++;
            }
            rd.Close();
            koneksi.Close();
        }
        private void guna2TextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah anda yakin ingin logout?", "Peringatan", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Sessions.id = 0;
                Sessions.username = ""; 
                login login = new login();
                MessageBox.Show("Anda telah logout", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                login.Show();
                this.Hide();
            }
            else
            {
                return;
            }
        }

        private void guna2TextBox1_TextChanged_3(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (koneksi.State == ConnectionState.Closed)
                {
                    koneksi.Open();
                }
                cmd = new SqlCommand("SELECT * FROM products where nama_produk LIKE @nama", koneksi);
                cmd.Parameters.AddWithValue("@nama", "%" + cari.Text + "%");
                dt = new DataTable();
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

        private void guna2TextBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.Title = "Simpan File Excel";
                saveFileDialog.FileName = "dataProducts.xlsx";
                saveFileDialog.DefaultExt = "xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (XLWorkbook xl = new XLWorkbook())
                        {
                            xl.Worksheets.Add(dt, "products");
                            xl.SaveAs(saveFileDialog.FileName);
                        }
                        MessageBox.Show("Data berhasil disimpan!", "Sukses",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saat menyimpan file: {ex.Message}", "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}

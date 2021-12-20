using System;
using Npgsql;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kahvedukkani
{
    public partial class FrmKategori : Form
    {
        public FrmKategori()
        {
            InitializeComponent();
        }
        private void FrmKategori_Load(object sender,EventArgs e)
        {

        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=kahve_dukkani; user ID=postgres; password=asdfg");

        private void btnListele_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from kategori";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand("insert into kategori " +
                "(kategorino,kategoriadi) values (@p1,@p2)",baglanti);
            komut1.Parameters.AddWithValue("@p1", int.Parse(txtKategoriNo.Text));
            komut1.Parameters.AddWithValue("@p2", txtKategoriAdi.Text);
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kategori ekleme işlemi tamamlandı.");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("delete from kategori where kategorino=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(txtKategoriNo.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kategori başarıyla silindi.");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("update kategori set kategoriadi=@p1 where kategorino=@p2",baglanti);
            komut.Parameters.AddWithValue("@p1", txtKategoriAdi.Text);
            komut.Parameters.AddWithValue("@p2", int.Parse(txtKategoriNo.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kategori güncelleme işlemi tamamlandı.");
        }
    }
}

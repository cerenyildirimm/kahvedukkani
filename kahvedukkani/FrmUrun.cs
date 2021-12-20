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
    public partial class FrmUrun : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=kahve_dukkani; user ID=postgres; password=asdfg");

        public FrmUrun()
        {
            InitializeComponent();
        }

        private void FrmUrun_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from kategori", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbKategori.DisplayMember = "kategoriadi";
            cmbKategori.ValueMember = "kategorino";
            cmbKategori.DataSource = dt;
            baglanti.Close();
        }
        
        private void btnListele_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from urun";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into urun (urunid,urunadi,islemesuresi,kategorino) " +
                "values (@p1,@p2,@p3,@p4)",baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(txtUrunId.Text));
            komut.Parameters.AddWithValue("@p2", txtUrunAdi.Text);
            komut.Parameters.AddWithValue("@p3", int.Parse(txtIslemeSuresi.Text));
            komut.Parameters.AddWithValue("@p4", int.Parse(cmbKategori.SelectedValue.ToString()));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün ekleme işlemi tamamlandı.");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("delete from urun where urunid=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(txtUrunId.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün silme işlemi tamamlandı.");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("update urun set urunadi=@p1" +
                ",islemesuresi=@p2,kategorino=@p3 where urunid=@p4",baglanti);
            komut.Parameters.AddWithValue("@p1", txtUrunAdi.Text);
            komut.Parameters.AddWithValue("@p2", int.Parse(txtIslemeSuresi.Text));
            komut.Parameters.AddWithValue("@p3", int.Parse(cmbKategori.SelectedValue.ToString()));
            komut.Parameters.AddWithValue("@p4", int.Parse(txtUrunId.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme işlemi tamamlandı.");
        }

        private void btnArama_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("select * from urungetir(@p1)", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(txtUrunId.Text));
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }
    }
}

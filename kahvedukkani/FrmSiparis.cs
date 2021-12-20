using Npgsql;
using System;
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
    public partial class FrmSiparis : Form
    {
        public FrmSiparis()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=kahve_dukkani; user ID=postgres; password=asdfg");

        private void FrmSiparis_Load(object sender, EventArgs e)
        {
            baglanti.Open();


            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from calisan",baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbPersonelNo.DisplayMember = "personelno";
            cmbPersonelNo.ValueMember = "personelno";
            cmbPersonelNo.DataSource = dt;

            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter("select * from masa", baglanti);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            cmbMasaNo.DisplayMember = "masano";
            cmbMasaNo.ValueMember = "masano";
            cmbMasaNo.DataSource = dt1;

            NpgsqlDataAdapter da2 = new NpgsqlDataAdapter("select * from musteri", baglanti);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            cmbMusteriNo.DisplayMember = "musterino";
            cmbMusteriNo.ValueMember = "musterino";
            cmbMusteriNo.DataSource = dt2;

            NpgsqlDataAdapter da3 = new NpgsqlDataAdapter("select * from urun", baglanti);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);
            cmbUrunID.DisplayMember = "urunid";
            cmbUrunID.ValueMember = "urunid";
            cmbUrunID.DataSource = dt3;

            cmbOdemeTuru.Items.Add('K');
            cmbOdemeTuru.Items.Add('N');

            baglanti.Close();
        }

        private void btnToplamSiparis_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from toplamsiparis",baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into siparis (siparisno,odemeturu,personelno,masano,musterino,urunid) values" +
                "(@p1,@p2,@p3,@p4,@p5,@p6)",baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(txtSiparisNo.Text));
            komut.Parameters.AddWithValue("@p2", cmbOdemeTuru.SelectedItem.ToString());
            komut.Parameters.AddWithValue("@p3", int.Parse(cmbPersonelNo.SelectedValue.ToString()));
            komut.Parameters.AddWithValue("@p4", int.Parse(cmbMasaNo.SelectedValue.ToString()));
            komut.Parameters.AddWithValue("@p5", int.Parse(cmbMusteriNo.SelectedValue.ToString()));
            komut.Parameters.AddWithValue("@p6", int.Parse(cmbUrunID.SelectedValue.ToString()));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ekleme işlemi başarıyla tamamlandı.");
        }


        private void btnListele_Click(object sender, EventArgs e)
        {
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from siparis", baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("delete from siparis where siparisno=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(txtSiparisNo.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Silme işlemi başarıyla tamamlandı.");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("update siparis set odemeturu=@p1,personelno=@p2,masano=@p3,musterino=@p4,urunid=@p5 where siparisno=@p6", baglanti);
            komut.Parameters.AddWithValue("@p1", cmbOdemeTuru.SelectedItem.ToString());
            komut.Parameters.AddWithValue("@p2", int.Parse(cmbPersonelNo.SelectedValue.ToString()));
            komut.Parameters.AddWithValue("@p3", int.Parse(cmbMasaNo.SelectedValue.ToString()));
            komut.Parameters.AddWithValue("@p4", int.Parse(cmbMusteriNo.SelectedValue.ToString()));
            komut.Parameters.AddWithValue("@p5", int.Parse(cmbUrunID.SelectedValue.ToString()));
            komut.Parameters.AddWithValue("@p6", int.Parse(txtSiparisNo.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Güncelleme işlemi başarıyla tamamlandı.");
        }
    }
}

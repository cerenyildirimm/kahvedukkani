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
    public partial class FrmPersonel : Form
    {
        public FrmPersonel()
        {
            InitializeComponent();
        }

        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=kahve_dukkani; user ID=postgres; password=asdfg");

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into personel (personelno,personelturu,personeladi,personelsoyadi, " +
                "personelmaasi,postakodu) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(txtPersonelNo.Text));
            komut.Parameters.AddWithValue("@p2", cmbPersonelTuru.SelectedItem);
            komut.Parameters.AddWithValue("@p3", txtPersonelAdi.Text);
            komut.Parameters.AddWithValue("@p4", txtPersonelSoyadi.Text);
            komut.Parameters.AddWithValue("@p5", int.Parse(txtPersonelMaasi.Text));
            komut.Parameters.AddWithValue("@p6", int.Parse(cmbPostaKodu.SelectedValue.ToString()));
            komut.ExecuteNonQuery();
            if (cmbPersonelTuru.SelectedItem=="Yönetici")
            {
                NpgsqlCommand komut2 = new NpgsqlCommand("select yoneticiekle(@p1,@p2,@p3)",baglanti);
                komut2.Parameters.AddWithValue("@p1", int.Parse(txtPersonelNo.Text));
                komut2.Parameters.AddWithValue("@p2", txtYoneticiTuru.Text);
                komut2.Parameters.AddWithValue("@p3", int.Parse(txtYoneticiMesai.Text));
                komut2.ExecuteNonQuery();
            }
            else if (cmbPersonelTuru.SelectedItem=="Çalışan")
            {
                NpgsqlCommand komut3 = new NpgsqlCommand("select calisanekle(@p1,@p2,@p3)",baglanti);
                komut3.Parameters.AddWithValue("@p1", int.Parse(txtPersonelNo.Text));
                komut3.Parameters.AddWithValue("@p2", txtCalisanTuru.Text);
                komut3.Parameters.AddWithValue("@p3", int.Parse(txtCalisanMesai.Text));
                komut3.ExecuteNonQuery();
            }
            else
            {
                NpgsqlCommand komut4 = new NpgsqlCommand("select tamirciekle(@p1,@p2,@p3,@p4)",baglanti);
                komut4.Parameters.AddWithValue("@p1", int.Parse(txtPersonelNo.Text));
                komut4.Parameters.AddWithValue("@p2", Convert.ToInt64(txtTamirciNo.Text));
                komut4.Parameters.AddWithValue("@p3", int.Parse(txtTamirciGelme.Text));
                komut4.Parameters.AddWithValue("@p4", int.Parse(txtTamirciMakine.Text));
                komut4.ExecuteNonQuery();
            }
            baglanti.Close();
            MessageBox.Show("Personel ekleme işlemi tamamlandı.");
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from personel";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("delete from personel where personelno=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(txtPersonelNo.Text));
            if (cmbPersonelTuru.SelectedItem.ToString()=="Yönetici")
            {
                NpgsqlCommand komut1 = new NpgsqlCommand("select yoneticisil(@p2)",baglanti);
                komut1.Parameters.AddWithValue("@p2", int.Parse(txtPersonelNo.Text));
                komut1.ExecuteNonQuery();
            }
            else if (cmbPersonelTuru.SelectedItem.ToString()=="Çalışan")
            {
                NpgsqlCommand komut2 = new NpgsqlCommand("select calisansil(@p3)", baglanti);
                komut2.Parameters.AddWithValue("@p3", int.Parse(txtPersonelNo.Text));
                komut2.ExecuteNonQuery();
            }
            else
            {
                NpgsqlCommand komut3 = new NpgsqlCommand("select tamircisil(@p4)",baglanti);
                komut3.Parameters.AddWithValue("@p4", int.Parse(txtPersonelNo.Text));
                komut3.ExecuteNonQuery();
            }
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Silme işlemi başarıyla gerçekleşti.");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("update personel set personelturu=@p1,personeladi=@p2," +
                "personelsoyadi=@p3,personelmaasi=@p4,postakodu=@p5 where personelno=@p6",baglanti);
            komut.Parameters.AddWithValue("@p1", cmbPersonelTuru.SelectedItem);
            komut.Parameters.AddWithValue("@p2", txtPersonelAdi.Text);
            komut.Parameters.AddWithValue("@p3", txtPersonelSoyadi.Text);
            komut.Parameters.AddWithValue("@p4", int.Parse(txtPersonelMaasi.Text));
            komut.Parameters.AddWithValue("@p5", int.Parse(cmbPostaKodu.SelectedValue.ToString()));
            komut.Parameters.AddWithValue("@p6", int.Parse(txtPersonelNo.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
        }

        private void FrmPersonel_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from adres", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbPostaKodu.DisplayMember = "postakodu";
            cmbPostaKodu.ValueMember = "postakodu";
            cmbPostaKodu.DataSource = dt;
            baglanti.Close();
            cmbPersonelTuru.Items.Add("Yönetici");
            cmbPersonelTuru.Items.Add("Çalışan");
            cmbPersonelTuru.Items.Add("Tamirci");
        }

        private void btnCalisanListele_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from calisan";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnTamirciListele_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from tamirci";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnYoneticiListele_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from yonetici";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ticari_Otomasyon
{
    public partial class FrmFaturaUrunDuzenleme : Form
    {
        public FrmFaturaUrunDuzenleme()
        {
            InitializeComponent();
        }
        public string urunid;
        sqlbaglantisi bgl = new sqlbaglantisi();

        private void FrmFaturaUrunDuzenleme_Load(object sender, EventArgs e)
        {
            TxtUrunid.Text = urunid;

            SqlCommand komut = new SqlCommand("Select * From TBL_FATURADETAY where FATURAURUNID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", urunid);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                TxtFiyat.Text = dr[3].ToString();
                TxtMiktar.Text = dr[2].ToString();
                TxtTutar.Text = dr[4].ToString();
                TxtUrunAd.Text = dr[1].ToString();

                bgl.baglanti().Close();
            }
        }


        void Temizle()
        {
            TxtUrunid.Text = "";
            TxtUrunAd.Text = "";
            TxtMiktar.Text = "";
            TxtFiyat.Text = "";
            TxtTutar.Text = "";
        }

        private void BtnGuncelle_Click_1(object sender, EventArgs e)
        {
            if (TxtUrunAd.Text == "" || TxtMiktar.Text == "" || TxtFiyat.Text == "" || TxtTutar.Text == "")
            {
                MessageBox.Show("Lütfen Boş Alan Bırakmayınız", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                SqlCommand komut = new SqlCommand("update TBL_FATURADETAY set URUNAD=@P1,MIKTAR=@P2,FIYAT=@P3,TUTAR=@P4 WHERE FATURAURUNID=@P5", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtUrunAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtMiktar.Text);
                komut.Parameters.AddWithValue("@p3", decimal.Parse(TxtFiyat.Text));
                komut.Parameters.AddWithValue("@p4", decimal.Parse(TxtTutar.Text));
                komut.Parameters.AddWithValue("@p5", TxtUrunid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Değişiklikler Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void BtnSil_Click_1(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete From TBL_FATURADETAY where FATURAURUNID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtUrunid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();

        }
    }
}

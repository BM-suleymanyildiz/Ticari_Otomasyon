using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ticari_Otomasyon
{
    public partial class FrmFaturalar : Form
    {
        public FrmFaturalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_FATURABILGI", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void Temizle()
        {
            TxtAlici.Text = "";
            Txtid.Text = "";
            TxtSeri.Text = "";
            TxtSiraNo.Text = "";
            TxtTeslimAlan.Text = "";
            TxtTeslimEden.Text = "";
            TxtVergiDairesi.Text = "";
            MskSaat.Text = "";
            MskTarih.Text = "";
            comboBox1.Text = "";
        }
        void Temizle1()
        {
            TxtUrunid.Text = "";
            TxtUrunAd.Text = "";
            TxtMiktar.Text = "";
            TxtTutar.Text = "";
            TxtFiyat.Text = "";
            TxtPersonel.Text = "";
            TxtFaturaid.Text = "";
            TxtFırma.Text = "";
        }
        private void FrmFaturalar_Load(object sender, EventArgs e)
        {
            listele();

            Temizle();
            Temizle1();
        }








        private void BtnSil_Click_1(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete From TBL_FATURABILGI where FATURABILGIID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Fatura Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Question);
            listele();
        }

        private void BtnGuncelle_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtSeri.Text) ||
    string.IsNullOrWhiteSpace(TxtSiraNo.Text) ||
    comboBox1.SelectedIndex == -1 ||   // ComboBox boşsa seçili eleman olmaz, index -1 olur
    string.IsNullOrWhiteSpace(MskTarih.Text) ||
    string.IsNullOrWhiteSpace(MskSaat.Text) ||
    string.IsNullOrWhiteSpace(TxtVergiDairesi.Text) ||
    string.IsNullOrWhiteSpace(TxtAlici.Text) ||
    string.IsNullOrWhiteSpace(TxtTeslimEden.Text) ||
    string.IsNullOrWhiteSpace(TxtTeslimAlan.Text))
            {
                MessageBox.Show("Lütfen tüm alanları eksiksiz doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand komut = new SqlCommand("update TBL_FATURABILGI set SERI=@P1,SIRANO=@P2,TARIH=@P3,SAAT=@P4,VERGIDAIRE=@P5,ALICI=@P6,TESLIMEDEN=@P7,TESLIMALAN=@P8 WHERE FATURABILGIID=@P9", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtSeri.Text);
            komut.Parameters.AddWithValue("@P2", TxtSiraNo.Text);
            komut.Parameters.AddWithValue("@P3", MskTarih.Text);
            komut.Parameters.AddWithValue("@P4", MskSaat.Text);
            komut.Parameters.AddWithValue("@P5", TxtVergiDairesi.Text);
            komut.Parameters.AddWithValue("@P6", TxtAlici.Text);
            komut.Parameters.AddWithValue("@P7", TxtTeslimEden.Text);
            komut.Parameters.AddWithValue("@P8", TxtTeslimAlan.Text);
            komut.Parameters.AddWithValue("@P9", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Fatura Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();

        }

        private void BtnBul_Click_1(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select URUNAD,SATISFIYAT from TBL_URUNLER where ID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtUrunid.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                TxtUrunAd.Text = dr[0].ToString();
                TxtFiyat.Text = dr[1].ToString();
            }
            bgl.baglanti().Close();
        }

        private void BtnKaydet_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtSeri.Text) ||
    string.IsNullOrWhiteSpace(TxtSiraNo.Text) ||
    comboBox1.SelectedIndex == -1 ||   // ComboBox boşsa seçili eleman olmaz, index -1 olur
    string.IsNullOrWhiteSpace(MskTarih.Text) ||
    string.IsNullOrWhiteSpace(MskSaat.Text) ||
    string.IsNullOrWhiteSpace(TxtVergiDairesi.Text) ||
    string.IsNullOrWhiteSpace(TxtAlici.Text) ||
    string.IsNullOrWhiteSpace(TxtTeslimEden.Text) ||
    string.IsNullOrWhiteSpace(TxtTeslimAlan.Text))
            {
                MessageBox.Show("Lütfen tüm alanları eksiksiz doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (TxtFaturaid.Text == "")
            {
                SqlCommand komut = new SqlCommand("insert into TBL_FATURABILGI (SERI,SIRANO,TARIH,SAAT,VERGIDAIRE,ALICI,TESLIMEDEN,TESLIMALAN) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8)", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", TxtSeri.Text);
                komut.Parameters.AddWithValue("@P2", TxtSiraNo.Text);
                komut.Parameters.AddWithValue("@P3", MskTarih.Text);
                komut.Parameters.AddWithValue("@P4", MskSaat.Text);
                komut.Parameters.AddWithValue("@P5", TxtVergiDairesi.Text);
                komut.Parameters.AddWithValue("@P6", TxtAlici.Text);
                komut.Parameters.AddWithValue("@P7", TxtTeslimEden.Text);
                komut.Parameters.AddWithValue("@P8", TxtTeslimAlan.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura Bilgisi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }

            // Firma Carisi
            if (TxtFaturaid.Text != "" && comboBox1.Text == "Firma")
            {
                var connection = bgl.baglanti();

                // Firma kontrolü (TBLFIRMALAR veya TBLMUSTERILER)
                bool firmaVarMi = false;
                SqlCommand firmaKontrol1 = new SqlCommand("select count(*) from TBL_FIRMALAR where ID=@firma", connection);
                firmaKontrol1.Parameters.AddWithValue("@firma", TxtFırma.Text);
                int sayi1 = (int)firmaKontrol1.ExecuteScalar();



                if (sayi1 > 0)
                {
                    firmaVarMi = true;

                }

                if (!firmaVarMi)
                {
                    MessageBox.Show("Girilen firma/müşteri bilgisi bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connection.Close();
                    return;
                }

                // Personel kontrolü
                SqlCommand personelKontrol = new SqlCommand("select count(*) from TBL_PERSONELLER where ID=@personel", connection);
                personelKontrol.Parameters.AddWithValue("@personel", TxtPersonel.Text);
                int personelSayisi = (int)personelKontrol.ExecuteScalar();

                if (personelSayisi == 0)
                {
                    MessageBox.Show("Girilen personel bilgisi bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connection.Close();
                    return;
                }
                SqlCommand faturaKontrol = new SqlCommand("select count(*) from TBL_FATURABILGI where FATURABILGIID=@faturaId", connection);
                faturaKontrol.Parameters.AddWithValue("@faturaId", TxtFaturaid.Text);
                int faturaSayisi = (int)faturaKontrol.ExecuteScalar();

                if (faturaSayisi == 0)
                {
                    MessageBox.Show("Girilen fatura ID bilgisi bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connection.Close();
                    return;
                }


                // Stok miktarını kontrol et
                SqlCommand stokKontrol = new SqlCommand("select ADET from TBL_URUNLER where ID=@urunId", connection);
                stokKontrol.Parameters.AddWithValue("@urunId", TxtUrunid.Text);
                int mevcutStok = Convert.ToInt32(stokKontrol.ExecuteScalar());
                connection.Close();

                int talepEdilenMiktar = Convert.ToInt32(TxtMiktar.Text);

                if (mevcutStok < talepEdilenMiktar)
                {
                    MessageBox.Show("Stokta yeterli ürün yok. Mevcut stok: " + mevcutStok, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // İşlemi durdur
                }

                // Stok yeterliyse işlemleri yapmaya devam et

                double miktar, tutar, fiyat;
                fiyat = Convert.ToDouble(TxtFiyat.Text);
                miktar = Convert.ToDouble(TxtMiktar.Text);
                tutar = miktar * fiyat;
                TxtTutar.Text = tutar.ToString();

                SqlCommand komut2 = new SqlCommand("insert into TBL_FATURADETAY (URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                komut2.Parameters.AddWithValue("@p1", TxtUrunAd.Text);
                komut2.Parameters.AddWithValue("@p2", TxtMiktar.Text);
                komut2.Parameters.AddWithValue("@p3", decimal.Parse(TxtFiyat.Text));
                komut2.Parameters.AddWithValue("@p4", decimal.Parse(TxtTutar.Text));
                komut2.Parameters.AddWithValue("@p5", TxtFaturaid.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();

                SqlCommand komut3 = new SqlCommand("insert into TBL_FIRMAHAREKETLER (URUNID,ADET,PERSONEL,FIRMA,FIYAT,TOPLAM,FATURAID,TARIH) values (@h1,@h2,@h3,@h4,@h5,@h6,@h7,@h8)", bgl.baglanti());
                komut3.Parameters.AddWithValue("@h1", TxtUrunid.Text);
                komut3.Parameters.AddWithValue("@h2", TxtMiktar.Text);
                komut3.Parameters.AddWithValue("@h3", TxtPersonel.Text);
                komut3.Parameters.AddWithValue("@h4", TxtFırma.Text);
                komut3.Parameters.AddWithValue("@h5", decimal.Parse(TxtFiyat.Text));
                komut3.Parameters.AddWithValue("@h6", decimal.Parse(TxtTutar.Text));
                komut3.Parameters.AddWithValue("@h7", TxtFaturaid.Text);
                komut3.Parameters.AddWithValue("@h8", MskTarih.Text);
                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();

                SqlCommand komut4 = new SqlCommand("update TBL_URUNLER set ADET=ADET-@s1 where ID=@s2", bgl.baglanti());
                komut4.Parameters.AddWithValue("@s1", TxtMiktar.Text);
                komut4.Parameters.AddWithValue("@s2", TxtUrunid.Text);
                komut4.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Faturaya Ait Ürün Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Müşteri Carisi
            if (TxtFaturaid.Text != "" && comboBox1.Text == "Müşteri")
            {
                var connection = bgl.baglanti();

                // Firma kontrolü (TBLFIRMALAR veya TBLMUSTERILER)
                bool firmaVarMi = false;


                SqlCommand firmaKontrol2 = new SqlCommand("select count(*) from TBL_MUSTERILER where ID=@firma", connection);
                firmaKontrol2.Parameters.AddWithValue("@firma", TxtFırma.Text);
                int sayi2 = (int)firmaKontrol2.ExecuteScalar();

                if (sayi2 > 0)
                {
                    firmaVarMi = true;

                }

                if (!firmaVarMi)
                {
                    MessageBox.Show("Girilen firma/müşteri bilgisi bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connection.Close();
                    return;
                }

                // Personel kontrolü
                SqlCommand personelKontrol = new SqlCommand("select count(*) from TBL_PERSONELLER where ID=@personel", connection);
                personelKontrol.Parameters.AddWithValue("@personel", TxtPersonel.Text);
                int personelSayisi = (int)personelKontrol.ExecuteScalar();

                if (personelSayisi == 0)
                {
                    MessageBox.Show("Girilen personel bilgisi bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connection.Close();
                    return;
                }

                SqlCommand faturaKontrol = new SqlCommand("select count(*) from TBL_FATURABILGI where FATURABILGIID=@faturaId", connection);
                faturaKontrol.Parameters.AddWithValue("@faturaId", TxtFaturaid.Text);
                int faturaSayisi = (int)faturaKontrol.ExecuteScalar();

                if (faturaSayisi == 0)
                {
                    MessageBox.Show("Girilen fatura ID bilgisi bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connection.Close();
                    return;
                }


                // Öncelikle stoktaki mevcut miktarı sorgula
                SqlCommand stokKontrol = new SqlCommand("select ADET from TBL_URUNLER where ID=@urunId", connection);
                stokKontrol.Parameters.AddWithValue("@urunId", TxtUrunid.Text);
                int mevcutStok = Convert.ToInt32(stokKontrol.ExecuteScalar());
                connection.Close();

                // Kullanıcının girdiği miktarı al
                int talepEdilenMiktar = Convert.ToInt32(TxtMiktar.Text);

                if (mevcutStok < talepEdilenMiktar)
                {
                    MessageBox.Show("Stokta yeterli ürün yok. Mevcut stok: " + mevcutStok, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Fonksiyondan çık, işlemi devam ettirme
                }

                // Eğer stok yeterliyse, aşağıdaki işlemler devam eder:

                double miktar, tutar, fiyat;
                fiyat = Convert.ToDouble(TxtFiyat.Text);
                miktar = Convert.ToDouble(TxtMiktar.Text);
                tutar = miktar * fiyat;
                TxtTutar.Text = tutar.ToString();

                // Fatura detay kaydı
                SqlCommand komut2 = new SqlCommand("insert into TBL_FATURADETAY (URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                komut2.Parameters.AddWithValue("@p1", TxtUrunAd.Text);
                komut2.Parameters.AddWithValue("@p2", TxtMiktar.Text);
                komut2.Parameters.AddWithValue("@p3", decimal.Parse(TxtFiyat.Text));
                komut2.Parameters.AddWithValue("@p4", decimal.Parse(TxtTutar.Text));
                komut2.Parameters.AddWithValue("@p5", TxtFaturaid.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();

                // Hareket tablosuna veri girişi
                SqlCommand komut3 = new SqlCommand("insert into TBL_MUSTERIHAREKETLER (URUNID,ADET,PERSONEL,MUSTERI,FIYAT,TOPLAM,FATURAID,TARIH) values (@h1,@h2,@h3,@h4,@h5,@h6,@h7,@h8)", bgl.baglanti());
                komut3.Parameters.AddWithValue("@h1", TxtUrunid.Text);
                komut3.Parameters.AddWithValue("@h2", TxtMiktar.Text);
                komut3.Parameters.AddWithValue("@h3", TxtPersonel.Text);
                komut3.Parameters.AddWithValue("@h4", TxtFırma.Text);
                komut3.Parameters.AddWithValue("@h5", decimal.Parse(TxtFiyat.Text));
                komut3.Parameters.AddWithValue("@h6", decimal.Parse(TxtTutar.Text));
                komut3.Parameters.AddWithValue("@h7", TxtFaturaid.Text);
                komut3.Parameters.AddWithValue("@h8", MskTarih.Text);
                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();

                // Stok güncelleme
                SqlCommand komut4 = new SqlCommand("update TBL_URUNLER set ADET=ADET-@s1 where ID=@s2", bgl.baglanti());
                komut4.Parameters.AddWithValue("@s1", TxtMiktar.Text);
                komut4.Parameters.AddWithValue("@s2", TxtUrunid.Text);
                komut4.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Faturaya Ait Ürün Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }





        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Temizle1();

        }

        private void gridView1_FocusedRowChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                Txtid.Text = dr["FATURABILGIID"].ToString();
                TxtSiraNo.Text = dr["SIRANO"].ToString();
                TxtSeri.Text = dr["SERI"].ToString();
                MskTarih.Text = dr["TARIH"].ToString();
                MskSaat.Text = dr["SAAT"].ToString();
                TxtAlici.Text = dr["ALICI"].ToString();
                TxtTeslimEden.Text = dr["TESLIMEDEN"].ToString();
                TxtTeslimAlan.Text = dr["TESLIMALAN"].ToString();
                TxtVergiDairesi.Text = dr["VERGIDAIRE"].ToString();
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunDetay fr = new FrmFaturaUrunDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                fr.id = dr["FATURABILGIID"].ToString();
            }
            fr.Show();
        }
        XtraReport2 xr15;

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (xr15 == null || xr15.IsDisposed)
            {
                xr15 = new XtraReport2();
                xr15.PreviewFormClosed += (s, args) => { xr15 = null; };
                xr15.ShowPreview();
            }
            else
            {
                xr15.ShowPreview(); // Eğer nesne hala açıksa, tekrar göster
            }
        }

    }
}

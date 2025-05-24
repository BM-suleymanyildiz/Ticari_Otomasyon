using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ticari_Otomasyon
{
    public partial class FrmPersonel : Form
    {
        public FrmPersonel()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void personelliste()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_PERSONELLER", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void sehirlistesi()
        {
            SqlCommand komut = new SqlCommand("Select SEHIR From TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbil.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        void temizle()
        {
            Txtid.Text = "";
            TxtAd.Text = "";
            TxtGorev.Text = "";
            TxtSoyad.Text = "";
            TxtMail.Text = "";
            MskTC.Text = "";
            MskTelefon1.Text = "";
            Cmbil.Text = "";
            Cmbilce.Text = "";
            RchAdres.Text = "";
        }

        private void FrmPersonel_Load(object sender, EventArgs e)
        {
            personelliste();

            sehirlistesi();

            temizle();
        }







        private void BtnKaydet_Click(object sender, EventArgs e)
        {

            if (TxtAd.Text == "" || TxtSoyad.Text == "" || MskTelefon1.Text == "" || MskTC.Text == "" || TxtMail.Text == "" || Cmbil.Text == "" || Cmbilce.Text == "" || RchAdres.Text == "" || TxtGorev.Text == "")
            {
                MessageBox.Show("Lütfen boş alanları doldurunuz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // TC uzunluk kontrolü (11 karakter olmalı)
            if (MskTC.Text.Length != 11)
            {
                MessageBox.Show("TC Kimlik numarası 11 haneli olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Telefon numarası uzunluk kontrolü (örneğin (555) 555-5555 = 14 karakter)
            if (MskTelefon1.Text.Length < 14)
            {
                MessageBox.Show("Telefon numarası eksik veya hatalı girildi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mail formatı kontrolü
            string email = TxtMail.Text;
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                MessageBox.Show("Lütfen geçerli bir mail adresi giriniz.", "Geçersiz Mail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // TC numarası daha önce eklenmiş mi kontrolü
            SqlCommand kontrolKomut = new SqlCommand("SELECT COUNT(*) FROM TBL_PERSONELLER WHERE TC = @TC", bgl.baglanti());
            kontrolKomut.Parameters.AddWithValue("@TC", MskTC.Text);
            int kayitSayisi = (int)kontrolKomut.ExecuteScalar();
            bgl.baglanti().Close();

            if (kayitSayisi > 0)
            {
                MessageBox.Show("Bu TC numarasıyla zaten bir personel kayıtlı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Veriyi ekleme işlemi
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_PERSONELLER (AD,SOYAD,TELEFON,TC,MAIL,IL,ILCE,ADRES,GOREV) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9)", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@P4", MskTC.Text);
            komut.Parameters.AddWithValue("@P5", TxtMail.Text);
            komut.Parameters.AddWithValue("@P6", Cmbil.Text);
            komut.Parameters.AddWithValue("@P7", Cmbilce.Text);
            komut.Parameters.AddWithValue("@P8", RchAdres.Text);
            komut.Parameters.AddWithValue("@P9", TxtGorev.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Personel Bilgileri Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            personelliste();




        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            if (TxtAd.Text == "" || TxtSoyad.Text == "" || MskTelefon1.Text == "" || MskTC.Text == "" || TxtMail.Text == "" || Cmbil.Text == "" || Cmbilce.Text == "" || RchAdres.Text == "" || TxtGorev.Text == "")
            {
                MessageBox.Show("Lütfen boş alanları doldurunuz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // TC uzunluk kontrolü (11 karakter olmalı)
            if (MskTC.Text.Trim().Length != 11)
            {
                MessageBox.Show("TC Kimlik numarası 11 haneli olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Telefon numarası uzunluk kontrolü (örnek maske: (999) 000-0000 → 14 karakter)
            if (MskTelefon1.Text.Trim().Length < 14)
            {
                MessageBox.Show("Telefon numarası eksik veya hatalı girildi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mail formatı kontrolü
            string email = TxtMail.Text;
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                MessageBox.Show("Lütfen geçerli bir mail adresi giriniz.", "Geçersiz Mail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                SqlCommand komut = new SqlCommand("UPDATE TBL_PERSONELLER SET AD=@P1, SOYAD=@P2, TELEFON=@P3, TC=@P4, MAIL=@P5, IL=@P6, ILCE=@P7, ADRES=@P8, GOREV=@P9 WHERE ID=@P10", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", TxtAd.Text);
                komut.Parameters.AddWithValue("@P2", TxtSoyad.Text);
                komut.Parameters.AddWithValue("@P3", MskTelefon1.Text);
                komut.Parameters.AddWithValue("@P4", MskTC.Text);
                komut.Parameters.AddWithValue("@P5", TxtMail.Text);
                komut.Parameters.AddWithValue("@P6", Cmbil.Text);
                komut.Parameters.AddWithValue("@P7", Cmbilce.Text);
                komut.Parameters.AddWithValue("@P8", RchAdres.Text);
                komut.Parameters.AddWithValue("@P9", TxtGorev.Text);
                komut.Parameters.AddWithValue("@P10", Txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Personel Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                personelliste();
            }



        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            // Önce personelin hareketlerini sil
            SqlCommand hareketSil = new SqlCommand("DELETE FROM TBL_MUSTERIHAREKETLER WHERE PERSONEL = @p1", bgl.baglanti());
            hareketSil.Parameters.AddWithValue("@p1", Txtid.Text);
            hareketSil.ExecuteNonQuery();

            // Sonra personeli sil
            SqlCommand komutSil = new SqlCommand("DELETE FROM TBL_PERSONELLER WHERE ID = @p1", bgl.baglanti());
            komutSil.Parameters.AddWithValue("@p1", Txtid.Text);
            komutSil.ExecuteNonQuery();

            bgl.baglanti().Close();
            MessageBox.Show("Personel ve ilişkili hareketler silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            personelliste();
            temizle();


        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();

        }

        private void Cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cmbilce.Properties.Items.Clear();

            SqlCommand komut = new SqlCommand("Select ILCE from TBL_ILCELER where SEHIR=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Cmbil.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbilce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                Txtid.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtSoyad.Text = dr["SOYAD"].ToString();
                MskTelefon1.Text = dr["TELEFON"].ToString();
                MskTC.Text = dr["TC"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                Cmbil.Text = dr["IL"].ToString();
                Cmbilce.Text = dr["ILCE"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();
                TxtGorev.Text = dr["GOREV"].ToString();
            }
        }

        private void gridControl1_FocusedViewChanged(object sender, DevExpress.XtraGrid.ViewFocusEventArgs e)
        {
            //
        }

        private void gridView1_FocusedRowChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                Txtid.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtSoyad.Text = dr["SOYAD"].ToString();
                MskTelefon1.Text = dr["TELEFON"].ToString();
                MskTC.Text = dr["TC"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                Cmbil.Text = dr["IL"].ToString();
                Cmbilce.Text = dr["ILCE"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();
                TxtGorev.Text = dr["GOREV"].ToString();
            }
        }
    }
}

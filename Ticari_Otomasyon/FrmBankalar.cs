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
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute BankaBilgileri", bgl.baglanti());
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

        void firmalistesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select ID,AD From Tbl_FIRMALAR", bgl.baglanti());
            da.Fill(dt);
            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.DisplayMember = "AD";
            lookUpEdit1.Properties.DataSource = dt;
        }
        void temizle()
        {
            TxtBankaAd.Text = "";
            TxtHesapNo.Text = "";
            TxtHesapTuru.Text = "";
            TxtIBAN.Text = "";
            Txtid.Text = "";
            TxtSube.Text = "";
            TxtYetkili.Text = "";
            MskTarih.Text = "";
            MskTelefon.Text = "";
            lookUpEdit1.Text = "";
            Cmbil.Text = "";
            Cmbilce.Text = "";
        }


        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            listele();

            sehirlistesi();

            firmalistesi();

            temizle();
        }




        private void BtnKaydet_Click_1(object sender, EventArgs e)
        {
            if (TxtBankaAd.Text == "" || TxtHesapNo.Text == "" || TxtHesapTuru.Text == "" || TxtIBAN.Text == "" || TxtSube.Text == "" || TxtYetkili.Text == "" || MskTelefon.Text == "" || MskTarih.Text == "" || Cmbil.Text == "" || Cmbilce.Text == "" || lookUpEdit1.EditValue == null)
            {
                MessageBox.Show("Lütfen boş alan bırakmayınız", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SqlCommand ibanKontrol = new SqlCommand("SELECT COUNT(*) FROM TBL_BANKALAR WHERE IBAN = @iban", bgl.baglanti());
            ibanKontrol.Parameters.AddWithValue("@iban", TxtIBAN.Text.Trim());
            int ibanSayisi = (int)ibanKontrol.ExecuteScalar();
            bgl.baglanti().Close();

            if (ibanSayisi > 0)
            {
                MessageBox.Show("Bu IBAN numarası zaten kayıtlıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Telefon uzunluk kontrolü (örnek maske: (999) 000-0000 → 14 karakter)
            if (MskTelefon.Text.Trim().Length < 14)
            {
                MessageBox.Show("Telefon numarası eksik veya hatalı girildi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // IBAN format kontrolü (opsiyonel, TR ile başlıyor ve 26 karakter)
            if (!TxtIBAN.Text.StartsWith("TR") || TxtIBAN.Text.Length != 24)
            {
                MessageBox.Show("Lütfen geçerli bir IBAN giriniz (TR ile başlamalı ve 24 karakter olmalı).", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hesap No uzunluk kontrolü (en az 10 karakter örnek)
            if (TxtHesapNo.Text.Trim().Length < 10)
            {
                MessageBox.Show("Hesap numarası en az 10 karakter olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand komut = new SqlCommand("INSERT INTO TBL_BANKALAR (BANKAADI, IL, ILCE, SUBE, IBAN, HESAPNO, YETKILI, TELEFON, TARIH, HESAPTURU, FIRMAID) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtBankaAd.Text);
            komut.Parameters.AddWithValue("@p2", Cmbil.Text);
            komut.Parameters.AddWithValue("@p3", Cmbilce.Text);
            komut.Parameters.AddWithValue("@p4", TxtSube.Text);
            komut.Parameters.AddWithValue("@p5", TxtIBAN.Text);
            komut.Parameters.AddWithValue("@p6", TxtHesapNo.Text);
            komut.Parameters.AddWithValue("@p7", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@p8", MskTelefon.Text);
            komut.Parameters.AddWithValue("@p9", MskTarih.Text);
            komut.Parameters.AddWithValue("@p10", TxtHesapTuru.Text);
            komut.Parameters.AddWithValue("@p11", lookUpEdit1.EditValue);
            komut.ExecuteNonQuery();

            listele();
            bgl.baglanti().Close();
            MessageBox.Show("Banka bilgisi sisteme kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);



        }

        private void BtnSil_Click_1(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from TBL_BANKALAR where ID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            temizle();
            MessageBox.Show("Banka Bilgisi Sistemden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            listele();
        }

        private void BtnGuncelle_Click_1(object sender, EventArgs e)
        {
            if (TxtBankaAd.Text == "" || TxtHesapNo.Text == "" || TxtHesapTuru.Text == "" || TxtIBAN.Text == "" || TxtSube.Text == "" || TxtYetkili.Text == "" || MskTelefon.Text == "" || MskTarih.Text == "")
            {
                MessageBox.Show("Lütfen Boş Alan Bırakmayınız", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Telefon numarası uzunluk kontrolü (örnek: maske ile 14 karakter)
            if (MskTelefon.Text.Trim().Length < 14)
            {
                MessageBox.Show("Telefon numarası eksik veya hatalı girildi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // IBAN kontrolü (TR ile başlamalı ve 26 karakter olmalı)
            if (!TxtIBAN.Text.StartsWith("TR") || TxtIBAN.Text.Length != 24)
            {
                MessageBox.Show("Lütfen geçerli bir IBAN giriniz (TR ile başlamalı ve 24 karakter olmalı).", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hesap No uzunluk kontrolü (en az 10 karakter örnek)
            if (TxtHesapNo.Text.Trim().Length < 10)
            {
                MessageBox.Show("Hesap numarası en az 10 karakter olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand komut = new SqlCommand("update TBL_BANKALAR set BANKAADI=@P1,IL=@P2,ILCE=@P3,SUBE=@P4,IBAN=@P5,HESAPNO=@P6,YETKILI=@P7,TELEFON=@P8,TARIH=@P9,HESAPTURU=@P10,FIRMAID=@P11 WHERE ID=@P12", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtBankaAd.Text);
            komut.Parameters.AddWithValue("@p2", Cmbil.Text);
            komut.Parameters.AddWithValue("@p3", Cmbilce.Text);
            komut.Parameters.AddWithValue("@p4", TxtSube.Text);
            komut.Parameters.AddWithValue("@p5", TxtIBAN.Text);
            komut.Parameters.AddWithValue("@p6", TxtHesapNo.Text);
            komut.Parameters.AddWithValue("@p7", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@p8", MskTelefon.Text);
            komut.Parameters.AddWithValue("@p9", MskTarih.Text);
            komut.Parameters.AddWithValue("@p10", TxtHesapTuru.Text);
            komut.Parameters.AddWithValue("@p11", lookUpEdit1.EditValue);
            komut.Parameters.AddWithValue("@p12", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();

            listele();

            MessageBox.Show("Banka Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


        private void BtnTemizle_Click_1(object sender, EventArgs e)
        {
            temizle();

        }

        private void Cmbil_SelectedIndexChanged_1(object sender, EventArgs e)
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
                TxtBankaAd.Text = dr["BANKAADI"].ToString();
                Cmbil.Text = dr["IL"].ToString();
                Cmbilce.Text = dr["ILCE"].ToString();
                TxtSube.Text = dr["SUBE"].ToString();
                TxtIBAN.Text = dr["IBAN"].ToString();
                TxtHesapNo.Text = dr["HESAPNO"].ToString();
                TxtYetkili.Text = dr["YETKILI"].ToString();
                MskTelefon.Text = dr["TELEFON"].ToString();
                MskTarih.Text = dr["TARIH"].ToString();
                TxtHesapTuru.Text = dr["HESAPTURU"].ToString();
            }
        }
    }
}

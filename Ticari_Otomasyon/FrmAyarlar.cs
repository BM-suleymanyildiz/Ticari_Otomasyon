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
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_ADMIN", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            listele();
            TxtKullaniciAd.Text = "";
            TxtSifre.Text = "";
        }

        private void Btnislem_Click_1(object sender, EventArgs e)
        {
            if (TxtKullaniciAd.Text != "")
            {
                
                    SqlCommand komut = new SqlCommand("insert into TBL_ADMIN values (@p1,@p2)", bgl.baglanti());
                    komut.Parameters.AddWithValue("@p1", TxtKullaniciAd.Text);
                    komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    MessageBox.Show("Yeni Admin Sisteme Kaydedildi", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listele();
                    
               
            }
            else
            {
                MessageBox.Show("Lütfen Kullanıcı Adı Giriniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void gridView1_FocusedRowChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                TxtKullaniciAd.Text = dr["KULLANICIAD"].ToString();
                TxtSifre.Text = dr["SIFRE"].ToString();
            }
            
        }

        private void TxtKullaniciAd_TextChanged(object sender, EventArgs e)
        {
            if (TxtKullaniciAd.Text != "")
            {
                
            }
            else
            {
               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TxtKullaniciAd.Text != "")
            {
               
                if (Btnislem.Text == "Güncelle")
                {
                    SqlCommand komut1 = new SqlCommand("update TBL_ADMIN set SIFRE=@p2 where KULLANICIAD=@p1", bgl.baglanti());
                    komut1.Parameters.AddWithValue("@p1", TxtKullaniciAd.Text);
                    komut1.Parameters.AddWithValue("@p2", TxtSifre.Text);
                    komut1.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    MessageBox.Show("Kayıt Güncellendi", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    listele();
                }
            }
            else
            {
                MessageBox.Show("Lütfen Kullanıcı Adı Giriniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (TxtKullaniciAd.Text != "")
            {
                SqlCommand komut1 = new SqlCommand("update TBL_ADMIN set SIFRE=@p2 where KULLANICIAD=@p1", bgl.baglanti());
                komut1.Parameters.AddWithValue("@p1", TxtKullaniciAd.Text);
                komut1.Parameters.AddWithValue("@p2", TxtSifre.Text);
                komut1.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Kayıt Güncellendi", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
            }
            else
            {
                MessageBox.Show("Lütfen Kullanıcı Adı Giriniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

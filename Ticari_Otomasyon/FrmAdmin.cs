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
    public partial class FrmAdmin : Form
    {
        public FrmAdmin()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(TxtKullaniciAd.Text) || string.IsNullOrWhiteSpace(TxtSifre.Text))
            {
                MessageBox.Show("Lütfen Kullanıcı Adı ve Şifre Giriniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                SqlCommand komut = new SqlCommand("Select * From TBL_ADMIN where KULLANICIAD=@p1 and SIFRE=@p2", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtKullaniciAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    FrmAnaModul fr = new FrmAnaModul();
                    fr.kullanici = TxtKullaniciAd.Text;
                    fr.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Hatalı Kullanıcı Adı ya da Şifre", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                bgl.baglanti().Close();
            }
            
        }

        private void BtnGirisYap_MouseHover(object sender, EventArgs e)
        {
            BtnGirisYap.BackColor = Color.Green;
        }

        private void BtnGirisYap_MouseLeave(object sender, EventArgs e)
        {
            BtnGirisYap.BackColor = Color.LightSeaGreen;

        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {
            this.AcceptButton = BtnGirisYap;
        }
    }
}

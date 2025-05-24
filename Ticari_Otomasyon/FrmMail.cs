using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace Ticari_Otomasyon
{
    public partial class FrmMail : Form
    {
        public FrmMail()
        {
            InitializeComponent();
        }

        public string mail;

        private void FrmMail_Load(object sender, EventArgs e)
        {
            TxtMailAdres.Text = mail;
        }


        private void BtnGonder_Click_1(object sender, EventArgs e)
        {

            if (TxtMailAdres.Text == "" || TxtMesaj.Text == "")
            {
                MessageBox.Show("Lütfen Mail Adresini veya mesaj içeriğini Giriniz");
                return;
            }
            else
            {

                try
                {
                    MailMessage mesajim = new MailMessage();
                    SmtpClient istemci = new SmtpClient();

                    string gonderenMail = "";
                    string uygulamaSifresi = "";

                    istemci.Credentials = new System.Net.NetworkCredential(gonderenMail, uygulamaSifresi);
                    istemci.Port = 587;
                    istemci.Host = "smtp.gmail.com";
                    istemci.EnableSsl = true;

                    mesajim.To.Add(TxtMailAdres.Text);
                    mesajim.From = new MailAddress(gonderenMail);
                    mesajim.Subject = TxtKonu.Text;
                    mesajim.Body = TxtMesaj.Text;

                    istemci.Send(mesajim);

                    MessageBox.Show("📨 Mesaj başarıyla gönderildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }








            }

        }
    }
}

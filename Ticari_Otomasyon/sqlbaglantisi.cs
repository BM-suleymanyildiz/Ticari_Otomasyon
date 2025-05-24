using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Ticari_Otomasyon
{
    class sqlbaglantisi
    {
    public SqlConnection baglanti()
        {
             
            SqlConnection baglan = new SqlConnection("Data Source=sunucuadı;Initial Catalog=veritabanıadı;Integrated Security=True;\r\n");
            baglan.Open();
            return baglan;
        }
    }
}

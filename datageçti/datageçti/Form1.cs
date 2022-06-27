using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace datageçti
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string kaynak = "provider=microsoft.ACE.OLEDB.12.0;Data source= veriler.accdb";
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            OleDbConnection bağlantı = new OleDbConnection(kaynak);
            bağlantı.Open();
           
            string sorgu = "select* from öğrenciler";
            OleDbCommand komut = new OleDbCommand(sorgu, bağlantı);
            OleDbDataReader okuyucu= komut.ExecuteReader();
            for (int i = 0; i < 7; i++)
            {
                dataGridView1.Columns.Add(okuyucu.GetName(i), okuyucu.GetName(i));
            }
            object[] nesne = new object[7];
            while (okuyucu.Read())
            {
                okuyucu.GetValues(nesne);
                dataGridView1.Rows.Add(nesne);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OleDbConnection bağlantı = new OleDbConnection(kaynak);
          

            string sorgu = "insert into öğrenciler (ad,soyad,vize,final) values"+
                "('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "')";
           
            OleDbCommand komut = new OleDbCommand(sorgu, bağlantı);
            komut.Connection.Open();
            int sonuc = komut.ExecuteNonQuery();
            if (sonuc==1)
            {
                MessageBox.Show("işlem başarılı");
            }
            else
            {
                MessageBox.Show("işlem başaarısız");
            }
            bağlantı.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OleDbConnection bağlantı = new OleDbConnection(kaynak);
            string sorgu ="update öğrenciler set final='"+textBox6.Text+"'where ad='"+textBox5.Text+"'";
            OleDbCommand komut = new OleDbCommand(sorgu, bağlantı);
            komut.Connection.Open();
            int sonuc = komut.ExecuteNonQuery();
            if (sonuc == 1)
            {
                MessageBox.Show("değer güncellendi");

            }
            bağlantı.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
           int vize= int.Parse(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
           int final= int.Parse(dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
            double ort = (vize * (0.4)) + (final * (0.6));
            string sorgu = "update öğrenciler set geçmenotu='" + ort+ "'where ad='" + textBox7.Text + "'";
            OleDbConnection bağlantı = new OleDbConnection(kaynak);
            OleDbCommand komut = new OleDbCommand(sorgu, bağlantı);
            komut.Connection.Open();
            int sonuç= komut.ExecuteNonQuery();
            if (sonuç==1)
            {
                MessageBox.Show("işlem yapıldı");
            }
            else
            {
                MessageBox.Show("işlem başarısız");
            }

        }

        private void button5_Click(object sender, EventArgs e)
        { int gn = Int32.Parse(dataGridView1.SelectedRows[0].Cells[5].Value.ToString());
            string n;
            if (gn>60)
            {
                n = "geçti";
            }
            else
            {
                n = "kaldı";
            }
            string sorgu = "update öğrenciler set durum='" + n + "'where ad='"+textBox8.Text+"'";
            OleDbConnection bağlantı = new OleDbConnection(kaynak);
            OleDbCommand komut = new OleDbCommand(sorgu, bağlantı);
            komut.Connection.Open();
            int sonuç = komut.ExecuteNonQuery();
            if (sonuç==1)
            {
                MessageBox.Show("işlem yapıldı");
            }
            else
            {
                MessageBox.Show("işlem başarısız");
            }
        }
    }
}

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
using Tutorial.SqlConn;

namespace WindowsFormsApp1
{
    public partial class Form4 : Form
    {
        public string login;
        public string pass;
        public int id;
        public string NameOfBook;
        public string Author;
        public string Valueofbook;
        public int Valuestr;
        public int Rate;
        public Form4(string txt1, string txt2)
        {
            InitializeComponent();
            label1.Text = txt1;
            label2.Text = txt2;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            SqlConnection connection = DBUtils.GetDBConnection(); // Подключение базы данных
            connection.Open(); // открытие доступа
            SqlCommand select = new SqlCommand("SELECT * FROM Book", connection); // создание команды
            SqlDataReader reader = select.ExecuteReader(); // создание ридера для чтения данных
            while (reader.Read()) // прохождение по массиву данных
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {

                    if (reader.GetName(i) == "Id")
                    {
                        id = int.Parse(String.Format("{0}", reader[i]));// запись данных в переменную
                    }
                    else if (reader.GetName(i) == "NameOfBook")
                    {
                        NameOfBook = String.Format("{0}", reader[i]);// запись данных в переменную
                    }
                    else if(reader.GetName(i) == "Author")
                    {
                        Author = String.Format("{0}", reader[i]);// запись данных в переменную
                    }
                    else if (reader.GetName(i) == "Valueofbook")
                    {
                        Valueofbook = String.Format("{0}", reader[i]);// запись данных в переменную
                    }
                    else if(reader.GetName(i) == "Valuestr")
                    {
                        Valuestr = int.Parse(String.Format("{0}", reader[i]));// запись данных в переменную
                    }
                    else if(reader.GetName(i) == "Rate")
                    {
                        Rate = int.Parse(String.Format("{0}", reader[i]));// запись данных в переменную
                    }
                    
                }
                dataGridView1.Rows.Add(NameOfBook, Author, Valuestr, Rate);// запись данных в таблицу
            }
            connection.Close();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //  поиск из базы данных
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void button2_Click(object sender, EventArgs e)
        {
            string searchValue = maskedTextBox1.Text;


            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value.ToString().Equals(searchValue))
                    {
                        row.Selected = true;
                        dataGridView1.CurrentCell =
                        dataGridView1[0, row.Index];
                        break;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //  Получение ссылки
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void button1_Click(object sender, EventArgs e)
        {
            string linked = " ";
            SqlConnection con = DBUtils.GetDBConnection();
            con.Open(); //Открываем подключение
            SqlCommand com = new SqlCommand($"SELECT * FROM Book WHERE NameOfBook=@NameOfBook1", con);// запрос на удаление
            string id1 = dataGridView1.CurrentRow.Cells[0].Value.ToString();// название выбранной книги
            com.Parameters.AddWithValue("@NameOfBook1", id1);// указание параметра
            SqlDataReader reader = com.ExecuteReader(); // создание ридера для чтения данных
            try
            {
                while (reader.Read()) // прохождение по массиву данных
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {

                        if (reader.GetName(i) == "Valueofbook")
                        {
                            linked = String.Format("{0}", reader[i]);// запись данных в переменную
                        }
                    }
                }
            
                Form3 form3 = new Form3(linked);
                form3.ShowDialog();
                
            }
            catch
            {
                MessageBox.Show("Не удалось найти ссылку!");
            }
            con.Close();


        }
    }
}

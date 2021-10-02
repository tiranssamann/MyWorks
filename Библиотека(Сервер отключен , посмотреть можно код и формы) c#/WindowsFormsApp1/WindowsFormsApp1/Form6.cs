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
    public partial class Form6 : Form
    {
        public int id;
        public string NameOfBook;
        public string Author;
        public string Valueofbook;
        public int Valuestr;
        public int Rate;
        public Form6()
        {
            InitializeComponent();
            
            

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
                    else if (reader.GetName(i) == "Author")
                    {
                        Author = String.Format("{0}", reader[i]);// запись данных в переменную
                    }
                    else if (reader.GetName(i) == "Valueofbook")
                    {
                        Valueofbook = String.Format("{0}", reader[i]);// запись данных в переменную
                    }
                    else if (reader.GetName(i) == "Valuestr")
                    {
                        Valuestr = int.Parse(String.Format("{0}", reader[i]));// запись данных в переменную
                    }
                    else if (reader.GetName(i) == "Rate")
                    {
                        Rate = int.Parse(String.Format("{0}", reader[i]));// запись данных в переменную
                    }

                }
                dataGridView1.Rows.Add(NameOfBook, Author, Valueofbook, Valuestr, Rate);// запись данных в таблицу
            }
            connection.Close();
        }



        private void Button1_Click_1(object sender, EventArgs e)
        {
            SqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            try
            {
                id += 1;
                NameOfBook = maskedTextBox1.Text;
                Author = maskedTextBox2.Text;
                Valueofbook = maskedTextBox3.Text;
                Valuestr = int.Parse(maskedTextBox4.Text);
                Rate = int.Parse(maskedTextBox5.Text);
                dataGridView1.Rows.Add(NameOfBook, Author, Valueofbook, Valuestr, Rate);
                SqlCommand insert = connection.CreateCommand();
                string str = string.Format("INSERT Book(Id,NameOfBook,Author,Valueofbook,Valuestr,Rate) VALUES ('{0}',N'{1}',N'{2}',N'{3}','{4}','{5}')", id, NameOfBook, Author, Valueofbook, Valuestr, Rate);
                insert.CommandText = str;
                int rowaffected = insert.ExecuteNonQuery();
                maskedTextBox1.Text = "";
                maskedTextBox2.Text = "";
                maskedTextBox3.Text = "";
                maskedTextBox4.Text = "";
                maskedTextBox5.Text = "";
                MessageBox.Show("Данные добавлены");
            }
            catch
            {
                maskedTextBox1.Text = "";
                maskedTextBox2.Text = "";
                maskedTextBox3.Text = "";
                maskedTextBox4.Text = "";
                maskedTextBox5.Text = "";
                MessageBox.Show("Вы не заполнили данные!");
            }
            connection.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = DBUtils.GetDBConnection();
            SqlCommand com = new SqlCommand("DELETE FROM Book WHERE NameOfBook=@NameOfBook1", con);// запрос на удаление
            string id1 = dataGridView1.CurrentRow.Cells[0].Value.ToString();// название выбранной книги
            com.Parameters.AddWithValue("@NameOfBook1", id1);// указание параметра
            con.Open(); //Открываем подключение
            try
            {
                com.ExecuteNonQuery();
                MessageBox.Show("Запись удалена");
            }
            catch
            {
                MessageBox.Show("Удалить не удалось!");
            }
            con.Close();
            dataGridView1.Rows.Clear();// обновление таблицы

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
                    else if (reader.GetName(i) == "Author")
                    {
                        Author = String.Format("{0}", reader[i]);// запись данных в переменную
                    }
                    else if (reader.GetName(i) == "Valueofbook")
                    {
                        Valueofbook = String.Format("{0}", reader[i]);// запись данных в переменную
                    }
                    else if (reader.GetName(i) == "Valuestr")
                    {
                        Valuestr = int.Parse(String.Format("{0}", reader[i]));// запись данных в переменную
                    }
                    else if (reader.GetName(i) == "Rate")
                    {
                        Rate = int.Parse(String.Format("{0}", reader[i]));// запись данных в переменную
                    }

                }
                dataGridView1.Rows.Add(NameOfBook, Author, Valueofbook, Valuestr, Rate);// запись данных в таблицу
            }
            connection.Close();
        }
    }
}

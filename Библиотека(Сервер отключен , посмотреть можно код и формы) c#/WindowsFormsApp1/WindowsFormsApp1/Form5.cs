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
    
    public partial class Form5 : Form
    {

        List<Person> peoples = new List<Person>();
        List<book> books = new List<book>();
        public int id;
        public string NameOfBook;
        public string Author;
        public string Valueofbook;
        public int Valuestr;
        public int Rate;
        //переменные учеников
        public int idstud1;
        public string Lastname1;
        public string Name1;
        public string Surname1;
        public string Login1;
        public string Pass1;
        public string GetBook1;
        public string GetRole1;
        public Form5(string txt1, string txt2)
        {
            InitializeComponent();
            label6.Text = txt1;
            label7.Text = txt2;
            
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.AllowUserToAddRows = false;
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
                books.Add(new book() { NameOfBook = NameOfBook, Author = Author, Rate = Rate, Valueofbook = Valueofbook });
                dataGridView1.Rows.Add(NameOfBook, Author, Valueofbook, Valuestr, Rate);// запись данных в таблицу
            }
            connection.Close();
            // чтение студентов
            SqlConnection connection2 = DBUtils.GetDBConnection(); // Подключение базы данных
            connection2.Open(); // открытие доступа
            SqlCommand select2 = new SqlCommand("SELECT * FROM Person WHERE GetRole = N'Студент'", connection2); // создание команды
            SqlDataReader reader2 = select2.ExecuteReader(); // создание ридера для чтения данных
            while (reader2.Read()) // прохождение по массиву данных
            {
                for (int i = 0; i < reader2.FieldCount; i++)
                {

                    if (reader2.GetName(i) == "Id")
                    {
                        idstud1 = int.Parse(String.Format("{0}", reader2[i]));// запись данных в переменную
                    }
                    else if (reader2.GetName(i) == "Lastname")
                    {
                        Lastname1 = String.Format("{0}", reader2[i]);// запись данных в переменную
                    }
                    else if (reader2.GetName(i) == "Name")
                    {
                        Name1 = String.Format("{0}", reader2[i]);// запись данных в переменную
                    }
                    else if (reader2.GetName(i) == "Surname")
                    {
                        Surname1 = String.Format("{0}", reader2[i]);// запись данных в переменную
                    }
                    else if (reader2.GetName(i) == "Login")
                    {
                        Login1 = String.Format("{0}", reader2[i]);// запись данных в переменную
                    }
                    else if (reader2.GetName(i) == "Pass")
                    {
                        Pass1 = String.Format("{0}", reader2[i]);// запись данных в переменную
                    }
                    else if (reader2.GetName(i) == "GetBook")
                    {
                        GetBook1 = String.Format("{0}", reader2[i]);// запись данных в переменную
                    }
                    else if (reader2.GetName(i) == "GetRole")
                    {
                        GetRole1 = String.Format("{0}", reader2[i]);// запись данных в переменную
                    }
                }
                if(GetRole1 == "Администратор")
                {
                    peoples.Add(new Administrator() { name = Name1, Lastname = Lastname1, Login = Login1, Password = Pass1, Surname = Surname1 });
                }
                if (GetRole1 == "Студент")
                {
                    peoples.Add(new student() { name = Name1, Lastname = Lastname1, Login = Login1, Password = Pass1, Surname = Surname1 });
                }
                if (GetRole1 == "Библиотекарь")
                {
                    peoples.Add(new biblio() { name = Name1, Lastname = Lastname1, Login = Login1, Password = Pass1, Surname = Surname1 });
                }
                dataGridView2.Rows.Add(Lastname1, Name1, Surname1, Login1, Pass1, GetBook1, GetRole1);
            }

            connection2.Close();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //  добавление книг в базу
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void button1_Click(object sender, EventArgs e)
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
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //  Удаление книги из базы данных
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void button2_Click(object sender, EventArgs e)
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
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //  добавление студента в базу
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            try
            {
                if (maskedTextBox7.Text != "" && maskedTextBox6.Text != "")
                {


                    SqlCommand insert = connection.CreateCommand();
                    string str = string.Format("INSERT Person(Id,Lastname,Name,Surname,Login,Pass,GetBook,GetRole)" +
                        " VALUES (N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}',N'{7}')"
                        , idstud1 + 1, maskedTextBox10.Text, maskedTextBox9.Text, maskedTextBox8.Text,
                        maskedTextBox7.Text, maskedTextBox6.Text, "0", "Студент");
                    dataGridView2.Rows.Add(maskedTextBox10.Text, maskedTextBox9.Text, maskedTextBox8.Text, maskedTextBox7.Text, maskedTextBox6.Text, "0", "Студент");
                    insert.CommandText = str; // выполнение комманды с присваиванием текста
                    int rowaffected = insert.ExecuteNonQuery();// запись данных в базу данных
                    maskedTextBox10.Text = "";
                    maskedTextBox9.Text = "";
                    maskedTextBox8.Text = "";
                    maskedTextBox7.Text = "";
                    maskedTextBox6.Text = "";
                    MessageBox.Show("Аккаунт зарегистрирован!");
                }

            }
            catch
            {
                maskedTextBox10.Text = "";
                maskedTextBox9.Text = "";
                maskedTextBox8.Text = "";
                maskedTextBox7.Text = "";
                maskedTextBox6.Text = "";
                MessageBox.Show("Вы не заполнили данные!");
            }
            connection.Close();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //  Удаление студента из базы данных
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection con = DBUtils.GetDBConnection();
            SqlCommand com = new SqlCommand("DELETE FROM Person WHERE Lastname=@Lastname1", con);// запрос на удаление
            string id1 = dataGridView1.CurrentRow.Cells[0].Value.ToString();// название выбранной книги
            com.Parameters.AddWithValue("@Lastname1", id1);// указание параметра
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
            dataGridView2.Rows.Clear();// обновление таблицы

            SqlConnection connection2 = DBUtils.GetDBConnection(); // Подключение базы данных
            connection2.Open(); // открытие доступа
            SqlCommand select2 = new SqlCommand("SELECT * FROM Person WHERE GetRole = N'Студент'", connection2); // создание команды
            SqlDataReader reader2 = select2.ExecuteReader(); // создание ридера для чтения данных
            while (reader2.Read()) // прохождение по массиву данных
            {
                for (int i = 0; i < reader2.FieldCount; i++)
                {

                    if (reader2.GetName(i) == "Id")
                    {
                        idstud1 = int.Parse(String.Format("{0}", reader2[i]));// запись данных в переменную
                    }
                    else if (reader2.GetName(i) == "Lastname")
                    {
                        Lastname1 = String.Format("{0}", reader2[i]);// запись данных в переменную
                    }
                    else if (reader2.GetName(i) == "Name")
                    {
                        Name1 = String.Format("{0}", reader2[i]);// запись данных в переменную
                    }
                    else if (reader2.GetName(i) == "Surname")
                    {
                        Surname1 = String.Format("{0}", reader2[i]);// запись данных в переменную
                    }
                    else if (reader2.GetName(i) == "Login")
                    {
                        Login1 = String.Format("{0}", reader2[i]);// запись данных в переменную
                    }
                    else if (reader2.GetName(i) == "Pass")
                    {
                        Pass1 = String.Format("{0}", reader2[i]);// запись данных в переменную
                    }
                    else if (reader2.GetName(i) == "GetBook")
                    {
                        GetBook1 = String.Format("{0}", reader2[i]);// запись данных в переменную
                    }
                    else if (reader2.GetName(i) == "GetRole")
                    {
                        GetRole1 = String.Format("{0}", reader2[i]);// запись данных в переменную
                    }
                }
                dataGridView2.Rows.Add(Lastname1, Name1, Surname1, Login1, Pass1, GetBook1, GetRole1);
            }

            connection2.Close();
        }

       

        private void Button6_Click(object sender, EventArgs e)
        {
            SqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            try
            {
                if (maskedTextBox12.Text != "" && maskedTextBox11.Text != "")
                {
                    SqlCommand insert = connection.CreateCommand();
                    string str = string.Format("INSERT Person(Id,Lastname,Name,Surname,Login,Pass,GetBook,GetRole)" +
                        " VALUES (N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}',N'{7}')"
                        , idstud1 + 1, maskedTextBox15.Text, maskedTextBox14.Text, maskedTextBox13.Text,
                        maskedTextBox12.Text, maskedTextBox11.Text, "0", "Библиотекарь");
                    dataGridView3.Rows.Add(maskedTextBox15.Text, maskedTextBox14.Text, maskedTextBox13.Text, maskedTextBox12.Text, maskedTextBox11.Text, "0", "Библиотекарь");
                    insert.CommandText = str; // выполнение комманды с присваиванием текста
                    int rowaffected = insert.ExecuteNonQuery();// запись данных в базу данных
                    maskedTextBox15.Text = "";
                    maskedTextBox14.Text = "";
                    maskedTextBox13.Text = "";
                    maskedTextBox12.Text = "";
                    maskedTextBox11.Text = "";
                    MessageBox.Show("Аккаунт зарегистрирован!");
                }

            }
            catch
            {
                maskedTextBox10.Text = "";
                maskedTextBox9.Text = "";
                maskedTextBox8.Text = "";
                maskedTextBox7.Text = "";
                maskedTextBox6.Text = "";
                MessageBox.Show("Вы не заполнили данные!");
            }
            connection.Close();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            SqlConnection con = DBUtils.GetDBConnection();
            SqlCommand com = new SqlCommand("DELETE FROM Person WHERE Lastname=@Lastname1", con);// запрос на удаление
            string id1 = dataGridView1.CurrentRow.Cells[0].Value.ToString();// название выбранной книги
            com.Parameters.AddWithValue("@Lastname1", id1);// указание параметра
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
            dataGridView3.Rows.Clear();// обновление таблицы

            SqlConnection connection2 = DBUtils.GetDBConnection(); // Подключение базы данных
            connection2.Open(); // открытие доступа
            SqlCommand select2 = new SqlCommand("SELECT * FROM Person WHERE GetRole = N'Библиотекарь'", connection2); // создание команды
            SqlDataReader reader2 = select2.ExecuteReader(); // создание ридера для чтения данных
            while (reader2.Read()) // прохождение по массиву данных
            {
                for (int i = 0; i < reader2.FieldCount; i++)
                {

                    if (reader2.GetName(i) == "Id")
                    {
                        idstud1 = int.Parse(String.Format("{0}", reader2[i]));// запись данных в переменную
                    }
                    else if (reader2.GetName(i) == "Lastname")
                    {
                        Lastname1 = String.Format("{0}", reader2[i]);// запись данных в переменную
                    }
                    else if (reader2.GetName(i) == "Name")
                    {
                        Name1 = String.Format("{0}", reader2[i]);// запись данных в переменную
                    }
                    else if (reader2.GetName(i) == "Surname")
                    {
                        Surname1 = String.Format("{0}", reader2[i]);// запись данных в переменную
                    }
                    else if (reader2.GetName(i) == "Login")
                    {
                        Login1 = String.Format("{0}", reader2[i]);// запись данных в переменную
                    }
                    else if (reader2.GetName(i) == "Pass")
                    {
                        Pass1 = String.Format("{0}", reader2[i]);// запись данных в переменную
                    }
                    else if (reader2.GetName(i) == "GetBook")
                    {
                        GetBook1 = String.Format("{0}", reader2[i]);// запись данных в переменную
                    }
                    else if (reader2.GetName(i) == "GetRole")
                    {
                        GetRole1 = String.Format("{0}", reader2[i]);// запись данных в переменную
                    }
                }
                dataGridView3.Rows.Add(Lastname1, Name1, Surname1, Login1, Pass1, GetBook1, GetRole1);
            }
            connection2.Close();
        }
    }
}

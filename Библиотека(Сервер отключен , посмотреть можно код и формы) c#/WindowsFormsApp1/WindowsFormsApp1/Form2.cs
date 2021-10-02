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
    public partial class Form2 : Form
    {
        public string login;
        public string pass;
        public string getrole;
        public int id;
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //  Инитиализация формы
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public Form2()
        {
            InitializeComponent();
            
            SqlConnection connection = DBUtils.GetDBConnection(); // Подключение базы данных
            connection.Open(); // открытие доступа
            SqlCommand select = new SqlCommand("SELECT * FROM Person", connection); // создание команды
            SqlDataReader reader = select.ExecuteReader(); // создание ридера для чтения данных
            while (reader.Read()) // прохождение по массиву данных
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {

                    if (reader.GetName(i) == "Id")
                    {
                        id = int.Parse(String.Format("{0}", reader[i]));// запись данных в переменную
                    }
                }
            }
            connection.Close();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //  Регистрация пользователя
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void Button2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            try
            {
                if(maskedTextBox5.Text != "" && maskedTextBox6.Text != "")
                {
                    

                    SqlCommand insert = connection.CreateCommand();
                    string str = string.Format("INSERT Person(Id,Lastname,Name,Surname,Login,Pass,GetBook,GetRole)" +
                        " VALUES (N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}',N'{7}')"
                        , id + 1, maskedTextBox1.Text, maskedTextBox2.Text, maskedTextBox3.Text,
                        maskedTextBox5.Text, maskedTextBox6.Text, "0", "Студент");

                    insert.CommandText = str; // выполнение комманды с присваиванием текста
                    int rowaffected = insert.ExecuteNonQuery();// запись данных в базу данных
                    MessageBox.Show("Аккаунт зарегистрирован!");
                }
                
            }
            catch
            {
                MessageBox.Show("Вы не заполнили данные!");
            }
            connection.Close();
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //  Закрытие формы
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //  Вход в аккаунт
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = DBUtils.GetDBConnection();
                connection.Open();
            // проверка на ввод данных
            try
            {
                
                SqlCommand select = new SqlCommand($"SELECT * FROM Person Where Login = N'{maskedTextBox7.Text}' AND Pass = N'{maskedTextBox8.Text}'", connection);
                SqlDataReader reader = select.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {

                        if (reader.GetName(i) == "Login")
                        {
                            login = String.Format("{0}", reader[i]);
                        }
                        else if (reader.GetName(i) == "Pass")
                        {
                            pass = String.Format("{0}", reader[i]);
                        }
                        else if (reader.GetName(i) == "GetRole")
                        {
                            getrole = String.Format("{0}", reader[i]);
                        }
                        
                    }
                }
                
                    // проверка на администратора
                    if (login == maskedTextBox7.Text)
                    {
                        if (pass == maskedTextBox8.Text)
                        {
                            if (getrole == "Администратор")
                            {
                                connection.Close();
                                this.Hide();
                                Form5 form5 = new Form5(login, pass);
                                form5.ShowDialog();
                            }
                        }
                    }
                    // проверка на студента
                    if (login == maskedTextBox7.Text)
                    {
                        if (pass == maskedTextBox8.Text)
                        {
                            if (getrole == "Студент")
                            {
                                connection.Close();
                                this.Hide();
                                Form4 form4 = new Form4(login, pass);
                                form4.ShowDialog();
                            }
                        }
                    }
                // проверка на Библиотекаря
                if (login == maskedTextBox7.Text)
                {
                    if (pass == maskedTextBox8.Text)
                    {
                        if (getrole == "Библиотекарь")
                        {
                            connection.Close();
                            this.Hide();
                            Form6 form6 = new Form6();
                            form6.ShowDialog();
                        }
                    }
                }

            }
            catch
            {
                connection.Close();
                MessageBox.Show("Вы не заполнили данные или ввели их неправильно!");
            }
            
        }

    }
}

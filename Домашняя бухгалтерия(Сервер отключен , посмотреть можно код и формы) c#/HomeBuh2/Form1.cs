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
namespace Домашняя_бухгалтерия
{
    public partial class Form1 : Form
    {
        //string connectStr = @"Data Source=192.168.0.106\SQLEXPRESS; Initial Catalog=HomeBuh; Integrated Security=True;";
        int id;
        public string dates = " ";
        string type;
        string category;
        string date;
        string commentary;
        double Sum;
        double sum1 = 0;
        double sum2 = 0;
        List<double> itog = new List<double>();
        List<double> itog1 = new List<double>();
        List<double> itogs = new List<double>();// сделано для сохранения общей суммы
        public double sum = 0;// общая сумма
        public Form1()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            string result2 = "";
            string result = "";
            SqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            SqlCommand select = new SqlCommand("SELECT * FROM Finanse1", connection);
            SqlDataReader reader = select.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {

                    if (reader.GetName(i) == "Id")
                    {
                        id = int.Parse(String.Format("{0}", reader[i]));
                    }
                    else if (reader.GetName(i) == "Тип")
                    {
                        type = String.Format("{0}", reader[i]);
                    }
                    else if (reader.GetName(i) == "Категория")
                    {
                        category = String.Format("{0}", reader[i]);
                    }
                    else if (reader.GetName(i) == "Дата")
                    {
                        date = String.Format("{0}", reader[i]);
                    }
                    else if (reader.GetName(i) == "Сумма")
                    {
                        result = String.Format("{0:# ### ###}", reader[i]);
                    }
                    else if (reader.GetName(i) == "Итого")
                    {

                        result2 = String.Format("{0:# ### ###}", reader[i]);
                        string b = String.Format("{0}", reader[i]);
                        double a = double.Parse(b);
                        itogs.Add(a);
                    }
                    else if (reader.GetName(i) == "Комментарий")
                    {
                        commentary = String.Format("{0}", reader[i]);
                    }

                }
                sum = itogs.Last();// приравнивание общей суммы к последнему элементу таблицы
                dataGridView1.Rows.Add(id, type, category, date, result, result2, commentary);
                dataGridView4.Rows.Add(id, type, category, date, result, result2, commentary);
            }

        }

        private void insert_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = DBUtils.GetDBConnection();
                connection.Open();

                id += 1;

                category = comboBox1.SelectedItem.ToString();
                date = dateTimePicker1.Text;
                Sum = int.Parse(textBox5.Text);
                commentary = textBox6.Text;

                if (type == "Доход")
                {
                    string resultat = String.Format("{0:# ### ###}", Sum);
                    sum = sum + Sum;
                    string resultat2 = String.Format("{0:# ### ###}", sum);
                    dataGridView1.Rows.Add(id, type, category, date, resultat, resultat2, commentary);
                    dataGridView4.Rows.Add(id, type, category, date, resultat, resultat2, commentary);
                    SqlCommand insert = connection.CreateCommand();
                    string str = string.Format("INSERT Finanse1(Id,Тип,Категория,Дата,Сумма,Итого,Комментарий,Итогоint) VALUES ('{0}',N'{1}',N'{2}',N'{3}','{4}','{5}',N'{6}','{7}')", id, type, category, date, resultat, resultat2, commentary, Sum);
                    insert.CommandText = str;
                    int rowaffected = insert.ExecuteNonQuery();
                    MessageBox.Show("Данные добавлены");
                }
                else if (type == "Расход")
                {
                    try
                    {
                        if (Sum >= sum) throw new Exception();
                        else
                        {
                            string resultat = String.Format("{0:# ### ###}", Sum);
                            sum = sum - Sum;
                            string resultat2 = String.Format("{0:# ### ###}", sum);
                            dataGridView1.Rows.Add(id, type, category, date, resultat, resultat2, commentary);
                            dataGridView4.Rows.Add(id, type, category, date, resultat, resultat2, commentary);
                            SqlCommand insert = connection.CreateCommand();
                            string str = string.Format("INSERT Finanse1(Id,Тип,Категория,Дата,Сумма,Итого,Комментарий,Итогоint) VALUES ('{0}',N'{1}',N'{2}',N'{3}','{4}','{5}',N'{6}','{7}')", id, type, category, date, resultat, resultat2, commentary, Sum);
                            insert.CommandText = str;
                            int rowaffected = insert.ExecuteNonQuery();
                            MessageBox.Show("Данные добавлены");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Сумма не может быть больше итоговой");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Вы не заполнили данные!");
            }
        }


        private void seacrh_Click(object sender, EventArgs e)
        {

            string searchValue = textBox7.Text;


            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[2].Value.ToString().Equals(searchValue))
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

        private void button4_Click(object sender, EventArgs e)
        {

            dateTimePicker1.Value = DateTime.Now;
            textBox5.Text = "";
            textBox6.Text = "";
            comboBox1.Items.Clear();
        }

        private void Dohod_Click(object sender, EventArgs e)
        {
            type = "Доход";
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Заработная плата");
            comboBox1.Items.Add("Иные доходы");
        }

        private void rasxod_Click(object sender, EventArgs e)
        {
            type = "Расход";
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Продукты питания");
            comboBox1.Items.Add("Транспорт");
            comboBox1.Items.Add("Мобильная связь");
            comboBox1.Items.Add("Интернет");
            comboBox1.Items.Add("Развлечения");
            comboBox1.Items.Add("Другое");
        }
        private void dohod_button_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            try
            {

                if (dateTimePicker2.Text == "Январь")
                {
                    dates = "Января";
                }
                else if (dateTimePicker2.Text == "Февраль")
                {
                    dates = "Февраля";
                }
                else if (dateTimePicker2.Text == "Март")
                {
                    dates = "Марта";
                }
                else if (dateTimePicker2.Text == "Апрель")
                {
                    dates = "Апреля";
                }
                else if (dateTimePicker2.Text == "Май")
                {
                    dates = "Мая";
                }
                else if (dateTimePicker2.Text == "Июнь")
                {
                    dates = "Июня";
                }
                else if (dateTimePicker2.Text == "Июль")
                {
                    dates = "Июля";
                }
                else if (dateTimePicker2.Text == "Август")
                {
                    dates = "Августа";
                }
                else if (dateTimePicker2.Text == "Сентябрь")
                {
                    dates = "Сентября";
                }
                else if (dateTimePicker2.Text == "Октябрь")
                {
                    dates = "Октября";
                }
                else if (dateTimePicker2.Text == "Ноябрь")
                {
                    dates = "Ноября";
                }
                else if (dateTimePicker2.Text == "Декабрь")
                {
                    dates = "Декабря";
                }
                string result2 = "";
                string result = "";
                SqlConnection connection = DBUtils.GetDBConnection();
                connection.Open();
                string command = string.Format("SELECT * FROM Finanse1 WHERE Тип = N'Доход' AND Дата LIKE N'%{0}%'", dates);
                SqlCommand select = new SqlCommand(command, connection);
                SqlDataReader reader = select.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (reader.GetName(i) == "Id")
                        {
                            id = int.Parse(String.Format("{0}", reader[i]));
                        }
                        else if (reader.GetName(i) == "Тип")
                        {
                            type = String.Format("{0}", reader[i]);
                        }
                        else if (reader.GetName(i) == "Категория")
                        {
                            category = String.Format("{0}", reader[i]);
                        }
                        else if (reader.GetName(i) == "Дата")
                        {
                            date = String.Format("{0}", reader[i]);
                        }
                        else if (reader.GetName(i) == "Сумма")
                        {
                            result = String.Format("{0:# ### ###}", reader[i]);
                            string b = String.Format("{0}", reader[i]);
                            double a = double.Parse(b);
                            itog.Add(a);
                            sum1 += a;
                        }
                        else if (reader.GetName(i) == "Итого")
                        {

                            result2 = String.Format("{0:# ### ###}", reader[i]);
                            string b = String.Format("{0}", reader[i]);
                            double a = double.Parse(b);
                        }
                        else if (reader.GetName(i) == "Комментарий")
                        {
                            commentary = String.Format("{0}", reader[i]);
                        }
                    }
                    dataGridView2.Rows.Add(id, type, category, date, result, result2, commentary);

                }
                connection.Close();
                double m = itog.Max();
                label1.Text = String.Format("{0:# ### ###}", sum1);
                label6.Text = String.Format("{0:# ### ###}", m);
            }
            catch
            {
                MessageBox.Show("Нет данных для формирования отчета по доходам!");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            dataGridView3.Rows.Clear();
            try
            {

                if (dateTimePicker3.Text == "Январь")
                {
                    dates = "Января";
                }
                else if (dateTimePicker3.Text == "Февраль")
                {
                    dates = "Февраля";
                }
                else if (dateTimePicker3.Text == "Март")
                {
                    dates = "Марта";
                }
                else if (dateTimePicker3.Text == "Апрель")
                {
                    dates = "Апреля";
                }
                else if (dateTimePicker3.Text == "Май")
                {
                    dates = "Мая";
                }
                else if (dateTimePicker3.Text == "Июнь")
                {
                    dates = "Июня";
                }
                else if (dateTimePicker3.Text == "Июль")
                {
                    dates = "Июля";
                }
                else if (dateTimePicker3.Text == "Август")
                {
                    dates = "Августа";
                }
                else if (dateTimePicker3.Text == "Сентябрь")
                {
                    dates = "Сентября";
                }
                else if (dateTimePicker3.Text == "Октябрь")
                {
                    dates = "Октября";
                }
                else if (dateTimePicker3.Text == "Ноябрь")
                {
                    dates = "Ноября";
                }
                else if (dateTimePicker3.Text == "Декабрь")
                {
                    dates = "Декабря";
                }
                string result2 = "";
                string result = "";
                SqlConnection connection = DBUtils.GetDBConnection();
                connection.Open();
                string command = string.Format("SELECT * FROM Finanse1 WHERE Тип = N'Расход' AND Дата LIKE N'%{0}%'", dates);
                SqlCommand select = new SqlCommand(command, connection);
                SqlDataReader reader = select.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (reader.GetName(i) == "Id")
                        {
                            id = int.Parse(String.Format("{0}", reader[i]));
                        }
                        else if (reader.GetName(i) == "Тип")
                        {
                            type = String.Format("{0}", reader[i]);
                        }
                        else if (reader.GetName(i) == "Категория")
                        {
                            category = String.Format("{0}", reader[i]);
                        }
                        else if (reader.GetName(i) == "Дата")
                        {
                            date = String.Format("{0}", reader[i]);
                        }
                        else if (reader.GetName(i) == "Сумма")
                        {
                            result = String.Format("{0:# ### ###}", reader[i]);
                            string b = String.Format("{0}", reader[i]);
                            double a = double.Parse(b);
                            itog1.Add(a);
                            sum2 += a;
                        }
                        else if (reader.GetName(i) == "Итого")
                        {

                            result2 = String.Format("{0:# ### ###}", reader[i]);
                            string b = String.Format("{0}", reader[i]);
                            double a = double.Parse(b);
                        }
                        else if (reader.GetName(i) == "Комментарий")
                        {
                            commentary = String.Format("{0}", reader[i]);
                        }
                    }
                    dataGridView3.Rows.Add(id, type, category, date, result, result2, commentary);

                }
                connection.Close();

                double m = itog1.Max();
                label16.Text = String.Format("{0:# ### ###}", sum2);
                label15.Text = String.Format("{0:# ### ###}", m);
            }
            catch
            {
                MessageBox.Show("Нет данных для формирования отчета по расходам!");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                string cmd_text;
                Form2 f2 = new Form2();
                int index;
                string num_book;

                index = dataGridView4.CurrentRow.Index;
                num_book = Convert.ToString(dataGridView4[0, index].Value);
                f2.textBox1.Text = num_book;
                f2.textBox3.Text = Convert.ToString(dataGridView4[2, index].Value);
                f2.textBox4.Text = Convert.ToString(dataGridView4[3, index].Value);
                f2.textBox6.Text = Convert.ToString(dataGridView4[6, index].Value);
                if (f2.ShowDialog() == DialogResult.OK)
                {

                    cmd_text = "UPDATE Finanse1 SET Id = " + f2.textBox1.Text + ", " +
                    "[Категория] = N'" + f2.textBox3.Text + "', " +
                    "[Дата] = N'" + f2.textBox4.Text + "', " +
                    "[Комментарий] = N'" + f2.textBox6.Text + "' " +
                    "WHERE Id = " + num_book + "";
                    SqlConnection connection = DBUtils.GetDBConnection();
                    SqlCommand sql_comm = new SqlCommand(cmd_text, connection);

                    connection.Open();
                    sql_comm.ExecuteNonQuery();
                    connection.Close();
                    dataGridView1.Rows.Clear();
                    dataGridView4.Rows.Clear();
                    string result2 = "";
                    string result = "";
                    SqlConnection connection1 = DBUtils.GetDBConnection();
                    connection1.Open();
                    SqlCommand select = new SqlCommand("SELECT * FROM Finanse1", connection1);
                    SqlDataReader reader = select.ExecuteReader();
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {

                            if (reader.GetName(i) == "Id")
                            {
                                id = int.Parse(String.Format("{0}", reader[i]));
                            }
                            else if (reader.GetName(i) == "Тип")
                            {
                                type = String.Format("{0}", reader[i]);
                            }
                            else if (reader.GetName(i) == "Категория")
                            {
                                category = String.Format("{0}", reader[i]);
                            }
                            else if (reader.GetName(i) == "Дата")
                            {
                                date = String.Format("{0}", reader[i]);
                            }
                            else if (reader.GetName(i) == "Сумма")
                            {
                                result = String.Format("{0:# ### ###}", reader[i]);
                            }
                            else if (reader.GetName(i) == "Итого")
                            {

                                result2 = String.Format("{0:# ### ###}", reader[i]);
                                string b = String.Format("{0}", reader[i]);
                                double a = double.Parse(b);
                                itogs.Add(a);
                            }
                            else if (reader.GetName(i) == "Комментарий")
                            {
                                commentary = String.Format("{0}", reader[i]);
                            }
                        }
                        sum = itogs.Last();// приравнивание общей суммы к последнему элементу таблицы
                        dataGridView1.Rows.Add(id, type, category, date, result, result2, commentary);
                        dataGridView4.Rows.Add(id, type, category, date, result, result2, commentary);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Выберите данные для Редактирования!");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = DBUtils.GetDBConnection())
                {
                    con.Open(); //Открываем подключение
                    SqlCommand com1 = new SqlCommand("SELECT * FROM Finanse1 WHERE ID=@Id", con);
                    int id = int.Parse(dataGridView4.CurrentRow.Cells[0].Value.ToString());
                    com1.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = com1.ExecuteReader();
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if (reader.GetName(i) == "Доход")
                            {
                                for (int j = 0; j < reader.FieldCount; j++)
                                {
                                    if (reader.GetName(j) == "Cумма")
                                    {
                                        string b = String.Format("{0}", reader[i]);
                                        double a = double.Parse(b);
                                        sum -= a;
                                        itogs.Add(sum);
                                        sum = itogs.Last();// приравнивание общей суммы к последнему элементу таблицы
                                    }
                                }
                            }
                            else if (reader.GetName(i) == "Расход")
                            {
                                for (int j = 0; j < reader.FieldCount; j++)
                                {
                                    if (reader.GetName(j) == "Сумма")
                                    {
                                        string b = String.Format("{0}", reader[i]);
                                        double a = double.Parse(b);
                                        sum += a;
                                        itogs.Add(sum);
                                        sum = itogs.Last();// приравнивание общей суммы к последнему элементу таблицы
                                    }
                                }
                            }

                        }
                    }
                    con.Close();
                    SqlCommand com = new SqlCommand("DELETE FROM Finanse1 WHERE ID=@Id", con);
                    int id1 = int.Parse(dataGridView4.CurrentRow.Cells[0].Value.ToString());
                    com.Parameters.AddWithValue("@Id", id1);
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
                }
                dataGridView1.Rows.Clear();
                dataGridView4.Rows.Clear();
                string result2 = "";
                string result = "";
                SqlConnection connection1 = DBUtils.GetDBConnection();
                connection1.Open();
                SqlCommand select = new SqlCommand("SELECT * FROM Finanse1", connection1);
                SqlDataReader reader1 = select.ExecuteReader();
                while (reader1.Read())
                {
                    for (int i = 0; i < reader1.FieldCount; i++)
                    {

                        if (reader1.GetName(i) == "Id")
                        {
                            id = int.Parse(String.Format("{0}", reader1[i]));
                        }
                        else if (reader1.GetName(i) == "Тип")
                        {
                            type = String.Format("{0}", reader1[i]);
                        }
                        else if (reader1.GetName(i) == "Категория")
                        {
                            category = String.Format("{0}", reader1[i]);
                        }
                        else if (reader1.GetName(i) == "Дата")
                        {
                            date = String.Format("{0}", reader1[i]);
                        }
                        else if (reader1.GetName(i) == "Сумма")
                        {
                            result = String.Format("{0:# ### ###}", reader1[i]);
                        }
                        else if (reader1.GetName(i) == "Итого")
                        {

                            result2 = String.Format("{0:# ### ###}", reader1[i]);
                            string b = String.Format("{0}", reader1[i]);
                            double a = double.Parse(b);
                            itogs.Add(a);
                        }
                        else if (reader1.GetName(i) == "Комментарий")
                        {
                            commentary = String.Format("{0}", reader1[i]);
                        }
                    }
                    sum = itogs.Last();// приравнивание общей суммы к последнему элементу таблицы
                    dataGridView1.Rows.Add(id, type, category, date, result, result2, commentary);
                    dataGridView4.Rows.Add(id, type, category, date, result, result2, commentary);
                }
            }
            catch
            {
                MessageBox.Show("Выберите данные для удаления!");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text;


            dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    if (row.Cells[2].Value.ToString().Equals(searchValue))
                    {
                        row.Selected = true;
                        dataGridView4.CurrentCell =
                        dataGridView4[0, row.Index];
                        break;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            if (dateTimePicker4.Text == "Январь")
            {
                dates = "Января";
            }
            else if (dateTimePicker4.Text == "Февраль")
            {
                dates = "Февраля";
            }
            else if (dateTimePicker4.Text == "Март")
            {
                dates = "Марта";
            }
            else if (dateTimePicker4.Text == "Апрель")
            {
                dates = "Апреля";
            }
            else if (dateTimePicker4.Text == "Май")
            {
                dates = "Мая";
            }
            else if (dateTimePicker4.Text == "Июнь")
            {
                dates = "Июня";
            }
            else if (dateTimePicker4.Text == "Июль")
            {
                dates = "Июля";
            }
            else if (dateTimePicker4.Text == "Август")
            {
                dates = "Августа";
            }
            else if (dateTimePicker4.Text == "Сентябрь")
            {
                dates = "Сентября";
            }
            else if (dateTimePicker4.Text == "Октябрь")
            {
                dates = "Октября";
            }
            else if (dateTimePicker4.Text == "Ноябрь")
            {
                dates = "Ноября";
            }
            else if (dateTimePicker4.Text == "Декабрь")
            {
                dates = "Декабря";
            }
            string result2 = "";
            string result = "";
            double a = 0;
            string date11 = " ";
            string date12 = " ";
            SqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            string command = string.Format("SELECT * FROM Finanse1 WHERE Тип = N'Доход' AND Дата LIKE N'%{0}%'", dates);
            SqlCommand select = new SqlCommand(command, connection);
            SqlDataReader reader = select.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {

                    if (reader.GetName(i) == "Дата")
                    {
                        date11 = String.Format("{0}", reader[i]);
                    }
                    else if (reader.GetName(i) == "Сумма")
                    {
                        result = String.Format("{0:# ### ###}", reader[i]);
                        string b = String.Format("{0}", reader[i]);
                        a = double.Parse(b);

                    }

                    chart1.Series[0].Points.AddXY(date11, a);
                }
            }
            connection.Close();
            double z = 0;
            SqlConnection connection1 = DBUtils.GetDBConnection();
            connection1.Open();
            string command1 = string.Format("SELECT * FROM Finanse1 WHERE Тип = N'Расход' AND Дата LIKE N'%{0}%'", dates);
            SqlCommand select1 = new SqlCommand(command1, connection1);
            SqlDataReader reader1 = select1.ExecuteReader();
            while (reader1.Read())
            {
                for (int i = 0; i < reader1.FieldCount; i++)
                {
                    if (reader1.GetName(i) == "Дата")
                    {
                        date12 = String.Format("{0}", reader1[i]);
                    }
                    else if (reader1.GetName(i) == "Сумма")
                    {
                        string b = String.Format("{0}", reader1[i]);
                        z = double.Parse(b);

                    }
                    chart1.Series[1].Points.AddXY(date12, z);  
                }
            }
            connection.Close();
        }


    }

}


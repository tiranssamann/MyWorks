using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AviaSales.ItStep
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
            InitializeData();
            addfilter();
            combousers();
            Counts();
            combocity();
        }
        #region Инитиализация данных
        public void InitializeData()
        {

            using (AviaSalesContext context = new AviaSalesContext())
            {
                var query = context.Users.Select(p => new
                {
                    p.Id,
                    p.FullName,
                    p.UserName,
                    p.Password,
                    p.Money,
                    p.Email,
                    p.getTicket,
                    p.IsAdmin
                }).ToList();
                DisplayData1(query.ToList());
                var query1 = context.Tickets.Select(p => new
                {
                    p.Id,
                    p.FirstCountry,
                    p.LastCountry,
                    p.Date,
                    p.Cost
                }).ToList();
                DisplayData(query1.ToList());
                var query2 = context.OnTickets.Select(p => new
                {
                    p.Id,
                    p.Fullnames,
                    p.FullTrail,
                    p.Dateoftrail,
                    p.CostTrail
                }).ToList();
                DisplayData2(query2.ToList());
            }
        }
        public void DisplayData<T>(IEnumerable<T> query) where T : class
        {
            dataGridView1.DataSource = query;
        }
        public void DisplayData1<T>(IEnumerable<T> query) where T : class
        {
            dataGridView2.DataSource = query;
        }
        public void DisplayData2<T>(IEnumerable<T> query) where T : class
        {
            dataGridView3.DataSource = query;
        }
        #endregion
        //_____________________________________________________________________________________________________________________
        #region Решение для таблицы Рейсы
        //_____________________________________________________________________________________________________________________
        public void addfilter()
        {
            Filterbox.Items.Add("Стандарт");
            Filterbox.Items.Add("По конечному пункту");
            Filterbox.Items.Add("По цене(возрастание)");
            Filterbox.Items.Add("По цене(убывание)");

        }
        public void standart()
        {
            InitializeData();
        }
        public void LastCountry()
        {
            using (AviaSalesContext context = new AviaSalesContext())
            {
                var query = from c in context.Tickets
                            orderby c.LastCountry ascending
                            select c;
                DisplayData(query.ToList());
            }
        }
        public void Costasc()
        {
            using (AviaSalesContext context = new AviaSalesContext())
            {
                var query = from c in context.Tickets
                            orderby c.Cost ascending
                            select c;
                DisplayData(query.ToList());
            }
        }
        public void Costdesc()
        {
            using (AviaSalesContext context = new AviaSalesContext())
            {
                var query = from c in context.Tickets
                            orderby c.Cost descending
                            select c;
                DisplayData(query.ToList());
            }
        }
        //_____________________________________________________________________________________________________________________
        // добавление рейса
        //_____________________________________________________________________________________________________________________
        private void button1_Click(object sender, EventArgs e)
        {
            GenericRepository<Ticket> Ticketscompile = new GenericRepository<Ticket>(new AviaSalesContext());
            try
            {
                Ticket ticket = new Ticket { FirstCountry = textBox1.Text, LastCountry = textBox2.Text, Date = textBox3.Text, Cost = double.Parse(textBox4.Text) };
                Ticketscompile.Create(ticket);
                comboBox1.Items.Add(textBox2.Text);
                InitializeData();
            }
            catch { }
        }
        //_____________________________________________________________________________________________________________________
        // удаление выбранного рейса
        //_____________________________________________________________________________________________________________________
        private void button4_Click(object sender, EventArgs e)
        {
             int delId;
             Ticket delete;
             GenericRepository<Ticket> Ticketscompile = new GenericRepository<Ticket>(new AviaSalesContext());
                foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
                { 
                    delId = int.Parse(string.Format("{0}",item.Cells[0].Value));
                    delete = Ticketscompile.FindById(delId);   
                    Ticketscompile.Remove(delete);
                }
             InitializeData();
        }
        //_____________________________________________________________________________________________________________________
        // поиск по всей таблице
        //_____________________________________________________________________________________________________________________
        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox6.Text))
                        {
                            dataGridView1.Rows[i].Selected = true;
                            break;
                        }
            }
        }
        //_____________________________________________________________________________________________________________________
        // поиск по ID
        //_____________________________________________________________________________________________________________________
        private void button2_Click(object sender, EventArgs e)
        {
            string searchValue = textBox5.Text;


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
        //_____________________________________________________________________________________________________________________
        // фильтрация
        //_____________________________________________________________________________________________________________________
        private void button5_Click(object sender, EventArgs e)
        {
            
            if (Filterbox.Text == "Стандарт") standart();
            else if (Filterbox.Text == "По конечному пункту") LastCountry();
            else if (Filterbox.Text == "По цене(возрастание)") Costasc();
            else if (Filterbox.Text == "По цене(убывание)") Costdesc();
        }
        //_____________________________________________________________________________________________________________________
        // изменение Выбранного рейса
        //_____________________________________________________________________________________________________________________
        private void button6_Click(object sender, EventArgs e)
        {
            
            int upId;
            Ticket updates;
            GenericRepository<Ticket> Ticketscompile = new GenericRepository<Ticket>(new AviaSalesContext());

            foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
            {
                UpdateForm form = new UpdateForm(string.Format("{0}", item.Cells[1].Value), string.Format("{0}", item.Cells[2].Value), string.Format("{0}", item.Cells[3].Value), double.Parse(string.Format("{0}", item.Cells[4].Value)));
                if (form.ShowDialog() == DialogResult.OK)
                {
                    upId = int.Parse(string.Format("{0}", item.Cells[0].Value));
                    updates = Ticketscompile.FindById(upId);

                    updates.FirstCountry = form.textBox1.Text;
                    updates.LastCountry = form.textBox2.Text;
                    updates.Date = form.textBox3.Text;
                    updates.Cost = double.Parse(form.textBox4.Text);

                    Ticketscompile.Update(updates);
                }
                InitializeData();
            }
            
        }
        public void combocity()
        {
            comboBox1.Items.AddRange(new string[] { "Dubai", "Almati", "Moscow", "New York", "UK London", "UK London", "Minsk", "Samarkand", "Buhara" });
        }
        private void button8_Click(object sender, EventArgs e)
        {
            using (AviaSalesContext context = new AviaSalesContext())
            {
                var query = from t in context.Tickets
                            where t.LastCountry == comboBox1.Text
                            group t by t.LastCountry;
                List<Ticket> a = new List<Ticket>();
                foreach(IGrouping<string, Ticket> s in query)
                {
                    foreach (var c in s)
                        a.Add(new Ticket { Id = c.Id, FirstCountry = c.FirstCountry, LastCountry = c.LastCountry, Date = c.Date, Cost = c.Cost });
                }
                DisplayData(a);
            }
        }
        #endregion
        //_____________________________________________________________________________________________________________________
        #region Решение для таблицы Пользователи
        //_____________________________________________________________________________________________________________________
        private void btncreateuser_Click(object sender, EventArgs e)
        {
            GenericRepository<User> Usercompile = new GenericRepository<User>(new AviaSalesContext());
            try
            {
                User ticket = new User { FullName = FIOtext.Text, UserName = LoginText.Text
                    , Password = PasswordText.Text, Email = EmailText.Text, IsAdmin = checkBox1.Checked, Money = 0, getTicket = 0 };
                Usercompile.Create(ticket);
                InitializeData();
            }
            catch { }
        }
        //_____________________________________________________________________________________________________________________
        // поиск по ID
        //_____________________________________________________________________________________________________________________
        private void btnsearchuser_Click(object sender, EventArgs e)
        {
            string searchValue = IdSearch.Text;


            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Cells[0].Value.ToString().Equals(searchValue))
                    {
                        row.Selected = true;
                        dataGridView2.CurrentCell =
                        dataGridView2[0, row.Index];
                        break;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        //_____________________________________________________________________________________________________________________
        // поиск по всей таблице
        //_____________________________________________________________________________________________________________________
        private void btnglobalsearch_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                dataGridView2.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                    if (dataGridView2.Rows[i].Cells[j].Value != null)
                        if (dataGridView2.Rows[i].Cells[j].Value.ToString().Contains(GlobalSearch.Text))
                        {
                            dataGridView2.Rows[i].Selected = true;
                            break;
                        }
            }
        }
        //_____________________________________________________________________________________________________________________
        // удаление выбранного Пользователя
        //_____________________________________________________________________________________________________________________
        private void btndeleteuser_Click(object sender, EventArgs e)
        {
            int delId;
            User delete;
            GenericRepository<User> Usercompile = new GenericRepository<User>(new AviaSalesContext());
            foreach (DataGridViewRow item in this.dataGridView2.SelectedRows)
            {
                delId = int.Parse(string.Format("{0}", item.Cells[0].Value));
                delete = Usercompile.FindById(delId);
                Usercompile.Remove(delete);
            }
            InitializeData();
        }
        //_____________________________________________________________________________________________________________________
        // изменение Выбранного Пользователя
        //_____________________________________________________________________________________________________________________
        private void btnupdateuser_Click(object sender, EventArgs e)
        {
            int upId;
            User updates;
            GenericRepository<User> Usercompile = new GenericRepository<User>(new AviaSalesContext());

            foreach (DataGridViewRow item in this.dataGridView2.SelectedRows)
            {
                UpdateUserForm form = new UpdateUserForm(string.Format("{0}", item.Cells[1].Value), string.Format("{0}", item.Cells[2].Value), string.Format("{0}", item.Cells[3].Value),string.Format("{0}", item.Cells[4].Value),false);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    upId = int.Parse(string.Format("{0}", item.Cells[0].Value));
                    updates = Usercompile.FindById(upId);

                    updates.FullName = form.FIOtext.Text;
                    updates.UserName = form.LoginText.Text;
                    updates.Password = form.PasswordText.Text;
                    updates.Email = form.EmailText.Text;
                    updates.IsAdmin = form.checkBox1.Checked;

                    Usercompile.Update(updates);
                }
                InitializeData();
            }
        }
        //_____________________________________________________________________________________________________________________
        // Фильтрация
        //_____________________________________________________________________________________________________________________
        public void combousers()
        {
            usercombo.Items.Add("Стандарт");
            usercombo.Items.Add("По ФИО");
            usercombo.Items.Add("По счету");
            usercombo.Items.Add("По правам"); 
            
        }
        
        public void Fio()
        {
            using (AviaSalesContext context = new AviaSalesContext())
            {
                var query = from c in context.Users
                            orderby c.FullName ascending
                            select c;
                DisplayData1(query.ToList());
            }
        }
        public void Moneys()
        {
            using (AviaSalesContext context = new AviaSalesContext())
            {
                var query = from c in context.Users
                            orderby c.Money ascending
                            select c;
                DisplayData1(query.ToList());
            }
        }
        public void Moneyss()
        {
            using (AviaSalesContext context = new AviaSalesContext())
            {
                var query = from c in context.Users
                            orderby c.Money descending
                            select c;
                DisplayData1(query.ToList());
            }
        }
        public void Isadmin()
        {
            using (AviaSalesContext context = new AviaSalesContext())
            {
                var query = from c in context.Users
                            orderby c.IsAdmin descending
                            select c;
                DisplayData1(query.ToList());
            }
        }
        
        private void btnfiltr_Click(object sender, EventArgs e)
        {
            if (usercombo.Text == "Стандарт") standart();
            else if (usercombo.Text == "По ФИО") Fio();
            else if (usercombo.Text == "По счету(Возрастание)") Moneys();
            else if (usercombo.Text == "По счету(Убывание)") Moneyss();
            else if (usercombo.Text == "По правам") Isadmin();
            
        }
        public void Counts()
        {
            using (AviaSalesContext context = new AviaSalesContext())
            {
                var query = context.Users.Count();
                label9.Text = string.Format("Количество пользователей: {0}",query);
            }
        }
        
        #endregion
        //_____________________________________________________________________________________________________________________
        #region Пользователи с билетами
        //_____________________________________________________________________________________________________________________
        private void button1_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView3.RowCount; i++)
            {
                dataGridView3.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView3.ColumnCount; j++)
                    if (dataGridView3.Rows[i].Cells[j].Value != null)
                        if (dataGridView3.Rows[i].Cells[j].Value.ToString().Contains(textBox7.Text))
                        {
                            dataGridView3.Rows[i].Selected = true;
                            break;
                        }
            }
        }
        
        private void button7_Click(object sender, EventArgs e)
        {
            string searchValue = textBox8.Text;


            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    if (row.Cells[0].Value.ToString().Equals(searchValue))
                    {
                        row.Selected = true;
                        dataGridView3.CurrentCell =
                        dataGridView3[0, row.Index];
                        break;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        #endregion

        
    }
}

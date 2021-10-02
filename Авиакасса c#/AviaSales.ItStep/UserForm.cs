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
    public partial class UserForm : Form
    {
        User log;
        string logs;
        public UserForm(User user)
        {
            InitializeComponent();
            log = user;
            logs = user.FullName;
            InitializeData();
            addfilter();
            
            textBox1.Text = user.UserName;
            textBox2.Text = user.Password;
            textBox3.Text = string.Format("{0}", user.Money);
        }
        public void InitializeData()
        {
            
            using (AviaSalesContext context = new AviaSalesContext())
            {
                var query1 = context.Tickets.Select(p => new
                {
                    p.Id,
                    p.FirstCountry,
                    p.LastCountry,
                    p.Date,
                    p.Cost
                }).ToList();
                DisplayData(query1.ToList());
                var query2 = from ons in context.OnTickets
                             where ons.Fullnames == logs
                             select ons;
                DisplayData1(query2.ToList());
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

        private void button5_Click(object sender, EventArgs e)
        {
            if (Filterbox.Text == "Стандарт") standart();
            else if (Filterbox.Text == "По конечному пункту") LastCountry();
            else if (Filterbox.Text == "По цене(возрастание)") Costasc();
            else if (Filterbox.Text == "По цене(убывание)") Costdesc();
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
        public void addfilter()
        {
            Filterbox.Items.Add("Стандарт");
            Filterbox.Items.Add("По конечному пункту");
            Filterbox.Items.Add("По цене(возрастание)");
            Filterbox.Items.Add("По цене(убывание)");
        }
        //_________________________________________________________________________________________________________________
        private void button1_Click(object sender, EventArgs e)
        {
            GenericRepository<User> Userscompile = new GenericRepository<User>(new AviaSalesContext());
            CashForm form = new CashForm(log);
            if (form.ShowDialog() == DialogResult.OK)
            {
                log.Money += double.Parse(form.textBox5.Text);
                textBox3.Text = string.Format("{0}", log.Money);
                Userscompile.Update(log);
            }
        }

        private void Createbtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (AviaSalesContext context = new AviaSalesContext())
                {
                    GenericRepository<User> Userscompile = new GenericRepository<User>(new AviaSalesContext());
                    User user1 = (from u in context.Users
                                  where u.UserName == textBox1.Text || u.Password == textBox2.Text 
                                  select u)
                                 .FirstOrDefault();
                    int delId;
                    double cost = 0;
                    Ticket delete;
                    GenericRepository<Ticket> Ticketscompile = new GenericRepository<Ticket>(new AviaSalesContext());
                    foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
                    {
                        delId = int.Parse(string.Format("{0}", item.Cells[0].Value));
                        delete = Ticketscompile.FindById(delId);
                        cost = delete.Cost;

                        if (user1.Money != 0 && user1.Money >= cost)
                        {
                            user1.Money -= cost;
                            user1.getTicket += 1;
                            textBox3.Text = string.Format("{0}", user1.Money);
                            Userscompile.Update(user1);
                            CheckSave checkSave = new CheckSave(user1, delete);
                            checkSave.ShowDialog();
                        }
                        else throw new Exception("Недостаточно средств");
                    }
                    
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                dataGridView2.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                    if (dataGridView2.Rows[i].Cells[j].Value != null)
                        if (dataGridView2.Rows[i].Cells[j].Value.ToString().Contains(textBox4.Text))
                        {
                            dataGridView2.Rows[i].Selected = true;
                            break;
                        }
            }
        }
    }
}

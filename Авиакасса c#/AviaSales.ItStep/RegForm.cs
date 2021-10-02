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
    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();
        }

        private void bntReg_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "")
                {
                    using (AviaSalesContext context = new AviaSalesContext()) {
                        GenericRepository<User> Userscompile = new GenericRepository<User>(new AviaSalesContext());
                        User user1 = (from u in context.Users
                                     where u.FullName == textBox1.Text || u.UserName == textBox2.Text || u.Password == textBox3.Text || u.Email == textBox4.Text select u)
                                     .FirstOrDefault();
                        if (user1 != null)
                        {
                            User user4 = new User { FullName = textBox1.Text, UserName = textBox2.Text, Password = textBox3.Text, Email = textBox4.Text, IsAdmin = false, Money = 0, getTicket = 0 };
                            if (user4.FullName != user1.FullName && user4.UserName != user1.UserName && user4.Password != user1.Password && user4.Email != user1.Email)
                            {
                                Userscompile.Create(user4);
                                this.Hide();
                                UserForm userForm = new UserForm(user4);
                                userForm.ShowDialog();
                            }
                            else throw new Exception("Вы не заполнили столбцы или данный пользователь уже существует");
                        }
                        else
                        {
                            User user4 = new User { FullName = textBox1.Text, UserName = textBox2.Text, Password = textBox3.Text, Email = textBox4.Text, IsAdmin = false, Money = 0, getTicket = 0 };
                            Userscompile.Create(user4);
                            this.Hide();
                            UserForm userForm = new UserForm(user4);
                            userForm.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception er){ MessageBox.Show(er.Message); }
        }

        private void RegForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}

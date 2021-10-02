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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                using (AviaSalesContext context = new AviaSalesContext())
                {
                    User userlog = (from u in context.Users
                                    where  u.UserName == textBox2.Text || u.Password == textBox3.Text
                                    select u)
                                     .FirstOrDefault();
                    if(userlog != null)
                    {
                        if (userlog.IsAdmin == true)
                        {
                            this.Hide();
                            AdminForm form = new AdminForm();
                            form.ShowDialog();
                        }
                        else if (userlog.IsAdmin == false)
                        {
                            this.Hide();
                            UserForm userForm = new UserForm(userlog);
                            userForm.ShowDialog();
                        }
                    }
                    
                    else throw new Exception("Неверное имя или пароль");
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}

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
    public partial class CashForm : Form
    {
        public CashForm(User user)
        {
            InitializeComponent();
            textBox1.Text = user.UserName;
            textBox2.Text = user.Password;
            textBox3.Text = string.Format("{0}", user.Money);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox4.Text.Length != 4 && textBox6.Text.Length != 4 && textBox7.Text.Length != 4 && textBox4.Text.Length != 4)
            {
                button1.DialogResult = DialogResult.Cancel;
                MessageBox.Show("Номер карты заполнен неправильно!\n Пример: 4444 5555 6666 7777");
            }
            else button1.DialogResult = DialogResult.OK;
        }
    }
}

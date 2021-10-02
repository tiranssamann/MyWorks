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
    public partial class UpdateUserForm : Form
    {
        public UpdateUserForm(string a, string b, string c, string j,bool k)
        {
            InitializeComponent();
            FIOtext.Text = a;
            LoginText.Text = b;
            PasswordText.Text = c;
            EmailText.Text = j;
            checkBox1.Checked = k;
        }
    }
}

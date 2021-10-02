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
    public partial class UpdateForm : Form
    {
        public UpdateForm(string a,string b,string c,double j)
        {
            InitializeComponent();
            textBox1.Text = a;
            textBox2.Text = b;
            textBox3.Text = c;
            textBox4.Text = string.Format("{0}",j);
        }
    }
}

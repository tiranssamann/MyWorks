using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Домашняя_бухгалтерия
{
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
            button1.Text = "OK";
            button2.Text = "Cancel";
            button1.DialogResult = DialogResult.OK;

        }
    }
}

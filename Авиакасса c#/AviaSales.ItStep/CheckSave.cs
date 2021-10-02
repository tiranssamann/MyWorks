using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
namespace AviaSales.ItStep
{
    public partial class CheckSave : Form
    {
        UserOnTicket userOn;
        public CheckSave(User user1, Ticket ticket1)
        {
            InitializeComponent();
            GenericRepository<UserOnTicket> UserOnTickets = new GenericRepository<UserOnTicket>(new AviaSalesContext());
            userOn = new UserOnTicket 
            { 
                Fullnames = user1.FullName,
                FullTrail = ticket1.FirstCountry + " - " + ticket1.LastCountry,
                Dateoftrail = ticket1.Date,
                CostTrail = ticket1.Cost
            };
            UserOnTickets.Create(userOn);
            listBox1.Items.Add("|---------------------------------------------------|");
            listBox1.Items.Add($"|-------    Номер чека:{userOn.Id}    ");
            listBox1.Items.Add("|ФИО: " + userOn.Fullnames);
            listBox1.Items.Add("|Билет: " + userOn.FullTrail);
            listBox1.Items.Add("|Дата: " + userOn.Dateoftrail);
            listBox1.Items.Add("|Цена билета: " + userOn.CostTrail);
            listBox1.Items.Add("|---------------------------------------------------|" );
            listBox1.Items.Add("|-------    Благодарим за покупку    -------|" );
            listBox1.Items.Add("|---------------------------------------------------|");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") { MessageBox.Show("Укажите путь для сохранения чека"); }
            else
            {
                XElement Check = new XElement("Check_For_Ticket",
                                 new XElement("ФИО", new XAttribute("Покупателя", userOn.Fullnames),
                                 new XElement("Билет", userOn.FullTrail),
                                 new XElement("Дата", userOn.Dateoftrail),
                                 new XElement("Цена_билета", userOn.CostTrail)
                                     )
                                 );
                // Сохранить документ в файл.
                Check.Save(textBox1.Text.ToString() + "Check_For_Ticket"  + ".xml");
            }
        }
    }
}

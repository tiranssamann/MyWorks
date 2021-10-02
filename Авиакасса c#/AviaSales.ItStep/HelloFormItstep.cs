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
    public partial class HelloFormItstep : Form
    {
        public HelloFormItstep()
        {
            InitializeComponent();
            InitializeDataWithRepositiry();
        }
        public void InitializeDataWithRepositiry()
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
                    p.getTicket,
                    p.IsAdmin
                }).ToList();
                var query1 = context.Tickets.Select(p => new
                {
                    p.Id,
                    p.FirstCountry,
                    p.LastCountry,
                    p.Date,
                    p.Cost
                }).ToList();
                var query2 = context.OnTickets.Select(p => new
                {
                    p.Id,
                    p.Fullnames,
                    p.FullTrail,
                    p.Dateoftrail,
                    p.CostTrail
                }).ToList();
                if (query.Count == 0 || query1.Count == 0) InsertUsersAndTickets();
            }
        }
        public void InsertUsersAndTickets()
        {
            GenericRepository<User> Userscompile = new GenericRepository<User>(new AviaSalesContext());
            GenericRepository<Ticket> Ticketscompile = new GenericRepository<Ticket>(new AviaSalesContext());
            List<User> us = new List<User>
            {
                new User { FullName = "Zakarin Artur Eldarovich", UserName = "Tiranssamann", Password = "7779134tira",Email = "TiransSamann@gmail.com", IsAdmin = true, Money = 99999, getTicket = 0 },
                new User { FullName = "test", UserName = "test", Password = "test", Email = "test@gmail.com", IsAdmin = true, Money = 99999, getTicket = 0 },
            };
            List<User> us1 = new List<User>
            {
                new User { FullName = "test1", UserName = "test1", Password = "test1", Email = "test1@gmail.com", IsAdmin = false, Money = 0, getTicket = 0 }
            };

            Userscompile.Create1(us.Union(us1).ToList());

            List<Ticket> t1 = new List<Ticket> {
            new Ticket { FirstCountry = "Tashkent", LastCountry = "Dubai", Date = "2020-11-23", Cost = 900 },
            new Ticket { FirstCountry = "Tashkent", LastCountry = "Almati", Date = "2020-10-15", Cost = 200 },
            new Ticket { FirstCountry = "Tashkent", LastCountry = "Moscow", Date = "2020-12-30", Cost = 250 },
            new Ticket { FirstCountry = "Tashkent", LastCountry = "New York", Date = "2020-10-25", Cost = 1000 }
            };
            List<Ticket> t2 = new List<Ticket> {
            new Ticket { FirstCountry = "Tashkent", LastCountry = "UK London", Date = "2020-11-23", Cost = 800 },
            new Ticket { FirstCountry = "Tashkent", LastCountry = "Minsk", Date = "2020-11-30", Cost = 300 },
            new Ticket { FirstCountry = "Tashkent", LastCountry = "Samarkand", Date = "2020-10-14", Cost = 100 },
            new Ticket { FirstCountry = "Tashkent", LastCountry = "Buhara", Date = "2020-10-20", Cost = 120 }
            };
            
            Ticketscompile.Create1(t1.Union(t2).ToList());
        }
        private void btnReg_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegForm form = new RegForm();
            form.ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm form = new LoginForm();
            form.ShowDialog();
        }
    }
}

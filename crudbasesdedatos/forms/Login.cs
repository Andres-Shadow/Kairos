using crudbasesdedatos.dao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace crudbasesdedatos.forms
{
    public partial class Login : Form
    {
        private LoginDao login;
        public Login()
        {
            InitializeComponent();

            login = new LoginDao();
            login.conectar();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string passwd = txtPasswd.Text;

            bool result = login.loginAdmin(usuario, passwd);

            if (result)
            {
                gestion_admin gestion= new gestion_admin(usuario);
                gestion.Show();
                this.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

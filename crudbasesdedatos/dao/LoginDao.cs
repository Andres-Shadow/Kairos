using crudbasesdedatos.logica;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crudbasesdedatos.dao
{
    internal class LoginDao
    {
        private string servidor = "localhost";
        private string usr = "root";
        private string pswd = "andres_1";
        private string bbdd = "test";

        //CONEXION
        private MySqlConnection cx;
        public void conectar()
        {


            string conexion = "Database=" + bbdd + "; Data Source=" + servidor + "; User Id=" + usr + "; Password=" + pswd + "";

            cx = new MySqlConnection(conexion);
            cx.Open();
            cx.Close();
        }


        public bool loginAdmin(string usuario, string password)
        {
            cx.Open();
            //string consulta = "select * from administrador where usuario=\'"+usuario+ "\'and password=\'"+password+"\'";
            string consulta = "select * from empleado where cedula = \'"+usuario+"\' and password = \'"+password+"\' and tipo = \'admin\'";
            MySqlCommand cmd = new MySqlCommand(consulta, cx);
            MySqlDataReader reader;

            try
            {
                reader = cmd.ExecuteReader();

                if(reader.HasRows)
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            cx.Close();
            return false;
        }

       
    }
}

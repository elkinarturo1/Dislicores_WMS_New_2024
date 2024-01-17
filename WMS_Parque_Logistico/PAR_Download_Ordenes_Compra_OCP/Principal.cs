using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAR_Download_Ordenes_Compra_OCP
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            try
            {
                string strConexionSQL;
                string nomConexionProceso;

                //Definir ambiente
                strConexionSQL = Properties.Settings.Default.strConexionSQL;
                nomConexionProceso = Properties.Settings.Default.nomConexionProceso;

                Download_Configuracion_Inicial.configuracionInicial(strConexionSQL, nomConexionProceso);
                Crear_ASN_PO.ejecutarProceso();

            }
            catch (Exception ex)
            {

            }

            this.Close();
        }
    }
}

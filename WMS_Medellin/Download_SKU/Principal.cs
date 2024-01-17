using Dominio;
using Infraestructure;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Download_SKU
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
                string strConexionOracle;

                //================================================================================
                //Definir ambiente
                if (Properties.Settings.Default.bitPruebas)
                {
                    strConexionSQL = Properties.Settings.Default.strConexionSQL_Pruebas;
                }
                else
                {
                    strConexionSQL = Properties.Settings.Default.strConexionSQL;
                }

                strConexionOracle = Properties.Settings.Default.strConexionOracle;

                //================================================================================


                Asignar_Variables_Globales.asignar_strConexionSQL(strConexionSQL);

                Asignar_Variables_Globales.asignar_strConexionOracle(strConexionOracle);

                Crear_SKU.ejecutarProceso("sp_Download_SKU");


            }
            catch (Exception ex)
            {
                Log_Download_Model objLog = new Log_Download_Model();
                objLog.bitError = true;
                objLog.detalleResultado = ex.Message;

                Logs_DAO.sp_Admin_Log_Download_Insert(objLog);
            }

            this.Close();

        }
    }
}

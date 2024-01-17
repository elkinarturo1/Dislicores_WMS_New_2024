using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UPLOAD_Entrada_TCE_Desde_Tranferencias_TCO
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            string strConexionSQL;

            try
            {

                if(Properties .Settings .Default .bitPruebas)
                {
                    strConexionSQL = Properties.Settings.Default.strConexionSQL_Pruebas;
                }
                else
                {
                    strConexionSQL = Properties.Settings.Default.strConexionSQL;
                }

                Asignar_Variables_Globales.asignar_strConexionSQL(strConexionSQL);
                Asignar_Variables_Globales.asignar_nombreTareaInterface(Properties.Settings.Default.nombreTareaInterface);
                Asignar_Variables_Globales.asignar_procedimientoPrincipal(Properties.Settings.Default.procedimientoPrincipal);

                Asignar_Variables_Globales.asignar_bitEnviar_UNOEE(Properties.Settings.Default.EnviarDatosUNOEE);
                Asignar_Variables_Globales.asignar_cia_UNOEE(Properties.Settings.Default.cia_UNOEE);
                Asignar_Variables_Globales.asignar_conexion_UNOEE(Properties.Settings.Default.conexion_UNOEE);
                Asignar_Variables_Globales.asignar_usuario_UNOEE(Properties.Settings.Default.usuario_UNOEE);
                Asignar_Variables_Globales.asignar_clave_UNOEE(Properties.Settings.Default.clave_UNOEE);
                Asignar_Variables_Globales.asignar_rutaPlanos(Properties.Settings.Default.rutaPlanosGT);

                DataSet ds = new DataSet();
                ConsultarDatos.consultar(ref ds);

                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow documento in ds.Tables[0].Rows)
                    {

                        Contexto_Upload_WMS_Siesa contexto_Upload_WMS_Siesa = new Contexto_Upload_WMS_Siesa();

                        Thread AddThread1 = new Thread(new ThreadStart(contexto_Upload_WMS_Siesa.ejecutar_Hilo));
                        AddThread1.Start(identificador1);

                        System.Threading.Thread.Sleep(3000);
                    }
                }

            }
            catch (Exception ex)
            {

            }

            this.Close();

        }
    }
}

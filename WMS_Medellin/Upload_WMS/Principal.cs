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

namespace Upload_WMS
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
                string proceso;
                IUpload objEnvio = new Upload_Ventas_Facturar_FV2();
                string[] arguments = Environment.GetCommandLineArgs();


                //================================================================================
                //Capturar argumentos
                if (arguments.Length > 1)
                {
                    proceso = arguments[1];
                }
                else
                {
                    proceso = "4";
                }
                //================================================================================


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

                //strConexionOracle = Properties.Settings.Default.strConexionOracle;

                //================================================================================


                Asignar_Variables_Globales.asignar_strConexionSQL(strConexionSQL);

                //Asignar_Variables_Globales.asignar_strConexionOracle(strConexionOracle);


                Asignar_Variables_Globales.asignar_bitEnviar_UNOEE(Properties.Settings.Default.EnviarDatosUNOEE);
                Asignar_Variables_Globales.asignar_cia_UNOEE(Properties.Settings.Default.cia_UNOEE);
                Asignar_Variables_Globales.asignar_conexion_UNOEE(Properties.Settings.Default.conexion_UNOEE);
                Asignar_Variables_Globales.asignar_usuario_UNOEE(Properties.Settings.Default.usuario_UNOEE);
                Asignar_Variables_Globales.asignar_clave_UNOEE(Properties.Settings.Default.clave_UNOEE);
                Asignar_Variables_Globales.asignar_rutaPlanos(Properties.Settings.Default.rutaPlanosGT);


                //================================================================================
                //Ejecutar Download
                switch (proceso)
                {
                    case "1": //Pedidos_PEV
                        Asignar_Variables_Globales.asignar_nombreTareaInterface("BOG_UPLOAD_Ventas_Pedidos_Comprometer_Remisionar");
                        Asignar_Variables_Globales.asignar_procedimientoPrincipal("sp_BOG_UPLOAD_Ventas_Pedidos_Comprometer_Remisionar_PEV");
                        objEnvio = new Upload_Ventas_Pedidos_Comprometer_PEV();                       
                        break;

                    case "2": //Facturas_FV2
                        Asignar_Variables_Globales.asignar_nombreTareaInterface("BOG_UPLOAD_Ventas_Facturar_FV2");
                        Asignar_Variables_Globales.asignar_procedimientoPrincipal("sp_BOG_UPLOAD_Ventas_Facturar_FV2");
                        objEnvio = new Upload_Ventas_Facturar_FV2();
                        break;

                    case "3": //Ordenes_Compra_OCP
                        Asignar_Variables_Globales.asignar_nombreTareaInterface("BOG_UPLOAD_Entrada_ECP_Desde_OCP");
                        Asignar_Variables_Globales.asignar_procedimientoPrincipal("sp_BOG_UPLOAD_Entrada_ECP_Desde_OCP");
                        objEnvio = new Upload_Entradas_ECP_Desde_OCP();
                        break;

                    case "4": //Transferencias_TCO
                        Asignar_Variables_Globales.asignar_nombreTareaInterface("BOG_UPLOAD_Entrada_TCE_Desde_Tranferencias_TCO");
                        Asignar_Variables_Globales.asignar_procedimientoPrincipal("sp_BOG_UPLOAD_Entrada_TCE_Desde_Tranferencias_TCO");
                        objEnvio = new Upload_Entrada_TCE_Desde_Tranferencias_TCO();
                        break;

                    case "5": //Transferencias_Directas_TWA
                        Asignar_Variables_Globales.asignar_nombreTareaInterface("BOG_UPLOAD_Transferencias_Directas_TWA");
                        Asignar_Variables_Globales.asignar_procedimientoPrincipal("sp_BOG_UPLOAD_Transferencias_Directas_TWA");
                        objEnvio = new Upload_Transferencias_Directas_TWA();
                        break;

                    case "6": //Ajustes_AIC
                        Asignar_Variables_Globales.asignar_nombreTareaInterface("BOG_UPLOAD_Ajustes_AIC");
                        Asignar_Variables_Globales.asignar_procedimientoPrincipal("sp_BOG_UPLOAD_Ajustes_AIC");
                        objEnvio = new Upload_Ajustes_AIC();
                        break;

                    case "7": //Transferencias_Desde_Requisicion_RQT
                        Asignar_Variables_Globales.asignar_nombreTareaInterface("BOG_UPLOAD_Transferencia_Desde_Requisicion_RQT");
                        Asignar_Variables_Globales.asignar_procedimientoPrincipal("sp_BOG_UPLOAD_Transferencia_TCO_Desde_Requisicion_RQT");
                        objEnvio = new Upload_Transferencia_TCO_Desde_Requisicion_RQT();
                        break;

                    case "8": //Transferencias_Desde_Requisicion_RQV
                        Asignar_Variables_Globales.asignar_nombreTareaInterface("BOG_UPLOAD_Transferencia_Desde_Requisicion_RQV");
                        Asignar_Variables_Globales.asignar_procedimientoPrincipal("sp_BOG_UPLOAD_Transferencia_TBV_Desde_Requisicion_RQV");
                        objEnvio = new Upload_Transferencia_TBV_Desde_Requisicion_RQV();
                        break;

                    case "9": //Notas_NV2_Desde_PDC
                        Asignar_Variables_Globales.asignar_nombreTareaInterface("BOG_UPLOAD_Notas_NV2_Desde_PDC");
                        Asignar_Variables_Globales.asignar_procedimientoPrincipal("sp_BOG_UPLOAD_Nota_NV2_Desde_PDC");
                        objEnvio = new Upload_Entradas_Notas_NV2_Desde_PDC();
                        break;

                    default:
                        break;
                }
                //================================================================================

                Upload_WMS_SIESA objUpload = new Upload_WMS_SIESA(objEnvio);
                objUpload.ejecutar();

            }
            catch (Exception ex)
            {
                //Log_Download_Model objLog = new Log_Download_Model();
                //objLog.bitError = true;
                //objLog.detalleResultado = ex.Message;

                //Logs_DAO.sp_Admin_Log_Download_Insert(objLog);
            }

            this.Close();

        }
    }
}

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

namespace Download_WMS
{
    public partial class Principal : Form
    {

        DataSet dsOracle = new DataSet();
        DataSet dsSQL = new DataSet();
        DataSet dsConfiguracion = new DataSet();

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
                string parametro = "";

                string vista_Oracle="";
                string tabla_temporal_SQL = "";
                string sp_consultaPrincipal = "";
                string proceso_WMS = "";
                bool bitEncendida = false;

                string[] arguments = Environment.GetCommandLineArgs();


                //================================================================================
                //Capturar argumentos
                if (arguments.Length > 1)
                {
                    parametro = arguments[1];
                }
                else
                {
                    parametro = "1";
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

                strConexionOracle = Properties.Settings.Default.strConexionOracle;

                //================================================================================


                Asignar_Variables_Globales.asignar_strConexionSQL(strConexionSQL);

                Asignar_Variables_Globales.asignar_strConexionOracle(strConexionOracle);



                //================================================================================
                //Ejecutar Download
                
                dsConfiguracion  = OperationsGenericsSQL_DAO.sp_Admin_Download_Interfaces(int.Parse(parametro));

                if (dsConfiguracion.Tables.Count > 0)
                {
                    vista_Oracle = dsConfiguracion.Tables[0].Rows[0]["vista_Oracle"].ToString();
                    tabla_temporal_SQL = dsConfiguracion.Tables[0].Rows[0]["tabla_temporal_SQL"].ToString();
                    sp_consultaPrincipal = dsConfiguracion.Tables[0].Rows[0]["sp_consultaPrincipal"].ToString();
                    proceso_WMS = dsConfiguracion.Tables[0].Rows[0]["proceso_WMS"].ToString();
                    bitEncendida = bool.Parse(dsConfiguracion.Tables[0].Rows[0]["bitEncendida"].ToString());
                }


                if (bitEncendida)
                {

                    capturarDatos(vista_Oracle, tabla_temporal_SQL, sp_consultaPrincipal);

                    switch (proceso_WMS)
                    {
                        case "OEX":
                            Crear_OEX.ejecutarProceso(ref dsSQL);
                            break;

                        case "ASN":
                            Crear_ASN_Receipt.ejecutarProceso(ref dsSQL);
                            break;

                        case "PO":
                            Crear_ASN_PO.ejecutarProceso(ref dsSQL);
                            break;

                        default:
                            break;

                    }

                }

                //switch (proceso)
                //{
                //    case "1":
                //        capturarDatos("V_BOG_DOWNLOAD_PEDIDOS_PEV", "T_BOG_Download_Pedidos_PEV", "sp_BOG_Download_Pedidos_PEV");
                //        Crear_OEX.ejecutarProceso(dsSQL);
                //        break;

                //    case "2":
                //        capturarDatos("V_BOG_DOWNLOAD_PEDIDOS_PEE", "T_BOG_Download_Pedidos_PEE", "sp_BOG_Download_Pedidos_PEE");
                //        Crear_OEX.ejecutarProceso(dsSQL);
                //        break;

                //    case "3":
                //        capturarDatos("V_BOG_DOWNLOAD_OCP", "T_BOG_Download_Ordenes_Compra_OCP", "sp_BOG_Download_Ordenes_Compra_OCP");
                //        Crear_ASN_PO.ejecutarProceso(ref dsSQL);
                //        break;

                //    case "4":
                //        capturarDatos("V_BOG_DOWNLOAD_TRANSFER_TCO", "T_BOG_Download_Transferencias_TCO", "sp_BOG_Download_Ordenes_Compra_OCP");
                //        Crear_ASN_PO.ejecutarProceso(ref dsSQL);
                //        break;

                //    case "5":
                //        capturarDatos("V_BOG_DOWNLOAD_DEVOLUCION_PDC", "T_BOG_Download_Devoluciones_PDC", "sp_BOG_Download_Devoluciones_PDC");
                //        Crear_ASN_PO.ejecutarProceso(ref dsSQL);
                //        break;

                //    case "6":
                //        capturarDatos("V_BOG_DOWNLOAD_DEVOLUCION_SDP", "T_BOG_Download_Devoluciones_Proveedor_SDP", "sp_BOG_Download_Devoluciones_Proveedor_SDP");
                //        Crear_ASN_PO.ejecutarProceso(ref dsSQL);
                //        break;

                //    case "7":
                //        capturarDatos("V_BOG_DOWNLOAD_RECHAZO_EDE", "T_BOG_Download_Rechazo_Factura_EDE_DNF", "sp_BOG_Download_Rechazo_Factura_EDE_DNF");
                //        Crear_ASN_PO.ejecutarProceso(ref dsSQL);
                //        break;

                //    case "8":
                //        capturarDatos("V_BOG_DOWNLOAD_REQUISICION_RQV", "T_BOG_Download_Requisiciones_RQV", "sp_BOG_Download_Requisiciones_RQV");
                //        Crear_ASN_PO.ejecutarProceso(ref dsSQL);
                //        break;

                //    case "9":
                //        capturarDatos("V_BOG_DOWNLOAD_REQUISICION_RQT", "T_BOG_Download_Requisiciones_RQT", "sp_BOG_Download_Requisiciones_RQT");
                //        Crear_ASN_PO.ejecutarProceso(ref dsSQL);
                //        break;

                //    default:
                //        break;
                //}
                ////================================================================================

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


        /// <summary>
        /// Ejecuta el proceso de Download
        /// </summary>
        /// <param name="vista_Oracle">Consulta los datos en Siesa Originales que se bajaran a WMS</param>
        /// <param name="tabla_temporal">Tabla donde se guardaran temporalmente los pedidos</param>
        /// <param name="sp_consultaPrincipal">procedimiento SQL que trae el diferencial entre siesa y wms</param>
        private void capturarDatos(string vista_Oracle = "", string tabla_temporal = "", string sp_consultaPrincipal = "")
        {

            try
            {
                //Consulto Datos Oracle
                dsOracle = Oracle_DAO.consultar_Vista_Oracle(vista_Oracle);

                //Limpio la tabla temporal
                OperationsGenericsSQL_DAO.ejecucion_Query($"truncate table {tabla_temporal}");

                //Guardo los datos en la tabla temporal
                Orders_DAO.Guardar_en_BD_x_BulkCopy(dsOracle.Tables[0], tabla_temporal);

                //Consulto el diferencial
                dsSQL = OperationsGenericsSQL_DAO.ejecucion_procedimiento_principal(ref sp_consultaPrincipal);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}

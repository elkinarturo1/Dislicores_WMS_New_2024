using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure
{
    public class Logs_DAO
    {

        public static void sp_Admin_Log_Download_Insert(Log_Download_Model objLog)
        {

            SqlConnection sqlConexion = new SqlConnection(VariablesGlobales.strConexionSQL);
            SqlCommand sqlComando = new SqlCommand();

            try
            {

                sqlComando.Connection = sqlConexion;
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.CommandText = "sp_Admin_Log_Download_Insert";
                sqlComando.CommandTimeout = 0;

                sqlComando.Parameters.AddWithValue("@bitError", objLog.bitError);
                sqlComando.Parameters.AddWithValue("@identificador1", objLog.identificador1);
                sqlComando.Parameters.AddWithValue("@identificador2", objLog.identificador2);
                sqlComando.Parameters.AddWithValue("@cia", objLog.cia);
                sqlComando.Parameters.AddWithValue("@co", objLog.co);
                sqlComando.Parameters.AddWithValue("@tipoDoc", objLog.tipoDoc);
                sqlComando.Parameters.AddWithValue("@numDoc", objLog.numDoc);
                sqlComando.Parameters.AddWithValue("@datosEnviados", objLog.datosEnviados);
                sqlComando.Parameters.AddWithValue("@detalleResultado", objLog.detalleResultado);

                sqlComando.Connection.Open();
                sqlComando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                sqlComando.Parameters.Clear();
                sqlComando.Connection.Close();
            }
        }


        public static void sp_Admin_Log_Upload_Insert(EstructuraUploadModel objLog)
        {

            SqlConnection sqlConexion = new SqlConnection(VariablesGlobales.strConexionSQL_Proceso);
            SqlCommand sqlComando = new SqlCommand();

            try
            {

                sqlComando.Connection = sqlConexion;
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.CommandText = "sp_Admin_Log_Upload_Insert";
                sqlComando.CommandTimeout = 0;

                sqlComando.Parameters.AddWithValue("@bitError", objLog.bitError);
                sqlComando.Parameters.AddWithValue("@identificador1", objLog.identificador1);
                sqlComando.Parameters.AddWithValue("@identificador2", objLog.identificador2);
                sqlComando.Parameters.AddWithValue("@cia", objLog.cia_Origen);
                sqlComando.Parameters.AddWithValue("@co", objLog.co_Origen);
                sqlComando.Parameters.AddWithValue("@tipoDoc", objLog.tipoDoc_Origen);
                sqlComando.Parameters.AddWithValue("@numDoc", objLog.numDoc_Origen);
                sqlComando.Parameters.AddWithValue("@datosEnviados_GT", objLog.datosEnviados_GT);
                sqlComando.Parameters.AddWithValue("@resultado_GT", objLog.resultado_GT);
                sqlComando.Parameters.AddWithValue("@detalleResultado_GT", objLog.detalleResultado_GT);
                sqlComando.Parameters.AddWithValue("@datosEnviados_UNOEE", objLog.datosEnviados_UNOEE);
                sqlComando.Parameters.AddWithValue("@resultado_UNOEE", objLog.resultado_UNOEE);
                sqlComando.Parameters.AddWithValue("@detalleResultado_UNOEE", objLog.detalleResultado_UNOEE);
                sqlComando.Parameters.AddWithValue("@otrosDetalles", objLog.otrosDetalles);

                sqlComando.Connection.Open();
                sqlComando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlComando.Parameters.Clear();
                sqlComando.Connection.Close();
            }
        }

        public static void sp_Documentos_Rechazados(string Paquete, string Detalle,string Tabla)
        {

            SqlConnection sqlConexion = new SqlConnection(VariablesGlobales.strConexionSQL_Proceso);
            SqlCommand sqlComando = new SqlCommand();

            try
            {

                sqlComando.Connection = sqlConexion;
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.CommandText = "sp_Rechazo_Upload_Insert";
                sqlComando.CommandTimeout = 0;

                sqlComando.Parameters.AddWithValue("@tabla", Tabla);
                sqlComando.Parameters.AddWithValue("@paquete", Paquete);
                sqlComando.Parameters.AddWithValue("@detalle", Detalle);

                sqlComando.Connection.Open();
                sqlComando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlComando.Parameters.Clear();
                sqlComando.Connection.Close();
            }
        }

    }
}

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
    public static class OperationsGenericsSQL_DAO
    {

        public static DataSet ejecucion_Query(string strQuery)
        {
            SqlConnection sqlConexion = new SqlConnection(VariablesGlobales.strConexionSQL);
            SqlDataAdapter sqlAdaptador = new SqlDataAdapter();
            SqlCommand sqlComando = new SqlCommand();
            DataSet ds = new DataSet();

            try
            {
                sqlComando.Connection = sqlConexion;
                sqlComando.CommandType = CommandType.Text;
                sqlComando.CommandText = strQuery;
                sqlComando.CommandTimeout = 0;

                sqlConexion.Open();
                sqlComando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConexion.Close();
                sqlComando.Parameters.Clear();
                sqlComando.Connection.Close();
            }

            return ds;
        }


        public static DataSet ejecucion_procedimiento_principal()
        {
            SqlConnection sqlConexion = new SqlConnection(VariablesGlobales.strConexionSQL_Proceso);
            SqlDataAdapter sqlAdaptador = new SqlDataAdapter();
            SqlCommand sqlComando = new SqlCommand();
            DataSet ds = new DataSet();

            try
            {
                sqlComando.Connection = sqlConexion;
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.CommandText = VariablesGlobales.procedimientoPrincipal;
                sqlComando.CommandTimeout = 0;
                
                sqlAdaptador.SelectCommand = sqlComando;
                sqlAdaptador.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConexion.Close();
                sqlComando.Parameters.Clear();
                sqlComando.Connection.Close();
            }

            return ds;
        }


        public static DataSet sp_Admin_Download_Interfaces(int parametro)
        {
            SqlConnection sqlConexion = new SqlConnection(VariablesGlobales.strConexionSQL);
            SqlDataAdapter sqlAdaptador = new SqlDataAdapter();
            SqlCommand sqlComando = new SqlCommand();
            DataSet ds = new DataSet();

            try
            {
                sqlComando.Connection = sqlConexion;
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.CommandText = "sp_Admin_Download_Interfaces";
                sqlComando.CommandTimeout = 0;

                sqlComando.Parameters.AddWithValue("parametro", parametro);

                sqlConexion.Open();
                sqlAdaptador.SelectCommand = sqlComando;
                sqlAdaptador.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConexion.Close();
                sqlComando.Parameters.Clear();
                sqlComando.Connection.Close();
            }

            return ds;
        }


        public static (bool bitEncendida, string urlWebServiceUNOEE) sp_Admin_Consultar_Url_WSUNOEE()
        {
            SqlConnection sqlConexion = new SqlConnection(VariablesGlobales.strConexionSQL_Proceso);
            SqlDataAdapter sqlAdaptador = new SqlDataAdapter();
            SqlCommand sqlComando = new SqlCommand();
            DataSet ds = new DataSet();
            bool bitEncendida = false;
            string urlWebServiceUNOEE = "";


            try
            {
                sqlComando.Connection = sqlConexion;
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.CommandText = "sp_Admin_Consultar_Url_WSUNOEE";
                sqlComando.CommandTimeout = 0;

                sqlComando.Parameters.AddWithValue("@interfaz", VariablesGlobales.nombreTareaInterface);

                sqlAdaptador.SelectCommand = sqlComando;
                sqlAdaptador.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        bitEncendida = (bool)ds.Tables[0].Rows[0]["bitEncendida"];
                        urlWebServiceUNOEE = (string)ds.Tables[0].Rows[0]["urlWebServiceUNOEE"];
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConexion.Close();
                sqlComando.Parameters.Clear();
                sqlComando.Connection.Close();
            }

            return (bitEncendida, urlWebServiceUNOEE);

        }

    }
}

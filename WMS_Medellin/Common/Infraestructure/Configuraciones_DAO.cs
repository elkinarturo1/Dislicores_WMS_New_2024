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
    public static class Configuraciones_DAO
    {

        public static (bool,string) sp_Enrutamiento_WebService_Select(string strInterface)
        {

            SqlConnection sqlConexion = new SqlConnection(VariablesGlobales.strConexionSQL);
            SqlCommand sqlComando = new SqlCommand();
            SqlDataAdapter sqlAdaptador = new SqlDataAdapter();
            DataSet ds = new DataSet();

            bool bitEncendida = false;
            string strConexion = "";

            try
            {

                sqlComando.Connection = sqlConexion;
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.CommandText = "sp_Enrutamiento_WebService_Select";
                sqlComando.CommandTimeout = 0;

                sqlComando.Parameters.AddWithValue("@interface", strInterface);  

                sqlAdaptador.SelectCommand = sqlComando;
                sqlAdaptador.Fill(ds);

                if (ds.Tables .Count > 0)
                {
                    if (ds.Tables[0].Rows .Count > 0)
                    {
                        bitEncendida = bool.Parse(ds.Tables[0].Rows[0]["bitEncendida"].ToString());
                        strConexion = ds.Tables[0].Rows[0]["urlWebServiceUNOEE"].ToString();
                    }
                }

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


            return (bitEncendida, strConexion);

        }




        public static (string,string) sp_Conexiones_Select()
        {

            SqlConnection sqlConexion = new SqlConnection(VariablesGlobales.strConexionSQL);
            SqlCommand sqlComando = new SqlCommand();
            SqlDataAdapter sqlAdaptador = new SqlDataAdapter();
            DataSet ds = new DataSet();

            string strConexionProceso = "";
            string sp_consultaPrincipal = "";

            try
            {

                sqlComando.Connection = sqlConexion;
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.CommandText = "sp_Conexiones_Select";
                sqlComando.CommandTimeout = 0;

                sqlComando.Parameters.AddWithValue("@nomConexionProceso", VariablesGlobales.nomConexionProceso);

                sqlAdaptador.SelectCommand = sqlComando;
                sqlAdaptador.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        strConexionProceso = ds.Tables[0].Rows[0]["strConexion"].ToString();
                        sp_consultaPrincipal = ds.Tables[0].Rows[0]["sp_consultaPrincipal"].ToString();
                    }
                }

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


            return (strConexionProceso, sp_consultaPrincipal);

        }



    }
}

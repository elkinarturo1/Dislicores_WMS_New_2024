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
    public static class SKU_DAO
    {
        public static void sp_ION_Limpiar_SKU(string id)
        {

            SqlConnection sqlConexion = new SqlConnection(VariablesGlobales.strConexionSQL);
            SqlCommand sqlComando = new SqlCommand();

            try
            {

                sqlComando.Connection = sqlConexion;
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.CommandText = "sp_ION_Limpiar_SKU";
                sqlComando.CommandTimeout = 0;

                sqlComando.Parameters.AddWithValue("@SKU", id);

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


        public static void sp_ION_Download_INT_SKU(clsModelo_SKU objDatos)
        {

            SqlConnection sqlConexion = new SqlConnection(VariablesGlobales.strConexionSQL);
            SqlCommand sqlComando = new SqlCommand();

            try
            {

                sqlComando.Connection = sqlConexion;
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.CommandText = "sp_ION_Download_INT_SKU";
                sqlComando.CommandTimeout = 0;

                sqlComando.Parameters.AddWithValue("@SKU", objDatos.SKU);
                sqlComando.Parameters.AddWithValue("@BUSR1", objDatos.BUSR1);
                sqlComando.Parameters.AddWithValue("@BUSR2", objDatos.BUSR2);
                sqlComando.Parameters.AddWithValue("@BUSR3", objDatos.BUSR3);
                sqlComando.Parameters.AddWithValue("@BUSR4", objDatos.BUSR4);
                sqlComando.Parameters.AddWithValue("@BUSR5", objDatos.BUSR5);
                sqlComando.Parameters.AddWithValue("@STORERKEY", objDatos.STORERKEY);
                sqlComando.Parameters.AddWithValue("@DESCR", objDatos.DESCR);
                sqlComando.Parameters.AddWithValue("@PACKKEY", objDatos.PACKKEY);
                sqlComando.Parameters.AddWithValue("@STDCUBE", objDatos.STDCUBE);
                sqlComando.Parameters.AddWithValue("@STDGROSSWGT", objDatos.STDGROSSWGT);
                sqlComando.Parameters.AddWithValue("@STDNETWGT", objDatos.STDNETWGT);
                sqlComando.Parameters.AddWithValue("@TARE", objDatos.TARE);
                sqlComando.Parameters.AddWithValue("@SHELFLIFEINDICATOR", objDatos.SHELFLIFEINDICATOR);
                sqlComando.Parameters.AddWithValue("@SHELFLIFEONRECEIVING", objDatos.SHELFLIFEONRECEIVING);
                sqlComando.Parameters.AddWithValue("@SHELFLIFE", objDatos.SHELFLIFE);
                sqlComando.Parameters.AddWithValue("@SHELFLIFECODETYPE", objDatos.SHELFLIFECODETYPE);

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

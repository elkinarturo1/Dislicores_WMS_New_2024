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
    public static class Storer_DAO
    {

        public static void sp_ION_Limpiar_STORERS(string id)
        {

            SqlConnection sqlConexion = new SqlConnection(VariablesGlobales.strConexionSQL);
            SqlCommand sqlComando = new SqlCommand();

            try
            {

                sqlComando.Connection = sqlConexion;
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.CommandText = "sp_ION_Limpiar_STORERS";
                sqlComando.CommandTimeout = 0;

                sqlComando.Parameters.AddWithValue("@STORERKEY", id);

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


        public static void sp_ION_Download_INT_STORER(clsModelo_Storers objDatos)
        {

            SqlConnection sqlConexion = new SqlConnection(VariablesGlobales.strConexionSQL);
            SqlCommand sqlComando = new SqlCommand();

            try
            {

                sqlComando.Connection = sqlConexion;
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.CommandText = "sp_ION_Download_INT_STORER";
                sqlComando.CommandTimeout = 0;

                sqlComando.Parameters.AddWithValue("@STORERKEY", objDatos.STORERKEY);
                sqlComando.Parameters.AddWithValue("@TYPE", objDatos.TYPE);
                sqlComando.Parameters.AddWithValue("@COMPANY ", objDatos.COMPANY);
                sqlComando.Parameters.AddWithValue("@ADDRESS1", objDatos.ADDRESS1);
                sqlComando.Parameters.AddWithValue("@ADDRESS5", objDatos.ADDRESS5);
                sqlComando.Parameters.AddWithValue("@ADDRESS6", objDatos.ADDRESS6);
                sqlComando.Parameters.AddWithValue("@ADDRESS2", objDatos.ADDRESS2);
                sqlComando.Parameters.AddWithValue("@CITY", objDatos.CITY);
                sqlComando.Parameters.AddWithValue("@STATE", objDatos.STATE);
                sqlComando.Parameters.AddWithValue("@COUNTRY", objDatos.COUNTRY);
                sqlComando.Parameters.AddWithValue("@CONTACT1", objDatos.CONTACT1);
                sqlComando.Parameters.AddWithValue("@PHONE1", objDatos.PHONE1);
                sqlComando.Parameters.AddWithValue("@PHONE2", objDatos.PHONE2);
                sqlComando.Parameters.AddWithValue("@EMAIL1", objDatos.EMAIL1);
                sqlComando.Parameters.AddWithValue("@SUSR1", objDatos.SUSR1);
                sqlComando.Parameters.AddWithValue("@SUSR2", objDatos.SUSR2);
                sqlComando.Parameters.AddWithValue("@SUSR3", objDatos.SUSR3);
                sqlComando.Parameters.AddWithValue("@SUSR4", objDatos.SUSR4);
                sqlComando.Parameters.AddWithValue("@SUSR5", objDatos.SUSR5);
                sqlComando.Parameters.AddWithValue("@SUSR6", objDatos.SUSR6);

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

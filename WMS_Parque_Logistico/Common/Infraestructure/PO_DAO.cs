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
    public class PO_DAO
    {
        public static void sp_ION_Limpiar_PO(string id)
        {

            SqlConnection sqlConexion = new SqlConnection(VariablesGlobales.strConexionSQL_Proceso);
            SqlCommand sqlComando = new SqlCommand();

            try
            {

                sqlComando.Connection = sqlConexion;
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.CommandText = "sp_ION_Limpiar_PO";
                sqlComando.CommandTimeout = 0;

                sqlComando.Parameters.AddWithValue("@EXTERNPOKEY", id);

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


        public static void sp_ION_Download_INT_PO(clsModelo_PO objHeader)
        {

            SqlConnection sqlConexion = new SqlConnection(VariablesGlobales.strConexionSQL_Proceso);
            SqlCommand sqlComando = new SqlCommand();

            try
            {

                sqlComando.Connection = sqlConexion;
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.CommandText = "sp_ION_Download_INT_PO";
                sqlComando.CommandTimeout = 0;

                sqlComando.Parameters.AddWithValue("@EXTERNPOKEY", objHeader.EXTERNPOKEY);
                sqlComando.Parameters.AddWithValue("@STORERKEY", objHeader.STORERKEY);
                sqlComando.Parameters.AddWithValue("@TYPE", objHeader.TYPE);
                sqlComando.Parameters.AddWithValue("@SELLERNAME", objHeader.SELLERNAME);
                sqlComando.Parameters.AddWithValue("@WHSEID", objHeader.WHSEID);
                sqlComando.Parameters.AddWithValue("@EXPECTEDRECEIPTDATE", objHeader.EXPECTEDRECEIPTDATE);
                sqlComando.Parameters.AddWithValue("@SUSR1", objHeader.SUSR1);
                sqlComando.Parameters.AddWithValue("@SUSR2", objHeader.SUSR2);
                sqlComando.Parameters.AddWithValue("@SUSR3", objHeader.SUSR3);
                sqlComando.Parameters.AddWithValue("@SUSR4", objHeader.SUSR4);
                sqlComando.Parameters.AddWithValue("@SUSR5", objHeader.SUSR5);


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


        public static void sp_ION_Download_INT_PODETAIL(clsModelo_PO_Detail objDetail)
        {

            SqlConnection sqlConexion = new SqlConnection(VariablesGlobales.strConexionSQL_Proceso);
            SqlCommand sqlComando = new SqlCommand();

            try
            {

                sqlComando.Connection = sqlConexion;
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.CommandText = "sp_ION_Download_INT_PODETAIL";
                sqlComando.CommandTimeout = 0;

                sqlComando.Parameters.AddWithValue("@EXTERNPOKEY", objDetail.EXTERNPOKEY);
                sqlComando.Parameters.AddWithValue("@STORERKEY", objDetail.STORERKEY);
                sqlComando.Parameters.AddWithValue("@EXTERNLINENO", objDetail.EXTERNLINENO);
                sqlComando.Parameters.AddWithValue("@SKU", objDetail.SKU);
                sqlComando.Parameters.AddWithValue("@QTYEXPECTED", objDetail.QTYEXPECTED);
                sqlComando.Parameters.AddWithValue("@UOM", objDetail.UOM);
                sqlComando.Parameters.AddWithValue("@WHSEID", objDetail.WHSEID);
                sqlComando.Parameters.AddWithValue("@lotattribute06", objDetail.lotattribute06);
                sqlComando.Parameters.AddWithValue("@SUSR1", objDetail.SUSR1);
                sqlComando.Parameters.AddWithValue("@SUSR2", objDetail.SUSR2);
                sqlComando.Parameters.AddWithValue("@SUSR3", objDetail.SUSR3);
                sqlComando.Parameters.AddWithValue("@SUSR4", objDetail.SUSR4);
                sqlComando.Parameters.AddWithValue("@SUSR5", objDetail.SUSR5);
                ;
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

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
    public static class Orders_DAO
    {

        public static void Guardar_en_BD_x_BulkCopy(DataTable Datos, String strNombreTabla)
        {

            SqlConnection sqlConexion = new SqlConnection(VariablesGlobales.strConexionSQL);
            SqlCommand sqlComando = new SqlCommand();
            SqlDataAdapter sqlAdaptador = new SqlDataAdapter();

            try
            {
                sqlConexion.Open();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConexion))
                {
                    bulkCopy.DestinationTableName = strNombreTabla;
                    bulkCopy.WriteToServer(Datos);
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        public static void sp_ION_Limpiar_ORDERS(string id)
        {

            SqlConnection sqlConexion = new SqlConnection(VariablesGlobales.strConexionSQL_Proceso);
            SqlCommand sqlComando = new SqlCommand();

            try
            {

                sqlComando.Connection = sqlConexion;
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.CommandText = "sp_ION_Limpiar_ORDERS";
                sqlComando.CommandTimeout = 0;

                sqlComando.Parameters.AddWithValue("@EXTERNORDERKEY", id);

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


        public static void sp_ION_Download_INT_ORDER(clsModelo_Order objHeader)
        {

            SqlConnection sqlConexion = new SqlConnection(VariablesGlobales.strConexionSQL_Proceso);
            SqlCommand sqlComando = new SqlCommand();

            try
            {

                sqlComando.Connection = sqlConexion;
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.CommandText = "sp_ION_Download_INT_ORDER";
                sqlComando.CommandTimeout = 0;

                sqlComando.Parameters.AddWithValue("@EXTERNORDERKEY", objHeader.EXTERNORDERKEY);
                sqlComando.Parameters.AddWithValue("@STORERKEY", objHeader.STORERKEY);
                sqlComando.Parameters.AddWithValue("@TYPE", objHeader.TYPE);
                sqlComando.Parameters.AddWithValue("@CONSIGNEEKEY", objHeader.CONSIGNEEKEY);
                sqlComando.Parameters.AddWithValue("@B_CONTACT1", objHeader.B_CONTACT1);
                sqlComando.Parameters.AddWithValue("@WHSEID", objHeader.WHSEID);
                sqlComando.Parameters.AddWithValue("@NOTES", objHeader.NOTES);
                sqlComando.Parameters.AddWithValue("@DELIVERYDATE", objHeader.DELIVERYDATE);
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


        public static void sp_ION_Download_INT_ORDERDETAIL(clsModelo_OrderDetails objDetail)
        {

            SqlConnection sqlConexion = new SqlConnection(VariablesGlobales.strConexionSQL_Proceso);
            SqlCommand sqlComando = new SqlCommand();

            try
            {

                sqlComando.Connection = sqlConexion;
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.CommandText = "sp_ION_Download_INT_ORDERDETAIL";
                sqlComando.CommandTimeout = 0;

                sqlComando.Parameters.AddWithValue("@EXTERNORDERKEY", objDetail.EXTERNORDERKEY);
                sqlComando.Parameters.AddWithValue("@STORERKEY", objDetail.STORERKEY);
                sqlComando.Parameters.AddWithValue("@EXTERNLINENO", objDetail.EXTERNLINENO);
                sqlComando.Parameters.AddWithValue("@SKU", objDetail.SKU);
                sqlComando.Parameters.AddWithValue("@UOM", objDetail.UOM);
                sqlComando.Parameters.AddWithValue("@OPENQTY", objDetail.OPENQTY);
                sqlComando.Parameters.AddWithValue("@ORIGINALQTY", objDetail.ORIGINALQTY);
                sqlComando.Parameters.AddWithValue("@WHSEID", objDetail.WHSEID);
                sqlComando.Parameters.AddWithValue("@LOTATTRIBUTE06", objDetail.lOTATTRIBUTE06);
                sqlComando.Parameters.AddWithValue("@SUSR1", objDetail.SUSR1);
                sqlComando.Parameters.AddWithValue("@SUSR2", objDetail.SUSR2);
                sqlComando.Parameters.AddWithValue("@SUSR3", objDetail.SUSR3);
                sqlComando.Parameters.AddWithValue("@SUSR4", objDetail.SUSR4);
                sqlComando.Parameters.AddWithValue("@SUSR5", objDetail.SUSR5);

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

using Infraestructure;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public static class Crear_ASN_Receipt
    {

        /// <summary>
        /// Ejecuta el proceso de Download
        /// </summary>       
        public static void ejecutarProceso()
        {
            DataSet ds = new DataSet();
            Log_Download_Model objLog = new Log_Download_Model();
            string id = "";

            try
            {

                //Consulto el diferencial
                ds = OperationsGenericsSQL_DAO.ejecucion_procedimiento_principal();

                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow registro in ds.Tables[0].Rows)
                    {
                        //objLog.bitError = false;
                        DataTable dtDetalle = new DataTable();

                        // ===================================================================================
                        // Filtrado pedido a insertar en ION
                        try
                        {
                            id = registro["EXTERNRECEIPTKEY"].ToString();
                            objLog.identificador1 = id;
                            objLog.cia = int.Parse(registro["STORERKEY"].ToString());
                            objLog.co = registro["CO"].ToString();
                            objLog.tipoDoc = registro["TYPE"].ToString();
                            objLog.numDoc = int.Parse(registro["SUSR1"].ToString());
                            objLog.datosEnviados = ds.GetXml();

                            // Filtrar
                            DataRow[] drDetalle = ds.Tables[1].Select("EXTERNRECEIPTKEY = '" + id.Trim() + "'");
                            dtDetalle = drDetalle.CopyToDataTable();
                        }
                        catch (Exception ex)
                        {
                            objLog.bitError = true;
                            objLog.detalleResultado = "Error Filtrando datos a insertar en ION - " + ex.Message;
                        }
                        // ===================================================================================


                        // ===================================================================================
                        // Limpiar registro ION
                        if (objLog.bitError == false)
                        {
                            try
                            {
                                Receipt_DAO.sp_ION_Limpiar_RECEIPT(id);
                            }
                            catch (Exception ex)
                            {
                                objLog.bitError = true;
                                objLog.detalleResultado = "Error al Limpiar registro ION - " + ex.Message;
                            }
                        }
                        // ===================================================================================

                        // ===================================================================================
                        // Enviar datos ION enviar_OEX_ION_PEV
                        if (objLog.bitError == false)
                        {
                            try
                            {
                                sendData(ref dtDetalle);
                            }
                            catch (Exception ex)
                            {
                                objLog.bitError = true;
                                objLog.detalleResultado = "Error al Enviar datos ION - " + ex.Message;
                            }
                        }
                        // ===================================================================================

                        if ((objLog.bitError))
                        {
                            Logs_DAO.sp_Admin_Log_Download_Insert(objLog);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void sendData(ref DataTable dt)
        {

            try
            {

                // ===============================================================================================

                foreach (DataRow registro in dt.Rows)
                {
                    clsModelo_ReceiptDetail objDetails = new clsModelo_ReceiptDetail();

                    objDetails.STORERKEY = registro["STORERKEY"].ToString().Substring(0, Math.Min(registro["STORERKEY"].ToString().Length, 15));
                    objDetails.EXTERNRECEIPTKEY = registro["EXTERNRECEIPTKEY"].ToString().Substring(0, Math.Min(registro["EXTERNRECEIPTKEY"].ToString().Length, 32));
                    objDetails.EXTERNLINENO = registro["EXTERNLINENO"].ToString().Substring(0, Math.Min(registro["EXTERNLINENO"].ToString().Length, 20));
                    objDetails.SKU = registro["SKU"].ToString().Substring(0, Math.Min(registro["SKU"].ToString().Length, 50));
                    objDetails.QTYEXPECTED = registro["QTYEXPECTED"].ToString().Substring(0, Math.Min(registro["QTYEXPECTED"].ToString().Length, 50));
                    objDetails.UOM = registro["UOM"].ToString().Substring(0, Math.Min(registro["UOM"].ToString().Length, 10));
                    objDetails.WHSEID = registro["WHSEID"].ToString().Substring(0, Math.Min(registro["WHSEID"].ToString().Length, 30));
                    objDetails.LOTATTRIBUTE06 = registro["LOTATTRIBUTE06"].ToString().Substring(0, Math.Min(registro["LOTATTRIBUTE06"].ToString().Length, 30));
                    objDetails.SUSR1 = registro["D_SUSR1"].ToString().Substring(0, Math.Min(registro["D_SUSR1"].ToString().Length, 30));
                    objDetails.SUSR2 = registro["D_SUSR2"].ToString().Substring(0, Math.Min(registro["D_SUSR2"].ToString().Length, 30));
                    objDetails.SUSR3 = registro["D_SUSR3"].ToString().Substring(0, Math.Min(registro["D_SUSR3"].ToString().Length, 30));
                    objDetails.SUSR4 = registro["D_SUSR4"].ToString().Substring(0, Math.Min(registro["D_SUSR4"].ToString().Length, 30));
                    objDetails.SUSR5 = registro["D_SUSR5"].ToString().Substring(0, Math.Min(registro["D_SUSR5"].ToString().Length, 30));

                    Receipt_DAO.sp_ION_Download_INT_RECEIPTDETAIL(objDetails);

                }

                // ===============================================================================================

                // ===============================================================================================
                // Encabezado
                foreach (DataRow registro in dt.Rows)
                {
                    clsModelo_Receipt objHeader = new clsModelo_Receipt();

                    objHeader.STORERKEY = registro["STORERKEY"].ToString().Substring(0, Math.Min(registro["STORERKEY"].ToString().Length, 15));
                    objHeader.EXTERNRECEIPTKEY = registro["EXTERNRECEIPTKEY"].ToString().Substring(0, Math.Min(registro["EXTERNRECEIPTKEY"].ToString().Length, 32));
                    objHeader.TYPE = registro["TYPE"].ToString().Substring(0, Math.Min(registro["TYPE"].ToString().Length, 10));
                    objHeader.WHSEID = registro["WHSEID"].ToString().Substring(0, Math.Min(registro["WHSEID"].ToString().Length, 30));
                    objHeader.SUPPLIERCODE = registro["SUPPLIERCODE"].ToString().Substring(0, Math.Min(registro["SUPPLIERCODE"].ToString().Length, 15));
                    objHeader.NOTES = registro["NOTES"].ToString().Substring(0, Math.Min(registro["NOTES"].ToString().Length, 60));
                    objHeader.SUSR1 = registro["SUSR1"].ToString().Substring(0, Math.Min(registro["SUSR1"].ToString().Length, 30));
                    objHeader.SUSR2 = registro["SUSR2"].ToString().Substring(0, Math.Min(registro["SUSR2"].ToString().Length, 30));
                    objHeader.SUSR3 = registro["SUSR3"].ToString().Substring(0, Math.Min(registro["SUSR3"].ToString().Length, 30));
                    objHeader.SUSR4 = registro["SUSR4"].ToString().Substring(0, Math.Min(registro["SUSR4"].ToString().Length, 30));
                    objHeader.SUSR5 = registro["SUSR5"].ToString().Substring(0, Math.Min(registro["SUSR5"].ToString().Length, 30));

                    objHeader.NOTES.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");

                    Receipt_DAO.sp_ION_Download_INT_RECEIPT(objHeader);

                    // Solo se ejecuta una vez
                    break;
                }
            }
            // ===============================================================================================

            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

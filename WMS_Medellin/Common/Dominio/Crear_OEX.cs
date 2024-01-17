﻿using Infraestructure;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public static class Crear_OEX
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
                            id = registro["EXTERNORDERKEY"].ToString();
                            objLog.identificador1 = id;
                            objLog.identificador2 = id;
                            objLog.cia = int.Parse(registro["STORERKEY"].ToString());
                            objLog.co = registro["CO"].ToString();
                            objLog.tipoDoc = registro["TYPE"].ToString();
                            objLog.numDoc = int.Parse(registro["SUSR1"].ToString());
                            objLog.datosEnviados = ds.GetXml();

                            // Filtrar
                            DataRow[] drDetalle = ds.Tables[1].Select("EXTERNORDERKEY = '" + id.Trim() + "'");
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
                                Orders_DAO.sp_ION_Limpiar_ORDERS(id);
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
                                objLog.detalleResultado = "Datos enviados a las tablas intermedias INTSCE";
                            }
                            catch (Exception ex)
                            {
                                objLog.bitError = true;
                                objLog.detalleResultado = "Error al Enviar datos ION - " + ex.Message;
                            }
                        }
                        // ===================================================================================

                        Logs_DAO.sp_Admin_Log_Download_Insert(objLog);
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

                string cerveza = "";

                // ===============================================================================================

                foreach (DataRow registro in dt.Rows)
                {
                    clsModelo_OrderDetails objDetails = new clsModelo_OrderDetails();

                    if (registro["CATEGORIA"].ToString() == "CERVEZA")
                    {
                        cerveza = registro["CATEGORIA"].ToString();
                    }

                    objDetails.STORERKEY = registro["STORERKEY"].ToString().Substring(0, Math.Min(registro["STORERKEY"].ToString().Length, 15));
                    objDetails.EXTERNORDERKEY = registro["EXTERNORDERKEY"].ToString().Substring(0, Math.Min(registro["EXTERNORDERKEY"].ToString().Length, 32));
                    objDetails.EXTERNLINENO = registro["EXTERNLINENO"].ToString().Substring(0, Math.Min(registro["EXTERNLINENO"].ToString().Length, 20));
                    objDetails.SKU = registro["SKU"].ToString().Substring(0, Math.Min(registro["SKU"].ToString().Length, 50));
                    objDetails.UOM = registro["UOM"].ToString().Substring(0, Math.Min(registro["UOM"].ToString().Length, 10));
                    objDetails.OPENQTY = registro["OPENQTY"].ToString().Substring(0, Math.Min(registro["OPENQTY"].ToString().Length, 30));
                    objDetails.ORIGINALQTY = registro["ORIGINALQTY"].ToString().Substring(0, Math.Min(registro["ORIGINALQTY"].ToString().Length, 30));
                    objDetails.SUSR1 = registro["D_SUSR1"].ToString().Substring(0, Math.Min(registro["D_SUSR1"].ToString().Length, 30));
                    objDetails.SUSR2 = registro["D_SUSR2"].ToString().Substring(0, Math.Min(registro["D_SUSR2"].ToString().Length, 30));
                    objDetails.SUSR3 = registro["D_SUSR3"].ToString().Substring(0, Math.Min(registro["D_SUSR3"].ToString().Length, 30));
                    objDetails.SUSR4 = registro["D_SUSR4"].ToString().Substring(0, Math.Min(registro["D_SUSR4"].ToString().Length, 30));
                    objDetails.SUSR5 = registro["D_SUSR5"].ToString().Substring(0, Math.Min(registro["D_SUSR5"].ToString().Length, 30));
                    objDetails.WHSEID = registro["WHSEID"].ToString().Substring(0, Math.Min(registro["WHSEID"].ToString().Length, 30));
                    objDetails.lOTATTRIBUTE06 = registro["lOTATTRIBUTE06"].ToString().Substring(0, Math.Min(registro["lOTATTRIBUTE06"].ToString().Length, 30));

                    Orders_DAO.sp_ION_Download_INT_ORDERDETAIL(objDetails);

                }

                // ===============================================================================================

                // ===============================================================================================
                // Encabezado
                foreach (DataRow registro in dt.Rows)
                {
                    clsModelo_Order objHeader = new clsModelo_Order();

                    objHeader.STORERKEY = registro["STORERKEY"].ToString().Substring(0, Math.Min(registro["STORERKEY"].ToString().Length, 15));
                    objHeader.EXTERNORDERKEY = registro["EXTERNORDERKEY"].ToString().Substring(0, Math.Min(registro["EXTERNORDERKEY"].ToString().Length, 32));
                    objHeader.TYPE = registro["TYPE"].ToString().Substring(0, Math.Min(registro["TYPE"].ToString().Length, 10));
                    objHeader.CONSIGNEEKEY = registro["CONSIGNEEKEY"].ToString().Substring(0, Math.Min(registro["CONSIGNEEKEY"].ToString().Length, 15));
                    objHeader.SUSR1 = registro["SUSR1"].ToString().Substring(0, Math.Min(registro["SUSR1"].ToString().Length, 30));
                    objHeader.SUSR2 = registro["SUSR2"].ToString().Substring(0, Math.Min(registro["SUSR2"].ToString().Length, 30));

                    if (cerveza == "CERVEZA")
                    {
                        objHeader.SUSR3 = cerveza;
                    }
                    else
                    {
                        objHeader.SUSR3 = registro["SUSR3"].ToString().Substring(0, Math.Min(registro["SUSR3"].ToString().Length, 30));
                    }

                    objHeader.SUSR4 = registro["SUSR4"].ToString().Substring(0, Math.Min(registro["SUSR4"].ToString().Length, 30));
                    objHeader.SUSR5 = registro["SUSR5"].ToString().Substring(0, Math.Min(registro["SUSR5"].ToString().Length, 30));
                    objHeader.B_CONTACT1 = registro["B_CONTACT1"].ToString().Substring(0, Math.Min(registro["B_CONTACT1"].ToString().Length, 30));
                    objHeader.WHSEID = registro["WHSEID"].ToString().Substring(0, Math.Min(registro["WHSEID"].ToString().Length, 30));
                    objHeader.NOTES = registro["NOTES"].ToString().Substring(0, Math.Min(registro["NOTES"].ToString().Length, 60));
                    objHeader.DELIVERYDATE = DateTime.Parse(registro["DELIVERYDATE"].ToString());
                   
                    objHeader.B_CONTACT1 =  objHeader.B_CONTACT1.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
                    objHeader.NOTES = objHeader.NOTES.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");

                    Orders_DAO.sp_ION_Download_INT_ORDER(objHeader);

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

using Infraestructure;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dominio
{
    public class Upload_WMS_SIESA
    {

        IUpload objImportacion;
       
        DataSet dsDatos;
        DataRow registro;

        string identificador1;
        string identificador2;

        string cia_Destino;
        string co_Destino;
        string tipoDoc_Destino;
        string numDoc_Destino;

        string cia_Origen;
        string co_Origen;
        string tipoDoc_Origen;
        string numDoc_Origen;

        int idDocumentoGT;
        string strDocumentoGT;

        public Upload_WMS_SIESA(IUpload p_objImportacion)
        {           
            objImportacion = p_objImportacion;
            dsDatos = new DataSet();            
        }

        public void ejecutar()
        {

            DataSet dsConfigTarea = new DataSet();
            bool bitEncendida = false;
            string urlWebServiceUNOEE = "";

            try
            {
                //string nombreTareaInterface = My.Settings.nombreTareaInterface;
                //string procedimientoPrincipal = My.Settings.procedimientoPrincipal;

                // Consultar Configuracion de la Tarea
                (bitEncendida, urlWebServiceUNOEE) = OperationsGenericsSQL_DAO.sp_Admin_Consultar_Url_WSUNOEE();

                if (bitEncendida)
                {

                    Asignar_Variables_Globales.asignar_url_UNOEE(urlWebServiceUNOEE);

                    // Traer Datos a Enviar
                    string sp = VariablesGlobales.procedimientoPrincipal;
                    dsDatos = OperationsGenericsSQL_DAO.ejecucion_procedimiento_principal();

                    if (dsDatos.Tables.Count > 0)
                    {
                        foreach (DataRow registro in dsDatos.Tables[0].Rows)
                        {
                            try
                            {

                                identificador1 = registro["paquete_Origen"].ToString();
                                identificador2 = registro["referencia"].ToString();

                                cia_Destino = registro["cia_Destino"].ToString();
                                co_Destino = registro["co_Destino"].ToString();
                                tipoDoc_Destino = registro["tipoDoc_Destino"].ToString();
                                numDoc_Destino = registro["numDoc_Destino"].ToString();

                                cia_Origen = registro["cia_Origen"].ToString();
                                co_Origen = registro["co_Origen"].ToString();
                                tipoDoc_Origen = registro["tipoDoc_Origen"].ToString();
                                numDoc_Origen = registro["numDoc_Origen"].ToString();

                                //System.Threading.Thread AddThread1 = new System.Threading.Thread(objImportacion.procesar ); ;
                                //AddThread1 = new System.Threading.Thread(objImportacion.procesar);
                                Thread AddThread1 = new Thread(new ThreadStart(ejecutarHilo));
                                AddThread1.Start(identificador1);

                                System.Threading.Thread.Sleep(3000);

                            }
                            catch (Exception ex)
                            {
                                //objEnvio.bitError = true;
                                //objEnvio.otrosDetalles = ex.Message;
                                //Logs_DAO.sp_Admin_Log_Upload_Insert(objEnvio);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                EstructuraUploadModel objLog = new EstructuraUploadModel();
                objLog.bitError = true;
                objLog.identificador1 = VariablesGlobales.nombreTareaInterface;
                objLog.otrosDetalles = ex.Message;
                Logs_DAO.sp_Admin_Log_Upload_Insert(objLog);
            }
        }

        public void ejecutarHilo()
        {

            EstructuraUploadModel objEnvio = new EstructuraUploadModel();           

            objEnvio.identificador1 = identificador1;
            objEnvio.identificador2 = identificador2;

            objEnvio.cia_Destino = cia_Destino;
            objEnvio.co_Destino = co_Destino;
            objEnvio.tipoDoc_Destino = tipoDoc_Destino;
            objEnvio.numDoc_Destino = numDoc_Destino;

            objEnvio.cia_Origen = int.Parse(cia_Origen);
            objEnvio.co_Origen = co_Origen;
            objEnvio.tipoDoc_Origen = tipoDoc_Origen;
            objEnvio.numDoc_Origen = int.Parse(numDoc_Origen);


            if (dsDatos.Tables.Count > 1)
            {
                if(dsDatos.Tables[1].Rows.Count > 0)
                {
                    DataRow[] drDocumentos = dsDatos.Tables[1].Select("paquete_Origen = '" + objEnvio.identificador1.Trim() + "'");
                    objEnvio.dtDocumentos = drDocumentos.CopyToDataTable();
                }               
            }

            //if (tipoDoc_Origen.ToLower().Contains("p"))
            //{
            //    DataRow[] drMovimientos = dsDatos.Tables[1].Select("paquete_Origen = '" + objEnvio.identificador1.Trim() + "'");
            //    objEnvio.dtMovimientos = drMovimientos.CopyToDataTable();
            //}

            if (dsDatos.Tables.Count > 2)
            {
                if (dsDatos.Tables[2].Rows.Count > 0)
                {
                    DataRow[] drMovimientos = dsDatos.Tables[2].Select("paquete_Origen = '" + objEnvio.identificador1.Trim() + "'");
                    objEnvio.dtMovimientos = drMovimientos.CopyToDataTable();
                }                
            }

            if (dsDatos.Tables.Count > 3)
            {
                if (dsDatos.Tables[3].Rows.Count > 0)
                {
                    DataRow[] drDescuentos = dsDatos.Tables[3].Select("paquete_Origen = '" + objEnvio.identificador1.Trim() + "'");
                    objEnvio.dtDescuentos = drDescuentos.CopyToDataTable();
                }                  
            }


            objImportacion.procesar(objEnvio);

        }

    }
}

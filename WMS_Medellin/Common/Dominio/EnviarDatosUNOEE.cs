using Infraestructure;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServices;

namespace Dominio
{
    public static class EnviarDatosUNOEE
    {

        public static void enviar(ref EstructuraUploadModel objEnvio)
        {

            DataSet dsDatosGT = new DataSet();
            string plano = "";

            if (objEnvio.bitError == false)
            {
                try
                {
                    dsDatosGT = Conversiones_Genericas.convertir_xml_to_dataset(objEnvio.datosEnviados_GT);
                }
                catch (Exception ex)
                {
                    objEnvio.bitError = true;
                    //objEnvio.datosEnviados_GT = strXML_GT;
                    objEnvio.otrosDetalles = "Error al convertir el XML de GT en dataset " + ex.Message;
                }
            }
            // //////////////////////////////////////////////////////////////////////////////////////////////////


            // //////////////////////////////////////////////////////////////////////////////////////////////////
            if (objEnvio.bitError == false)
            {
                try
                {
                    string resultadoConsumoGT = "";
                    (resultadoConsumoGT, plano) = Realizar_Importacion_WSGT.ejecutarProceso(objEnvio.idDocumentoGT, objEnvio.strDocumentoGT, objEnvio.cia_Destino, dsDatosGT, VariablesGlobales.rutaPlanos);

                    if ((resultadoConsumoGT != "Se genero el plano correctamente"))
                    {
                        objEnvio.bitError = true;
                        objEnvio.resultado_GT = "Error al consumir GTIntegration " + resultadoConsumoGT;
                    }
                }
                catch (Exception ex)
                {
                    objEnvio.bitError = true;
                    objEnvio.resultado_GT = "Error al consumir GTIntegration " + ex.Message;
                }
            }
            // //////////////////////////////////////////////////////////////////////////////////////////////////


            // //////////////////////////////////////////////////////////////////////////////////////////////////
            // Cargar UNOEE
            if (objEnvio.bitError == false)
            {
                try
                {
                    Realizar_Importacion_WSUNOEE.ejecutarProceso(plano, ref objEnvio);
                }
                catch (Exception ex)
                {
                    objEnvio.bitError = true;
                    objEnvio.otrosDetalles = ex.Message;
                }
            }
            // //////////////////////////////////////////////////////////////////////////////////////////////////
          
        }



        public static void procesarRemision(EstructuraUploadModel objEnvio)
        {
            string strXML_GT = "";

            // //////////////////////////////////////////////////////////////////////////////////////////////////
            if (objEnvio.bitError == false)
            {
                try
                {
                    string fechaDoc = DateTime.Now.Date.ToString("yyyyMMdd");

                    strXML_GT += "<MyDataSet>";

                    strXML_GT += "<Inicial>";
                    strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>";
                    strXML_GT += "</Inicial>";

                    strXML_GT += "<Remision>";
                    strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>";
                    strXML_GT += "<F350_ID_CO>" + objEnvio.co_Destino + "</F350_ID_CO>";
                    strXML_GT += "<F350_ID_TIPO_DOCTO>" + objEnvio.tipoDoc_Destino + "</F350_ID_TIPO_DOCTO>";
                    strXML_GT += "<F350_CONSEC_DOCTO>" + objEnvio.numDoc_Destino + "</F350_CONSEC_DOCTO>";
                    strXML_GT += "<F350_FECHA>" + fechaDoc + "</F350_FECHA>";
                    strXML_GT += "<F430_ID_TIPO_DOCTO>" + objEnvio.tipoDoc_Origen + "</F430_ID_TIPO_DOCTO>";
                    strXML_GT += "<F430_CONSEC_DOCTO>" + objEnvio.numDoc_Origen + "</F430_CONSEC_DOCTO>";
                    strXML_GT += "</Remision>";

                    strXML_GT += "<Final>";
                    strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>";
                    strXML_GT += "</Final>";

                    strXML_GT += "</MyDataSet>";

                    objEnvio.datosEnviados_GT = strXML_GT;

                }
                catch (Exception ex)
                {
                    objEnvio.bitError = true;
                    objEnvio.otrosDetalles = "Error al armar el xml de GT " + ex.Message;
                }
            }
            // //////////////////////////////////////////////////////////////////////////////////////////////////

            DataSet dsDatosGT = new DataSet();
            string plano = "";

            if (objEnvio.bitError == false)
            {
                try
                {
                    dsDatosGT = Conversiones_Genericas.convertir_xml_to_dataset(objEnvio.datosEnviados_GT);
                }
                catch (Exception ex)
                {
                    objEnvio.bitError = true;
                    //objEnvio.datosEnviados_GT = strXML_GT;
                    objEnvio.otrosDetalles = "Error al convertir el XML de GT en dataset " + ex.Message;
                }
            }
            // //////////////////////////////////////////////////////////////////////////////////////////////////


            // //////////////////////////////////////////////////////////////////////////////////////////////////
            if (objEnvio.bitError == false)
            {
                try
                {
                    string resultadoConsumoGT = "";
                    (resultadoConsumoGT, plano) = Realizar_Importacion_WSGT.ejecutarProceso(objEnvio.idDocumentoGT, objEnvio.strDocumentoGT, objEnvio.cia_Destino, dsDatosGT, VariablesGlobales.rutaPlanos);

                    if ((resultadoConsumoGT != "Se genero el plano correctamente"))
                    {
                        objEnvio.bitError = true;
                        objEnvio.resultado_GT = "Error al consumir GTIntegration " + resultadoConsumoGT;
                    }
                }
                catch (Exception ex)
                {
                    objEnvio.bitError = true;
                    objEnvio.resultado_GT = "Error al consumir GTIntegration " + ex.Message;
                }
            }
            // //////////////////////////////////////////////////////////////////////////////////////////////////


            // //////////////////////////////////////////////////////////////////////////////////////////////////
            // Cargar UNOEE
            if (objEnvio.bitError == false)
            {
                try
                {
                    Realizar_Importacion_WSUNOEE.ejecutarProceso(plano, ref objEnvio);
                }
                catch (Exception ex)
                {
                    objEnvio.bitError = true;
                    objEnvio.otrosDetalles = ex.Message;
                }
            }
            // //////////////////////////////////////////////////////////////////////////////////////////////////
          
        }


        public static void procesarTransferencia(EstructuraUploadModel objEnvio)
        {
            string strXML_GT = "";

            // //////////////////////////////////////////////////////////////////////////////////////////////////
            if (objEnvio.bitError == false)
            {
                try
                {
                    string fechaDoc = DateTime.Now.Date.ToString("yyyyMMdd");

                    strXML_GT += "<MyDataSet>";

                    strXML_GT += "<Inicial>";
                    strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>";
                    strXML_GT += "</Inicial>";

                    strXML_GT += "<Documento>";
                    strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>";
                    strXML_GT += "<F350_ID_CO>" + objEnvio.co_Transf_Salida + "</F350_ID_CO>";
                    strXML_GT += "<F350_ID_TIPO_DOCTO>" + objEnvio.tipoDoc_Destino + "</F350_ID_TIPO_DOCTO>";
                    strXML_GT += "<F350_CONSEC_DOCTO>" + objEnvio.numDoc_Destino + "</F350_CONSEC_DOCTO>";
                    strXML_GT += "<F350_FECHA>" + fechaDoc + "</F350_FECHA>";
                    strXML_GT += "<f440_id_co_req_int>" + objEnvio.co_Origen + "</f440_id_co_req_int>";
                    strXML_GT += "<f440_id_tipo_docto_req_int>" + objEnvio.tipoDoc_Origen + "</f440_id_tipo_docto_req_int>";
                    strXML_GT += "<f440_consec_docto_req_int>" + objEnvio.numDoc_Origen + "</f440_consec_docto_req_int>";
                    strXML_GT += "<f350_notas>" + objEnvio.identificador2 + "</f350_notas>";
                    strXML_GT += "<f450_docto_alterno>" + objEnvio.identificador2 + "</f450_docto_alterno>";
                    strXML_GT += "</Documento>";

                    strXML_GT += "<Final>";
                    strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>";
                    strXML_GT += "</Final>";

                    strXML_GT += "</MyDataSet>";

                }
                catch (Exception ex)
                {
                    objEnvio.bitError = true;
                    objEnvio.otrosDetalles = "Error al armar el xml de GT " + ex.Message;
                }
            }
            // //////////////////////////////////////////////////////////////////////////////////////////////////

            DataSet dsDatosGT = new DataSet();
            string plano = "";

            if (objEnvio.bitError == false)
            {
                try
                {
                    objEnvio.datosEnviados_GT = strXML_GT;
                    dsDatosGT = Conversiones_Genericas.convertir_xml_to_dataset(objEnvio.datosEnviados_GT);
                }
                catch (Exception ex)
                {
                    objEnvio.bitError = true;
                    //objEnvio.datosEnviados_GT = strXML_GT;
                    objEnvio.otrosDetalles = "Error al convertir el XML de GT en dataset " + ex.Message;
                }
            }
            // //////////////////////////////////////////////////////////////////////////////////////////////////


            // //////////////////////////////////////////////////////////////////////////////////////////////////
            if (objEnvio.bitError == false)
            {
                try
                {
                    string resultadoConsumoGT = "";
                    (resultadoConsumoGT, plano) = Realizar_Importacion_WSGT.ejecutarProceso(objEnvio.idDocumentoGT, objEnvio.strDocumentoGT, objEnvio.cia_Destino, dsDatosGT, VariablesGlobales.rutaPlanos);

                    if ((resultadoConsumoGT != "Se genero el plano correctamente"))
                    {
                        objEnvio.bitError = true;
                        objEnvio.resultado_GT = "Error al consumir GTIntegration " + resultadoConsumoGT;
                    }
                }
                catch (Exception ex)
                {
                    objEnvio.bitError = true;
                    objEnvio.resultado_GT = "Error al consumir GTIntegration " + ex.Message;
                }
            }
            // //////////////////////////////////////////////////////////////////////////////////////////////////


            // //////////////////////////////////////////////////////////////////////////////////////////////////
            // Cargar UNOEE
            if (objEnvio.bitError == false)
            {
                try
                {
                    Realizar_Importacion_WSUNOEE.ejecutarProceso(plano, ref objEnvio);
                }
                catch (Exception ex)
                {
                    objEnvio.bitError = true;
                    objEnvio.otrosDetalles = ex.Message;
                }
            }
            // //////////////////////////////////////////////////////////////////////////////////////////////////

        }


    }
}

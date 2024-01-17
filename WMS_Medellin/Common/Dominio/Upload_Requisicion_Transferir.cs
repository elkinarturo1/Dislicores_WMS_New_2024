using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Upload_Requisicion_Transferir : IUpload
    {
        public void procesar(EstructuraUploadModel objEnvio)
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
                    strXML_GT += "<F350_ID_CO>" + objEnvio.co_Destino + "</F350_ID_CO>";
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

            objEnvio.datosEnviados_GT = strXML_GT;
            objEnvio.idDocumentoGT = 110437;
            objEnvio.strDocumentoGT = "WMS_COMPROMETER_REQUISICION_RQV";

            EnviarDatosUNOEE.enviar(ref objEnvio);
        }
    }
}

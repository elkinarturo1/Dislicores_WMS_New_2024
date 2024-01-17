using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Upload_Ventas_Pedidos_Remisionar_REV
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

                    strXML_GT += "<Remision>";
                    strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>";
                    strXML_GT += "<F350_ID_CO>" + objEnvio.co_Destino + "</F350_ID_CO>";
                    strXML_GT += "<F350_ID_TIPO_DOCTO>REV</F350_ID_TIPO_DOCTO>";
                    strXML_GT += "<F350_CONSEC_DOCTO>" + objEnvio.numDoc_Destino + "</F350_CONSEC_DOCTO>";
                    strXML_GT += "<F350_FECHA>" + fechaDoc + "</F350_FECHA>";
                    strXML_GT += "<F430_ID_TIPO_DOCTO>" + objEnvio.tipoDoc_Origen + "</F430_ID_TIPO_DOCTO>";
                    strXML_GT += "<F430_CONSEC_DOCTO>" + objEnvio.numDoc_Origen + "</F430_CONSEC_DOCTO>";
                    strXML_GT += "</Remision>";

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

            EnviarDatosUNOEE.enviar(ref objEnvio);
        }
    }
}

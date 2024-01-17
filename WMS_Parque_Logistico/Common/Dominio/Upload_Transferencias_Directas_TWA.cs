using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Upload_Transferencias_Directas_TWA : IUpload
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

                    foreach (DataRow registro in objEnvio.dtDocumentos.Rows)
                    {
                        strXML_GT += "<Documentos>";
                        strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>";
                        strXML_GT += "<f350_id_co>" + objEnvio.co_Destino + "</f350_id_co>";
                        strXML_GT += "<f350_id_tipo_docto>" + objEnvio.tipoDoc_Destino + "</f350_id_tipo_docto>";
                        strXML_GT += "<f350_consec_docto>" + objEnvio.numDoc_Destino + "</f350_consec_docto>";
                        // strXML_GT &= "<f350_consec_docto>3</f350_consec_docto>"
                        strXML_GT += "<f350_fecha>" + fechaDoc + "</f350_fecha>";
                        strXML_GT += "<f350_notas> </f350_notas>";
                        strXML_GT += "<f450_id_bodega_salida>" + registro["f450_id_bodega_salida"].ToString() + "</f450_id_bodega_salida>";
                        strXML_GT += "<f450_id_bodega_entrada>" + registro["f450_id_bodega_entrada"].ToString() + "</f450_id_bodega_entrada>";
                        strXML_GT += "<f450_docto_alterno>" + registro["f450_docto_alterno"].ToString() + "</f450_docto_alterno>";
                        strXML_GT += "</Documentos>";
                    }

                    foreach (DataRow registro in objEnvio.dtMovimientos.Rows)
                    {
                        strXML_GT += "<Movimientos>";
                        strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>";
                        strXML_GT += "<f470_id_co>" + objEnvio.co_Destino + "</f470_id_co>";
                        strXML_GT += "<f470_id_tipo_docto>" + objEnvio.tipoDoc_Destino + "</f470_id_tipo_docto>";
                        strXML_GT += "<f470_consec_docto>" + objEnvio.numDoc_Destino + "</f470_consec_docto>";
                        // strXML_GT &= "<f470_consec_docto>3</f470_consec_docto>"
                        strXML_GT += "<f470_nro_registro>" + registro["f470_nro_registro"].ToString() + "</f470_nro_registro>";
                        strXML_GT += "<f470_id_bodega>" + registro["f470_id_bodega"].ToString() + "</f470_id_bodega>";
                        strXML_GT += "<f470_id_motivo>" + registro["f470_id_motivo"].ToString() + "</f470_id_motivo>";
                        strXML_GT += "<f470_id_co_movto>" + registro["f470_id_co_movto"].ToString() + "</f470_id_co_movto>";
                        strXML_GT += "<f470_id_unidad_medida>" + registro["f470_id_unidad_medida"].ToString() + "</f470_id_unidad_medida>";
                        strXML_GT += "<f470_cant_base>" + registro["f470_cant_base"].ToString() + "</f470_cant_base>";
                        strXML_GT += "<f470_notas>" + registro["f470_notas"].ToString() + "</f470_notas>";
                        strXML_GT += "<f470_referencia_item>" + registro["f470_referencia_item"].ToString() + "</f470_referencia_item>";
                        strXML_GT += "<f470_id_un_movto>" + registro["f470_id_un_movto"].ToString() + "</f470_id_un_movto>";
                        strXML_GT += "</Movimientos>";
                    }



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
            objEnvio.idDocumentoGT = 78955;
            objEnvio.strDocumentoGT = "WMS_TRASNFERENCIA_TWA_MOVIMIENTOS_BOD";

            EnviarDatosUNOEE.enviar(ref objEnvio);

        }
    }
}

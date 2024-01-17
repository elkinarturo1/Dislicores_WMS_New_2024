using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Upload_Entrada_TCE_Desde_Tranferencias_TCO : IUpload
    {
        public void procesar(EstructuraUploadModel objEnvio)
        {
            string strXML_GT = "";

            // //////////////////////////////////////////////////////////////////////////////////////////////////
            if (objEnvio.bitError == false)
            {
                try
                {
                    strXML_GT += "<MyDataSet>";

                    strXML_GT += "<Inicial>";
                    strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>";
                    strXML_GT += "</Inicial>";

                    foreach (DataRow registro in objEnvio.dtDocumentos.Rows)
                    {
                        string fechaDoc = DateTime.Now.Date.ToString("yyyyMMdd");

                        strXML_GT += "<Documentos>";
                        strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>";
                        strXML_GT += "<f350_id_co>" + objEnvio.co_Destino + "</f350_id_co>";
                        strXML_GT += "<f350_id_tipo_docto>" + objEnvio.tipoDoc_Destino + "</f350_id_tipo_docto>";
                        strXML_GT += "<f350_consec_docto>" + objEnvio.numDoc_Destino + "</f350_consec_docto>";
                        strXML_GT += "<f350_fecha>" + fechaDoc + "</f350_fecha>";
                        strXML_GT += "<f450_docto_alterno>" + registro["docto_Alterno"].ToString() + "</f450_docto_alterno>";
                        strXML_GT += "<f450_id_bodega_salida>" + registro["BodegaSalida"].ToString() + "</f450_id_bodega_salida>";
                        strXML_GT += "<f450_id_bodega_entrada>" + registro["BodegaEntrada"].ToString() + "</f450_id_bodega_entrada>";
                        strXML_GT += "<f350_id_co_base>" + objEnvio.co_Origen + "</f350_id_co_base>";
                        strXML_GT += "<f350_id_tipo_docto_base>" + objEnvio.tipoDoc_Origen + "</f350_id_tipo_docto_base>";
                        strXML_GT += "<f350_consec_docto_base>" + objEnvio.numDoc_Origen + "</f350_consec_docto_base>";
                        strXML_GT += "<f350_notas>" + registro["notas"].ToString() + "</f350_notas>";
                        strXML_GT += "</Documentos>";
                    }

                    foreach (DataRow registro in objEnvio.dtMovimientos.Rows)
                    {
                        strXML_GT += "<Movimientos>";
                        strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>";
                        strXML_GT += "<f470_id_co>" + objEnvio.co_Destino + "</f470_id_co>";
                        strXML_GT += "<f470_id_tipo_docto>" + objEnvio.tipoDoc_Destino + "</f470_id_tipo_docto>";
                        strXML_GT += "<f470_consec_docto>" + objEnvio.numDoc_Destino + "</f470_consec_docto>";
                        strXML_GT += "<f470_nro_registro>" + registro["nro_registro"].ToString() + "</f470_nro_registro>";
                        strXML_GT += "<f470_id_bodega>" + registro["BodegaSalida"].ToString() + "</f470_id_bodega>";
                        strXML_GT += "<f470_id_motivo>" + registro["idmotivo"].ToString() + "</f470_id_motivo>";
                        strXML_GT += "<f470_id_co_movto>" + registro["co_BodegaEntrada"].ToString() + "</f470_id_co_movto>";
                        strXML_GT += "<f470_id_un_movto>" + registro["id_UN_Movto"].ToString() + "</f470_id_un_movto>";
                        strXML_GT += "<f470_id_unidad_medida>" + registro["UnidadMedida"].ToString() + "</f470_id_unidad_medida>";
                        strXML_GT += "<f470_referencia_item>" + registro["SKU"].ToString() + "</f470_referencia_item>";
                        strXML_GT += "<f470_cant_base>" + registro["Cantidad"].ToString() + "</f470_cant_base>";
                        strXML_GT += "<f470_notas>" + registro["notas_Detalle"].ToString() + "</f470_notas>";
                        strXML_GT += "</Movimientos>";
                    }



                    strXML_GT += "<Final>";
                    strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>";
                    strXML_GT += "</Final>";

                    strXML_GT += "</MyDataSet>" + Environment.NewLine;

                }
                catch (Exception ex)
                {
                    objEnvio.bitError = true;
                    objEnvio.otrosDetalles = "Error al armar el xml de GT " + ex.Message;
                }
            }
            // //////////////////////////////////////////////////////////////////////////////////////////////////

            objEnvio.datosEnviados_GT = strXML_GT;
            objEnvio.idDocumentoGT = 78935;
            objEnvio.strDocumentoGT = "WMS_ENTRADA_DESDE_SALIDA_TCE";

            EnviarDatosUNOEE.enviar(ref objEnvio);

        }
    }
}

using Microsoft.VisualBasic;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Upload_Transferencia_TBV_Desde_Requisicion_RQI : IUpload
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

                    strXML_GT += "<Inicial>" + Environment.NewLine;
                    strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>" + Environment.NewLine;
                    strXML_GT += "</Inicial>" + Environment.NewLine;

                    foreach (DataRow registro in objEnvio.dtMovimientos.Rows)
                    {
                        if (Information.IsNumeric(registro["CantidadCompromiso"].ToString()))
                        {
                            strXML_GT += "<Compromisos>" + Environment.NewLine;
                            strXML_GT += "<f441_nro_registro>" + registro["f471_nro_registro"].ToString() + "</f441_nro_registro>" + Environment.NewLine;
                            strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>" + Environment.NewLine;
                            strXML_GT += "<f440_id_co>" + objEnvio.co_Destino + "</f440_id_co>" + Environment.NewLine;
                            strXML_GT += "<f440_id_tipo_docto>" + objEnvio.tipoDoc_Origen + "</f440_id_tipo_docto>" + Environment.NewLine;
                            strXML_GT += "<f440_consec_docto>" + objEnvio.numDoc_Destino + "</f440_consec_docto>" + Environment.NewLine;
                            strXML_GT += "<f441_referencia_item>" + registro["SKU"].ToString() + "</f441_referencia_item>" + Environment.NewLine;
                            strXML_GT += "<f441_id_bodega>" + registro["Bodega_Origen"].ToString() + "</f441_id_bodega>" + Environment.NewLine;
                            strXML_GT += "<f441_id_ubicación_aux></f441_id_ubicación_aux>" + Environment.NewLine;
                            strXML_GT += "<f441_id_unidad_medida>" + registro["UnidadMedida"].ToString() + "</f441_id_unidad_medida>" + Environment.NewLine;
                            strXML_GT += "<f441_id_bodega_ent>" + registro["Bodega_Destino"].ToString() + "</f441_id_bodega_ent>" + Environment.NewLine;
                            strXML_GT += "<f441_id_ubicación_aux_ent></f441_id_ubicación_aux_ent>" + Environment.NewLine;
                            strXML_GT += "<f441_cant_base>" + registro["CantidadCompromiso"].ToString() + "</f441_cant_base>" + Environment.NewLine;
                            strXML_GT += "<f441_cant_por_remisionar_base>" + registro["CantidadCompromiso"].ToString() + "</f441_cant_por_remisionar_base>" + Environment.NewLine;
                            strXML_GT += "</Compromisos>" + Environment.NewLine;
                        }
                    }

                    strXML_GT += "<Final>" + Environment.NewLine;
                    strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>" + Environment.NewLine;
                    strXML_GT += "</Final>" + Environment.NewLine;

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
            objEnvio.idDocumentoGT = 110439;
            objEnvio.strDocumentoGT = "WMS_MED_COMPROMETER_REQUISICION_RQI";

            EnviarDatosUNOEE.enviar(ref objEnvio);

            objEnvio.bitError = false;
            if (objEnvio.bitError == false)
            {
                try
                {
                    objEnvio.idDocumentoGT = 110440;
                    objEnvio.strDocumentoGT = "WMS_MED_TRASNFERENCIA_TBV_DESDE_REQUISICION_RQI";
                    EnviarDatosUNOEE.procesarTransferencia(objEnvio);
                }
                catch (Exception ex)
                {
                    objEnvio.bitError = true;
                    objEnvio.otrosDetalles = ex.Message;
                }
            }

        }
    }
}

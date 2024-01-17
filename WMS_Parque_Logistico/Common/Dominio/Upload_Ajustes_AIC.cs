using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Upload_Ajustes_AIC : IUpload
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

                    strXML_GT += "<MyDataSet>" + Environment.NewLine;

                    strXML_GT += "<Inicial>" + Environment.NewLine;
                    strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>" + Environment.NewLine;
                    strXML_GT += "</Inicial>" + Environment.NewLine;

                    foreach (DataRow registro in objEnvio.dtDocumentos.Rows)
                    {
                        strXML_GT += "<Documentos>" + Environment.NewLine;
                        strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>" + Environment.NewLine;
                        strXML_GT += "<f350_id_co>" + objEnvio.co_Destino + "</f350_id_co>" + Environment.NewLine;
                        strXML_GT += "<f350_id_tipo_docto>" + objEnvio.tipoDoc_Destino + "</f350_id_tipo_docto>" + Environment.NewLine;
                        strXML_GT += "<f350_consec_docto>" + objEnvio.numDoc_Destino + "</f350_consec_docto>" + Environment.NewLine;
                        // strXML_GT &= "<f350_consec_docto>3</f350_consec_docto>" & Environment.NewLine
                        strXML_GT += "<f350_fecha>" + registro["f350_fecha"].ToString() + "</f350_fecha>" + Environment.NewLine;
                        strXML_GT += "<f350_id_tercero>" + registro["f350_id_tercero"].ToString() + "</f350_id_tercero>" + Environment.NewLine;
                        strXML_GT += "<f350_id_clase_docto>" + registro["f350_id_clase_docto"].ToString() + "</f350_id_clase_docto>" + Environment.NewLine;
                        strXML_GT += "<f350_ind_estado>" + registro["f350_ind_estado"].ToString() + "</f350_ind_estado>" + Environment.NewLine;
                        strXML_GT += "<f350_notas>" + registro["f350_notas"].ToString() + "</f350_notas>" + Environment.NewLine;
                        strXML_GT += "<f450_id_concepto>" + registro["f450_id_concepto"].ToString() + "</f450_id_concepto>" + Environment.NewLine;
                        strXML_GT += "<f450_docto_alterno>" + registro["f450_docto_alterno"].ToString() + "</f450_docto_alterno>" + Environment.NewLine;

                        strXML_GT += "</Documentos>" + Environment.NewLine;
                    }

                    foreach (DataRow registro in objEnvio.dtMovimientos.Rows)
                    {
                        int cantidad = 0;

                        cantidad = int.Parse(registro["f470_cant_base"].ToString());

                        if (cantidad < 0)
                            cantidad = cantidad * -1;

                        strXML_GT += "<Movimientos>" + Environment.NewLine;
                        strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>" + Environment.NewLine;
                        strXML_GT += "<f470_id_co>" + objEnvio.co_Destino + "</f470_id_co>" + Environment.NewLine;
                        strXML_GT += "<f470_id_tipo_docto>" + objEnvio.tipoDoc_Destino + "</f470_id_tipo_docto>" + Environment.NewLine;
                        strXML_GT += "<f470_consec_docto>" + objEnvio.numDoc_Destino + "</f470_consec_docto>" + Environment.NewLine;
                        // strXML_GT &= "<f470_consec_docto>3</f470_consec_docto>" & Environment.NewLine
                        strXML_GT += "<f470_nro_registro>" + registro["f470_nro_registro"].ToString() + "</f470_nro_registro>" + Environment.NewLine;
                        strXML_GT += "<f470_id_concepto>" + registro["f470_id_concepto"].ToString() + "</f470_id_concepto>" + Environment.NewLine;
                        strXML_GT += "<f470_id_motivo>" + registro["f470_id_motivo"].ToString() + "</f470_id_motivo>" + Environment.NewLine;
                        strXML_GT += "<f470_id_co_movto>" + registro["f470_id_co_movto"].ToString() + "</f470_id_co_movto>" + Environment.NewLine;
                        strXML_GT += "<f470_id_unidad_medida>" + registro["f470_id_unidad_medida"].ToString() + "</f470_id_unidad_medida>" + Environment.NewLine;
                        strXML_GT += "<f470_cant_base>" + cantidad + "</f470_cant_base>" + Environment.NewLine;
                        strXML_GT += "<f470_costo_prom_uni>" + registro["f470_costo_prom_uni"].ToString() + "</f470_costo_prom_uni>" + Environment.NewLine;
                        strXML_GT += "<f470_notas>" + registro["f470_notas"].ToString() + "</f470_notas>" + Environment.NewLine;
                        strXML_GT += "<f470_id_ccosto_movto></f470_id_ccosto_movto>" + Environment.NewLine;
                        strXML_GT += "<f470_id_ubicación_aux>" + registro["f470_id_ubicación_aux"].ToString() + "</f470_id_ubicación_aux>" + Environment.NewLine;
                        strXML_GT += "<f470_id_ubicación_aux_ent>" + registro["f470_id_ubicación_aux_ent"].ToString() + "</f470_id_ubicación_aux_ent>" + Environment.NewLine;
                        strXML_GT += "<f470_referencia_item>" + registro["f470_referencia_item"].ToString() + "</f470_referencia_item>" + Environment.NewLine;
                        strXML_GT += "<f470_id_un_movto>" + registro["f470_id_un_movto"].ToString() + "</f470_id_un_movto>" + Environment.NewLine;
                        strXML_GT += "<f470_id_bodega>" + registro["f470_id_bodega"].ToString() + "</f470_id_bodega>" + Environment.NewLine;
                        strXML_GT += "</Movimientos>" + Environment.NewLine;
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
            objEnvio.idDocumentoGT = 80691;
            objEnvio.strDocumentoGT = "WMS_AJUSTES_ENTRADAS_SALIDAS_AIC";

            EnviarDatosUNOEE.enviar(ref objEnvio);

        }
    }
}

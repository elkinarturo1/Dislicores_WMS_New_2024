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
    public class Upload_Ventas_Comprometer_Remisionar_Pedidos_PEE : IUpload
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
                            strXML_GT += "<F_CIA>" + objEnvio.cia_Origen + "</F_CIA>" + Environment.NewLine;
                            strXML_GT += "<f430_id_co>" + objEnvio.co_Origen + "</f430_id_co>" + Environment.NewLine;
                            strXML_GT += "<f430_id_tipo_docto>" + objEnvio.tipoDoc_Origen + "</f430_id_tipo_docto>" + Environment.NewLine;
                            strXML_GT += "<f430_consec_docto>" + objEnvio.numDoc_Origen + "</f430_consec_docto>" + Environment.NewLine;
                            strXML_GT += "<f431_referencia_item>" + registro["SKU"].ToString() + "</f431_referencia_item>" + Environment.NewLine;
                            strXML_GT += "<f431_id_bodega>" + registro["Bodega"].ToString() + "</f431_id_bodega>" + Environment.NewLine;
                            strXML_GT += "<f431_id_unidad_medida>" + registro["UnidadMedida"].ToString() + "</f431_id_unidad_medida>" + Environment.NewLine;
                            strXML_GT += "<f431_cant_base>" + registro["CantidadCompromiso"].ToString() + "</f431_cant_base>" + Environment.NewLine;
                            strXML_GT += "<f431_nro_registro>" + registro["RowIdMovto"].ToString() + "</f431_nro_registro>" + Environment.NewLine;
                            strXML_GT += "<f405_cant_por_remisionar_base>" + registro["CantidadCompromiso"].ToString() + "</f405_cant_por_remisionar_base>" + Environment.NewLine;
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

            EnviarDatosUNOEE.enviar(ref objEnvio);

        }
    }
}

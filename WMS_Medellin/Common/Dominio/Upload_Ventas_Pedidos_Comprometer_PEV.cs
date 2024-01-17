using Infraestructure;
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
    public class Upload_Ventas_Pedidos_Comprometer_PEV : IUpload
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
                            strXML_GT += "<f431_nro_registro>" + registro["RowIdMovto"].ToString() + "</f431_nro_registro>" + Environment.NewLine;


                            //Cantidad Comprometida
                            strXML_GT += "<f431_cant_base>" + registro["CantidadCompromiso"].ToString() + "</f431_cant_base>" + Environment.NewLine;

                            //Cantidad Comprometida Parcial nunca debe ser superior a la cantidad comprometida
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

            objEnvio.idDocumentoGT = 77717;
            objEnvio.strDocumentoGT = "WMS_PEDIDOS_COMPROMISOS";

            EnviarDatosUNOEE.enviar(ref objEnvio);
            
            if (objEnvio.bitError == false)
            {
                try
                {

                    if(objEnvio.tipoDoc_Destino.ToUpper() == "SM")
                    {
                        objEnvio.idDocumentoGT = 119643;
                        objEnvio.strDocumentoGT = "WMS_REMISION_SM_DESDE_PEDIDO_PIN";
                    }
                    else
                    {
                        objEnvio.idDocumentoGT = 77816;
                        objEnvio.strDocumentoGT = "WMS_REMISION_DESDE_PEDIDO";
                    }
                    
                    EnviarDatosUNOEE.procesarRemision(objEnvio);

                }
                catch (Exception ex)
                {
                    objEnvio.bitError = true;
                    objEnvio.otrosDetalles = ex.Message;
                }
            }

            Logs_DAO.sp_Admin_Log_Upload_Insert(objEnvio);

        }
    }
}

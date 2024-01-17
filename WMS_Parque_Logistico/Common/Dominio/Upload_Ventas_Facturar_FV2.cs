using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Upload_Ventas_Facturar_FV2 : IUpload
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
                        strXML_GT += "<DoctoVentasComercial>";
                        strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>";
                        strXML_GT += "<F350_ID_CO>" + objEnvio.co_Destino + "</F350_ID_CO>";
                        strXML_GT += "<F350_ID_TIPO_DOCTO>" + objEnvio.tipoDoc_Destino + "</F350_ID_TIPO_DOCTO>";
                        strXML_GT += "<F350_CONSEC_DOCTO>" + objEnvio.numDoc_Destino + "</F350_CONSEC_DOCTO>";
                        strXML_GT += "<F350_FECHA>" + fechaDoc + "</F350_FECHA>";
                        strXML_GT += "<F350_ID_TERCERO>" + registro["id_Tercero_Facturar"].ToString() + "</F350_ID_TERCERO>";
                        strXML_GT += "<f461_id_sucursal_fact>" + registro["id_Sucursal_Facturar"].ToString() + "</f461_id_sucursal_fact>";
                        strXML_GT += "<f461_id_tercero_rem>" + registro["id_Tercero_Remisionar"].ToString() + "</f461_id_tercero_rem>";
                        strXML_GT += "<f461_id_sucursal_rem>" + registro["id_Sucursal_Remisionar"].ToString() + "</f461_id_sucursal_rem>";
                        strXML_GT += "<f461_id_tercero_vendedor>" + registro["idTercero_vendedor"].ToString() + "</f461_id_tercero_vendedor>";
                        strXML_GT += "<f461_num_docto_referencia>" + registro["f460_num_docto_referencia"].ToString().Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Substring(0, Math.Min(registro["f460_num_docto_referencia"].ToString().Length, 12)) + "</f461_num_docto_referencia>";
                        strXML_GT += "<f461_id_tipo_cli_fact>" + registro["f460_id_tipo_cli_fact"].ToString() + "</f461_id_tipo_cli_fact>";
                        strXML_GT += "<f461_id_co_fact>" + registro["f460_id_co_fact"].ToString() + "</f461_id_co_fact>";
                        strXML_GT += "<f461_id_cond_pago>" + registro["f460_id_cond_pago"].ToString() + "</f461_id_cond_pago>";
                        strXML_GT += "<f461_notas>" + objEnvio.identificador2 + " - " + registro["f430_Notas"].ToString().Replace("&", "y").Replace("\r\n", "").Replace("\r", "").Replace("\n", "") + "</f461_notas>";
                        strXML_GT += "</DoctoVentasComercial>";
                    }


                    foreach (DataRow registro in objEnvio.dtDocumentos.Rows)
                    {
                        strXML_GT += "<RelacionDoctos>";
                        strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>";
                        strXML_GT += "<F350_ID_CO>" + objEnvio.co_Destino + "</F350_ID_CO>";
                        strXML_GT += "<F350_ID_TIPO_DOCTO>" + objEnvio.tipoDoc_Destino + "</F350_ID_TIPO_DOCTO>";
                        strXML_GT += "<F350_CONSEC_DOCTO>" + objEnvio.numDoc_Destino + "</F350_CONSEC_DOCTO>";
                        strXML_GT += "<F460_ID_CO>" + objEnvio.co_Origen + "</F460_ID_CO>";
                        strXML_GT += "<F460_ID_TIPO_DOCTO>" + objEnvio.tipoDoc_Origen + "</F460_ID_TIPO_DOCTO>";
                        strXML_GT += "<F460_CONSEC_DOCTO>" + objEnvio.numDoc_Origen + "</F460_CONSEC_DOCTO>";
                        strXML_GT += "</RelacionDoctos>";
                    }


                    foreach (DataRow registro in objEnvio.dtMovimientos.Rows)
                    {
                        string fechaVcto;
                        string fechaDsctoPP;

                        string diasVencimiento = registro["f460_cond_pago_dias_vcto"].ToString();
                        string diasProntoPago = registro["f460_cond_pago_dias_pp"].ToString();

                        fechaVcto = DateTime.Now.Date.AddDays(double.Parse(diasVencimiento)).ToString("yyyyMMdd");
                        fechaDsctoPP = DateTime.Now.Date.AddDays(double.Parse(diasProntoPago)).ToString("yyyyMMdd");

                        strXML_GT += "<CuotasCxC>";
                        strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>";
                        strXML_GT += "<F350_ID_CO>" + objEnvio.co_Destino + "</F350_ID_CO>";
                        strXML_GT += "<F350_ID_TIPO_DOCTO>" + objEnvio.tipoDoc_Destino + "</F350_ID_TIPO_DOCTO>";
                        strXML_GT += "<F350_CONSEC_DOCTO>" + objEnvio.numDoc_Destino + "</F350_CONSEC_DOCTO>";
                        strXML_GT += "<F353_FECHA_VCTO>" + fechaVcto + "</F353_FECHA_VCTO>";
                        strXML_GT += "<F353_FECHA_DSCTO_PP>" + fechaDsctoPP + "</F353_FECHA_DSCTO_PP>";
                        strXML_GT += "</CuotasCxC>";
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
            objEnvio.idDocumentoGT = 77819;
            objEnvio.strDocumentoGT = "WMS_FACTURA_DESDE_REMISION";

            EnviarDatosUNOEE.enviar(ref objEnvio);

        }
    }
}

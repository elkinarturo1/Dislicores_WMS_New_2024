using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data;

namespace Dominio
{
    internal class Upload_Entradas_Notas_NV2_Desde_EDE : IUpload
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

                    foreach (DataRow registro in objEnvio.dtDocumentos.Rows)
                    {
                        string f350_id_tercero_fact = "";
                        string f461_id_sucursal_fact = "";

                        string f461_id_tercero_rem = "";
                        string f461_id_sucursal_rem = "";

                        string f461_id_tipo_cli_fact = "";
                        string f461_id_cond_pago = "";

                        Conversiones_Genericas.destelle_campos_Guion(registro["SUSR2_Cliente_Fact"].ToString(), ref f350_id_tercero_fact, ref f461_id_sucursal_fact);
                        Conversiones_Genericas.destelle_campos_Guion(registro["SUSR3_Cliente_Rem"].ToString(), ref f461_id_tercero_rem, ref f461_id_sucursal_rem);
                        Conversiones_Genericas.destelle_campos_Guion(registro["SUSR4_id_cond_pago"].ToString(), ref f461_id_tipo_cli_fact, ref f461_id_cond_pago);


                        strXML_GT += "<Documentos>";
                        strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>";
                        strXML_GT += "<f350_id_co>" + objEnvio.co_Destino + "</f350_id_co>";
                        strXML_GT += "<f350_id_tipo_docto>" + objEnvio.tipoDoc_Destino + "</f350_id_tipo_docto>";
                        strXML_GT += "<f350_consec_docto>" + objEnvio.numDoc_Destino + "</f350_consec_docto>";
                        strXML_GT += "<f350_fecha>" + registro["fecha_DoctoDestino"].ToString() + "</f350_fecha>";
                        strXML_GT += "<f350_id_tercero>" + f350_id_tercero_fact + "</f350_id_tercero>";
                        strXML_GT += "<f461_id_sucursal_fact>" + f461_id_sucursal_fact + "</f461_id_sucursal_fact>";
                        strXML_GT += "<f461_id_tipo_cli" +
                            "_fact>" + f461_id_tipo_cli_fact + "</f461_id_tipo_cli_fact>";
                        strXML_GT += "<f461_id_co_fact>" + objEnvio.co_Destino + "</f461_id_co_fact>";
                        strXML_GT += "<f461_id_tercero_rem>" + f461_id_tercero_rem + "</f461_id_tercero_rem>";
                        strXML_GT += "<f461_id_sucursal_rem>" + f461_id_sucursal_rem + "</f461_id_sucursal_rem>";
                        strXML_GT += "<f461_id_tercero_vendedor>" + registro["SUSR5_id_vendedor"].ToString() + "</f461_id_tercero_vendedor>";
                        strXML_GT += "<f461_referencia>" + objEnvio.identificador2 + "</f461_referencia>";
                        strXML_GT += "<f461_id_cond_pago>" + objEnvio.co_Origen + "</f461_id_cond_pago>";
                        strXML_GT += "<f461_notas>" + objEnvio.tipoDoc_Origen + "</f461_notas>";

                        strXML_GT += "</Documentos>";
                    }



                    foreach (DataRow registro in objEnvio.dtMovimientos.Rows)
                    {
                        string f470_id_motivo = "";
                        string f470_id_un_movto = "";
                        Conversiones_Genericas.destelle_campos_Guion(registro["D_SUSR5_id_motivo"].ToString(), ref f470_id_motivo, ref f470_id_un_movto);

                        strXML_GT += "<Movimientos>";
                        strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>";
                        strXML_GT += "<f470_id_co>" + objEnvio.co_Destino + "</f470_id_co>";
                        strXML_GT += "<f470_id_tipo_docto>" + objEnvio.tipoDoc_Destino + "</f470_id_tipo_docto>";
                        strXML_GT += "<f470_consec_docto>" + objEnvio.numDoc_Destino + "</f470_consec_docto>";
                        strXML_GT += "<f470_nro_registro>" + registro["nro_registro"].ToString() + "</f470_nro_registro>";
                        strXML_GT += "<f470_id_bodega>" + registro["D_SUSR3_rowid_bod_ent"].ToString() + "</f470_id_bodega>";
                        strXML_GT += "<f470_id_motivo>" + f470_id_motivo + "</f470_id_motivo>";
                        strXML_GT += "<f470_id_co_movto>" + objEnvio.co_Destino + "</f470_id_co_movto>";
                        strXML_GT += "<f470_id_ccosto_movto></f470_id_ccosto_movto>";
                        strXML_GT += "<f470_id_lista_precio>" + registro["D_SUSR2_id_lista_precio"].ToString() + "</f470_id_lista_precio>";
                        strXML_GT += "<f470_id_unidad_medida>" + registro["unidadMedida"].ToString() + "</f470_id_unidad_medida>";
                        strXML_GT += "<f470_cant_base>" + registro["Cantidad"].ToString() + "</f470_cant_base>";
                        strXML_GT += "<f470_vlr_bruto>" + registro["D_SUSR4_vlr_bruto"].ToString() + "</f470_vlr_bruto>";
                        strXML_GT += "<f470_referencia_item>" + registro["SKU"].ToString() + "</f470_referencia_item>";
                        strXML_GT += "<f470_id_un_movto>" + f470_id_un_movto + "</f470_id_un_movto>";
                        strXML_GT += "</Movimientos>";
                    }


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
            objEnvio.idDocumentoGT = 114935;
            objEnvio.strDocumentoGT = "WMS_NOTA_NV2_DESDE_PDC_Celis";

            EnviarDatosUNOEE.enviar(ref objEnvio);

        }
    }
}

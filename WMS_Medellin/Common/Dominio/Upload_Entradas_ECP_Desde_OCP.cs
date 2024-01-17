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
    public class Upload_Entradas_ECP_Desde_OCP : IUpload
    {
        public void procesar(EstructuraUploadModel objEnvio)
        {
            string strXML_GT = "";

            // //////////////////////////////////////////////////////////////////////////////////////////////////
            if (objEnvio.bitError == false)
            {
                try
                {
                    

                    strXML_GT += "<MyDataSet>" + Environment.NewLine;

                    strXML_GT += "<Inicial>" + Environment.NewLine;
                    strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>" + Environment.NewLine;
                    strXML_GT += "</Inicial>" + Environment.NewLine;

                    foreach (DataRow registro in objEnvio.dtDocumentos.Rows)
                    {


                        string idProveedor = "";
                        string idSucProveedor = "";
                        string idMoneda = "";
                        string idMonedaConversion = "";
                        string idTasaConv = "";
                        string idTasaLocal = "";        
                    
                        Conversiones_Genericas.destelle_campos_Guion(registro["SUSR2_Proveedor"].ToString(), ref idProveedor, ref idSucProveedor);
                        Conversiones_Genericas.destelle_campos_Guion(registro["SUSR5_Moneda"].ToString(), ref idMoneda, ref idMonedaConversion);
                        Conversiones_Genericas.destelle_campos_Guion(registro["SUSR4_Tasas"].ToString(), ref idTasaConv, ref idTasaLocal);


                        strXML_GT += "<Documentos>";
                        strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>";
                        strXML_GT += "<f350_id_co>" + objEnvio.co_Destino + "</f350_id_co>";
                        strXML_GT += "<f350_id_tipo_docto>" + objEnvio.tipoDoc_Destino + "</f350_id_tipo_docto>";
                        strXML_GT += "<f350_consec_docto>" + objEnvio.numDoc_Destino + "</f350_consec_docto>";
                        strXML_GT += "<f350_fecha>" + registro["f420_fecha"].ToString() + "</f350_fecha>";
                        
                        strXML_GT += "<f350_id_tercero>" + idProveedor + "</f350_id_tercero>";
                        strXML_GT += "<f451_id_sucursal_prov>" + idSucProveedor + "</f451_id_sucursal_prov>";
                       
                        //strXML_GT += "<f350_id_tercero>" + registro["idProveedor"].ToString() + "</f350_id_tercero>";
                        //strXML_GT += "<f451_id_sucursal_prov>" + registro["f420_id_sucursal_prov"].ToString() + "</f451_id_sucursal_prov>";
                        
                        strXML_GT += "<f451_id_tercero_comprador>" + registro["SUSR3_idComp"].ToString() + "</f451_id_tercero_comprador>";
                        strXML_GT += "<f451_num_docto_referencia>" + objEnvio.identificador2 + "</f451_num_docto_referencia>";


                        strXML_GT += "<f451_id_moneda_docto>" + idMoneda + "</f451_id_moneda_docto>";
                        strXML_GT += "<f451_id_moneda_conv>" + idMonedaConversion + "</f451_id_moneda_conv>";

                        strXML_GT += "<f451_tasa_conv>" + idTasaConv + "</f451_tasa_conv>";
                        strXML_GT += "<f451_tasa_local>" + idTasaLocal + "</f451_tasa_local>";



                        //strXML_GT += "<f451_id_moneda_docto>" + registro["f420_id_moneda_docto"].ToString() + "</f451_id_moneda_docto>";
                        //strXML_GT += "<f451_id_moneda_conv>" + registro["f420_id_moneda_conv"].ToString() + "</f451_id_moneda_conv>";

                        //strXML_GT += "<f451_tasa_conv>" + registro["f420_tasa_conv"].ToString() + "</f451_tasa_conv>";
                        //strXML_GT += "<f451_tasa_local>" + registro["f420_tasa_local"].ToString() + "</f451_tasa_local>";

                        strXML_GT += "<f420_id_co_docto>" + objEnvio.co_Origen + "</f420_id_co_docto>";
                        strXML_GT += "<f420_id_tipo_docto>" + objEnvio.tipoDoc_Origen + "</f420_id_tipo_docto>";
                        strXML_GT += "<f420_consec_docto>" + objEnvio.numDoc_Origen + "</f420_consec_docto>";
                        strXML_GT += "<f350_notas> </f350_notas>";
                        strXML_GT += "</Documentos>";
                                         
                    }

                    foreach (DataRow registro in objEnvio.dtMovimientos.Rows)
                    {
                        strXML_GT += "<Movimientos>";
                        strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>";
                        strXML_GT += "<f470_id_co>" + objEnvio.co_Destino + "</f470_id_co>";
                        strXML_GT += "<f470_id_tipo_docto>" + objEnvio.tipoDoc_Destino + "</f470_id_tipo_docto>";
                        strXML_GT += "<f470_consec_docto>" + objEnvio.numDoc_Destino + "</f470_consec_docto>";
                        strXML_GT += "<NumRegistro>" + registro["nro_registro"].ToString() + "</NumRegistro>";
                        strXML_GT += "<f470_id_bodega>" + registro["bodega_Destino"].ToString() + "</f470_id_bodega>";
                        strXML_GT += "<f470_id_unidad_medida>" + registro["unidadMedida"].ToString() + "</f470_id_unidad_medida>";
                        strXML_GT += "<f421_fecha_entrega>" + registro["f421_fecha_entrega"].ToString() + "</f421_fecha_entrega>";
                        strXML_GT += "<f470_cant_base>" + registro["Cantidad"].ToString() + "</f470_cant_base>";
                        strXML_GT += "<f470_referencia_item>" + registro["SKU"].ToString() + "</f470_referencia_item>";
                        strXML_GT += "<f470_notas>" + objEnvio.identificador2 + "</f470_notas>";
                        strXML_GT += "<f470_id_ccosto_movto></f470_id_ccosto_movto>";
                        strXML_GT += "</Movimientos>";
                    }



                    foreach (DataRow registro in objEnvio.dtDescuentos.Rows)
                    {
                       
                        if (Information.IsNumeric(registro["f421_vlr_dscto_linea"].ToString()))
                        {
                            if (int.Parse(registro["f421_vlr_dscto_linea"].ToString()) > 0)
                            {
                                strXML_GT += "<Descuentos>";
                                strXML_GT += "<F_CIA>" + objEnvio.cia_Destino + "</F_CIA>";
                                strXML_GT += "<f471_id_co>" + objEnvio.co_Destino + "</f471_id_co>";
                                strXML_GT += "<f471_id_tipo_docto>" + objEnvio.tipoDoc_Destino + "</f471_id_tipo_docto>";
                                strXML_GT += "<f471_consec_docto>" + objEnvio.numDoc_Destino + "</f471_consec_docto>";
                                strXML_GT += "<NumRegistro>" + registro["nro_registro"].ToString() + "</NumRegistro>";
                                strXML_GT += "<f471_orden>1</f471_orden>";
                                strXML_GT += "<f471_tasa>" + registro["f422_tasa"].ToString() + "</f471_tasa>";
                                strXML_GT += "<f471_vlr_tot>" + registro["f421_vlr_dscto_linea"].ToString() + "</f471_vlr_tot>";
                                strXML_GT += "</Descuentos>";

                            }
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
            objEnvio.idDocumentoGT = 77709;
            objEnvio.strDocumentoGT = "WMS_ENTRADA_DESDE_OC_ECP";

            EnviarDatosUNOEE.enviar(ref objEnvio);

        }
    }
}

Public Class ImportacionesSiesa

    Public Sub enviarDatos(ByVal objEnvio As EstructuraUploadModel)


        Dim dsDatosGT As New DataSet
        Dim strXML_GT As String = ""
        Dim plano As String = ""


        '//////////////////////////////////////////////////////////////////////////////////////////////////
        If objEnvio.bitError = False Then

            Try

                strXML_GT &= "<MyDataSet>"

                strXML_GT &= "<Inicial>"
                strXML_GT &= "<F_CIA>" & objEnvio.cia_Destino & "</F_CIA>"
                strXML_GT &= "</Inicial>"


                For Each registro As DataRow In objEnvio.dtDocumentos.Rows

                    Dim idProveedor As String = ""
                    Dim idSucProveedor As String = ""
                    Dim idMoneda As String = ""
                    Dim idMonedaConversion As String = ""
                    Dim idTasaConv As String = ""
                    Dim idTasaLocal As String = ""

                    ModuloConversionesDatos.destelle_campos_Guion(registro.Item("proveedor").ToString, idProveedor, idSucProveedor)
                    ModuloConversionesDatos.destelle_campos_Guion(registro.Item("moneda").ToString, idMoneda, idMonedaConversion)
                    ModuloConversionesDatos.destelle_campos_Guion(registro.Item("tasa").ToString, idTasaConv, idTasaLocal)


                    strXML_GT &= "<Documentos>"
                    strXML_GT &= "<F_CIA>" & objEnvio.cia_Destino & "</F_CIA>"
                    strXML_GT &= "<f350_id_co>" & objEnvio.co_Destino & "</f350_id_co>"
                    strXML_GT &= "<f350_id_tipo_docto>" & objEnvio.tipoDoc_Destino & "</f350_id_tipo_docto>"
                    strXML_GT &= "<f350_consec_docto>" & objEnvio.numDoc_Destino & "</f350_consec_docto>"
                    strXML_GT &= "<f350_fecha>" & registro.Item("fecha_DoctoDestino").ToString & "</f350_fecha>"
                    strXML_GT &= "<f350_id_tercero>" & idProveedor & "</f350_id_tercero>"
                    strXML_GT &= "<f451_id_sucursal_prov>" & idSucProveedor & "</f451_id_sucursal_prov>"
                    strXML_GT &= "<f451_id_tercero_comprador>" & registro.Item("comprador").ToString & "</f451_id_tercero_comprador>"
                    strXML_GT &= "<f451_num_docto_referencia>" & objEnvio.identificador2 & "</f451_num_docto_referencia>"
                    strXML_GT &= "<f451_id_moneda_docto>" & idMoneda & "</f451_id_moneda_docto>"
                    strXML_GT &= "<f451_id_moneda_conv>" & idMonedaConversion & "</f451_id_moneda_conv>"
                    strXML_GT &= "<f451_tasa_conv>" & idTasaConv & "</f451_tasa_conv>"
                    strXML_GT &= "<f451_tasa_local>" & idTasaLocal & "</f451_tasa_local>"
                    strXML_GT &= "<f420_id_co_docto>" & objEnvio.co_Origen & "</f420_id_co_docto>"
                    strXML_GT &= "<f420_id_tipo_docto>" & objEnvio.tipoDoc_Origen & "</f420_id_tipo_docto>"
                    strXML_GT &= "<f420_consec_docto>" & objEnvio.numDoc_Origen & "</f420_consec_docto>"
                    strXML_GT &= "<f350_notas> </f350_notas>"
                    strXML_GT &= "</Documentos>"
                Next


                For Each registro As DataRow In objEnvio.dtMovimientos.Rows

                    strXML_GT &= "<Movimientos>"
                    strXML_GT &= "<F_CIA>" & objEnvio.cia_Destino & "</F_CIA>"
                    strXML_GT &= "<f470_id_co>" & objEnvio.co_Destino & "</f470_id_co>"
                    strXML_GT &= "<f470_id_tipo_docto>" & objEnvio.tipoDoc_Destino & "</f470_id_tipo_docto>"
                    strXML_GT &= "<f470_consec_docto>" & objEnvio.numDoc_Destino & "</f470_consec_docto>"
                    strXML_GT &= "<NumRegistro>" & registro.Item("nro_registro").ToString & "</NumRegistro>"
                    strXML_GT &= "<f470_id_bodega>" & registro.Item("bodega_Destino").ToString & "</f470_id_bodega>"
                    strXML_GT &= "<f470_id_unidad_medida>" & registro.Item("unidadMedida").ToString & "</f470_id_unidad_medida>"
                    strXML_GT &= "<f421_fecha_entrega>" & registro.Item("fechaEntrega").ToString & "</f421_fecha_entrega>"
                    strXML_GT &= "<f470_cant_base>" & CInt(registro.Item("Cantidad").ToString) & "</f470_cant_base>"
                    strXML_GT &= "<f470_referencia_item>" & registro.Item("SKU").ToString & "</f470_referencia_item>"
                    strXML_GT &= "<f470_notas>" & objEnvio.identificador2 & "</f470_notas>"
                    strXML_GT &= "<f470_id_ccosto_movto></f470_id_ccosto_movto>"
                    strXML_GT &= "</Movimientos>"
                Next


                For Each registro As DataRow In objEnvio.dtDescuentos.Rows

                    If IsNumeric(registro.Item("D_SUSR5_Valor_dscto").ToString) Then
                        If CInt(registro.Item("D_SUSR5_Valor_dscto").ToString) > 0 Then
                            strXML_GT &= "<Descuentos>"
                            strXML_GT &= "<F_CIA>" & objEnvio.cia_Destino & "</F_CIA>"
                            strXML_GT &= "<f471_id_co>" & objEnvio.co_Destino & "</f471_id_co>"
                            strXML_GT &= "<f471_id_tipo_docto>" & objEnvio.tipoDoc_Destino & "</f471_id_tipo_docto>"
                            strXML_GT &= "<f471_consec_docto>" & objEnvio.numDoc_Destino & "</f471_consec_docto>"
                            strXML_GT &= "<NumRegistro>" & registro.Item("nro_registro").ToString & "</NumRegistro>"
                            strXML_GT &= "<f471_orden>1</f471_orden>"
                            strXML_GT &= "<f471_tasa>" & registro.Item("TasaDescuento").ToString & "</f471_tasa>"
                            strXML_GT &= "<f471_vlr_tot>" & CInt(registro.Item("ValorDescuento").ToString) & "</f471_vlr_tot>"
                            strXML_GT &= "</Descuentos>"
                        End If
                    End If

                Next

                strXML_GT &= "<Final>"
                strXML_GT &= "<F_CIA>" & objEnvio.cia_Destino & "</F_CIA>"
                strXML_GT &= "</Final>"

                strXML_GT &= "</MyDataSet>"

            Catch ex As Exception
                objEnvio.bitError = True
                objEnvio.datosEnviados_GT = strXML_GT
                objEnvio.otrosDetalles = "Error al armar el xml de GT " & ex.Message
            End Try
        End If
        '//////////////////////////////////////////////////////////////////////////////////////////////////


        '//////////////////////////////////////////////////////////////////////////////////////////////////
        If objEnvio.bitError = False Then
            Try
                dsDatosGT = ModuloConversionesDatos.convertirXML_to_DataSet(strXML_GT)
            Catch ex As Exception
                objEnvio.bitError = True
                objEnvio.datosEnviados_GT = strXML_GT
                objEnvio.otrosDetalles = "Error al convertir el XML de GT en dataset " & ex.Message
            End Try
        End If
        '//////////////////////////////////////////////////////////////////////////////////////////////////


        '//////////////////////////////////////////////////////////////////////////////////////////////////
        If objEnvio.bitError = False Then
            Try
                Dim resultadoConsumoGT As String = ""
                plano = ConsumoGT(77709, "WMS_ENTRADAS_DESDE_OC", objEnvio.cia_Destino, dsDatosGT, resultadoConsumoGT)

                If (resultadoConsumoGT <> "Se genero el plano correctamente") Then
                    objEnvio.bitError = True
                    objEnvio.datosEnviados_GT = dsDatosGT.GetXml
                    objEnvio.resultado_GT = "Error al consumir GTIntegration " & resultadoConsumoGT
                End If

            Catch ex As Exception
                objEnvio.bitError = True
                objEnvio.datosEnviados_GT = dsDatosGT.GetXml
                objEnvio.resultado_GT = "Error al consumir GTIntegration " & ex.Message
            End Try
        End If
        '//////////////////////////////////////////////////////////////////////////////////////////////////


        '//////////////////////////////////////////////////////////////////////////////////////////////////
        'Cargar UNOEE
        If objEnvio.bitError = False Then
            Try
                CargasWSUNOEE(objEnvio.identificador1, plano, objEnvio)
            Catch ex As Exception
                objEnvio.bitError = True
                objEnvio.otrosDetalles = ex.Message
            End Try
        End If
        '//////////////////////////////////////////////////////////////////////////////////////////////////


        '//////////////////////////////////////////////////////////////////////////////////////////////////
        'Cargar UNOEE
        ModuloSQL.sp_Admin_Log_Upload_Insert(objEnvio)
        '//////////////////////////////////////////////////////////////////////////////////////////////////


    End Sub

End Class

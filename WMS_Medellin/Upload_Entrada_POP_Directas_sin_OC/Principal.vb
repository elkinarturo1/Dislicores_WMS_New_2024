Public Class Principal
    Private Sub Principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim dsDatos As New DataSet
        Dim dsConfigTarea As New DataSet
        Dim bitEncendida As Boolean = False
        Dim urlWebServiceUNOEE As String = ""

        Try

            Dim nombreTareaInterface As String = My.Settings.nombreTareaInterface
            Dim procedimientoPrincipal As String = My.Settings.procedimientoPrincipal

            'Consultar Configuracion de la Tarea
            ModuloSQL.sp_Admin_Consultar_Url_WSUNOEE(nombreTareaInterface, bitEncendida, urlWebServiceUNOEE)

            If bitEncendida Then

                'Traer Datos a Enviar
                dsDatos = ModuloSQL.ejecucionProcedimientoPrincipal(procedimientoPrincipal)

                If dsDatos.Tables.Count > 0 Then
                    If dsDatos.Tables(0).Rows.Count > 0 Then

                        For Each registro As DataRow In dsDatos.Tables(0).Rows

                            Dim objEnvio As New EstructuraUploadModel

                            Try

                                objEnvio.urlWebServiceUNOEE = urlWebServiceUNOEE


                                objEnvio.identificador1 = registro.Item("paquete_Origen").ToString
                                objEnvio.identificador2 = registro.Item("referencia").ToString

                                objEnvio.cia_Destino = registro.Item("cia_Destino").ToString
                                objEnvio.co_Destino = registro.Item("co_Destino").ToString
                                objEnvio.tipoDoc_Destino = registro.Item("tipoDoc_Destino").ToString
                                objEnvio.numDoc_Destino = registro.Item("numDoc_Destino").ToString

                                objEnvio.cia_Origen = registro.Item("cia_Origen").ToString
                                objEnvio.co_Origen = registro.Item("co_Origen").ToString
                                objEnvio.tipoDoc_Origen = registro.Item("tipoDoc_Origen").ToString
                                objEnvio.numDoc_Origen = registro.Item("numDoc_Origen").ToString

                                'Consulta ultimo consecutivo ingresado como documento entrada en Siesa
                                ModuloSQL.sp_Admin_Consultar_Consecutivo_Siesa(objEnvio.cia_Origen, objEnvio.co_Origen, objEnvio.tipoDoc_Origen, objEnvio.numDoc_Destino)


                                Dim drDocumento() As DataRow = dsDatos.Tables(1).Select("paquete_Origen = '" & Trim(objEnvio.identificador1) & "'")
                                objEnvio.dtDocumentos = drDocumento.CopyToDataTable()

                                Dim drMovimientos() As DataRow = dsDatos.Tables(2).Select("paquete_Origen = '" & Trim(objEnvio.identificador1) & "'")
                                objEnvio.dtMovimientos = drMovimientos.CopyToDataTable()

                                Dim drDescuentos() As DataRow = dsDatos.Tables(3).Select("paquete_Origen = '" & Trim(objEnvio.identificador1) & "'")
                                objEnvio.dtDescuentos = drDescuentos.CopyToDataTable()

                                'La Entrada desde OC no trabaja en paralelo
                                Dim objPaquete As New ImportacionesSiesa
                                objPaquete.enviarDatos(objEnvio)

                                'Dim AddThread1 As Threading.Thread
                                'AddThread1 = New Threading.Thread(AddressOf objPaquete.enviarDatos)
                                'AddThread1.Start(objEnvio)

                                'System.Threading.Thread.Sleep(2000)

                                'If contadorPaquetes = My.Settings.numeroPaquetesCorte Then
                                '    System.Threading.Thread.Sleep(My.Settings.tiempoEsperaPaquete)
                                'Else
                                '    System.Threading.Thread.Sleep(500)
                                'End If

                            Catch ex As Exception
                                objEnvio.bitError = True
                                objEnvio.otrosDetalles = ex.Message
                                ModuloSQL.sp_Admin_Log_Upload_Insert(objEnvio)
                            End Try

                        Next

                    End If
                End If

            End If

        Catch ex As Exception
            Dim objLog As New EstructuraUploadModel
            objLog.bitError = True
            objLog.identificador1 = "Upload_Entradas_Desde_OC"
            objLog.otrosDetalles = ex.Message
            ModuloSQL.sp_Admin_Log_Upload_Insert(objLog)
        End Try

        Me.Close()

    End Sub
End Class
﻿Imports Dominio
Imports Models

Public Class Principal
    Private Sub Principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim strConexionSQL As String

        Try
            If My.Settings.bitPruebas Then
                strConexionSQL = My.Settings.strConexionSQL_Pruebas
            Else
                strConexionSQL = My.Settings.strConexionSQL
            End If

            Asignar_Variables_Globales.asignar_strConexionSQL_Proceso(strConexionSQL)
            Asignar_Variables_Globales.asignar_nombreTareaInterface(My.Settings.nombreTareaInterface)
            Asignar_Variables_Globales.asignar_procedimientoPrincipal(My.Settings.procedimientoPrincipal)

            Asignar_Variables_Globales.asignar_bitEnviar_UNOEE(My.Settings.EnviarDatosUNOEE)
            Asignar_Variables_Globales.asignar_cia_UNOEE(My.Settings.cia_UNOEE)
            Asignar_Variables_Globales.asignar_conexion_UNOEE(My.Settings.conexion_UNOEE)
            Asignar_Variables_Globales.asignar_usuario_UNOEE(My.Settings.usuario_UNOEE)
            Asignar_Variables_Globales.asignar_clave_UNOEE(My.Settings.clave_UNOEE)
            Asignar_Variables_Globales.asignar_rutaPlanos(My.Settings.rutaPlanosGT)

            Dim dsDatos As New DataSet
            ConsultarDatos.consultar(dsDatos)


            If dsDatos.Tables.Count > 0 Then

                For Each registro As DataRow In dsDatos.Tables(0).Rows

                    Dim objEnvio As New EstructuraUploadModel
                    Dim pedido As Array

                    Try
                        objEnvio.tabla = "T_UPLOAD_Ventas_Pedidos_Comprometer_Remisionar_PEV_Rech"
                        objEnvio.identificador1 = registro.Item("paquete_Origen").ToString
                        objEnvio.identificador2 = registro.Item("referencia").ToString

                        pedido = objEnvio.identificador1.Split("-")

                        objEnvio.cia_Origen = pedido(0)
                        objEnvio.co_Origen = pedido(1)
                        objEnvio.tipoDoc_Origen = pedido(2)
                        objEnvio.numDoc_Origen = pedido(3)

                        'objEnvio.cia_Origen = registro.Item("cia_Origen").ToString
                        'objEnvio.co_Origen = registro.Item("co_Origen").ToString
                        'objEnvio.tipoDoc_Origen = registro.Item("tipoDoc_Origen").ToString
                        'objEnvio.numDoc_Origen = registro.Item("numDoc_Origen").ToString

                        objEnvio.cia_Destino = registro.Item("cia_Destino").ToString
                        objEnvio.co_Destino = registro.Item("co_Destino").ToString
                        objEnvio.tipoDoc_Destino = registro.Item("tipoDoc_Destino").ToString
                        objEnvio.numDoc_Destino = registro.Item("numDoc_Destino").ToString


                        If dsDatos.Tables.Count > 1 Then
                            Dim drDocumento() As DataRow = dsDatos.Tables(1).Select("paquete_Origen = '" & Trim(objEnvio.identificador1) & "'")
                            objEnvio.dtDocumentos = drDocumento.CopyToDataTable()
                        End If

                        If dsDatos.Tables.Count > 2 Then
                            Dim drMovimientos() As DataRow = dsDatos.Tables(2).Select("paquete_Origen = '" & Trim(objEnvio.identificador1) & "'")
                            objEnvio.dtMovimientos = drMovimientos.CopyToDataTable()
                        End If

                        If dsDatos.Tables.Count > 3 Then
                            Dim drDescuentos() As DataRow = dsDatos.Tables(3).Select("paquete_Origen = '" & Trim(objEnvio.identificador1) & "'")
                            objEnvio.dtDescuentos = drDescuentos.CopyToDataTable()
                        End If


                        Dim AddThread1 As Threading.Thread
                        Dim objImportacion As New Upload_Ventas_Pedidos_Comprometer_PEV
                        AddThread1 = New Threading.Thread(AddressOf objImportacion.procesar)
                        AddThread1.Start(objEnvio)

                        'objImportacion.procesar(objEnvio)

                        System.Threading.Thread.Sleep(2000)

                    Catch ex As Exception
                        objEnvio.bitError = True
                        objEnvio.otrosDetalles = ex.Message
                        'Logs_DAO(objEnvio)
                    End Try

                Next

            End If

        Catch ex As Exception
            Dim resultado As String = ex.Message
        End Try

        Me.Close()

    End Sub
End Class
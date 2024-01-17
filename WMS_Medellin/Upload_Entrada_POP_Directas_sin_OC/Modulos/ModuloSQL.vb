Imports System.Data.SqlClient

Module ModuloSQL

    Dim strConexionSQL As String = ""

    Sub New()

        If My.Settings.bitPruebas Then
            strConexionSQL = My.Settings.strConexionSQL_Pruebas
        Else
            strConexionSQL = My.Settings.strConexionSQL
        End If

    End Sub

    Public Sub sp_Admin_Consultar_Url_WSUNOEE(ByVal interfaz As String, ByRef bitEncendida As Boolean, ByRef urlWebServiceUNOEE As String)

        Dim sqlConexion As New SqlConnection(strConexionSQL)
        Dim sqlAdaptador As New SqlDataAdapter
        Dim sqlComando As SqlCommand = New SqlCommand
        Dim ds As New DataSet

        Try

            sqlComando.Connection = sqlConexion
            sqlComando.CommandType = CommandType.StoredProcedure
            sqlComando.CommandText = "sp_Admin_Consultar_Url_WSUNOEE"
            sqlComando.CommandTimeout = 0

            sqlComando.Parameters.AddWithValue("@interfaz", interfaz)

            sqlConexion.Open()
            sqlAdaptador.SelectCommand = sqlComando
            sqlAdaptador.Fill(ds)

            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    bitEncendida = ds.Tables(0).Rows(0).Item("bitEncendida")
                    urlWebServiceUNOEE = ds.Tables(0).Rows(0).Item("urlWebServiceUNOEE")
                End If
            End If

        Catch ex As Exception
            Throw ex
        Finally
            sqlConexion.Close()
            sqlComando.Parameters.Clear()
            sqlComando.Connection.Close()
        End Try

    End Sub

    Public Function ejecucionProcedimientoPrincipal(ByVal nombreProcedimientoSP As String) As DataSet

        Dim sqlConexion As New SqlConnection(strConexionSQL)
        Dim sqlAdaptador As New SqlDataAdapter
        Dim sqlComando As SqlCommand = New SqlCommand
        Dim ds As New DataSet

        Try
            sqlComando.Connection = sqlConexion
            sqlComando.CommandType = CommandType.StoredProcedure
            sqlComando.CommandText = nombreProcedimientoSP
            sqlComando.CommandTimeout = 0

            sqlConexion.Open()
            sqlAdaptador.SelectCommand = sqlComando
            sqlAdaptador.Fill(ds)

        Catch ex As Exception
            Throw ex
        Finally
            sqlConexion.Close()
            sqlComando.Parameters.Clear()
            sqlComando.Connection.Close()
        End Try

        Return ds

    End Function

    Public Sub sp_Admin_Consultar_Consecutivo_Siesa(ByVal idCia As String, ByVal co As String, ByVal tipoDoc As String, ByRef numDoc As String)

        Dim sqlConexion As New SqlConnection(strConexionSQL)
        Dim sqlAdaptador As New SqlDataAdapter
        Dim sqlComando As SqlCommand = New SqlCommand
        Dim ds As New DataSet

        Try
            sqlComando.Connection = sqlConexion
            sqlComando.CommandType = CommandType.StoredProcedure
            sqlComando.CommandText = "sp_Admin_Consultar_Consecutivo_Siesa"
            sqlComando.CommandTimeout = 0

            sqlComando.Parameters.AddWithValue("@idCia", idCia)
            sqlComando.Parameters.AddWithValue("@co", co)
            sqlComando.Parameters.AddWithValue("@tipoDoc", tipoDoc)

            sqlConexion.Open()
            sqlAdaptador.SelectCommand = sqlComando
            sqlAdaptador.Fill(ds)

            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    numDoc = ds.Tables(0).Rows(0).Item("f022_cons_proximo")
                End If
            End If

        Catch ex As Exception
            Throw ex
        Finally
            sqlConexion.Close()
            sqlComando.Parameters.Clear()
            sqlComando.Connection.Close()
        End Try

    End Sub

    Public Sub sp_Admin_Log_Upload_Insert(ByVal modelo As EstructuraUploadModel)

        Dim sqlConexion As SqlConnection
        Dim sqlComando As SqlCommand = New SqlCommand

        Try

            sqlConexion = New SqlConnection(strConexionSQL)

            sqlComando.Connection = sqlConexion
            sqlComando.CommandType = CommandType.StoredProcedure
            sqlComando.CommandText = "sp_Admin_Log_Upload_Insert"
            sqlComando.CommandTimeout = 0

            sqlComando.Parameters.AddWithValue("@bitError", modelo.bitError)
            sqlComando.Parameters.AddWithValue("@identificador1", modelo.identificador1)
            sqlComando.Parameters.AddWithValue("@identificador2", modelo.identificador2)
            sqlComando.Parameters.AddWithValue("@cia", modelo.cia_Origen)
            sqlComando.Parameters.AddWithValue("@co", modelo.co_Origen)
            sqlComando.Parameters.AddWithValue("@tipoDoc", modelo.tipoDoc_Origen)
            sqlComando.Parameters.AddWithValue("@numDoc", modelo.numDoc_Origen)
            sqlComando.Parameters.AddWithValue("@datosEnviados_GT", modelo.datosEnviados_GT)
            sqlComando.Parameters.AddWithValue("@resultado_GT", modelo.resultado_GT)
            sqlComando.Parameters.AddWithValue("@detalleResultado_GT", modelo.detalleResultado_GT)
            sqlComando.Parameters.AddWithValue("@datosEnviados_UNOEE", modelo.datosEnviados_UNOEE)
            sqlComando.Parameters.AddWithValue("@resultado_UNOEE", modelo.resultado_UNOEE)
            sqlComando.Parameters.AddWithValue("@detalleResultado_UNOEE", modelo.detalleResultado_UNOEE)
            sqlComando.Parameters.AddWithValue("@otrosDetalles", modelo.otrosDetalles)


            sqlComando.Connection.Open()
            sqlComando.ExecuteNonQuery()

        Catch ex As Exception

        Finally
            sqlConexion.Close()
            sqlComando.Parameters.Clear()
            sqlComando.Connection.Close()
        End Try

    End Sub

End Module

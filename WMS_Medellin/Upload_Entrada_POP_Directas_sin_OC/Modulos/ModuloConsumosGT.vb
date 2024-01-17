Module ModuloConsumosGT

    Public Function ConsumoGT(ByVal idDocumento As String, ByVal nombreDocumento As String, ByVal cia As String, ByVal dsDatosGT As DataSet, ByRef resultadoConsumoGT As String) As String

        Dim plano As String = ""
        Dim objGT As New WSGT.wsGenerarPlano

        Try
            Dim rutaPlanos As String = My.Settings.rutaPlanosGT
            plano = objGT.GenerarPlanoXML(idDocumento, nombreDocumento, 2, cia, "gt", "gt", dsDatosGT, rutaPlanos, resultadoConsumoGT)

        Catch ex As Exception
            Throw ex
        Finally
            objGT.Dispose()
        End Try

        Return plano

    End Function

End Module

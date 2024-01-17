Imports Dominio

Public Class Principal
    Private Sub Principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim strConexionSQL As String

        Try

            If My.Settings.bitPruebas Then
                strConexionSQL = My.Settings.strConexionSQL_Pruebas
            Else
                strConexionSQL = My.Settings.strConexionSQL
            End If

            Asignar_Variables_Globales.asignar_strConexionSQL(strConexionSQL)
            Crear_ASN_Receipt.ejecutarProceso("sp_BOG_Download_Transferencias_TCO")

        Catch ex As Exception
        End Try

        Me.Close()

    End Sub
End Class
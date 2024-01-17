Imports System.IO
Imports System.Xml

Module ModuloConversionesDatos

    Public Function convertirXML_to_DataSet(ByVal strXML As String) As DataSet

        Dim ds As New DataSet

        Try

            'Valida la estructura del XML convirtiendolo a DataSet
            Dim txtReader1 As TextReader = New StringReader(strXML)
            Dim reader1 As XmlReader = New XmlTextReader(txtReader1)
            ds.ReadXml(reader1)

        Catch ex As Exception
            Throw ex
        End Try

        Return ds

    End Function

    Public Sub destelle_campos_Guion(ByVal campoOrigen As String, ByRef campo1 As String, ByRef campo2 As String)

        Dim arregloCampos As Array

        Try
            arregloCampos = campoOrigen.Split("-")

            If arregloCampos.Length > 1 Then
                campo1 = arregloCampos(0)
                campo2 = arregloCampos(1)
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

End Module

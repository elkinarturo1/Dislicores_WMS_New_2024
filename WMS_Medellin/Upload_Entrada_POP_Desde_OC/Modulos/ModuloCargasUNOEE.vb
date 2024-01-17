Module ModuloCargasUNOEE


    Public Sub CargasWSUNOEE(ByVal proceso As String, ByVal plano As String, ByRef objEnvio As EstructuraUploadModel)


        Dim objWSUNOEE As New WSUNOEE.WSUNOEE
        Dim resultadoUNOEE As Short = 123
        Dim ds As New DataSet
        Dim strMensaje As String = ""

        Dim strXML_UNOEE As String = ""

        If My.Settings.EnviarDatosUNOEE Then

            Dim strConexionUNOEE As String = My.Settings.strConexionSiesa
            If My.Settings.bitPruebas Then
                strConexionUNOEE = My.Settings.strConexionSiesaPruebas
            End If

            strXML_UNOEE &= "<Importar>"
            strXML_UNOEE &= "<NombreConexion>" & My.Settings.strConexionSiesa & "</NombreConexion>" & Environment.NewLine
            strXML_UNOEE &= "<IdCia>" & objEnvio.cia_Destino & "</IdCia>" & Environment.NewLine
            strXML_UNOEE &= "<Usuario>" & My.Settings.strUsuarioSiesa & "</Usuario>" & Environment.NewLine
            strXML_UNOEE &= "<Clave>" & My.Settings.strClaveSiesa & "</Clave>" & Environment.NewLine
            strXML_UNOEE &= plano
            strXML_UNOEE &= "</Importar>" & Environment.NewLine

            Try

                objWSUNOEE.Timeout = 120000
                objEnvio.datosEnviados_UNOEE = strXML_UNOEE
                objWSUNOEE.Url = objEnvio.urlWebServiceUNOEE
                'ds = objWSUNOEE.ImportarXML(strXML_UNOEE, resultadoUNOEE)

                Select Case resultadoUNOEE
                    Case 0
                        strMensaje = proceso & " : Importacion Exitosa " & vbNewLine
                    Case 1
                        strMensaje = proceso & " : Error : 1 - Error de datos al cargar la informacion a siesa a Siesa " & vbNewLine & vbNewLine & ds.GetXml().ToString()
                    Case 2
                        strMensaje = proceso & " : Error : 2 - El impodatos no envio algun parametro " & vbNewLine
                    Case 3
                        strMensaje = proceso & " : Error :  3 - El usuario o la contraseña que ingreso no son validos " & vbNewLine
                    Case 4
                        strMensaje = proceso & " : Error : 4 - La version del impodatos no se corresponde con la version del ERP o el impodatos esta en una maquina que no tiene cliente Siesa o el ERP esta inacesible o tiene los servicios caidos " & vbNewLine
                    Case 5
                        strMensaje = proceso & " : Error :  5 - La base de datos no existe o están ingresándole un parámetro erróneo a la hora de especificar la conexión. " & vbNewLine
                    Case 6
                        strMensaje = proceso & " : Error : 6 - El archivo que se está especificando en la ruta de los parámetros del .BAT no existe " & vbNewLine
                    Case 7
                        strMensaje = proceso & "  Error :  7 - El archivo que se está especificando en la ruta de los parámetros del .BAT no es valido " & vbNewLine
                    Case 8
                        strMensaje = proceso & " : Error : 8 - Hay un problema con la tabla en la base de datos donde se ingresaran los archivos " & vbNewLine
                    Case 9
                        strMensaje = proceso & " : Error :  9 - La compañía que se ingresó en los parámetros del .BAT no es valida " & vbNewLine
                    Case 10
                        strMensaje = proceso & " : Error : 10 - Error desconocido " & vbNewLine
                    Case 99
                        strMensaje = "Error : 99 - Error de tipo diferente a los anteriores, normalmente de permisos a nivel del ERP " & vbNewLine
                End Select

                'Dim detalle As String = "Datos Enviados : " & vbNewLine & strXML_UNOEE & vbNewLine & " Resultado : " & vbNewLine & ds.GetXml.ToString

                If resultadoUNOEE <> 0 Then
                    objEnvio.bitError = True
                    objEnvio.resultado_UNOEE = strMensaje
                End If

            Catch ex As Exception
                objEnvio.bitError = True
                objEnvio.resultado_UNOEE = ex.Message
            End Try

        End If

        objWSUNOEE.Dispose()

    End Sub

End Module
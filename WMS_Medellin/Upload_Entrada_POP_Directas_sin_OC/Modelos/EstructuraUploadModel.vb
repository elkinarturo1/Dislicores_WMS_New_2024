Public Class EstructuraUploadModel

    Public Property identificador1 As String = " "
    Public Property identificador2 As String = " "
    Public Property cia_Destino As String = " "
    Public Property co_Destino As String = " "
    Public Property tipoDoc_Destino As String = " "
    Public Property numDoc_Destino As String = " "
    Public Property cia_Origen As Integer = 0
    Public Property co_Origen As String = " "
    Public Property tipoDoc_Origen As String = " "
    Public Property numDoc_Origen As Integer = 0
    Public Property bitError As Boolean = False
    Public Property datosEnviados_GT As String = " "
    Public Property resultado_GT As String = " "
    Public Property detalleResultado_GT As String = " "
    Public Property datosEnviados_UNOEE As String = " "
    Public Property resultado_UNOEE As String = " "
    Public Property detalleResultado_UNOEE As String = " "
    Public Property urlWebServiceUNOEE As String = " "
    Public Property otrosDetalles As String = " "

    Public dtPaquetes As DataTable
    Public dtDocumentos As DataTable
    Public dtMovimientos As DataTable
    Public dtDescuentos As DataTable

End Class

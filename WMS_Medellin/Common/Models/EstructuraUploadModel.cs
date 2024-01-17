using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class EstructuraUploadModel
    {
        public string identificador1 { get; set; } = " ";
        public string identificador2 { get; set; } = " ";
        public string cia_Destino { get; set; } = " ";
        public string co_Destino { get; set; } = " ";
        public string tipoDoc_Destino { get; set; } = " ";
        public string numDoc_Destino { get; set; } = " ";
        public int cia_Origen { get; set; } = 0;
        public string co_Origen { get; set; } = " ";
        public string tipoDoc_Origen { get; set; } = " ";
        public int numDoc_Origen { get; set; } = 0;
        public string co_Transf_Salida { get; set; } = " ";

        public int idDocumentoGT { get; set; } = 0;
        public string strDocumentoGT { get; set; } = " ";

        public bool bitError { get; set; } = false;
        public string datosEnviados_GT { get; set; } = " ";
        public string resultado_GT { get; set; } = " ";
        public string detalleResultado_GT { get; set; } = " ";
        public string datosEnviados_UNOEE { get; set; } = " ";
        public string resultado_UNOEE { get; set; } = " ";
        public string detalleResultado_UNOEE { get; set; } = " ";
        public string urlWebServiceUNOEE { get; set; } = " ";
        public string otrosDetalles { get; set; } = " ";
        public string tabla { get; set; } = " ";

        public DataSet dsDatos;
        public DataTable dtPaquetes;
        public DataTable dtDocumentos;
        public DataTable dtMovimientos;
        public DataTable dtDescuentos;
        public DataTable dtCxC;
    }
}

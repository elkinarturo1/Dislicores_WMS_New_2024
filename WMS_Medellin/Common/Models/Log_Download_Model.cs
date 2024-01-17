using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Log_Download_Model
    {
        public bool bitError { get; set; } = false;
        public string identificador1 { get; set; }
        public string identificador2 { get; set; }
        public int cia { get; set; } = -1;
        public string co { get; set; } = "-1";
        public string tipoDoc { get; set; } = "-1";
        public int numDoc { get; set; } = -1;
        public string datosEnviados { get; set; } = "-1";
        public string detalleResultado { get; set; } = "-1";
    }
}

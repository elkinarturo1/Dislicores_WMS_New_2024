using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class clsModelo_OrderDetails
    {
        public string STORERKEY { get; set; }
        public string EXTERNORDERKEY { get; set; }
        public string EXTERNLINENO { get; set; }
        public string SKU { get; set; }
        public string UOM { get; set; }
        public string OPENQTY { get; set; }
        public string ORIGINALQTY { get; set; }
        public string lOTATTRIBUTE06 { get; set; }
        public string SUSR1 { get; set; }
        public string SUSR2 { get; set; }
        public string SUSR3 { get; set; }
        public string SUSR4 { get; set; }
        public string SUSR5 { get; set; }
        public string WHSEID { get; set; }
        public string Fecha_erp { get; set; }
        public string Fecha_ION { get; set; }
        public string ION_flag { get; set; }
    }
}

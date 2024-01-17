using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class clsModelo_Receipt 
    {
        public string STORERKEY { get; set; }
        public string EXTERNRECEIPTKEY { get; set; }
        public string TYPE { get; set; }
        public string NOTES { get; set; }
        public string SUSR1 { get; set; }
        public string SUSR2 { get; set; }
        public string SUSR3 { get; set; }
        public string SUSR4 { get; set; }
        public string SUSR5 { get; set; }
        public string WHSEID { get; set; }
        public string Fecha_erp { get; set; }
        public string Fecha_ION { get; set; }
        public string ION_flag { get; set; }
        public string IDKEY { get; set; }
        public string SUPPLIERCODE { get; set; }
    }
}

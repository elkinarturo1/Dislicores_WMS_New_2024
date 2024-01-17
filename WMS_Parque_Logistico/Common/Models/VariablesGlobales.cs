using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class VariablesGlobales
    {
        public static string strConexionSQL { get; set; }

        public static string nomConexionProceso { get; set; }
        public static string strConexionSQL_Proceso { get; set; }
        public static string strConexionOracle { get; set; }
        public static string nombreTareaInterface { get; set; }
        public static string procedimientoPrincipal { get; set; }


        public static bool bitEnviar_UNOEE { get; set; }
        public static string proceso { get; set; }
        public static string conexion_UNOEE { get; set; }
        public static string cia_UNOEE { get; set; }
        public static string usuario_UNOEE { get; set; }
        public static string clave_UNOEE { get; set; }
        public static string url_UNOEE { get; set; }

        public static string rutaPlanos { get; set; }

        public static int idConectorGT_1 { get; set; }   
        public static string strConectorGT_1 { get; set; }

        public static int idConectorGT_2 { get; set; }
        public static string strConectorGT_2 { get; set; }


    }
}

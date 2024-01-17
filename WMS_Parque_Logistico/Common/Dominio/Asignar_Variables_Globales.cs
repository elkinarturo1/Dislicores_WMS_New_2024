using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public static class Asignar_Variables_Globales
    {
        public static void asignar_strConexionSQL(string p_Variable) => VariablesGlobales.strConexionSQL = p_Variable;

        public static void asignar_nomConexionProceso(string p_Variable) => VariablesGlobales.nomConexionProceso = p_Variable;
        public static void asignar_strConexionSQL_Proceso(string p_Variable) => VariablesGlobales.strConexionSQL_Proceso = p_Variable;
        public static void asignar_strConexionOracle(string p_Variable) => VariablesGlobales.strConexionOracle = p_Variable;
        public static void asignar_nombreTareaInterface(string p_Variable) => VariablesGlobales.nombreTareaInterface = p_Variable;

        public static void asignar_procedimientoPrincipal(string p_Variable) => VariablesGlobales.procedimientoPrincipal = p_Variable;

        public static void asignar_bitEnviar_UNOEE(bool p_Variable) => VariablesGlobales.bitEnviar_UNOEE = p_Variable;

        public static void asignar_proceso(string p_Variable) => VariablesGlobales.proceso = p_Variable;

        public static void asignar_conexion_UNOEE(string p_Variable) => VariablesGlobales.conexion_UNOEE = p_Variable;

        public static void asignar_cia_UNOEE(string p_Variable) => VariablesGlobales.cia_UNOEE  = p_Variable;
        public static void asignar_usuario_UNOEE(string p_Variable) => VariablesGlobales.usuario_UNOEE = p_Variable;
        public static void asignar_clave_UNOEE(string p_Variable) => VariablesGlobales.clave_UNOEE = p_Variable;
        public static void asignar_url_UNOEE(string p_Variable) => VariablesGlobales.url_UNOEE = p_Variable;

        public static void asignar_rutaPlanos(string p_Variable) => VariablesGlobales.rutaPlanos = p_Variable;

        public static void asignar_idConector_1(int p_Variable) => VariablesGlobales.idConectorGT_1  = p_Variable;

        public static void asignar_strConector_1(string p_Variable) => VariablesGlobales.strConectorGT_1 = p_Variable;

        public static void asignar_idConector_2(int p_Variable) => VariablesGlobales.idConectorGT_2 = p_Variable;

        public static void asignar_strConector_2(string p_Variable) => VariablesGlobales.strConectorGT_2 = p_Variable;



    }
}

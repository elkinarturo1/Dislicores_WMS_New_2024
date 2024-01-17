using Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public static class Download_Configuracion_Inicial
    {

        public static void configuracionInicial(string strConexionSQL, string nomConexionProceso)
        {

            try
            {             

                Asignar_Variables_Globales.asignar_strConexionSQL(strConexionSQL);
                Asignar_Variables_Globales.asignar_nomConexionProceso(nomConexionProceso);

                string strConexionSQL_Proceso = "";
                string sp_consultaPrincipal = "";

                (strConexionSQL_Proceso, sp_consultaPrincipal) = Configuraciones_DAO.sp_Conexiones_Select();

                Asignar_Variables_Globales.asignar_strConexionSQL_Proceso(strConexionSQL_Proceso);
                Asignar_Variables_Globales.asignar_procedimientoPrincipal(sp_consultaPrincipal);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}

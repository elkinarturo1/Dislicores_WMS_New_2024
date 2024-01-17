using Infraestructure;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public static class ConsultarDatos
    {

        public static void consultar(ref DataSet dsDatos)
        {

            bool bitEncendida = false;
            string urlWebServiceUNOEE = "";

            try
            {
                (bitEncendida, urlWebServiceUNOEE) = OperationsGenericsSQL_DAO.sp_Admin_Consultar_Url_WSUNOEE();

                if (bitEncendida)
                {

                    Asignar_Variables_Globales.asignar_url_UNOEE(urlWebServiceUNOEE);

                    // Traer Datos a Enviar
                    string sp = VariablesGlobales.procedimientoPrincipal;
                    dsDatos = OperationsGenericsSQL_DAO.ejecucion_procedimiento_principal();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

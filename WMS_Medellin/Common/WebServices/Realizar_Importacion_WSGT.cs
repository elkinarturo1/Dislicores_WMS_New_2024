using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServices
{
    public static class Realizar_Importacion_WSGT
    {

        public static (string resultadoConsumoGT, string plano) ejecutarProceso(int idDocumento
                                                                                , string nombreDocumento
                                                                                , string cia
                                                                                , DataSet dsDatosGT
                                                                                , string rutaPlanos
                                                                                )
        {

            string resultadoConsumoGT = "";
            string plano = "";

            WSGT.wsGenerarPlano objGT = new WSGT.wsGenerarPlano();

            try
            {
                plano = objGT.GenerarPlanoXML(idDocumento, nombreDocumento, 2, cia, "gt", "gt", dsDatosGT, ref rutaPlanos, ref resultadoConsumoGT);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objGT.Dispose();
            }

            return (resultadoConsumoGT, plano);
        }

    }
}

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
    public static class Crear_SKU
    {

        public static void ejecutarProceso(string sp_consultaPrincipal)
        {

            DataSet ds = new DataSet();
            Log_Download_Model objLog = new Log_Download_Model();
            string id = "";

            try
            {
                
                ds = OperationsGenericsSQL_DAO.ejecucion_procedimiento_principal();

                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow registro in ds.Tables[0].Rows)
                    {
                        clsModelo_SKU objDatos = new clsModelo_SKU();
                        string strSKU = "";

                        strSKU = registro["SKU"].ToString().Substring(0, Math.Min(registro["SKU"].ToString().Length, 50));                         

                        objDatos.SKU = strSKU;
                        objDatos.BUSR1 = registro["BUSR1"].ToString().Substring(0, Math.Min(registro["BUSR1"].ToString().Length, 30));
                        objDatos.BUSR2 = registro["BUSR2"].ToString().Substring(0, Math.Min(registro["BUSR2"].ToString().Length, 30));
                        objDatos.BUSR3 = registro["BUSR3"].ToString().Substring(0, Math.Min(registro["BUSR3"].ToString().Length, 30));
                        objDatos.BUSR4 = registro["BUSR4"].ToString().Substring(0, Math.Min(registro["BUSR4"].ToString().Length, 30));
                        objDatos.BUSR5 = registro["BUSR5"].ToString().Substring(0, Math.Min(registro["BUSR5"].ToString().Length, 30));
                        objDatos.STORERKEY = registro["STORERKEY"].ToString().Substring(0, Math.Min(registro["STORERKEY"].ToString().Length, 15));
                        objDatos.DESCR = registro["DESCR"].ToString().Substring(0, Math.Min(registro["DESCR"].ToString().Length, 60));
                        objDatos.PACKKEY = registro["PACKKEY"].ToString().Substring(0, Math.Min(registro["PACKKEY"].ToString().Length, 50));
                        objDatos.STDCUBE = registro["STDCUBE"].ToString().Substring(0, Math.Min(registro["STDCUBE"].ToString().Length, 15));
                        objDatos.STDGROSSWGT = registro["STDGROSSWGT"].ToString().Substring(0, Math.Min(registro["STDGROSSWGT"].ToString().Length, 15));
                        objDatos.STDNETWGT = registro["STDNETWGT"].ToString().Substring(0, Math.Min(registro["STDNETWGT"].ToString().Length, 15));
                        objDatos.TARE = registro["TARE"].ToString().Substring(0, Math.Min(registro["TARE"].ToString().Length, 15));
                        objDatos.SHELFLIFEINDICATOR = registro["SHELFLIFEINDICATOR"].ToString().Substring(0, Math.Min(registro["SHELFLIFEINDICATOR"].ToString().Length, 15));
                        objDatos.SHELFLIFEONRECEIVING = registro["SHELFLIFEONRECEIVING"].ToString().Substring(0, Math.Min(registro["SHELFLIFEONRECEIVING"].ToString().Length, 15));
                        objDatos.SHELFLIFE = registro["SHELFLIFE"].ToString().Substring(0, Math.Min(registro["SHELFLIFE"].ToString().Length, 15));
                        objDatos.SHELFLIFECODETYPE = registro["SHELFLIFECODETYPE"].ToString().Substring(0, Math.Min(registro["SHELFLIFECODETYPE"].ToString().Length, 15));
                                              
                        SKU_DAO .sp_ION_Limpiar_SKU(strSKU);
                        SKU_DAO.sp_ION_Download_INT_SKU(objDatos);

                        if ((objLog.bitError))
                        {
                            Logs_DAO.sp_Admin_Log_Download_Insert(objLog);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

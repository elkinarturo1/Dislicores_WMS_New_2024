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
    public static class Crear_Storer
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
                        clsModelo_Storers objDatos = new clsModelo_Storers();
                        string strStorerKey = "";

                        objLog.bitError = false;
                        strStorerKey = registro["STORERKEY"].ToString().Substring(0, Math.Min(registro["STORERKEY"].ToString().Length, 15));
                        objLog.identificador1 = strStorerKey;
                        objLog.datosEnviados = ds.GetXml();

                        objDatos.STORERKEY = strStorerKey;
                        objDatos.TYPE = registro["TYPE"].ToString().Substring(0, Math.Min(registro["TYPE"].ToString().Length, 30));
                        objDatos.COMPANY = registro["COMPANY"].ToString().Substring(0, Math.Min(registro["COMPANY"].ToString().Length, 45));
                        objDatos.ADDRESS1 = registro["ADDRESS1"].ToString().Substring(0, Math.Min(registro["ADDRESS1"].ToString().Length, 45));
                        objDatos.ADDRESS5 = registro["ADDRESS5"].ToString().Substring(0, Math.Min(registro["ADDRESS5"].ToString().Length, 45));
                        objDatos.ADDRESS6 = registro["ADDRESS6"].ToString().Substring(0, Math.Min(registro["ADDRESS6"].ToString().Length, 45));
                        objDatos.ADDRESS2 = registro["ADDRESS2"].ToString().Substring(0, Math.Min(registro["ADDRESS2"].ToString().Length, 14));
                        objDatos.CITY = registro["CITY"].ToString().Substring(0, Math.Min(registro["CITY"].ToString().Length, 45));
                        objDatos.STATE = registro["STATE"].ToString().Substring(0, Math.Min(registro["STATE"].ToString().Length, 45));
                        objDatos.COUNTRY = registro["COUNTRY"].ToString().Substring(0, Math.Min(registro["COUNTRY"].ToString().Length, 30));
                        objDatos.CONTACT1 = registro["CONTACT1"].ToString().Substring(0, Math.Min(registro["CONTACT1"].ToString().Length, 30));
                        objDatos.PHONE1 = registro["PHONE1"].ToString().Substring(0, Math.Min(registro["PHONE1"].ToString().Length, 18));
                        objDatos.PHONE2 = registro["PHONE2"].ToString().Substring(0, Math.Min(registro["PHONE2"].ToString().Length, 18));
                        objDatos.EMAIL1 = registro["EMAIL1"].ToString().Substring(0, Math.Min(registro["EMAIL1"].ToString().Length, 50));
                        objDatos.SUSR1 = registro["SUSR1"].ToString().Substring(0, Math.Min(registro["SUSR1"].ToString().Length, 30));
                        objDatos.SUSR2 = registro["SUSR2"].ToString().Substring(0, Math.Min(registro["SUSR2"].ToString().Length, 30));
                        objDatos.SUSR3 = registro["SUSR3"].ToString().Substring(0, Math.Min(registro["SUSR3"].ToString().Length, 30));
                        objDatos.SUSR4 = registro["SUSR4"].ToString().Substring(0, Math.Min(registro["SUSR4"].ToString().Length, 30));
                        objDatos.SUSR5 = registro["SUSR5"].ToString().Substring(0, Math.Min(registro["SUSR5"].ToString().Length, 30));
                        objDatos.SUSR6 = registro["SUSR6"].ToString().Substring(0, Math.Min(registro["SUSR6"].ToString().Length, 30));


                        Storer_DAO.sp_ION_Limpiar_STORERS(strStorerKey);
                        Storer_DAO.sp_ION_Download_INT_STORER(objDatos);

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

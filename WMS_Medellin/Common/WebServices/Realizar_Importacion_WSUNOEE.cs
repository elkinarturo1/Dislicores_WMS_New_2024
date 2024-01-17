using Infraestructure;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServices
{
    public static class Realizar_Importacion_WSUNOEE
    {

        public static void ejecutarProceso(string plano_UNOEE,ref EstructuraUploadModel objEnvio)
        {
            WSUNOEE.WSUNOEE objWSUNOEE = new WSUNOEE.WSUNOEE();
            short resultadoUNOEE = 123;
            DataSet ds = new DataSet();
            string strMensaje = "";

            string strXML_UNOEE = "";

            if (VariablesGlobales.bitEnviar_UNOEE)
            {

                strXML_UNOEE += "<Importar>";
                strXML_UNOEE += "<NombreConexion>" + VariablesGlobales.conexion_UNOEE + "</NombreConexion>" + Environment.NewLine;
                strXML_UNOEE += "<IdCia>" + VariablesGlobales.cia_UNOEE + "</IdCia>" + Environment.NewLine;
                strXML_UNOEE += "<Usuario>" + VariablesGlobales.usuario_UNOEE + "</Usuario>" + Environment.NewLine;
                strXML_UNOEE += "<Clave>" + VariablesGlobales.clave_UNOEE + "</Clave>" + Environment.NewLine;
                strXML_UNOEE += plano_UNOEE;
                strXML_UNOEE += "</Importar>" + Environment.NewLine;

                try
                {
                    objWSUNOEE.Timeout = 1200000;
                    objWSUNOEE.Url = VariablesGlobales.url_UNOEE;
                    ds = objWSUNOEE.ImportarXML(strXML_UNOEE, ref resultadoUNOEE);

                    switch (resultadoUNOEE)
                    {
                        case 0:
                            {
                                strMensaje = VariablesGlobales.proceso + " : Importacion Exitosa " + Environment.NewLine;
                                break;
                            }

                        case 1:
                            {
                                strMensaje = VariablesGlobales.proceso + " : Error : 1 - Error de datos al cargar la informacion a siesa a Siesa " + Environment.NewLine + ds.GetXml().ToString();
                                Logs_DAO.sp_Documentos_Rechazados(objEnvio.identificador1, strMensaje,objEnvio.tabla);
                                break;
                            }

                        case 2:
                            {
                                strMensaje = VariablesGlobales.proceso + " : Error : 2 - El impodatos no envio algun parametro " + Environment.NewLine;
                                break;
                            }

                        case 3:
                            {
                                strMensaje = VariablesGlobales.proceso + " : Error :  3 - El usuario o la contraseña que ingreso no son validos " + Environment.NewLine;
                                break;
                            }

                        case 4:
                            {
                                strMensaje = VariablesGlobales.proceso + " : Error : 4 - La version del impodatos no se corresponde con la version del ERP o el impodatos esta en una maquina que no tiene cliente Siesa o el ERP esta inacesible o tiene los servicios caidos " + Environment.NewLine;
                                break;
                            }

                        case 5:
                            {
                                strMensaje = VariablesGlobales.proceso + " : Error :  5 - La base de datos no existe o están ingresándole un parámetro erróneo a la hora de especificar la conexión. " + Environment.NewLine;
                                break;
                            }

                        case 6:
                            {
                                strMensaje = VariablesGlobales.proceso + " : Error : 6 - El archivo que se está especificando en la ruta de los parámetros del .BAT no existe " + Environment.NewLine;
                                break;
                            }

                        case 7:
                            {
                                strMensaje = VariablesGlobales.proceso + "  Error :  7 - El archivo que se está especificando en la ruta de los parámetros del .BAT no es valido " + Environment.NewLine;
                                break;
                            }

                        case 8:
                            {
                                strMensaje = VariablesGlobales.proceso + " : Error : 8 - Hay un problema con la tabla en la base de datos donde se ingresaran los archivos " + Environment.NewLine;
                                break;
                            }

                        case 9:
                            {
                                strMensaje = VariablesGlobales.proceso + " : Error :  9 - La compañía que se ingresó en los parámetros del .BAT no es valida " + Environment.NewLine;
                                break;
                            }

                        case 10:
                            {
                                strMensaje = VariablesGlobales.proceso + " : Error : 10 - Error desconocido " + Environment.NewLine;
                                break;
                            }

                        case 99:
                            {
                                strMensaje = "Error : 99 - Error de tipo diferente a los anteriores, normalmente de permisos a nivel del ERP " + Environment.NewLine;
                                break;
                            }
                    }

                    if (resultadoUNOEE != 0)
                    {
                        objEnvio.bitError = true;
                        objEnvio.resultado_UNOEE = resultadoUNOEE.ToString();
                        objEnvio.detalleResultado_UNOEE = strMensaje;
                    }                   

                }
                catch (Exception ex)
                {
                    objEnvio.bitError = true;
                    objEnvio.resultado_UNOEE = ex.Message;
                }
            }

            objWSUNOEE.Dispose();

            Logs_DAO.sp_Admin_Log_Upload_Insert(objEnvio);

        }

    }
}

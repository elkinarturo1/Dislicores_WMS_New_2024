using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Dominio
{
    public static class Conversiones_Genericas
    {

        public static DataSet convertir_xml_to_dataset(string strXML)
        {

            DataSet dsDatos = new DataSet();

            try
            {
                TextReader txtReader1 = new StringReader(strXML);
                XmlReader reader1 = new XmlTextReader(txtReader1);
                dsDatos.ReadXml(reader1);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsDatos;

        }


        public static void destelle_campos_Guion(string campoOrigen, ref string campo1, ref string campo2)
        {
            string[] arregloCampos;

            try
            {
                arregloCampos = campoOrigen.Split(char.Parse("-"));

                if (arregloCampos.Length > 1)
                {
                    campo1 = arregloCampos[0];
                    campo2 = arregloCampos[1];
                }
                else
                {
                    campo1 = "0";
                    campo2 = "0";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}

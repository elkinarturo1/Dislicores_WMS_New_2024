using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure
{
    public static class Oracle_DAO
    {


        public static DataSet consultar_Vista_Oracle(string v_Oracle)
        {

            DataSet ds = new DataSet();

            OdbcConnection conexionOracle = new OdbcConnection(VariablesGlobales.strConexionOracle);
            OdbcCommand ComandoODBC = new OdbcCommand();
            OdbcDataAdapter adaptadorODBC = new OdbcDataAdapter();

            ComandoODBC.Connection = conexionOracle;
            ComandoODBC.CommandType = CommandType.Text;
            //ComandoODBC.CommandText = "{call DSP_GEO_ACT_DIRECCION(?,?)}";
            ComandoODBC.CommandText = $"select * from {v_Oracle}";
            ComandoODBC.CommandTimeout = 300; //3 minutos

            //ComandoODBC.CommandText = "DSP_GEO_ACT_DIRECCION";

            //OdbcParameter P1 = new OdbcParameter("p_f015_rowid", ParameterDirection.Input);
            //OdbcParameter P2 = new OdbcParameter("p_direccion1", ParameterDirection.Input);

            try
            {

                //P1.Value = rowIdDireccion;
                //P2.Value = p_direccion1;

                //ComandoODBC.Parameters.Add(P1);
                //ComandoODBC.Parameters.Add(P2);

                adaptadorODBC.SelectCommand = ComandoODBC;
                adaptadorODBC.Fill(ds);

                //ComandoODBC.Connection.Open();
                //ComandoODBC.ExecuteNonQuery();               

            }
            catch (Exception ex)
            {                
                throw ex;
            }
            finally
            {
                ComandoODBC.Parameters.Clear();
                ComandoODBC.Connection.Close();
            }

            return ds;

        }



    }
}

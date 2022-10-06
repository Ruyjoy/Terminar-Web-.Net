using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using EntidadesCompartidas;

namespace Persistencia
{
    internal class PersistenciaFacilidad
    {
        internal static void Alta(Facilidad facilidad, string codigoTerminal, SqlTransaction transaccion)
        {
            try
            {
                SqlCommand oComando = new SqlCommand("AltaFacilidad", transaccion.Connection);
                oComando.CommandType = CommandType.StoredProcedure;
                oComando.Parameters.AddWithValue("@Codigo", codigoTerminal);
                oComando.Parameters.AddWithValue("@Descripcion", facilidad.Descripcion);
                SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
                _Retorno.Direction = ParameterDirection.ReturnValue;
                oComando.Parameters.Add(_Retorno);
                oComando.Transaction = transaccion;
                oComando.ExecuteNonQuery();

                int resultado = (int)oComando.Parameters["@Retorno"].Value;
                if (resultado == -1)
                {
                    throw new Exception("La Terminal no existe.");
                }
                else if (resultado == -2)
                {
                    throw new Exception("Ya existe esa facilidad para la Terminal.");
                }
                else if (resultado != 0)
                {
                    throw new Exception("Ocurrió un error inesperado al dar de alta la Facilidad.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal static void BajaFacilidades(string codigoTerminal, SqlTransaction transaccion)
        {
            try
            {
                SqlCommand oComando = new SqlCommand("BajaFacilidadesTerminal", transaccion.Connection);
                oComando.CommandType = CommandType.StoredProcedure;
                oComando.Parameters.AddWithValue("@Codigo", codigoTerminal);
                SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
                _Retorno.Direction = ParameterDirection.ReturnValue;
                oComando.Parameters.Add(_Retorno);
                oComando.Transaction = transaccion;
                oComando.ExecuteNonQuery();
                int resultado = (int)oComando.Parameters["@Retorno"].Value;
                if (resultado == -1)
                {
                    throw new Exception("La Terminal no existe.");
                }

                else if (resultado != 0)
                {
                    throw new Exception("Ocurrió un error inesperado al dar de baja las Facilidades.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

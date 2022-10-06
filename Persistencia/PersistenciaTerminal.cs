using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    internal class PersistenciaTerminal:IPersistenciaTerminal
    {
         //singleton
        private static PersistenciaTerminal _instancia = null;
        private PersistenciaTerminal() { }
        public static PersistenciaTerminal GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaTerminal();
            return _instancia;
        }
        public void AltaTerminal(Terminal terminal)
        {
            SqlConnection oConexion = null;
            SqlTransaction transaccion = null;
            try
            {
                oConexion = new SqlConnection(Conexion.Cnn);
                SqlCommand oComando = new SqlCommand("AltaTerminal", oConexion);
                oComando.CommandType = CommandType.StoredProcedure;
                oComando.Parameters.AddWithValue("@Codigo", terminal.Codigo);
                oComando.Parameters.AddWithValue("@Pais", terminal.Pais);
                oComando.Parameters.AddWithValue("@NombreCiudad", terminal.NombreCiudad);
                SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
                _Retorno.Direction = ParameterDirection.ReturnValue;
                oComando.Parameters.Add(_Retorno);
                
                oConexion.Open();
                transaccion = oConexion.BeginTransaction();
                oComando.Transaction = transaccion;

                oComando.ExecuteNonQuery();

                int resultado = (int)oComando.Parameters["@Retorno"].Value;
                if (resultado == -1)
                {
                    throw new Exception("El Código de la Terminal ya existe.");
                }
                else if (resultado == -2)
                {
                    throw new Exception("El Código de la Terminal debe ser de tres carácteres.");
                }
                else if (resultado != 0)
                {
                    throw new Exception("Ocurrió un error inesperado al dar de alta la Terminal.");
                }

                foreach (Facilidad facilidad in terminal.Facilidades)
                {
                    PersistenciaFacilidad.Alta(facilidad, terminal.Codigo, transaccion);
                }
                transaccion.Commit();
            }
            catch (Exception ex)
            {
                transaccion.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                if (oConexion != null)
                    oConexion.Close();
            }
        }

        public void ModificarTerminal(Terminal terminal)
        {
            SqlConnection oConexion = null;
            SqlTransaction transaccion = null;
            try
            {
                oConexion = new SqlConnection(Conexion.Cnn);
                SqlCommand oComando = new SqlCommand("ModificarTerminal", oConexion);
                oComando.CommandType = CommandType.StoredProcedure;
                oComando.Parameters.AddWithValue("@Codigo", terminal.Codigo);
                oComando.Parameters.AddWithValue("@Pais", terminal.Pais);
                oComando.Parameters.AddWithValue("@NombreCiudad", terminal.NombreCiudad);
                SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
                _Retorno.Direction = ParameterDirection.ReturnValue;
                oComando.Parameters.Add(_Retorno);

                oConexion.Open();

                transaccion = oConexion.BeginTransaction();
                oComando.Transaction = transaccion;

                oComando.ExecuteNonQuery();

                int resultado = (int)oComando.Parameters["@Retorno"].Value;
                if (resultado == -1)
                {
                    throw new Exception("El Código de la Terminal no existe.");
                }
                else if (resultado == -2)
                {
                    throw new Exception("El Código de la Terminal debe ser de tres carácteres.");
                }
                else if (resultado != 0)
                {
                    throw new Exception("Ocurrió un error inesperado al modificar la Terminal.");
                }

                //Debo dar de baja a todas las facilidades
                PersistenciaFacilidad.BajaFacilidades(terminal.Codigo, transaccion);

                foreach (Facilidad facilidad in terminal.Facilidades)
                {
                    PersistenciaFacilidad.Alta(facilidad, terminal.Codigo, transaccion);
                }
                transaccion.Commit();
            }
            catch (Exception ex)
            {
                transaccion.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                if (oConexion != null)
                    oConexion.Close();
            }
        }

        public void BajaTerminal(Terminal terminal)
        {
            SqlConnection oConexion = null;
            SqlTransaction transaccion = null;
            try
            {
                oConexion = new SqlConnection(Conexion.Cnn);
                SqlCommand oComando = new SqlCommand("BajaTerminal", oConexion);
                oComando.CommandType = CommandType.StoredProcedure;
                oComando.Parameters.AddWithValue("@Codigo", terminal.Codigo);
                SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
                _Retorno.Direction = ParameterDirection.ReturnValue;
                oComando.Parameters.Add(_Retorno);

                oConexion.Open();

                transaccion = oConexion.BeginTransaction();
                oComando.Transaction = transaccion;

                PersistenciaFacilidad.BajaFacilidades(terminal.Codigo, transaccion);

                oComando.ExecuteNonQuery();

                int resultado = (int)oComando.Parameters["@Retorno"].Value;
                if (resultado == -1)
                {
                    throw new Exception("El Código de la Terminal no existe.");
                }
                else if (resultado != 0)
                {
                    throw new Exception("Ocurrió un error inesperado al dar de baja la Terminal.");
                }
                transaccion.Commit();
            }
            catch (Exception ex)
            {
                transaccion.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                if (oConexion != null)
                    oConexion.Close();
            }
        }

        public List<Terminal> ListaTerminales()
        {
            List<Terminal> listaTerminales = new List<Terminal>();

            SqlConnection oConexion = null;

            try
            {
                oConexion = new SqlConnection(Conexion.Cnn);
                SqlCommand oComando = new SqlCommand("ListaTerminales", oConexion);
                oComando.CommandType = CommandType.StoredProcedure;
                SqlDataReader _Reader;


                oConexion.Open();
                _Reader = oComando.ExecuteReader();
                while (_Reader.Read())
                {
                    Terminal term = new Terminal((string)_Reader["Codigo"], (string)_Reader["NombreCiudad"], (string)_Reader["Pais"]);
                    listaTerminales.Add(term);
                }
                _Reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (oConexion != null)
                    oConexion.Close();
            }

            return listaTerminales;
        }

        public List<Terminal> ListaTerminalesInactivas()
        {
            List<Terminal> listaTerminales = new List<Terminal>();

            SqlConnection oConexion = null;

            try
            {
                oConexion = new SqlConnection(Conexion.Cnn);
                SqlCommand oComando = new SqlCommand("ListaTerminalesInactivas", oConexion);
                oComando.CommandType = CommandType.StoredProcedure;
                SqlDataReader _Reader;


                oConexion.Open();
                _Reader = oComando.ExecuteReader();
                while (_Reader.Read())
                {
                    Terminal term = new Terminal((string)_Reader["Codigo"], (string)_Reader["NombreCiudad"], (string)_Reader["Pais"]);
                    listaTerminales.Add(term);
                }
                _Reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (oConexion != null)
                    oConexion.Close();
            }

            return listaTerminales;
        }

        public Terminal BuscarTerminal(string codigo)
        {
            Terminal terminal = null;

            SqlConnection oConexion = null;

            try
            {
                oConexion = new SqlConnection(Conexion.Cnn);
                SqlCommand oComando = new SqlCommand("BuscarTerminal", oConexion);
                oComando.CommandType = CommandType.StoredProcedure;
                oComando.Parameters.AddWithValue("@Codigo", codigo);
                SqlDataReader _Reader;

                oConexion.Open();
                _Reader = oComando.ExecuteReader();
                
                while (_Reader.Read())
                {
                    if(terminal==null)
                        terminal = new Terminal((string)_Reader["Codigo"], (string)_Reader["NombreCiudad"], (string)_Reader["Pais"]);
                    
                    if (!DBNull.Value.Equals(_Reader["Descripcion"]))
                        terminal.AgregarFacilidad(new Facilidad((string)_Reader["Descripcion"]));
                }
                
                _Reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (oConexion != null)
                    oConexion.Close();
            }

            return terminal;

        }
    }
}
